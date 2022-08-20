using AutoMapper;
using EmployeeSchedule.MVC.Helper;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeSchedule.MVC.Extensions
{
    public static class Extensions
    {
        public static void AddMapperConfiguration(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
