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
        private ModuliInseriti PopolaModuliAssegnati(Progetto progetto){
            var tuttiModuli = _context.Moduli;
            var moduliProgetto = new HashSet<ModuliProgetto>(progetto.ModuliProgetto);
            var dati= new List<ModuliInProgetto>();
            foreach(var modulo in tuttiModuli){
                bool inserito = moduliProgetto.Any(m=>m.ModuloID == modulo.ID);
                string target="";
                if(inserito) target = progetto.ModuliProgetto.SingleOrDefault(m=>m.ModuloID==modulo.ID).Target;
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

        private ModuliInseriti PopolaModuliAssegnatiErrore(Progetto progetto, List<ModuliInProgetto> listaModuliAggiornati, List<string> ModuliSenzaTarget,List<string> ModuliSenzaInserito){
            var tuttiModuli = _context.Moduli;
            var dati= new List<ModuliInProgetto>();
            foreach(var modulo in tuttiModuli){
                bool inserito = listaModuliAggiornati.Any(m=>m.ModuloID == modulo.ID && m.Inserito);
                string target = listaModuliAggiornati.SingleOrDefault(m=>m.ModuloID==modulo.ID).Target;
                if(inserito && string.IsNullOrEmpty(target)){
                    //guardo se c'è il target, sennò lo metto nella lista dei moduli senza target
                    ModuliSenzaTarget.Add(modulo.Nome);
                }
                else if(!inserito && !string.IsNullOrEmpty(target)){
                    // il target è non vuoto ma non è stato inserito, quindi lo metto nella lista dei moduli senza inserito
                    ModuliSenzaInserito.Add(modulo.Nome);
                }
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
                    List<string> ModuliSenzaTarget = new List<string>();
                    List<string> ModuliSenzaInserito = new List<string>();
                    var view = PopolaModuliAssegnatiErrore(progetto, moduliInseriti.ListaModuliConTarget, ModuliSenzaTarget, ModuliSenzaInserito);
                    ViewData["ListaSenzaTarget"] = ModuliSenzaTarget;
                    ViewData["ListaSenzaInserito"] = ModuliSenzaInserito;
                    if (!moduliInseriti.ListaModuliConTarget.Any(m=>m.Inserito && !string.IsNullOrEmpty(m.Target))){
                        ViewData["ProgettoSenzaModuli"] = "Inserire almeno un modulo col relativo target";
                    }
                    return View(view);
                }
                return RedirectToAction(nameof(Index));
            }
            List<string> ModuliSenzaTarget1 = new List<string>();
            List<string> ModuliSenzaInserito1 = new List<string>();
            var view1 = PopolaModuliAssegnatiErrore(progetto, moduliInseriti.ListaModuliConTarget, ModuliSenzaTarget1, ModuliSenzaInserito1);
            ViewData["ListaSenzaTarget"] = ModuliSenzaTarget1;
            ViewData["ListaSenzaInserito"] = ModuliSenzaInserito1;
            if (!moduliInseriti.ListaModuliConTarget.Any(m=>m.Inserito && !string.IsNullOrEmpty(m.Target))){
                ViewData["ProgettoSenzaModuli"] = "Inserire almeno un modulo col relativo target";
            }
            return View(view1);
        }

        private bool AggiornaModuliInseriti(List<ModuliInProgetto> moduliDaAggiornare, Progetto progetto)
        {
          
            if (!moduliDaAggiornare.Any(m=>m.Inserito && !string.IsNullOrEmpty(m.Target)))
            {
                progetto.ModuliProgetto = new List<ModuliProgetto>();
                //Console.WriteLine("Inserire almeno un modulo col relativo target");
                return false;
            }
            if(moduliDaAggiornare.Any(m=>!m.Inserito && !string.IsNullOrEmpty(m.Target))){
                //Console.WriteLine("Ci sono dei moduli non inseriti ma col target");
                return false;
            }
            if(moduliDaAggiornare.Any(m=>m.Inserito && string.IsNullOrEmpty(m.Target))){
                //Console.WriteLine("Ci sono dei moduli inseriti ma senza target");
                return false;
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
                        var associazioneDaAggiornare = progetto.ModuliProgetto.SingleOrDefault(m=>m.ModuloID==modulo.ID && m.ProgettoID == progetto.ID);
                        associazioneDaAggiornare.Target = moduliDaAggiornare.SingleOrDefault(m=>m.ModuloID == modulo.ID).Target;
                        _context.Update(associazioneDaAggiornare);
                        return true;
                    }
                }
                // se sono qui il modulo va eliminato
                else
                {
                    if (moduliGiaPresenti.Any(m=>m.ModuloID == modulo.ID))
                    {
                        ModuliProgetto moduloDaRimuovere = progetto.ModuliProgetto.SingleOrDefault(m => m.ModuloID == modulo.ID && m.ProgettoID == progetto.ID);
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
            var data = DateTime.Now;
            Report report = new Report{
                ProgettoID=progetto.ID,
                Data = data};
            List<string> percorsi = new List<string>();
            Dictionary<string, string> risultati = new Dictionary<string, string>();
            foreach (ModuliProgetto modprog in progetto.ModuliProgetto)
            {
                Modulo modulo = modprog.Modulo;
                string nomeFile = CreaNomeFile(modulo.Comando, modulo.Nome);
                string cartellaProgetto = Globals.CreaCartellaProgetto(progetto.Nome, data);
                string comando = CreaComando(modulo, modprog.Target, Path.Combine(progetto.Nome, data.ToString("dd-MM-yyyy_HH:mm")), nomeFile, percorsi);
                
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
            _context.Report.Add(report);
            try{
                await _context.SaveChangesAsync();
            }
            
            catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                var ID = _context.Report.SingleOrDefault(x=>x.ProgettoID==progetto.ID && x.Data==data).ID;
            foreach(var percorso in percorsi){
                _context.PercorsiReport.Add(new PercorsiReport{
                    ReportID=ID,
                    Percorso = percorso
                });
            }
            try{
                await _context.SaveChangesAsync();
            }
            
            catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            //return View(new RisultatoVM { NomeProgetto = progetto.Nome, risultati = risultati });
            return Redirect(Url.Action($"Details/{report.ID}", "Report").Replace("%2F","/"));
        }
        private string CreaNomeFile(string comandoModulo, string nomeModulo){
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_");
            string nomeCamelCase = nomeModulo.ToCamelCase();
            return $"{timestamp}{nomeCamelCase}_";
        }
        private string CreaComando(Modulo mod, string target, string cartella, string nomeFile, List<string> percorsi){
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_");
            string nomeCamelCase = mod.Nome.ToCamelCase();
            //Controllo di che tipo è il modulo
            if (mod is ModuloNMAP){
                // Inserisco il comando generato dal modulo, il target e la direttiva
                // per esportare un xml con nome derivato dal timestamp e dal nome del modulo
                string comando = $"{mod.Comando} -oX {nomeFile}nmap.xml {target}";
                percorsi.Add(Path.Combine(cartella, nomeFile+"nmap.xml"));
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
                string comando = $"python \"{percorsoExec}\" {mod.Comando} --xml {nomeFile}dnsrecon.xml";
                percorsi.Add(Path.Combine(cartella, nomeFile+"dnsrecon.xml"));
                return comando;
            }
            if(mod is ModuloFIERCE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "fierce", "fierce");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {nomeFile}fierce.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile+"fierce.txt"));
                return comando;
            }
            if(mod is ModuloDROOPE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "droopescan");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {nomeFile}droopescan.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile+"droopescan.txt"));
                return comando;
            }
             if(mod is ModuloINFOGA || mod is ModuloINFOGAEMAIL)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "Infoga");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {nomeFile}infoga.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile+"infoga.txt"));
                return comando;
            }
             if(mod is ModuloSUBLIST3R)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "Sublist3r");
                string comando = $"python \"{percorsoExec}\" {mod.Comando}  -o {nomeFile}sublist3r.txt";
                percorsi.Add(nomeFile+"sublist3r.txt");
                return comando;
            }
            if(mod is ModuloWAPITI)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wapiti");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} -f html -o {nomeFile}wapiti.html";
                percorsi.Add(Path.Combine(cartella, nomeFile+"wapiti.html"));
                return comando;
            }
             if(mod is ModuloOPENDOOR)
            {
                 string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "OpenDoor");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {nomeFile}opendoor.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile+"opendoor.txt"));
                return comando;
            }
            if(mod is ModuloSQLMAP)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "sqlmap");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {nomeFile}sqlmap.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile+"sqlmap.txt"));
                return comando;
            }
            if(mod is ModuloWIFITE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wifite2");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {nomeFile}wifite2.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile+"wifite2.txt"));
                return comando;
            }
             if(mod is ModuloJOOMSCAN)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "joomscan");
                string comando = $"perl \"{percorsoExec}\" {mod.Comando} >> {nomeFile}joomscan.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile+"joomscan.txt"));
                return comando;
            }
              if(mod is ModuloWPSCAN)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wpscan");
                string comando = $"ruby \"{percorsoExec}\" {mod.Comando} --log {nomeFile}wpscan.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile+"wpscan.txt"));
                return comando;
            }
            if(mod is ModuloWASCAN)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "WAScan");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {nomeFile}wascan.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile+"wascan.txt"));
                return comando;
            }
            if(mod is ModuloDNSENUM)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "dnsenum");
                string comando = $"perl \"{percorsoExec}\" {mod.Comando} -o {nomeFile}dnsenum.xml ";
                percorsi.Add(Path.Combine(cartella, nomeFile+"dnsenum.xml"));
                return comando;
            }
            if(mod is ModuloODAT)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "odat");
                string comando = $"python \"{percorsoExec}\" {mod.Comando} >> {nomeFile}odat.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile+"odat.txt"));
                return comando;
            }
             
             
             
            else return "";
        }
    }
}
