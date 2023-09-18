namespace Payroll.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Payroll.Middlewares;
    using Payroll.Models;

    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            InitializeRoles(serviceProvider);
            InitializeCache(serviceProvider);
        }

        public static void InitializeCache(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            using var auditContext = new AuditContext(
                serviceProvider.GetRequiredService<DbContextOptions<AuditContext>>());

            context.Database.EnsureCreated();

            if (!context.EnumTitles.Any())
            {
                context.EnumTitles.AddRange(
                new EnumTitle() { Description = "Mr" },
                new EnumTitle() { Description = "Mrs" },
                new EnumTitle() { Description = "Master" },
                new EnumTitle() { Description = "Miss" });
            }

            if (!context.EnumQualifications.Any())
            {
                context.EnumQualifications.AddRange(
                new EnumQualification() { DateAdded = DateTime.Now, Description = "Comp Sci" });
            }

            if (!context.EnumInstitutionTypes.Any())
            {
                context.EnumInstitutionTypes.AddRange(
                new EnumInstitutionTypes() { DateAdded = DateTime.Now, Description = "UCT" });
            }

            if (!context.EnumEducationLevels.Any())
            {
                context.EnumEducationLevels.AddRange(
                new EnumEducationLevel() { DateAdded = DateTime.Now, Description = "Tertiary" });
            }

            if (!context.EnumCountries.Any())
            {
                context.EnumCountries.AddRange(
                new EnumCountry() { Country = "South Africa" });
            }

            if (!context.EnumDependantTypes.Any())
            {
                context.Database.OpenConnection();
                context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.EnumDependantTypes ON");

                context.EnumDependantTypes.AddRange(
                new EnumDependantType() { DependantTypeID = 1, DependantType = "Adult" },
                new EnumDependantType() { DependantTypeID = 2, DependantType = "Spouse" },
                new EnumDependantType() { DependantTypeID = 3, DependantType = "Child" });

                context.SaveChanges();
                context.Database.ExecuteSql($"SET IDENTITY_INSERT dbo.EnumDependantTypes OFF");
                context.Database.CloseConnection();
            }

            if (!context.Employees.Any())
            {
                context.Employees.AddRange(
                new Employee()
                {
                    Email = "John@gmail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    EmployeeNumber = "EMP001",
                    Initials = "N",
                    MaidenName = "N/A",
                    MiddleName = "N/A",
                    EmployeeRetired = false,
                    TitleId = context.EnumTitles.OrderBy(_ => _.TitleId).Last().TitleId,
                    PreferredName = "JD",
                });
            }

            if (!context.EmployeeAddresses.Any())
            {
                context.EmployeeAddresses.AddRange(
                new EmployeeAddress()
                {
                    City = "JHB",
                    Code = "sdsd",
                    ComplexName = "Some Name",
                    StreetName = "Some street",
                    EmployeeId = 1,
                    CountryId = context.EnumCountries.OrderBy(_ => _.CountryId).Last().CountryId,
                    Suburb = "j",
                    StreetNumber = "sd",
                    UnitNumber = "d",
                });
            }

            if (!context.EmployeeDependants.Any())
            {
                context.EmployeeDependants.AddRange(
                new EmployeeDependant()
                {
                    BirthDate = DateTime.Now,
                    ContactNumber = 1234567890,
                    DependantTypeID = context.EnumDependantTypes.OrderBy(_ => _.DependantTypeID).Last().DependantTypeID,
                    EffectiveDate = DateTime.Now,
                    FirstName = "blaj",
                    LastName = "Yo",
                    EmployeeId = 1,
                    MedicalAidDependant = true,
                });
            }

            if (!context.EmployeeQualifications.Any())
            {
                context.EmployeeQualifications.AddRange(
                new EmployeeQualification()
                {
                    DateCompleted = DateTime.Now,
                    EducationLevelID = context.EnumEducationLevels.OrderBy(_ => _.EducationLevelID).Last().EducationLevelID,
                    EmpId = 1,
                    QualificationID = context.EnumQualifications.OrderBy(_ => _.QualificationsID).Last().QualificationsID,
                    InstitutionType = context.EnumInstitutionTypes.OrderBy(_ => _.InstitutionTypeID).Last().InstitutionTypeID,
                    InProgress = false,
                    Institution = "Inst",
                });
            }

            context.SaveChanges();

            if (!auditContext.AuditTable.Any())
            {
                auditContext.AddRange(
                    new AuditTable() { AuditTableName = "Employee" },
                    new AuditTable() { AuditTableName = "EmployeeAddress" },
                    new AuditTable() { AuditTableName = "EmployeeDependant" },
                    new AuditTable() { AuditTableName = "EmployeeQualification" });
            }

            auditContext.SaveChanges();
        }

        public static void InitializeRoles(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<AuthDbContext>();

            if (context != null)
            {
                string[] roles = new string[] { "Employee", "Company" };

                foreach (string role in roles)
                {
                    if (!context.Roles.Any(_ => _.Name == role))
                    {
                        context.Roles.Add(new IdentityRole(role)
                        {
                            NormalizedName = role.ToUpperInvariant(),
                        });

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}