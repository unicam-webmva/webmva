using System;
using static System.PlatformID;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace webmva.Helpers
{
    /*
    https://loune.net/2017/06/running-shell-bash-commands-in-net-core/
    https://github.com/dotnet/corefx/issues/9729
    */


    public static class ShellExecute
    {
        
        
            
        public static string EseguiCLI(this string cmd, string cartellaProgetto){
            switch (Globals.SistemaOperativoAttuale){
                case Win32NT:
                    return Batch(cmd, cartellaProgetto);
                case Unix:
                    return Bash(cmd, cartellaProgetto);
                default:
                    throw new ApplicationException("Non so come tu sia finito qui");
            }
        }
        private static string Bash(string cmd, string cartellaProgetto)
        {
            CreaCartellaProgetto(cartellaProgetto);
            var escapedArgs = cmd.Replace("\"", "\\\"");
            //escapedArgs = escapedArgs.Replace(" ", "\\ ");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Path.Combine(Globals.CartellaTuttiProgetti,cartellaProgetto),
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
        private static string Batch(string cmd, string cartellaProgetto)
        {
            CreaCartellaProgetto(cartellaProgetto);
            //var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = Path.Combine(Globals.CartellaTuttiProgetti, cartellaProgetto),
                    FileName = "cmd.exe",
                    Arguments = "/c " + cmd,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
        private static void CreaCartellaProgetto(string cartellaProgetto){
            // mi assicuro che la cartella dedicata al progetto esista
            // altrimenti la creo
            if(!Directory.Exists(Path.Combine(Globals.CartellaTuttiProgetti, cartellaProgetto))) 
                Directory.CreateDirectory(Path.Combine(Globals.CartellaTuttiProgetti, cartellaProgetto));
        }
    }
}