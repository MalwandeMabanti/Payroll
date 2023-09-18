namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmployeeDependant
    {
        [Key]
        [Column("pkEmployeeDependantID")]
        public int EmployeeDependantID { get; set; }

        [Column("fkEmployeeId")]
        public int EmployeeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long ContactNumber { get; set; }

        public bool MedicalAidDependant { get; set; }

        public DateTime BirthDate { get; set; }

        [Column("fkDependantTypeID")]
        public int DependantTypeID { get; set; }
    }
}