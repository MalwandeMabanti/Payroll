namespace Payroll.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Payroll.Core;

    public class EmployeeController : Controller
    {
        [Authorize(Roles = $"{Constant.Roles.Company}")]
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
