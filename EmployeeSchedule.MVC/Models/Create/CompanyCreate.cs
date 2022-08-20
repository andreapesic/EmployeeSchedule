using EmployeeSchedule.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeSchedule.MVC.Models.Create
{
    public class CompanyCreate : Company
    {
        [TempData]
        public string ValidationMessage { get; set; } = string.Empty;
        public CompanyCreate()
        {
                
        }
        public CompanyCreate(string validationMessage)
        {
            ValidationMessage = validationMessage;
        }
    }
}
