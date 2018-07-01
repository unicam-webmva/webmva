using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webmva.Data;
using webmva.Models;
using webmva.ViewModels;

namespace webmva.Controllers
{
    public class ModuloController : Controller
    {
        private readonly MyDbContext _context;

        public ModuloController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Modulo
        public async Task<IActionResult> Index()
        {
            var listaNMAP = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.NMAP).ToListAsync();
            var listaNESSUS = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.NESSUS).ToListAsync();
            var listaDNSRECON = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.DNSRECON).ToListAsync();
            var listaFIERCE = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.FIERCE).ToListAsync();
            var listaDROOPE = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.DROOPE).ToListAsync();
            var listaJOOMSCAN = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.JOOMSCAN).ToListAsync();
            var listaOPENDOOR = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.OPENDOOR).ToListAsync();
            var listaWPSCAN = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.WPSCAN).ToListAsync();
            var listaINFOGA = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.INFOGA).ToListAsync();
            var listaINFOGAEMAIL = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.INFOGAEMAIL).ToListAsync();
            var listaSUBLIST3R = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.SUBLIST3R).ToListAsync();
            var listaWAPITI = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.WAPITI).ToListAsync();
            var listaSQLMAP = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.SQLMAP).ToListAsync();
            var listaWIFITE = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.WIFITE).ToListAsync();
            var listaWASCAN = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.WASCAN).ToListAsync();
            var listaNOSQL = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.NOSQL).ToListAsync();
            var listaODAT = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.ODAT).ToListAsync();
            var listaDNSENUM = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.DNSENUM).ToListAsync();
            var listaOPENVAS = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.OPENVAS).ToListAsync();
            var listaTHEHARVESTER = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.THEHARVESTER).ToListAsync();
            var listaAMASS = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.AMASS).ToListAsync();
            MyLogger.Log(messaggio: "Richiesta GET", controller: "ModuloController", metodo: "Index");
            return View(new ListaModuliVM { ModuliNMAP = listaNMAP, ModuliNESSUS = listaNESSUS, ModuliOPENDOOR = listaOPENDOOR, ModuliDNSRECON = listaDNSRECON, ModuliFIERCE = listaFIERCE, ModuliDROOPE = listaDROOPE, ModuliJOOMSCAN = listaJOOMSCAN, ModuliWPSCAN = listaWPSCAN, ModuliINFOGA = listaINFOGA, ModuliINFOGAEMAIL = listaINFOGAEMAIL, ModuliSUBLIST3R = listaSUBLIST3R, ModuliWAPITI = listaWAPITI, ModuliSQLMAP = listaSQLMAP, ModuliWIFITE = listaWIFITE, ModuliWASCAN = listaWASCAN, ModuliNOSQL = listaNOSQL, ModuliODAT = listaODAT, ModuliDNSENUM = listaDNSENUM, ModuliOPENVAS = listaOPENVAS, ModuliTHEHARVESTER = listaTHEHARVESTER, ModuliAMASS = listaAMASS });
        }

        // GET: Modulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ModuloController", metodo: "Details");
                return NotFound();
            }

            var modulo = await _context.Moduli
                .SingleOrDefaultAsync(m => m.ID == id);
            if (modulo == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun modulo con questo id", controller: "ModuloController", metodo: "Details");
                return NotFound();
            }
            MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ModuloController", metodo: "Details");
            return View(modulo);
        }

        // GET: Modulo/Create
        public IActionResult Create()
        {
            var model = TempData.Peek<EditModuloVM>("Model");
            if (model != null)
            {
                TempData.Remove("Model");
                var anchor = TempData.Peek<string>("Anchor");
                TempData.Remove("Anchor");
                ViewData["Anchor"] = anchor;
                if (anchor == "Nessus")
                {
                    ViewData["TestN"] = TempData.Peek<string>("TestN");
                    TempData.Remove("TestN");
                }
                else if (anchor == "Openvas")
                {
                    ViewData["TestO"] = TempData.Peek<string>("TestO");
                    TempData.Remove("TestO");
                }
                MyLogger.Log(messaggio: $"Richiesta GET in seguito a Test", controller: "ModuloController", metodo: "Create");
                return View(model);
            }
            else
                MyLogger.Log(messaggio: $"Richiesta GET", controller: "ModuloController", metodo: "Create");
                return View(new EditModuloVM());
        }

        // POST: Modulo/
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string cosa, EditModuloVM createmodulo)
        {
            if (createmodulo.NMAP.Nome != null && cosa.Equals("nmap"))
            {
                if (ModelState.IsValid)
                {
                    ModuloNMAP mod = createmodulo.NMAP;
                    mod.Applicazione = APPLICAZIONE.NMAP;
                    _context.Moduli.Add(mod);
                    await _context.SaveChangesAsync();
                    MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo nmap con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (createmodulo.NESSUS.Nome != null && cosa.Equals("nessus"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloNESSUS mod = createmodulo.NESSUS;
                        mod.Applicazione = APPLICAZIONE.NESSUS;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Nessus con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.DNSRECON.Nome != null && cosa.Equals("dnsrecon"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloDNSRECON mod = createmodulo.DNSRECON;
                        mod.Applicazione = APPLICAZIONE.DNSRECON;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo DNSRecon con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.FIERCE.Nome != null && cosa.Equals("fierce"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloFIERCE mod = createmodulo.FIERCE;
                        mod.Applicazione = APPLICAZIONE.FIERCE;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Fierce con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.DROOPE.Nome != null && cosa.Equals("droopescan"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloDROOPE mod = createmodulo.DROOPE;
                        mod.Applicazione = APPLICAZIONE.DROOPE;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo DroopeScan con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.JOOMSCAN.Nome != null && cosa.Equals("joomscan"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloJOOMSCAN mod = createmodulo.JOOMSCAN;
                        mod.Applicazione = APPLICAZIONE.JOOMSCAN;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo JoomScan con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.WPSCAN.Nome != null && cosa.Equals("wpscan"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloWPSCAN mod = createmodulo.WPSCAN;
                        mod.Applicazione = APPLICAZIONE.WPSCAN;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo WPScan con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.INFOGA.Nome != null && cosa.Equals("infoga"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloINFOGA mod = createmodulo.INFOGA;
                        mod.Applicazione = APPLICAZIONE.INFOGA;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Infoga con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.INFOGAEMAIL.Nome != null && cosa.Equals("infogaemail"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloINFOGAEMAIL mod = createmodulo.INFOGAEMAIL;
                        mod.Applicazione = APPLICAZIONE.INFOGAEMAIL;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Infoga (email) con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.SUBLIST3R.Nome != null && cosa.Equals("sublist3r"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloSUBLIST3R mod = createmodulo.SUBLIST3R;
                        mod.Applicazione = APPLICAZIONE.SUBLIST3R;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Sublist3r con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.WAPITI.Nome != null && cosa.Equals("wapiti"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloWAPITI mod = createmodulo.WAPITI;
                        mod.Applicazione = APPLICAZIONE.WAPITI;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Wapiti con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.WASCAN.Nome != null && cosa.Equals("wascan"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloWASCAN mod = createmodulo.WASCAN;
                        mod.Applicazione = APPLICAZIONE.WASCAN;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Wascan con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.SQLMAP.Nome != null && cosa.Equals("sqlmap"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloSQLMAP mod = createmodulo.SQLMAP;
                        mod.Applicazione = APPLICAZIONE.SQLMAP;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo SQLMap con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.WIFITE.Nome != null && cosa.Equals("wifite"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloWIFITE mod = createmodulo.WIFITE;
                        mod.Applicazione = APPLICAZIONE.WIFITE;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Wifite con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.OPENDOOR.Nome != null && cosa.Equals("opendoor"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloOPENDOOR mod = createmodulo.OPENDOOR;
                        mod.Applicazione = APPLICAZIONE.OPENDOOR;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo OpenDoor con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.NOSQL.Nome != null && cosa.Equals("nosql"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloNOSQL mod = createmodulo.NOSQL;
                        mod.Applicazione = APPLICAZIONE.NOSQL;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo NoSQLMap con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.ODAT.Nome != null && cosa.Equals("odat"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloODAT mod = createmodulo.ODAT;
                        mod.Applicazione = APPLICAZIONE.ODAT;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo ODAT con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.DNSENUM.Nome != null && cosa.Equals("dnsEnum"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloDNSENUM mod = createmodulo.DNSENUM;
                        mod.Applicazione = APPLICAZIONE.DNSENUM;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo DNSEnum con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else if (createmodulo.OPENVAS.Nome != null && cosa.Equals("openvas"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloOPENVAS mod = createmodulo.OPENVAS;
                        mod.Applicazione = APPLICAZIONE.OPENVAS;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo OpenVAS con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
              else if (createmodulo.THEHARVESTER.Nome != null && cosa.Equals("theharvester"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloTHEHARVESTER mod = createmodulo.THEHARVESTER;
                        mod.Applicazione = APPLICAZIONE.THEHARVESTER;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo TheHarvester con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
               else if (createmodulo.AMASS.Nome != null && cosa.Equals("amass"))
            {
                {
                    if (ModelState.IsValid)
                    {
                        ModuloAMASS mod = createmodulo.AMASS;
                        mod.Applicazione = APPLICAZIONE.AMASS;
                        _context.Moduli.Add(mod);
                        await _context.SaveChangesAsync();
                        MyLogger.Log(messaggio: $"Richiesta POST: \n\tNuovo modulo Amass con nome: {mod.Nome}", controller: "ModuloController", metodo: "Create");
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: BadRequest", controller: "ModuloController", metodo: "Create");
                return BadRequest();
            }
            MyLogger.Log(messaggio: $"ERRORE: Richiesta POST: Richiesta malformata", controller: "ModuloController", metodo: "Create");
            return View(createmodulo);

        }
        // POST: Modulo/Test
        [HttpPost]
        public async Task<IActionResult> Test(EditModuloVM createmodulo, string cosa, string daDove)
        {
            if (cosa == "nessus")
            {
                bool check = await CheckServer(createmodulo.NESSUS.ServerIP, createmodulo.NESSUS.Porta);
                MyLogger.Log(messaggio: $"Richiesta POST: Test verso https://{createmodulo.NESSUS.ServerIP}:{createmodulo.NESSUS.Porta} "+
                    ((check)?"up":"down"), controller: "ModuloController", metodo: "Test");
                TempData.Put("TestN", check.ToString());
                TempData.Put("Anchor", "Nessus");


            }
            else if (cosa == "openvas")
            {
                bool check = await CheckServer(createmodulo.OPENVAS.ServerIPOpenvas, createmodulo.OPENVAS.PortaOpenvas);
                MyLogger.Log(messaggio: $"Richiesta POST: Test verso https://{createmodulo.OPENVAS.ServerIPOpenvas}:{createmodulo.OPENVAS.PortaOpenvas} "+
                    ((check)?"up":"down"), controller: "ModuloController", metodo: "Test");
                TempData.Put("TestO", check.ToString());
                TempData.Put("Anchor", "Openvas");

            }
            TempData.Put("Model", createmodulo);
            //return View(nameof(Create),createmodulo);
            return Redirect(Url.Action(daDove, "Modulo").Replace("%2F", "/"));

        }
        // GET: Modulo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var model = TempData.Peek<EditModuloVM>("Model");
            if (model != null)
            {
                Modulo m = null;
                TempData.Remove("Model");
                var anchor = TempData.Peek<string>("Anchor");
                TempData.Remove("Anchor");
                if (anchor == "Nessus")
                {
                    m = model.NESSUS;
                    ViewData["TestN"] = TempData.Peek<string>("TestN");
                    TempData.Remove("TestN");
                }
                else if (anchor == "Openvas")
                {
                    m = model.OPENVAS;
                    ViewData["TestO"] = TempData.Peek<string>("TestO");
                    TempData.Remove("TestO");
                }
                MyLogger.Log(messaggio: $"Richiesta GET con id {id} in seguito a Test", controller: "ModuloController", metodo: "Edit");
                return View(new EditModuloVM(m));
            }
            else
            {
                if (id == null)
                {
                    MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ModuloController", metodo: "Edit");
                    return NotFound();
                }
                var modulo = await _context.Moduli.SingleOrDefaultAsync(m => m.ID == id);
                if (modulo == null)
                {
                    MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun modulo con questo id", controller: "ModuloController", metodo: "Details");
                    return NotFound();
                }
                MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ModuloController", metodo: "Edit");
                return View(new EditModuloVM(modulo));
            }
        }

        // POST: Modulo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditModuloVM editmodulo)
        {
            Modulo m;

            if (!string.IsNullOrEmpty(editmodulo.NMAP.Nome))
                m = editmodulo.NMAP;
            else if (!string.IsNullOrEmpty(editmodulo.NESSUS.Nome))
                m = editmodulo.NESSUS;
            else if (!string.IsNullOrEmpty(editmodulo.DNSRECON.Nome))
                m = editmodulo.DNSRECON;
            else if (!string.IsNullOrEmpty(editmodulo.FIERCE.Nome))
                m = editmodulo.FIERCE;
            else if (!string.IsNullOrEmpty(editmodulo.DROOPE.Nome))
                m = editmodulo.DROOPE;
            else if (!string.IsNullOrEmpty(editmodulo.JOOMSCAN.Nome))
                m = editmodulo.JOOMSCAN;
            else if (!string.IsNullOrEmpty(editmodulo.WPSCAN.Nome))
                m = editmodulo.WPSCAN;
            else if (!string.IsNullOrEmpty(editmodulo.INFOGA.Nome))
                m = editmodulo.INFOGA;
            else if (!string.IsNullOrEmpty(editmodulo.INFOGAEMAIL.Nome))
                m = editmodulo.INFOGAEMAIL;
            else if (!string.IsNullOrEmpty(editmodulo.SUBLIST3R.Nome))
                m = editmodulo.SUBLIST3R;
            else if (!string.IsNullOrEmpty(editmodulo.WASCAN.Nome))
                m = editmodulo.WASCAN;
            else if (!string.IsNullOrEmpty(editmodulo.OPENDOOR.Nome))
                m = editmodulo.OPENDOOR;
            else if (!string.IsNullOrEmpty(editmodulo.WAPITI.Nome))
                m = editmodulo.WAPITI;
            else if (!string.IsNullOrEmpty(editmodulo.SUBLIST3R.Nome))
                m = editmodulo.SUBLIST3R;
            else if (!string.IsNullOrEmpty(editmodulo.SQLMAP.Nome))
                m = editmodulo.SQLMAP;
            else if (!string.IsNullOrEmpty(editmodulo.WIFITE.Nome))
                m = editmodulo.WIFITE;
            else if (!string.IsNullOrEmpty(editmodulo.NOSQL.Nome))
                m = editmodulo.NOSQL;
            else if (!string.IsNullOrEmpty(editmodulo.ODAT.Nome))
                m = editmodulo.ODAT;
            else if (!string.IsNullOrEmpty(editmodulo.DNSENUM.Nome))
                m = editmodulo.DNSENUM;
            else if (!string.IsNullOrEmpty(editmodulo.THEHARVESTER.Nome))
                m = editmodulo.THEHARVESTER;
            else if (!string.IsNullOrEmpty(editmodulo.AMASS.Nome))
                m = editmodulo.AMASS;
            else // è rimasto solo openvas, controllo dopo se m è ancora null o no
                m = editmodulo.OPENVAS;
            
            if (m==null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST con id {id}: BadRequest", controller: "ModuloController", metodo: "Edit");
                return BadRequest();
            }
            
            if (id != m.ID)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta POST con id {id}: Richiesta malformata", controller: "ModuloController", metodo: "Edit");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(m);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuloExists(m.ID))
                    {
                        MyLogger.Log(messaggio: $"ERRORE: Richiesta POST con id {id}: Nessun modulo con questo id", controller: "ModuloController", metodo: "Edit");
                        return NotFound();
                    }
                    else
                    {
                        MyLogger.Log(messaggio: $"ERRORE CRITICO: Richiesta POST con id {id}: Errore nel DB", controller: "ModuloController", metodo: "Edit");
                        throw;
                    }
                }
                MyLogger.Log(messaggio: $"Richiesta POST: \n\tModulo di tipo {m.Applicazione.ToString()} con nome: {m.Nome} modificato", controller: "ModuloController", metodo: "Edit");
                return RedirectToAction(nameof(Index));
            }
            
            return View(editmodulo);
        }


        // GET: Modulo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET: nessun id fornito", controller: "ModuloController", metodo: "Delete");
                return NotFound();
            }

            var modulo = await _context.Moduli
                .SingleOrDefaultAsync(m => m.ID == id);
            if (modulo == null)
            {
                MyLogger.Log(messaggio: $"ERRORE: Richiesta GET con id {id} fallita: nessun modulo con questo id", controller: "ModuloController", metodo: "Delete");
                return NotFound();
            }
            var progetti = _context.ModuliProgetto.Where(m => m.ModuloID == modulo.ID).Select(x => x.Progetto.Nome).ToList();

            ViewData["Progetti"] = progetti;
            MyLogger.Log(messaggio: $"Richiesta GET con id {id}", controller: "ModuloController", metodo: "Delete");
            return View(modulo);
        }

        // POST: Modulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modulo = await _context.Moduli.SingleOrDefaultAsync(m => m.ID == id);
            _context.Moduli.Remove(modulo);
            await _context.SaveChangesAsync();
            MyLogger.Log(messaggio: $"Richiesta POST con id {id}:\n\tModulo di tipo {modulo.Applicazione.ToString()} con nome {modulo.Nome} eliminato", controller: "ModuloController", metodo: "Delete");
            return RedirectToAction(nameof(Index));
        }

        private bool ModuloExists(int id)
        {
            return _context.Moduli.Any(e => e.ID == id);
        }
        private async Task<bool> CheckServer(string ip, int port)
        {
            using (var handler = new HttpClientHandler())
            {
                handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                using (var client = new HttpClient(handler))
                {
                    // timeout per la richiesta
                    client.Timeout = new TimeSpan(0, 0, 5);
                    Uri ppp = new Uri($"https://{ip}:{port}");
                    Console.WriteLine(ppp.ToString());
                    try
                    {
                        var result = await client.GetAsync(ppp);
                        return result.IsSuccessStatusCode;
                    }
                    // se finisco qui sono passati più di 5 secondi e il server non ha risposto, lo do per morto
                    catch (System.Threading.Tasks.TaskCanceledException /*e*/)
                    {
                        return false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                    finally
                    {
                        client.Dispose();
                    }
                }
            }
        }
    }
}