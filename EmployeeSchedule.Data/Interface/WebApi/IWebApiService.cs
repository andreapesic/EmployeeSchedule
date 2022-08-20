
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Entities.ApiEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Interface.WebApi
{
    public interface IWebApiService
    {
        Task<bool> DeleteEmployee(int id);
        Task<IEnumerable<Company>> GetCompanies();
        Task<Schedule> GetScheduleById(int id);
        Task<bool> UpdateSchedule(Schedule schedule);
        Task<List<Holiday>> GetHolidays();
        Task<List<City>> GetCities();
    }
}
