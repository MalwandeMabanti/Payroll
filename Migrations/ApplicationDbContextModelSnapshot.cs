﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payroll.Data;

#nullable disable

namespace Payroll.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Payroll.Models.CompanyUser", b =>
                {
                    b.Property<int>("CompanyUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyUserId"));

                    b.Property<string>("CompanyUserFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyUserLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CompanyUserEmail");

                    b.HasKey("CompanyUserId");

                    b.ToTable("CompanyUsers");
                });

            modelBuilder.Entity("Payroll.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkEmployeeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("EmployeeRetired")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaidenName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TitleId")
                        .HasColumnType("int")
                        .HasColumnName("fkTitle");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Payroll.Models.EmployeeAddress", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PKAddressId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComplexName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("FKCountry");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("FKEmployeeId");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Suburb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("EmployeeAddresses");
                });

            modelBuilder.Entity("Payroll.Models.EmployeeDependant", b =>
                {
                    b.Property<int>("EmployeeDependantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkEmployeeDependantID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeDependantID"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("ContactNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("DependantTypeID")
                        .HasColumnType("int")
                        .HasColumnName("fkDependantTypeID");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("fkEmployeeId");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MedicalAidDependant")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeDependantID");

                    b.ToTable("EmployeeDependants");
                });

            modelBuilder.Entity("Payroll.Models.EmployeeQualification", b =>
                {
                    b.Property<int>("EmployeeQualificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkEmployeeQualificationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeQualificationID"));

                    b.Property<DateTime?>("DateCompleted")
                        .HasColumnType("datetime2");

                    b.Property<int>("EducationLevelID")
                        .HasColumnType("int")
                        .HasColumnName("fkEducationLevel");

                    b.Property<int?>("EmpId")
                        .HasColumnType("int")
                        .HasColumnName("fkEmpID");

                    b.Property<bool>("InProgress")
                        .HasColumnType("bit");

                    b.Property<string>("Institution")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InstitutionType")
                        .HasColumnType("int")
                        .HasColumnName("fkInstitutionType");

                    b.Property<int>("QualificationID")
                        .HasColumnType("int")
                        .HasColumnName("fkQualification");

                    b.HasKey("EmployeeQualificationID");

                    b.ToTable("EmployeeQualifications");
                });

            modelBuilder.Entity("Payroll.Models.EnumCountry", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PKCountryId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("EnumCountries");
                });

            modelBuilder.Entity("Payroll.Models.EnumDependantType", b =>
                {
                    b.Property<int>("DependantTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkDependantTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DependantTypeID"));

                    b.Property<string>("DependantType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DependantTypeID");

                    b.ToTable("EnumDependantTypes");
                });

            modelBuilder.Entity("Payroll.Models.EnumEducationLevel", b =>
                {
                    b.Property<int>("EducationLevelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkEducationLevelID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EducationLevelID"));

                    b.Property<DateTime?>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EducationLevelID");

                    b.ToTable("EnumEducationLevels");
                });

            modelBuilder.Entity("Payroll.Models.EnumInstitutionTypes", b =>
                {
                    b.Property<int>("InstitutionTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkInstitutionTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InstitutionTypeID"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InstitutionTypeID");

                    b.ToTable("EnumInstitutionTypes");
                });

            modelBuilder.Entity("Payroll.Models.EnumQualification", b =>
                {
                    b.Property<int>("QualificationsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkQualificationID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QualificationsID"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QualificationsID");

                    b.ToTable("EnumQualifications");
                });

            modelBuilder.Entity("Payroll.Models.EnumTitle", b =>
                {
                    b.Property<int>("TitleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pkTitleId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TitleId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TitleId");

                    b.ToTable("EnumTitles");
                });
#pragma warning restore 612, 618
        }
    }
}
