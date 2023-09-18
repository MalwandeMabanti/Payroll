namespace Payroll.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Payroll.Core;
    using Payroll.ViewModels;

    [Authorize]
    public class EmployeeDependantController : Controller
    {
        public IActionResult Index()
        {
            var model = new EmployeeDependantViewModel();

            if (this.User.IsInRole(Constant.Roles.Company))
            {
                model.IsCompanyUser = true;
            }

            return this.View(model);
        }
    }
}