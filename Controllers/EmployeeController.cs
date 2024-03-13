using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Employee employee)
        {
            if (ModelState.IsValid)
            {
                DBBusinessLayer dbBusinessLayer = new DBBusinessLayer();
                bool success = dbBusinessLayer.AddEmployee(employee);

                if (success)
                {
                    TempData["Message"] = "All data are successfully saved.";
                }
                else
                {
                    TempData["Message"] = "An error occurred while saving data.";
                }
                return Json(new
                {
                    message = TempData["Message"]
                });
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EmployeeDetails()
        {
            DBBusinessLayer dbBusinessLayer = new DBBusinessLayer();
            List<Employee> employeesDetails = dbBusinessLayer.ViewEmployeesDetails();
            return View(employeesDetails);
        }

        // Add a new action method for searching employees by code
        //[HttpPost]
        //public ActionResult EmployeeDetails(string employeeCode)
        //{
        //    // Check if employeeCode is null or empty
        //    if (string.IsNullOrEmpty(employeeCode))
        //    {
        //        // Retrieve all employees
        //        DBBusinessLayer dbBusinessLayer = new DBBusinessLayer();
        //        List<Employee> employeesDetails = dbBusinessLayer.ViewEmployeesDetails();
        //        return View(employeesDetails);
        //    }
        //    else
        //    {
        //        // Retrieve employees based on the provided code
        //        DBBusinessLayer dbBusinessLayer = new DBBusinessLayer();
        //        List<Employee> employeesDetails = dbBusinessLayer.SearchEmployeesByCode(employeeCode);
        //        return View(employeesDetails);
        //    }
        //}

        #region search using Linq
        //[HttpPost]
        //public ActionResult EmployeeDetails(Employee employee)
        //{
        //    DBBusinessLayer dbBusinessLayer = new DBBusinessLayer();
        //    List<Employee> employees = dbBusinessLayer.ViewEmployeesDetails();

        //    // Apply filters based on search criteria
        //    if (!string.IsNullOrEmpty(employee.EmployeeCode))
        //    {
        //        employees = employees.Where(e => e.EmployeeCode.ToLower().Contains(employee.EmployeeCode.ToLower())).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(employee.Name))
        //    {
        //        employees = employees.Where(e => e.Name.ToLower().Contains(employee.Name.ToLower())).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(employee.EmailId))
        //    {
        //        employees = employees.Where(e => e.EmailId.ToLower().Contains(employee.EmailId.ToLower())).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(employee.PhoneNo))
        //    {
        //        employees = employees.Where(e => e.PhoneNo.ToLower().Contains(employee.PhoneNo.ToLower())).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(employee.Gender))
        //    {
        //        employees = employees.Where(e => e.Gender.ToLower().Contains(employee.Gender.ToLower())).ToList();
        //    }

        //    return View(employees);
        //}
        #endregion

        [HttpPost]
        public ActionResult EmployeeDetails(Employee employee)
        {
            try
            {
                DBBusinessLayer dbBusinessLayer = new DBBusinessLayer();
                List<Employee> employeesDetails = dbBusinessLayer.SearchEmployeesByAllFields(employee);
                return View(employeesDetails);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult ClearSearch()
        {
            return RedirectToAction("EmployeeDetails");
        }

        #region Without creating Function code in controller 
        //[HttpPost]
        //public ActionResult EmployeeDetails(Employee employee)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusinessLayer_DBConnection"].ConnectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand("spSearchEmployees", con);
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            cmd.Parameters.AddWithValue("@Name", employee.Name);
        //            cmd.Parameters.AddWithValue("@EmployeeCode", employee.EmployeeCode);
        //            cmd.Parameters.AddWithValue("@EmailId", employee.EmailId);
        //            cmd.Parameters.AddWithValue("@PhoneNo", employee.PhoneNo);
        //            cmd.Parameters.AddWithValue("@Gender", employee.Gender);

        //            con.Open();
        //            SqlDataReader rdr = cmd.ExecuteReader();

        //            List<Employee> employees = new List<Employee>();
        //            while (rdr.Read())
        //            {
        //                Employee emp = new Employee();
        //                emp.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
        //                emp.Name = rdr["Name"].ToString();
        //                emp.EmployeeCode = rdr["EmployeeCode"].ToString();
        //                emp.EmailId = rdr["EmailId"].ToString();
        //                emp.PhoneNo = rdr["PhoneNo"].ToString();
        //                emp.Gender = rdr["Gender"].ToString();
        //                employees.Add(emp);
        //            }
        //            return View(employees);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        return View("Error");
        //    }
        //}
        #endregion 
    }
}