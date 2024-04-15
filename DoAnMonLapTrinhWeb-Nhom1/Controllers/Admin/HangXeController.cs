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
                return RedirectToAction("Index", "HangXe");
            }
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
            _context.HangXes.Remove(hangxe);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); // Chuyển hướng đến action Index sau khi xóa thành công
        }
    }
}
