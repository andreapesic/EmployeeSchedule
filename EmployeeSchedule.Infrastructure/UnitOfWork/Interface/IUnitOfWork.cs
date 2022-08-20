using EmployeeSchedule.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace EmployeeSchedule.Infrastructure.UnitOfWork.Interface
{
    public interface IUnitOfWork<T> : IDisposable  where T: class 
    {
        public IRepository<T> Repository { get; set; }
        public Task Commit();

    }
}
