using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigAutomation
{
    public static class clsIO
    {
        public static void SaveFile<T>(this T objMain, string dir, string fileName, string extension) where T : class, new()
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string path = $@"{dir}\{fileName}.{extension}";
                if (!File.Exists(path))
                    File.Create(path).Close();

                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(objMain.SerializeObjectToXML());
                sw.Close();
            }
            catch { }
        }
        public static void SaveLog(string dir, string fileName, string extension, params string[] msg)
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string path = string.Format(@"{0}\{1}.{2}", dir, fileName, extension);

                if (!File.Exists(path))
                    File.Create(path).Close();

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    msg = msg ?? new string[] { };
                    msg.ToList().ForEach(x => sw.WriteLine(x));
                }
            }
            catch { }
        }
        public static T LoadFile<T>(string dir, string fileName, string extension) where T : class, new()
        {
            try
            {
                string path = $@"{dir}\{fileName}.{extension}";
                if (!File.Exists(path))
                    return new T();
                string strMain = File.ReadAllText(path) ?? string.Empty;
                return strMain.DeserializeXMLToObject<T>();
            }
            catch { return new T(); }
        }
    }
}
