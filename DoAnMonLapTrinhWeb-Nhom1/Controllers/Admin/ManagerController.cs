using DoAnMonLapTrinhWeb_Nhom1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAnMonLapTrinhWeb_Nhom1.Controllers.Admin
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employer)]
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
