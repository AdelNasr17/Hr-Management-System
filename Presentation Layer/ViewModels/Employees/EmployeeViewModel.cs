﻿using DataAccess.Models.Employee;
using DataAccess.Models.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }        
        public string Name { get; set; } = null!;   
        public int Age { get; set; }    
        public string? Address { get; set; }     
        public decimal Salary { get; set; }       
        public bool IsActive { get; set; }     
        public string? Email { get; set; }      
        public string? PhoneNumber { get; set; }      
        public DateTime HiringDate { get; set; }
        public string Gender { get; set; } = null!;
        public string EmployeeType { get; set; } = null!;
        public int DepartmentID { get; set; }
    }
}
