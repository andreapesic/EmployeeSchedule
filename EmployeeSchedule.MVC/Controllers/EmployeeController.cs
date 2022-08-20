using AutoMapper;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Data.Interface.WebApi;
using EmployeeSchedule.MVC.Models.Create;
using EmployeeSchedule.MVC.Models.ViewModel;
using EmployeeSchedule.MVC.Session;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericService<Company> _companyService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IWebApiService _apiService;

        public EmployeeController(IEmployeeService employeeService, IGenericService<Company> companyService, IMapper mapper, IWebApiService apiService)
        {
            _employeeService = employeeService;
            _companyService = companyService;
            _mapper = mapper;
            _apiService = apiService;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var employees = await _employeeService.GetAll();
            return View(_mapper.Map<List<EmployeeViewModel>>(employees));
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var employee = await _employeeService.GetById(id);
                var employeeCreate = _mapper.Map<EmployeeCreate>(employee);
                return View(employeeCreate);
            }
            catch (Exception ex)
            {
                return View(new EmployeeCreate(ex.Message));
            }
        }

        // GET: EmployeeController/Create
        public async Task<ActionResult> Create()
        {
            var companies = await _companyService.GetAll();
            var employeeCreate = new EmployeeCreate();
            employeeCreate.CompanySelectList(companies);
            var cities = await _apiService.GetCities();
            employeeCreate.CitySelectList(cities);
            return View(employeeCreate);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeCreate employeeCreate)
        {
            try
            {
                employeeCreate.CompanySelectList(await _companyService.GetAll());
                employeeCreate.CitySelectList(await _apiService.GetCities());
                if (!ModelState.IsValid)
                {
                    return View(employeeCreate);
                }

                var employee = _mapper.Map<Employee>(employeeCreate);
                await _employeeService.Insert(employee);
                employeeCreate.ValidationMessage = "Success";
                return View(employeeCreate);
            }
            catch (Exception ex)
            {
                return View(new EmployeeCreate(ex.Message));
            }
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetById(id);
            var employeeCreate = _mapper.Map<EmployeeCreate>(employee);
            var companies = await _companyService.GetAll();
            var cities = await _apiService.GetCities();
            employeeCreate.CitySelectList(cities);
            employeeCreate.CompanySelectList(companies);
            return View(employeeCreate);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeCreate employeeCreate)
        {
            try
            {
                employeeCreate.Id = id;
                employeeCreate.CompanySelectList(await _companyService.GetAll());
                employeeCreate.CitySelectList(await _apiService.GetCities());

                if (!ModelState.IsValid)
                {
                    return View(employeeCreate);
                }

                var employee = _mapper.Map<Employee>(employeeCreate);
                await _employeeService.Update(employee);
                employeeCreate.ValidationMessage = "Success";
                return View(employeeCreate);
            }
            catch (Exception ex)
            {
                return View(new EmployeeCreate(ex.Message));
            }
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _apiService.DeleteEmployee(id);
            TempData["DeleteError"] = !result;
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Search(string criteria)
        {
            var employees = await _employeeService.Search(criteria ?? string.Empty);
            return PartialView(_mapper.Map<List<EmployeeViewModel>>(employees));
        }

        public async Task<ActionResult> Sort(string criteria)
        {
            var employees = await _employeeService.Sort(criteria);
            return PartialView(_mapper.Map<List<EmployeeViewModel>>(employees));
        }

        [HttpPost]
        public async Task<ActionResult> Login(EmployeeCreate employeeCreate)
        {
            var loginEmployee = await _employeeService.Login(employeeCreate.Email, employeeCreate.Password);
            if (loginEmployee == null)
            {
                employeeCreate.ValidationMessage = "Employee doesn't exist";
                return View(employeeCreate);
            }
            Storage.Instance.LoginEmployee = loginEmployee;
            Storage.Instance.IsAdmin = loginEmployee.Administrator ? LoginCurrentRole.Admin : LoginCurrentRole.Employee;

            if (loginEmployee.Administrator)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index), "Schedule");
        }

        public ActionResult Login()
        {
            Storage.Instance.LoginEmployee = null;
            Storage.Instance.IsAdmin = LoginCurrentRole.No;
            return View(new EmployeeCreate());
        }
    }
}
