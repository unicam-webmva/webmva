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
        // DA VEDERE PER L'OUTPUT DEI PROCESSI: PUO' FORSE PORTARE L'OUTPUT ALLA PAGINA IN MANIERA ASINCRONA?
        // https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why/7608823#7608823
        // GUARDARE ANCHE RISPOSTE SUCCESSIVE



        public static string EseguiCLI(this string cmd, string cartellaProgetto, bool finestra = true)
        {
            switch (Globals.SistemaOperativoAttuale)
            {
                case Win32NT:
                    return Batch(cmd, cartellaProgetto);
                case Unix:
                    if (finestra)
                        return BashXTERM(cmd, cartellaProgetto);
                    else return BashConShell(cmd, cartellaProgetto);
                default:
                    throw new ApplicationException("Non so come tu sia finito qui");
            }
        }

        //https://askubuntu.com/questions/46627/how-can-i-make-a-script-that-opens-terminal-windows-and-executes-commands-in-the
        private static string BashXTERM(string cmd, string cartellaDiLavoro)
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
                    Arguments = $"-c \"exec stdbuf -oL xterm -e '{escapedArgs}'\"",
                    RedirectStandardOutput = false,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                }
            };
            //Console.WriteLine(process.StartInfo.FileName + " " + process.StartInfo.Arguments);
            process.Start();
            //string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return "";
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