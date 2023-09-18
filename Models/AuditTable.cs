namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AuditTable
    {
        [Key]
        public int AuditTableId { get; set; }

        public string AuditTableName { get; set; }
    }
}
