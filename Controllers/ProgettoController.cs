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

namespace webmva.Controllers_
{
    public class ProgettoController : Controller
    {
        private readonly MyDbContext _context;

        public ProgettoController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Progetto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Progetti.ToListAsync());
        }

        // GET: Progetto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progetto = await _context.Progetti
                .Include(list => list.ModuliProgetto)
                    .ThenInclude(mod => mod.Modulo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (progetto == null)
            {
                return NotFound();
            }

            return View(progetto);
        }

        // GET: Progetto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Progetto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome")] Progetto progetto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progetto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(progetto);
        }

        // GET: Progetto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progetto = await _context.Progetti.SingleOrDefaultAsync(m => m.ID == id);
            var listaModuli = await _context.Moduli.ToListAsync();
            if (progetto == null)
            {
                return NotFound();
            }

            var progettoVM = new ProgettoVM{Progetto = progetto, TuttiModuli=listaModuli};
            return View(progettoVM);
        }

        // POST: Progetto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProgettoVM progettoVM)
        {
            var progetto = progettoVM.Progetto;
            if (id != progetto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progetto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgettoExists(progetto.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(progettoVM);
        }

        // GET: Progetto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progetto = await _context.Progetti
                .Include(list => list.ModuliProgetto)
                    .ThenInclude(mod => mod.Modulo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (progetto == null)
            {
                return NotFound();
            }

            return View(progetto);
        }

        // POST: Progetto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var progetto = await _context.Progetti.SingleOrDefaultAsync(m => m.ID == id);
            _context.Progetti.Remove(progetto);
            var listaRecord = await _context.ModuliProgetto.Where(riga =>riga.ProgettoID == id).ToListAsync();
            _context.ModuliProgetto.RemoveRange(listaRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgettoExists(int id)
        {
            return _context.Progetti.Any(e => e.ID == id);
        }
        public async Task<IActionResult> Run(int? id){

            var progetto = await _context.Progetti
                .Include(list => list.ModuliProgetto)
                    .ThenInclude(mod => mod.Modulo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            //List<string> result = new List<string>();
            Dictionary<string, string> risultati = new Dictionary<string, string>();
            foreach (ModuliProgetto modprog in progetto.ModuliProgetto){
                Modulo modulo = modprog.Modulo;
                string comando = modulo.Comando + " " + progetto.Target;
                risultati.Add(modulo.Nome, comando.EseguiCLI());
            }

            //string comando = primoModulo.Comando + " " + progetto.Target;

            // esecuzione
            /*
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.Arguments = "/C " + comando;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            string result;
            using (Process proc = Process.Start(info))
            {
                using (StreamReader reader = proc.StandardOutput)
                {
                    result = await reader.ReadToEndAsync();
                }
            }
            */
            return View(new RisultatoVM{NomeProgetto=progetto.Nome, risultati=risultati});
        }
    }
}
