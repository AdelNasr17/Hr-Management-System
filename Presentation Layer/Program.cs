using Bussiness_Layer.Services.DepartmentService;
using Bussiness_Layer.Services.EmployeeService;
using DataAccess.Data.DbContexts;
using Bussiness_Layer.Comman.Services.AttachmentServices;
using DataAccess.Repositories.DepartmentRepository;
using DataAccess.Repositories.EmployeeRepository;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Presentation_Layer.Mapping;

namespace Presentation_Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.
            
            builder.Services.AddControllersWithViews();

            //DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            //Services
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            //IUnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            // Auto Mapper
            builder.Services.AddAutoMapper(M => M.AddProfile(new Presentation_Layer.Mapping.MappingProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new Bussiness_Layer.Mapping.MappingProfile()));


            //Images
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();
            #endregion

            var app = builder.Build();

            #region  Configure the HTTP request pipeline.
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            #endregion
            app.Run();
        }
    }
}
