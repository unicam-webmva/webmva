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
    private bool _all = true; 
    public bool AllSUB { get {return _all;} set {_all = value;} } 
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
                if(!string.IsNullOrEmpty(PorteSUB))
                risultato += " -p " + PorteSUB;  
                if(VerbositàSUB)
                risultato += " -v";
                if(BruteforceSUB)
                risultato += " -b";
                if (!All)
                    {
                        string appoggio = " -e ";
                        if (BaiduSUB) appoggio += "baidu ";
                        if (YahooSUB) appoggio += "yahoo ";
                        if (GoogleSUB) appoggio += "google ";
                        if (BingSUB) appoggio += "bing ";
                        if (AskSUB) appoggio += "ask ";
                        if (NetcraftSUB) appoggio += "netcraft ";
                        if (DNSdumpsterSUB) appoggio += "dnsdumpster ";
                        if (VirustotalSUB) appoggio += "virustotal ";
                        if (ThreatCrowdSUB) appoggio += "threatcrowd ";
                        if (SSLCertificatesSUB) appoggio += "sslcertificates ";
                        if (PassiveDNSSUB) appoggio += "passivedns ";
                        string[] appoggio2= appoggio.Split(' ');
                        appoggio = "";
                        for(int i=0; i < appoggio2.Length-1; i++){
                            if(i<3)
                            appoggio += " "+ appoggio2[i];
                            else appoggio += "," +appoggio2[i];
                        }
                        risultato +=appoggio;
                        
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