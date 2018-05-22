﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace webmva.Migrations
{
    public partial class InseritoWapiti4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moduli",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Applicazione = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    AXFREnum = table.Column<bool>(nullable: true),
                    BingEnum = table.Column<bool>(nullable: true),
                    ComandoPersonalizzato = table.Column<string>(nullable: true),
                    CrtShEnum = table.Column<bool>(nullable: true),
                    DeepWhois = table.Column<bool>(nullable: true),
                    Dominio = table.Column<string>(nullable: true),
                    GoogleEnum = table.Column<bool>(nullable: true),
                    NameServer = table.Column<string>(nullable: true),
                    ReverseLookupEnum = table.Column<bool>(nullable: true),
                    ZoneWalk = table.Column<bool>(nullable: true),
                    Check = table.Column<int>(nullable: true),
                    Cms = table.Column<int>(nullable: true),
                    ModuloDROOPE_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    Breach = table.Column<bool>(nullable: true),
                    ModuloINFOGA_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloINFOGA_Dominio = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Source = table.Column<int>(nullable: true),
                    Verbose = table.Column<int>(nullable: true),
                    JSON = table.Column<string>(nullable: true),
                    AckDiscoveryPorts = table.Column<string>(nullable: true),
                    AllDetections = table.Column<bool>(nullable: true),
                    ArpDiscovery = table.Column<bool>(nullable: true),
                    ModuloNMAP_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    FastScan = table.Column<bool>(nullable: true),
                    Fragmented = table.Column<bool>(nullable: true),
                    IPv6Scan = table.Column<bool>(nullable: true),
                    IncreaseVerbosity = table.Column<bool>(nullable: true),
                    ListSpecificPort = table.Column<string>(nullable: true),
                    NoDNSResolution = table.Column<bool>(nullable: true),
                    NoHostDiscovery = table.Column<bool>(nullable: true),
                    NonTCPScan = table.Column<int>(nullable: true),
                    OSDetectionAggressive = table.Column<bool>(nullable: true),
                    OSdetection = table.Column<bool>(nullable: true),
                    ScanAllPorts = table.Column<bool>(nullable: true),
                    SendScansFromSpoofedIP = table.Column<string>(nullable: true),
                    ServiceVersion = table.Column<bool>(nullable: true),
                    SynDiscoveryPorts = table.Column<string>(nullable: true),
                    TCPScan = table.Column<int>(nullable: true),
                    Tempo = table.Column<int>(nullable: true),
                    UdpDiscoveryPorts = table.Column<string>(nullable: true),
                    All = table.Column<bool>(nullable: true),
                    BackUp = table.Column<bool>(nullable: true),
                    BlindSql = table.Column<bool>(nullable: true),
                    Buster = table.Column<bool>(nullable: true),
                    ModuloWAPITI_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Crlf = table.Column<bool>(nullable: true),
                    Exec = table.Column<bool>(nullable: true),
                    File = table.Column<bool>(nullable: true),
                    Force = table.Column<int>(nullable: true),
                    Htaccess = table.Column<bool>(nullable: true),
                    MaxMinutes = table.Column<int>(nullable: true),
                    Nikto = table.Column<bool>(nullable: true),
                    PermanentXss = table.Column<bool>(nullable: true),
                    Scope = table.Column<int>(nullable: true),
                    ShellShock = table.Column<bool>(nullable: true),
                    Sql = table.Column<bool>(nullable: true),
                    ModuloWAPITI_URL = table.Column<string>(nullable: true),
                    ModuloWAPITI_Verbose = table.Column<int>(nullable: true),
                    Xss = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduli", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Progetti",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descrizione = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Target = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progetti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ModuliProgetto",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModuloID = table.Column<int>(nullable: false),
                    ProgettoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuliProgetto", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ModuliProgetto_Moduli_ModuloID",
                        column: x => x.ModuloID,
                        principalTable: "Moduli",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuliProgetto_Progetti_ProgettoID",
                        column: x => x.ProgettoID,
                        principalTable: "Progetti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuliProgetto_ModuloID",
                table: "ModuliProgetto",
                column: "ModuloID");

            migrationBuilder.CreateIndex(
                name: "IX_ModuliProgetto_ProgettoID",
                table: "ModuliProgetto",
                column: "ProgettoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuliProgetto");

            migrationBuilder.DropTable(
                name: "Moduli");

            migrationBuilder.DropTable(
                name: "Progetti");
        }
    }
}