using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSchedule.Data.Entities
{
    public class EmployeeSummary
    {
        
        //public int Id { get { return Employee.Id}; set { value = Employee.Id }; }
        public Employee Employee { get; set; }
        public List<Schedule> Schedules  { get; set; }

        public int NumberOfSchedules { get; set; }

        public int OnTimeCount { get; set; }

        public int LateCount { get; set; }

        public int FreeDaysCount { get; set; }
        public int NoStatisticsCount { get; set; } 

    }
}
