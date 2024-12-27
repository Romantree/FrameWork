using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Dac.Core;

namespace TS.FW.WpfLanguage
{
    internal class LanguageDac : IDacBase<LanguageEntity>
    {
        public LanguageDac() : base("LANGUAGE_DB") { }

        public List<LanguageEntity> SelectedAll()
        {
            return this.Table.ToList();
        }

        public LanguageEntity Selected(string code)
        {
            return this.Table.SingleOrDefault(t => t.Code == code);
        }

        public void Insert(LanguageEntity item)
        {
            if (item == null) throw new NullReferenceException();

            var query = @"INSERT INTO TbLanguage
                        VALUES
                            ({0}, {1}, {2}, {3})";

            this.Context.ExecuteCommand(query, item.Code, item.Korea, item.English, item.China);
        }

        public void Update(LanguageEntity item)
        {
            if (item == null) throw new NullReferenceException();

            var query = @"UPDATE TbLanguage
                        SET
                            Korea = {1},
                            English = {2},
                            China = {3}
                        WHERE
                            Code = {0}";

            this.Context.ExecuteCommand(query, item.Code, item.Korea, item.English, item.China);
        }

        public void Delete(string code)
        {
            var query = @"DELETE FROM TbLanguage WHERE Code = {0}";

            this.Context.ExecuteCommand(query, code);
        }

        public void DeleteAll()
        {
            var query = @"DELETE FROM TbLanguage";

            this.Context.ExecuteCommand(query);
        }
    }
}
