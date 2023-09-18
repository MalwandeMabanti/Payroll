namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum DependantType
    {
        Adult = 1,
        Spouse = 2,
        Child = 3,
    }

    public class EnumDependantType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("pkDependantTypeID")]
        public int DependantTypeID { get; set; }

        public string DependantType { get; set; }
    }
}