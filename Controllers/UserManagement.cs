namespace Payroll.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Payroll.Data;

    public class UserManagement : Controller
    {
        private readonly ApplicationDbContext context;

        public UserManagement(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}
