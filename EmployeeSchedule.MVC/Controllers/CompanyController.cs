using AutoMapper;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Data.Interface.WebApi;
using EmployeeSchedule.MVC.Models.Create;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.MVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IGenericService<Company> _service;
        private readonly IMapper _mapper;
        private readonly IWebApiService _apiService;
        private readonly IGenericService<CompanyDomain> _companyDomainService;
        public CompanyController(IGenericService<Company> service, IGenericService<CompanyDomain> companyDomainService, IMapper mapper, IWebApiService apiService)
        {
            _service = service;
            _mapper = mapper;
            _apiService = apiService;
            _companyDomainService = companyDomainService;
        }

        // GET: CompanyController
        public async Task<ActionResult> Index()
        {
            var companies = await _apiService.GetCompanies();
            return View(_mapper.Map<List<CompanyCreate>>(companies));
        }

        // GET: CompanyController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var company = await _service.GetById(id);
                return View(_mapper.Map<CompanyCreate>(company));
            }
            catch (Exception ex)
            {
                return View(new CompanyCreate(ex.Message));
            }
        }

        // GET: CompanyController/Create
        public async Task<ActionResult> Create()
        {
            var domains = await _companyDomainService.GetAll();
            var companyCreate = new CompanyCreate();
            companyCreate.CompanyDomainSelectList(domains);
            return View(companyCreate);
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyCreate companyCreate)
        {
            try
            {
                companyCreate.CompanyDomainSelectList(await _companyDomainService.GetAll());

                if (!ModelState.IsValid)
                {
                    return View(companyCreate);
                }

                var company = _mapper.Map<Company>(companyCreate);
                await _service.Insert(company);
                companyCreate.ValidationMessage = "Success";
                return View(companyCreate);
            }
            catch (Exception ex)
            {
                return View(new CompanyCreate(ex.Message));
            }
        }

        // GET: CompanyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var company = await _service.GetById(id);
            var companyCreate = _mapper.Map<CompanyCreate>(company);
            var domains = await _companyDomainService.GetAll();
            companyCreate.CompanyDomainSelectList(domains);
            return View(companyCreate);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CompanyCreate companyCreate)
        {
            try
            {
                companyCreate.Id = id;
                companyCreate.CompanyDomainSelectList(await _companyDomainService.GetAll());

                if (!ModelState.IsValid)
                {
                    return View(companyCreate);
                }

                var company = _mapper.Map<Company>(companyCreate);
                await _service.Update(company);
                companyCreate.ValidationMessage = "Success";
                return View(companyCreate);
            }
            catch (Exception  ex)
            {
                return View(new CompanyCreate(ex.Message));
            }
        }

    }
}
