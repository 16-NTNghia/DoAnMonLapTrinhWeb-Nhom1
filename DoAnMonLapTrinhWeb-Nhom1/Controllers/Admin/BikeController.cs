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
