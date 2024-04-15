using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    [Authorize(Roles = SD.Role_Admin+ ","+SD.Role_Employer)]

    public class BikeController : Controller
    {
        // Trong controller Admin, sử dụng [Authorize(Roles = "RoleName")] để xác thực
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public BikeController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Xe = await _context.Xes.ToListAsync();
            return View(Xe);
        }
        public IActionResult Display(string bienSoXe)
        {
            var xe = _context.Xes.FirstOrDefault(x => x.BienSoXe == bienSoXe);
            if (xe == null)
            {
                return NotFound();
            }
            return View(xe);
        }

        public async Task<IActionResult> Update(string bienSoXe)
        {
            List<SelectListItem> DiaDiems = new List<SelectListItem>();
            List<SelectListItem> HangXes = new List<SelectListItem>();
            List<SelectListItem> LoaiXes = new List<SelectListItem>();
            foreach (var item in _context.DiaDiems)
            {
                DiaDiems.Add(new SelectListItem { Value = item.MaDiaDiem.ToString(), Text = item.TenDiaDiem });
            }
            foreach (var item in _context.HangXes)
            {
                HangXes.Add(new SelectListItem { Value = item.MaHangXe.ToString(), Text = item.TenHang });
            }
            foreach (var item in _context.LoaiXes)
            {
                LoaiXes.Add(new SelectListItem { Value = item.MaLoai.ToString(), Text = item.TenLoai });
            }
            ViewBag.MaDiaDiem = DiaDiems;
            ViewBag.MaHangXe = HangXes;
            ViewBag.MaLoai = LoaiXes;
            if (bienSoXe == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes.FindAsync(bienSoXe);
            return View(xe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Xe xe)
        {
            if (xe != null)
            {
                return NotFound();
            }

            else
            {
                var xeupdate = await _context.Xes.FirstOrDefaultAsync(p => p.BienSoXe == xe.BienSoXe);
                xeupdate.TenXe = xe.TenXe;
                xeupdate.MaLucPhanKhoi = xe.MaLucPhanKhoi;
                xeupdate.MaLoai = xe.MaLoai;
                xeupdate.MaDiaDiem = xe.MaDiaDiem;
                xeupdate.MaHangXe = xe.MaHangXe;
                xeupdate.NamSanXuat = xe.NamSanXuat;
                xeupdate.MoTa = xe.MoTa;
                xeupdate.TrangThai = xe.TrangThai;
                xeupdate.Hide = xe.Hide;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Bike");
            }
        }
        public async Task<IActionResult> Delete(string bienSoXe)
        {
            if (bienSoXe == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes.FirstOrDefaultAsync(m => m.BienSoXe == bienSoXe);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // Action để xác nhận xóa xe
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string bienSoXe)
        {
            var xe = await _context.Xes.FindAsync(bienSoXe);
            if (xe == null)
            {
                return NotFound();
            }

            _context.Xes.Remove(xe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Chuyển hướng đến action Index sau khi xóa thành công
        }
        public async Task<IActionResult> CheckAddBike()
        {
            var xelist = await _context.Xes.Where(p=>p.Hide == true).ToListAsync();
            return View(xelist);
        }
        public async Task<IActionResult> AcceptBike(string bienSoXe)
        {
            if(bienSoXe == null)
            {
                return NotFound();
            }
            var xeupdate = await _context.Xes.FirstOrDefaultAsync(p => p.BienSoXe == bienSoXe);
            if (xeupdate == null)
            {
                return NotFound();
            }
            else
            {             
                xeupdate.Hide = false;
                await _context.SaveChangesAsync();
                return RedirectToAction("CheckAddBike","Bike");
            }
        }
    }

}
