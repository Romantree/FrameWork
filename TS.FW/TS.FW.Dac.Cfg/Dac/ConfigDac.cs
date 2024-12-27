using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;

namespace TS.FW.Dac.Cfg
{
    internal class ConfigDac : IDacBase<ConfigEntity>
    {
        public ConfigDac() : base("CONFIG_DB")
        {

        }

        public ConfigEntity SelectedForName(string group, string name)
        {
            return this.Table.SingleOrDefault(t => t.GroupName == group && t.Name == name);
        }

        public void Insert(ConfigEntity entity)
        {
            if (entity == null) throw new NullReferenceException("Entity 정보가 Null 입니다.");

            var query = @"INSERT INTO TbConfig (GroupName, Name, Value, UpdateTime) VALUES ({0}, {1}, {2}, {3})";

            this.Context.ExecuteCommand(query, entity.GroupName, entity.Name, entity.Value, entity.UpdateTime);
        }

        public void Update(string group, string name, string value)
        {
            var query = @"UPDATE TbConfig
                        SET
                            Value = {2},
                            UpdateTime = {3}
                        WHERE
                            GroupName = {0} AND Name = {1}";

            this.Context.ExecuteCommand(query, group, name, value, DateTime.Now);
        }
    }
}
