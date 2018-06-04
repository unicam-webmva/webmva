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
            var view = PopolaModuliAssegnati(progetto);
            return View(view);
        }
        private ModuliInseriti PopolaModuliAssegnati(Progetto progetto, bool nulloTuttiITarget=false){
            var tuttiModuli = _context.Moduli;
            var moduliProgetto = new HashSet<ModuliProgetto>(progetto.ModuliProgetto);
            var dati= new List<ModuliInProgetto>();
            foreach(var modulo in tuttiModuli){
                bool inserito = moduliProgetto.Any(m=>m.ModuloID == modulo.ID);
                string target="";
                if(nulloTuttiITarget) target = null;
                else if(inserito) target = progetto.ModuliProgetto.SingleOrDefault(m=>m.ModuloID==modulo.ID).Target;
                dati.Add(new ModuliInProgetto{
                    ModuloID = modulo.ID,
                    Nome = modulo.Nome,
                    Comando = modulo.Comando,
                    Inserito = inserito,
                    Target = target,
                    Applicazione = modulo.Applicazione,
                });
            }
            var viewmodel = new ModuliInseriti{Progetto = progetto, ListaModuliConTarget = dati};
            //ViewData["Moduli"] = dati;
            return viewmodel;
        }

        // POST: Progetto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ModuliInseriti moduliInseriti, string[] moduliSelezionati)
        {
            if (id == null) return NotFound();
            var progetto = await _context.Progetti
                        .Include(p => p.ModuliProgetto)
                        .ThenInclude(m => m.Modulo)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(a => a.ID == id);

            if (progetto == null) return NotFound();

            if (await TryUpdateModelAsync<Progetto>(progetto, "",
                i => i.Nome, i => i.Descrizione))
            {
                  if(!moduliInseriti.ListaModuliConTarget.Any(l => l.Inserito == true)){
                  ViewData["errori"] = "Non è stato selezionato alcun modulo!";
                  var view = PopolaModuliAssegnati(progetto);
                  Dictionary<string, string> lista = new Dictionary<string, string>();
                  foreach(var it in view.ListaModuliConTarget.Where(x=>!string.IsNullOrEmpty(x.Target))){
                    lista.Add(it.Nome, it.Target);
                    }
                    ViewData["MessaggiTarget"] = lista;
                  return View(view);
                  }
                if(AggiornaModuliInseriti(moduliInseriti.ListaModuliConTarget, progetto)){
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
                }
                else{
                    var idStronzi = moduliInseriti.ListaModuliConTarget.Where(m=>string.IsNullOrEmpty(m.Target) && m.Inserito==true).Select(x=>x.ModuloID).ToArray();
                    List<string> nomiStronzi = new List<string>();
                    for(int i = 0; i<idStronzi.Length; i++){
                        nomiStronzi.Add(_context.Moduli.SingleOrDefault(m=>m.ID == idStronzi[i]).Nome);
                    }
                    ViewData["moduliStronzi"]=nomiStronzi;
                    return View(PopolaModuliAssegnati(progetto));
                }
                
                return RedirectToAction(nameof(Index));
            }
            
            AggiornaModuliInseriti(moduliInseriti.ListaModuliConTarget, progetto);
            ViewData["errori"] = "Non è stato selezionato alcun modulo!";
            return View(PopolaModuliAssegnati(progetto));
        }

        private bool AggiornaModuliInseriti(List<ModuliInProgetto> moduliDaAggiornare, Progetto progetto)
        {
          
            if (!moduliDaAggiornare.Any(m=>m.Inserito == true))
            {
                progetto.ModuliProgetto = new List<ModuliProgetto>();
                return true;
            }

            var moduliSelezionatiHS = moduliDaAggiornare.Where(m=>m.Inserito == true);
            var moduliGiaPresenti = new HashSet<ModuliProgetto>(progetto.ModuliProgetto);
            
            foreach (var modulo in _context.Moduli)
            {
                if (moduliSelezionatiHS.Any(m=>m.ModuloID==modulo.ID))
                {
                    // Se nei moduli già presenti nel progetto non c'è quello che sto guardando:
                    if (!moduliGiaPresenti.Any(m=>m.ModuloID==modulo.ID))
                    {
                        var mod = moduliDaAggiornare.SingleOrDefault(m=>m.ModuloID==modulo.ID);
                        if(string.IsNullOrEmpty(mod.Target)){
                            return false;
                        }
                        // Creo l'associazione tra progetto e modulo col relativo target
                        _context.Add(new ModuliProgetto { ModuloID = modulo.ID, ProgettoID = progetto.ID, Target = mod.Target });
                        return true;
                    }
                    // altrimenti, se il modulo era presente ma il target è stato cambiato
                    else if(moduliGiaPresenti.SingleOrDefault(m=>m.ModuloID==modulo.ID).Target != moduliDaAggiornare.SingleOrDefault(m=>m.ModuloID==modulo.ID).Target)
                    {
                        // lo aggiorno col nuovo target
                        var associazioneDaAggiornare = moduliGiaPresenti.SingleOrDefault(m=>m.ModuloID==modulo.ID);
                        associazioneDaAggiornare.Target = moduliDaAggiornare.SingleOrDefault(m=>m.ModuloID==modulo.ID).Target;
                        _context.Update(associazioneDaAggiornare);
                        return true;
                    }
                }
                // se sono qui il modulo va eliminato
                else
                {
                    if (moduliGiaPresenti.Any(m=>m.ModuloID == modulo.ID))
                    {
                        ModuliProgetto moduloDaRimuovere = progetto.ModuliProgetto.SingleOrDefault(m => m.ModuloID == modulo.ID);
                        _context.Remove(moduloDaRimuovere);
                        return true;
                    }
                }
            }
            return true;
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
            PopolaModuliAssegnati(progetto);
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
                string comando = CreaComando(modulo, modprog.Target);
                string cartellaProgetto = Globals.CreaCartellaProgetto(progetto.Nome);
                risultati.Add(modulo.Nome, $"<h3> {modulo.Applicazione.ToString()}</h3> \r\n <p>{comando.EseguiCLI(cartellaProgetto)}</p>");
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
                // Qui non viene usato il comando perché si verrà reindirizzati fuori tramite python, 
                // soluzione compatibile con quasi tutti i SO
                
                return "python -mwebbrowser " + mod.Comando;
            }
            if(mod is ModuloDNSRECON)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "dnsrecon");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} --xml {timestamp}dnsrecon_{nomeCamelCase}.xml";
                return comando;
            }
            if(mod is ModuloFIERCE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "fierce", "fierce");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}fierce_{nomeCamelCase}.txt";
                return comando;
            }
            if(mod is ModuloDROOPE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "droopescan");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}droopescan_{nomeCamelCase}.txt";
                return comando;
            }
             if(mod is ModuloINFOGA || mod is ModuloINFOGAEMAIL)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "Infoga");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}infoga_{nomeCamelCase}.txt";
                return comando;
            }
             if(mod is ModuloSUBLIST3R)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "Sublist3r");
                string comando = $"python \"{percorsoExec}\" {mod.Comando}  -o {timestamp}sublist3r_{nomeCamelCase}.txt";
                return comando;
            }
            if(mod is ModuloWAPITI)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wapiti");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} -f html -o {timestamp}wapiti_{nomeCamelCase}.html";
                return comando;
            }
             if(mod is ModuloOPENDOOR)
            {
                 string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "OpenDoor");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}opendoor_{nomeCamelCase}.txt ";
                return comando;
            }
            if(mod is ModuloSQLMAP)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "sqlmap");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}sqlmap_{nomeCamelCase}.txt ";
                return comando;
            }
            if(mod is ModuloWIFITE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wifite2");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}wifite2_{nomeCamelCase}.txt ";
                return comando;
            }
             if(mod is ModuloJOOMSCAN)
            {
                 string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "joomscan");
                string comando = $"perl \"{percorsoExec}\" {mod.Comando} >> {timestamp}joomsan_{nomeCamelCase}.txt ";
                return comando;
            }
              if(mod is ModuloWPSCAN)
            {
                 string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wpscan");
                string comando = $"ruby \"{percorsoExec}\" {mod.Comando} --log {timestamp}wpscan_{nomeCamelCase}.txt ";
                return comando;
            }
            if(mod is ModuloWASCAN)
            {
                 string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "WAScan");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}wascan_{nomeCamelCase}.txt ";
                return comando;
            }
            if(mod is ModuloDNSENUM)
            {
                 string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "dnsenum");
                string comando = $"perl \"{percorsoExec}\" {mod.Comando} -o {timestamp}dnsenum_{nomeCamelCase}.xml ";
                return comando;
            }
            if(mod is ModuloODAT)
            {
                 string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "odat");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {timestamp}odat_{nomeCamelCase}.txt ";
                return comando;
            }
             
             
             
            else return "";
        }
    }
}
