namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmployeeQualification
    {
        [Key]
        [Column("pkEmployeeQualificationID")]
        public int EmployeeQualificationID { get; set; }

        [Column("fkEmpID")]
        public int? EmpId { get; set; }

        [Column("fkQualification")]
        public int QualificationID { get; set; }

        [Column("fkEducationLevel")]
        public int EducationLevelID { get; set; }

        public string Institution { get; set; }

        [Column("fkInstitutionType")]
        public int InstitutionType { get; set; }

        public bool InProgress { get; set; }

        public DateTime? DateCompleted { get; set; }
    }
}
