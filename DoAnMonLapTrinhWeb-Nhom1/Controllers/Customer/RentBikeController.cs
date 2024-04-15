using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Customer
{
    public class RentBikeController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;
        public RentBikeController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>  Index(string bienSoXe)
        {
            DateTime Ngaynhan = DateTime.Now;
            DateTime Ngaytra = Ngaynhan.AddDays(1);
            string username = User.Identity.Name;
            YeuCauDatXe datXe = new YeuCauDatXe();
            datXe.NgayNhan = Ngaynhan;
            datXe.NgayTra = Ngaytra;
            List<SelectListItem> DanhGia = new List<SelectListItem>();
            List<SelectListItem> LoaiXe = new List<SelectListItem>();
            List<SelectListItem> DiaDiems = new List<SelectListItem>();
            foreach (var item in _context.DiaDiems)
            {
                DiaDiems.Add(new SelectListItem { Value = item.MaDiaDiem.ToString(), Text = item.TenDiaDiem });
            }
            foreach (var item in _context.LoaiXes)
            {
                LoaiXe.Add(new SelectListItem { Value = item.MaLoai.ToString(), Text = item.TenLoai });
            }
            var xe = await _context.Xes.FirstOrDefaultAsync(p => p.BienSoXe == bienSoXe);
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var xeList = await _context.Xes.Where(p => p.Email == username).ToListAsync();
            var viewmodel = new UserViewModel() {
                Xe = xe,
                datXe = datXe,
                datXeList = datxeList
            };
            return View(viewmodel);
        }

        public async Task<IActionResult> Menu()
        {
            return PartialView();
        }

        // Action để xử lý việc yêu cầu đặt của xe
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(string bienSoXe, UserViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return NotFound();
            }

            else
            {
                var users = new TaiKhoan();
                if (User.Identity.IsAuthenticated)
                {
                    string username = User.Identity.Name;
                    if (username != null)
                    {
                        userViewModel.datXe.BienSoXe = bienSoXe;
                        users = await _context.TaiKhoans.FirstOrDefaultAsync(m => m.Email == username);
                        userViewModel.datXe.Email = users.Email;
                        // Tính số ngày thuê
                        DateTime ngayTra = userViewModel.datXe.NgayTra;
                        DateTime ngayNhan = userViewModel.datXe.NgayNhan;
                        TimeSpan thoiGianThue = ngayTra.Subtract(ngayNhan);
                        int soNgayThue = thoiGianThue.Days;

                        // Lưu số ngày thuê vào đối tượng
                        userViewModel.datXe.SoNgayThue = soNgayThue;

                        // Lưu yêu cầu đặt xe vào cơ sở dữ liệu
                        _context.YeuCauDatXes.Add(userViewModel.datXe);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Order thành công!";
                        return RedirectToAction("Index", "Home");
                    }
                }
                // Gán giá trị Email cho đối tượng yeucaudatxe
                TempData["ErrorMessage"] = "Order thất bại!";
                return View();
            }
        }
    }
}
