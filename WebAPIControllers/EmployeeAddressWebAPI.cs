namespace Payroll.WebAPIControllers
{
    using System.Security.Claims;

    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using FluentValidation;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    using Payroll.Core;
    using Payroll.Interfaces;
    using Payroll.Models;
    using Payroll.Validators;

    public class EmployeeAddressWebAPI : Controller
    {
        private readonly IEmployeeAddressService employeeAddressService;
        private readonly IValidator<EmployeeAddress> validator;
        private readonly IEmployeeService employeeService;

        public EmployeeAddressWebAPI(
            IEmployeeAddressService employeeAddressService,
            IValidator<EmployeeAddress> validator,
            IEmployeeService employeeService)
        {
            this.employeeAddressService = employeeAddressService;
            this.validator = validator;
            this.employeeService = employeeService;
        }

        [Authorize(Roles = $"{Constant.Roles.Company}, {Constant.Roles.Employee}")]
        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            if (this.User.IsInRole(Constant.Roles.Employee))
            {
                var userEmail = this.User.FindFirstValue(ClaimTypes.Email);

                var employee = this.employeeService.FindByField("Email", userEmail);
                if (employee == null)
                {
                    return this.BadRequest($"Employee with email {userEmail} was not found");
                }

                var address = new List<EmployeeAddress?>
                {
                    this.employeeAddressService.FindByField("EmployeeId", employee.EmployeeId),
                };

                return this.Json(DataSourceLoader.Load(address, loadOptions));
            }
            else if (this.User.IsInRole(Constant.Roles.Company))
            {
                var employeeaddresses = this.employeeAddressService.GetAll();
                return this.Json(DataSourceLoader.Load(this.employeeAddressService.GetAll(), loadOptions));
            }

            return this.RedirectToAction("Index");
        }

        [Authorize(Roles = Constant.Roles.Company)]
        [HttpPost]
        public IActionResult Create(string values)
        {
            var model = JsonConvert.DeserializeObject<EmployeeAddress>(values);
            if (model == null)
            {
                this.ModelState.AddModelError("all", "1 one or more properties malformed");
                return this.BadRequest(this.ModelState);
            }

            var result = this.validator.Validate(model, _ => _.IncludeRuleSets("default", "Create"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.employeeAddressService.Add(model);

            return this.Ok(model);
        }

        [Authorize(Roles = $"{Constant.Roles.Company}, {Constant.Roles.Employee}")]
        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var model = this.employeeAddressService.GetById(key);

            JsonConvert.PopulateObject(values, model);

            var result = this.validator.Validate(model, _ => _.IncludeRuleSets("default", "Edit"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.employeeAddressService.Update(model);

            return this.Ok(model);
        }

        [Authorize(Roles = Constant.Roles.Company)]
        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var model = new EmployeeAddress()
            {
                AddressId = key,
            };

            var result = this.validator.Validate(model, _ => _.IncludeRuleSets("Delete"));
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.employeeAddressService.Delete(model.AddressId);

            return this.Ok();
        }
    }
}
