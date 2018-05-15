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
            return View(new ListaModuliVM { ModuliNMAP = listaNMAP, ModuliNESSUS= listaNESSUS, ModuliDNSRECON = listaDNSRECON });
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

            if (editmodulo.NMAP != null)
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
            else if (editmodulo.NESSUS!= null)
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
