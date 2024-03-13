using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {

        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Name should not be blank")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Employee Code should not be blank")]
        [RegularExpression("^[0-9]{1,6}$", ErrorMessage = "Employee Code must be a number with a maximum of 6 digits")]
        public string EmployeeCode { get; set; }


        [Required(ErrorMessage = "Email ID should not be blank")]   
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }


        [Required(ErrorMessage = "Phone Number should not be blank")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Phone Number must be a 10-digit number")]
        public string PhoneNo { get; set; }


        [Required(ErrorMessage = "Gender should not be blank")]
        public string Gender { get; set; }
    }
}