using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace webmva.Migrations
{
    public partial class InseritoOPENDOOR7 : Migration
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
                    GoogleEnum = table.Column<bool>(nullable: true),
                    NameServer = table.Column<string>(nullable: true),
                    ReverseLookupEnum = table.Column<bool>(nullable: true),
                    ZoneWalk = table.Column<bool>(nullable: true),
                    Check = table.Column<int>(nullable: true),
                    Cms = table.Column<int>(nullable: true),
                    ModuloDROOPE_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloFIERCE_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Connect = table.Column<bool>(nullable: true),
                    DnServer = table.Column<string>(nullable: true),
                    SubDomain = table.Column<string>(nullable: true),
                    Wide = table.Column<bool>(nullable: true),
                    ModuloINFOGA_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Source = table.Column<int>(nullable: true),
                    Verbose = table.Column<int>(nullable: true),
                    Breach = table.Column<bool>(nullable: true),
                    ModuloINFOGAEMAIL_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloINFOGAEMAIL_Verbose = table.Column<int>(nullable: true),
                    ModuloJOOMSCAN_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Cookie = table.Column<string>(nullable: true),
                    EnumerateComponents = table.Column<bool>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true),
                    Porta = table.Column<int>(nullable: true),
                    ServerIP = table.Column<string>(nullable: true),
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
                    AcceptCookies = table.Column<bool>(nullable: true),
                    ModuloOPENDOOR_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    DelayO = table.Column<int>(nullable: true),
                    Metodo = table.Column<string>(nullable: true),
                    PortaO = table.Column<int>(nullable: true),
                    RetriesO = table.Column<int>(nullable: true),
                    TimeoutO = table.Column<int>(nullable: true),
                    ModuloSQLMAP_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Detection = table.Column<int>(nullable: true),
                    Rischio = table.Column<int>(nullable: true),
                    TorType = table.Column<int>(nullable: true),
                    ModuloSQLMAP_Verbose = table.Column<int>(nullable: true),
                    a = table.Column<bool>(nullable: true),
                    allOptimization = table.Column<bool>(nullable: true),
                    b = table.Column<bool>(nullable: true),
                    checkTor = table.Column<bool>(nullable: true),
                    columns = table.Column<bool>(nullable: true),
                    commonColumns = table.Column<bool>(nullable: true),
                    commonTables = table.Column<bool>(nullable: true),
                    count = table.Column<bool>(nullable: true),
                    credenzialiAutenticazione = table.Column<string>(nullable: true),
                    currentDb = table.Column<bool>(nullable: true),
                    currentUser = table.Column<bool>(nullable: true),
                    dbms = table.Column<string>(nullable: true),
                    dbmsCredenziali = table.Column<string>(nullable: true),
                    dbs = table.Column<bool>(nullable: true),
                    delay = table.Column<int>(nullable: true),
                    dumpAll = table.Column<bool>(nullable: true),
                    excludesSySdbs = table.Column<bool>(nullable: true),
                    forceAggressive = table.Column<bool>(nullable: true),
                    forceSsl = table.Column<bool>(nullable: true),
                    header = table.Column<string>(nullable: true),
                    hostName = table.Column<bool>(nullable: true),
                    keepAlive = table.Column<bool>(nullable: true),
                    nessunaConnessione = table.Column<bool>(nullable: true),
                    passwords = table.Column<bool>(nullable: true),
                    portaSql = table.Column<int>(nullable: true),
                    predictOutput = table.Column<bool>(nullable: true),
                    privileges = table.Column<bool>(nullable: true),
                    retries = table.Column<int>(nullable: true),
                    roles = table.Column<bool>(nullable: true),
                    schema = table.Column<bool>(nullable: true),
                    search = table.Column<bool>(nullable: true),
                    sistemaOperativo = table.Column<string>(nullable: true),
                    tables = table.Column<bool>(nullable: true),
                    tecnique = table.Column<string>(nullable: true),
                    threads = table.Column<int>(nullable: true),
                    timeout = table.Column<int>(nullable: true),
                    timesec = table.Column<int>(nullable: true),
                    tor = table.Column<bool>(nullable: true),
                    users = table.Column<bool>(nullable: true),
                    All = table.Column<bool>(nullable: true),
                    BackUp = table.Column<bool>(nullable: true),
                    BlindSql = table.Column<bool>(nullable: true),
                    Buster = table.Column<bool>(nullable: true),
                    ModuloWAPITI_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Common = table.Column<bool>(nullable: true),
                    Crlf = table.Column<bool>(nullable: true),
                    Exec = table.Column<bool>(nullable: true),
                    File = table.Column<bool>(nullable: true),
                    Force = table.Column<int>(nullable: true),
                    Htaccess = table.Column<bool>(nullable: true),
                    MaxMinutes = table.Column<int>(nullable: true),
                    Nessuno = table.Column<bool>(nullable: true),
                    Nikto = table.Column<bool>(nullable: true),
                    PermanentXss = table.Column<bool>(nullable: true),
                    Scope = table.Column<int>(nullable: true),
                    ShellShock = table.Column<bool>(nullable: true),
                    Sql = table.Column<bool>(nullable: true),
                    ModuloWAPITI_Verbose = table.Column<int>(nullable: true),
                    Xss = table.Column<bool>(nullable: true),
                    ModuloWIFITE_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloWIFITE_Verbose = table.Column<int>(nullable: true),
                    Wps = table.Column<int>(nullable: true),
                    accessPoint = table.Column<bool>(nullable: true),
                    bully = table.Column<bool>(nullable: true),
                    channel = table.Column<int>(nullable: true),
                    client = table.Column<bool>(nullable: true),
                    crack = table.Column<bool>(nullable: true),
                    fakeAutenticazione = table.Column<bool>(nullable: true),
                    interfaccia = table.Column<string>(nullable: true),
                    keepIvs = table.Column<bool>(nullable: true),
                    mac = table.Column<bool>(nullable: true),
                    newHs = table.Column<bool>(nullable: true),
                    scanTime = table.Column<int>(nullable: true),
                    wep = table.Column<bool>(nullable: true),
                    wpa = table.Column<bool>(nullable: true),
                    wpsSetting = table.Column<bool>(nullable: true),
                    ModuloWPSCAN_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloWPSCAN_Cookie = table.Column<string>(nullable: true),
                    DisableChecks = table.Column<bool>(nullable: true),
                    ModuloWPSCAN_Force = table.Column<bool>(nullable: true),
                    RandomAgent = table.Column<bool>(nullable: true),
                    ModuloWPSCAN_UserAgent = table.Column<string>(nullable: true),
                    VerbositàWP = table.Column<bool>(nullable: true)
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
                    ProgettoID = table.Column<int>(nullable: false),
                    Taget = table.Column<string>(nullable: true)
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
