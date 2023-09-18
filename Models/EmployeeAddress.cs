namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmployeeAddress
    {
        [Key]
        [Column("PKAddressId")]
        public int AddressId { get; set; }

        [Column("FKEmployeeId")]
        public int EmployeeId { get; set; }

        public string UnitNumber { get; set; }

        public string ComplexName { get; set; }

        public string StreetNumber { get; set; }

        public string StreetName { get; set; }

        public string Suburb { get; set; }

        public string City { get; set; }

        public string Code { get; set; }

        [Column("FKCountry")]
        public int CountryId { get; set; }
    }
}
