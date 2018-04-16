
using System.ComponentModel.DataAnnotations;
namespace webmva.Models
{
    public enum TCPSCAN
    {
        SYN, ACK, CONNECTION, WINDOW, MAIMON, NESSUNO
    }
    public enum NONTCPSCAN
    {
        UDP, LIST, NOPORT, NESSUNO
    }
    public enum TEMPI
    { ZERO, UNO, DUE, TRE, QUATTRO, CINQUE}
    
    public class ModuloNMAP : Modulo
    {
        // https://www.stationx.net/nmap-cheat-sheet/

        // VEDIAMO SE SARA' COSI'

        // SWITCH
        private TCPSCAN _tcpscan = TCPSCAN.NESSUNO;
        public TCPSCAN TCPScan
        {
            get { return _tcpscan; }
            set { _tcpscan = value; }
        }
        private NONTCPSCAN _nontcpscan = NONTCPSCAN.NESSUNO;
        public NONTCPSCAN NonTCPScan
        {
            get { return _nontcpscan; }
            set { _nontcpscan = value; }
        }

        // DISCOVERY

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
        private bool _osDet = false;
        public bool OSdetection { get { return _osDet; } set { _osDet = value; } } //-O
        private bool _osAggr = false;
        public bool OSDetectionAggressive { get { return _osAggr; } set { if (OSdetection.Equals(false) && value.Equals(true)) OSdetection = true; _osAggr = value; } } //--osscan-guess dopo O
        private bool _allDet = false;
        public bool AllDetections { get { return _allDet; } set { _allDet = value; } } //-A comprende OS detection, version detection, script scanning e traceroute

        // TIMING
        private TEMPI _tempo = TEMPI.TRE;
        public TEMPI Tempo
        {
            get { return _tempo; }
            set { _tempo = value; }
        } // -Tnum

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
                    switch (TCPScan)
                    {
                        case TCPSCAN.ACK:
                            risultato += " -sA";
                            break;
                        case TCPSCAN.SYN:
                            risultato += " -sS";
                            break;
                        case TCPSCAN.CONNECTION:
                            risultato += " -sT";
                            break;
                        case TCPSCAN.WINDOW:
                            risultato += " -sW";
                            break;
                        case TCPSCAN.MAIMON:
                            risultato += " -sM";
                            break;
                        case TCPSCAN.NESSUNO:
                            break;
                        default:
                            break;
                    }
                    switch (NonTCPScan)
                    {
                        case NONTCPSCAN.LIST:
                            risultato += " -sL";
                            break;
                        case NONTCPSCAN.NOPORT:
                            risultato += " -sn";
                            break;
                        case NONTCPSCAN.UDP:
                            risultato += " -sU";
                            break;
                        case NONTCPSCAN.NESSUNO:
                            break;
                        default: break;
                    }
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
                        if (OSdetection)
                        {
                            risultato += " -O";
                            if (OSDetectionAggressive) risultato += " --osscan-guess";
                        }
                    }
                    if(Tempo != TEMPI.TRE) switch (Tempo) {
                            case TEMPI.ZERO:
                                risultato += " -T0";
                                break;
                            case TEMPI.UNO:
                                risultato += " -T1";
                                break;
                            case TEMPI.DUE:
                                risultato += " -T2";
                                break;
                            case TEMPI.QUATTRO:
                                risultato += " -T4";
                                break;
                            case TEMPI.CINQUE:
                                risultato += " -T5";
                                break;
                            default: break;
                        }
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