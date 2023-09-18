namespace Payroll.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EnumCountry
    {
        [Key]
        [Column("PKCountryId")]
        public int CountryId { get; set; }

        public string Country { get; set; }
    }
}
