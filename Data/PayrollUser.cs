namespace Payroll.Data
{
    using Microsoft.AspNetCore.Identity;

    public class PayrollUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
