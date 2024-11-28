﻿// <auto-generated />
using LoginStartMenu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoginStartMenu.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241126083656_RelazioneMoltiaMoltiUtenteRuolo")]
    partial class RelazioneMoltiaMoltiUtenteRuolo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoginStartMenu.Models.Entity.Ruolo", b =>
                {
                    b.Property<int>("IdRuolo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRuolo"));

                    b.Property<string>("NomeRuolo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRuolo");

                    b.ToTable("Ruoli");
                });

            modelBuilder.Entity("LoginStartMenu.Models.Entity.Utente", b =>
                {
                    b.Property<int>("IdUtente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUtente"));

                    b.Property<string>("CodiceFiscale")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUtente");

                    b.ToTable("Utenti");
                });

            modelBuilder.Entity("LoginStartMenu.Models.Entity.UtenteRuolo", b =>
                {
                    b.Property<int>("IdUtenteRuolo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUtenteRuolo"));

                    b.Property<int>("IdRuolo")
                        .HasColumnType("int");

                    b.Property<int>("IdUtente")
                        .HasColumnType("int");

                    b.HasKey("IdUtenteRuolo");

                    b.HasIndex("IdRuolo");

                    b.HasIndex("IdUtente");

                    b.ToTable("UtentiRuoli");
                });

            modelBuilder.Entity("LoginStartMenu.Models.Entity.UtenteRuolo", b =>
                {
                    b.HasOne("LoginStartMenu.Models.Entity.Ruolo", "Ruolo")
                        .WithMany("UtentiRuoli")
                        .HasForeignKey("IdRuolo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoginStartMenu.Models.Entity.Utente", "Utente")
                        .WithMany("UtentiRuoli")
                        .HasForeignKey("IdUtente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ruolo");

                    b.Navigation("Utente");
                });

            modelBuilder.Entity("LoginStartMenu.Models.Entity.Ruolo", b =>
                {
                    b.Navigation("UtentiRuoli");
                });

            modelBuilder.Entity("LoginStartMenu.Models.Entity.Utente", b =>
                {
                    b.Navigation("UtentiRuoli");
                });
#pragma warning restore 612, 618
        }
    }
}