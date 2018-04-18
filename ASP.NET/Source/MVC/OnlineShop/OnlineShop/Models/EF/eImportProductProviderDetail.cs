

namespace OnlineShop.Models
{
    public class eImportProductProviderDetail : Master, IImportProductProvider, IProductCategory, IProduct, IUnit, IWarehouse
    {
        public int IDProduct { get; set; }
        public string CodeProduct { get; set; }
        public string NameProduct { get; set; }
        public int IDUnit { get; set; }
        public string CodeUnit { get; set; }
        public string NameUnit { get; set; }
        public int IDWarehouse { get; set; }
        public string CodeWarehouse { get; set; }
        public string NameWarehouse { get; set; }
        public System.DateTime? ExpiredDate { get; set; }
        public decimal WholeQuantity { get; set; }
        public decimal RetailQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public decimal VAT { get; set; }
        public decimal Discount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int IDProductCategory { get; set; }
        public string CodeProductCategory { get; set; }
        public string NameProductCategory { get; set; }
        public string CodeImportProductProvider { get; set; }
        public string NameImportProductProvider { get; set; }
        public int IDImportProductProvider { get; set; }
    }
}
