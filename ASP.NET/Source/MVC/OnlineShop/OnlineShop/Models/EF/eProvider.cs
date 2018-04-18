

namespace OnlineShop.Models
{
    public class eProvider : Master, ICountry
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ContactBy { get; set; }
        public int IDCountry { get; set; }
        public string CodeCountry { get; set; }
        public string NameCountry { get; set; }
    }
}
