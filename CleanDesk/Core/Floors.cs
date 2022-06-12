using CleanDesk.Models;
using System.Data.SqlClient;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Core
{
    public class Floors : IFloor
    {
        private readonly IConfiguration _configuration;
        public Floors(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public List<FloorModel> GetAll()
        {
            try
            {
                List<FloorModel> floor = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM Floor ORDER BY FloorId", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    floor.Add(new FloorModel
                    {
                        FloorId = (int)dr["FloorId"],
                        FloorNumber = (int)dr["FloorNumber"],
                    });
                }

                return floor.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
