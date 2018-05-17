using OnlineShop.Models.General;

namespace OnlineShop.Models.EF
{
    public class eCurrency : Master
    {
        public string CodeDigital { get; set; }
        public string Prefix { get; set; }
        public byte[] Logo { get; set; }
    }
}
