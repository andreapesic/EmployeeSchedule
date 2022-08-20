using EmployeeSchedule.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Interface.Pdf
{
    public interface IPdfService
    {
        Task<string> GeneratePdfScheduleForEmployee(List<Schedule> schedules);
    }
}
