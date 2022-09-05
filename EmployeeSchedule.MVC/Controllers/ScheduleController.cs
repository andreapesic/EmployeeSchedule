using AutoMapper;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Data.Interface.Pdf;
using EmployeeSchedule.Data.Interface.WebApi;
using EmployeeSchedule.MVC.Extensions;
using EmployeeSchedule.MVC.Models.Create;
using EmployeeSchedule.MVC.Models.ViewModel;
using EmployeeSchedule.MVC.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IWebApiService _apiService;
        private readonly IPdfService _pdfService;

        public ScheduleController(IScheduleService scheduleService, IEmployeeService employeeService, IMapper mapper, IWebApiService apiService, IPdfService pdfService)
        {
            _scheduleService = scheduleService;
            _employeeService = employeeService;
            _mapper = mapper;
            _apiService = apiService;
            _pdfService = pdfService;
        }


        // GET: ScheduleController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Schedule> schedules;

            if(Storage.Instance.IsAdmin == LoginCurrentRole.Admin)
            {
                schedules = await _scheduleService.GetAll();
            }
            else
            {
                schedules = await _scheduleService.GetScheduleForEmployee(Storage.Instance.LoginEmployee.Id);
            }

            schedules = schedules.OrderByDescending(e => e.Date.Date);
        

            var schedulesViewModel = _mapper.Map<List<ScheduleViewModel>>(schedules);
            schedulesViewModel.ForEach(e => e.SetCheckInStatus());

            var employees = await _employeeService.GetAll();
            ViewBag.EmployeeSelectList = employees.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name + e.Surname
            }).ToList();

            ViewBag.Holidays = await _apiService.GetHolidays();

            if(Storage.Instance.IsAdmin == LoginCurrentRole.Admin)
            {
                ViewBag.NumberOfRequests = schedules.Count(e => string.IsNullOrEmpty(e.ShiftWork));
            }
            else
            {
                ViewBag.CreateScheduleEnabled = !schedules.Any(e => e.Date.Date == DateTime.Now.Date && e.Employee.Id == Storage.Instance.LoginEmployee.Id);
            }

            return View(schedulesViewModel);
        }

        // GET: ScheduleController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var schedule = await _apiService.GetScheduleById(id);
            return View(_mapper.Map<ScheduleCreate>(schedule));
        }

        public async Task<FileResult> GeneratePdf(int id)
        {
            var schedules = await _scheduleService.GetScheduleForEmployee(id);

            var filePath = await _pdfService.GeneratePdfScheduleForEmployee(schedules.Where(e => e.Date > DateTime.Now.AddMonths(-1)).ToList());
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, "application/octet-stream", Path.GetFileName(filePath));
        }



        // GET: ScheduleController/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                if (Storage.Instance.IsAdmin == LoginCurrentRole.Employee)
                {
                    var schedule = new Schedule()
                    {
                        Employee = Storage.Instance.LoginEmployee,
                        CheckInTime = DateTime.Now,
                        Date = DateTime.Now,
                        ShiftWork = string.Empty
                    };

                    _ = await _scheduleService.Insert(schedule);
                    return RedirectToAction(nameof(Index));

                }

                var employees = await _employeeService.GetAll();
                var scheduleCreate = new ScheduleCreate();
                scheduleCreate.EmployeeSelectList(employees);
                return View(scheduleCreate);
            }
            catch (Exception ex)
            {
                return View(new ScheduleCreate(ex.Message));
            }
        }

        // POST: ScheduleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ScheduleCreate scheduleCreate)
        {
            try
            {
                scheduleCreate.EmployeeSelectList(await _employeeService.GetAll());

                if (!ModelState.IsValid)
                {
                    return View(scheduleCreate);
                }

                var schedule = _mapper.Map<Schedule>(scheduleCreate);
                //DateTime pom = schedule.Date;
                //schedule.Date = pom.Date.AddHours(23);
                //schedule.Date = schedule.Date.AddMinutes(35);
                await _scheduleService.Insert(schedule);
                scheduleCreate.ValidationMessage = "Success PROBA";
                try
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("itehprojekat51@gmail.com", "qqwfrmdhypwumkpw"),
                        EnableSsl = true,
                    };
                    string subject = "Obavestenje o napravljenom rasporedu";
                    string body = "Postovana/i " + schedule.Employee.Name + ",<br/>" +
                        "Obavestavamo Vas da je kreiran Vas raspored rada za: " + schedule.Date.ToShortDateString()
                        +", "+schedule.ShiftWork+" smena.";

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("itehprojekat51@gmail.com"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(schedule.Employee.Email);
                    smtpClient.SendAsync(mailMessage, new object());
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
                return View(scheduleCreate);
            }
            catch (Exception ex)
            {
                return View(new ScheduleCreate(ex.Message));
            }
        }

        // GET: ScheduleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var schedule = await _scheduleService.GetById(id);

                if (Storage.Instance.IsAdmin == LoginCurrentRole.Employee)
                {
                    schedule.CheckInTime = DateTime.Now;
                    _ = await _scheduleService.Update(schedule);
                    return RedirectToAction(nameof(Index));
                }

                var scheduleCreate = _mapper.Map<ScheduleCreate>(schedule);
                var employees = await _employeeService.GetAll();
                scheduleCreate.EmployeeSelectList(employees);
                return View(scheduleCreate);
            }
            catch (Exception ex)
            {
                return View(new ScheduleCreate(ex.Message));
            }
        }

        // POST: ScheduleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ScheduleCreate scheduleCreate)
        {
            try
            {
                scheduleCreate.Id = id;

                scheduleCreate.EmployeeSelectList(await _employeeService.GetAll());

                if (!ModelState.IsValid)
                {
                    return View(scheduleCreate);
                }

                var employee = _mapper.Map<Schedule>(scheduleCreate);
                await _apiService.UpdateSchedule(employee);
                scheduleCreate.ValidationMessage = "Success";
                return View(scheduleCreate);
            }
            catch (Exception ex) 
            {
                return View(new ScheduleCreate(ex.Message));
            }
        }

        // GET: ScheduleController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _scheduleService.Delete(id);
            TempData["DeleteError"] = !result;
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Search(string text, string employeeId, DateTime date)
        {
            var schedules = await _scheduleService.Search(text ?? string.Empty,employeeId,date);

            if (Storage.Instance.IsAdmin != LoginCurrentRole.Admin)
            {
                schedules = schedules.Where(x => x.Employee.Id == Storage.Instance.LoginEmployee.Id).ToList();
            }

                return PartialView(_mapper.Map<List<ScheduleViewModel>>(schedules));
        }
    }
}
