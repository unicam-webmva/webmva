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

                    b.Property<string>("Target");

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

            modelBuilder.Entity("webmva.Models.PercorsiReport", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Percorso");

                    b.Property<int>("ReportID");

                    b.HasKey("ID");

                    b.HasIndex("ReportID");

                    b.ToTable("PercorsiReport");
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

            modelBuilder.Entity("webmva.Models.Report", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int>("ProgettoID");

                    b.HasKey("ID");

                    b.HasIndex("ProgettoID");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("webmva.Models.ModuloDNSENUM", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("ComandoPersonalizzato");

                    b.Property<int>("DelayDNS");

                    b.Property<bool>("NoReverseDNS");

                    b.Property<int>("PagesDNS");

                    b.Property<bool>("RecursionDNS");

                    b.Property<int>("ScrapDNS");

                    b.Property<int>("ThreadsDNS");

                    b.Property<int>("TimeoutDNS");

                    b.Property<bool>("VerboseDNS");

                    b.Property<bool>("Whois");

                    b.ToTable("ModuloDNSENUM");

                    b.HasDiscriminator().HasValue("ModuloDNSENUM");
                });

            modelBuilder.Entity("webmva.Models.ModuloDNSRECON", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("AXFREnum");

                    b.Property<bool>("BingEnum");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloDNSRECON_ComandoPersonalizzato");

                    b.Property<bool>("CrtShEnum");

                    b.Property<bool>("DeepWhois");

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

                    b.ToTable("ModuloDROOPE");

                    b.HasDiscriminator().HasValue("ModuloDROOPE");
                });

            modelBuilder.Entity("webmva.Models.ModuloFIERCE", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloFIERCE_ComandoPersonalizzato");

                    b.Property<bool>("Connect");

                    b.Property<string>("DnServer");

                    b.Property<string>("SubDomain");

                    b.Property<bool>("Wide");

                    b.ToTable("ModuloFIERCE");

                    b.HasDiscriminator().HasValue("ModuloFIERCE");
                });

            modelBuilder.Entity("webmva.Models.ModuloINFOGA", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloINFOGA_ComandoPersonalizzato");

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

                    b.Property<int>("Verbose")
                        .HasColumnName("ModuloINFOGAEMAIL_Verbose");

                    b.ToTable("ModuloINFOGAEMAIL");

                    b.HasDiscriminator().HasValue("ModuloINFOGAEMAIL");
                });

            modelBuilder.Entity("webmva.Models.ModuloJOOMSCAN", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloJOOMSCAN_ComandoPersonalizzato");

                    b.Property<string>("Cookie");

                    b.Property<bool>("EnumerateComponents");

                    b.Property<string>("UserAgent");

                    b.ToTable("ModuloJOOMSCAN");

                    b.HasDiscriminator().HasValue("ModuloJOOMSCAN");
                });

            modelBuilder.Entity("webmva.Models.ModuloNESSUS", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<int>("Porta");

                    b.Property<string>("ServerIP");

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

            modelBuilder.Entity("webmva.Models.ModuloNOSQL", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");


                    b.ToTable("ModuloNOSQL");

                    b.HasDiscriminator().HasValue("ModuloNOSQL");
                });

            modelBuilder.Entity("webmva.Models.ModuloODAT", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("AllOdat");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloODAT_ComandoPersonalizzato");

                    b.Property<bool>("PasswordGuesser");

                    b.Property<string>("PasswordOdat");

                    b.Property<bool>("PasswordStealer");

                    b.Property<int>("PortaOdat");

                    b.Property<string>("SID");

                    b.Property<bool>("SmbOdat");

                    b.Property<bool>("TestOdat");

                    b.Property<bool>("Tnscmd");

                    b.Property<bool>("Tnspoison");

                    b.Property<string>("UtenteOdat");

                    b.Property<int>("VerboseOdat");

                    b.ToTable("ModuloODAT");

                    b.HasDiscriminator().HasValue("ModuloODAT");
                });

            modelBuilder.Entity("webmva.Models.ModuloOPENDOOR", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("AcceptCookies");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloOPENDOOR_ComandoPersonalizzato");

                    b.Property<int>("DelayO");

                    b.Property<string>("Metodo");

                    b.Property<int>("PortaO");

                    b.Property<int>("RetriesO");

                    b.Property<int>("TimeoutO");

                    b.ToTable("ModuloOPENDOOR");

                    b.HasDiscriminator().HasValue("ModuloOPENDOOR");
                });

            modelBuilder.Entity("webmva.Models.ModuloOPENVAS", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<int>("PortaOpenvas");

                    b.Property<string>("ServerIPOpenvas");

                    b.ToTable("ModuloOPENVAS");

                    b.HasDiscriminator().HasValue("ModuloOPENVAS");
                });

            modelBuilder.Entity("webmva.Models.ModuloSQLMAP", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloSQLMAP_ComandoPersonalizzato");

                    b.Property<int>("Detection");

                    b.Property<int>("Rischio");

                    b.Property<int>("TorType");

                    b.Property<int>("Verbose")
                        .HasColumnName("ModuloSQLMAP_Verbose");

                    b.Property<bool>("a");

                    b.Property<bool>("allOptimization");

                    b.Property<bool>("b");

                    b.Property<bool>("checkTor");

                    b.Property<bool>("columns");

                    b.Property<bool>("commonColumns");

                    b.Property<bool>("commonTables");

                    b.Property<bool>("count");

                    b.Property<string>("credenzialiAutenticazione");

                    b.Property<bool>("currentDb");

                    b.Property<bool>("currentUser");

                    b.Property<string>("dbms");

                    b.Property<string>("dbmsCredenziali");

                    b.Property<bool>("dbs");

                    b.Property<int>("delay");

                    b.Property<bool>("dumpAll");

                    b.Property<bool>("excludesSySdbs");

                    b.Property<bool>("forceAggressive");

                    b.Property<bool>("forceSsl");

                    b.Property<string>("header");

                    b.Property<bool>("hostName");

                    b.Property<bool>("keepAlive");

                    b.Property<bool>("nessunaConnessione");

                    b.Property<bool>("passwords");

                    b.Property<int>("portaSql");

                    b.Property<bool>("predictOutput");

                    b.Property<bool>("privileges");

                    b.Property<int>("retries");

                    b.Property<bool>("roles");

                    b.Property<bool>("schema");

                    b.Property<bool>("search");

                    b.Property<string>("sistemaOperativo");

                    b.Property<bool>("tables");

                    b.Property<string>("tecnique");

                    b.Property<int>("threads");

                    b.Property<int>("timeout");

                    b.Property<int>("timesec");

                    b.Property<bool>("tor");

                    b.Property<bool>("users");

                    b.ToTable("ModuloSQLMAP");

                    b.HasDiscriminator().HasValue("ModuloSQLMAP");
                });

            modelBuilder.Entity("webmva.Models.ModuloSUBLIST3R", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("All");

                    b.Property<bool>("AskSUB");

                    b.Property<bool>("BaiduSUB");

                    b.Property<bool>("BingSUB");

                    b.Property<bool>("BruteforceSUB");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloSUBLIST3R_ComandoPersonalizzato");

                    b.Property<bool>("DNSdumpsterSUB");

                    b.Property<bool>("GoogleSUB");

                    b.Property<bool>("NetcraftSUB");

                    b.Property<bool>("PassiveDNSSUB");

                    b.Property<string>("PorteSUB");

                    b.Property<bool>("SSLCertificatesSUB");

                    b.Property<int>("ThreadSUB");

                    b.Property<bool>("ThreatCrowdSUB");

                    b.Property<bool>("VerbositàSUB");

                    b.Property<bool>("VirustotalSUB");

                    b.Property<bool>("YahooSUB");

                    b.ToTable("ModuloSUBLIST3R");

                    b.HasDiscriminator().HasValue("ModuloSUBLIST3R");
                });

            modelBuilder.Entity("webmva.Models.ModuloWAPITI", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("All")
                        .HasColumnName("ModuloWAPITI_All");

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

                    b.Property<int>("Verbose")
                        .HasColumnName("ModuloWAPITI_Verbose");

                    b.Property<bool>("Xss");

                    b.ToTable("ModuloWAPITI");

                    b.HasDiscriminator().HasValue("ModuloWAPITI");
                });

            modelBuilder.Entity("webmva.Models.ModuloWASCAN", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<bool>("Attacks");

                    b.Property<bool>("Audit");

                    b.Property<string>("AutenticazioneW");

                    b.Property<bool>("Bruteforce");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloWASCAN_ComandoPersonalizzato");

                    b.Property<string>("CookiesW");

                    b.Property<bool>("Disclosure");

                    b.Property<bool>("Fingerprint");

                    b.Property<bool>("FullScanW");

                    b.Property<string>("HeadersW");

                    b.Property<int>("MethodsW");

                    b.Property<bool>("ReagentW");

                    b.Property<bool>("RedirectW");

                    b.Property<int>("TimeoutW");

                    b.Property<string>("UserAgentW");

                    b.Property<bool>("VerbositàW");

                    b.ToTable("ModuloWASCAN");

                    b.HasDiscriminator().HasValue("ModuloWASCAN");
                });

            modelBuilder.Entity("webmva.Models.ModuloWIFITE", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloWIFITE_ComandoPersonalizzato");

                    b.Property<int>("Wps");

                    b.Property<bool>("accessPoint");

                    b.Property<bool>("bully");

                    b.Property<int>("channel");

                    b.Property<bool>("client");

                    b.Property<bool>("crack");

                    b.Property<bool>("fakeAutenticazione");

                    b.Property<bool>("keepIvs");

                    b.Property<bool>("newHs");

                    b.Property<int>("scanTime");

                    b.Property<bool>("verbositaWifite");

                    b.Property<bool>("wep");

                    b.Property<bool>("wpa");

                    b.Property<bool>("wpsSetting");

                    b.ToTable("ModuloWIFITE");

                    b.HasDiscriminator().HasValue("ModuloWIFITE");
                });

            modelBuilder.Entity("webmva.Models.ModuloWPSCAN", b =>
                {
                    b.HasBaseType("webmva.Models.Modulo");

                    b.Property<string>("ComandoPersonalizzato")
                        .HasColumnName("ModuloWPSCAN_ComandoPersonalizzato");

                    b.Property<string>("Cookie")
                        .HasColumnName("ModuloWPSCAN_Cookie");

                    b.Property<bool>("DisableChecks");

                    b.Property<bool>("Force")
                        .HasColumnName("ModuloWPSCAN_Force");

                    b.Property<bool>("RandomAgent");

                    b.Property<string>("UserAgent")
                        .HasColumnName("ModuloWPSCAN_UserAgent");

                    b.Property<bool>("VerbositàWP");

                    b.ToTable("ModuloWPSCAN");

                    b.HasDiscriminator().HasValue("ModuloWPSCAN");
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

            modelBuilder.Entity("webmva.Models.PercorsiReport", b =>
                {
                    b.HasOne("webmva.Models.Report", "Report")
                        .WithMany("Percorsi")
                        .HasForeignKey("ReportID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("webmva.Models.Report", b =>
                {
                    b.HasOne("webmva.Models.Progetto", "Progetto")
                        .WithMany()
                        .HasForeignKey("ProgettoID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
