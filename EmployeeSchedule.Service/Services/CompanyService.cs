using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeSchedule.Service.Services
{
    public class CompanyService : IGenericService<Company>
    {
        private readonly IUnitOfWork<Company> _unitOfWork;
        public CompanyService(IUnitOfWork<Company> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Delete(int id)
        {
            var entity = await GetById(id);
            var result = await _unitOfWork.Repository.Delete(entity);
            await _unitOfWork.Commit();

            return result;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            var entities = await _unitOfWork.Repository.GetAll();
            return entities;
        }

        public async Task<Company> GetById(int id)
        {
                var entity = await _unitOfWork.Repository.GetById(id);
                return entity;
           
        }

        public async Task<bool> Insert(Company entity)
        {
            if (await CompanyExists(entity))
            {
                throw new Exception("Company exist");
            }

            var result = await _unitOfWork.Repository.Insert(entity);
            await _unitOfWork.Commit();
            return result;
        }
        public Task<IEnumerable<Company>> Search(string criteria, string criteria2 = null, DateTime date = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> Sort(string criteria)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Company entity)
        {
            if (await CompanyExists(entity))
            {
                throw new Exception("Company exist");
            }

            var result = await _unitOfWork.Repository.Update(entity);
            await _unitOfWork.Commit();
            return result;
        }

        private async Task<bool> CompanyExists(Company company)
        {
            var companies = await GetAll();
            return companies.Any(e => (e.IdentificationNumber == company.IdentificationNumber || e.Adress == company.Adress) && e.Id != company.Id);
        }
    }
}
