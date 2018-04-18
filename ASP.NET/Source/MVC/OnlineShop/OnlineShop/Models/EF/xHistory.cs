

namespace OnlineShop.Models
{
    public class xHistory : Master
    {
        public string Action { get; set; }
        public string Table { get; set; }
        public string OldRecord { get; set; }
        public string NewRecord { get; set; }
    }
}
