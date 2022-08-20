using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.Repository.Implementation
{
    public class ScheduleRepository : IScheduleRepository
    {
        public readonly ApplicationDbContext _db;
        public ScheduleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Schedule entity)
        {
            var oldEntity = await GetById(entity.Id);
            _db.Entry(oldEntity).State = EntityState.Deleted;
            return true;
        }

        public async Task<IEnumerable<Schedule>> GetAll()
        {
            var schedules = await _db.Schedule
                .Include(e => e.Employee)
                .ToListAsync();
            return schedules;
        }

        public async Task<Schedule> GetById(int id)
        {
            var schedule = await _db.Schedule
                .Include(e => e.Employee)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (schedule == null)
            {
                throw new NullReferenceException("Schedule doesn't exist");
            }

            return schedule;
        }

        public async Task<IEnumerable<Schedule>> GetScheduleForEmployee(int id)
        {
            var employeeSchedule = await _db.Schedule
                .Include(e => e.Employee)
                .ThenInclude(e => e.Company)
                .Where(e => e.Employee.Id == id)
                .ToListAsync();

            return employeeSchedule;
        }

        public async Task<bool> Insert(Schedule entity)
        {
            var employee = await _db.Employee.SingleOrDefaultAsync(e => e.Id == entity.Employee.Id);
            if (employee == null)
            {
                throw new Exception("Employee doesn't exists");
            }

            entity.Employee = employee;
            await _db.AddAsync(entity);

            return true;
        }

        public async Task<bool> Update(Schedule entity)
        {
            var oldEntity = await GetById(entity.Id);

            var employee = await _db.Employee.SingleOrDefaultAsync(e => e.Id == entity.Employee.Id);
            if (employee == null)
            {
                throw new NullReferenceException("Employee doesn't exists");
            }

            entity.Employee = employee;

            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Update(entity);

            return true;
        }
    }
}
