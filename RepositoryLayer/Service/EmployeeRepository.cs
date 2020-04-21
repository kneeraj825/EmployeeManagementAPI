using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Http;

namespace RepositoryLayer.Service
{
    public class EmployeeRepository : IEmployeeRepository
    {
        
        public static string sqlConnectionString = @"Data Source=.;Initial Catalog=Employee;Integrated Security=True";

        /// <summary>
        ///  Gives Employee Data
        /// </summary>
        /// <returns></returns>
        public List<Employee> Get()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(dataReader["ID"]);
                    employee.FirstName = dataReader["FirstName"].ToString();
                    employee.LastName = dataReader["LastName"].ToString();
                    employee.Gender = dataReader["Gender"].ToString();
                    employee.Company = dataReader["Company"].ToString();
                    employee.Department = dataReader["Department"].ToString();
                    employee.Salary = Convert.ToInt32(dataReader["Salary"]);

                    employees.Add(employee);
                }
            }
            return employees;
        }

        /// <summary>
        /// Gives Specific Employee Data
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns></returns>
        public Employee Get(int id)
        {
            Employee employee = null;

            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetEmployeeByID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();
                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    employee = new Employee();
                    employee.ID = Convert.ToInt32(dataReader["ID"]);
                    employee.FirstName = dataReader["FirstName"].ToString();
                    employee.LastName = dataReader["LastName"].ToString();
                    employee.Gender = dataReader["Gender"].ToString();
                    employee.Company = dataReader["Company"].ToString();
                    employee.Department = dataReader["Department"].ToString();
                    employee.Salary = Convert.ToInt32(dataReader["Salary"]);
                }
            }
            return employee;
        }

        /// <summary>
        /// Adds the Employee Details to the Database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int Post([FromBody]Employee employee)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Company", employee.Company);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                con.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
                return count;
            }
        }

        /// <summary>
        /// Deletes the Employee Details from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                count = cmd.ExecuteNonQuery();

                return count;
            }
        }

        /// <summary>
        /// Updates the Employee Details in the Database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int Put(int id, [FromBody]Employee employee)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Company", employee.Company);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                con.Open();
                count = cmd.ExecuteNonQuery();

                return count;
            }
        }

    }
}
