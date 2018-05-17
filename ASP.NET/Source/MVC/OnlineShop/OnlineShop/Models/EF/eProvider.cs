using OnlineShop.Models.General;
using OnlineShop.Models.Interface;

namespace OnlineShop.Models.EF
{
    public class eProvider : Master, ICountry
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ContactBy { get; set; }
        public int IDCountry { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
