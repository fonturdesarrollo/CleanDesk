using CleanDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReport _report;
        private readonly IRequest _request;
        public ReportController(IReport report, IRequest request)
        {
            this._report = report;
            this._request = request;
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

        public IActionResult AllResolveRequestsDetailQueue()
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");

                    List<RequestDetailModel> model = _report.GetResolveRequestsDetailQueue();

                    return View(model);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult AllUnresolveRequestsDetailQueue()
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");

                    List<RequestDetailModel> model = _report.GetUnresolveRequestsDetailQueue();

                    return View(model);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }


        public IActionResult TechRequestObservationsDetail(int requestId, string techName)
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    int employeeId = 0;

                    RequestDetailModel model = new()
                    {
                        RequestId = requestId,
                        TechnicianName = techName
                    };

                    var request = _request.GetAllById(requestId);

                    foreach (var item in request)
                    {
                        ViewBag.EmployeeRequestName = item.EmployeeName;
                        ViewBag.RequestDescription = item.RequestDescription;
                        ViewBag.LocationName = item.LocationName;
                        ViewBag.FloorNumber = item.FloorNumber;
                        ViewBag.ExtensionNumber = item.ExtensionNumber;
                        ViewBag.RequestDate = item.RequestDate;
                        ViewBag.IPNumber = item.IPNumber;
                        ViewBag.Management = item.ManagementName;
                        employeeId = item.EmployeeId;
                    }

                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");
                    ViewBag.EmployeeId = (int)HttpContext.Session.GetInt32("EmployeeId");
                    ViewBag.RequestDetailList = _request.GetAllDetailsByRequestId(requestId);

                    return View(model);
                }
            }

            catch (Exception)
            {

                throw;
            }

            return View();
        }
        public IActionResult SelectRequest(int requestId, string techName)
        {
            var employeeId = HttpContext.Session.GetInt32("EmployeeId");

            try
            {
                if (ModelState.IsValid)
                {
                    RequestDetailModel model = new()
                    {
                        RequestId = requestId,
                        EmployeeId = (int)employeeId
                    };

                    return RedirectToAction("TechRequestObservationsDetail", new { requestId, techName });
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
