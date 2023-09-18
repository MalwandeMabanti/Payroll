namespace Payroll.WebAPIControllers
{
    using System.Security.Claims;

    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using FluentValidation;
    using FluentValidation.AspNetCore;
    using FluentValidation.Results;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    using Payroll.Core;
    using Payroll.Interfaces;
    using Payroll.Models;

    [Authorize]
    public class EmployeeDependantWebAPI : Controller
    {
        private readonly IEmployeeDependantService employeeDependantService;
        private readonly IEnumDependantTypeService enumDependantTypeService;
        private readonly IEmployeeService employeeService;
        private readonly IValidator<EmployeeDependant> validator;

        public EmployeeDependantWebAPI(
            IEmployeeDependantService employeeDependantService,
            IEnumDependantTypeService enumDependantTypeService,
            IValidator<EmployeeDependant> validator,
            IEmployeeService employeeService)
        {
            this.employeeDependantService = employeeDependantService;
            this.enumDependantTypeService = enumDependantTypeService;
            this.validator = validator;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions loadOptions)
        {
            List<EmployeeDependant> employeeDependants;

            if (this.User.IsInRole(Constant.Roles.Company))
            {
                employeeDependants = this.employeeDependantService.GetAll();
                return this.Json(DataSourceLoader.Load(employeeDependants, loadOptions));
            }
            else if (this.User.IsInRole(Constant.Roles.Employee))
            {
                var userEmail = this.User.FindFirstValue(ClaimTypes.Email);
                var employee = this.employeeService.FindByField("Email", userEmail);

                if (employee == null)
                {
                    return this.BadRequest("{\"Error\":[\"Invalid Employee\"]}");
                }

                employeeDependants = this.employeeDependantService.FindAllByField("EmployeeId", employee.EmployeeId);

                return this.Json(DataSourceLoader.Load(employeeDependants, loadOptions));
            }

            return this.RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(string values)
        {
            var model = new EmployeeDependant();

            if (this.User.IsInRole(Constant.Roles.Employee))
            {
                var userEmail = this.User.FindFirstValue(ClaimTypes.Email);
                var employee = this.employeeService.FindByField("Email", userEmail);

                if (employee == null)
                {
                    return this.BadRequest("{\"Error\":[\"Invalid Employee\"]}");
                }

                model.EmployeeId = employee.EmployeeId;
            }

            JsonConvert.PopulateObject(values, model);

            ValidationResult result = this.validator.Validate(model, _ => _.IncludeRuleSets("Create"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return this.BadRequest(this.ModelState);
            }

            this.employeeDependantService.Add(model);

            return this.Ok(model);
        }

        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var model = this.employeeDependantService.GetById(key);

            if (model == null)
            {
                return this.BadRequest("{\"Error\":[\"Invalid model\"]}");
            }

            JsonConvert.PopulateObject(values, model);
            ValidationResult result = this.validator.Validate(model, _ => _.IncludeRuleSets("Edit"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return this.BadRequest(this.ModelState);
            }

            this.employeeDependantService.Update(model);

            return this.Ok(model);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var model = this.employeeDependantService.GetById(key);

            if (model == null)
            {
                return this.BadRequest("{\"Error\":[\"ID Not found\"]}");
            }

            ValidationResult result = this.validator.Validate(model, _ => _.IncludeRuleSets("Delete"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return this.BadRequest(result.Errors);
            }

            this.employeeDependantService.Delete(model.EmployeeDependantID);

            return this.Ok(model);
        }

        public IActionResult GetDependantTypes(DataSourceLoadOptions loadOptions)
        {
            var dependantTypeList = this.enumDependantTypeService.GetAll();

            return this.Json(DataSourceLoader.Load(dependantTypeList, loadOptions));
        }
    }
}