namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EnumDependantTypeService : GenericEnumService<EnumDependantType>, IEnumDependantTypeService
    {
        public EnumDependantTypeService(ApplicationDbContext context, ICacheService redisCacheService, ILogger<EnumDependantType> logger)
            : base(context, redisCacheService, logger)
        {
        }
    }
}