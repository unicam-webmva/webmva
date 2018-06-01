using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace webmva.Models
{
    public class ModuloDNSENUM : Modulo{
    
       private int _timeoutDNS = 10;
       public int TimeoutDNS{
           get{return _timeoutDNS;}
           set{
               _timeoutDNS = value;
           }
       }
       private int _delayDNS = 3;
       public int DelayDNS{
           get{return _delayDNS;}
           set{
               _delayDNS = value;
           }
       }
       private int _pagesDNS = 5;
       public int PagesDNS{
           get{return _pagesDNS;}
           set{
               _pagesDNS = value;
           }
       }
       private int _scrapDNS = 15;
       public int ScrapDNS{
           get{return _scrapDNS;}
           set{
               _scrapDNS = value;
           }
       }
       public int ThreadsDNS{get;set;}
       public bool VerboseDNS{get;set;}
       public bool RecursionDNS {get;set;}
       public bool NoReverseDNS {get;set;}
       public bool Whois{get;set;}
        public string ComandoPersonalizzato { get; set; }
        public override string Comando
        {
            get
            {
                if (string.IsNullOrEmpty(ComandoPersonalizzato))
                {
                    string risultato ="dnsenum.pl";
                    if (TimeoutDNS != 10 && TimeoutDNS> 0) 
                    risultato += " -t" + TimeoutDNS;
                    if (DelayDNS != 3 && DelayDNS> 0) 
                    risultato += " -d" + DelayDNS;
                    if (PagesDNS != 5 && PagesDNS> 0) 
                    risultato += " -p" + PagesDNS;
                    if (ScrapDNS != 15 && ScrapDNS> 0) 
                    risultato += " -s" + ScrapDNS;
                    if (ThreadsDNS> 0) 
                    risultato += " --threads" + ThreadsDNS;
                    if (VerboseDNS) 
                    risultato += " -v";
                    if (RecursionDNS) 
                    risultato += " -r";
                    if (Whois) 
                    risultato += " -w";
                     if (NoReverseDNS) 
                    risultato += " -noreverse";
                    
                    return risultato;
                }
                else return ComandoPersonalizzato;
            }

        }
    }
}