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
       
    }
}