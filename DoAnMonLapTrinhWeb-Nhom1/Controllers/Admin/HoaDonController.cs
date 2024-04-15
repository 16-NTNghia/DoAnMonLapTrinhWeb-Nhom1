using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employer)]
    public class HoaDonController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public HoaDonController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<SelectListItem> PhuongThucThanhToans = new List<SelectListItem>();
            foreach (var item in _context.PhuongThucThanhToans)
            {
                PhuongThucThanhToans.Add(new SelectListItem { Value = item.MaPhuongThuc.ToString(), Text = item.TenPhuongThuc});
            }
            var hoadon = await _context.HoaDons.ToListAsync();
            var viewmodel = new UserViewModel() {
                HoaDonlist = hoadon,
            };
            return View(viewmodel);
        }
        public IActionResult Display(string bienSoXe)
        {
            var hoadon = _context.HoaDons.FirstOrDefault(x => x.BienSoXe == bienSoXe);
            if (hoadon == null)
            {
                return NotFound();
            }
            return View(hoadon);
        }
    }
}
