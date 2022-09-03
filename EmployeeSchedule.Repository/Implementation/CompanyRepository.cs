using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.Repository.Implementation
{
    public class CompanyRepository : IRepository<Company>
    {
        public readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Delete(Company entity)
        {
            var oldEntity = await GetById(entity.Id);
            _db.Entry(oldEntity).State = EntityState.Deleted;
            return true;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            var companies = await _db.Company.Include(x => x.Domain).ToListAsync();
            return companies;
        }

        public async Task<Company> GetById(int id)
        {
            var company = await _db.Company.Include(x=>x.Domain).SingleOrDefaultAsync(e => e.Id == id);

            if(company == null)
            {
                throw new Exception("Company doesn't exist");
            }

            return company;
        }

        public async Task<bool> Insert(Company entity)
        {
            var domain = await _db.CompanyDomain.SingleOrDefaultAsync(e => e.Id == entity.Domain.Id);
            if (domain == null)
            {
                throw new NullReferenceException("Company domain doesn't exists");
            }
            entity.Domain = domain;

            await _db.AddAsync(entity);
             
            return true;
        }

        public async Task<bool> Update(Company entity)
        {
            var oldEntity = await GetById(entity.Id);

            var domain = await _db.CompanyDomain.SingleOrDefaultAsync(e => e.Id == entity.Domain.Id);
            if (domain == null)
            {
                throw new NullReferenceException("Company domain doesn't exists");
            }
            entity.Domain = domain;

            _db.Entry(oldEntity).State = EntityState.Detached;
            _db.Update(entity);

            return true;
        }
    }
}
