

namespace OnlineShop.Models
{
    public class eCountry : Master, ICountry, IType
    {
        public string PostalCode { get; set; }
        public string LocationCode { get; set; }
        public string ZipCode { get; set; }
        public int IDCountry { get; set; }
        public string CodeCountry { get ; set ; }
        public string NameCountry { get ; set ; }
        public int IDType { get ; set ; }
        public string CodeType { get ; set ; }
        public string NameType { get ; set ; }
    }
}
