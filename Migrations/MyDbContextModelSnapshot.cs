﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
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

                    b.Property<DateTime>("Data");

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
