namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class AuditTableService : GenericService<AuditTable>, IAuditTableService
    {
        public AuditTableService(AuditContext context)
            : base(context)
        {
        }
    }
}
