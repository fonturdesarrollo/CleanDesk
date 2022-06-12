using CleanDesk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequest _request;
        private readonly IRequestArea _requestArea;
        private readonly IFloor _floor;
        private readonly ILocation _location;
        private readonly IStatus _status;
        public RequestController(IRequest request, IRequestArea requestArea, IFloor floor, ILocation location, IStatus status)
        {
            this._request = request;
            this._requestArea = requestArea;
            this._floor = floor;
            this._location = location;
            this._status = status;
        }
        public IActionResult Add(int employeeIdNumber)
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    var model = new RequestModel();

                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");
                    ViewBag.IdentificationNumber = (int)HttpContext.Session.GetInt32("IdentificationNumber");
                    ViewBag.EmployeeId = (int)HttpContext.Session.GetInt32("EmployeeId");
                    ViewBag.RequestArea = new SelectList(_requestArea.GetAll(), "RequestAreaId", "RequestAreaName");
                    ViewBag.Floor = new SelectList(_floor.GetAll(), "FloorId", "FloorNumber");
                    ViewBag.Location = new SelectList(_location.GetAll(), "LocationId", "LocationName");

                    return View(model);
                }
            }

            catch (Exception)
            {

                throw;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Add(RequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.EmployeeId = (int)HttpContext.Session.GetInt32("EmployeeId");

                    int requestId = _request.AddOrEdit(model);

                    if (requestId > 0)
                    {
                        return RedirectToAction("Result", "Request", new { successMessage = "Registro realizado correctamente" });
                    }
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult AddRequestDetail(int requestId)
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    //int technicianId = (int)HttpContext.Session.GetInt32("EmployeeId");
                    int employeeId = 0;

                    RequestDetailModel model = new()
                    {
                        RequestId = requestId
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
                        employeeId = item.EmployeeId;
                    }                

                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");
                    ViewBag.EmployeeId = (int)HttpContext.Session.GetInt32("EmployeeId");
                    ViewBag.Status = new SelectList(_status.GetOnlyForTechnician(), "RequestDetailStatusId", "RequestDetailStatusName");
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

        [HttpPost]
        public IActionResult AddRequestDetail(RequestDetailModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.EmployeeId = (int)HttpContext.Session.GetInt32("EmployeeId");

                    int requestId = _request.RequestDetailAdd(model);

                    if (requestId > 0)
                    {
                        return RedirectToAction("AddRequestDetail", new { requestId = model.RequestId });
                    }
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult RequestQueue()
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");

                    List<RequestModel> model = _request.GetAllByStatusId(1);

                    return View(model);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult RequestDetailQueue()
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");

                    bool employeeAdmin = (int)HttpContext.Session.GetInt32("EmployeeTypeId") == 3 ? true : false;

                    List<RequestDetailModel> model = _request.GetAllByTechnicianId((int)HttpContext.Session.GetInt32("EmployeeId"), employeeAdmin);

                    return View(model);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult RequestUserDetailQueue()
        {
            try
            {
                if (HttpContext.Session.GetString("EmployeeId") != null)
                {
                    ViewBag.ManagementName = (string)HttpContext.Session.GetString("ManagementName");
                    ViewBag.EmployeeName = (string)HttpContext.Session.GetString("EmployeeName");

                    List<RequestDetailModel> model = _request.GetLastByEmployeeId((int)HttpContext.Session.GetInt32("EmployeeId"));

                    return View(model);
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult TakeRequest(int requestId)
        {
            var employeeId = HttpContext.Session.GetInt32("EmployeeId");

            try
            {
                if (ModelState.IsValid)
                {
                    RequestDetailModel model = new()
                    {
                        RequestId = requestId,
                        EmployeeId = (int)employeeId,
                        Observations = "Solicitud tomada por el tecnico",
                        Minutes = 0,
                        RequestDetailStatusId = 2
                    };

                    if (_request.RequestDetailAdd(model) > 0)
                    {
                        return RedirectToAction("RequestQueue");
                    }
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult SelectRequest(int requestId)
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

                    return RedirectToAction("AddRequestDetail", new { requestId = requestId} );
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Result(string successMessage)
        {
            ViewBag.SuccessMessage = successMessage;
            return View();
        }
    }
}
