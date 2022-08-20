using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Interface
{
    public interface IGenericService<T> where T: class
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<IEnumerable<T>> Search(string criteria, string criteria2 = default, DateTime date = default);
        public Task<IEnumerable<T>> Sort(string criteria);

        public Task<T> GetById(int id);
        public Task<bool> Insert(T entity);
        public Task<bool> Update(T entity);
        public Task<bool> Delete(int id);

    }
}
