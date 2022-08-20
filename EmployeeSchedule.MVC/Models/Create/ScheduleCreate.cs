
using EmployeeSchedule.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeSchedule.MVC.Models.Create
{
    public class ScheduleCreate : Schedule
    {
        [TempData]
        public string ValidationMessage { get; set; } = string.Empty;
        public List<SelectListItem> EmployeesSelectList { get; set; }
        public List<SelectListItem> ShiftWorkSelectList { get; set; }
        public ScheduleCreate()
        {
            Employee = new Employee();
        }
        public ScheduleCreate(string validationMessage) 
        {
            ValidationMessage = validationMessage;
            Employee = new Employee();
        }
        public void EmployeeSelectList(IEnumerable<Employee> employees)
        {
            EmployeesSelectList = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name + e.Surname
            }).ToList();

            ShiftWorkSelectList = new List<SelectListItem>()
            {
                new SelectListItem("Prva","Prva"),
                new SelectListItem("Druga","Druga"),
                new SelectListItem("Slobodan dan","Slobodan dan")
            };
        }
    }
}
