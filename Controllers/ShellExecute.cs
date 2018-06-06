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
        
        
            
        public static string EseguiCLI(this string cmd, string cartellaProgetto, bool finestra = false){
            switch (Globals.SistemaOperativoAttuale){
                case Win32NT:
                    return Batch(cmd, cartellaProgetto);
                case Unix:
                if(finestra)
                    return BashConShell(cmd, cartellaProgetto);
                    else return Bash(cmd,cartellaProgetto);
                default:
                    throw new ApplicationException("Non so come tu sia finito qui");
            }
        }
        private static string Bash(string cmd, string cartellaDiLavoro)
        {
            //CreaCartellaProgetto(cartellaProgetto);
            var escapedArgs = cmd.Replace("\"", "\\\"");
            //escapedArgs = escapedArgs.Replace(" ", "\\ ");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = cartellaDiLavoro,
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
        private static string BashConShell(string cmd, string cartellaDiLavoro)
        {
            //CreaCartellaProgetto(cartellaProgetto);
            var escapedArgs = cmd.Replace("\"", "\\\"");
            //escapedArgs = escapedArgs.Replace(" ", "\\ ");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = cartellaDiLavoro,
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = false,
                    UseShellExecute = true,
                    CreateNoWindow = false,
                }
            };
            process.Start();
            
            process.WaitForExit();
            
            return process.ExitCode.ToString();
        }
        private static string Batch(string cmd, string cartellaDiLavoro)
        {
            //CreaCartellaProgetto(cartellaProgetto);
            //var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = cartellaDiLavoro,
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
        
    }
}