namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Audit
    {
        [Key]
        public int AuditId { get; set; }

        [Column("fkUserId")]
        public string UserId { get; set; }

        [Column("fkAuditTableId")]
        public int AuditTableId { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        public DateTime DateOccured { get; set; }

        public string State { get; set; }

        public string Details { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }
    }
}
