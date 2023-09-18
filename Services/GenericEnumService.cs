namespace Payroll.Services
{
    using Microsoft.AspNetCore.Identity;

    using Payroll.Data;
    using Payroll.Interfaces;

    public class GenericEnumService<T> : GenericService<T>, IGenericEnumService<T>
        where T : class
    {
        private readonly ICacheService redisCacheService;
        private readonly ILogger<T> logger;

        public GenericEnumService(ApplicationDbContext context, ICacheService redisCacheService, ILogger<T> logger)
            : base(context)
        {
            this.logger = logger;
            this.redisCacheService = redisCacheService;
        }

        public new List<T> GetAll()
        {
            try
            {
                var data = this.redisCacheService.GetData<T>(typeof(T).Name);

                if (data == null)
                {
                    data = base.GetAll();
                    this.redisCacheService.SaveData(typeof(T).Name, data, TimeSpan.FromMinutes(30));
                }

                return data;
            }
            catch (StackExchange.Redis.RedisConnectionException)
            {
                this.logger.LogCritical("Redis-Server Down!");
                return base.GetAll();
            }
        }
    }
}
