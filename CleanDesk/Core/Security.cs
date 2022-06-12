using CleanDesk.Models;
using System.Data.SqlClient;
using static CleanDesk.Core.Interfaces;

namespace CleanDesk.Core
{
    public class Security : ISecurity
    {
        private readonly IConfiguration _configuration;
        public Security(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public EmployeeModel GetValidUser(string login)
        {
            try
            {
                EmployeeModel user = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM View_GetAllEmployees Where IdentificationNumber = '{login}'", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    user.EmployeeId = (int)dr["EmployeeId"];
                    user.EmployeeName = (string)dr["EmployeeName"];
                    user.IdentificationNumber = (int)dr["IdentificationNumber"];
                    user.EmployeeTypeId = (int)dr["EmployeeTypeId"];
                    user.ManagementId = (int)dr["ManagementId"];
                    user.ManagementName = (string)dr["ManagementName"];
                }

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GroupModel> GetAllGroups()
        {
            try
            {
                List<GroupModel> groups = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM SecurityGroup ORDER BY SecurityGroupName", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    groups.Add(new GroupModel
                    {
                        SecurityGroupId = (int)dr["SecurityGroupId"],
                        SecurityGroupName = (string)dr["SecurityGroupName"],
                        SecurityGroupDescription = (string)dr["SecurityGroupDescription"]
                    });
                }

                return groups.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<StatusModel> GetAllStatus()
        {
            try
            {
                List<StatusModel> status = new();
                using SqlConnection sqlConnection = new(_configuration.GetConnectionString("connectionString"));
                sqlConnection.Open();
                SqlCommand cmd = new($"SELECT * FROM Status ORDER BY StatusName", sqlConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    status.Add(new StatusModel
                    {
                        StatusId = (int)dr["StatusId"],
                        StatusName = (string)dr["StatusName"]
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
