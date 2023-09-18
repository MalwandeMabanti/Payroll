namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Newtonsoft.Json;

    public class Employee
    {
        [Key]
        [Column("pkEmployeeID")]
        public int EmployeeId { get; set; }

        public string? EmployeeNumber { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name cannot be empty")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Inititals cannot be empty")]
        public string Initials { get; set; }

        [Required(ErrorMessage = "Preferred name cannot be empty")]
        public string PreferredName { get; set; }

        public string? MaidenName { get; set; }

        [Column("fkTitle")]
        [Display(Name = "Title")]
        public int TitleId { get; set; }

        public bool? EmployeeRetired { get; set; }
    }
}
