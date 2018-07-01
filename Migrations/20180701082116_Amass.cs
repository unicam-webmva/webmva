using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace webmva.Migrations
{
    public partial class Amass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moduli",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Applicazione = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    ActiveAmass = table.Column<bool>(type: "INTEGER", nullable: true),
                    AllTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    BlacklistAmass = table.Column<string>(type: "TEXT", nullable: true),
                    BlacklistSubdomainAmass = table.Column<string>(type: "TEXT", nullable: true),
                    BruteAmass = table.Column<bool>(type: "INTEGER", nullable: true),
                    ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    IpAmass = table.Column<bool>(type: "INTEGER", nullable: true),
                    NoAltsAmass = table.Column<bool>(type: "INTEGER", nullable: true),
                    NoDnsAmass = table.Column<bool>(type: "INTEGER", nullable: true),
                    NumberOfFrequences = table.Column<int>(type: "INTEGER", nullable: true),
                    PorteAmass = table.Column<string>(type: "TEXT", nullable: true),
                    VerboseAmass = table.Column<bool>(type: "INTEGER", nullable: true),
                    WhoisAmass = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloDNSENUM_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    DelayDNS = table.Column<int>(type: "INTEGER", nullable: true),
                    NoReverseDNS = table.Column<bool>(type: "INTEGER", nullable: true),
                    PagesDNS = table.Column<int>(type: "INTEGER", nullable: true),
                    RecursionDNS = table.Column<bool>(type: "INTEGER", nullable: true),
                    ScrapDNS = table.Column<int>(type: "INTEGER", nullable: true),
                    ThreadsDNS = table.Column<int>(type: "INTEGER", nullable: true),
                    TimeoutDNS = table.Column<int>(type: "INTEGER", nullable: true),
                    VerboseDNS = table.Column<bool>(type: "INTEGER", nullable: true),
                    Whois = table.Column<bool>(type: "INTEGER", nullable: true),
                    AXFREnum = table.Column<bool>(type: "INTEGER", nullable: true),
                    BingEnum = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloDNSRECON_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    CrtShEnum = table.Column<bool>(type: "INTEGER", nullable: true),
                    DeepWhois = table.Column<bool>(type: "INTEGER", nullable: true),
                    GoogleEnum = table.Column<bool>(type: "INTEGER", nullable: true),
                    NameServer = table.Column<string>(type: "TEXT", nullable: true),
                    ReverseLookupEnum = table.Column<bool>(type: "INTEGER", nullable: true),
                    ZoneWalk = table.Column<bool>(type: "INTEGER", nullable: true),
                    Check = table.Column<int>(type: "INTEGER", nullable: true),
                    Cms = table.Column<int>(type: "INTEGER", nullable: true),
                    ModuloDROOPE_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    ModuloFIERCE_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    Connect = table.Column<bool>(type: "INTEGER", nullable: true),
                    DnServer = table.Column<string>(type: "TEXT", nullable: true),
                    SubDomain = table.Column<string>(type: "TEXT", nullable: true),
                    Wide = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloINFOGA_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    Source = table.Column<int>(type: "INTEGER", nullable: true),
                    Verbose = table.Column<int>(type: "INTEGER", nullable: true),
                    Breach = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloINFOGAEMAIL_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    ModuloINFOGAEMAIL_Verbose = table.Column<int>(type: "INTEGER", nullable: true),
                    ModuloJOOMSCAN_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    EnumerateComponents = table.Column<bool>(type: "INTEGER", nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    Porta = table.Column<int>(type: "INTEGER", nullable: true),
                    ServerIP = table.Column<string>(type: "TEXT", nullable: true),
                    AckDiscoveryPorts = table.Column<string>(type: "TEXT", nullable: true),
                    AllDetections = table.Column<bool>(type: "INTEGER", nullable: true),
                    ArpDiscovery = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloNMAP_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    FastScan = table.Column<bool>(type: "INTEGER", nullable: true),
                    Fragmented = table.Column<bool>(type: "INTEGER", nullable: true),
                    IPv6Scan = table.Column<bool>(type: "INTEGER", nullable: true),
                    IncreaseVerbosity = table.Column<bool>(type: "INTEGER", nullable: true),
                    ListSpecificPort = table.Column<string>(type: "TEXT", nullable: true),
                    NoDNSResolution = table.Column<bool>(type: "INTEGER", nullable: true),
                    NoHostDiscovery = table.Column<bool>(type: "INTEGER", nullable: true),
                    NonTCPScan = table.Column<int>(type: "INTEGER", nullable: true),
                    OSDetectionAggressive = table.Column<bool>(type: "INTEGER", nullable: true),
                    OSdetection = table.Column<bool>(type: "INTEGER", nullable: true),
                    ScanAllPorts = table.Column<bool>(type: "INTEGER", nullable: true),
                    SendScansFromSpoofedIP = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceVersion = table.Column<bool>(type: "INTEGER", nullable: true),
                    SynDiscoveryPorts = table.Column<string>(type: "TEXT", nullable: true),
                    TCPScan = table.Column<int>(type: "INTEGER", nullable: true),
                    Tempo = table.Column<int>(type: "INTEGER", nullable: true),
                    UdpDiscoveryPorts = table.Column<string>(type: "TEXT", nullable: true),
                    AllOdat = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloODAT_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordGuesser = table.Column<bool>(type: "INTEGER", nullable: true),
                    PasswordOdat = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordStealer = table.Column<bool>(type: "INTEGER", nullable: true),
                    PortaOdat = table.Column<int>(type: "INTEGER", nullable: true),
                    SID = table.Column<string>(type: "TEXT", nullable: true),
                    SmbOdat = table.Column<bool>(type: "INTEGER", nullable: true),
                    TestOdat = table.Column<bool>(type: "INTEGER", nullable: true),
                    Tnscmd = table.Column<bool>(type: "INTEGER", nullable: true),
                    Tnspoison = table.Column<bool>(type: "INTEGER", nullable: true),
                    UtenteOdat = table.Column<string>(type: "TEXT", nullable: true),
                    VerboseOdat = table.Column<int>(type: "INTEGER", nullable: true),
                    AcceptCookies = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloOPENDOOR_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    DelayO = table.Column<int>(type: "INTEGER", nullable: true),
                    Metodo = table.Column<string>(type: "TEXT", nullable: true),
                    PortaO = table.Column<int>(type: "INTEGER", nullable: true),
                    RetriesO = table.Column<int>(type: "INTEGER", nullable: true),
                    TimeoutO = table.Column<int>(type: "INTEGER", nullable: true),
                    PortaOpenvas = table.Column<int>(type: "INTEGER", nullable: true),
                    ServerIPOpenvas = table.Column<string>(type: "TEXT", nullable: true),
                    ModuloSQLMAP_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    Detection = table.Column<int>(type: "INTEGER", nullable: true),
                    Rischio = table.Column<int>(type: "INTEGER", nullable: true),
                    ModuloSQLMAP_Verbose = table.Column<int>(type: "INTEGER", nullable: true),
                    a = table.Column<bool>(type: "INTEGER", nullable: true),
                    allOptimization = table.Column<bool>(type: "INTEGER", nullable: true),
                    b = table.Column<bool>(type: "INTEGER", nullable: true),
                    columns = table.Column<bool>(type: "INTEGER", nullable: true),
                    commonColumns = table.Column<bool>(type: "INTEGER", nullable: true),
                    commonTables = table.Column<bool>(type: "INTEGER", nullable: true),
                    count = table.Column<bool>(type: "INTEGER", nullable: true),
                    credenzialiAutenticazione = table.Column<string>(type: "TEXT", nullable: true),
                    currentDb = table.Column<bool>(type: "INTEGER", nullable: true),
                    currentUser = table.Column<bool>(type: "INTEGER", nullable: true),
                    dbms = table.Column<string>(type: "TEXT", nullable: true),
                    dbmsCredenziali = table.Column<string>(type: "TEXT", nullable: true),
                    dbs = table.Column<bool>(type: "INTEGER", nullable: true),
                    delay = table.Column<int>(type: "INTEGER", nullable: true),
                    dumpAll = table.Column<bool>(type: "INTEGER", nullable: true),
                    excludesSySdbs = table.Column<bool>(type: "INTEGER", nullable: true),
                    forceAggressive = table.Column<bool>(type: "INTEGER", nullable: true),
                    forceSsl = table.Column<bool>(type: "INTEGER", nullable: true),
                    header = table.Column<string>(type: "TEXT", nullable: true),
                    hostName = table.Column<bool>(type: "INTEGER", nullable: true),
                    keepAlive = table.Column<bool>(type: "INTEGER", nullable: true),
                    nessunaConnessione = table.Column<bool>(type: "INTEGER", nullable: true),
                    passwords = table.Column<bool>(type: "INTEGER", nullable: true),
                    predictOutput = table.Column<bool>(type: "INTEGER", nullable: true),
                    privileges = table.Column<bool>(type: "INTEGER", nullable: true),
                    retries = table.Column<int>(type: "INTEGER", nullable: true),
                    roles = table.Column<bool>(type: "INTEGER", nullable: true),
                    schema = table.Column<bool>(type: "INTEGER", nullable: true),
                    search = table.Column<bool>(type: "INTEGER", nullable: true),
                    sistemaOperativo = table.Column<string>(type: "TEXT", nullable: true),
                    tables = table.Column<bool>(type: "INTEGER", nullable: true),
                    tecnique = table.Column<string>(type: "TEXT", nullable: true),
                    threads = table.Column<int>(type: "INTEGER", nullable: true),
                    timeout = table.Column<int>(type: "INTEGER", nullable: true),
                    timesec = table.Column<int>(type: "INTEGER", nullable: true),
                    users = table.Column<bool>(type: "INTEGER", nullable: true),
                    AllSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    AskSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    BaiduSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    BingSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    BruteforceSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloSUBLIST3R_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    DNSdumpsterSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    GoogleSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    NetcraftSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    PassiveDNSSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    PorteSUB = table.Column<string>(type: "TEXT", nullable: true),
                    SSLCertificatesSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    ThreadSUB = table.Column<int>(type: "INTEGER", nullable: true),
                    ThreatCrowdSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    VerbositàSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    VirustotalSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    YahooSUB = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloTHEHARVESTER_AllTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    BingApiTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    BingTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloTHEHARVESTER_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    DNSBruteForceTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    DNSReverseQueryTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    DNSTLDTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    GoogleProfilesTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    GoogleTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    JigsawTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    LinkedinTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    NumberOfResult = table.Column<int>(type: "INTEGER", nullable: true),
                    People123TheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    PgpTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    RicercaHostsVirtualiTheH = table.Column<bool>(type: "INTEGER", nullable: true),
                    All = table.Column<bool>(type: "INTEGER", nullable: true),
                    BackUp = table.Column<bool>(type: "INTEGER", nullable: true),
                    BlindSql = table.Column<bool>(type: "INTEGER", nullable: true),
                    Buster = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloWAPITI_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    Common = table.Column<bool>(type: "INTEGER", nullable: true),
                    Crlf = table.Column<bool>(type: "INTEGER", nullable: true),
                    Exec = table.Column<bool>(type: "INTEGER", nullable: true),
                    File = table.Column<bool>(type: "INTEGER", nullable: true),
                    Force = table.Column<int>(type: "INTEGER", nullable: true),
                    Htaccess = table.Column<bool>(type: "INTEGER", nullable: true),
                    MaxMinutes = table.Column<int>(type: "INTEGER", nullable: true),
                    Nessuno = table.Column<bool>(type: "INTEGER", nullable: true),
                    Nikto = table.Column<bool>(type: "INTEGER", nullable: true),
                    PermanentXss = table.Column<bool>(type: "INTEGER", nullable: true),
                    Scope = table.Column<int>(type: "INTEGER", nullable: true),
                    ShellShock = table.Column<bool>(type: "INTEGER", nullable: true),
                    Sql = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloWAPITI_Verbose = table.Column<int>(type: "INTEGER", nullable: true),
                    Xss = table.Column<bool>(type: "INTEGER", nullable: true),
                    Attacks = table.Column<bool>(type: "INTEGER", nullable: true),
                    Audit = table.Column<bool>(type: "INTEGER", nullable: true),
                    AutenticazioneW = table.Column<string>(type: "TEXT", nullable: true),
                    Bruteforce = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloWASCAN_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    CookiesW = table.Column<string>(type: "TEXT", nullable: true),
                    Disclosure = table.Column<bool>(type: "INTEGER", nullable: true),
                    Fingerprint = table.Column<bool>(type: "INTEGER", nullable: true),
                    FullScanW = table.Column<bool>(type: "INTEGER", nullable: true),
                    MethodsW = table.Column<int>(type: "INTEGER", nullable: true),
                    ReagentW = table.Column<bool>(type: "INTEGER", nullable: true),
                    RedirectW = table.Column<bool>(type: "INTEGER", nullable: true),
                    TimeoutW = table.Column<int>(type: "INTEGER", nullable: true),
                    UserAgentW = table.Column<string>(type: "TEXT", nullable: true),
                    VerbositàW = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloWIFITE_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    Wps = table.Column<int>(type: "INTEGER", nullable: true),
                    accessPoint = table.Column<bool>(type: "INTEGER", nullable: true),
                    bully = table.Column<bool>(type: "INTEGER", nullable: true),
                    channel = table.Column<int>(type: "INTEGER", nullable: true),
                    client = table.Column<bool>(type: "INTEGER", nullable: true),
                    crack = table.Column<bool>(type: "INTEGER", nullable: true),
                    fakeAutenticazione = table.Column<bool>(type: "INTEGER", nullable: true),
                    keepIvs = table.Column<bool>(type: "INTEGER", nullable: true),
                    newHs = table.Column<bool>(type: "INTEGER", nullable: true),
                    scanTime = table.Column<int>(type: "INTEGER", nullable: true),
                    verbositaWifite = table.Column<bool>(type: "INTEGER", nullable: true),
                    wep = table.Column<bool>(type: "INTEGER", nullable: true),
                    wpa = table.Column<bool>(type: "INTEGER", nullable: true),
                    wpsSetting = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloWPSCAN_ComandoPersonalizzato = table.Column<string>(type: "TEXT", nullable: true),
                    Cookie = table.Column<string>(type: "TEXT", nullable: true),
                    DisableChecks = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloWPSCAN_Force = table.Column<bool>(type: "INTEGER", nullable: true),
                    RandomAgent = table.Column<bool>(type: "INTEGER", nullable: true),
                    ModuloWPSCAN_UserAgent = table.Column<string>(type: "TEXT", nullable: true),
                    VerbositàWP = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moduli", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Progetti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descrizione = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Target = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progetti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ModuliProgetto",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModuloID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProgettoID = table.Column<int>(type: "INTEGER", nullable: false),
                    Target = table.Column<string>(type: "TEXT", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProgettoID = table.Column<int>(type: "INTEGER", nullable: false),
                    isImportati = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Report_Progetti_ProgettoID",
                        column: x => x.ProgettoID,
                        principalTable: "Progetti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PercorsiReport",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Percorso = table.Column<string>(type: "TEXT", nullable: true),
                    ReportID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PercorsiReport", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PercorsiReport_Report_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Report",
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

            migrationBuilder.CreateIndex(
                name: "IX_PercorsiReport_ReportID",
                table: "PercorsiReport",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProgettoID",
                table: "Report",
                column: "ProgettoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuliProgetto");

            migrationBuilder.DropTable(
                name: "PercorsiReport");

            migrationBuilder.DropTable(
                name: "Moduli");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Progetti");
        }
    }
}
