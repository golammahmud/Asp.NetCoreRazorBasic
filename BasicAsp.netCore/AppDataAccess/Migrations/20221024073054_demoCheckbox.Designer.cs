﻿// <auto-generated />
using System;
using AppDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppDataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221024073054_demoCheckbox")]
    partial class demoCheckbox
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppDomain.DataModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("AppDomain.DataModels.FileModel", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<byte[]>("File")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("FileId");

                    b.HasIndex("ProductId");

                    b.ToTable("FileModel");
                });

            modelBuilder.Entity("AppDomain.DataModels.MultipleCheckbox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MultipleCheckbox");
                });

            modelBuilder.Entity("AppDomain.DataModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("File")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AppDomain.DataModels.SubCheckBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("IsChecked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MultiCheckboxId")
                        .HasColumnType("int");

                    b.Property<int?>("MultipleCheckboxId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MultipleCheckboxId");

                    b.ToTable("CheckBoxSubMOdel");
                });

            modelBuilder.Entity("AppDomain.DataModels.FileModel", b =>
                {
                    b.HasOne("AppDomain.DataModels.Product", null)
                        .WithMany("Files")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("AppDomain.DataModels.Product", b =>
                {
                    b.HasOne("AppDomain.DataModels.Category", "CategoriList")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("CategoriList");
                });

            modelBuilder.Entity("AppDomain.DataModels.SubCheckBox", b =>
                {
                    b.HasOne("AppDomain.DataModels.MultipleCheckbox", null)
                        .WithMany("CheckBoxes")
                        .HasForeignKey("MultipleCheckboxId");
                });

            modelBuilder.Entity("AppDomain.DataModels.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("AppDomain.DataModels.MultipleCheckbox", b =>
                {
                    b.Navigation("CheckBoxes");
                });

            modelBuilder.Entity("AppDomain.DataModels.Product", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
