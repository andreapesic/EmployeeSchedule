using AutoMapper;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.MVC.Models.Create;
using EmployeeSchedule.MVC.Models.ViewModel;

namespace EmployeeSchedule.MVC.Helper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Company, CompanyCreate>();
            CreateMap<CompanyCreate, Company>();

            CreateMap<Employee, EmployeeCreate>();
            CreateMap<EmployeeCreate, Employee>();
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<Employee, EmployeeViewModel>();

            CreateMap<Schedule, ScheduleCreate>();
            CreateMap<ScheduleCreate, Schedule>();
            CreateMap<ScheduleViewModel, Schedule>();
            CreateMap<Schedule, ScheduleViewModel>();
            CreateMap<EmployeeSummaryViewModel, EmployeeSummary>();
            CreateMap<EmployeeSummary, EmployeeSummaryViewModel>();
            

        }
    }
}
