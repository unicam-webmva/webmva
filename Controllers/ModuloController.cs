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
            return View(new ListaModuliVM { ModuliNMAP = listaNMAP, ModuliNESSUS= listaNESSUS,ModuliOPENDOOR= listaOPENDOOR, ModuliDNSRECON = listaDNSRECON, ModuliFIERCE= listaFIERCE, ModuliDROOPE= listaDROOPE,ModuliJOOMSCAN=listaJOOMSCAN,ModuliWPSCAN=listaWPSCAN, ModuliINFOGA =listaINFOGA,ModuliINFOGAEMAIL = listaINFOGAEMAIL, ModuliSUBLIST3R = listaSUBLIST3R, ModuliWAPITI = listaWAPITI, ModuliSQLMAP = listaSQLMAP, ModuliWIFITE = listaWIFITE, ModuliWASCAN =listaWASCAN, ModuliNOSQL = listaNOSQL, ModuliODAT = listaODAT, ModuliDNSENUM = listaDNSENUM, ModuliOPENVAS = listaOPENVAS });
        }

        // GET: Modulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulo = await _context.Moduli
                .SingleOrDefaultAsync(m => m.ID == id);
            if (modulo == null)
            {
                return NotFound();
            }

            return View(modulo);
        }

        // GET: Modulo/Create
        public IActionResult Create()
        {
            var model = TempData.Peek<EditModuloVM>("Model");
            if (model!=null){
                TempData.Remove("Model");
                var anchor = TempData.Peek<string>("Anchor");
                TempData.Remove("Anchor");
                ViewData["Anchor"] = anchor;
                if(anchor=="Nessus"){
                    ViewData["TestN"] = TempData.Peek<string>("TestN");
                    TempData.Remove("TestN");
                }
                else if(anchor=="Openvas"){
                    ViewData["TestO"] = TempData.Peek<string>("TestO");
                    TempData.Remove("TestO");
                }
                return View(model);
            }
            else
            return View(new EditModuloVM());
        }

        // POST: Modulo/CreateNMAP
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
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            else return BadRequest();

            return View(createmodulo);

        }
        // POST: Modulo/Test
        [HttpPost]
        public async Task<IActionResult>Test(EditModuloVM createmodulo, string cosa){
            if(cosa== "nessus"){
                bool check = await CheckServer(createmodulo.NESSUS.ServerIP, createmodulo.NESSUS.Porta);
                TempData.Put("TestN", check.ToString());
                TempData.Put("Anchor", "Nessus");
                
                
            } 
            else if(cosa == "openvas"){
                bool check = await CheckServer(createmodulo.OPENVAS.ServerIPOpenvas, createmodulo.OPENVAS.PortaOpenvas);
                TempData.Put("TestO", check.ToString());
                TempData.Put("Anchor", "Openvas");
                
            }
            TempData.Put("Model", createmodulo);
            //return View(nameof(Create),createmodulo);
            return RedirectToAction("Create", "Modulo");
            
        }
        // GET: Modulo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulo = await _context.Moduli.SingleOrDefaultAsync(m => m.ID == id);
            if (modulo == null)
            {
                return NotFound();
            }
            if (modulo is ModuloNMAP)
                return View(new EditModuloVM((ModuloNMAP) modulo));
            else if (modulo is ModuloNESSUS)
                return View(new EditModuloVM((ModuloNESSUS) modulo));
            else if (modulo is ModuloDNSRECON)
                return View(new EditModuloVM((ModuloDNSRECON) modulo));
            else if (modulo is ModuloDROOPE)
                return View(new EditModuloVM((ModuloDROOPE) modulo));
             else if (modulo is ModuloFIERCE)
                return View(new EditModuloVM((ModuloFIERCE) modulo));    
            else if (modulo is ModuloJOOMSCAN)
                return View(new EditModuloVM((ModuloJOOMSCAN) modulo));
             else if (modulo is ModuloWPSCAN)
                return View(new EditModuloVM((ModuloWPSCAN) modulo));    
            else if (modulo is ModuloINFOGA)
                return View(new EditModuloVM((ModuloINFOGA) modulo));
            else if (modulo is ModuloINFOGAEMAIL)
                return View(new EditModuloVM((ModuloINFOGAEMAIL) modulo));
             else if (modulo is ModuloSUBLIST3R)
                return View(new EditModuloVM((ModuloSUBLIST3R) modulo));    
            else if (modulo is ModuloWAPITI)
                return View(new EditModuloVM((ModuloWAPITI) modulo));
            else if (modulo is ModuloSQLMAP)
                return View(new EditModuloVM((ModuloSQLMAP) modulo));
            else if (modulo is ModuloWIFITE)
                return View(new EditModuloVM((ModuloWIFITE) modulo));
            else if (modulo is ModuloOPENDOOR)
                return View(new EditModuloVM((ModuloOPENDOOR) modulo));
            else if (modulo is ModuloWASCAN)
                return View(new EditModuloVM((ModuloWASCAN) modulo));
            else if (modulo is ModuloNOSQL)
                return View(new EditModuloVM((ModuloNOSQL) modulo));
            else if (modulo is ModuloODAT)
                return View(new EditModuloVM((ModuloODAT) modulo)); 
            else if (modulo is ModuloDNSENUM)
                return View(new EditModuloVM((ModuloDNSENUM) modulo)); 
            else if (modulo is ModuloOPENVAS)
                return View(new EditModuloVM((ModuloOPENVAS) modulo));                
            // PROVVISORIO, SOLO PER NON DARE ERRORI DI COMPILAZIONE
            else return View(new EditModuloVM() );
        }

        // POST: Modulo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditModuloVM editmodulo)
        {

            if (!string.IsNullOrEmpty(editmodulo.NMAP.Nome))
            {
                ModuloNMAP mod = editmodulo.NMAP;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            else if (!string.IsNullOrEmpty(editmodulo.NESSUS.Nome))
            {
                ModuloNESSUS mod = editmodulo.NESSUS;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            else if (!string.IsNullOrEmpty(editmodulo.DNSRECON.Nome))
            {
                ModuloDNSRECON mod = editmodulo.DNSRECON;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.FIERCE.Nome))
            {
                ModuloFIERCE mod = editmodulo.FIERCE;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.DROOPE.Nome))
            {
                ModuloDROOPE mod = editmodulo.DROOPE;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.JOOMSCAN.Nome))
            {
                ModuloJOOMSCAN mod = editmodulo.JOOMSCAN;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.WPSCAN.Nome))
            {
                ModuloWPSCAN mod = editmodulo.WPSCAN;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.INFOGA.Nome))
            {
                ModuloINFOGA mod = editmodulo.INFOGA;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.INFOGAEMAIL.Nome))
            {
                ModuloINFOGAEMAIL mod = editmodulo.INFOGAEMAIL;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.SUBLIST3R.Nome))
            {
                ModuloSUBLIST3R mod = editmodulo.SUBLIST3R;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.WASCAN.Nome))
            {
                ModuloWASCAN mod = editmodulo.WASCAN;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            else if (!string.IsNullOrEmpty(editmodulo.OPENDOOR.Nome))
            {
                ModuloOPENDOOR mod = editmodulo.OPENDOOR;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            else if (!string.IsNullOrEmpty(editmodulo.WAPITI.Nome))
            {
                ModuloWAPITI mod = editmodulo.WAPITI;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.SUBLIST3R.Nome))
            {
                ModuloSUBLIST3R mod = editmodulo.SUBLIST3R;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            else if (!string.IsNullOrEmpty(editmodulo.SQLMAP.Nome))
            {
                ModuloSQLMAP mod = editmodulo.SQLMAP;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            else if (!string.IsNullOrEmpty(editmodulo.WIFITE.Nome))
            {
                ModuloWIFITE mod = editmodulo.WIFITE;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            else if (!string.IsNullOrEmpty(editmodulo.NOSQL.Nome))
            {
                ModuloNOSQL mod = editmodulo.NOSQL;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
              else if (!string.IsNullOrEmpty(editmodulo.ODAT.Nome))
            {
                ModuloODAT mod = editmodulo.ODAT;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            else if (!string.IsNullOrEmpty(editmodulo.DNSENUM.Nome))
            {
                ModuloDNSENUM mod = editmodulo.DNSENUM;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
             else if (!string.IsNullOrEmpty(editmodulo.OPENVAS.Nome))
            {
                ModuloOPENVAS mod = editmodulo.OPENVAS;
                if (id != mod.ID)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(mod);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ModuloExists(mod.ID))
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
            }
            return View(editmodulo);
        }
        

        // GET: Modulo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modulo = await _context.Moduli
                .SingleOrDefaultAsync(m => m.ID == id);
            if (modulo == null)
            {
                return NotFound();
            }
            var progetti = _context.ModuliProgetto.Where(m=>m.ModuloID==modulo.ID).Select(x =>x.Progetto.Nome).ToList();
            
            ViewData["Progetti"]= progetti;

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

                using (var client = new HttpClient(handler)){
                    // timeout per la richiesta
                    client.Timeout = new TimeSpan(0,0,5);
                    Uri ppp = new Uri($"https://{ip}:{port}");
                    Console.WriteLine(ppp.ToString());
                    try{
                        var result = await client.GetAsync(ppp);
                        return result.IsSuccessStatusCode;
                    }
                    // se finisco qui sono passati più di 5 secondi e il server non ha risposto, lo do per morto
                    catch(System.Threading.Tasks.TaskCanceledException /*e*/){
                        return false;
                    }
                    catch(Exception e){
                        Console.WriteLine(e.Message);
                        return false;
                    }
                    finally{
                        client.Dispose();
                    }
                }
            }


        }
    }
}
