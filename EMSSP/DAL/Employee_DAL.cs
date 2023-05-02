using EMSSP.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EMSSP.DAL
{
    public class Employee_DAL
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");

        }
        public List<Employee> GetAll() 
        {
            List<Employee> employeeList = new List<Employee>();
            using(_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[GetAllEmployees]";
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while(dr.Read()) 
                {
                    Employee employee = new Employee();
                    employee.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);
                    employee.Name = dr["Name"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.PhoneNo = dr["PhoneNo"].ToString();
                    employee.JobTitle = dr["JobTitle"].ToString();
                    employeeList.Add(employee);
                }
                _connection.Close();
            }
            return employeeList;
        }

        public bool Insert(Employee model)
        {
            int row = 0;
            using(_connection = new SqlConnection(GetConnectionString())) 
            {
                _command=_connection.CreateCommand();
                _command.CommandType=CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[insertEmployee]";
                _command.Parameters.AddWithValue("@Name", model.Name);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@PhoneNo", model.PhoneNo);
                _command.Parameters.AddWithValue("@JobTitle", model.JobTitle);
                _connection.Open();
                row = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return row > 0 ? true : false;
        }
        public Employee GetById(int id)
        {
            Employee employee = new Employee();
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[sp_getEmployeeById]";
                _command.Parameters.AddWithValue("@id",id);
                _connection.Open();
                SqlDataReader dr = _command.ExecuteReader();
                while (dr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(dr["EmployeeId"]);
                    employee.Name = dr["Name"].ToString();
                    employee.Email = dr["Email"].ToString();
                    employee.PhoneNo = dr["PhoneNo"].ToString();
                    employee.JobTitle = dr["JobTitle"].ToString();
                }
                _connection.Close();
            }
            return employee;
        }
        public bool Update(Employee model)
        {
            int row = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[UpdateEmployeeRecord]";
                _command.Parameters.AddWithValue("@Id", model.EmployeeId);
                _command.Parameters.AddWithValue("@Name", model.Name);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@PhoneNo", model.PhoneNo);
                _command.Parameters.AddWithValue("@JobTitle", model.JobTitle);
                _connection.Open();
                row = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return row > 0 ? true : false;
        }
        public bool Delete(int id)
        {
            int row = 0;
            using (_connection = new SqlConnection(GetConnectionString()))
            {
                _command = _connection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "[DBO].[sp_employee_delete]";
                _command.Parameters.AddWithValue("@id", id);
                _connection.Open();
                row = _command.ExecuteNonQuery();
                _connection.Close();
            }
            return row > 0 ? true : false;
        }
    }
}
