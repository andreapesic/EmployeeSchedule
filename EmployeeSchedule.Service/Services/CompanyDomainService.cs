using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services
{
    public class CompanyDomainService : IGenericService<CompanyDomain>
    {

        private readonly IUnitOfWork<CompanyDomain> _unitOfWork;
       
        public CompanyDomainService(IUnitOfWork<CompanyDomain> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompanyDomain>> GetAll()
        {
            var entities = await _unitOfWork.Repository.GetAll();
            return entities;
        }

        public async Task<CompanyDomain> GetById(int id)
        {
            var entity = await _unitOfWork.Repository.GetById(id);
            return entity;
        }

        public Task<bool> Insert(CompanyDomain entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CompanyDomain>> Search(string criteria, string criteria2 = null, DateTime date = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CompanyDomain>> Sort(string criteria)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(CompanyDomain entity)
        {
            throw new NotImplementedException();
        }
    }
}
