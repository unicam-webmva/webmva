using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloSUBLIST3R : Modulo
    {    
    public string PorteSUB{get;set;}
    public bool VerbositàSUB{get;set;} 
    public bool BruteforceSUB{get;set;} 
    public bool All { get; set; } 
    public bool BaiduSUB{get;set;}
    public bool YahooSUB{get;set;}
    public bool GoogleSUB{get;set;}
    public bool BingSUB{get;set;}
    public bool AskSUB{get;set;}
    public bool NetcraftSUB{get;set;}
    public bool DNSdumpsterSUB{get;set;}
    public bool VirustotalSUB{get;set;}
    public bool ThreatCrowdSUB{get;set;}
    public bool SSLCertificatesSUB{get;set;}
    public bool PassiveDNSSUB{get;set;}
    public int ThreadSUB{get;set;}
    public string ComandoPersonalizzato { get; set; }
    public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    
                    string risultato ="sublist3r.py" ;
                if(string.IsNullOrEmpty(PorteSUB))
                risultato += " -p " + PorteSUB;  
                if(VerbositàSUB)
                risultato += " -v";
                if(BruteforceSUB)
                risultato += " -b";
                if (!All)
                    {
                        risultato += " -e ";
                        if (BaiduSUB) risultato += ",baidu";
                        if (YahooSUB) risultato += ",yahoo";
                        if (GoogleSUB) risultato += ",google";
                        if (BingSUB) risultato += ",bing";
                        if (AskSUB) risultato += ",ask";
                        if (NetcraftSUB) risultato += ",netcraft";
                        if (DNSdumpsterSUB) risultato += ",dnsdumpster";
                        if (VirustotalSUB) risultato += ",virustotal";
                        if (ThreatCrowdSUB) risultato += ",threatcrowd";
                        if (SSLCertificatesSUB) risultato += ",sslcertificates";
                        if (PassiveDNSSUB) risultato += ",passivedns";
                        
                        risultato += " ";
                    }
                    if (ThreadSUB != 0)
                    {
                        risultato += "-t " + ThreadSUB;
                    }

                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}