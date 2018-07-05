using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp;
using webmva.Helpers;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using Shark.PdfConvert;
using Microsoft.EntityFrameworkCore;
using webmva.Data;
using webmva.Models;
using Microsoft.Extensions.Configuration;

namespace webmva
{
    public static class Globals
    {
        public static readonly PlatformID SistemaOperativoAttuale = Environment.OSVersion.Platform;
        public static string CartellaWEBMVA;
        public static string CARTELLALOG;
        public static int PORTA;
        public static bool LOGGING;
        public static string LOGFILE;
        public static string CARTELLAREPORT;
        public static string TIPODB; 
        public static string CONNECTIONSTRING;

        
        public static void CaricaFileConfig(IConfiguration config, string cartellaCorrente)
        {
            if(string.IsNullOrEmpty(cartellaCorrente)) CartellaWEBMVA = Directory.GetCurrentDirectory();
            else CartellaWEBMVA = cartellaCorrente;
            if(config!=null)
            {
                PORTA = config.GetValue<int>("Porta");
                if (PORTA == 0) PORTA=5000;
            
                var tmpReportDir=config.GetValue<string>("CartellaReport");
                if (string.IsNullOrEmpty(tmpReportDir))
                    // Non è stato fornito un percorso, default:
                    CARTELLAREPORT=Path.Combine(CartellaWEBMVA,"wwwroot", "Report");
                else if (tmpReportDir.ElementAt(0).Equals(Path.DirectorySeparatorChar))
                    // il percorso fornito è assoluto, lo metto tale e quale
                    CARTELLAREPORT=tmpReportDir;
                else
                    // il percorso lo intendo relativo alla root di WebMVA
                    CARTELLAREPORT=Path.Combine(CartellaWEBMVA, tmpReportDir);

                var tmpLogDir=config.GetValue<string>("CartellaLog");
                if (string.IsNullOrEmpty(tmpLogDir))
                    // Non è stato fornito un percorso, default:
                    CARTELLALOG=Path.Combine(CartellaWEBMVA,"wwwroot", "Log");
                else if (tmpLogDir.ElementAt(0).Equals(Path.DirectorySeparatorChar))
                    // il percorso fornito è assoluto, lo metto tale e quale
                    CARTELLALOG=tmpLogDir;
                else
                    // il percorso lo intendo relativo alla root di WebMVA
                    CARTELLALOG=Path.Combine(CartellaWEBMVA, tmpLogDir);
            
                var tmpLog =config.GetValue<string>("Log");
                if(!bool.TryParse(tmpLog, out LOGGING)) LOGGING=false;

                TIPODB=config.GetValue<string>("TipoDB");
                if (string.IsNullOrEmpty(TIPODB)) TIPODB="sqlite";

                CONNECTIONSTRING=config.GetValue<string>("ConnectionString");
                if (string.IsNullOrEmpty(CONNECTIONSTRING)) CONNECTIONSTRING="Data Source=webmva.db";    
            }
            else
            {
                // metto valori di default
                PORTA=5000;
                CARTELLAREPORT=Path.Combine(CartellaWEBMVA, "wwwroot", "Report");
                CARTELLALOG=Path.Combine(CartellaWEBMVA, "wwwroot", "Log");
                LOGGING=false;
                TIPODB="sqlite";
                CONNECTIONSTRING="Data Source=webmva.db";
            }
            CreaCartellaLog();
            CreaCartellaReport();
        }
        internal static void CreaCartellaLog()
        {
            // mi assicuro che la cartella log esista, altrimenti la creo
            if (!Directory.Exists(CARTELLALOG))
                Directory.CreateDirectory(CARTELLALOG);
            if(LOGGING)
            { // creo un file vuoto chiamato TIMESTAMP_webmva.log
                string nomeFile = Path.Combine(CARTELLALOG, $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_webmva.log");
                File.Create(nomeFile).Dispose();
                LOGFILE=nomeFile;
            }
        }
        internal static void CreaCartellaReport()
        {
            // mi assicuro che la cartella log esista, altrimenti la creo
            if (!Directory.Exists(CARTELLAREPORT))
                Directory.CreateDirectory(CARTELLAREPORT);
        }


        /// <summary>
        /// Questo metodo converte una stringa con spazi in CamelCase.
        /// </summary>
        /// <param name="stringa">La stringa da convertire.</param>
        /// <returns>La stringa convertita in CamelCase.</returns>
        public static string ToCamelCase(this string stringa)
        {
            // controllo se stringa è lunga almeno tre caratteri
            if (stringa == null || stringa.Length < 2) return stringa;
            // creo un array di parole, spezzando la stringa ad ogni spazio
            string[] parole = stringa.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
            // rendo la prima parola tutta minuscola
            string camelCase = parole[0].ToLower();
            // per ogni parola successiva alla prima
            for (int i = 1; i < parole.Length; i++)
            {
                // aggiungo alla stringa di ritorno le parole successive, col primo carattere maiuscolo
                camelCase += parole[i].Substring(0, 1).ToUpper() + parole[i].Substring(1);
            }
            return camelCase;
        }

        /// <summary>
        /// Questo metodo accorpa più documenti PDF in uno solo.
        /// Riferimenti web:
        /// https://stackoverflow.com/questions/808670/combine-two-or-more-pdfs
        /// https://www.nuget.org/packages/PdfSharp/1.50.4845-RC2a
        /// https://www.nuget.org/packages/System.Text.Encoding.CodePages/4.4.0
        /// </summary>
        /// <param name="destinazione">Il percorso, completo di nome del file, dove salvare il risultato delle operazioni.</param>
        /// <param name="pdfDaAccorpare">Una lista di percorsi, completi di nomi dei file, dei PDF da accorpare.</param>
        /// <returns>True se riesce ad esportare, false altrimenti.</returns>
        public static bool MergePDF(string destinazione, List<string> pdfDaAccorpare)
        {
            if (string.IsNullOrEmpty(destinazione) || pdfDaAccorpare == null || pdfDaAccorpare.Count == 0)
                return false;
            using (PdfDocument targetDoc = new PdfDocument())
            {
                foreach (string elemento in pdfDaAccorpare)
                {
                    using (PdfDocument pdfDoc = PdfReader.Open(elemento, PdfDocumentOpenMode.Import))
                    {
                        for (int i = 0; i < pdfDoc.PageCount; i++)
                        {
                            targetDoc.AddPage(pdfDoc.Pages[i]);
                        }
                    }
                }
                targetDoc.Save(destinazione);
            }
            return true;
        }

        public static string ConvertiReportXML(string percorsoXMLAssoluto)
        {
            if (string.IsNullOrEmpty(percorsoXMLAssoluto)) return string.Empty;
            Dictionary<string, string> varsPdf = GetPercorsoPDF(percorsoXMLAssoluto);
            string percorsoPdf = varsPdf.GetValueOrDefault("dove");
            string cosa = varsPdf.GetValueOrDefault("cosa");
            if (cosa.Equals("nmap"))
            {
                // Tolgo la dicitura DOCTYPE perché sennò FOP sclera male
                System.IO.File.WriteAllLines(percorsoXMLAssoluto,
                    System.IO.File.ReadLines(percorsoXMLAssoluto)
                        .Where(l => !l.Contains("<!DOCTYPE")).ToList());
            }
            string comando = $"fop -xml {percorsoXMLAssoluto} -xsl {cosa}-fo.xsl -pdf {percorsoPdf}";
            comando.EseguiCLI(Path.Combine(CartellaWEBMVA, "Script", "fop"));
            return percorsoPdf;
        }

        private static Dictionary<string, string> GetPercorsoPDF(string percorsoXML)
        {
            string percorsoPdf = percorsoXML.Replace(".xml", ".pdf");
            // cerco di evitare un caso in cui il nome del modulo è nmap o dnsrecon anche se non è davvero quello il software usato, da pazzi ma vabbè
            string nomeFile = Path.GetFileName(percorsoPdf);
            string software = "";
            if (nomeFile.Substring(nomeFile.IndexOf('_'), nomeFile.LastIndexOf('_')).Contains("nmap")) software = "nmap";
            else if (nomeFile.Substring(nomeFile.IndexOf('_'), nomeFile.LastIndexOf('_')).Contains("dnsrecon")) software = "dnsrecon";
            else if (nomeFile.Substring(nomeFile.IndexOf('_'), nomeFile.LastIndexOf('_')).Contains("dnsenum")) software = "dnsenum";
            return new Dictionary<string, string>{
                {"dove", percorsoPdf},
                {"cosa", software}
            };
        }

        internal static string ConvertiReportTXT(string percorsoTXTAssoluto)
        {
            if (string.IsNullOrEmpty(percorsoTXTAssoluto)) return string.Empty;
            var extension = Path.GetExtension(percorsoTXTAssoluto).ToLower();
            string content = System.IO.File.ReadAllText(percorsoTXTAssoluto);
            string[] pezzi = Path.GetFileName(percorsoTXTAssoluto).Split('_');
            string data = DateTime.ParseExact(pezzi[0] + pezzi[1], "yyyyMMddHHmmss",
                                       System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMMM yyyy HH:mm:ss");
            
            string percorsoPdf = percorsoTXTAssoluto.Substring(0, percorsoTXTAssoluto.LastIndexOf('.')) + ".pdf";
            string comando = "wkhtmltopdf " + percorsoTXTAssoluto + " " + percorsoPdf;
            comando.EseguiCLI(Globals.CartellaWEBMVA);
            return percorsoPdf;
        }

        public static string CreaCartellaProgetto(string cartellaProgetto)
        {
            // mi assicuro che la cartella dedicata al progetto esista
            // altrimenti la creo
            string cartellaAssoluta = Path.Combine(CARTELLAREPORT, cartellaProgetto);
            if (!Directory.Exists(cartellaAssoluta))
                Directory.CreateDirectory(cartellaAssoluta);
            return cartellaAssoluta;
        }
        public static string CreaCartellaScan(string cartellaProgetto, DateTime data)
        {
            // mi assicuro che la cartella dedicata al progetto esista
            // altrimenti la creo
            string cartellaAssoluta = CreaCartellaProgetto(cartellaProgetto);
            string cartellaConData = Path.Combine(cartellaAssoluta, data.ToString("dd-MM-yyyy_HH-mm"));
            if (!Directory.Exists(cartellaConData))
                Directory.CreateDirectory(cartellaConData);
            return cartellaConData;
        }
        public static string CreaCartellaImportati(string cartellaProgetto)
        {
            // mi assicuro che la cartella dedicata al progetto esista
            // altrimenti la creo
            string cartellaAssoluta = Path.Combine(CARTELLAREPORT, cartellaProgetto);
            if (!Directory.Exists(cartellaAssoluta))
                Directory.CreateDirectory(cartellaAssoluta);
            string cartellaImportati = Path.Combine(cartellaAssoluta, "Importati");
            if (!Directory.Exists(cartellaImportati))
                Directory.CreateDirectory(cartellaImportati);
            return cartellaImportati;
        }
        // https://stackoverflow.com/questions/34638823/store-complex-object-in-tempdata
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
        public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o = tempData.Peek(key);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

        public static void AggiungiIntestazione(string percorso, MyDbContext context)
        {
            if (string.IsNullOrEmpty(percorso)) return;
            List<string> content = System.IO.File.ReadLines(Path.Combine("wwwroot", "Report", percorso)).ToList();
            var progetto = percorso.Split(Path.DirectorySeparatorChar).ElementAt(0);
            Progetto p = context.Progetti
                .Include(x=>x.ModuliProgetto)
                    .ThenInclude(m=>m.Modulo)
                .SingleOrDefault(s=>s.Nome == progetto);
            var filename = Path.GetFileName(percorso).Split('_');
            var nomeModulo = filename[3].Substring(0, filename[3].LastIndexOf('.'));
            var data = DateTime
                .ParseExact(filename[0] + filename[1],
                    "yyyyMMddHHmmss",
                    System.Globalization.CultureInfo.InvariantCulture)
                .ToString("dd MMMM yyyy alle HH:mm:ss");
            var associazione = p.ModuliProgetto.SingleOrDefault(x=>x.Modulo.Nome.ToCamelCase() == nomeModulo && x.Modulo.Applicazione.ToString().ToLower() == filename[2]);
            var comando = associazione.Modulo.Comando;
            var target = associazione.Target;
            List<string> intestazione = new List<string>();

            if (Path.GetExtension(percorso).Equals(".txt"))
            {
                if (filename[2].Equals("infoga") || filename[2].Equals("infogaemail"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@"  _____        __                  
 |_   _|      / _|                 
   | |  _ __ | |_ ___   __ _  __ _ 
   | | | '_ \|  _/ _ \ / _` |/ _` |
  _| |_| | | | || (_) | (_| | (_| |
 |_____|_| |_|_| \___/ \__, |\__,_|
                        __/ |      
                       |___/      ");
                }
                else if (filename[2].Equals("sublist3r"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@"   _____       _     _ _     _   ____       
  / ____|     | |   | (_)   | | |___ \      
 | (___  _   _| |__ | |_ ___| |_  __) |_ __ 
  \___ \| | | | '_ \| | / __| __||__ <| '__|
  ____) | |_| | |_) | | \__ \ |_ ___) | |   
 |_____/ \__,_|_.__/|_|_|___/\__|____/|_|   ");
                }
                else if (filename[2].Equals("wapiti"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@" __          __         _ _   _ 
 \ \        / /        (_) | (_)
  \ \  /\  / /_ _ _ __  _| |_ _ 
   \ \/  \/ / _` | '_ \| | __| |
    \  /\  / (_| | |_) | | |_| |
     \/  \/ \__,_| .__/|_|\__|_|
                 | |            
                 |_|            ");
                }
                else if (filename[2].Equals("opendoor"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@"   ____                   _____                   
  / __ \                 |  __ \                  
 | |  | |_ __   ___ _ __ | |  | | ___   ___  _ __ 
 | |  | | '_ \ / _ \ '_ \| |  | |/ _ \ / _ \| '__|
 | |__| | |_) |  __/ | | | |__| | (_) | (_) | |   
  \____/| .__/ \___|_| |_|_____/ \___/ \___/|_|   
        | |                                       
        |_|                                      ");
                }
                else if (filename[2].Equals("sqlmap"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@"   _____  ____  _      __  __             
  / ____|/ __ \| |    |  \/  |            
 | (___ | |  | | |    | \  / | __ _ _ __  
  \___ \| |  | | |    | |\/| |/ _` | '_ \ 
  ____) | |__| | |____| |  | | (_| | |_) |
 |_____/ \___\_\______|_|  |_|\__,_| .__/ 
                                   | |    
                                   |_|   ");
                }
                else if (filename[2].Equals("joomscan"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@"       _                        _____                 
      | |                      / ____|                
      | | ___   ___  _ __ ___ | (___   ___ __ _ _ __  
  _   | |/ _ \ / _ \| '_ ` _ \ \___ \ / __/ _` | '_ \ 
 | |__| | (_) | (_) | | | | | |____) | (_| (_| | | | |
  \____/ \___/ \___/|_| |_| |_|_____/ \___\__,_|_| |_|");
                }
                else if (filename[2].Equals("wpscan"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@" __          _______   _____                 
 \ \        / /  __ \ / ____|                
  \ \  /\  / /| |__) | (___   ___ __ _ _ __  
   \ \/  \/ / |  ___/ \___ \ / __/ _` | '_ \ 
    \  /\  /  | |     ____) | (_| (_| | | | |
     \/  \/   |_|    |_____/ \___\__,_|_| |_| ");
                }
                else if (filename[2].Equals("wascan"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@" __          __      _____                 
 \ \        / /\    / ____|                
  \ \  /\  / /  \  | (___   ___ __ _ _ __  
   \ \/  \/ / /\ \  \___ \ / __/ _` | '_ \ 
    \  /\  / ____ \ ____) | (_| (_| | | | |
     \/  \/_/    \_\_____/ \___\__,_|_| |_|");
                }
                else if (filename[2].Equals("odat"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@"   ____  _____       _______ 
  / __ \|  __ \   /\|__   __|
 | |  | | |  | | /  \  | |   
 | |  | | |  | |/ /\ \ | |   
 | |__| | |__| / ____ \| |   
  \____/|_____/_/    \_\_|   ");
                }
                else if (filename[2].Equals("droope"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@"  _____                                                          ");
                    intestazione.Add(@" |  __ \                                                         ");
                    intestazione.Add(@" | |  | | _ __  ___    ___   _ __    ___  ___   ___  __ _  _ __  ");
                    intestazione.Add(@" | |  | || '__|/ _ \  / _ \ | '_ \  / _ \/ __| / __|/ _` || '_ \ ");
                    intestazione.Add(@" | |__| || |  | (_) || (_) || |_) ||  __/\__ \| (__| (_| || | | |");
                    intestazione.Add(@" |_____/ |_|   \___/  \___/ | .__/  \___||___/ \___|\__,_||_| |_|");
                    intestazione.Add(@"                            | |                                  ");
                    intestazione.Add(@"                            |_|                                  ");
                }
                else if (filename[2].Equals("fierce"))
                {
                    intestazione.Add(@"___________________________________________________________________");
                    intestazione.Add(@"          ______ _                   ");
                    intestazione.Add(@"         |  ____(_)                  ");
                    intestazione.Add(@"         | |__   _  ___ _ __ ___ ___ ");
                    intestazione.Add(@"         |  __| | |/ _ \ '__/ __/ _ \");
                    intestazione.Add(@"         | |    | |  __/ | | (_|  __/");
                    intestazione.Add(@"         |_|    |_|\___|_|  \___\___|");
                }
                else if(filename[2].Equals("amass"))
                {
                    intestazione.Add(@"     /\                            
    /  \   _ __ ___   __ _ ___ ___ 
   / /\ \ | '_ ` _ \ / _` / __/ __|
  / ____ \| | | | | | (_| \__ \__ \
 /_/    \_\_| |_| |_|\__,_|___/___/");

                }
                else if(filename[2].Equals("drupwn"))
                {
                    intestazione.Add(@"  _____             _______          ___   _ 
 |  __ \           |  __ \ \        / / \ | |
 | |  | |_ __ _   _| |__) \ \  /\  / /|  \| |
 | |  | | '__| | | |  ___/ \ \/  \/ / | . ` |
 | |__| | |  | |_| | |      \  /\  /  | |\  |
 |_____/|_|   \__,_|_|       \/  \/   |_| \_|");
                }

                intestazione.Add(@"___________________________________________________________________");
                intestazione.Add("");
                intestazione.Add(@" Progetto: " + progetto);
                intestazione.Add(@" Nome Modulo: " + nomeModulo);
                intestazione.Add(@" Comando eseguito: " + comando);
                intestazione.Add(@" Target: " + target);
                intestazione.Add("");
                intestazione.Add("\tScan iniziato il " + data);
                intestazione.Add(@"___________________________________________________________________");
                intestazione.Add("");
                intestazione.Add("");


                for (int i = 0; i < intestazione.Count; i++)
                {
                    content.Insert(i, intestazione.ElementAt(i));
                }
            }
            else if (Path.GetExtension(percorso).Equals(".xml"))
            {
                int riga = 0;
                if(filename[2].Equals("nmap")){
                    riga = content.IndexOf("</nmaprun>");
                }
                else if(filename[2].Equals("dnsrecon")){
                    riga = content.IndexOf("</records>");
                }
                else if(filename[2].Equals("dnsenum")){
                    int r = content.FindIndex(x=>x.Contains("</testdata>"));
                    if(r == 1){
                        int index = content.ElementAt(r).Substring(0,content.ElementAt(r).LastIndexOf("/")).LastIndexOf("/")-1;
                        int lunghezza = content.ElementAt(r).Length;
                        string pezzoDaMettereInNuovaRiga = content.ElementAt(r).Substring(index, lunghezza-index);
                        string rigaNuova = content.ElementAt(r).Substring(0, index);
                        content.RemoveAt(r);
                        content.Add(rigaNuova);
                        content.Add("");
                        content.Add("");
                        content.Add(pezzoDaMettereInNuovaRiga);
                        riga = r +1;
                    }
                    else riga = r;
                    
                }
                content.Insert(riga, $@"<webmva>
                <scaninfo>
                <progetto>{progetto}</progetto>
                <modulo>{nomeModulo}</modulo>
                <comando>{comando}</comando>
                <target>{target}</target>
                <data>{data}</data>
                </scaninfo>
                </webmva>");
            }
            else if(Path.GetExtension(percorso).Equals(".html")){
                if(filename[2].Equals("theharvester")){
                    int r = content.FindIndex(x=>x.Contains("<body>"));
                        string appoggio = $@"<body>
                        <br />
                        <h3>Progetto: {progetto}<br />
                        Modulo: {nomeModulo}<br />
                        Comando: {comando}<br />
                        Target: {target}<br />
                        Data: {data}</h3>
                        <h1>";
                        string rigaNuova = content.ElementAt(r).Replace("<body><h1>",appoggio);
                        
                        content.RemoveAt(r);
                        content.Add(rigaNuova);
                }
            }
            System.IO.File.WriteAllLines(Path.Combine("wwwroot", "Report", percorso), content);

        }
    }
}