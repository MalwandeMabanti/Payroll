namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EnumQualification
    {
        [Key]
        [Column("pkQualificationID")]
        public int QualificationsID { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
