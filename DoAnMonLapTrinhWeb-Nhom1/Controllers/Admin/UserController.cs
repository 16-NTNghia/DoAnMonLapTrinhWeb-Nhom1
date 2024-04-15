using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    [Authorize(Roles = SD.Role_Admin)]

    public class UserController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public UserController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var taikhoan = await _context.TaiKhoans.ToListAsync();
            return View(taikhoan);
        }
        public IActionResult Display(string Email)
        {
            var taikhoan = _context.TaiKhoans.FirstOrDefault(x => x.Email == Email);
            if (taikhoan == null)
            {
                return NotFound();
            }
            return View(taikhoan);
        }

        public async Task<IActionResult> Add()
        {
            List<SelectListItem> ChucVus = new List<SelectListItem>();
            foreach (var item in _context.ChucVus)
            {
                ChucVus.Add(new SelectListItem { Value = item.IdchucVu.ToString(), Text = item.TenChucVu });
            }

            ViewBag.IdchucVu = ChucVus;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaiKhoan taiKhoan, IFormFile Avatar)
        {

            List<SelectListItem> ChucVus = new List<SelectListItem>();
            foreach (var item in _context.ChucVus)
            {
                ChucVus.Add(new SelectListItem { Value = item.IdchucVu.ToString(), Text = item.TenChucVu });
            }
            ViewBag.IdchucVu = ChucVus;
            var existingUser = await _context.TaiKhoans.FirstOrDefaultAsync(u => u.Email == taiKhoan.Email);
            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại.";
                return RedirectToAction("Index", "Home");
            }
            taiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoan.MatKhau);
            if (Avatar != null)
            {
                // Lưu hình ảnh đại diện
                taiKhoan.Avarta = await SaveImage(Avatar);
            }
            if (Avatar != null)
            {
                // Lưu hình ảnh đại diện
                taiKhoan.Avarta = await SaveImage(Avatar);
            }
            if (taiKhoan != null)
            {
                
                if (_context.Xes.FirstOrDefaultAsync(p => p.Email == taiKhoan.Email) != null)
                {
                    _context.TaiKhoans.Add(taiKhoan);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "User"); // Chuyển hướng đến trang chính sau khi thêm thành công
                }
            }
            return View(); // Trả về view với model nếu có lỗi
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

        public void DeleteImage(string imagePath)
        {
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}
