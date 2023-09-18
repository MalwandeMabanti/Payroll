namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EnumCountryService : GenericEnumService<EnumCountry>, IEnumCountryService
    {
        public EnumCountryService(ApplicationDbContext context, ICacheService redisCacheService, ILogger<EnumCountry> logger)
            : base(context, redisCacheService, logger)
        {
        }
    }
}
