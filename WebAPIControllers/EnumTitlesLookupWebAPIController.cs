namespace Payroll.WebApiControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using Microsoft.AspNetCore.Mvc;

    using Payroll.Interfaces;

    [Route("api/[Controller]")]
    public class EnumTitlesLookupWebAPIController : Controller
    {
        private readonly IEnumTitleService enumTitleService;

        public EnumTitlesLookupWebAPIController(IEnumTitleService enumTitleService)
        {
            this.enumTitleService = enumTitleService;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(this.enumTitleService.GetAll(), loadOptions);
        }
    }
}
