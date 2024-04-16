using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Customer
{
    public class FindBikeController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public FindBikeController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(UserViewModel userViewModel)
        {
			List<SelectListItem> DiaDiems = new List<SelectListItem>();
			foreach (var item in _context.DiaDiems)
			{
				DiaDiems.Add(new SelectListItem { Value = item.MaDiaDiem.ToString(), Text = item.TenDiaDiem });
			}
			string username = User.Identity.Name;
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && 
                                                            p.TrangThaiChapNhan == false).ToListAsync();
            var xeList = await _context.Xes.Where(p => p.MaDiaDiem == userViewModel.DiaDiem.MaDiaDiem &&
                                            !p.YeuCauDatXes.Any(y =>
			                                y.NgayNhan.Date <= userViewModel.datXe.NgayTra.Date &&
				                            y.NgayTra.Date >= userViewModel.datXe.NgayNhan.Date &&
                                            y.TrangThaiChapNhan == true) && p.Hide == false).ToListAsync();

            var viewmodel = new UserViewModel
            {
                XeList = xeList,
                datXeList = datxeList
            };
            return View(viewmodel);
        }

        public async Task<IActionResult> Menu()
        {
            return PartialView();
        }
    }
}
