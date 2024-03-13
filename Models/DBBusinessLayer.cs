using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class DBBusinessLayer
    {
        public bool AddEmployee(Employee employee)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["BusinessLayer_DBConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramName = new SqlParameter();
                    paramName.ParameterName = "@E_Name";
                    paramName.Value = employee.Name;
                    cmd.Parameters.Add(paramName);

                    SqlParameter paramEmployeeCode = new SqlParameter();
                    paramEmployeeCode.ParameterName = "@E_EmployeeCode";
                    paramEmployeeCode.Value = employee.EmployeeCode;
                    cmd.Parameters.Add(paramEmployeeCode);

                    SqlParameter paramEmailId = new SqlParameter();
                    paramEmailId.ParameterName = "@E_EmailId";
                    paramEmailId.Value = employee.EmailId;
                    cmd.Parameters.Add(paramEmailId);

                    SqlParameter paramPhoneNo = new SqlParameter();
                    paramPhoneNo.ParameterName = "@E_PhoneNo";
                    paramPhoneNo.Value = employee.PhoneNo;
                    cmd.Parameters.Add(paramPhoneNo);


                    SqlParameter paramGender = new SqlParameter();
                    paramGender.ParameterName = "@E_Gender";
                    paramGender.Value = employee.Gender;
                    cmd.Parameters.Add(paramGender);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public List<Employee> ViewEmployeesDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BusinessLayer_DBConnection"].ConnectionString;
            List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.EmployeeCode = rdr["EmployeeCode"].ToString();
                    employee.EmailId = rdr["EmailId"].ToString();
                    employee.PhoneNo = rdr["PhoneNo"].ToString();
                    employee.Gender = rdr["Gender"].ToString();

                    employees.Add(employee);
                }
                rdr.Close();
            }
            return employees;
        }

        #region
        // Add a new method to search employees by code
        //public List<Employee> SearchEmployeesByCode(string employeeCode)
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["BusinessLayer_DBConnection"].ConnectionString;
        //    List<Employee> employees = new List<Employee>();
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spSearchEmployeesByCode", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            Employee employee = new Employee();
        //            employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
        //            employee.Name = rdr["Name"].ToString();
        //            employee.EmployeeCode = rdr["EmployeeCode"].ToString();
        //            employee.EmailId = rdr["EmailId"].ToString();
        //            employee.PhoneNo = rdr["PhoneNo"].ToString();
        //            employee.Gender = rdr["Gender"].ToString();

        //            employees.Add(employee);
        //        }
        //    }
        //    return employees;
        //}
        #endregion

        public List<Employee> SearchEmployeesByAllFields(Employee employee)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusinessLayer_DBConnection"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("spSearchEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@EmployeeCode", employee.EmployeeCode);
                    cmd.Parameters.AddWithValue("@EmailId", employee.EmailId);
                    cmd.Parameters.AddWithValue("@PhoneNo", employee.PhoneNo);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                        emp.Name = rdr["Name"].ToString();
                        emp.EmployeeCode = rdr["EmployeeCode"].ToString();
                        emp.EmailId = rdr["EmailId"].ToString();
                        emp.PhoneNo = rdr["PhoneNo"].ToString();
                        emp.Gender = rdr["Gender"].ToString();
                        employees.Add(emp);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return employees;

        }
    }
}
