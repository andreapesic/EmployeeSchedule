using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Entities.ApiEntities;
using System.Collections.Generic;

namespace EmployeeSchedule.Service.Storage
{
    public class Storage
    {
        private static Storage instance; 
        public List<City> Cities { get; set; }
        public List<Holiday> Holidays { get; set; }
        public List<Employee> Employees { get; set; }
        private Storage()
        {

        }
        public static Storage Instance
        {
            get
            {
                instance ??= new Storage();
                return instance;
            }
        }
    }
}
