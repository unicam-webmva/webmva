using System;
using System.IO;

namespace webmva
{
    public static class MyLogger
    {
        public static void Log(string messaggio)
        {
            if(!Globals.LOGGING) return;
            string riga = $"[{DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss")}] webmva - {messaggio}";
            Console.WriteLine(riga);
            File.AppendAllText(Globals.LOGFILE, riga+"\n");
        }
        public static void Log(string messaggio, string controller, string metodo)
        {
            if(!Globals.LOGGING) return;
            string riga = $"[{DateTime.Now.ToString("dd-MM-yyyy_HH:mm:ss")}] webmva - {controller}:{metodo} - {messaggio}";
            Console.WriteLine(riga);
            File.AppendAllText(Globals.LOGFILE, riga+"\n");
        }
    }
}