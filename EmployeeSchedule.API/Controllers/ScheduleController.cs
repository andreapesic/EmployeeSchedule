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
                    
                    try
                    {
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("andrea.softveri.spacentar@gmail.com", "dnttozzavgawlqnw"),
                            EnableSsl = true,
                        };
                        string subject = "Obavestenje o napravljenom rasporedu";
                        string body = "Postovana/i " + entity.Employee.Name + ",<br/>" +
                            "Obavestavamo Vas da je kreiran Vas raspored rada za: " + entity.Date;

                        var mailMessage = new MailMessage
                        {
                            From = new MailAddress("andrea.softveri.spacentar@gmail.com"),
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true,
                        };

                        mailMessage.To.Add(entity.Employee.Email);
                        smtpClient.SendAsync(mailMessage, new object());
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e.Message);
                    }
                
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
