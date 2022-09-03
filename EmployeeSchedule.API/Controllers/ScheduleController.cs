using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Threading.Tasks;
using System.Reflection;

namespace EmployeeSchedule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _service;
        public ScheduleController(IScheduleService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAll()
        {
            var result = await _service.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Schedule>> Get(int id)
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

        [HttpGet("{employeeId}/employee")]
        public async Task<ActionResult<Schedule>> GetScheduleForEmployee(int employeeId)
        {
            var result = await _service.GetScheduleForEmployee(employeeId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] Schedule entity)
        {
            try
            {
                var result = await _service.Insert(entity);
                Console.WriteLine(result);
                    
                    
                
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Schedule entity)
        {
            try
            {
                entity.Id = id;
                var result = await _service.Update(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _service.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
