using System.Data.Linq.Mapping;

namespace TS.FW.WpfLanguage
{
    [Table(Name = "TbLanguage")]
    public class LanguageEntity
    {
        [Column(IsPrimaryKey = true)]
        public string Code { get; set; }

        [Column]
        public string Korea { get; set; } = string.Empty;

        [Column]
        public string English { get; set; } = string.Empty;

        [Column]
        public string China { get; set; } = string.Empty;
    }
}
