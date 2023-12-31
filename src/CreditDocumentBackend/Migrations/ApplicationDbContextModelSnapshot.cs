﻿// <auto-generated />
using System;
using CreditDocumentBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CreditDocumentBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SharedModels.CreditDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("LoanNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganizationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CreditDocuments");
                });

            modelBuilder.Entity("SharedModels.DocumentAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreditDocumentId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreditDocumentId");

                    b.ToTable("DocumentAttachments");
                });

            modelBuilder.Entity("SharedModels.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreditDocumentId")
                        .HasColumnType("int");

                    b.Property<int?>("CreditDocumentId1")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreditDocumentId");

                    b.HasIndex("CreditDocumentId1");

                    b.ToTable("People");
                });

            modelBuilder.Entity("SharedModels.DocumentAttachment", b =>
                {
                    b.HasOne("SharedModels.CreditDocument", null)
                        .WithMany("Attachments")
                        .HasForeignKey("CreditDocumentId");
                });

            modelBuilder.Entity("SharedModels.Person", b =>
                {
                    b.HasOne("SharedModels.CreditDocument", null)
                        .WithMany("Guarantors")
                        .HasForeignKey("CreditDocumentId");

                    b.HasOne("SharedModels.CreditDocument", null)
                        .WithMany("Signatories")
                        .HasForeignKey("CreditDocumentId1");
                });

            modelBuilder.Entity("SharedModels.CreditDocument", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Guarantors");

                    b.Navigation("Signatories");
                });
#pragma warning restore 612, 618
        }
    }
}
