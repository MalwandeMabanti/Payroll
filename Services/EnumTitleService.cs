namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class EnumTitleService : GenericEnumService<EnumTitle>, IEnumTitleService
    {
        public EnumTitleService(ApplicationDbContext context, ICacheService redisCacheService, ILogger<EnumTitle> logger)
            : base(context, redisCacheService, logger)
        {
        }
    }
}
