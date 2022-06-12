using CleanDesk.Models;
using Microsoft.AspNetCore.Mvc;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISecurity _security;
        public LoginController(ISecurity security)
        {
            this._security = security;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(EmployeeModel model)
        {
            try
            {
                var validUser = _security.GetValidUser(model.IdentificationNumber.ToString());

                if (validUser != null && validUser.EmployeeId != 0)
                {
                    HttpContext.Session.SetInt32("EmployeeId", validUser.EmployeeId);
                    HttpContext.Session.SetString("EmployeeName", validUser.EmployeeName);
                    HttpContext.Session.SetInt32("IdentificationNumber", validUser.IdentificationNumber);
                    HttpContext.Session.SetInt32("EmployeeTypeId", validUser.EmployeeTypeId);
                    HttpContext.Session.SetInt32("ManagementId", validUser.ManagementId);
                    HttpContext.Session.SetString("ManagementName", validUser.ManagementName);

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.InvalidUser = "true";

                return View("Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }
    }
}
