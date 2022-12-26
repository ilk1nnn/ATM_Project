using ATM_Project.DataAccess.Abstractions;
using ATM_Project.DataAccess.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Project.DataAccess.Concreates
{
    public class UserRepository : IUserRepository
    {

        public string ConnectionString { get; set; }


        public UserRepository()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        }


        public void AddData(User data)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            using(var conn = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM Users";
                var result = conn.Query<User>(query).ToList();

                return result;
            }
        }

        public void Update(User data)
        {
            using(var conn = new SqlConnection(ConnectionString))
            {
                var query = $@"UPDATE Users
                              SET Balance = {data.Balance}
                              WHERE CardNumber = {data.CardNumber}";
                conn.Query<User>(query).ToList();
            }    
        }
    }
}
