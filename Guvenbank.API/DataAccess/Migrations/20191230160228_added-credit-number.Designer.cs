﻿// <auto-generated />
using System;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataAccess.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20191230160228_added-credit-number")]
    partial class addedcreditnumber
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Entities.BankAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<int>("CustomerNo");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsActive");

                    b.Property<int>("No");

                    b.HasKey("Id");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreditNumber");

                    b.Property<string>("IdNo");

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<int>("No");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<DateTime>("Date");

                    b.Property<int>("From");

                    b.Property<int>("FromCustomerNo");

                    b.Property<string>("ReceiverFullName");

                    b.Property<string>("Summary");

                    b.Property<int>("To");

                    b.Property<int>("ToCustomerNo");

                    b.Property<byte>("Type");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
