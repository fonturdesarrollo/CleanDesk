using CleanDesk.Models;
using System.Data.SqlClient;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Core
{
    public class Locations : ILocation
    {
        private readonly IConfiguration _configuration;
        public Locations(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public List<LocationModel> GetAll()
        {
            try
            {
                List<LocationModel> userLocation = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM Location", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    userLocation.Add(new LocationModel
                    {
                        LocationId = (int)dr["LocationId"],
                        LocationName = (string)dr["LocationName"]
                    });
                }

                return userLocation.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
