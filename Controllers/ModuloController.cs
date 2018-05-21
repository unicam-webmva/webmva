using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webmva.Data;
using webmva.Models;
using webmva.ViewModels;

namespace webmva.Controllers_
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
            var listaDROOPE = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.DROOPE).ToListAsync();
            var listaINFOGA = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.INFOGA).ToListAsync();
            var listaWAPITI = await _context.Moduli.Where(modulo => modulo.Applicazione == APPLICAZIONE.WAPITI).ToListAsync();
            return View(new ListaModuliVM { ModuliNMAP = listaNMAP, ModuliNESSUS= listaNESSUS, ModuliDNSRECON = listaDNSRECON, ModuliDROOPE= listaDROOPE, ModuliINFOGA =listaINFOGA, ModuliWAPITI = listaWAPITI});
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
            else return BadRequest();

            return View(createmodulo);

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
            else if (modulo is ModuloINFOGA)
                return View(new EditModuloVM((ModuloINFOGA) modulo));
            else if (modulo is ModuloWAPITI)
                return View(new EditModuloVM((ModuloWAPITI) modulo));
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
    }
}
