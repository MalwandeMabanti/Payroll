namespace Payroll.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Payroll.Core;
    using Payroll.ViewModels;

    public class EmployeeAddressController : Controller
    {
        [Authorize(Roles = $"{Constant.Roles.Company}, {Constant.Roles.Employee}")]
        [HttpGet]
        public IActionResult Index()
        {
            var model = new EmployeeAddressViewModel()
            {
                IsCompanyUser = this.User.IsInRole(Constant.Roles.Company),
            };

            return this.View(model);
        }
    }
}
