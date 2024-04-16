using System.Net.Mail;
using System.Net;
using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Customer
{
    [Authorize(Roles = SD.Role_Customer)]
    public class CustomerBikeController : Controller
    {
        private readonly QuanLyThueXeMayTuLaiContext _context;
		private readonly IConfiguration _configuration;
		private readonly IMemoryCache _cache;

		public CustomerBikeController(QuanLyThueXeMayTuLaiContext context, IConfiguration configuration, IMemoryCache cache)
		{
			_context = context;
			_configuration = configuration;
			_cache = cache;
		}
		public async Task<IActionResult> Index(TaiKhoan taikhoan)
        {
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (username != null )
                {
                    // Tìm kiếm thông tin tài khoản
                    var user = await _context.TaiKhoans.FirstOrDefaultAsync(m => m.Email == username);

                    // Nếu tài khoản tồn tại, thì tìm danh sách xe của tài khoản đó

                    if (user != null)
                    {
                        var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
                        var xeList = await _context.Xes.Where(p => p.Email == user.Email).ToListAsync();
                        var viewmodel = new UserViewModel
                        {
                            XeList = xeList,
                            datXeList= datxeList
                        };
                        return View(viewmodel);
                    }
                }
            }
            // Nếu không có tài khoản hoặc không tìm thấy xe, trả về trang không tìm thấy hoặc xử lý khác tùy ý
            return NotFound();
        }
        public async Task<IActionResult> Display(string bienSoXe)
        {
            string username = User.Identity.Name;
            List<SelectListItem> Yeucaudatxes = new List<SelectListItem>();
            List<SelectListItem> DiaDiems = new List<SelectListItem>();
            List<SelectListItem> HangXes = new List<SelectListItem>();
            List<SelectListItem> LoaiXes = new List<SelectListItem>();
            List<SelectListItem> Xes = new List<SelectListItem>();
            foreach (var item in _context.Xes)
            {
                Xes.Add(new SelectListItem { Value = item.BienSoXe.ToString(), Text = item.TenXe });
            }
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
            foreach (var item in _context.YeuCauDatXes)
            {
               Yeucaudatxes.Add(new SelectListItem { Value = item.BienSoXe.ToString(), Text = item.BienSoXe});
            }
            var xe = _context.Xes.FirstOrDefault(x => x.BienSoXe == bienSoXe);
            if (xe == null)
            {
                return NotFound();
            }
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var viewmodel = new UserViewModel()
            {
                Xe = xe,
                datXeList = datxeList
            };
           
            return View(viewmodel);
        }
        public async Task<IActionResult> Add()
        {
            string username = User.Identity.Name;
            List<SelectListItem> DiaDiems = new List<SelectListItem>();
            List<SelectListItem> HangXes = new List<SelectListItem>();
            List<SelectListItem> LoaiXes = new List<SelectListItem>();
            List<SelectListItem> Xes = new List<SelectListItem>();
            foreach (var item in _context.Xes)
            {
                Xes.Add(new SelectListItem { Value = item.BienSoXe.ToString(), Text = item.TenXe });
            }
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
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var viewmodel = new UserViewModel()
            {
                datXeList = datxeList,
            };
            return View(viewmodel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Xe xe, IFormFile HinhAnh1)
        {
            // Tiếp tục code hiện tại của bạn...
            // ...
            string username = User.Identity.Name;
            if (xe != null)
            {
                if (await _context.Xes.AnyAsync(p => p.BienSoXe == xe.BienSoXe))
                {
                    TempData["Message"] = "Biển số xe đã tồn tại.";
                    return RedirectToAction("Index", "CustomerBike");
                }
                else
                {
                    // Tiến hành lưu thông tin xe vào cơ sở dữ liệu
                    xe.TrangThai = false;

                    // Lưu hình ảnh vào thư mục và cập nhật đường dẫn vào đối tượng xe
                    xe.HinhAnh1 = await SaveImage(HinhAnh1);
                    xe.Email = username;
                    xe.Hide= true;
                    xe.HinhAnh2 = "/Images/bike/default.jpg";
                    xe.HinhAnh3 = "/Images/bike/default.jpg";
                    xe.HinhAnh4 = "/Images/bike/default.jpg";
                    _context.Xes.Add(xe);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Tạo mới thành công.";
                    return RedirectToAction("Index", "CustomerBike"); // Chuyển hướng đến trang chính sau khi thêm thành công
                    
                }
            }
            // Trả về view với model nếu có lỗi
            TempData["Message"] = "Tạo mới không thành công.";
            return View();
        }

        public void DeleteImage(string imagePath)
        {
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/Images/bike", image.FileName); // Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/Images/bike/" + image.FileName; // Trả về đường dẫn tương đối
        }

        public async Task<IActionResult> Update(string bienSoXe)
        {
            string username = User.Identity.Name;
            List<SelectListItem> DiaDiems = new List<SelectListItem>();
            List<SelectListItem> HangXes = new List<SelectListItem>();
            List<SelectListItem> LoaiXes = new List<SelectListItem>();
            List<SelectListItem> Xes = new List<SelectListItem>();
            foreach (var item in _context.Xes)
            {
                Xes.Add(new SelectListItem { Value = item.BienSoXe.ToString(), Text = item.TenXe });
            }
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
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var xe = await _context.Xes.FindAsync(bienSoXe);
            var viewmodel = new UserViewModel()
            {
                Xe = xe,
                datXeList= datxeList,
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Xe xe , IFormFile HinhAnh1)
        {
            if (xe == null)
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
                xeupdate.GiaThue = xe.GiaThue;
                if (HinhAnh1 != null)
                {
                    if (!string.IsNullOrEmpty(xeupdate.HinhAnh1))
                    {
                        DeleteImage(xeupdate.HinhAnh1);
                    }
                    xeupdate.HinhAnh1 = await SaveImage(HinhAnh1);
                }
                xeupdate.MoTa = xe.MoTa;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Cập nhật không thành công.";
                return RedirectToAction("Index", "CustomerBike");
            }
        }

        public async Task<IActionResult> CheckOrder(string bienSoXe, string email)
        {
            List<SelectListItem> Xes = new List<SelectListItem>();
            foreach (var item in _context.Xes)
            {
                Xes.Add(new SelectListItem { Value = item.BienSoXe.ToString(), Text = item.TenXe });
            }
            string username = User.Identity.Name;
            var today = DateTime.Today;
            var order = await _context.YeuCauDatXes.Where(p => p.BienSoXe == bienSoXe && p.Email==email ).FirstOrDefaultAsync();
            var datxeList = await _context.YeuCauDatXes.Where(p =>p.TrangThaiChapNhan == false).ToListAsync();
            var viewmodel = new UserViewModel()
            {
                datXeList = datxeList,
                datXe = order ,
            };
            return View(viewmodel);
        }

        public async Task<IActionResult> Accept(string bienSoXe, string email)
        {
            string username = User.Identity.Name;
            if (string.IsNullOrEmpty(bienSoXe))
            {
                return NotFound();
            }

            var yeuCauDatXe = await _context.YeuCauDatXes.FirstOrDefaultAsync(x => x.BienSoXe == bienSoXe && x.Email == email);

            if (yeuCauDatXe == null)
            {
                return NotFound();
			}
			yeuCauDatXe.TrangThaiChapNhan = true; // Đánh dấu yêu cầu đã được chấp nhận
			await _context.SaveChangesAsync();
			var existingUser = await _context.TaiKhoans.FirstOrDefaultAsync(u => u.Email == email);
			if (existingUser != null)
			{
				string subject = "ĐẶT XE THÀNH CÔNG";
				string thanhtoantoken = Guid.NewGuid().ToString();
				var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1));
				string combinedData = $"{existingUser.Email}|{bienSoXe}";
				_cache.Set(thanhtoantoken, combinedData, cacheOptions);

				// Tạo đường dẫn reset password với token
				string ThanhToanUrl = Url.Action("Index", "HoaDonThueXe", new { token = thanhtoantoken }, Request.Scheme);

				//string resetPasswordUrl = Url.Action("Resetpassword", "Home", new { email = existingUser.Email }, Request.Scheme);
				string body = $@"
                            <html>
                            <body>
                                <p>Bạn đã thuê thành công xe mang số hiệu ""{bienSoXe}""</p>
                                <a href=""{ThanhToanUrl}""><button type=""submit"">thanh toán</button></a>
                            </body>
                            </html>";
				SendMail(existingUser.Email, subject, body);
			}
            var xelist = await _context.Xes.Where(p => p.Email == username).ToListAsync();
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var viewmodel = new UserViewModel()
            {
                datXeList = datxeList,
                datXe = yeuCauDatXe,
                XeList= xelist,
            };

            // Chuyển hướng về view CheckOrder để kiểm tra kết quả đã được cập nhật chưa
            TempData["Message"] = "Đã Chấp nhận yêu cầu.";
            return View("Index", viewmodel);
        }

        public async Task<IActionResult> Deny(string bienSoXe, string email)
        {
            string username = User.Identity.Name;
            if (string.IsNullOrEmpty(bienSoXe))
            {
                return NotFound();
            }

            var yeuCauDatXe = await _context.YeuCauDatXes.FirstOrDefaultAsync(x => x.BienSoXe == bienSoXe && x.Email == email);

            if (yeuCauDatXe == null)
            {
                return NotFound();
            }

            // Xóa yêu cầu đặt xe
            _context.Remove(yeuCauDatXe);
            await _context.SaveChangesAsync();

            var xelist = await _context.Xes.Where(p => p.Email == username).ToListAsync();
            var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
            var viewmodel = new UserViewModel()
            {
                datXeList = datxeList,
                XeList = xelist,
            };

            // Chuyển hướng về view CheckOrder để kiểm tra kết quả đã được cập nhật chưa
            TempData["Message"] = "Đã từ chối yêu cầu.";
            return View("Index", viewmodel);
        }

        public async Task<IActionResult> Menu()
        {
            return PartialView();
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
	}
}
