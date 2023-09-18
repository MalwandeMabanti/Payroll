namespace Payroll.Middlewares
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    using Payroll.Data;
    using Payroll.Models;

    public class AuditMiddleware : ISaveChangesInterceptor
    {
        private readonly AuditContext auditContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AuthDbContext authContext;

        public AuditMiddleware(AuditContext auditContext, IHttpContextAccessor httpContextAccessor, AuthDbContext authContext)
        {
            this.auditContext = auditContext;
            this.httpContextAccessor = httpContextAccessor;
            this.authContext = authContext;
        }

        public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var audits = this.CreateAudit(eventData.Context!);

            foreach (var myAudit in audits)
            {
                this.auditContext.Add(myAudit);

                await this.auditContext.SaveChangesAsync(cancellationToken);
            }

            return result;
        }

        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var audits = this.CreateAudit(eventData.Context!);

            foreach (var myAudit in audits)
            {
                this.auditContext.Add(myAudit);

                this.auditContext.SaveChanges();
            }

            return result;
        }

        private IEnumerable<Audit> CreateAudit(DbContext context)
        {
            context.ChangeTracker.DetectChanges();

            var userEmail = this.httpContextAccessor.HttpContext!.User.Identity!.Name;
            var user = this.authContext.Users.FirstOrDefault(_ => _.Email == userEmail);

            var audits = new List<Audit>();

            Dictionary<EntityState, Func<EntityEntry, DbContext, string, int, List<Audit>>> actions = new ()
            {
                { EntityState.Added, CreateAddedMessage },
                { EntityState.Modified, CreateModifiedMessage },
                { EntityState.Deleted, CreateDeletedMessage },
            };

            foreach (var entry in context.ChangeTracker.Entries())
            {
                var auditTable = this.auditContext.AuditTable.FirstOrDefault(_ => _.AuditTableName == entry.Entity.GetType().Name);

                if (auditTable == null)
                {
                    continue;
                }

                var action = actions.GetValueOrDefault(entry.State);

                if (action == null)
                {
                    continue;
                }

                var audit = action(entry, context, user!.Id, auditTable.AuditTableId);

                audits.AddRange(audit);
            }

            return audits;
        }

        private static Audit CreateBaseAudit(string userId, EntityState state, int auditTableId)
        {
            return new Audit
            {
                DateOccured = DateTime.UtcNow,
                UserId = userId,
                State = state.ToString(),
                AuditTableId = auditTableId,
            };
        }

        private static List<Audit> CreateAddedMessage(EntityEntry entry, DbContext context, string userId, int auditTableId)
        {
            var audit = CreateBaseAudit(userId, entry.State, auditTableId);

            foreach (var property in entry.Properties)
            {
                if (Equals(property.CurrentValue, null) || property.Metadata.IsPrimaryKey())
                {
                    continue;
                }

                var propertyName = property.Metadata.Name;

                if (propertyName.ToLower().IndexOf("id") > -1 && propertyName.ToLower().IndexOf("id") == propertyName.Length - 2)
                {
                    propertyName = propertyName.Substring(0, propertyName.ToLower().IndexOf("id"));
                }

                var newEnumValue = GetEnumValue(property.Metadata.Name, property.CurrentValue!, context);

                audit.Details += $"{propertyName}: {newEnumValue ?? property.CurrentValue} ";
            }

            return new () { audit };
        }

        private static List<Audit> CreateModifiedMessage(EntityEntry entry, DbContext context, string userId, int auditTableId)
        {
            var audits = new List<Audit>();

            foreach (var property in entry.Properties)
            {
                var audit = CreateBaseAudit(userId, entry.State, auditTableId);

                if (Equals(property.OriginalValue, property.CurrentValue))
                {
                    continue;
                }

                var originalEnumValue = GetEnumValue(property.Metadata.Name, property.OriginalValue !, context);
                var newEnumValue = GetEnumValue(property.Metadata.Name, property.CurrentValue !, context);

                var propertyName = property.Metadata.Name;

                if (propertyName.ToLower().IndexOf("id") > -1 && propertyName.ToLower().IndexOf("id") == propertyName.Length - 2)
                {
                    propertyName = propertyName.Substring(0, propertyName.ToLower().IndexOf("id"));
                }

                audit.Details = $"{property.EntityEntry.Entity.GetType().Name} - {propertyName}";
                audit.OldValue = originalEnumValue ?? ConvertToString(property.OriginalValue);
                audit.NewValue = newEnumValue ?? ConvertToString(property.CurrentValue);

                audits.Add(audit);
            }

            return audits;
        }

        private static List<Audit> CreateDeletedMessage(EntityEntry entry, DbContext context, string userId, int auditTableId)
        {
            var audit = CreateBaseAudit(userId, entry.State, auditTableId);
            audit.Details = "Delete - " + audit.DateOccured.ToShortDateString();

            foreach (var property in entry.Properties)
            {
                if (Equals(property.CurrentValue, null) || property.Metadata.IsPrimaryKey())
                {
                    continue;
                }

                var propertyName = property.Metadata.Name;

                if (propertyName.ToLower().IndexOf("id") > -1 && propertyName.ToLower().IndexOf("id") == propertyName.Length - 2)
                {
                    propertyName = propertyName.Substring(0, propertyName.ToLower().IndexOf("id"));
                }

                var newEnumValue = GetEnumValue(property.Metadata.Name, property.CurrentValue!, context);
                audit.OldValue += $"{propertyName}: {newEnumValue ?? property.CurrentValue} ";
            }

            return new () { audit };
        }

        private static string ConvertToString(object? value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        private static string GetEnumValue(string propertyName, object id, DbContext context)
        {
            var enumList = new List<string>()
            {
                "TitleId",
                "CountryId",
                "DependantTypeID",
                "QualificationID",
                "InstitutionType",
                "EducationLevelID",
                "EmpId",
            };

            if (!enumList.Contains(propertyName))
            {
                return null;
            }

            var enumId = int.Parse(id.ToString() !);

            return propertyName switch
            {
                "TitleId" => context.Set<EnumTitle>().First(_ => _.TitleId == enumId).Description,
                "CountryId" => context.Set<EnumCountry>().First(_ => _.CountryId == enumId).Country,
                "DependantTypeID" => context.Set<EnumDependantType>().First(_ => _.DependantTypeID == enumId).DependantType,
                "QualificationID" => context.Set<EnumQualification>().First(_ => _.QualificationsID == enumId).Description,
                "InstitutionType" => context.Set<EnumInstitutionTypes>().First(_ => _.InstitutionTypeID == enumId).Description,
                "EducationLevelID" => context.Set<EnumEducationLevel>().First(_ => _.EducationLevelID == enumId).Description,
                "EmpId" => $"{context.Set<Employee>().First(_ => _.EmployeeId == enumId).FirstName} {context.Set<Employee>().First(_ => _.EmployeeId == enumId).LastName}",
                _ => null,
            };
        }
    }
}
