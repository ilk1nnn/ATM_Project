using ATM_Project.DataAccess.Abstractions;
using ATM_Project.DataAccess.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
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

        public void UpdateDataBase()
        {
            //string con1 = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Documents and Settings\sam\my documents\visual studio 2010\Projects\WindowsFormsApplication1\WindowsFormsApplication1\Database1.accdb";
            //string query1 = "SELECT * FROM Table2";
            //OleDbDataAdapter da1 = new OleDbDataAdapter(query1, con1);
            //OleDbCommandBuilder cmd = new OleDbCommandBuilder(da1);
            //da1.Update(database1DataSet2.Table2);
            //this.database1DataSet2.Table2.Clear(); //clears the dataset
            //da1.Fill(database1DataSet2.Table2);

            //using(var conn = new SqlConnection(ConnectionString))
            //{
            //    string query = "SELECT * FROM Users";
            //    OleDbDataAdapter da1 = new OleDbDataAdapter(query, ConnectionString);
            //    OleDbCommandBuilder cmd = new OleDbCommandBuilder(da1);
            //    da1.Update(datase)
            //}

        }
    }
}
