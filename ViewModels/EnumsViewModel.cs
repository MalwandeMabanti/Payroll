namespace Payroll.ViewModels
{
    using Microsoft.EntityFrameworkCore;

    using Payroll.Models;

    [Keyless]
    public class EnumsViewModel
    {
        public List<EnumEducationLevel>? EducationLevel { get; set; }

        public List<EnumInstitutionTypes>? InstitutionType { get; set; }

        public List<EnumQualification>? Qualification { get; set; }
    }
}
