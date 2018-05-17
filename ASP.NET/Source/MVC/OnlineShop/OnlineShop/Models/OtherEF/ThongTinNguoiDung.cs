using OnlineShop.Models.EF;

namespace OnlineShop.Models.OtherEF
{
    public class ThongTinNguoiDung
    {
        public xPersonnel xPersonnel { get; set; } = new xPersonnel();
        public xAccount xAccount { get; set; } = new xAccount();
    }

}
