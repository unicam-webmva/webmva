using System;
using System.Diagnostics;
using System.IO;

namespace webmva.Helpers
{
    /*
    https://loune.net/2017/06/running-shell-bash-commands-in-net-core/
    */


    public static class ShellExecute
    {
        
private enum OS{
    WINDOWS, UNIX, UNSUPPORTED
}
        private static OS RUNNINGOS=GetCurrentOS();
        private static OS GetCurrentOS()
        {
            string windir = Environment.GetEnvironmentVariable("windir");
            if (!string.IsNullOrEmpty(windir) && windir.Contains(@"\") && Directory.Exists(windir))
            {
                // Windows
                return OS.WINDOWS;
            }
            else if (File.Exists(@"/proc/sys/kernel/ostype"))
            {
                string osType = File.ReadAllText(@"/proc/sys/kernel/ostype");
                if (osType.StartsWith("Linux", StringComparison.OrdinalIgnoreCase))
                {
                    // Linux
                    return OS.UNIX;
                }
                else
                {
                    return OS.UNSUPPORTED;
                }
            }
            else if (File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
            {
                // OSX
                return OS.UNIX;
            }
            else
            {
                return OS.UNSUPPORTED;
            }
        }
        public static string EseguiCLI(this string cmd){
            switch (RUNNINGOS){
                case OS.WINDOWS:
                    return Batch(cmd);
                case OS.UNIX:
                    return Bash(cmd);
                case OS.UNSUPPORTED:
                    throw new NotSupportedException("Non capisco il tipo di sistema operativo!");
                default:
                    throw new ApplicationException("Non so come tu sia finito qui");
            }
        }
        private static string Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            escapedArgs = escapedArgs.Replace(" ", "\\ ");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = System.IO.Directory.GetCurrentDirectory(),
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
        private static string Batch(string cmd)
        {
            //var escapedArgs = cmd.Replace("\"", "\\\"");
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory = System.IO.Directory.GetCurrentDirectory(),
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