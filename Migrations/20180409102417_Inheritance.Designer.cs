﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using webmva.Data;

namespace webmva.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20180409102417_Inheritance")]
    partial class Inheritance
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Nome");

                    b.HasKey("ID");

                    b.ToTable("Progetti");
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

                    b.Property<bool>("ACKScan");

                    b.Property<string>("AckDiscoveryPorts");

                    b.Property<bool>("AllDetections");

                    b.Property<bool>("ArpDiscovery");

                    b.Property<bool>("FastScan");

                    b.Property<bool>("Fragmented");

                    b.Property<bool>("IPv6Scan");

                    b.Property<bool>("IncreaseVerbosity");

                    b.Property<string>("ListSpecificPort");

                    b.Property<int>("LivelloParanoia");

                    b.Property<bool>("MaimonScan");

                    b.Property<bool>("NoDNSResolution");

                    b.Property<bool>("NoHostDiscovery");

                    b.Property<bool>("NoPortScan");

                    b.Property<bool>("NoScan");

                    b.Property<bool>("OSDetectionAggressive");

                    b.Property<bool>("OSdetection");

                    b.Property<bool>("SYNScan");

                    b.Property<bool>("ScanAllPorts");

                    b.Property<string>("SendScansFromSpoofedIP");

                    b.Property<bool>("ServiceVersion");

                    b.Property<int>("ServiceVersionIntensity");

                    b.Property<string>("SynDiscoveryPorts");

                    b.Property<bool>("TCPConnectScan");

                    b.Property<bool>("UDPScan");

                    b.Property<string>("UdpDiscoveryPorts");

                    b.Property<bool>("WindowPortScan");

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
