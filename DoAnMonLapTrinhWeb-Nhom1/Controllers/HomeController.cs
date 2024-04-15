using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using X.PagedList;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;

        public HomeController(QuanLyThueXeMayTuLaiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 4;
            if (page == null) page = 1;
			int pageNumber = page ?? 1;
			DateTime Ngaynhan = DateTime.Now;
            DateTime Ngaytra = Ngaynhan.AddDays(1);
            YeuCauDatXe datXe = new YeuCauDatXe();
            datXe.NgayNhan = Ngaynhan;
            datXe.NgayTra = Ngaytra;
            List<SelectListItem> LoaiXe = new List<SelectListItem>();
            List<SelectListItem> DiaDiems = new List<SelectListItem>();
            string username = User.Identity.Name;
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            foreach (var item in _context.DiaDiems)
            {
                DiaDiems.Add(new SelectListItem { Value = item.MaDiaDiem.ToString(), Text = item.TenDiaDiem });
            }
            foreach (var item in _context.LoaiXes)
            {
                LoaiXe.Add(new SelectListItem { Value = item.MaLoai.ToString(), Text = item.TenLoai });
            }
            ViewBag.DiaDiems = DiaDiems;
			var XePageList = await _context.Xes.Where(p => p.Hide == false).ToPagedListAsync(pageNumber, pageSize);
			var Xe = await _context.Xes.Where(p => p.Hide == false).ToListAsync();
			var viewmodel = new UserViewModel()
			{
                XeList = Xe,
                XePageList = XePageList,
                datXe = datXe,
                datXeList = datxeList
            };
            return View(viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            var viewModel = new UserViewModel
            {
                Register = model.Register,
            };
            if (model.Register != null)
            {
                var existingUser = await _context.TaiKhoans.FirstOrDefaultAsync(u => u.Email == model.Register.Email);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại.";
                    return RedirectToAction("Index", "Home");
                }
                model.Register.MatKhau = BCrypt.Net.BCrypt.HashPassword(model.Register.MatKhau);
                model.Register.IdchucVu = 2;
                _context.TaiKhoans.Add(model.Register);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            var viewModel = new UserViewModel
            {
                Register = model.Register,
            };
            if (model.Register != null)
            {
                var user = await _context.TaiKhoans.FirstOrDefaultAsync(u => u.Email == model.Register.Email);
                if (user != null && BCrypt.Net.BCrypt.Verify(model.Register.MatKhau, user.MatKhau))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, user.IdchucVu.ToString()),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Menu()
        {
            
            return PartialView();
        }

        public async Task<IActionResult> Find()
        {
            return PartialView();
        }
        public async Task<IActionResult> BikeRent()
        {
            
            return PartialView();
        }
        public IActionResult NoSee() {
            return View();
        }
    }
}
