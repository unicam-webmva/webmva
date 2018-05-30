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
            string checkDipendenze = Path.Combine(Globals.CartellaWEBMVA, "testDipendenzeBase.sh").EseguiCLI(Globals.CartellaWEBMVA);
            string[] dips = checkDipendenze.Split(' ');
            ViewData["Dipendenze"] = dips;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
