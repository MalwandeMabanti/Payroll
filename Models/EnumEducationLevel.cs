namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EnumEducationLevel
    {
        [Key]
        [Column("pkEducationLevelID")]
        public int EducationLevelID { get; set; }

        public string Description { get; set; }

        public DateTime? DateAdded { get; set; }
    }
}
