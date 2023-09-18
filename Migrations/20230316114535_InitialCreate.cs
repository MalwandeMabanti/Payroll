#nullable disable

namespace Payroll.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeAddresses",
                columns: table => new
                {
                    PKAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKEmployeeId = table.Column<int>(type: "int", nullable: false),
                    UnitNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplexName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FKCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAddresses", x => x.PKAddressId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeQualifications",
                columns: table => new
                {
                    pkEmployeeQualificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fkEmpID = table.Column<int>(type: "int", nullable: false),
                    fkQualification = table.Column<int>(type: "int", nullable: false),
                    fkEducationLevel = table.Column<int>(type: "int", nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fkInstitutionType = table.Column<int>(type: "int", nullable: false),
                    InProgress = table.Column<bool>(type: "bit", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeQualifications", x => x.pkEmployeeQualificationID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    pkEmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaidenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fkTitle = table.Column<int>(type: "int", nullable: false),
                    EmployeeRetired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.pkEmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "EnumCountries",
                columns: table => new
                {
                    PKCountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumCountries", x => x.PKCountryId);
                });

            migrationBuilder.CreateTable(
                name: "EnumDependantTypes",
                columns: table => new
                {
                    pkDependantTypeID = table.Column<int>(type: "int", nullable: false),
                    DependantType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumDependantTypes", x => x.pkDependantTypeID);
                });

            migrationBuilder.CreateTable(
                name: "EnumEducationLevels",
                columns: table => new
                {
                    pkEducationLevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumEducationLevels", x => x.pkEducationLevelID);
                });

            migrationBuilder.CreateTable(
                name: "EnumInstitutionTypes",
                columns: table => new
                {
                    pkInstitutionTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumInstitutionTypes", x => x.pkInstitutionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "EnumQualifications",
                columns: table => new
                {
                    pkQualificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumQualifications", x => x.pkQualificationID);
                });

            migrationBuilder.CreateTable(
                name: "EnumTitles",
                columns: table => new
                {
                    pkTitleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnumTitles", x => x.pkTitleId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDependants",
                columns: table => new
                {
                    pkEmployeeDependantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fkEmployeeId = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<long>(type: "bigint", nullable: false),
                    MedicalAidDependant = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fkDependantTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDependants", x => x.pkEmployeeDependantID);
                    table.ForeignKey(
                        name: "FK_EmployeeDependants_EnumDependantTypes_fkDependantTypeID",
                        column: x => x.fkDependantTypeID,
                        principalTable: "EnumDependantTypes",
                        principalColumn: "pkDependantTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDependants_fkDependantTypeID",
                table: "EmployeeDependants",
                column: "fkDependantTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAddresses");

            migrationBuilder.DropTable(
                name: "EmployeeDependants");

            migrationBuilder.DropTable(
                name: "EmployeeQualifications");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EnumCountries");

            migrationBuilder.DropTable(
                name: "EnumEducationLevels");

            migrationBuilder.DropTable(
                name: "EnumInstitutionTypes");

            migrationBuilder.DropTable(
                name: "EnumQualifications");

            migrationBuilder.DropTable(
                name: "EnumTitles");

            migrationBuilder.DropTable(
                name: "EnumDependantTypes");
        }
    }
}
