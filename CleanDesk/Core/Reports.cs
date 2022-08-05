using CleanDesk.Models;
using System.Data.SqlClient;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Core
{
    public class Reports : IReport
    {
        private readonly IConfiguration _configuration;
        public Reports(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public List<RequestDetailModel> GetAllRequestsDetailQueue()
        {
            try
            {
                List<RequestDetailModel> queues = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM View_LastRequestDetailsList WHERE RequestDetailStatusId <> 7 AND RequestDetailStatusId <> 8 AND RequestDetailStatusId <> 6  AND RequestDetailStatusId <> 4  ORDER BY RequestDetailId", sqlConnection);
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
                        Minutes = (int)dr["Minutes"],
                        TechnicianName = (string)dr["TechnicianName"],
                        TechnicianId = (int)dr["TechnicianId"]
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

