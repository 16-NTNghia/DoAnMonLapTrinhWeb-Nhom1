using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            string username = User.Identity.Name;
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && 
                                                            p.TrangThaiChapNhan == false).ToListAsync();
            var xeList = await _context.Xes.Where(p => p.MaDiaDiem == userViewModel.DiaDiem.MaDiaDiem &&
                                            !p.YeuCauDatXes.Any(y =>
           y.NgayTra.Date >= userViewModel.datXe.NgayTra.Date &&
           y.TrangThaiChapNhan == true)).ToListAsync();

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
