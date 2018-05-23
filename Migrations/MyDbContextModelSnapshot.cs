﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using webmva.Data;
using webmva.Models;

namespace webmva.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("webmva.Models.ModuliProgetto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ModuloID");

                    b.Property<int>("ProgettoID");

                    b.HasKey("ID");

                    b.HasIndex("ModuloID");

                    b.HasIndex("ProgettoID");

                    b.ToTable("ModuliProgetto");
                });

            modelBuilder.Entity("webmva.Models.Modulo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Applicazione");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Nome");

                    b.HasKey("ID");

                    b.ToTable("Moduli");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Modulo");
                });

            modelBuilder.Entity("webmva.Models.Progetto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descrizione");

                    b.Property<string>("Nome");

                    b.Property<string>("Target");

                    b.HasKey("ID");

                    b.ToTable("Progetti");
                });

            modelBuilder.Entity("webmva.Models.ModuloDNSRECON", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("AXFREnum");

                    b.Property<bool>("BingEnum");

                    b.Property<string>("ComandoPersonalizzato");

                    b.Property<bool>("CrtShEnum");

                    b.Property<bool>("DeepWhois");

                    b.Property<string>("Dominio");

                    b.Property<bool>("GoogleEnum");

                    b.Property<string>("NameServer");

                    b.Property<bool>("ReverseLookupEnum");

                    b.Property<bool>("ZoneWalk");

                    b.ToTable("ModuloDNSRECON");

                    b.HasDiscriminator().HasValue("ModuloDNSRECON");
                });

            modelBuilder.Entity("webmva.Models.ModuloDROOPE", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<int>("Check");

                    b.Property<int>("Cms");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloDROOPE_ComandoPersonalizzato");

                    b.Property<string>("URL");

                    b.ToTable("ModuloDROOPE");

                    b.HasDiscriminator().HasValue("ModuloDROOPE");
                });

            modelBuilder.Entity("webmva.Models.ModuloINFOGA", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloINFOGA_ComandoPersonalizzato");

                    b.Property<string>("Dominio")
                        .HasColumnName("ModuloINFOGA_Dominio");

                    b.Property<int>("Source");

                    b.Property<int>("Verbose");

                    b.ToTable("ModuloINFOGA");

                    b.HasDiscriminator().HasValue("ModuloINFOGA");
                });

            modelBuilder.Entity("webmva.Models.ModuloINFOGAEMAIL", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("Breach");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloINFOGAEMAIL_ComandoPersonalizzato");

                    b.Property<string>("Email");

                    b.Property<int>("Verbose")
                        .HasColumnName("ModuloINFOGAEMAIL_Verbose");

                    b.ToTable("ModuloINFOGAEMAIL");

                    b.HasDiscriminator().HasValue("ModuloINFOGAEMAIL");
                });

            modelBuilder.Entity("webmva.Models.ModuloNESSUS", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("JSON");

                    b.ToTable("ModuloNESSUS");

                    b.HasDiscriminator().HasValue("ModuloNESSUS");
                });

            modelBuilder.Entity("webmva.Models.ModuloNMAP", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("AckDiscoveryPorts");

                    b.Property<bool>("AllDetections");

                    b.Property<bool>("ArpDiscovery");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloNMAP_ComandoPersonalizzato");

                    b.Property<bool>("FastScan");

                    b.Property<bool>("Fragmented");

                    b.Property<bool>("IPv6Scan");

                    b.Property<bool>("IncreaseVerbosity");

                    b.Property<string>("ListSpecificPort");

                    b.Property<bool>("NoDNSResolution");

                    b.Property<bool>("NoHostDiscovery");

                    b.Property<int>("NonTCPScan");

                    b.Property<bool>("OSDetectionAggressive");

                    b.Property<bool>("OSdetection");

                    b.Property<bool>("ScanAllPorts");

                    b.Property<string>("SendScansFromSpoofedIP");

                    b.Property<bool>("ServiceVersion");

                    b.Property<string>("SynDiscoveryPorts");

                    b.Property<int>("TCPScan");

                    b.Property<int>("Tempo");

                    b.Property<string>("UdpDiscoveryPorts");

                    b.ToTable("ModuloNMAP");

                    b.HasDiscriminator().HasValue("ModuloNMAP");
                });

            modelBuilder.Entity("webmva.Models.ModuloWAPITI", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("All");

                    b.Property<bool>("BackUp");

                    b.Property<bool>("BlindSql");

                    b.Property<bool>("Buster");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloWAPITI_ComandoPersonalizzato");

                    b.Property<bool>("Common");

                    b.Property<bool>("Crlf");

                    b.Property<bool>("Exec");

                    b.Property<bool>("File");

                    b.Property<int>("Force");

                    b.Property<bool>("Htaccess");

                    b.Property<int>("MaxMinutes");

                    b.Property<bool>("Nessuno");

                    b.Property<bool>("Nikto");

                    b.Property<bool>("PermanentXss");

                    b.Property<int>("Scope");

                    b.Property<bool>("ShellShock");

                    b.Property<bool>("Sql");

                    b.Property<string>("URL")
                        .HasColumnName("ModuloWAPITI_URL");

                    b.Property<int>("Verbose")
                        .HasColumnName("ModuloWAPITI_Verbose");

                    b.Property<bool>("Xss");

                    b.ToTable("ModuloWAPITI");

                    b.HasDiscriminator().HasValue("ModuloWAPITI");
                });

            modelBuilder.Entity("webmva.Models.ModuliProgetto", b =>
                {
                    b.HasOne("webmva.Models.Modulo", "Modulo")
                        .WithMany()
                        .HasForeignKey("ModuloID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("webmva.Models.Progetto", "Progetto")
                        .WithMany("ModuliProgetto")
                        .HasForeignKey("ProgettoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
