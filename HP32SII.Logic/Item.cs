
using SQLite;

namespace HP32SII.Logic
{
    [Table("Items")]
    public class Item
    {
        [PrimaryKey]
        public string Key { get; set; }
        public double Value { get; set; }
    }
}
