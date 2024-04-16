using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    [Authorize(Roles = SD.Role_Admin)]
    public class DiaDiemController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public DiaDiemController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }

        // GET: AdminDiaDiem
        public async Task<IActionResult> Index()
        {
            var diaDiems = await _context.DiaDiems.ToListAsync();
            return View(diaDiems);
        }

        // GET: AdminDiaDiem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminDiaDiem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDiaDiem,TenDiaDiem")] DiaDiem diaDiem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diaDiem);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Đã tạo mới thành công.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "Tạo mới không thành công.";
            return View(diaDiem);
        }

        // GET: AdminDiaDiem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diaDiem = await _context.DiaDiems.FindAsync(id);
            if (diaDiem == null)
            {
                return NotFound();
            }
            return View(diaDiem);
        }

        // POST: AdminDiaDiem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDiaDiem,TenDiaDiem")] DiaDiem diaDiem)
        {
            if (id != diaDiem.MaDiaDiem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diaDiem);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Đã cập nhật thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiaDiemExists(diaDiem.MaDiaDiem))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["Message"] = "Cập nhật không thành công.";
            return View(diaDiem);
        }

        // GET: AdminDiaDiem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diaDiem = await _context.DiaDiems
                .FirstOrDefaultAsync(m => m.MaDiaDiem == id);
            if (diaDiem == null)
            {
                return NotFound();
            }

            return View(diaDiem);
        }

        // POST: AdminDiaDiem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DiaDiem diadiem)
        {
            var hasForeignKey = await _context.Xes.AnyAsync(b => b.MaDiaDiem == diadiem.MaDiaDiem);

            if (hasForeignKey)
            {
                // Nếu tồn tại khóa ngoại, hiển thị thông báo không xóa được
                TempData["Message"] = "Không thể xóa địa điểm này vì tồn tại khóa ngoại.";

                // Chuyển hướng đến trang Index hoặc trang trước đó
                return RedirectToAction(nameof(Index));
            }

            // Nếu không có khóa ngoại tồn tại, tiến hành xóa đối tượng DiaDiem
            var diaDiem = await _context.DiaDiems.FirstOrDefaultAsync(p => p.MaDiaDiem == diadiem.MaDiaDiem);
            _context.DiaDiems.Remove(diaDiem);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Đã xóa địa điểm thành công.";
            return RedirectToAction(nameof(Index));
        }

        private bool DiaDiemExists(int id)
        {
            return _context.DiaDiems.Any(e => e.MaDiaDiem == id);
        }
    }
}