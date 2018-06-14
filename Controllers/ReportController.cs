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
            return View(await _context.Progetti
            .Include(x => x.ListaReport)
            .AsNoTracking()
            .ToListAsync());
        }
        public async Task<IActionResult> Lista(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lista = await _context.Report
                .Where(m => m.ProgettoID == id)
                .Include(p => p.Progetto)
                .AsNoTracking()
                .ToListAsync();
            ViewData["Progetto"] = _context.Progetti.SingleOrDefault(x=>x.ID == id);
            return View(lista);
        }
        public async Task<IActionResult> Tutti(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lista = await _context.Report
                .Where(m => m.ProgettoID == id)
                .Include(p => p.Progetto)
                .Include(m => m.Percorsi)
                .AsNoTracking()
                .ToListAsync();
            return View(lista);
        }
        public async Task<IActionResult> Details(int? id)
        {
            {
                if (id == null)
                {
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
                    return NotFound();
                }
                ViewData["Lista"] = report.Percorsi.ToList();
                return View(report);
            }
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public FileResult Download(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) { return null; }
            var dir = Path.Combine(Globals.CartellaWEBMVA, "wwwroot", Path.GetDirectoryName(filePath));
            var fileName = Path.GetFileName(filePath);
            var extension = Path.GetExtension(fileName).ToLower();
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
            if (string.IsNullOrEmpty(cosa)) return Error();
            if (cosa.Equals("unico"))
            {
                if (check == null || check.Count()==0) return Redirect(Url.Action(daDove, "Report").Replace("%2F", "/"));
                // converto tutti i report in pdf (se già non lo sono) e li accorpo, poi restituisco l'oggetto risultante
                var collezionePercorsi = await _context.PercorsiReport.ToListAsync();
                string[] percorsiPdf = new string[check.Length];
                for (int i = 0; i < check.Length; i++)
                {
                    var percorso = collezionePercorsi.SingleOrDefault(x => x.ID == int.Parse(check[i]));
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
                List<string> listaPercorsi = percorsiPdf.ToList();
                string percorsoPdfUnico = Path.Combine(Globals.CartellaWEBMVA, "wwwroot", "Report", nomeProgetto, nomeProgetto + ".pdf");
                if (Globals.MergePDF(percorsoPdfUnico, listaPercorsi))
                {
                    var fileName = Path.GetFileName(percorsoPdfUnico);
                    IFileProvider provider = new PhysicalFileProvider(Path.GetDirectoryName(percorsoPdfUnico));
                    IFileInfo fileInfo = provider.GetFileInfo(fileName);

                    var readStream = fileInfo.CreateReadStream();
                    string mimetype = "application/pdf";

                    return File(readStream, mimetype, fileName);
                }

            }
            else if (cosa.Equals("zip"))
            {
                //https://stackoverflow.com/questions/43281554/create-zip-in-net-core-from-urls-without-downloading-on-server
                if (nativo.Count() == 0 && pdf.Count() == 0) return Redirect(Url.Action(daDove, "Report").Replace("%2F", "/"));
                // prendo i report selezionati, li zippo e restituisco l'oggetto risultante
                string[] percorsiPdf = new string[pdf.Length];
                var collezionePercorsi = await _context.PercorsiReport.ToListAsync();

                List<string> listaPercorsi = new List<string>();
                foreach (string str in nativo)
                {
                    if (Path.GetFileName(str).Split('_').ElementAt(2).Equals("droope"))
                    {
                        List<string> content = System.IO.File.ReadLines(Path.Combine(Globals.CartellaWEBMVA, "wwwroot", str)).ToList();
                        if (!content.ElementAt(0).Contains("Report di Droopescan"))
                        {
                            content.Insert(0, "Report di Droopescan");
                            System.IO.File.WriteAllLines(str, content);
                        }
                    }
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
                        }

                        IFileProvider provider2 = new PhysicalFileProvider(Path.GetDirectoryName(path));
                        IFileInfo fileInfo2 = provider2.GetFileInfo(Path.GetFileName(path));
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
                return Content("file not selected");
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

            return Redirect(Url.Action($"Tutti/{idProgetto}", "Report").Replace("%2F", "/"));
        }


        // GET: Report/Delete/5 (elimina totalmente un report e i suoi percorsi)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(list => list.Percorsi)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (report == null)
            {
                return NotFound();
            }
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
                return NotFound();
            }
            if (cancellaFile.Equals("true"))
            {
                // cancello i file
                foreach (var percorso in report.Percorsi)
                    System.IO.File.Delete(Path.Combine(Globals.CartellaWEBMVA, "wwwroot", "Report", percorso.Percorso));
            }
            _context.Report.Remove(report);
            var listaRecord = await _context.PercorsiReport.Where(riga => riga.ReportID == id).ToListAsync();
            _context.PercorsiReport.RemoveRange(listaRecord);
            await _context.SaveChangesAsync();
            return Redirect(Url.Action($"Lista/{report.ProgettoID}","Report").Replace("%2F","/"));
        }

        // GET: Report/Delete/5 (elimina totalmente un report e i suoi percorsi)
        public async Task<IActionResult> DeletePercorso(int? id, string daDove)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percorso = await _context.PercorsiReport
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (percorso == null)
            {
                return NotFound();
            }
            ViewData["daDove"] = daDove;
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
                return NotFound();
            }
            System.IO.File.Delete(Path.Combine(Globals.CartellaWEBMVA, "wwwroot", "Report", percorso.Percorso));
            _context.PercorsiReport.Remove(percorso);
            await _context.SaveChangesAsync();
            return Redirect(Url.Action($"{daDove}","Report").Replace("%2F","/"));
        }


[HttpPost]
        // POST: Report/EliminaSelezionati 
        public async Task<IActionResult> EliminaSelezionati(string[] check)
        {
            if (check == null || check.Count()==0)
            {
                return NotFound();
            }
            List<Report> lista = new List<Report>();
            for(int i = 0; i<check.Length;i++){
                var report = await _context.Report
                .Include(list => list.Percorsi)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == int.Parse(check[i]));
            if (report == null)
            {
                return NotFound();
            }
            lista.Add(report);
            }
            
            return View(lista);
        }

        // POST: Report/Delete/5
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminaSelezionatiConfirmed(string[] id, string progettoID)
        {
            for(int i = 0; i<id.Length;i++){
                var report = await _context.Report
            .Include(x => x.Percorsi)
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ID == int.Parse(id[i]));
            if (report == null)
            {
                return NotFound();
            }
                // cancello i file
                foreach (var percorso in report.Percorsi)
                    System.IO.File.Delete(Path.Combine(Globals.CartellaWEBMVA, "wwwroot", "Report", percorso.Percorso));
            _context.Report.Remove(report);
            var listaRecord = await _context.PercorsiReport.Where(riga => riga.ReportID == i).ToListAsync();
            _context.PercorsiReport.RemoveRange(listaRecord);
            await _context.SaveChangesAsync();
            }

            
            return Redirect(Url.Action($"Lista/{progettoID}","Report").Replace("%2F","/"));
        }
    }
}