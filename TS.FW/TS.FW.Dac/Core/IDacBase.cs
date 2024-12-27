using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;

namespace TS.FW.Dac.Core
{

    /// <summary>
    /// 데이터 베이스 정의 추상화 클래스
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IDacBase : IDisposable
    {
        private static Dictionary<string, DbConnection> _connectionList = new Dictionary<string, DbConnection>();

        static IDacBase()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            try
            {
                foreach (var connection in _connectionList)
                {
                    connection.Value.Close();
                    connection.Value.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(IDacBase), ex);
            }
        }

        protected DbConnection Connection { get; private set; }

        protected DataContext Context { get; private set; }

        public IDacBase(string connectionString)
        {
            var source = ConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;
            var config = source.ConnectionStrings[connectionString];

            this.InitConnection(config);
        }

        private void InitConnection(ConnectionStringSettings config)
        {
            var provider = config.ProviderName.Trim();

            switch (provider)
            {
                case "System.Data.SQLite":
                    {
                        if (_connectionList.ContainsKey(config.ConnectionString))
                        {
                            this.Connection = _connectionList[config.ConnectionString];
                        }
                        else
                        {
                            this.Connection = new SQLiteConnection(config.ConnectionString);
                            _connectionList.Add(config.ConnectionString, this.Connection);
                            this.Connection.Open();
                        }
                    }
                    break;
                case "MySql.Data":
                    {
                        this.Connection = new MySqlConnection(config.ConnectionString);
                        this.Connection.Open();
                    }
                    break;
                case "System.Data.SqlClient":
                    {
                        this.Connection = new SqlConnection(config.ConnectionString);
                        this.Connection.Open();
                    }
                    break;
                default: throw new NotImplementedException(string.Format("지원하지 않은 Database Provider 입니다. '{0}'", provider));
            }

            this.Context = new DataContext(this.Connection);
        }

        protected int MySqlExecuteNonQuery(string query, params TsSqlParameter[] parameters)
        {
            using (var command = new MySqlCommand(query, (MySqlConnection)this.Context.Connection, (MySqlTransaction)this.Context.Transaction))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(new MySqlParameter(parameter.ParameterName, parameter.MySqlDbType) { Value = parameter.Value });
                    }
                }

                return command.ExecuteNonQuery();
            }
        }

        protected IEnumerable<T> ExecuteQuery<T>(string command, List<TsSqlParameter> parameters)
        {
            var query = string.Format("{0} {1}"
                , command
                , string.Join(", ", parameters.Select((t, i) => string.Format("{0}={1}", t.ParameterName, ("{" + i + "}")))));

            return this.Context.ExecuteQuery<T>(query, parameters.Select(t => t.Value).ToArray());
        }

        protected int ExecuteCommand(string command, List<TsSqlParameter> parameters)
        {
            var query = string.Format("{0} {1}"
                , command
                , string.Join(", ", parameters.Select((t, i) => string.Format("{0}={1}", t.ParameterName, ("{" + i + "}")))));

            return this.Context.ExecuteCommand(query, parameters.Select(t => t.Value).ToArray());
        }

        protected IEnumerable<T> ExecuteQuery<T>(string command)
        {
            return this.Context.ExecuteQuery<T>(command);
        }

        protected int ExecuteCommand(string command)
        {
            return this.Context.ExecuteCommand(command);
        }

        protected TsSqlParameterCollection CreateParameter() => new TsSqlParameterCollection();

        protected DateTime? GetDateTimeNow()
        {
            var list = this.Context.ExecuteQuery<DateTime>("SELECT NOW(6)").ToList();
            if (list == null || list.Count <= 0) return null;

            return list.First();
        }

        protected void SubmitChanges()
        {
            this.Context.SubmitChanges();
        }

        public void Dispose()
        {
            if (this.Context != null) this.Context.Dispose();
            this.Context = null;

            if (this.Connection is MySqlConnection)
            {
                this.Connection.Close();
                this.Connection.Dispose();
                this.Connection = null;
            }

            if(this.Connection is SqlConnection)
            {
                this.Connection.Close();
                this.Connection.Dispose();
                this.Connection = null;
            }
        }
    }




    /// <summary>
    /// 데이터 베이스 정의 추상화 클래스
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IDacBase<T> : IDacBase where T : class
    {
        protected Table<T> Table
        {
            get
            {
                return this.Context.GetTable<T>();
            }
        }

        public IDacBase(string connectionString) : base(connectionString)
        {
        }

        protected void InsertOnSubmit(T model)
        {
            this.Table.InsertOnSubmit(model);
            this.SubmitChanges();
        }

        protected void InsertAllOnSubmi(IEnumerable<T> modelList)
        {
            this.Table.InsertAllOnSubmit(modelList);
            this.SubmitChanges();
        }

        protected void DeleteOnSubmit(T model)
        {
            this.Table.DeleteOnSubmit(model);
            this.SubmitChanges();
        }

        protected void DeleteAllOnSubmit(IEnumerable<T> model)
        {
            this.Table.DeleteAllOnSubmit(model);
            this.SubmitChanges();
        }

        protected int AutoIncrement(Func<T, int> keySelector)
        {
            return this.Table.Count() <= 0 ? 1 : this.Table.Max(keySelector) + 1;
        }

        //protected T GetModel(Expression<Func<T, bool>> predicate)
        //{
        //    return this.Table.SingleOrDefault(predicate);
        //}

        //protected IEnumerable<T> GetModels(Expression<Func<T, bool>> predicate)
        //{
        //    return this.Table.Where(predicate);
        //}
    }
}
