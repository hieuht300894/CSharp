namespace OnlineShop.Models
{
    public class eCustomer : Master, ICountry, IGender
    {
        public System.DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Logo { get; set; }
        public int IDCountry { get ; set ; }
        public string CodeCountry { get ; set ; }
        public string NameCountry { get ; set ; }
        public int IDGender { get ; set ; }
        public string CodeGender { get ; set ; }
        public string NameGender { get ; set ; }
    }
}
