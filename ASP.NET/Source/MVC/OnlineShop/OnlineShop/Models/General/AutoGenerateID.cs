using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models.General
{
    public class AutoGenerateID
    {
        private static Int32 _keyID = 0;
        public static Int32 KeyID { get { return _keyID--; } }
    }
}