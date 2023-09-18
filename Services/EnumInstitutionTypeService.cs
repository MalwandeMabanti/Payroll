namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EnumInstitutionTypeService : GenericEnumService<EnumInstitutionTypes>, IEnumInstitutionTypeService
    {
        public EnumInstitutionTypeService(ApplicationDbContext context, ICacheService redisCacheService, ILogger<EnumInstitutionTypes> logger)
            : base(context, redisCacheService, logger)
        {
        }
    }
}
