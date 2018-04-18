

namespace OnlineShop.Models
{
    public class eOpeningDebtCustomer : Master, ICustomer
    {
        public int IDCustomer { get; set; }
        public string CodeCustomer { get; set; }
        public string NameCustomer { get; set; }
        public System.DateTime DateFrom { get; set; }
        public System.DateTime DateTo { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
