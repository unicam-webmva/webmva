using System;
using System.IO;

namespace webmva
{
    public static class MyLogger
    {
        public static void Log(string messaggio)
        {
            if (!Globals.LOGGING) return;
            string riga = $"[{DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss")}] {messaggio}";
            Console.WriteLine(riga);
            File.AppendAllText(Globals.LOGFILE, riga + "\n");
        }
        public static void Log(string messaggio, string controller, string metodo)
        {
            if (!Globals.LOGGING) return;
            string riga = $"[{DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss")}] {controller}:{metodo} - {messaggio}";
            Console.WriteLine(riga);
            File.AppendAllText(Globals.LOGFILE, riga + "\n");
        }
    }
}