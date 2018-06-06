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
            return View(await _context.Report
            .Include(x=>x.Progetto)
            .ToListAsync());
        }
        public async Task<IActionResult> Details(int? id){
{
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(x => x.Progetto)
                    .ThenInclude(ll => ll.ModuliProgetto)
                        .ThenInclude(mm=>mm.Modulo)
                .Include(x=>x.Percorsi)
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
            var dir = Path.Combine(Globals.CartellaWEBMVA, "wwwroot", Path.GetDirectoryName(filePath));
            var fileName = Path.GetFileName(filePath);
            var extension = Path.GetExtension(fileName).ToLower();
            IFileProvider provider = new PhysicalFileProvider(dir);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            string mimetype="text/plain";
            switch(extension){
                case ".html":
                    mimetype="text/html";
                    break;
                case ".xml":
                    mimetype="application/xml";
                    break;
                default: break;
            }
            return File(readStream, mimetype, fileName);
        }
    }
}