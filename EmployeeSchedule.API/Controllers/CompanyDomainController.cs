using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyDomainController : ControllerBase
    {
        private readonly IGenericService<CompanyDomain> _service;
       
        public CompanyDomainController(IGenericService<CompanyDomain> service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDomain>>> GetAll()
        {
            var result = await _service.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
