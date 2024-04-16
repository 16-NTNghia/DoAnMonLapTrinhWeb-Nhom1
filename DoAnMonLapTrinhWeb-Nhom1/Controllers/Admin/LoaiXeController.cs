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
                TempData["Message"] = "Đã cập nhật thành công.";
                return RedirectToAction("Index", "ManageLoaiXe");
            }
            TempData["Message"] = "Cập nhật không thành công.";
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
                    TempData["Message"] = "Đã tạo mới thành công.";
                    return RedirectToAction("Index", "LoaiXe"); // Chuyển hướng đến trang chính sau khi thêm thành công
                }
            }
            TempData["Message"] = "Tạo mới không thành công.";
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
            var hasForeignKey = await _context.Xes.AnyAsync(b => b.MaLoai == loaixe.MaLoai);

            if (hasForeignKey)
            {
                // Nếu tồn tại khóa ngoại, hiển thị thông báo không xóa được
                TempData["Message"] = "Không thể xóa hãng xe này vì tồn tại khóa ngoại.";

                // Chuyển hướng đến trang Index hoặc trang trước đó
                return RedirectToAction(nameof(Index));
            }
            _context.LoaiXes.Remove(loaixe);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Đã xóa thành công.";
            return RedirectToAction("Index"); // Chuyển hướng đến action Index sau khi xóa thành công
        }
    }
}
