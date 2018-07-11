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
            MyLogger.Log(messaggio: $"Richiesta GET", controller: "ProgettoController", metodo: "Index");
            return View(await _context.Progetti
                .Include(list => list.ListaReport)
                .Include(m => m.ModuliProgetto)
                    .ThenInclude(m => m.Modulo)
                .AsNoTracking()
                .ToListAsync());
        }

        // GET: Progetto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ProgettoController", metodo: "Details");
                return NotFound();
            }

            var listaModuli = await _context.Progetti
                .Include(m => m.ModuliProgetto)
                    .ThenInclude(m => m.Modulo)
                .SingleOrDefaultAsync(x => x.ID == id);
            if (listaModuli == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun progetto con questo id", controller: "ProgettoController", metodo: "Details");
                return NotFound();
            }
            MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ProgettoController", metodo: "Details");
            return View(listaModuli);
        }

        // GET: Progetto/Create
        public IActionResult Create()
        {
            MyLogger.Log(messaggio: $"Richiesta GET", controller: "ProgettoController", metodo: "Create");
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
                MyLogger.Log(messaggio: $"Richiesta POST:\n\tNuovo progetto con nome {progetto.Nome}", controller: "ProgettoController", metodo: "Create");
                return RedirectToAction(nameof(Edit), new { id = progetto.ID });
            }
            MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: Richiesta malformata", controller: "ProgettoController", metodo: "Create");
            return View(progetto);
        }

        // GET: Progetto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ProgettoController", metodo: "Edit");
                return NotFound();
            }

            var progetto = await _context.Progetti
                .Include(list => list.ModuliProgetto)
                    .ThenInclude(mod => mod.Modulo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (progetto == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun progetto con questo id", controller: "ProgettoController", metodo: "Details");
                return NotFound();
            }
            var view = PopolaModuliAssegnati(progetto);
            MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ProgettoController", metodo: "Edit");
            return View(view);
        }
        private ModuliInseriti PopolaModuliAssegnati(Progetto progetto)
        {
            var tuttiModuli = _context.Moduli;
            var moduliProgetto = new HashSet<ModuliProgetto>(progetto.ModuliProgetto);
            var dati = new List<ModuliInProgetto>();
            foreach (var modulo in tuttiModuli)
            {
                bool inserito = moduliProgetto.Any(m => m.ModuloID == modulo.ID);
                string target = "";
                if (inserito) target = progetto.ModuliProgetto.SingleOrDefault(m => m.ModuloID == modulo.ID).Target;
                dati.Add(new ModuliInProgetto
                {
                    ModuloID = modulo.ID,
                    Nome = modulo.Nome,
                    Comando = modulo.Comando,
                    Inserito = inserito,
                    Target = target,
                    Applicazione = modulo.Applicazione,
                });
            }
            var viewmodel = new ModuliInseriti { Progetto = progetto, ListaModuliConTarget = dati };
            //ViewData["Moduli"] = dati;
            return viewmodel;
        }

        private ModuliInseriti PopolaModuliAssegnatiErrore(Progetto progetto, List<ModuliInProgetto> listaModuliAggiornati, List<string> ModuliSenzaTarget, List<string> ModuliSenzaInserito)
        {
            var tuttiModuli = _context.Moduli;
            var dati = new List<ModuliInProgetto>();
            foreach (var modulo in tuttiModuli)
            {
                bool inserito = listaModuliAggiornati.Any(m => m.ModuloID == modulo.ID && m.Inserito);
                string target = listaModuliAggiornati.SingleOrDefault(m => m.ModuloID == modulo.ID).Target;
                if (inserito && string.IsNullOrEmpty(target))
                {
                    //guardo se c'è il target, sennò lo metto nella lista dei moduli senza target
                    if (modulo.Applicazione != webmva.Models.APPLICAZIONE.WIFITE && modulo.Applicazione != webmva.Models.APPLICAZIONE.NOSQL && modulo.Applicazione != webmva.Models.APPLICAZIONE.OPENVAS && modulo.Applicazione != webmva.Models.APPLICAZIONE.NESSUS)
                    {
                        ModuliSenzaTarget.Add(modulo.Applicazione.ToString() + "- " + modulo.Nome);
                    }
                }

                else if (!inserito && !string.IsNullOrEmpty(target))
                {
                    // il target è non vuoto ma non è stato inserito, quindi lo metto nella lista dei moduli senza inserito
                    ModuliSenzaInserito.Add(modulo.Applicazione.ToString() + "- " + modulo.Nome);
                }
                dati.Add(new ModuliInProgetto
                {
                    ModuloID = modulo.ID,
                    Nome = modulo.Nome,
                    Comando = modulo.Comando,
                    Inserito = inserito,
                    Target = target,
                    Applicazione = modulo.Applicazione,
                });
            }
            var viewmodel = new ModuliInseriti { Progetto = progetto, ListaModuliConTarget = dati };
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
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: nessun id fornito", controller: "ProgettoController", metodo: "Edit");
                return NotFound();
            }
            var progetto = await _context.Progetti
                        .Include(p => p.ModuliProgetto)
                        .ThenInclude(m => m.Modulo)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(a => a.ID == id);

            if (progetto == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST con id {id} fallita: nessun progetto con questo id", controller: "ProgettoController", metodo: "Edit");
                return NotFound();
            }

            if (await TryUpdateModelAsync<Progetto>(progetto, "",
                i => i.Nome, i => i.Descrizione))
            {
                if (AggiornaModuliInseriti(moduliInseriti.ListaModuliConTarget, progetto))
                {
                    progetto.Nome = moduliInseriti.Progetto.Nome;
                    progetto.Descrizione = moduliInseriti.Progetto.Descrizione;

                    _context.Update(progetto);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        MyLogger.Log(messaggio: $"ERRORE CRITICO: Richiesta POST con id {id}: Errore nel DB", controller: "ProgettoController", metodo: "Edit");
                        throw;
                    }
                }
                else
                {
                    List<string> ModuliSenzaTarget = new List<string>();
                    List<string> ModuliSenzaInserito = new List<string>();
                    var view = PopolaModuliAssegnatiErrore(progetto, moduliInseriti.ListaModuliConTarget, ModuliSenzaTarget, ModuliSenzaInserito);
                    ViewData["ListaSenzaTarget"] = ModuliSenzaTarget;
                    ViewData["ListaSenzaInserito"] = ModuliSenzaInserito;
                    if (moduliInseriti.ListaModuliConTarget.All(m => m.Inserito == false && string.IsNullOrEmpty(m.Target)))
                    {
                        ViewData["ProgettoSenzaModuli"] = "Inserire almeno un modulo col relativo target";
                    }
                    MyLogger.Log(messaggio: $"AVVISO: Richiesta POST con id {id}: Richiesta riuscita ma contenuto con errori.", controller: "ProgettoController", metodo: "Edit");
                    return View(view);
                }
                MyLogger.Log(messaggio: $"Richiesta POST con id {id}", controller: "ProgettoController", metodo: "Edit");
                MyLogger.Log(messaggio: $"\tProgetto con nome {progetto.Nome} modificato", controller: "ProgettoController", metodo: "Edit");
                return RedirectToAction(nameof(Index));
            }
            List<string> ModuliSenzaTarget1 = new List<string>();
            List<string> ModuliSenzaInserito1 = new List<string>();
            var view1 = PopolaModuliAssegnatiErrore(progetto, moduliInseriti.ListaModuliConTarget, ModuliSenzaTarget1, ModuliSenzaInserito1);
            ViewData["ListaSenzaTarget"] = ModuliSenzaTarget1;
            ViewData["ListaSenzaInserito"] = ModuliSenzaInserito1;
            if (moduliInseriti.ListaModuliConTarget.All(m => m.Inserito == false && string.IsNullOrEmpty(m.Target)))
            {
                ViewData["ProgettoSenzaModuli"] = "Inserire almeno un modulo col relativo target";
            }
            MyLogger.Log(messaggio: $"AVVISO: Richiesta POST con id {id}: Richiesta riuscita ma contenuto con errori.", controller: "ProgettoController", metodo: "Edit");
            return View(view1);
        }

        private bool AggiornaModuliInseriti(List<ModuliInProgetto> moduliDaAggiornare, Progetto progetto)
        {

            if (moduliDaAggiornare.All(m => m.Inserito == false && string.IsNullOrEmpty(m.Target)))
            {
                progetto.ModuliProgetto = new List<ModuliProgetto>();
                //Console.WriteLine("Inserire almeno un modulo col relativo target");
                return false;
            }
            if (moduliDaAggiornare.Any(m => !m.Inserito && !string.IsNullOrEmpty(m.Target)))
            {
                //Console.WriteLine("Ci sono dei moduli non inseriti ma col target");
                return false;
            }
            if (moduliDaAggiornare.Any(m => m.Inserito && string.IsNullOrEmpty(m.Target)))
            {
                //I moduli WIFITE, NOSQLMAP, NESSUS e OPENVAS non hanno bisogno del target
                List<ModuliInProgetto> ModuliEccezioni = moduliDaAggiornare.Where(m => m.Inserito && string.IsNullOrEmpty(m.Target)).ToList();
                foreach (var itm in ModuliEccezioni)
                {
                    if (itm.Applicazione == webmva.Models.APPLICAZIONE.WIFITE ||
                        itm.Applicazione == webmva.Models.APPLICAZIONE.NOSQL ||
                        itm.Applicazione == webmva.Models.APPLICAZIONE.NESSUS ||
                        itm.Applicazione == webmva.Models.APPLICAZIONE.OPENVAS)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                //Console.WriteLine("Ci sono dei moduli inseriti ma senza target");
                //return false;
            }

            var moduliSelezionatiHS = moduliDaAggiornare.Where(m => m.Inserito == true);
            var moduliGiaPresenti = new HashSet<ModuliProgetto>(progetto.ModuliProgetto);

            foreach (var modulo in _context.Moduli)
            {
                if (moduliSelezionatiHS.Any(m => m.ModuloID == modulo.ID))
                {
                    // Se nei moduli già presenti nel progetto non c'è quello che sto guardando:
                    if (!moduliGiaPresenti.Any(m => m.ModuloID == modulo.ID))
                    {
                        var mod = moduliDaAggiornare.SingleOrDefault(m => m.ModuloID == modulo.ID);
                        // Creo l'associazione tra progetto e modulo col relativo target
                        _context.Add(new ModuliProgetto { ModuloID = modulo.ID, ProgettoID = progetto.ID, Target = mod.Target });
                    }
                    // altrimenti, se il modulo era presente ma il target è stato cambiato
                    else if (moduliGiaPresenti.SingleOrDefault(m => m.ModuloID == modulo.ID).Target != moduliDaAggiornare.SingleOrDefault(m => m.ModuloID == modulo.ID).Target)
                    {
                        // lo aggiorno col nuovo target
                        var associazioneDaAggiornare = progetto.ModuliProgetto.SingleOrDefault(m => m.ModuloID == modulo.ID && m.ProgettoID == progetto.ID);
                        associazioneDaAggiornare.Target = moduliDaAggiornare.SingleOrDefault(m => m.ModuloID == modulo.ID).Target;
                        _context.Update(associazioneDaAggiornare);
                    }
                }
                // se sono qui il modulo va eliminato
                else
                {
                    if (moduliGiaPresenti.Any(m => m.ModuloID == modulo.ID))
                    {
                        ModuliProgetto moduloDaRimuovere = progetto.ModuliProgetto.SingleOrDefault(m => m.ModuloID == modulo.ID && m.ProgettoID == progetto.ID);
                        _context.Remove(moduloDaRimuovere);
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
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ProgettoController", metodo: "Delete");
                return NotFound();
            }

            var progetto = await _context.Progetti
                .Include(list => list.ModuliProgetto)
                    .ThenInclude(mod => mod.Modulo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (progetto == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun progetto con questo id", controller: "ProgettoController", metodo: "Delete");
                return NotFound();
            }
            PopolaModuliAssegnati(progetto);
            MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ProgettoController", metodo: "Delete");
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
            MyLogger.Log(messaggio: $"Richiesta POST con id {id}:\n\tProgetto con nome {progetto.Nome} eliminato", controller: "ProgettoController", metodo: "Delete");
            return RedirectToAction(nameof(Index));
        }

        private bool ProgettoExists(int id)
        {
            return _context.Progetti.Any(e => e.ID == id);
        }
        public async Task<IActionResult> Run(int? id)
        {
            MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ProgettoController", metodo: "Run");
            var progetto = await _context.Progetti
                .Include(list => list.ModuliProgetto)
                    .ThenInclude(mod => mod.Modulo)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            //List<string> result = new List<string>();
            var data = DateTime.Now;
            Report report = new Report
            {
                ProgettoID = progetto.ID,
                Data = data
            };
            List<string> percorsi = new List<string>();
            string cartellaProgetto = Globals.CreaCartellaScan(progetto.Nome, data);
            foreach (ModuliProgetto modprog in progetto.ModuliProgetto)
            {
                Modulo modulo = modprog.Modulo;
                string nomeFile = CreaNomeFile(modulo.Applicazione, modulo.Comando, modulo.Nome);
                string comando = CreaComando(modulo, modprog.Target, Path.Combine(progetto.Nome, data.ToString("dd-MM-yyyy_HH-mm")), nomeFile, percorsi);
                MyLogger.Log(messaggio: $"\tModulo {modulo.Applicazione.ToString()} - {modulo.Nome}, esecuzione comando: {comando} ...", controller: "ProgettoController", metodo: "Run");
                comando.EseguiCLI(cartellaProgetto);
                MyLogger.Log(messaggio: $"\tFine esecuzione comando", controller: "ProgettoController", metodo: "Run");

            }
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
            foreach (var percorso in percorsi)
                Globals.AggiungiIntestazione(percorso, _context);
            var ID = _context.Report.SingleOrDefault(x => x.ProgettoID == progetto.ID && x.Data == data).ID;
            foreach (var percorso in percorsi)
            {
                _context.PercorsiReport.Add(new PercorsiReport
                {
                    ReportID = ID,
                    Percorso = percorso
                });
            }
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
            MyLogger.Log(messaggio: $"\tFine esecuzione scan. Report con id {ID} salvato.", controller: "ProgettoController", metodo: "Run");
            //return View(new RisultatoVM { NomeProgetto = progetto.Nome, risultati = risultati });
            return Redirect(Url.Action($"Details/{report.ID}", "Report").Replace("%2F", "/"));
        }
        private string CreaNomeFile(APPLICAZIONE app, string comandoModulo, string nomeModulo)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string nomeCamelCase = nomeModulo.ToCamelCase();
            return $"{timestamp}_{app.ToString().ToLower()}_{nomeCamelCase}";
        }
        private string CreaComando(Modulo mod, string target, string cartella, string nomeFile, List<string> percorsi)
        {
            //Controllo di che tipo è il modulo
            string regex = "s/\x1B\\[([0-9]{1,2}(;[0-9]{1,2})?)?[m|K]//g";
            if (mod is ModuloNMAP)
            {
                // Inserisco il comando generato dal modulo, il target e la direttiva
                // per esportare un xml con nome derivato dal timestamp e dal nome del modulo
                string comando = $"{mod.Comando} -oX {nomeFile}.xml --webxml {target}";
                var percorsoFile = Path.Combine(cartella, nomeFile + ".xml");
                percorsi.Add(percorsoFile);
                return comando;
            }
            if (mod is ModuloNESSUS || mod is ModuloOPENVAS)
            {
                // Qui non viene usato il comando perché si verrà reindirizzati fuori tramite python, 
                // soluzione compatibile con quasi tutti i SO

                return "python -mwebbrowser " + mod.Comando;
            }
            if (mod is ModuloDNSRECON)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "dnsrecon");
                string comando = $"python {percorsoExec}/{mod.Comando} -d {target} --xml {nomeFile}.xml";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".xml"));
                return comando;
            }
            if (mod is ModuloFIERCE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "fierce", "fierce");
                string comando = $"python3 -u {percorsoExec}/{mod.Comando} --domain {target} | sed -r \"{regex}\" | tee {nomeFile}.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloDROOPE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "droopescan");
                string comando = $"python -u {percorsoExec}/{mod.Comando} -u {target} | sed -r \"{regex}\" | tee {nomeFile}.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloINFOGA)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "Infoga");
                string comando = $"python3 -u {percorsoExec}/{mod.Comando} -d {target} | sed -r \"{regex}\" | tee {nomeFile}.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloINFOGAEMAIL)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "Infoga");
                string comando = $"python3 -u {percorsoExec}/{mod.Comando} --info {target} | sed -r \"{regex}\" | tee {nomeFile}.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloSUBLIST3R)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "Sublist3r");
                string comando = $"python {percorsoExec}/{mod.Comando} -d {target} -o {nomeFile}.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloWAPITI)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wapiti");
                string comando = $"python3 {percorsoExec}/{mod.Comando} -u {target} -f txt -o {nomeFile}.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloOPENDOOR)
            {
                string comando = $"cd /usr/local/bin && {mod.Comando} --host {target} | sed -r \"{regex}\" | tee {Path.Combine(Globals.CARTELLAREPORT, cartella, nomeFile)}opendoor.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile + "opendoor.txt"));
                return comando;
            }
            if (mod is ModuloSQLMAP)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "sqlmap");
                string comando = $"python {percorsoExec}/{mod.Comando} -u {target} | sed -r \"{regex}\" | tee {nomeFile}.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloWIFITE)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wifite2");
                string comando = $"sudo python {percorsoExec}/{mod.Comando}";
                return comando;
            }
            if (mod is ModuloNOSQL)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "NoSQLMap");
                string comando = $"python {percorsoExec}/{mod.Comando}";
                return comando;
            }
            if (mod is ModuloJOOMSCAN)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "joomscan");
                string comando = $"perl {percorsoExec}/{mod.Comando} -u {target} | sed -r \"{regex}\" | tee {nomeFile}.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloWPSCAN)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "wpscan");
                string comando = $"ruby {percorsoExec}/{mod.Comando} -u {target} --log {Path.Combine(Globals.CARTELLAREPORT, cartella, nomeFile).Replace(" ", @"\ ")}.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloWASCAN)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "WAScan");
                string comando = $"python -u {percorsoExec}/{mod.Comando} -u {target} | sed -r \"{regex}\" | tee {nomeFile}.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloDNSENUM)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "dnsenum");
                string comando = $"perl {percorsoExec}/{mod.Comando} {target} -o {nomeFile}.xml ";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".xml"));
                return comando;
            }
            if (mod is ModuloODAT)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "odat");
                string comando = $"python -u {percorsoExec}/{mod.Comando} -s {target} | sed -r \"{regex}\" | tee {nomeFile}.txt ";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }
            if (mod is ModuloTHEHARVESTER)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "theHarvester");
                string comando = $"python2 -u {percorsoExec}/{mod.Comando} -d {target} -f {nomeFile}.html";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".html"));
                return comando;
            }
            if (mod is ModuloAMASS)
            {
                string percorsoExec = Path.Combine(Globals.CartellaWEBMVA, "Programmi", "amass");
                string comando = $"python -u {percorsoExec}/{mod.Comando} -d {target} -o {nomeFile}.txt -visjs {nomeFile}.visjs.html ";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                percorsi.Add(Path.Combine(cartella, nomeFile + ".visjs.html"));
                return comando;
            }
            if (mod is ModuloDRUPWN)
            {
                string comando = $"{mod.Comando} {target} | sed -r \"{regex}\" | tee {nomeFile}.txt";
                percorsi.Add(Path.Combine(cartella, nomeFile + ".txt"));
                return comando;
            }



            else return "";
        }
    }
}
