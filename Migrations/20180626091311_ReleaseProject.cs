using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webmva.Migrations
{
    public partial class ReleaseProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moduli",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Applicazione = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    TimeoutDNS = table.Column<int>(nullable: true),
                    DelayDNS = table.Column<int>(nullable: true),
                    PagesDNS = table.Column<int>(nullable: true),
                    ScrapDNS = table.Column<int>(nullable: true),
                    ThreadsDNS = table.Column<int>(nullable: true),
                    VerboseDNS = table.Column<bool>(nullable: true),
                    RecursionDNS = table.Column<bool>(nullable: true),
                    NoReverseDNS = table.Column<bool>(nullable: true),
                    Whois = table.Column<bool>(nullable: true),
                    ComandoPersonalizzato = table.Column<string>(nullable: true),
                    NameServer = table.Column<string>(nullable: true),
                    AXFREnum = table.Column<bool>(nullable: true),
                    ReverseLookupEnum = table.Column<bool>(nullable: true),
                    GoogleEnum = table.Column<bool>(nullable: true),
                    BingEnum = table.Column<bool>(nullable: true),
                    CrtShEnum = table.Column<bool>(nullable: true),
                    DeepWhois = table.Column<bool>(nullable: true),
                    ZoneWalk = table.Column<bool>(nullable: true),
                    ModuloDNSRECON_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Cms = table.Column<int>(nullable: true),
                    Check = table.Column<int>(nullable: true),
                    ModuloDROOPE_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    DnServer = table.Column<string>(nullable: true),
                    SubDomain = table.Column<string>(nullable: true),
                    Connect = table.Column<bool>(nullable: true),
                    Wide = table.Column<bool>(nullable: true),
                    ModuloFIERCE_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Source = table.Column<int>(nullable: true),
                    Verbose = table.Column<int>(nullable: true),
                    ModuloINFOGA_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    Breach = table.Column<bool>(nullable: true),
                    ModuloINFOGAEMAIL_Verbose = table.Column<int>(nullable: true),
                    ModuloINFOGAEMAIL_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloJOOMSCAN_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true),
                    EnumerateComponents = table.Column<bool>(nullable: true),
                    ServerIP = table.Column<string>(nullable: true),
                    Porta = table.Column<int>(nullable: true),
                    TCPScan = table.Column<int>(nullable: true),
                    NonTCPScan = table.Column<int>(nullable: true),
                    NoHostDiscovery = table.Column<bool>(nullable: true),
                    SynDiscoveryPorts = table.Column<string>(nullable: true),
                    AckDiscoveryPorts = table.Column<string>(nullable: true),
                    UdpDiscoveryPorts = table.Column<string>(nullable: true),
                    ArpDiscovery = table.Column<bool>(nullable: true),
                    NoDNSResolution = table.Column<bool>(nullable: true),
                    ListSpecificPort = table.Column<string>(nullable: true),
                    ScanAllPorts = table.Column<bool>(nullable: true),
                    FastScan = table.Column<bool>(nullable: true),
                    ServiceVersion = table.Column<bool>(nullable: true),
                    OSdetection = table.Column<bool>(nullable: true),
                    OSDetectionAggressive = table.Column<bool>(nullable: true),
                    AllDetections = table.Column<bool>(nullable: true),
                    Tempo = table.Column<int>(nullable: true),
                    Fragmented = table.Column<bool>(nullable: true),
                    SendScansFromSpoofedIP = table.Column<string>(nullable: true),
                    IPv6Scan = table.Column<bool>(nullable: true),
                    IncreaseVerbosity = table.Column<bool>(nullable: true),
                    ModuloNMAP_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    VerboseOdat = table.Column<int>(nullable: true),
                    PortaOdat = table.Column<int>(nullable: true),
                    PasswordOdat = table.Column<string>(nullable: true),
                    AllOdat = table.Column<bool>(nullable: true),
                    Tnspoison = table.Column<bool>(nullable: true),
                    Tnscmd = table.Column<bool>(nullable: true),
                    SmbOdat = table.Column<bool>(nullable: true),
                    PasswordStealer = table.Column<bool>(nullable: true),
                    PasswordGuesser = table.Column<bool>(nullable: true),
                    UtenteOdat = table.Column<string>(nullable: true),
                    SID = table.Column<string>(nullable: true),
                    TestOdat = table.Column<bool>(nullable: true),
                    ModuloODAT_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    PortaO = table.Column<int>(nullable: true),
                    RetriesO = table.Column<int>(nullable: true),
                    Metodo = table.Column<string>(nullable: true),
                    DelayO = table.Column<int>(nullable: true),
                    TimeoutO = table.Column<int>(nullable: true),
                    AcceptCookies = table.Column<bool>(nullable: true),
                    ModuloOPENDOOR_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ServerIPOpenvas = table.Column<string>(nullable: true),
                    PortaOpenvas = table.Column<int>(nullable: true),
                    header = table.Column<string>(nullable: true),
                    credenzialiAutenticazione = table.Column<string>(nullable: true),
                    dbms = table.Column<string>(nullable: true),
                    dbmsCredenziali = table.Column<string>(nullable: true),
                    sistemaOperativo = table.Column<string>(nullable: true),
                    forceAggressive = table.Column<bool>(nullable: true),
                    forceSsl = table.Column<bool>(nullable: true),
                    allOptimization = table.Column<bool>(nullable: true),
                    predictOutput = table.Column<bool>(nullable: true),
                    keepAlive = table.Column<bool>(nullable: true),
                    nessunaConnessione = table.Column<bool>(nullable: true),
                    a = table.Column<bool>(nullable: true),
                    b = table.Column<bool>(nullable: true),
                    currentUser = table.Column<bool>(nullable: true),
                    currentDb = table.Column<bool>(nullable: true),
                    hostName = table.Column<bool>(nullable: true),
                    users = table.Column<bool>(nullable: true),
                    passwords = table.Column<bool>(nullable: true),
                    privileges = table.Column<bool>(nullable: true),
                    roles = table.Column<bool>(nullable: true),
                    dbs = table.Column<bool>(nullable: true),
                    tables = table.Column<bool>(nullable: true),
                    columns = table.Column<bool>(nullable: true),
                    schema = table.Column<bool>(nullable: true),
                    count = table.Column<bool>(nullable: true),
                    dumpAll = table.Column<bool>(nullable: true),
                    search = table.Column<bool>(nullable: true),
                    excludesSySdbs = table.Column<bool>(nullable: true),
                    commonTables = table.Column<bool>(nullable: true),
                    commonColumns = table.Column<bool>(nullable: true),
                    delay = table.Column<int>(nullable: true),
                    timeout = table.Column<int>(nullable: true),
                    retries = table.Column<int>(nullable: true),
                    threads = table.Column<int>(nullable: true),
                    timesec = table.Column<int>(nullable: true),
                    tecnique = table.Column<string>(nullable: true),
                    ModuloSQLMAP_Verbose = table.Column<int>(nullable: true),
                    Detection = table.Column<int>(nullable: true),
                    Rischio = table.Column<int>(nullable: true),
                    ModuloSQLMAP_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    PorteSUB = table.Column<string>(nullable: true),
                    VerbositàSUB = table.Column<bool>(nullable: true),
                    BruteforceSUB = table.Column<bool>(nullable: true),
                    AllSUB = table.Column<bool>(nullable: true),
                    BaiduSUB = table.Column<bool>(nullable: true),
                    YahooSUB = table.Column<bool>(nullable: true),
                    GoogleSUB = table.Column<bool>(nullable: true),
                    BingSUB = table.Column<bool>(nullable: true),
                    AskSUB = table.Column<bool>(nullable: true),
                    NetcraftSUB = table.Column<bool>(nullable: true),
                    DNSdumpsterSUB = table.Column<bool>(nullable: true),
                    VirustotalSUB = table.Column<bool>(nullable: true),
                    ThreatCrowdSUB = table.Column<bool>(nullable: true),
                    SSLCertificatesSUB = table.Column<bool>(nullable: true),
                    PassiveDNSSUB = table.Column<bool>(nullable: true),
                    ThreadSUB = table.Column<int>(nullable: true),
                    ModuloSUBLIST3R_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    All = table.Column<bool>(nullable: true),
                    Common = table.Column<bool>(nullable: true),
                    Nessuno = table.Column<bool>(nullable: true),
                    BackUp = table.Column<bool>(nullable: true),
                    BlindSql = table.Column<bool>(nullable: true),
                    Crlf = table.Column<bool>(nullable: true),
                    Exec = table.Column<bool>(nullable: true),
                    File = table.Column<bool>(nullable: true),
                    Htaccess = table.Column<bool>(nullable: true),
                    Nikto = table.Column<bool>(nullable: true),
                    PermanentXss = table.Column<bool>(nullable: true),
                    Sql = table.Column<bool>(nullable: true),
                    Xss = table.Column<bool>(nullable: true),
                    Buster = table.Column<bool>(nullable: true),
                    ShellShock = table.Column<bool>(nullable: true),
                    Scope = table.Column<int>(nullable: true),
                    Force = table.Column<int>(nullable: true),
                    ModuloWAPITI_Verbose = table.Column<int>(nullable: true),
                    MaxMinutes = table.Column<int>(nullable: true),
                    ModuloWAPITI_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloWASCAN_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    AutenticazioneW = table.Column<string>(nullable: true),
                    UserAgentW = table.Column<string>(nullable: true),
                    CookiesW = table.Column<string>(nullable: true),
                    Fingerprint = table.Column<bool>(nullable: true),
                    Attacks = table.Column<bool>(nullable: true),
                    Audit = table.Column<bool>(nullable: true),
                    Bruteforce = table.Column<bool>(nullable: true),
                    Disclosure = table.Column<bool>(nullable: true),
                    FullScanW = table.Column<bool>(nullable: true),
                    ReagentW = table.Column<bool>(nullable: true),
                    VerbositàW = table.Column<bool>(nullable: true),
                    RedirectW = table.Column<bool>(nullable: true),
                    TimeoutW = table.Column<int>(nullable: true),
                    MethodsW = table.Column<int>(nullable: true),
                    client = table.Column<bool>(nullable: true),
                    wep = table.Column<bool>(nullable: true),
                    fakeAutenticazione = table.Column<bool>(nullable: true),
                    keepIvs = table.Column<bool>(nullable: true),
                    newHs = table.Column<bool>(nullable: true),
                    verbositaWifite = table.Column<bool>(nullable: true),
                    wpa = table.Column<bool>(nullable: true),
                    accessPoint = table.Column<bool>(nullable: true),
                    crack = table.Column<bool>(nullable: true),
                    wpsSetting = table.Column<bool>(nullable: true),
                    bully = table.Column<bool>(nullable: true),
                    scanTime = table.Column<int>(nullable: true),
                    channel = table.Column<int>(nullable: true),
                    Wps = table.Column<int>(nullable: true),
                    ModuloWIFITE_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloWPSCAN_ComandoPersonalizzato = table.Column<string>(nullable: true),
                    ModuloWPSCAN_Force = table.Column<bool>(nullable: true),
                    RandomAgent = table.Column<bool>(nullable: true),
                    VerbositàWP = table.Column<bool>(nullable: true),
                    DisableChecks = table.Column<bool>(nullable: true),
                    ModuloWPSCAN_UserAgent = table.Column<string>(nullable: true),
                    Cookie = table.Column<string>(nullable: true)
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
                    Nome = table.Column<string>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    Descrizione = table.Column<string>(nullable: true)
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
                    Target = table.Column<string>(nullable: true)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(nullable: false),
                    ProgettoID = table.Column<int>(nullable: false),
                    isImportati = table.Column<bool>(nullable: false)
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
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportID = table.Column<int>(nullable: false),
                    Percorso = table.Column<string>(nullable: true)
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
