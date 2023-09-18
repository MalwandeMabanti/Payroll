namespace Payroll.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Payroll.Core;
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class HomeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly UserManager<PayrollUser> userManager;

        public HomeController(IEmployeeService employeeService, UserManager<PayrollUser> userManager)
        {
            this.employeeService = employeeService;
            this.userManager = userManager;
        }

        [Authorize(Roles = $"{Constant.Roles.Company}, {Constant.Roles.Employee}")]
        public async Task<IActionResult> Index()
        {
            Employee? model = new ();
            var payrollUser = await this.userManager.GetUserAsync(this.User);

            if (this.User.IsInRole("Employee"))
            {
                model = this.employeeService.FindByField("Email", payrollUser.Email);
            }
            else
            {
                model.FirstName = payrollUser.FirstName;
                model.LastName = payrollUser.LastName;
                model.Email = payrollUser.Email;
            }

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}