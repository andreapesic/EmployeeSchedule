using EmployeeSchedule.Data;
using EmployeeSchedule.Data.Entities;
using EmployeeSchedule.Data.Interface;
using EmployeeSchedule.Data.Interface.Pdf;
using EmployeeSchedule.Data.Interface.WebApi;
using EmployeeSchedule.Infrastructure.UnitOfWork.Implementation;
using EmployeeSchedule.Infrastructure.UnitOfWork.Interface;
using EmployeeSchedule.MVC.Extensions;
using EmployeeSchedule.Repository.Implementation;
using EmployeeSchedule.Repository.Interface;
using EmployeeSchedule.Service.Services;
using EmployeeSchedule.Service.Services.Pdf;
using EmployeeSchedule.Service.Services.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeeSchedule.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connection")));
            services.AddMapperConfiguration();
            services.AddScoped<IRepository<CompanyDomain>, CompanyDomainRepository>();
            services.AddScoped<IRepository<Company>, CompanyRepository>();
            services.AddScoped<IRepository<Employee>, EmployeeRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IUnitOfWork<CompanyDomain>, CompanyDomainUnitOfWork>();
            services.AddScoped<IUnitOfWork<Company>, CompanyUnitOfWork>();
            services.AddScoped<IUnitOfWork<Employee>, EmployeeUnitOfWork>();
            services.AddScoped<IUnitOfWork<Schedule>, ScheduleUnitOfWork>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IGenericService<CompanyDomain>, CompanyDomainService>();
            services.AddScoped<IGenericService<Company>, CompanyService>();
            services.AddScoped<IWebApiService, WebApiService>();
            services.AddScoped<IPdfService, PdfService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Employee}/{action=Login}/{id?}");
            });
        }
    }
}
