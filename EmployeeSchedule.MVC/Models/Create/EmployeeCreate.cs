using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Entities.ApiEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeSchedule.MVC.Models.Create
{
    public class EmployeeCreate : Employee
    {
        [TempData]
        public string ValidationMessage { get; set; } = string.Empty;
        public List<SelectListItem> CompaniesSelectList { get; set; }
        public List<SelectListItem> CitiesSelectList { get; set; }
        public EmployeeCreate()
        {
            Company = new Company();
        }
        public EmployeeCreate(string validationMessage)
        {
            ValidationMessage = validationMessage;
            Company = new Company();
        }
        public void CompanySelectList(IEnumerable<Company> companies)
        {
            CompaniesSelectList = companies.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();
        }

        public void CitySelectList(IEnumerable<City> cities)
        {
            CitiesSelectList = cities.Select(e => new SelectListItem
            {
                Value = e.Name.ToString(),
                Text = e.Name
            }).ToList();
        }

    }
}
