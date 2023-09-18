namespace Payroll
{
    using FluentValidation;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Middlewares;
    using Payroll.Models;
    using Payroll.Services;
    using Payroll.Validators;

    using StackExchange.Redis;

    public static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configBuilder.Build();

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<ISaveChangesInterceptor, AuditMiddleware>();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<AuditContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AuthDbContextConnection")));

            builder.Services.AddDefaultIdentity<PayrollUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IValidator<EmployeeQualification>, EmployeeQualificationValidator>();
            builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

            builder.Services.AddScoped<IEnumCountryService, EnumCountryService>();
            builder.Services.AddScoped<IEnumDependantTypeService, EnumDependantTypeService>();
            builder.Services.AddScoped<IEnumEducationLevelService, EnumEducationLevelService>();
            builder.Services.AddScoped<IEnumInstitutionTypeService, EnumInstitutionTypeService>();
            builder.Services.AddScoped<IEnumTitleService, EnumTitleService>();
            builder.Services.AddScoped<IEnumQualificationService, EnumQualificationService>();

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IEmployeeAddressService, EmployeeAddressService>();
            builder.Services.AddScoped<IEmployeeDependantService, EmployeeDependantService>();
            builder.Services.AddScoped<IEmployeeQualificationService, EmployeeQualificationService>();
            builder.Services.AddScoped<ICompanyUserService, CompanyUserService>();
            builder.Services.AddScoped<IAuditService, AuditService>();
            builder.Services.AddScoped<IAuditTableService, AuditTableService>();

            builder.Services.AddScoped<IValidator<Employee>, EmployeeValidator>();
            builder.Services.AddScoped<IValidator<EmployeeDependant>, EmployeeDependantValidator>();
            builder.Services.AddScoped<IValidator<EmployeeAddress>, EmployeeAddressValidator>();
            builder.Services.AddScoped<IValidator<Register>, RegisterValidator>();
            builder.Services.AddScoped<IValidator<CompanyUser>, CompanyUserValidator>();

            builder.Services.AddSingleton<ICacheService, RedisCacheService>();

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints = { { "localhost" } },
                    AllowAdmin = true,
                    ConnectTimeout = 500,
                };

                options.InstanceName = "Payroll_";
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Authentication/Index";
                options.AccessDeniedPath = "/Authentication/AccessDenied";
            });

            var app = builder.Build();

            // Seed data to the database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "employeeDependant",
                pattern: "{controller=EmployeeDependants}/{action=Index}");

            app.MapControllerRoute(
                name: "edit",
                pattern: "{controller=EmployeeDependants}/{action=Edit}/{id?}");

            app.MapControllerRoute(
                name: "employeeDetails",
                pattern: "{controller=Employee}/{action=Delete}/{id?}");

            app.Run();
        }
    }
}