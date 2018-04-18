

namespace OnlineShop.Models
{
    public class eExchangeUnit : Master
    {
        public int IDCurrentUnit { get; set; }
        public string CurrentUnit { get; set; }
        public int IDExchangeUnit { get; set; }
        public string ExchangeUnit { get; set; }
        public decimal ExchangeValue { get; set; }
    }
}
