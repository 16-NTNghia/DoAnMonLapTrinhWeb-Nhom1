using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Customer
{
    public class UserInforController : Controller
    {
        public readonly QuanLyThueXeMayTuLaiContext _content;
        private QuanLyThueXeMayTuLaiContext _context;

        public UserInforController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/Images/avatar", image.FileName); // Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/Images/avatar/" + image.FileName; // Trả về đường dẫn tương đối
        }
        public async Task<IActionResult> Update(string email)
        {
            if (email == null)
            {
                return NotFound();
            }
            string username = User.Identity.Name;
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var mail = await _context.TaiKhoans.FindAsync(email);
            var xeList = await _context.Xes.Where(p => p.Email == mail.Email).ToListAsync();
            var viewmodel = new UserViewModel() {
                TaiKhoan  = mail,
                datXeList=datxeList,
                XeList = xeList
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TaiKhoan taikhoan, IFormFile Avatar)
        {

            if (taikhoan == null)
            {
                return NotFound();
            }

            else
            {
                var tkupdate = await _context.TaiKhoans.FirstOrDefaultAsync(p => p.Email == taikhoan.Email);
                tkupdate.TenNguoiDung = taikhoan.TenNguoiDung;
                tkupdate.Sdt = taikhoan.Sdt;
                tkupdate.DiaChi = taikhoan.DiaChi;
                tkupdate.NgaySinh = taikhoan.NgaySinh;
                tkupdate.Gplx = taikhoan.Gplx;
                if (Avatar != null)
                {
                    if (!string.IsNullOrEmpty(tkupdate.Avarta))
                    {
                        DeleteImage(tkupdate.Avarta);
                    }
                    tkupdate.Avarta = await SaveImage(Avatar);
                }
                await _context.SaveChangesAsync();
                TempData["Message"] = "Cập nhật không thành công.";
                return RedirectToAction("Update", "UserInfor", new { email = taikhoan.Email });
            }
        }
        public void DeleteImage(string imagePath)
        {
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        public IActionResult Menu()
        {
            return PartialView();
        }

        public async Task<IActionResult> ChangePassword(string email) {
            string username = User.Identity.Name;
            if (email == null)
            {
                return NotFound();
            }

            var mail = await _context.TaiKhoans.FindAsync(email);
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var xeList = await _context.Xes.Where(p => p.Email == username).ToListAsync();
            var viewmodel = new UserViewModel
            {
                XeList = xeList,
                datXeList = datxeList
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(TaiKhoan taikhoan, string oldPassword, string newPassword, string confirmPassword)
        {
            if (taikhoan == null || oldPassword == null || newPassword == null || confirmPassword == null)
            {
                return NotFound();
            }

            var user = await _context.TaiKhoans.FirstOrDefaultAsync(p => p.Email == taikhoan.Email);

            if (user == null)
            {
                return NotFound();
            }

            // Kiểm tra mật khẩu cũ có đúng không
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.MatKhau))
            {
                TempData["Message"] = "Mật khẩu cũ không đúng.";
                return RedirectToAction("ChangePassword", "UserInfor", new { email = taikhoan.Email });
            }

            // Hash mật khẩu mới và lưu vào cơ sở dữ liệu
            user.MatKhau = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            TempData["Message"] = "đổi mật khẩu thành công.";
            return RedirectToAction("Index", "Home");
        }
    }
}
