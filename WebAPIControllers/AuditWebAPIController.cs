namespace Payroll.WebAPIControllers
{
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Mvc;

    using Microsoft.AspNetCore.Mvc;

    using Payroll.Interfaces;

    public class AuditWebAPIController : Controller
    {
        private readonly IAuditService auditService;
        private readonly IAuditTableService auditTableService;

        public AuditWebAPIController(IAuditService auditService, IAuditTableService auditTableService)
        {
            this.auditService = auditService;
            this.auditTableService = auditTableService;
        }

        public object Get(DataSourceLoadOptions loadOptions, string tableName)
        {
            var table = this.auditTableService.FindByField("AuditTableName", tableName!);

            return DataSourceLoader.Load(this.auditService.FindAllByField("AuditTableId", table!.AuditTableId), loadOptions);
        }
    }
}
