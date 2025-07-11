﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentAPI.Model;

#nullable disable

namespace PaymentAPI.Migrations
{
    [DbContext(typeof(PaymentDetailContext))]
    [Migration("20250512114141_AddUserForeignKey")]
    partial class AddUserForeignKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PaymentAPI.Model.College", b =>
                {
                    b.Property<int>("age")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("age"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("age");

                    b.ToTable("Colleges");
                });

            modelBuilder.Entity("PaymentAPI.Model.Employee", b =>
                {
                    b.Property<int>("employee_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("employee_id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("employee_id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PaymentAPI.Model.PaymentDetails", b =>
                {
                    b.Property<int>("PayemntDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PayemntDetailId"));

                    b.Property<string>("CarOwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CardExpiryDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(3)");

                    b.Property<int>("employee_id")
                        .HasColumnType("int");

                    b.Property<int?>("employee_id1")
                        .HasColumnType("int");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PayemntDetailId");

                    b.HasIndex("employee_id1");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("PaymentAPI.Model.PaymentDetails", b =>
                {
                    b.HasOne("PaymentAPI.Model.Employee", null)
                        .WithMany("PaymentDetails")
                        .HasForeignKey("employee_id1");
                });

            modelBuilder.Entity("PaymentAPI.Model.Employee", b =>
                {
                    b.Navigation("PaymentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
