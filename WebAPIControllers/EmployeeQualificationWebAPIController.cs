namespace Payroll.WebAPIControllers
{
    using System.Security.Claims;

    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using FluentValidation;
    using FluentValidation.AspNetCore;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Newtonsoft.Json;

    using Payroll.Core;
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    [Route("api/[controller]")]
    public class EmployeeQualificationWebAPIController : Controller
    {
        private readonly IEmployeeQualificationService employeeQualificationService;
        private readonly IEnumQualificationService enumQualificationService;
        private readonly IEnumEducationLevelService enumEducationLevelService;
        private readonly IEnumInstitutionTypeService enumInstitutionTypeService;
        private readonly IValidator<EmployeeQualification> validator;
        private readonly IEmployeeService employeeService;

        public EmployeeQualificationWebAPIController(
            IEmployeeQualificationService employeeQualificationService,
            IEnumQualificationService enumQualificationService,
            IEnumEducationLevelService enumEducationLevelService,
            IEnumInstitutionTypeService enumInstitutionTypeService,
            IValidator<EmployeeQualification> validator,
            IEmployeeService employeeService)
        {
            this.employeeQualificationService = employeeQualificationService;
            this.enumQualificationService = enumQualificationService;
            this.enumInstitutionTypeService = enumInstitutionTypeService;
            this.enumEducationLevelService = enumEducationLevelService;
            this.validator = validator;
            this.employeeService = employeeService;
        }

        [HttpGet("/LoadEmployeeQualifications")]
        public IActionResult LoadEmployeeQualifications(DataSourceLoadOptions loadOptions)
        {
            var employeeQualifications = new List<EmployeeQualification>();

            if (this.User.IsInRole(Constant.Roles.Company))
            {
                return this.Json(DataSourceLoader.Load(this.employeeQualificationService.GetAll(), loadOptions));
            }
            else if (this.User.IsInRole(Constant.Roles.Employee))
            {
                var userEmail = this.User.FindFirstValue(ClaimTypes.Email);
                var employee = this.employeeService.FindByField("Email", userEmail);

                if (employee == null)
                {
                    return this.BadRequest("{\"Error\":[\"Invalid Employee\"]}");
                }

                employeeQualifications = this.employeeQualificationService.FindAllByField("EmpId", employee.EmployeeId);
                return this.Json(DataSourceLoader.Load(employeeQualifications, loadOptions));
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet("/QualificationsLookup")]
        public object QualificationsLookup(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.enumQualificationService.GetAll(), loadOptions);
        }

        [HttpGet("/EducationLevelLookup")]
        public object EducationLevelLookup(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.enumEducationLevelService.GetAll(), loadOptions);
        }

        [HttpGet("/InstitutionTypesLookup")]
        public object InstitutionTypesLookup(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.enumInstitutionTypeService.GetAll(), loadOptions);
        }

        [HttpPost]
        public IActionResult Create(string values)
        {
            var model = new EmployeeQualification();

            if (this.User.IsInRole(Constant.Roles.Employee))
            {
                var userEmail = this.User.FindFirstValue(ClaimTypes.Email);
                var employee = this.employeeService.FindByField("Email", userEmail);

                if (employee == null)
                {
                    return this.BadRequest("{\"Error\":[\"Invalid Employee\"]}");
                }

                model.EmpId = employee.EmployeeId;
            }

            JsonConvert.PopulateObject(values, model);

            var result = this.validator.Validate(model, _ => _.IncludeRuleSets("Create"));
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return this.BadRequest(this.ModelState.ToFullErrorString());
            }

            this.employeeQualificationService.Add(model);

            return this.Ok();
        }

        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var model = this.employeeQualificationService.GetAll().First(_ => _.EmployeeQualificationID == key);
            JsonConvert.PopulateObject(values, model);

            var result = this.validator.Validate(model, _ => _.IncludeRuleSets("Edit"));
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return this.BadRequest(this.ModelState.ToFullErrorString());
            }

            this.employeeQualificationService.Update(model);

            return this.Ok();
        }

        [HttpDelete]
        public void Delete(int key)
        {
            var model = this.employeeQualificationService.GetAll().First(_ => _.EmployeeQualificationID == key);
            this.employeeQualificationService.Delete(key);
        }
    }
}
