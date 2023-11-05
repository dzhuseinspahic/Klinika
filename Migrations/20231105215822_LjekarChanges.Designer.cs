﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjektniZadatak.Data;

#nullable disable

namespace ProjektniZadatak.Migrations
{
    [DbContext(typeof(KlinikaContext))]
    [Migration("20231105215822_LjekarChanges")]
    partial class LjekarChanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjektniZadatak.Models.Ljekar", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Titula")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Ljekar", (string)null);
                });

            modelBuilder.Entity("ProjektniZadatak.Models.Nalaz", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DatumVrijemeKreiranja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("Nalaz", (string)null);
                });

            modelBuilder.Entity("ProjektniZadatak.Models.Pacijent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Spol")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Pacijent", (string)null);
                });

            modelBuilder.Entity("ProjektniZadatak.Models.Prijem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DatumVrijemePrijema")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HitniPrijem")
                        .HasColumnType("bit");

                    b.Property<int>("LjekarID")
                        .HasColumnType("int");

                    b.Property<int?>("NalazID")
                        .HasColumnType("int");

                    b.Property<int>("PacijentID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("LjekarID");

                    b.HasIndex("NalazID");

                    b.HasIndex("PacijentID");

                    b.ToTable("Prijem", (string)null);
                });

            modelBuilder.Entity("ProjektniZadatak.Models.Prijem", b =>
                {
                    b.HasOne("ProjektniZadatak.Models.Ljekar", "Ljekar")
                        .WithMany("Prijemi")
                        .HasForeignKey("LjekarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjektniZadatak.Models.Nalaz", "Nalaz")
                        .WithMany()
                        .HasForeignKey("NalazID");

                    b.HasOne("ProjektniZadatak.Models.Pacijent", "Pacijent")
                        .WithMany("Prijemi")
                        .HasForeignKey("PacijentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ljekar");

                    b.Navigation("Nalaz");

                    b.Navigation("Pacijent");
                });

            modelBuilder.Entity("ProjektniZadatak.Models.Ljekar", b =>
                {
                    b.Navigation("Prijemi");
                });

            modelBuilder.Entity("ProjektniZadatak.Models.Pacijent", b =>
                {
                    b.Navigation("Prijemi");
                });
#pragma warning restore 612, 618
        }
    }
}