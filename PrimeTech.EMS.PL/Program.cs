using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrimeTech.EMS.BLL.Common.Services;
using PrimeTech.EMS.BLL.Mapping;
using PrimeTech.EMS.BLL.Services.DepartmentServices;
using PrimeTech.EMS.BLL.Services.EmployeeServices;
using PrimeTech.EMS.DAL.Models.DepartmentModel;
using PrimeTech.EMS.DAL.Persistance.Data.Contexts;
using PrimeTech.EMS.DAL.Persistence.Repositories.DepartmentRepository;
using PrimeTech.EMS.DAL.Persistence.Repositories.EmployeeRepository;
using PrimeTech.EMS.DAL.Persistence.UnitOfWork;

namespace PrimeTech.EMS.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            #region Add services to the container.

            // Services LifeTime 
            // 1. Singelton : Per Application  [in caching , logging service]
            // 2. Scoped   : Per Request
            // 3. Transient : Per Operation

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            // ==
            // var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // 
            // builder.Services.AddDbContext<ApplicationDbContext>(options => 
            // {
            //     options.UseSqlServer(connectionString);
            // });
            // builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // Allow DI For IDepartmentRepository
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

            // builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(E=>E.AddProfile(new MappingProfile()));

            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
