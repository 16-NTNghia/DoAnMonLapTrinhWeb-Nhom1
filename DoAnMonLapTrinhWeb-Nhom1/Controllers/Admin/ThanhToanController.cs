using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    public class ThanhToanController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public ThanhToanController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var thanhtoan = await _context.PhuongThucThanhToans.ToListAsync();
            return View(thanhtoan);
        }
       
        [HttpGet]
        public async Task<IActionResult> Edit(int mapttt)
        {
            var pttt = await _context.PhuongThucThanhToans.FirstOrDefaultAsync(x => x.MaPhuongThuc == mapttt);
            return View(pttt);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PhuongThucThanhToan pttt)
        {
            var ptttUpdate = await _context.PhuongThucThanhToans.FirstOrDefaultAsync(x => x.MaPhuongThuc == pttt.MaPhuongThuc);
            if (ptttUpdate != null)
            {
                ptttUpdate.TenPhuongThuc = pttt.TenPhuongThuc;
                _context.SaveChangesAsync();
                return RedirectToAction("Index", "ThanhToan");
            }
            return View(pttt);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PhuongThucThanhToan pttt)
        {

            if (pttt != null)
            {
                if (_context.PhuongThucThanhToans.FirstOrDefaultAsync(p => p.MaPhuongThuc == pttt.MaPhuongThuc) != null)
                {
                    _context.PhuongThucThanhToans.Add(pttt);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "ThanhToan"); // Chuyển hướng đến trang chính sau khi thêm thành công
                }
            }
            return View();
        }
        public async Task<IActionResult> Delete(int mapttt)
        {
            var pttt = await _context.PhuongThucThanhToans.FirstOrDefaultAsync(p => p.MaPhuongThuc == mapttt);
            if (pttt == null)
                return NotFound();
            else
                return View(pttt);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(PhuongThucThanhToan pttt)
        {
            _context.PhuongThucThanhToans.Remove(pttt);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); // Chuyển hướng đến action Index sau khi xóa thành công
        }
    }
}
