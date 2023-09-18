namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EnumInstitutionTypes
    {
        [Key]
        [Column("pkInstitutionTypeID")]
        public int InstitutionTypeID { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
