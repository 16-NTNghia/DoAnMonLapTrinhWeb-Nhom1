using DoAnMonLapTrinhWeb_Nhom1.Models;
using X.PagedList;

namespace DoAnMonLapTrinhWeb_Nhom1.ViewModels
{
    public class UserViewModel
    {
        public TaiKhoan Register { get; set; }
        public UserViewModel()
        {
            Register = new TaiKhoan();
        }
        public YeuCauDatXe datXe { get; set; }


        public List<YeuCauDatXe> datXeList { get; set; }

        public HoaDon HoaDon { get; set; }
        
        public List<HoaDon> HoaDonlist { get; set; }

        public TaiKhoan TaiKhoan { get; set; }

        public Xe Xe { get; set; }
        public List<Xe> XeList { get; set; }
		public IPagedList<Xe> XePageList { get; set; }

		public DiaDiem DiaDiem { get; set; }

        public List<DiaDiem> DiaDiemList { get; set; }

        public string AvatarUrl { get; set; }

        // Phương thức để lấy URL của avatar từ đối tượng TaiKhoan
        
        public int TongNgayThue()
        {
            TimeSpan thoiGianThue = datXe.NgayTra.Date - datXe.NgayNhan.Date;
            return (int)thoiGianThue.TotalDays;
        }
        public float TinhTong()
        {
            return (float)(TongNgayThue() * Xe.GiaThue);
        }
    }
}
