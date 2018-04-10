using System;
namespace webmva.Models
{
    public class ModuloNMAP : Modulo
    {
        // https://www.stationx.net/nmap-cheat-sheet/

        // VEDIAMO SE SARA' COSI'

        // SWITCH
        private bool _syn = false;
        public bool SYNScan { get { return _syn; } set { _syn = value; } } //-sS
        private bool _tcp = false;
        public bool TCPConnectScan { get { return _tcp; } set { _tcp = value; } } //-sT
        private bool _udp = false;
        public bool UDPScan { get { return _udp; } set { _udp = value; } } //-sU
        private bool _ack = false;
        public bool ACKScan { get { return _ack; } set { _ack = value; } } //-sA
        private bool _window = false;
        public bool WindowPortScan { get { return _window; } set { _window = value; } } //-sW
        private bool _maimon = false;
        public bool MaimonScan { get { return _maimon; } set { _maimon = value; } } //-sM

        // DISCOVERY

        private bool _noScan = false;
        public bool NoScan { get { return _noScan; } set { _noScan = value; } } //-sL
        private bool _noPortScan = false;
        public bool NoPortScan { get { return _noPortScan; } set { _noPortScan = value; } } //-sn
        private bool _noHost = false;
        public bool NoHostDiscovery { get { return _noHost; } set { _noHost = value; } } //-Pn
        public string SynDiscoveryPorts { get; set; } //-PSlista_porte
        public string AckDiscoveryPorts { get; set; } //-PAlista_porte
        public string UdpDiscoveryPorts { get; set; } //-PUlista_porte
        private bool _arp = false;
        public bool ArpDiscovery { get { return _arp; } set { _arp = value; } } //-PR
        private bool _noDNS = false;
        public bool NoDNSResolution { get { return _noDNS; } set { _noDNS = value; } } //-n

        // PORTE
        public string ListSpecificPort { get; set; } //-p list, può essere range, comma-separated o singola, con U: e T: per UDP e TCP
        private bool _all = false;
        public bool ScanAllPorts { get { return _all; } set { _all = value; } } //-, da mettere dopo -p
        private bool _fast = false;
        public bool FastScan { get { return _fast; } set { _fast = value; } } //-F

        // SERVIZI E VERSIONI
        private bool _version = false;
        public bool ServiceVersion { get { return _version; } set { _version = value; } } //-sV
        private int _serviceVerInt = -1;
        public int ServiceVersionIntensity
        {
            get
            {
                return _serviceVerInt;
            }
            set
            {
                if (value == -1) _serviceVerInt = value;
                else
                {
                    if (ServiceVersion.Equals(false)) ServiceVersion = true;
                    if (value > 9 || value < 0) throw new ArgumentOutOfRangeException("value",
                                  "Il valore di ServiceVersionIntensity deve essere compreso tra 0 e 9");
                    _serviceVerInt = value;
                }
            }
        } //--version-intensity NUM (da 0 a 9)
        private bool _osDet = false;
        public bool OSdetection { get { return _osDet; } set { _osDet = value; } } //-O
        private bool _osAggr = false;
        public bool OSDetectionAggressive { get { return _osAggr; } set { if (OSdetection.Equals(false)) OSdetection = true; _osAggr = value; } } //--osscan-guess dopo O
        private bool _allDet = false;
        public bool AllDetections { get { return _allDet; } set { _allDet = value; } } //-A comprende OS detection, version detection, script scanning e traceroute

        // TIMING
        private int _velocita = 3; //default per nmap
        public int Velocita
        {
            get
            {
                return _velocita;
            }
            set
            {
                if (value > 5 || value < 0) throw new ArgumentOutOfRangeException("value",
                                  "Il valore di Velocita deve essere compreso tra 0 e 5");
                _velocita = value;
            }
        } //-TNum (Num da 0 a 5)

        // EVASIONI DAI FIREWALL/IDS
        private bool _fragmented = false;
        public bool Fragmented { get { return _fragmented; } set { _fragmented = value; } } //-f
        public string SendScansFromSpoofedIP { get; set; } //-D ip.in.dot.dec,altroIp.in.dot.dec

        // VARIE
        private bool _ipv6 = false;
        public bool IPv6Scan { get { return _ipv6; } set { _ipv6 = value; } } //-6
        private bool _verbose = false;
        public bool IncreaseVerbosity { get { return _verbose; } set { _verbose = value; } } //-vv


        // COMANDO PERSONALIZZATO TOTALMENTE
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                string risultato = "nmap ";
                if (ComandoPersonalizzato == "" || string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    //Costruisco il comando
                    if (SYNScan) risultato += " -sS";
                    if (TCPConnectScan) risultato += " -sT";
                    if (UDPScan) risultato += " -sU";
                    if (ACKScan) risultato += " -sA";
                    if (WindowPortScan) risultato += " -sW";
                    if (MaimonScan) risultato += " -sM";
                    if (NoScan) risultato += " -sL";
                    if (NoPortScan) risultato += " -sn";
                    if (!string.IsNullOrEmpty(SynDiscoveryPorts)) risultato += " -PS" + SynDiscoveryPorts;
                    if (!string.IsNullOrEmpty(AckDiscoveryPorts)) risultato += " -PA" + AckDiscoveryPorts;
                    if (!string.IsNullOrEmpty(UdpDiscoveryPorts)) risultato += " -PU" + UdpDiscoveryPorts;
                    if (ArpDiscovery) risultato += " -PR";
                    if (NoDNSResolution) risultato += " -n";
                    if (ScanAllPorts) risultato += " -p-";
                    else if (!string.IsNullOrEmpty(ListSpecificPort)) risultato += " -p " + ListSpecificPort;
                    if (FastScan) risultato += " -F";
                    if (AllDetections) risultato += " -A";
                    else
                    {
                        if (ServiceVersion) risultato += " -sV";
                        if (ServiceVersionIntensity != -1) risultato += " --version-intensity " + ServiceVersionIntensity;
                        if (OSdetection) risultato += " -O";
                        if (OSDetectionAggressive) risultato += " --ososcan-guess";
                    }
                    if (Velocita != 3) risultato += " -T" + Velocita;
                    if (Fragmented) risultato += " -f";
                    if (!string.IsNullOrEmpty(SendScansFromSpoofedIP)) risultato += "-D " + SendScansFromSpoofedIP;
                    if (IPv6Scan) risultato += " -6";
                    if (IncreaseVerbosity) risultato += " -vv";
                    ComandoPersonalizzato = "";
                    return risultato;
                }

                else
                {
                    return ComandoPersonalizzato;
                }
            }
        }
    }
}