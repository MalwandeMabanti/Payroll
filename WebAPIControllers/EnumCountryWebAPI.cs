namespace Payroll.WebAPIControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Data.ResponseModel;
    using DevExtreme.AspNet.Mvc;

    using Microsoft.AspNetCore.Mvc;

    using Payroll.Interfaces;

    public class EnumCountryWebAPI : Controller
    {
        private readonly IEnumCountryService enumCountryService;

        public EnumCountryWebAPI(IEnumCountryService enumCountryService)
        {
            this.enumCountryService = enumCountryService;
        }

        [HttpGet]
        public LoadResult Get(DataSourceLoadOptions loadOptions)
        {
            var countries = this.enumCountryService.GetAll();

            return DataSourceLoader.Load(countries, loadOptions);
        }
    }
}
