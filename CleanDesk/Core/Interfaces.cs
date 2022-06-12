
using CleanDesk.Models;

namespace CleanDesk.Core
{
    public class Interfaces
    {
        public interface ISecurity 
        {
            public EmployeeModel GetValidUser(string login);
        }

        public interface IRequest
        {
            public int AddOrEdit(RequestModel model);
            public int RequestDetailAdd(RequestDetailModel model);
            public List<RequestModel> GetAll();
            public List<RequestModel> GetAllById(int requestId);
            public List<RequestModel> GetAllByStatusId(int statusId);
            public List<RequestDetailModel> GetAllByTechnicianId(int technicianId, bool IsAllTechnician = false);
            public List<RequestDetailModel> GetAllDetailsByRequestId(int requestId);
            public List<RequestDetailModel> GetLastByEmployeeId(int employeeId);
        }

        public interface IRequestArea
        {
            public List<RequestAreaModel> GetAll();
        }
        public interface IFloor
        {
            public List<FloorModel> GetAll();
        }

        public interface IStatus
        {
            public List<StatusModel> GetAll();
            public List<StatusModel> GetOnlyForTechnician();
        }

        public interface ILocation
        {
            public List<LocationModel> GetAll();
        }
    }
}
