using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employer)]
    public class LoaiXeController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public LoaiXeController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var loaiXe = await _context.LoaiXes.ToListAsync();
            return View(loaiXe);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int maloai)
        {
            var loaiXe = await _context.LoaiXes.FirstOrDefaultAsync(x => x.MaLoai == maloai);
            return View(loaiXe);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LoaiXe loaiXe)
        {
            var loaiXeUpdate = await _context.LoaiXes.FirstOrDefaultAsync(x => x.MaLoai == loaiXe.MaLoai);
            if (loaiXeUpdate != null)
            {
                loaiXeUpdate.TenLoai = loaiXe.TenLoai;
                _context.SaveChangesAsync();
                return RedirectToAction("Index", "ManageLoaiXe");
            }
            return View(loaiXe);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LoaiXe loaixe)
        {

            if (loaixe != null)
            {
                if (_context.LoaiXes.FirstOrDefaultAsync(p => p.MaLoai == loaixe.MaLoai) != null)
                {
                    _context.LoaiXes.Add(loaixe);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "ManageLoaiXe"); // Chuyển hướng đến trang chính sau khi thêm thành công
                }
            }
            return View();
        }
        public async Task<IActionResult> Delete(int maloai)
        {
            var lx = await _context.LoaiXes.FirstOrDefaultAsync(p=>p.MaLoai == maloai);
            if (lx == null)
                return NotFound();
            else
                return View(lx);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(LoaiXe loaixe)
        {
            _context.LoaiXes.Remove(loaixe);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); // Chuyển hướng đến action Index sau khi xóa thành công
        }
    }
}
