namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EnumTitle
    {
        [Key]
        [Column("pkTitleId")]
        public int TitleId { get; set; }

        public string Description { get; set; }
    }
}
