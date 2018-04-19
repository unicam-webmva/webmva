using System;
using System.IO;

namespace webmva
{
    public static class Globals
    {
        public static readonly PlatformID SistemaOperativoAttuale = Environment.OSVersion.Platform;
        public static readonly string CartellaTuttiProgetti = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "webmvaProjects");

        public static string ToCamelCase(this string stringa){
            if(stringa == null || stringa.Length<2) return stringa;
            string[] parole = stringa.Split(new char[]{ }, StringSplitOptions.RemoveEmptyEntries);
            string camelCase = parole[0].ToLower();
            for(int i = 1; i< parole.Length; i++){
                camelCase += parole[i].Substring(0,1).ToUpper() + parole[i].Substring(1);
            }
            return camelCase;
        }
    }
}