using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop
{
    public static class clsEnum
    {
        public enum fLogin
        {
            NotFound = 0,
            Disable = 1,
            Success
        }

        public enum fStatus
        {
            Add = 1,
            Edit = 2
        }
    }
}