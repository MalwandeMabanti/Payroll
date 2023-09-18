namespace Payroll.WebApiControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;
    using Payroll.Core;
    using Payroll.Interfaces;
    using Payroll.Models;

    [Authorize(Roles = Constant.Roles.Company)]
    [Route("api/[controller]")]
    public class EmployeeWebAPIController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IValidator<Employee> employeeValidator;

        public EmployeeWebAPIController(IEmployeeService employeeService, IValidator<Employee> employeeValidator)
        {
            this.employeeService = employeeService;
            this.employeeValidator = employeeValidator;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.employeeService.GetAll(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var newEmployee = new Employee();
            JsonConvert.PopulateObject(values, newEmployee);

            newEmployee.EmployeeNumber = this.employeeService.GenerateEmployeeNumber();
            var result = this.employeeValidator.Validate(newEmployee, _ => _.IncludeRuleSets("Create"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.employeeService.Add(newEmployee);

            return this.Ok(newEmployee);
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var employee = this.employeeService.GetById(key);
            JsonConvert.PopulateObject(values, employee);

            var result = this.employeeValidator.Validate(employee, _ => _.IncludeRuleSets("Edit"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.employeeService.Update(employee);

            return this.Ok(employee);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var employee = new Employee() { EmployeeId = key };
            var result = this.employeeValidator.Validate(employee, _ => _.IncludeRuleSets("Delete"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState);
            }

            this.employeeService.Delete(key);

            return this.Ok();
        }
    }
}
