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
            
            string[] dips = checkDipendenze.EseguiCLI(Globals.CartellaWEBMVA, false).Split(' ');
            ViewData["Dipendenze"] = dips;
            MyLogger.Log(messaggio: "Richiesta GET", controller: "HomeController", metodo: "Index");
            return View();
        }

        public IActionResult Error()
        {
            MyLogger.Log(messaggio: "ERRORE: " + Activity.Current?.Id ?? HttpContext.TraceIdentifier, controller: "HomeController", metodo: "Error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Installa()
        {
            MyLogger.Log(messaggio: "Installazione dipendenze iniziata", controller: "HomeController", metodo: "Installa");
            var HOME = Environment.GetEnvironmentVariable("HOME");
            string installa = Path.Combine(Globals.CartellaWEBMVA, $"install.sh {HOME}").EseguiCLI(Globals.CartellaWEBMVA, true);
            MyLogger.Log(messaggio: "Installazione dipendenze finita", controller: "HomeController", metodo: "Installa");
            return RedirectToAction(nameof(Index));
        }
       
    }
}
