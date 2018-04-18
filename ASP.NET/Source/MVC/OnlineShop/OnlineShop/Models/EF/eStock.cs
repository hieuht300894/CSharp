

namespace OnlineShop.Models
{
    public class eStock : Master, IProductCategory, IProduct, IUnit, IWarehouse
    {
        public System.DateTime Date { get; set; }
        public int IDProduct { get; set; }
        public string CodeProduct { get; set; }
        public string NameProduct { get; set; }
        public int IDProductCategory { get; set; }
        public string CodeProductCategory { get; set; }
        public string NameProductCategory { get; set; }
        public int IDUnit { get; set; }
        public string CodeUnit { get; set; }
        public string NameUnit { get; set; }
        public int IDWarehouse { get; set; }
        public string CodeWarehouse { get; set; }
        public string NameWarehouse { get; set; }
        public System.DateTime? ExpiredDate { get; set; }
        public int IDType { get; set; }
        public int IDMaster { get; set; }
        public int IDDetail { get; set; }
        public decimal WholeQuantity { get; set; }
        public decimal RetailQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
    }
}
