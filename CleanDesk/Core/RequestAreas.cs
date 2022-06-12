using CleanDesk.Models;
using System.Data.SqlClient;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Core
{
    public class RequestAreas : IRequestArea
    {
        private readonly IConfiguration _configuration;
        public RequestAreas(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public List<RequestAreaModel> GetAll()
        {
            try
            {
                List<RequestAreaModel> ra = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM RequestArea", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ra.Add(new RequestAreaModel
                    {
                        RequestAreaId = (int)dr["RequestAreaId"],
                        RequestAreaName = (string)dr["RequestAreaName"],
                    });
                }

                return ra.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
