using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public static class clsIO
    {
        public static void SaveFile<T>(this T objMain, string dir, string fileName, string extension) where T : class, new()
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string path = string.Format(@"{0}\{1}.{2}", dir, fileName, extension);
                if (!File.Exists(path))
                    File.Create(path).Close();

                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(objMain.SerializeObjectToXML());
                sw.Close();
            }
            catch { }
        }
        public static void SaveLog(string dir, string fileName, string extension, int ColumnID, int RowID, string IP, int Port)
        {
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string path = string.Format(@"{0}\{1}.{2}", dir, fileName, extension);
                string text = string.Format(@"{0},{1},{2},{3},{4},{5}#.", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff"), clsGeneral.fKey.CLIENT.ToString(), ColumnID, RowID, IP, Port);
                if (!File.Exists(path))
                    File.Create(path).Close();
                else
                    text = string.Format(@"{0},{1},{2},{3},{4},{5}#.", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff"), clsGeneral.fKey.CLIENT.ToString(), ColumnID, RowID, " ", " ");

                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(text);
                sw.Close();
            }
            catch { }
        }
        public static T LoadFile<T>(string dir, string fileName, string extension) where T : class, new()
        {
            try
            {
                string path = string.Format(@"{0}\{1}.{2}", dir, fileName, extension);
                if (!File.Exists(path))
                    return new T();
                string strMain = File.ReadAllText(path) ?? string.Empty;
                return strMain.DeserializeXMLToObject<T>();
            }
            catch { return new T(); }
        }
    }
}
