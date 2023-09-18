namespace Payroll.Controllers
{
    using System.Security.Claims;
    using System.Text;

    using FluentValidation;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class AuthenticationController : Controller
    {
        private readonly SignInManager<PayrollUser> signInManager;
        private readonly UserManager<PayrollUser> userManager;
        private readonly IUserStore<PayrollUser> userStore;
        private readonly IUserEmailStore<PayrollUser> emailStore;
        private readonly ILogger<AuthenticationController> logger;
        private readonly IEmailSender emailSender;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IValidator<Register> registerValidator;
        private readonly AuthDbContext authContext;
        private readonly ICompanyUserService companyUser;
        private readonly IEmployeeService employeeService;

        public AuthenticationController(
            UserManager<PayrollUser> userManager,
            IUserStore<PayrollUser> userStore,
            SignInManager<PayrollUser> signInManager,
            ILogger<AuthenticationController> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IValidator<Register> registerValidator,
            AuthDbContext authDbContext,
            ICompanyUserService companyUser,
            IEmployeeService employeeService)
        {
            this.userManager = userManager;
            this.userStore = userStore;
            this.emailStore = this.GetEmailStore();
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.roleManager = roleManager;
            this.registerValidator = registerValidator;
            this.authContext = authDbContext;
            this.companyUser = companyUser;
            this.employeeService = employeeService;
        }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult AccessDenied()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model, string? returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.ModelState.IsValid)
            {
                var user = await this.signInManager.UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid email address.");
                    return this.View("Index");
                }

                var result = await this.signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!result.Succeeded)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View("Index");
                }

                var roles = await this.signInManager.UserManager.GetRolesAsync(user);

                if (roles.Any())
                {
                    var roleClaim = string.Join(",", roles);
                    var claims = new List<Claim>
                        {
                            new Claim("Roles", roleClaim),
                        };

                    await this.signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);
                }

                this.logger.LogInformation("User logged in.");
                return this.LocalRedirect(returnUrl);
            }

            return this.View("Index");
        }

        public async Task<IActionResult> Register()
        {
            var roles = await this.roleManager.Roles.ToListAsync();
            this.ViewBag.Roles = roles;
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model, string? returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            CompanyUser? companyUser = this.companyUser.FindByField("Email", model.Email);
            Employee? employee = this.employeeService.FindByField("Email", model.Email);

            model.Role = employee != null ? "Employee" : companyUser != null ? "Company" : string.Empty;
            model.FirstName = companyUser?.CompanyUserFirstName ?? employee?.FirstName ?? model.FirstName;
            model.LastName = companyUser?.CompanyUserLastName ?? employee?.LastName ?? model.LastName;

            var validationResult = this.registerValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                if (!this.authContext.Users.Any())
                {
                    model.Role = "Company";
                    model.FirstName = "Admin";
                    model.LastName = "Admin";
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid registration attempt.");

                    return this.View();
                }
            }

            var user = this.CreateUser();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await this.userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            await this.emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                return this.View();
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            this.logger.LogInformation("User created a new account with password.");

            await this.signInManager.SignInAsync(user, isPersistent: false);

            return this.LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await this.signInManager.SignOutAsync();
            this.logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return this.LocalRedirect(returnUrl);
            }
            else
            {
                return this.RedirectToPage("Index");
            }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                this.ModelState.AddModelError(string.Empty, this.ErrorMessage);
            }

            returnUrl ??= this.Url.Content("~/");

            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            this.ReturnUrl = returnUrl;
        }

        private PayrollUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<PayrollUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(PayrollUser)}'. " +
                    $"Ensure that '{nameof(PayrollUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<PayrollUser> GetEmailStore()
        {
            if (!this.userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }

            return (IUserEmailStore<PayrollUser>)this.userStore;
        }
    }
}
