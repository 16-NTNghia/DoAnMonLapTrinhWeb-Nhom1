using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employer)]
    public class HangXeController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public HangXeController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var hangXe = await _context.HangXes.ToListAsync();
            return View(hangXe);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int mahangxe)
        {
            var hangXe = await _context.HangXes.FirstOrDefaultAsync(x => x.MaHangXe == mahangxe);
            return View(hangXe);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(HangXe hangXe)
        {
            var hangXeUpdate = await _context.HangXes.FirstOrDefaultAsync(x => x.MaHangXe == hangXe.MaHangXe);
            if (hangXeUpdate != null)
            {
                hangXeUpdate.TenHang = hangXe.TenHang;
                _context.SaveChangesAsync();
                TempData["Message"] = "Đã cập nhật thành công.";
                return RedirectToAction("Index", "HangXe");
            }
            TempData["Message"] = "Cập nhật không thành công.";
            return View(hangXe);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HangXe hangxe)
        {

            if (hangxe != null)
            {
                if (_context.HangXes.FirstOrDefaultAsync(p => p.MaHangXe == hangxe.MaHangXe) != null)
                {
                    _context.HangXes.Add(hangxe);
                    _context.SaveChanges();
                    TempData["Message"] = "Đã tạo mới thành công.";
                    return RedirectToAction("Index", "HangXe"); // Chuyển hướng đến trang chính sau khi thêm thành công
                }
            }

            return View();
        }
        public async Task<IActionResult> Delete(int mahangxe)
        {
            var hx = await _context.HangXes.FirstOrDefaultAsync(p => p.MaHangXe  == mahangxe);
            if (hx == null)
                return NotFound();
            else
                return View(hx);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(HangXe hangxe)
        {
            var hasForeignKey = await _context.Xes.AnyAsync(b => b.MaHangXe == hangxe.MaHangXe);

            if (hasForeignKey)
            {
                // Nếu tồn tại khóa ngoại, hiển thị thông báo không xóa được
                TempData["Message"] = "Không thể xóa hãng xe này vì tồn tại khóa ngoại.";

                // Chuyển hướng đến trang Index hoặc trang trước đó
                return RedirectToAction(nameof(Index));
            }
            _context.HangXes.Remove(hangxe);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Đã xóa thành công";
            return RedirectToAction("Index"); // Chuyển hướng đến action Index sau khi xóa thành công
        }
    }
}
