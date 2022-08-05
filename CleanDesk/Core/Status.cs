using CleanDesk.Models;
using System.Data.SqlClient;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Core
{
    public class Status : IStatus
    {
        private readonly IConfiguration _configuration;
        public Status(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public List<StatusModel> GetAll()
        {
            try
            {
                List<StatusModel> status = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM RequestStatus ORDER BY RequestStatusId", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    status.Add(new StatusModel
                    {
                        StatusId = (int)dr["RequestStatusId"],
                        StatusName = (string)dr["RequestStatusName"],
                        RequestDetailStatusId = (int)dr["RequestStatusId"],
                        RequestDetailStatusName = (string)dr["RequestStatusName"]
                    });
                }

                return status.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<StatusModel> GetOnlyForTechnician()
        {
            try
            {
                List<StatusModel> status = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM RequestStatus WHERE RequestStatusId = {3} OR RequestStatusId = {4} OR RequestStatusId = {5} OR RequestStatusId = {8} ORDER BY RequestStatusId", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    status.Add(new StatusModel
                    {
                        StatusId = (int)dr["RequestStatusId"],
                        StatusName = (string)dr["RequestStatusName"],
                        RequestDetailStatusId = (int)dr["RequestStatusId"],
                        RequestDetailStatusName = (string)dr["RequestStatusName"]
                    });
                }

                return status.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
