﻿// <auto-generated />
using System;
using GWC.Web.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GWC.Web.DataAccess.Migrations
{
    [DbContext(typeof(GwcDbContext))]
    [Migration("20180913222830_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GWC.Web.Model.BillingCalendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BillMonth");

                    b.Property<int?>("BillYear");

                    b.Property<int?>("BillingDays");

                    b.Property<DateTime?>("BillingEmailCCDate");

                    b.Property<DateTime?>("BlockingDate");

                    b.Property<string>("Cycle")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("DueDate");

                    b.Property<DateTime?>("FirstCDRDate");

                    b.Property<DateTime?>("ImportBillingDate");

                    b.Property<DateTime?>("LastCDRDate");

                    b.Property<DateTime?>("LoadCDRDate");

                    b.Property<string>("MessageId")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("StartDate");

                    b.Property<DateTime?>("StatementDate");

                    b.Property<DateTime?>("StatementMailingDate");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("BillingCalendar");
                });

            modelBuilder.Entity("GWC.Web.Model.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasMaxLength(500);

                    b.Property<string>("Date")
                        .HasMaxLength(50);

                    b.Property<bool?>("Hide");

                    b.Property<string>("Page")
                        .HasMaxLength(50);

                    b.Property<bool?>("Quote");

                    b.Property<int>("SourceId");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("GWC.Web.Model.GwcLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .HasMaxLength(4000);

                    b.Property<string>("Exception")
                        .HasMaxLength(5000);

                    b.Property<string>("Level")
                        .HasMaxLength(50);

                    b.Property<string>("Line")
                        .HasMaxLength(20);

                    b.Property<DateTime>("LogDate");

                    b.Property<string>("Logger")
                        .HasMaxLength(255);

                    b.Property<string>("Message")
                        .HasMaxLength(1000);

                    b.Property<string>("Method")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("GwcLogs");
                });

            modelBuilder.Entity("GWC.Web.Model.Source", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Advertisement")
                        .HasMaxLength(1000);

                    b.Property<string>("Author")
                        .HasMaxLength(100);

                    b.Property<int?>("CD");

                    b.Property<string>("Date")
                        .HasMaxLength(50);

                    b.Property<string>("Editor")
                        .HasMaxLength(100);

                    b.Property<int>("ExternalId");

                    b.Property<int?>("Newspaper");

                    b.Property<int?>("PrintPages");

                    b.Property<string>("Publisher")
                        .HasMaxLength(100);

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.Property<string>("Volume")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Sources");
                });

            modelBuilder.Entity("GWC.Web.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GWC.Web.Model.Calendar", b =>
                {
                    b.HasOne("GWC.Web.Model.Source", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
