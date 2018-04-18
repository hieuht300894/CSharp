

namespace OnlineShop.Models
{
    public partial class xAccount : Master, IPersonnel, IPermissionCategory
    {
        public string IPAddress { get; set; } = string.Empty;
        public int IDPersonnel { get; set; }
        public string CodePersonnel { get; set; }
        public string NamePersonnel { get; set; }
        public int IDPermissionCategory { get; set; }
        public string CodePermissionCategory { get; set; }
        public string NamePermissionCategory { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
