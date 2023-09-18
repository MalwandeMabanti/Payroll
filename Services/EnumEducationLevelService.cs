namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EnumEducationLevelService : GenericEnumService<EnumEducationLevel>, IEnumEducationLevelService
    {
        public EnumEducationLevelService(ApplicationDbContext context, ICacheService redisCacheService, ILogger<EnumEducationLevel> logger)
            : base(context, redisCacheService, logger)
        {
        }
    }
}
