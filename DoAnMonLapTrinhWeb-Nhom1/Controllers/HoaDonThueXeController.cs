using DoAnMonLapTrinhWeb_Nhom1.Models;
using DoAnMonLapTrinhWeb_Nhom1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers
{
	public class HoaDonThueXeController : Controller
	{
		private readonly QuanLyThueXeMayTuLaiContext _context;
		private readonly IMemoryCache _cache;

		public HoaDonThueXeController(QuanLyThueXeMayTuLaiContext context, IMemoryCache cache)
		{
			_context = context;
			_cache = cache;
		}

		public async Task<IActionResult> Index(string token)
		{
			var cachedData = _cache.Get<string>(token);
			if (cachedData != null)
			{

				// Phân tách dữ liệu từ tokenData
				string[] dataParts = cachedData.Split('|'); // Sử dụng dấu gạch ngang làm ký tự phân tách

				if (dataParts.Length >= 2)
				{
					string email = dataParts[0];
					string bienSoXe = dataParts[1];

					// Bây giờ bạn đã tách được email và biển số xe ra từ token
					// Bạn có thể sử dụng chúng để thực hiện các thao tác tiếp theo
					List<SelectListItem> PhuongThucs = new List<SelectListItem>();
					foreach (var item in _context.PhuongThucThanhToans)
					{
						PhuongThucs.Add(new SelectListItem { Value = item.MaPhuongThuc.ToString(), Text = item.TenPhuongThuc });
					}
					ViewBag.PhuongThucs = PhuongThucs;
					string username = User.Identity.Name;
					var datxeList = await _context.YeuCauDatXes.Where(p => p.BienSoXeNavigation.Email == username && p.TrangThaiChapNhan == false).ToListAsync();
					YeuCauDatXe datXe = await _context.YeuCauDatXes.FirstOrDefaultAsync(x => x.Email == email && x.BienSoXe == bienSoXe);
					var xe = await _context.Xes.FirstOrDefaultAsync(x => x.BienSoXe == bienSoXe);
					var viewModel = new UserViewModel()
					{
						Xe = xe,
						datXeList = datxeList,
						datXe = datXe,
					};
					return View(viewModel);
				}
                TempData["Message"] = "Thông tin không hợp lệ";
				return RedirectToAction("Index", "Home");
			}
			else
			{
                TempData["Message"] = "Hóa đơn hết hạn thanh toán";
				return RedirectToAction("Index", "Home");
			}
			
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ThanhToan(UserViewModel model)
		{
			if(model != null)
			{
				HoaDon hoaDon = new HoaDon();
				hoaDon.BienSoXe = model.datXe.BienSoXe;
				hoaDon.Email = model.datXe.Email;
				hoaDon.NgayLap = DateTime.Now;
				hoaDon.MaPhuongThuc = model.HoaDon.MaPhuongThuc;
				hoaDon.TongDonGia = model.Xe.GiaThue * model.datXe.SoNgayThue;
				_context.HoaDons.Add(hoaDon);
				await _context.SaveChangesAsync();
				TempData["Message"] = "Thanh toán thành công";
				return RedirectToAction("Index", "Home");
			}
			TempData["Message"] = "Lỗi thanh toán";
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Menu()
		{
			return PartialView();
		}
	}
}
