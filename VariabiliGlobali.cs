using System;
using System.IO;

namespace webmva
{
    public static class Globals
    {
        public static readonly PlatformID SistemaOperativoAttuale = Environment.OSVersion.Platform;
        public static readonly string CartellaTuttiProgetti = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "webmvaProjects");
    }
}