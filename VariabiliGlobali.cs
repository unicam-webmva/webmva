using System;
using System.IO;

namespace webmva
{
    public static class Globals
    {
        public static readonly PlatformID SistemaOperativoAttuale = Environment.OSVersion.Platform;
        public static readonly string CartellaTuttiProgetti = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "webmvaProjects");

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
    }
}