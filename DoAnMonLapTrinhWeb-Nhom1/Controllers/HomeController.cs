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
using Microsoft.Extensions.Caching.Memory;
using System.Net.Mail;
using System.Net;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers
{
    
    public class HomeController : Controller
    {
		private readonly QuanLyThueXeMayTuLaiContext _context;
		private readonly IConfiguration _configuration;
		private readonly IMemoryCache _cache;

		public HomeController(QuanLyThueXeMayTuLaiContext context, IConfiguration configuration, IMemoryCache cache)
		{
			_context = context;
			_configuration = configuration;
			_cache = cache;
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
            List<SelectListItem> DiaDiems = new List<SelectListItem>();
            string username = User.Identity.Name;
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            foreach (var item in _context.DiaDiems)
            {
                DiaDiems.Add(new SelectListItem { Value = item.MaDiaDiem.ToString(), Text = item.TenDiaDiem });
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
		public void SendMail(string to, string subject, string content)
		{
			var from = _configuration["SMTPConfig:SenderAddress"];
			var displayname = _configuration["SMTPConfig:SenderDisplayName"];
			var pass = _configuration["SMTPConfig:Password"];


			var fromAddress = new MailAddress(from, displayname);
			var toAddress = new MailAddress(to);
			var smtp = new SmtpClient
			{
				Host = _configuration["SMTPConfig:Host"],
				Port = int.Parse(_configuration["SMTPConfig:Port"]),
				EnableSsl = bool.Parse(_configuration["SMTPConfig:EnableSsl"]),
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, pass)
			};

			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = content,
				IsBodyHtml = true
			})
			{
				smtp.Send(message);
			}
		}
		public async Task<IActionResult> forgotpassword()
		{
			var viewmodel = new UserViewModel();
			return View(viewmodel);
		}
		[HttpPost]
		public async Task<IActionResult> forgotpassword(UserViewModel model)
		{
			var viewModel = new UserViewModel
			{
				Register = model.Register,
			};
			var existingUser = await _context.TaiKhoans.FirstOrDefaultAsync(u => u.Email == model.Register.Email);
			if (existingUser != null)
			{
				string subject = "Forgot Password?";
				string resetToken = Guid.NewGuid().ToString();
				var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
				_cache.Set(resetToken, existingUser.Email, cacheOptions);

				// Tạo đường dẫn reset password với token
				string resetPasswordUrl = Url.Action("Resetpassword", "Home", new { token = resetToken }, Request.Scheme);

				//string resetPasswordUrl = Url.Action("Resetpassword", "Home", new { email = existingUser.Email }, Request.Scheme);
				string body = $@"
                            <html>
                            <body>
                                <h2>Are you forgot password?</h2>
                                <p>Please click the button below to reset password:</p>
                                <a href=""{resetPasswordUrl}""><button type=""submit"">Reset password</button></a>
                            </body>
                            </html>";
				SendMail(existingUser.Email, subject, body);
				return RedirectToAction("Index", "Home");
			}
			return View(viewModel);
		}

		public async Task<IActionResult> Resetpassword(string token)
		{
			if (_cache.TryGetValue(token, out string userEmail))
			{
				// Xóa token từ bộ nhớ cache sau khi sử dụng
				_cache.Remove(token);

				// Lấy thông tin tài khoản cần đặt lại mật khẩu
				var reset = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Email == userEmail);
				if (reset != null)
				{
					var viewModel = new UserViewModel
					{
						Register = reset
					};
					return View(viewModel);
				}
			}
			return RedirectToAction("Index", "Home");
		}
		[HttpPost]
		public async Task<IActionResult> Resetpassword(UserViewModel model)
		{
			var viewModel = new UserViewModel
			{
				Register = model.Register,
			};
			var existingUser = await _context.TaiKhoans.FirstOrDefaultAsync(u => u.Email == model.Register.Email);
			if (existingUser != null)
			{
				existingUser.MatKhau = BCrypt.Net.BCrypt.HashPassword(model.Register.MatKhau);
				_context.SaveChangesAsync();
				return RedirectToAction("Index", "Home");
			}
			return View(viewModel);
		}

	}
}
