using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Repository.Implementation
{
    public class CompanyDomainRepository : IRepository<CompanyDomain>
    {
        public readonly ApplicationDbContext _db;
        public CompanyDomainRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        

        public async Task<IEnumerable<CompanyDomain>> GetAll()
        {
            var domains = await _db.CompanyDomain.ToListAsync();

            return domains;
        }

        public async Task<CompanyDomain> GetById(int id)
        {
            var domain = await _db.CompanyDomain.SingleOrDefaultAsync(e => e.Id == id);

            if (domain == null)
            {
                throw new Exception("Domain doesn't exists");
            }

            return domain;
        }

        public Task<bool> Insert(CompanyDomain entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(CompanyDomain entity)
        {
            throw new NotImplementedException();
        }
       
        public Task<bool> Delete(CompanyDomain entity)
        {
            throw new NotImplementedException();
        }
    }
}
