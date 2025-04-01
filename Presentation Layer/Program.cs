using Bussiness_Layer.Services.DepartmentService;
using Bussiness_Layer.Services.EmployeeService;
using DataAccess.Data.DbContexts;
using Bussiness_Layer.Comman.Services.AttachmentServices;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DataAccess.Models.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Bussiness_Layer.Services.EmailService;


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


            //IdentityUser
            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>();
            //builder.Services.AddScoped<SignInManager<ApplicationUser>>();

            //Signature For Methods
            //Repositories==>stores
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(Options=>
            {
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequireDigit = true;
                Options.Password.RequireNonAlphanumeric = true;//@ # $
                Options.Password.RequiredLength = 5;
          
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();//  PasswordSignInAsync Depaend On AddDefaultTokenProviders Servoce


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options=>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Home/Error";
                    options.LogoutPath = "/Account/Login";


                }
                );



            //SendEmail
            builder.Services.AddScoped<IEmailService,EmailService>();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}")
                .WithStaticAssets();

            #endregion
            app.Run();
        }
    }
}
