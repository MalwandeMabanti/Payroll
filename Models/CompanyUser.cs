namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CompanyUser
    {
        [Key]
        public int CompanyUserId { get; set; }

        [Required(ErrorMessage = "First name cannot be empty")]
        public string CompanyUserFirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty")]
        public string CompanyUserLastName { get; set; }

        [Column("CompanyUserEmail")]
        [Required(ErrorMessage = "Email cannot be empty")]
        public string Email { get; set; }
    }
}
