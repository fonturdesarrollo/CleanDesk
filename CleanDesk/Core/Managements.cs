using CleanDesk.Models;
using System.Data.SqlClient;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Core
{
    public class Managements : IManagement
    {
        private readonly IConfiguration _configuration;
        public Managements(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public List<ManagementModel> GetAll()
        {
            try
            {
                List<ManagementModel> management = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM Management ORDER BY ManagementId", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    management.Add(new ManagementModel
                    {
                        ManagementId = (int)dr["ManagementId"],
                        ManagementName = (string)dr["ManagementName"],
                    });
                }

                return management.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
