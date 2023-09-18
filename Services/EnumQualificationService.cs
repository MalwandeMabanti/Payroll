namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EnumQualificationService : GenericEnumService<EnumQualification>, IEnumQualificationService
    {
        public EnumQualificationService(ApplicationDbContext context, ICacheService redisCacheService, ILogger<EnumQualification> logger)
            : base(context, redisCacheService, logger)
        {
        }
    }
}
