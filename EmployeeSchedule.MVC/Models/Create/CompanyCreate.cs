using EmployeeSchedule.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeSchedule.MVC.Models.Create
{
    public class CompanyCreate : Company
    {
        [TempData]
        public string ValidationMessage { get; set; } = string.Empty;
        public List<SelectListItem> CompanyDomainsSelectList { get; set; }

        public CompanyCreate()
        {
            Domain = new CompanyDomain();    
        }
        public CompanyCreate(string validationMessage)
        {
            ValidationMessage = validationMessage;
            Domain = new CompanyDomain();
        }

        public void CompanyDomainSelectList(IEnumerable<CompanyDomain> domains)
        {
            CompanyDomainsSelectList = domains.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            }).ToList();
        }
    }
}
