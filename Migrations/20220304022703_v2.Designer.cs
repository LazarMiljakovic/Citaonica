﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Citaonica.Migrations
{
    [DbContext(typeof(CitaonicaContext))]
    [Migration("20220304022703_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Fakultet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Fakultet");
                });

            modelBuilder.Entity("Models.Knjiga", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FakultetID")
                        .HasColumnType("int");

                    b.Property<int>("Godina")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PredmetID")
                        .HasColumnType("int");

                    b.Property<int?>("ProfesorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FakultetID");

                    b.HasIndex("PredmetID");

                    b.HasIndex("ProfesorID");

                    b.ToTable("Knjiga");
                });

            modelBuilder.Entity("Models.Predmet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FakultetID")
                        .HasColumnType("int");

                    b.Property<int>("Godina")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfesorID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FakultetID");

                    b.HasIndex("ProfesorID");

                    b.ToTable("Predmet");
                });

            modelBuilder.Entity("Models.Profesor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FakultetID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FakultetID");

                    b.ToTable("Profesor");
                });

            modelBuilder.Entity("Models.Skripta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FakultetID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PredmetID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FakultetID");

                    b.HasIndex("PredmetID");

                    b.ToTable("Skripta");
                });

            modelBuilder.Entity("Models.Knjiga", b =>
                {
                    b.HasOne("Models.Fakultet", "Fakultet")
                        .WithMany()
                        .HasForeignKey("FakultetID");

                    b.HasOne("Models.Predmet", "Predmet")
                        .WithMany()
                        .HasForeignKey("PredmetID");

                    b.HasOne("Models.Profesor", null)
                        .WithMany("Knjiga")
                        .HasForeignKey("ProfesorID");

                    b.Navigation("Fakultet");

                    b.Navigation("Predmet");
                });

            modelBuilder.Entity("Models.Predmet", b =>
                {
                    b.HasOne("Models.Fakultet", "Fakultet")
                        .WithMany("listaPredmeta")
                        .HasForeignKey("FakultetID");

                    b.HasOne("Models.Profesor", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorID");

                    b.Navigation("Fakultet");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("Models.Profesor", b =>
                {
                    b.HasOne("Models.Fakultet", "Fakultet")
                        .WithMany()
                        .HasForeignKey("FakultetID");

                    b.Navigation("Fakultet");
                });

            modelBuilder.Entity("Models.Skripta", b =>
                {
                    b.HasOne("Models.Fakultet", "Fakultet")
                        .WithMany()
                        .HasForeignKey("FakultetID");

                    b.HasOne("Models.Predmet", "Predmet")
                        .WithMany()
                        .HasForeignKey("PredmetID");

                    b.Navigation("Fakultet");

                    b.Navigation("Predmet");
                });

            modelBuilder.Entity("Models.Fakultet", b =>
                {
                    b.Navigation("listaPredmeta");
                });

            modelBuilder.Entity("Models.Profesor", b =>
                {
                    b.Navigation("Knjiga");
                });
#pragma warning restore 612, 618
        }
    }
}