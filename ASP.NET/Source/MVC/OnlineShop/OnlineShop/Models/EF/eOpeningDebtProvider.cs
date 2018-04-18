

namespace OnlineShop.Models
{
    public class eOpeningDebtProvider : Master, IProvider
    {
        public int IDProvider { get; set; }
        public string CodeProvider { get; set; }
        public string NameProvider { get; set; }
        public System.DateTime DateFrom { get; set; }
        public System.DateTime DateTo { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
