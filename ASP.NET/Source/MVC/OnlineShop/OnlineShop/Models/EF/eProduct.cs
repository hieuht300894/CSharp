﻿namespace OnlineShop.Models
{
    public class eProduct : Master, IUnit
    {
        //[NotCodepped]
        //public Color Color { get; set; }
        public int ColorHex { get; set; }
        public string ColorName { get; set; }
        public string Size { get; set; }
        public int IDUnit { get; set; }
        public string CodeUnit { get; set; }
        public string NameUnit { get; set; }
    }
}