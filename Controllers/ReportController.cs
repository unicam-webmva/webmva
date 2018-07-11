using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webmva.Data;
using webmva.Models;
using webmva.ViewModels;
using webmva.Helpers;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;

namespace webmva.Controllers
{
    public class ReportController : Controller
    {
        private readonly MyDbContext _context;

        public ReportController(MyDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            MyLogger.Log(messaggio: $"Richiesta GET", controller: "ReportController", metodo: "Index");
            return View(await _context.Progetti
            .Include(x => x.ListaReport)
            .AsNoTracking()
            .ToListAsync());
        }
        public async Task<IActionResult> Lista(int? id)
        {
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ReportController", metodo: "Lista");
                return NotFound();
            }
            var lista = await _context.Report
                .Where(m => m.ProgettoID == id)
                .Include(p => p.Progetto)
                .AsNoTracking()
                .ToListAsync();
            if (lista == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun progetto con questo id", controller: "ReportController", metodo: "Lista");
            }
            ViewData["Progetto"] = _context.Progetti.SingleOrDefault(x => x.ID == id);
            MyLogger.Log(messaggio: $"Richiesta GET con id progetto {id}", controller: "ReportController", metodo: "Lista");
            return View(lista);
        }
        public async Task<IActionResult> Tutti(int? id)
        {
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ReportController", metodo: "Tutti");
                return NotFound();
            }
            var lista = await _context.Report
                .Where(m => m.ProgettoID == id)
                .Include(p => p.Progetto)
                .Include(m => m.Percorsi)
                .AsNoTracking()
                .ToListAsync();
            if (lista == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun progetto con questo id", controller: "ReportController", metodo: "Tutti");
            }
            MyLogger.Log(messaggio: $"Richiesta GET con id progetto {id}", controller: "ReportController", metodo: "Tutti");
            return View(lista);
        }
        public async Task<IActionResult> Details(int? id)
        {
            {
                if (id == null)
                {
                    MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ReportController", metodo: "Details");
                    return NotFound();
                }

                var report = await _context.Report
                    .Include(x => x.Progetto)
                        .ThenInclude(ll => ll.ModuliProgetto)
                            .ThenInclude(mm => mm.Modulo)
                    .Include(x => x.Percorsi)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(r => r.ID == id);
                if (report == null)
                {
                    MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun report con questo id", controller: "ReportController", metodo: "Details");
                    return NotFound();
                }
                ViewData["Lista"] = report.Percorsi.ToList();
                MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ReportController", metodo: "Details");
                return View(report);
            }
        }


        public IActionResult Error()
        {
            MyLogger.Log(messaggio: "ERRORE: " + Activity.Current?.Id ?? HttpContext.TraceIdentifier, controller: "ReportController", metodo: "Error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public FileResult Download(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: nessun filePath fornito", controller: "ReportController", metodo: "Download");
                return null;
            }
            var dir = Path.Combine(Globals.CartellaWEBMVA, "wwwroot", Path.GetDirectoryName(filePath));
            var fileName = Path.GetFileName(filePath);
            var extension = Path.GetExtension(fileName).ToLower();
            if (!System.IO.File.Exists(Path.Combine(dir, fileName)))
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: il file {Path.Combine(dir, fileName)} non esiste", controller: "ReportController", metodo: "Download");
                return null;
            }
            IFileProvider provider = new PhysicalFileProvider(dir);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            string mimetype = "text/plain";
            switch (extension)
            {
                case ".html":
                    mimetype = "text/html";
                    break;
                case ".xml":
                    mimetype = "application/xml";
                    break;
                default: break;
            }
            MyLogger.Log(messaggio: $"Richiesta POST: file {Path.Combine(dir, fileName)} scaricato", controller: "ReportController", metodo: "Download");
            return File(readStream, mimetype, fileName);
        }
        private FileResult DownloadPDF(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) { return null; }
            var dir = Path.Combine(Globals.CartellaWEBMVA, "wwwroot", Path.GetDirectoryName(filePath));
            var fileName = Path.GetFileName(filePath);
            var extension = Path.GetExtension(fileName).ToLower();

            string percorsoAssolutoPDF;
            if (extension.Equals(".xml")) percorsoAssolutoPDF = Globals.ConvertiReportXML(Path.Combine(dir, fileName));
            else if (extension.Equals(".txt")) percorsoAssolutoPDF = Globals.ConvertiReportTXT(Path.Combine(dir, fileName));
            else return null; //per ora
            fileName = Path.GetFileName(percorsoAssolutoPDF);
            IFileProvider provider = new PhysicalFileProvider(dir);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);

            var readStream = fileInfo.CreateReadStream();
            string mimetype = "application/pdf";

            return File(readStream, mimetype, fileName);
        }
        [HttpPost]
        public async Task<IActionResult> DownloadUnico(string[] check, string[] pdf, string[] nativo, string cosa, string filePath, string nomeProgetto, string daDove)
        {
            if (string.IsNullOrEmpty(cosa))
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: richiesta malformata", controller: "ReportController", metodo: "DownloadUnico");
                return Error();
            }
            if (cosa.Equals("unico"))
            {
                if (check == null || check.Count() == 0)
                {
                    MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: richiesta malformata", controller: "ReportController", metodo: "DownloadUnico");
                    return Redirect(Url.Action(daDove, "Report").Replace("%2F", "/"));
                }
                MyLogger.Log(messaggio: $"Richiesta POST: iniziata generazione PDF unico", controller: "ReportController", metodo: "DownloadUnico");
                // converto tutti i report in pdf (se già non lo sono) e li accorpo, poi restituisco l'oggetto risultante
                var collezionePercorsi = await _context.PercorsiReport.ToListAsync();
                string[] percorsiPdf = new string[check.Length];
                for (int i = 0; i < check.Length; i++)
                {
                    var percorso = collezionePercorsi.SingleOrDefault(x => x.ID == int.Parse(check[i]));
                    string extension = Path.GetExtension(percorso.Percorso).ToLower();
                    if (extension.Equals(".txt") || extension.Equals(".html"))
                    {
                        percorsiPdf[i] = Globals.ConvertiReportTXT(Path.Combine(Globals.CARTELLAREPORT, percorso.Percorso));
                    }
                    else if (extension.Equals(".xml"))
                    {
                        percorsiPdf[i] = Globals.ConvertiReportXML(Path.Combine(Globals.CARTELLAREPORT, percorso.Percorso));
                    }
                    else if (extension.Equals(".pdf"))
                    {
                        percorsiPdf[i] = Path.Combine(Globals.CARTELLAREPORT, percorso.Percorso);
                    }
                    MyLogger.Log(messaggio: $"\t{percorsiPdf[i]} generato", controller: "ReportController", metodo: "DownloadUnico");
                }
                List<string> listaPercorsi = percorsiPdf.ToList();
                string percorsoPdfUnico = Path.Combine(Globals.CARTELLAREPORT, nomeProgetto, nomeProgetto + ".pdf");
                if (Globals.MergePDF(percorsoPdfUnico, listaPercorsi))
                {
                    var fileName = Path.GetFileName(percorsoPdfUnico);
                    IFileProvider provider = new PhysicalFileProvider(Path.GetDirectoryName(percorsoPdfUnico));
                    IFileInfo fileInfo = provider.GetFileInfo(fileName);

                    var readStream = fileInfo.CreateReadStream();
                    string mimetype = "application/pdf";
                    MyLogger.Log(messaggio: $"\tPDF unico generato correttamente", controller: "ReportController", metodo: "DownloadUnico");
                    return File(readStream, mimetype, fileName);
                }

            }
            else if (cosa.Equals("zip"))
            {
                //https://stackoverflow.com/questions/43281554/create-zip-in-net-core-from-urls-without-downloading-on-server
                if (nativo.Count() == 0 && pdf.Count() == 0)
                {
                    MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: richiesta malformata", controller: "ReportController", metodo: "DownloadUnico");
                    return Redirect(Url.Action(daDove, "Report").Replace("%2F", "/"));
                }
                MyLogger.Log(messaggio: $"Richiesta POST: iniziata generazione ZIP", controller: "ReportController", metodo: "DownloadUnico");
                // prendo i report selezionati, li zippo e restituisco l'oggetto risultante
                string[] percorsiPdf = new string[pdf.Length];
                var collezionePercorsi = await _context.PercorsiReport.ToListAsync();

                List<string> listaPercorsi = new List<string>();
                foreach (string str in nativo)
                {
                    listaPercorsi.Add(Path.Combine(Globals.CartellaWEBMVA, "wwwroot", str));
                }
                for (int i = 0; i < pdf.Length; i++)
                {

                    var percorso = collezionePercorsi.SingleOrDefault(x => "Report/" + x.Percorso == pdf[i]);
                    string extension = Path.GetExtension(percorso.Percorso).ToLower();
                    if (extension.Equals(".txt") || extension.Equals(".html"))
                    {
                        percorsiPdf[i] = Globals.ConvertiReportTXT(Path.Combine(Globals.CartellaWEBMVA, "wwwroot", "Report", percorso.Percorso));

                    }
                    else if (extension.Equals(".xml"))
                    {
                        percorsiPdf[i] = Globals.ConvertiReportXML(Path.Combine(Globals.CartellaWEBMVA, "wwwroot", "Report", percorso.Percorso));
                    }
                    else if (extension.Equals(".pdf"))
                    {
                        percorsiPdf[i] = Path.Combine(Globals.CartellaWEBMVA, "wwwroot", "Report", percorso.Percorso);
                    }

                }
                listaPercorsi.AddRange(percorsiPdf.ToList());
                var path = Path.Combine(Globals.CartellaWEBMVA, "wwwroot", "Report", nomeProgetto, "Report.zip");
                using (var fs = new FileStream(path, FileMode.Create))
                {

                    using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))
                    {

                        foreach (string item in listaPercorsi)
                        {
                            IFileProvider provider = new PhysicalFileProvider(Path.GetDirectoryName(item));
                            IFileInfo fileInfo = provider.GetFileInfo(Path.GetFileName(item));

                            var readStream = fileInfo.CreateReadStream();
                            ZipArchiveEntry entry = zip.CreateEntry(Path.GetFileName(item));
                            using (Stream entryStream = entry.Open())
                            {
                                readStream.CopyTo(entryStream);
                            }
                            MyLogger.Log(messaggio: $"\tFile {item} aggiunto allo ZIP", controller: "ReportController", metodo: "DownloadUnico");
                        }

                        IFileProvider provider2 = new PhysicalFileProvider(Path.GetDirectoryName(path));
                        IFileInfo fileInfo2 = provider2.GetFileInfo(Path.GetFileName(path));
                        MyLogger.Log(messaggio: $"\tFile ZIP generato con successo", controller: "ReportController", metodo: "DownloadUnico");
                        return File(fileInfo2.CreateReadStream(), "application/zip", "Report.zip");

                    }
                }



            }
            else if (cosa.Equals("singolo"))
            {
                // download del singolo report
                Download(filePath);
            }
            return Error();
        }
        private FileResult DownloadZip(string[] nativo, string[] pdf)
        {
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Importa(IFormFile file, int idProgetto, int reportID)
        {
            if (file == null || file.Length == 0)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: nessun file selezionato", controller: "ReportController", metodo: "Importa");
                return Content("Nessun file selezionato");
            }
            MyLogger.Log(messaggio: $"Richiesta POST", controller: "ReportController", metodo: "Importa");
            int reportIDDentro;
            if (reportID == -1)
            {
                // non esiste il report degli importati, lo creo
                Report report = new Report
                {
                    ProgettoID = idProgetto,
                    isImportati = true
                };
                _context.Report.Add(report);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                reportIDDentro = _context.Report.SingleOrDefault(x => x.ProgettoID == idProgetto && x.isImportati == true).ID;
                MyLogger.Log(messaggio: $"\tReport dei file importati creato con id {reportIDDentro}", controller: "ReportController", metodo: "Importa");
            }
            else reportIDDentro = reportID;
            string nomeProgetto = _context.Progetti.SingleOrDefault(x => x.ID == idProgetto).Nome;
            var path = Path.Combine(
                        Globals.CreaCartellaImportati(nomeProgetto),
                        file.FileName);

            string percorsoPerReport = Path.Combine(nomeProgetto, "Importati", Path.GetFileName(path));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            PercorsiReport pr = new PercorsiReport { ReportID = reportIDDentro, Percorso = percorsoPerReport };
            _context.PercorsiReport.Add(pr);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            MyLogger.Log(messaggio: $"\tFile {percorsoPerReport} importato", controller: "ReportController", metodo: "Importa");
            return Redirect(Url.Action($"Tutti/{idProgetto}", "Report").Replace("%2F", "/"));
        }


        // GET: Report/Delete/5 (elimina totalmente un report e i suoi percorsi)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ReportController", metodo: "Delete");
                return NotFound();
            }

            var report = await _context.Report
                .Include(list => list.Percorsi)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun report con questo id", controller: "ReportController", metodo: "Delete");
                return NotFound();
            }
            MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ReportController", metodo: "Delete");
            return View(report);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string cancellaFile = "false")
        {

            var report = await _context.Report
            .Include(x => x.Percorsi)
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST con id {id} fallita: nessun report con questo id", controller: "ReportController", metodo: "Delete");
                return NotFound();
            }
            MyLogger.Log(messaggio: $"Richiesta POST con id {id}", controller: "ReportController", metodo: "Delete");
            if (cancellaFile.Equals("true"))
            {
                MyLogger.Log(messaggio: $"\tE' stato richiesto di eliminare anche i file fisici", controller: "ReportController", metodo: "Delete");
                // cancello i file
                foreach (var percorso in report.Percorsi)
                {
                    string percorsoCompleto = Path.Combine(Globals.CARTELLAREPORT, percorso.Percorso);
                    if (System.IO.File.Exists(percorsoCompleto))
                    {
                        System.IO.File.Delete(percorsoCompleto);
                        if (System.IO.File.Exists(percorsoCompleto))
                            MyLogger.Log(messaggio: $"\tERRORE: impossibile eliminare il file {percorsoCompleto}. Probabilmente è dovuto ai permessi sul file.", controller: "ReportController", metodo: "Delete");
                        else
                            MyLogger.Log(messaggio: $"\tIl file {percorsoCompleto} è stato eliminato", controller: "ReportController", metodo: "Delete");
                    }
                    else MyLogger.Log(messaggio: $"\tERRORE: impossibile eliminare il file {percorsoCompleto}: il file non esiste", controller: "ReportController", metodo: "Delete");
                }
            }
            _context.Report.Remove(report);
            var listaRecord = await _context.PercorsiReport.Where(riga => riga.ReportID == id).ToListAsync();
            _context.PercorsiReport.RemoveRange(listaRecord);
            await _context.SaveChangesAsync();
            MyLogger.Log(messaggio: $"\tReport con id {id} eliminato", controller: "ReportController", metodo: "Delete");
            return Redirect(Url.Action($"Lista/{report.ProgettoID}", "Report").Replace("%2F", "/"));
        }

        // GET: Report/Delete/5 (elimina totalmente un report e i suoi percorsi)
        public async Task<IActionResult> DeletePercorso(int? id, string daDove)
        {
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ReportController", metodo: "DeletePercorso");
                return NotFound();
            }

            var percorso = await _context.PercorsiReport
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (percorso == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun percorso con questo id", controller: "ReportController", metodo: "DeletePercorso");
                return NotFound();
            }
            ViewData["daDove"] = daDove;
            MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ReportController", metodo: "DeletePercorso");
            return View(percorso);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("DeletePercorso")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePercorsoConfirmed(int id, string daDove)
        {

            var percorso = await _context.PercorsiReport
                .SingleOrDefaultAsync(m => m.ID == id);
            if (percorso == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST con id {id} fallita: nessun percorso con questo id", controller: "ReportController", metodo: "DeletePercorso");
                return NotFound();
            }
            MyLogger.Log(messaggio: $"Richiesta POST con id {id}", controller: "ReportController", metodo: "DeletePercorso");
            string percorsoCompleto = Path.Combine(Globals.CARTELLAREPORT, percorso.Percorso);
            if (System.IO.File.Exists(percorsoCompleto))
            {
                System.IO.File.Delete(percorsoCompleto);
                if (System.IO.File.Exists(percorsoCompleto))
                    MyLogger.Log(messaggio: $"\tERRORE: impossibile eliminare il file {percorsoCompleto}. Probabilmente è dovuto ai permessi sul file.", controller: "ReportController", metodo: "DeletePercorso");
                else
                    MyLogger.Log(messaggio: $"\tIl file {percorsoCompleto} è stato eliminato", controller: "ReportController", metodo: "DeletePercorso");
            }
            else MyLogger.Log(messaggio: $"\tERRORE: impossibile eliminare il file {percorsoCompleto}: il file non esiste", controller: "ReportController", metodo: "DeletePercorso");


            _context.PercorsiReport.Remove(percorso);
            await _context.SaveChangesAsync();
            MyLogger.Log(messaggio: $"\tPercorso con id {id} eliminato dal db", controller: "ReportController", metodo: "DeletePercorso");
            return Redirect(Url.Action($"{daDove}", "Report").Replace("%2F", "/"));
        }


        [HttpPost]
        // POST: Report/EliminaSelezionati 
        public async Task<IActionResult> EliminaSelezionati(string[] check)
        {
            if (check == null || check.Count() == 0)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: richiesta malformata", controller: "ReportController", metodo: "EliminaSelezionati");
                return NotFound();
            }
            List<Report> lista = new List<Report>();
            for (int i = 0; i < check.Length; i++)
            {
                var report = await _context.Report
                .Include(list => list.Percorsi)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == int.Parse(check[i]));
                if (report == null)
                {
                    MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: nessun report con id {check[i]}", controller: "ReportController", metodo: "EliminaSelezionati");
                    return NotFound();
                }
                lista.Add(report);
            }
            MyLogger.Log(messaggio: $"Richiesta POST", controller: "ReportController", metodo: "EliminaSelezionati");
            string concatenated = string.Join(",",
                          check.Select(x => x.ToString()).ToArray());
            MyLogger.Log(messaggio: $"\tReport selezionati: {concatenated}");
            return View(lista);
        }

        // POST: Report/EliminaSelezionatiConfirmed
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminaSelezionatiConfirmed(string[] id, string progettoID)
        {

            MyLogger.Log(messaggio: $"Richiesta POST", controller: "ReportController", metodo: "EliminaSelezionatiConfirmed");
            for (int i = 0; i < id.Length; i++)
            {
                var report = await _context.Report
                    .Include(x => x.Percorsi)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(m => m.ID == int.Parse(id[i]));
                if (report == null)
                {
                    MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: nessun report con id {id[i]}", controller: "ReportController", metodo: "EliminaSelezionati");
                    return NotFound();
                }
                string cartella="";
                // cancello i file
                foreach (var percorso in report.Percorsi)
                {
                    string percorsoCompleto = Path.Combine(Globals.CARTELLAREPORT, percorso.Percorso);
                    cartella = Path.GetDirectoryName(percorsoCompleto);
                    Console.WriteLine(percorsoCompleto);
                    if (System.IO.File.Exists(percorsoCompleto))
                    {
                        System.IO.File.Delete(percorsoCompleto);
                        if (System.IO.File.Exists(percorsoCompleto))
                            MyLogger.Log(messaggio: $"\tERRORE: impossibile eliminare il file {percorsoCompleto}. Probabilmente è dovuto ai permessi sul file.", controller: "ReportController", metodo: "EliminaSelezionatiConfirmed");
                        else
                            MyLogger.Log(messaggio: $"\tIl file {percorsoCompleto} è stato eliminato", controller: "ReportController", metodo: "EliminaSelezionatiConfirmed");
                    }
                    else MyLogger.Log(messaggio: $"\tERRORE: impossibile eliminare il file {percorsoCompleto}: il file non esiste", controller: "ReportController", metodo: "EliminaSelezionatiConfirmed");
                }

                _context.Report.Remove(report);
                var listaRecord = await _context.PercorsiReport.Where(riga => riga.ReportID == i).ToListAsync();
                _context.PercorsiReport.RemoveRange(listaRecord);
                await _context.SaveChangesAsync();
            }
            string concatenated = string.Join(",",
                          id.Select(x => x.ToString()).ToArray());
            MyLogger.Log(messaggio: $"\tEliminati i report {concatenated}", controller: "ReportController", metodo: "EliminaSelezionatiConfirmed");
            return Redirect(Url.Action($"Lista/{progettoID}", "Report").Replace("%2F", "/"));
        }
    }
}