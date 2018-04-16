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
            return View(new ListaModuliVM { ModuliNMAP = listaNMAP, ModuliNESSUS= listaNESSUS });
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
            return View();
        }

        // POST: Modulo/CreateNMAP
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNMAP([Bind("ID,Nome,Applicazione,TCPScan,NonTCPScan,NoHostDiscovery,SynDiscoveryPorts,AckDiscoveryPorts,UdpDiscoveryPorts,ArpDiscovery,NoDNSResolution,ListSpecificPort,ScanAllPorts,FastScan,ServiceVersion,OSdetection,OSDetectionAggressive,AllDetections,Tempo,Fragmented,IPv6Scan,IncreaseVerbosity,ComandoPersonalizzato")] ModuloNMAP modulo)
        {
            if (ModelState.IsValid)
            {
                _context.Moduli.Add(modulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modulo);
        }

        // POST: Modulo/CreateNESSUS
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNESSUS([Bind("ID,Nome,Applicazione,JSON")] ModuloNESSUS modulo)
        {
            if (ModelState.IsValid)
            {
                _context.Moduli.Add(modulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modulo);
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
                return View(new EditModuloVM { NMAP = (ModuloNMAP)modulo });
            else if (modulo is ModuloNESSUS)
                return View(new EditModuloVM { NESSUS = (ModuloNESSUS)modulo });
            // PROVVISORIO, SOLO PER NON DARE ERRORI DI COMPILAZIONE
            else return View(new EditModuloVM { NMAP = (ModuloNMAP)modulo });
        }

        // POST: Modulo/EditNMAP/5
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

        // POST: Modulo/EditNMAP/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNESSUS(int id, [Bind("ID,Nome,Applicazione,JSON")] ModuloNESSUS modulo)
        {
            if (id != modulo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuloExists(modulo.ID))
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
            return View(new EditModuloVM { NESSUS = modulo });
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
