using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientKVM
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
