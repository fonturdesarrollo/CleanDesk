using CleanDesk.Models;
using System.Data.SqlClient;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Core
{
    public class Requests : IRequest
    {
        private readonly IConfiguration _configuration;
        public Requests(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public int AddOrEdit(RequestModel model)
        {
            int result = 0;
            try
            {
                using (SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString")))
                {
                    if (model != null)
                    {
                        sqlConnection.Open();
                        SqlCommand cmd = new("RequestAddOrEdit", sqlConnection)
                        {
                            CommandType = System.Data.CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("RequestId", model.RequestId);
                        cmd.Parameters.AddWithValue("EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("RequestDescription", model.RequestDescription.ToUpper());
                        cmd.Parameters.AddWithValue("RequestAreaDetailId", model.RequestAreaDetailId);
                        cmd.Parameters.AddWithValue("RequestStatusId", model.RequestStatusId);
                        cmd.Parameters.AddWithValue("LocationId", model.LocationId);
                        cmd.Parameters.AddWithValue("ExtensionNumber", string.IsNullOrEmpty(model.ExtensionNumber) ? "N/D" : model.ExtensionNumber);
                        cmd.Parameters.AddWithValue("FloorId", model.FloorId);
                        cmd.Parameters.AddWithValue("ManagementId", model.ManagementId);
                        cmd.Parameters.AddWithValue("IPNumber", string.IsNullOrEmpty(model.IPNumber) ? "N/D" : model.IPNumber);

                        result = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int RequestDetailAdd(RequestDetailModel model)
        {
            int result = 0;
            try
            {
                using (SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString")))
                {
                    if (model != null)
                    {
                        sqlConnection.Open();
                        SqlCommand cmd = new("RequestDetailAdd", sqlConnection)
                        {
                            CommandType = System.Data.CommandType.StoredProcedure
                        };
                        cmd.Parameters.AddWithValue("RequestDetailId", model.RequestDetailId);
                        cmd.Parameters.AddWithValue("RequestId", model.RequestId);
                        cmd.Parameters.AddWithValue("EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("Observations", model.Observations.ToUpper());
                        cmd.Parameters.AddWithValue("Minutes", model.Minutes);
                        cmd.Parameters.AddWithValue("RequestDetailStatusId", model.RequestDetailStatusId);

                        result = Convert.ToInt32(cmd.ExecuteScalar());

                        if (model.RequestDetailStatusId == 8)
                        {
                            RequestEditStatus(model.RequestId, model.RequestDetailStatusId);
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int RequestEditStatus(int requestId, int statusId)
        {
            int result = 0;
            int backToQueue = 1;

            if(statusId != 8)
            {
                backToQueue = statusId;
            }

            try
            {
                using (SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString")))
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new("RequestEditStatus", sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("RequestId", requestId);
                    cmd.Parameters.AddWithValue("RequestStatusId", backToQueue);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RequestModel> GetAll()
        {
            try
            {
                List<RequestModel> queues = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM View_GetAllRequests", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    queues.Add(new RequestModel
                    {
                        RequestId = (int)dr["RequestId"],
                        EmployeeId = (int)dr["EmployeeId"],
                        EmployeeName = (string)dr["EmployeeName"],
                        RequestDescription = (string)dr["RequestDescription"],
                        RequestAreaDetailId = (int)dr["RequestAreaDetailId"],
                        RequestAreaName = (string)dr["RequestAreaName"],
                        RequestStatusId = (int)dr["RequestStatusId"],
                        LocationId = (int)dr["LocationId"],
                        LocationName = (string)dr["LocationName"],
                        FloorId = (int)dr["FloorId"],
                        FloorNumber = (string)dr["FloorNumber"],
                        ExtensionNumber = (string)dr["ExtensionNumber"],
                        RequestStatusName = (string)dr["RequestStatusName"],
                        RequestDate = (DateTime)dr["RequestDate"],
                    });
                }

                return queues.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RequestModel> GetAllById(int requestId)
        {
            try
            {
                List<RequestModel> queues = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM View_GetAllRequests WHERE RequestId = {requestId}", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    queues.Add(new RequestModel
                    {
                        RequestId = (int)dr["RequestId"],
                        EmployeeId = (int)dr["EmployeeId"],
                        EmployeeName = (string)dr["EmployeeName"],
                        RequestDescription = (string)dr["RequestDescription"],
                        RequestAreaDetailId = (int)dr["RequestAreaDetailId"],
                        RequestAreaName = (string)dr["RequestAreaName"],
                        RequestStatusId = (int)dr["RequestStatusId"],
                        LocationId = (int)dr["LocationId"],
                        LocationName = (string)dr["LocationName"],
                        FloorId = (int)dr["FloorId"],
                        FloorNumber = (string)dr["FloorNumber"],
                        ExtensionNumber = (string)dr["ExtensionNumber"],
                        RequestStatusName = (string)dr["RequestStatusName"],
                        RequestDate = (DateTime)dr["RequestDate"],
                        IPNumber = (string)dr["IPNumber"],
                        ManagementId = (int)dr["LocationId"],
                        ManagementName = (string)dr["ManagementName"]
                    });
                }

                return queues.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RequestModel> GetAllByStatusId(int statusId)
        {
            try
            {
                List<RequestModel> queues = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM View_GetAllRequests WHERE RequestStatusId = {statusId} ORDER BY RequestId", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    queues.Add(new RequestModel
                    {
                        RequestId = (int)dr["RequestId"],
                        EmployeeId = (int)dr["EmployeeId"],
                        EmployeeName = (string)dr["EmployeeName"],
                        RequestDescription = (string)dr["RequestDescription"],
                        RequestAreaDetailId = (int)dr["RequestAreaDetailId"],
                        RequestAreaName = (string)dr["RequestAreaName"],
                        RequestStatusId = (int)dr["RequestStatusId"],
                        LocationId = (int)dr["LocationId"],
                        LocationName = (string)dr["LocationName"],
                        FloorId = (int)dr["FloorId"],
                        FloorNumber = (string)dr["FloorNumber"],
                        ExtensionNumber = (string)dr["ExtensionNumber"],
                        RequestStatusName = (string)dr["RequestStatusName"],
                        RequestDate = (DateTime)dr["RequestDate"],
                        ManagementId = (int)dr["ManagementId"],
                        ManagementName = (string)dr["ManagementName"]
                    });
                }

                return queues.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RequestDetailModel> GetAllByTechnicianId(int technicianId, bool IsAllTechnician = false)
        {
            try
            {
                List<RequestDetailModel> queues = new();
                SqlCommand cmd = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();

                if(!IsAllTechnician)
                {
                    cmd = new($"SELECT * FROM View_GetAllRequestsDetailsList WHERE TechnicianId = {technicianId}", sqlConnection);
                }
                else 
                {
                    cmd = new($"SELECT * FROM View_GetAllRequestsDetailsList", sqlConnection);
                }
                
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    queues.Add(new RequestDetailModel
                    {
                        RequestId = (int)dr["RequestId"],
                        EmployeeId = (int)dr["EmployeeId"],
                        EmployeeName = (string)dr["EmployeeName"],
                        RequestDescription = (string)dr["RequestDescription"],
                        //RequestDetailStatusId = (int)dr["RequestDetailStatusId"],
                        LocationName = (string)dr["LocationName"],
                        LocationId = (int)dr["LocationId"],
                        FloorNumber = (string)dr["FloorNumber"],
                        ExtensionNumber = (string)dr["ExtensionNumber"],
                        //RequestDetailStatusName = (string)dr["RequestDetailStatusName"],
                        RequestDate = (DateTime)dr["RequestDate"],
                        //DateAssignated = (DateTime)dr["DateAssignated"],
                        TechnicianName = (string)dr["TechnicianName"],
                        IPNumber = (string)dr["IPNumber"],
                        ManagementId = (int)dr["ManagementId"],
                        ManagementName = (string)dr["ManagementName"]
                        //Minutes = (int)dr["Minutes"]
                    });
                }

                return queues.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RequestDetailModel> GetLastByEmployeeId(int employeeId)
        {
            try
            {
                List<RequestDetailModel> queues = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM View_LastRequestDetailsList WHERE EmployeeId = {employeeId} ORDER BY RequestDetailId", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    queues.Add(new RequestDetailModel
                    {
                        RequestId = (int)dr["RequestId"],
                        EmployeeId = (int)dr["EmployeeId"],
                        EmployeeName = (string)dr["EmployeeName"],
                        RequestDescription = (string)dr["RequestDescription"],
                        RequestDetailStatusId = (int)dr["RequestDetailStatusId"],
                        LocationName = (string)dr["LocationName"],
                        LocationId = (int)dr["LocationId"],
                        FloorNumber = (string)dr["FloorNumber"],
                        ExtensionNumber = (string)dr["ExtensionNumber"],
                        RequestDetailStatusName = (string)dr["RequestDetailStatusName"],
                        RequestDate = (DateTime)dr["RequestDate"],
                        DateAssignated = (DateTime)dr["DateAssignated"],
                        Observations = (string)dr["Observations"],
                        Minutes = (int)dr["Minutes"]
                    });
                }

                return queues.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<RequestDetailModel> GetAllDetailsByRequestId(int requestId)
        {
            try
            {
                List<RequestDetailModel> queues = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM View_GetAllRequestsDetails WHERE  RequestId = {requestId} ORDER BY RequestDetailId", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    queues.Add(new RequestDetailModel
                    {
                        RequestId = (int)dr["RequestId"],
                        EmployeeId = (int)dr["EmployeeId"],
                        EmployeeName = (string)dr["EmployeeName"],
                        RequestDescription = (string)dr["RequestDescription"],
                        RequestDetailStatusId = (int)dr["RequestDetailStatusId"],
                        LocationName = (string)dr["LocationName"],
                        LocationId = (int)dr["LocationId"],
                        FloorNumber = (string)dr["FloorNumber"],
                        ExtensionNumber = (string)dr["ExtensionNumber"],
                        RequestDetailStatusName = (string)dr["RequestDetailStatusName"],
                        RequestDate = (DateTime)dr["RequestDate"],
                        DateAssignated = (DateTime)dr["DateAssignated"],
                        Observations = (string)dr["Observations"],
                        Minutes = (int)dr["Minutes"]
                    });
                }

                return queues.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
