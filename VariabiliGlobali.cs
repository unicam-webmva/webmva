using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using webmva.Helpers;

namespace webmva
{
    public static class Globals
    {
        public static readonly PlatformID SistemaOperativoAttuale = Environment.OSVersion.Platform;
        public static readonly string CartellaTuttiProgetti = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "webmvaProjects");
        public static string CartellaWEBMVA;


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
            Dictionary<string,string> varsPdf = GetPercorsoPDF(percorsoXMLAssoluto);
            string percorsoPdf = varsPdf.GetValueOrDefault("dove");
            string cosa = varsPdf.GetValueOrDefault("cosa");

            string comando = $"fop -xml {percorsoXMLAssoluto} -xsl {cosa}-fo.xsl -pdf {percorsoPdf}";
            comando.EseguiCLI(Path.Combine(CartellaWEBMVA, "Script", "fop"));
            return percorsoPdf;
        }

        private static Dictionary<string, string> GetPercorsoPDF(string percorsoXML){
            string percorsoPdf = percorsoXML.Replace(".xml",".pdf");
            // cerco di evitare un caso in cui il nome del modulo è nmap o dnsrecon anche se non è davvero quello il software usato, da pazzi ma vabbè
            string nomeFile = Path.GetFileName(percorsoPdf);
            string software="";
            if(nomeFile.Substring(nomeFile.IndexOf('_'),nomeFile.LastIndexOf('_')).Contains("nmap")) software="nmap";
            else if(nomeFile.Substring(nomeFile.IndexOf('_'),nomeFile.LastIndexOf('_')).Contains("dnsrecon")) software="dnsrecon";
            return new Dictionary<string,string>{
                {"dove", percorsoPdf},
                {"cosa", software}
            };
        }

        public static string CreaCartellaProgetto(string cartellaProgetto, DateTime data)
        {
            // mi assicuro che la cartella dedicata al progetto esista
            // altrimenti la creo
            string cartellaAssoluta = Path.Combine(CartellaWEBMVA, "wwwroot", "Report", cartellaProgetto);
            if (!Directory.Exists(cartellaAssoluta))
                Directory.CreateDirectory(cartellaAssoluta);
            string cartellaConData = Path.Combine(cartellaAssoluta, data.ToString("dd-MM-yyyy_HH-mm"));
            if (!Directory.Exists(cartellaConData))
                Directory.CreateDirectory(cartellaConData);
            return cartellaConData;
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

    }
}