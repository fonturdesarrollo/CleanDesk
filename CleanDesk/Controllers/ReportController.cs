using CleanDesk.Models;
using Microsoft.AspNetCore.Mvc;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReport _report;
        public ReportController(IReport report)
        {
            this._report = report;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllRequestsDetailQueue()
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");

                    List<RequestDetailModel> model = _report.GetAllRequestsDetailQueue();

                    return View(model);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }
    }
}
