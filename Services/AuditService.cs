namespace Payroll.Services
{
    using Payroll.Data;
    using Payroll.Interfaces;
    using Payroll.Models;

    public class AuditService : GenericService<Audit>, IAuditService
    {
        private readonly AuthDbContext authContext;

        public AuditService(AuditContext context, AuthDbContext authContext)
            : base(context)
        {
            this.authContext = authContext;
        }

        public new List<Audit> GetAll()
        {
            var audits = base.GetAll();

            foreach (var audit in audits)
            {
                var user = this.authContext.Users.FirstOrDefault(_ => _.Id == audit.UserId);

                if (user != null)
                {
                    audit.UserName = user.FirstName + " " + user.LastName;
                }
            }

            return audits;
        }

        public new List<Audit> FindAllByField(string fieldName, object value)
        {
            var audits = base.FindAllByField(fieldName, value);

            foreach (var audit in audits)
            {
                var user = this.authContext.Users.FirstOrDefault(_ => _.Id == audit.UserId);

                if (user != null)
                {
                    audit.UserName = user.FirstName + " " + user.LastName;
                }
            }

            return audits;
        }
    }
}
