

namespace OnlineShop.Models
{
    public class xPermissionDetail : Master, IPermissionCategory, IPermission
    {
        public int IDPermissionCategory { get ; set ; }
        public string CodePermissionCategory { get ; set ; }
        public string NamePermissionCategory { get ; set ; }
        public int IDPermission { get ; set ; }
        public string Controller { get ; set ; }
        public string Action { get ; set ; }
        public string Method { get ; set ; }
        public string Template { get ; set ; }
        public string Path { get ; set ; }
    }
}
