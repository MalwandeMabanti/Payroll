namespace Payroll.WebAPIControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using FluentValidation;
    using FluentValidation.AspNetCore;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Newtonsoft.Json;

    using Payroll.Core;
    using Payroll.Interfaces;
    using Payroll.Models;

    [Authorize(Roles = $"{Constant.Roles.Company}")]
    public class CompanyUserWebAPIController : Controller
    {
        private readonly ICompanyUserService companyUserService;
        private readonly IValidator<CompanyUser> validator;

        public CompanyUserWebAPIController(ICompanyUserService companyUserService, IValidator<CompanyUser> validator)
        {
            this.companyUserService = companyUserService;
            this.validator = validator;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.companyUserService.GetAll(), loadOptions);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var newUser = new CompanyUser();
            JsonConvert.PopulateObject(values, newUser);

            var result = this.validator.Validate(newUser, _ => _.IncludeRuleSets("Create"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return this.BadRequest(this.ModelState.ToFullErrorString());
            }

            this.companyUserService.Add(newUser);

            return this.Ok(newUser);
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var model = this.companyUserService.GetById(key);

            if (model == null)
            {
                return this.NotFound(model);
            }

            JsonConvert.PopulateObject(values, model);

            var result = this.validator.Validate(model, _ => _.IncludeRuleSets("Edit"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return this.BadRequest(this.ModelState.ToFullErrorString());
            }

            this.companyUserService.Update(model);

            return this.Ok(model);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var model = new CompanyUser() { CompanyUserId = key };
            var result = this.validator.Validate(model, _ => _.IncludeRuleSets("Delete"));

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return this.BadRequest(this.ModelState.ToFullErrorString());
            }

            this.companyUserService.Delete(key);

            return this.Ok();
        }
    }
}
