using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Dac.Cfg
{
    [Table(Name = "TbConfig")]
    public class ConfigEntity
    {
        [Column(IsPrimaryKey = true)]
        public int No { get; set; }

        [Column]
        public string GroupName { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Value { get; set; }

        [Column]
        public DateTime UpdateTime { get; set; }

        public ConfigEntity()
        {
            this.GroupName = string.Empty;
            this.Name = string.Empty;
            this.Value = string.Empty;
            this.UpdateTime = DateTime.Now;
        }
    }
}
