namespace Payroll.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Payroll.Core;
    using Payroll.ViewModels;

    public class EmployeeQualificationsController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var model = new EmployeeRolesViewModel();

            if (this.User.IsInRole(Constant.Roles.Company))
            {
                model.IsCompanyUser = true;
            }

            return this.View(model);
        }
    }
}
