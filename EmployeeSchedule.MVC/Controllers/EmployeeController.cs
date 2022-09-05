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
using EmployeeSchedule.Data.Helper;
using System.Linq;

namespace EmployeeSchedule.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericService<Company> _companyService;
        private readonly IScheduleService _scheduleService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IWebApiService _apiService;
        private readonly string key = "b14ca5898a4e4133bbce2ea2315a1916";

        public EmployeeController(IEmployeeService employeeService, IGenericService<Company> companyService, IScheduleService scheduleService, IMapper mapper, IWebApiService apiService)
        {
            _employeeService = employeeService;
            _companyService = companyService;
            _scheduleService = scheduleService;
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
                string hashedPassword = AesOperation.EncryptString(key, employee.Password);
                employee.Password = hashedPassword;
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

        public async Task<ActionResult> EmployeeSummary()
        {
            var schedules = await _scheduleService.GetAll();
            var employees = await _employeeService.GetAll();

            List<EmployeeSummary> summaries = new List<EmployeeSummary>();

            foreach (var e in employees)
            {
                var schedulesForEmployee = schedules.Where(x => x.Employee.Id == e.Id).ToList();
                EmployeeSummary es = new EmployeeSummary
                {
                    Employee = e,
                    Schedules = schedulesForEmployee.ToList(),
                    NumberOfSchedules = schedulesForEmployee.Count(),
                    OnTimeCount = schedulesForEmployee.Where(x => !x.Late && !x.CheckInTime.Equals(DateTime.MinValue)).Count(),
                    LateCount = schedulesForEmployee.Where(x => x.Late).Count(),
                    NoStatisticsCount = schedulesForEmployee.Where(x => x.CheckInTime.Equals(DateTime.MinValue)).Count(),
                    FreeDaysCount = schedulesForEmployee.Where(x => x.ShiftWork == "Slobodan dan").Count()


                };
                summaries.Add(es);
            }


            return View(summaries);
        }
        public async Task<ActionResult> EmployeeSummarySearch(string criteria)
        {
            var employees = await _employeeService.Search(criteria ?? string.Empty);
            var schedules = await _scheduleService.GetAll();

            List<EmployeeSummary> summaries = new List<EmployeeSummary>();

            foreach (var e in employees)
            {
                var schedulesForEmployee = schedules.Where(x => x.Employee.Id == e.Id).ToList();
                EmployeeSummary es = new EmployeeSummary
                {
                    Employee = e,
                    Schedules = schedulesForEmployee.ToList(),
                    NumberOfSchedules = schedulesForEmployee.Count(),
                    OnTimeCount = schedulesForEmployee.Where(x => !x.Late && !x.CheckInTime.Equals(DateTime.MinValue)).Count(),
                    LateCount = schedulesForEmployee.Where(x => x.Late).Count(),
                    NoStatisticsCount = schedulesForEmployee.Where(x => x.CheckInTime.Equals(DateTime.MinValue)).Count(),
                    FreeDaysCount = schedulesForEmployee.Where(x => x.ShiftWork == "Slobodan dan").Count()


                };
                summaries.Add(es);
            }
            return PartialView("EmployeeSummarySearch",_mapper.Map<List<EmployeeSummaryViewModel>>(summaries));
            
        }
    } }

  
