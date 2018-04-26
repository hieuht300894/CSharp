﻿

namespace OnlineShop.Models
{
    public class eOpeningDebtCustomer : Master, ICustomer
    {
        public int IDCustomer { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public System.DateTime DateFrom { get; set; }
        public System.DateTime DateTo { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
