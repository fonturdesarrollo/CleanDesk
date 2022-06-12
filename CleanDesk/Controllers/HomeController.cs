using CleanDesk.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanDesk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");
                    ViewBag.EmployeeType = (int)HttpContext.Session.GetInt32("EmployeeTypeId");

                    return View();
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;
            return View();
        }
    }
}