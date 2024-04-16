using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    [Authorize(Roles = SD.Role_Admin + "," +SD.Role_Employer)]
    public class PromoController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public PromoController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var khuyenmai = await _context.KhuyenMais.ToListAsync();
            return View(khuyenmai);
        }
        public IActionResult Display(int makhuyenmai)
        {
            var khuyenmai = _context.KhuyenMais.FirstOrDefault(x => x.MaiKhuyenMai == makhuyenmai);
            if (khuyenmai == null)
            {
                return NotFound();
            }
            return View(khuyenmai);
        }
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(KhuyenMai khuyenmai)
        {
            var users = new TaiKhoan();
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (username != null)
                {
                    users = await _context.TaiKhoans.FirstOrDefaultAsync(m => m.Email == username);
                }
            }

            if (khuyenmai != null)
            {
                if (_context.KhuyenMais.FirstOrDefaultAsync(p => p.MaiKhuyenMai == khuyenmai.MaiKhuyenMai) != null)
                {
                    _context.KhuyenMais.Add(khuyenmai);
                    _context.SaveChanges();
                    TempData["Message"] = "Đã tạo mới thành công.";
                    return RedirectToAction("Index", "Promo"); // Chuyển hướng đến trang chính sau khi thêm thành công
                }
            }
            TempData["Message"] = "tạo mới không thành công.";
            return View();
        }
        public async Task<IActionResult> Update(int makhuyenmai)
        {
            if (makhuyenmai == null)
            {
                return NotFound();
            }

            var km = await _context.KhuyenMais.FindAsync(makhuyenmai);
            return View(km);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int makhuyenmai, KhuyenMai khuyenmai)
        {
            var kmupdate = await _context.KhuyenMais.FirstOrDefaultAsync(p => p.MaiKhuyenMai == khuyenmai.MaiKhuyenMai);
            if (kmupdate != null)
            {
                kmupdate.TenKhuyenMai = khuyenmai.TenKhuyenMai;
                kmupdate.NoiDungKhuyenMai = khuyenmai.NoiDungKhuyenMai;
                kmupdate.NgayKhuyenMai = khuyenmai.NgayKhuyenMai;
                kmupdate.PhanTramKhhuyenMai = khuyenmai.PhanTramKhhuyenMai;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Đã  cập nhật thành công.";
                return RedirectToAction("Index", "Promo");
            }
            TempData["Message"] = "Cập nhật không thành công.";
            return View(khuyenmai);
        }
        public async Task<IActionResult> Delete(int makhuyenmai)
        {
            if (makhuyenmai == null)
            {
                return NotFound();
            }

            var km = await _context.KhuyenMais.FirstOrDefaultAsync(m => m.MaiKhuyenMai == makhuyenmai);
            if (km == null)
            {
                return NotFound();
            }

            return View(km);
        }

        // Action để xác nhận xóa xe
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(KhuyenMai khuyenmai)
        {
            var km = await _context.KhuyenMais.FirstOrDefaultAsync(m => m.MaiKhuyenMai == khuyenmai.MaiKhuyenMai);
            if (km == null)
            {
                return NotFound();
            }

            _context.KhuyenMais.Remove(km);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Đã xóa thành công.";
            return RedirectToAction(nameof(Index)); // Chuyển hướng đến action Index sau khi xóa thành công
        }
    }
}
