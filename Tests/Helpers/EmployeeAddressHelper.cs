namespace Payroll.Tests.Helpers
{
    using Payroll.Models;

    public static class EmployeeAddressHelper
    {
        public static EmployeeAddress EmployeeAddressCreate()
        {
            return new EmployeeAddress
            {
                AddressId = 1,
                EmployeeId = 1,
                UnitNumber = "25",
                ComplexName = "Complicated",
                StreetNumber = "666",
                StreetName = "Masingafi",
                Suburb = "Mlamlankunzi",
                City = "Soweto",
                Code = "1804",
                CountryId = 1,
            };
        }
    }
}
