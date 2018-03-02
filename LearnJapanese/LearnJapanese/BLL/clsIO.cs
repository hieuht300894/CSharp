using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LearnJapanese
{
    public class clsIO
    {
        public static List<string> LoadFilesFromDirectory(string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                    return new List<string>();
                return Directory.GetFiles(dir).Select(x => string.Format(@"{0}\{1}", dir, Path.GetFileName(x))).ToList();
            }
            catch { return new List<string>(); }
        }

        public static string LoadText(string dir, string fileName)
        {
            try
            {
                string path = string.Format(@"{0}\{1}", dir, fileName);
                if (!File.Exists(path))
                    return string.Empty;
                string strMain = File.ReadAllText(path) ?? string.Empty;
                return strMain;
            }
            catch { return string.Empty; }
        }
        public static string LoadText(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return string.Empty;
                string strMain = File.ReadAllText(path) ?? string.Empty;
                return strMain;
            }
            catch { return string.Empty; }
        }
        public static List<string> LoadTexts(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return new List<string>();

                return File.ReadAllLines(path).ToList();
            }
            catch {return new List<string>(); }
        }
    }
}
