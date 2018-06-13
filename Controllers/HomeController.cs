using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webmva.Helpers;
using webmva.Models;

namespace webmva.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string checkDipendenze = Path.Combine(Globals.CartellaWEBMVA, "Script", "testDipendenzeBase.sh");
            
            string[] dips = checkDipendenze.EseguiCLI(Globals.CartellaWEBMVA, true).Split(' ');
            ViewData["Dipendenze"] = dips;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Installa()
        {
            string installa = Path.Combine(Globals.CartellaWEBMVA, "Script", "installazionedepsWEBMVA.sh").EseguiCLI(Globals.CartellaWEBMVA, true);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
