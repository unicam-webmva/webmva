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
        public async Task<IActionResult> Create(Progetto progetto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(progetto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Edit), new {id = progetto.ID });
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

            var progetto = await _context.Progetti
                .Include(list => list.ModuliProgetto)
                    .ThenInclude(mod => mod.Modulo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (progetto == null)
            {
                return NotFound();
            }
            PopolaModuliAssegnati(progetto);
            return View(progetto);
        }
        private void PopolaModuliAssegnati(Progetto progetto){
            var tuttiModuli = _context.Moduli;
            var moduliProgetto = new HashSet<int>(progetto.ModuliProgetto.Select(m=>m.ModuloID));
            var dati= new List<ModuliInseriti>();
            foreach(var modulo in tuttiModuli){
                dati.Add(new ModuliInseriti{
                    ModuloID = modulo.ID,
                    Nome = modulo.Nome,
                    Comando = modulo.Comando,
                    Inserito = moduliProgetto.Contains(modulo.ID),
                    Applicazione = modulo.Applicazione
                });
            }
            ViewData["Moduli"] = dati;
        }

        // POST: Progetto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] moduliSelezionati)
        {
            /*var progettoNuovo = progettoVM.Progetto;
            if (id != progettoNuovo.ID)
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
            */
            

            if (id == null) return NotFound();
            var progetto = await _context.Progetti
                        .Include(p => p.ModuliProgetto)
                        .ThenInclude(m => m.Modulo)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(a => a.ID == id);

            if (progetto == null) return NotFound();

            if (await TryUpdateModelAsync<Progetto>(progetto, "",
                i => i.Nome, i => i.Target, i => i.Data, i => i.Descrizione))
            {
                AggiornaModuliInseriti(moduliSelezionati, progetto);
                _context.Update(progetto);
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
                return RedirectToAction(nameof(Index));
            }
            AggiornaModuliInseriti(moduliSelezionati, progetto);
            PopolaModuliAssegnati(progetto);
            return View(progetto);
        }

        private void AggiornaModuliInseriti(string[] moduliSelezionati, Progetto progettoDaAggiornare)
        {
            if (moduliSelezionati == null)
            {
                progettoDaAggiornare.ModuliProgetto = new List<ModuliProgetto>();
                return;
            }

            var moduliSelezionatiHS = new HashSet<string>(moduliSelezionati);
            var moduliGiaPresenti = new HashSet<int>(progettoDaAggiornare.ModuliProgetto.Select(i => i.ModuloID));
            
            foreach (var modulo in _context.Moduli)
            {
                if (moduliSelezionatiHS.Contains(modulo.ID.ToString()))
                {
                    if (!moduliGiaPresenti.Contains(modulo.ID))
                    {
                        _context.Add(new ModuliProgetto { ModuloID = modulo.ID, ProgettoID = progettoDaAggiornare.ID });
                    }
                }
                else
                {
                    if (moduliGiaPresenti.Contains(modulo.ID))
                    {
                        ModuliProgetto moduloDaRimuovere = progettoDaAggiornare.ModuliProgetto.SingleOrDefault(m => m.ModuloID == modulo.ID);
                        _context.Remove(moduloDaRimuovere);
                    }
                }
            }
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
            var listaRecord = await _context.ModuliProgetto.Where(riga => riga.ProgettoID == id).ToListAsync();
            _context.ModuliProgetto.RemoveRange(listaRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgettoExists(int id)
        {
            return _context.Progetti.Any(e => e.ID == id);
        }
        public async Task<IActionResult> Run(int? id)
        {

            var progetto = await _context.Progetti
                .Include(list => list.ModuliProgetto)
                    .ThenInclude(mod => mod.Modulo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            //List<string> result = new List<string>();
            Dictionary<string, string> risultati = new Dictionary<string, string>();
            foreach (ModuliProgetto modprog in progetto.ModuliProgetto)
            {
                Modulo modulo = modprog.Modulo;
                string comando = CreaComando(modulo, progetto.Target);
                string cartellaProgetto = Globals.CreaCartellaProgetto(progetto.Nome);
                risultati.Add(modulo.Nome, comando.EseguiCLI(cartellaProgetto));
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
            return View(new RisultatoVM { NomeProgetto = progetto.Nome, risultati = risultati });
        }
        private string CreaComando(Modulo mod, string target){
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_");
            string nomeCamelCase = mod.Nome.ToCamelCase();
            //Controllo di che tipo è il modulo
            if (mod is ModuloNMAP){
                // Inserisco il comando generato dal modulo, il target e la direttiva
                // per esportare un xml con nome derivato dal timestamp e dal nome del modulo
                string comando = $"{mod.Comando} -oX {timestamp}nmap_{nomeCamelCase}.xml {target}";
                return comando;
            }
            if(mod is ModuloNESSUS){
                //TODO: creare JSON?
                return "";
            }
            if(mod is ModuloDNSRECON)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "dnsrecon");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} -x {timestamp}dnsrecon_{nomeCamelCase}.xml";
                return comando;
            }
            if(mod is ModuloDROOPE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "droopescan");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}droopescan_{nomeCamelCase}.txt";
                return comando;
            }
             if(mod is ModuloINFOGA)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "Infoga");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}infoga_{nomeCamelCase}.txt";
                return comando;
            }
            else return "";
        }
    }
}
