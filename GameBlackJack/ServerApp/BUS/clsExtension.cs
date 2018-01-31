using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace ServerApp
{
    public static class clsExtension
    {
        #region Command
        /// <summary>
        /// Parse a string "[key]=[value]" to key and value
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void ParseCommand(this string msg, out string key, out string value)
        {
            try
            {
                Match match = clsGeneral.regexCommand.Match(msg);
                if (!match.Success)
                    throw new Exception();

                key = match.Groups["Key"] != null ? match.Groups["Key"].Value : string.Empty;
                value = match.Groups["Value"] != null ? match.Groups["Value"].Value : string.Empty;
            }
            catch
            {
                key = string.Empty;
                value = string.Empty;
            }
        }
        /// <summary>
        /// convert key and value to a string "[key]=[value]"
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertCommand(this string key, string value)
        {
            return string.Format("[{0}]=[{1}]", key, value);
        }
        /// <summary>
        /// Check a string format is correct
        /// </summary>
        /// <param name="strMain"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool CheckFormat(this string strMain, Regex regex)
        {
            try { return regex.IsMatch(strMain); }
            catch { return false; }
        }
        /// <summary>
        /// Parse address string to address object
        /// </summary>
        /// <param name="strMain"></param>
        /// <returns></returns>
        public static IPEndPoint ParseAddress(this string strMain)
        {
            try
            {
                Match match = clsGeneral.regexAddress.Match(strMain);
                if (!match.Success)
                    throw new Exception();

                string ip = match.Groups["IP"] != null ? match.Groups["IP"].Value : string.Empty;
                int port = Convert.ToInt32(match.Groups["Port"].Value);

                if (!ip.IsEmpty() && port > 0)
                    return new IPEndPoint(IPAddress.Parse(ip), port);
            }
            catch { }
            return null;
        }
        #endregion

        #region XMl
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oSource"></param>
        /// <returns></returns>
        public static XmlDocument StringToXml(this string oSource)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(oSource);
            return xml;
        }
        /// <summary>
        /// Parse object to xml string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oSource"></param>
        /// <returns></returns>
        public static string SerializeObjectToXML<T>(this T oSource) where T : class, new()
        {
            if (oSource == null) return string.Empty;

            XmlSerializer xmlSerial = new XmlSerializer(typeof(T));
            using (StringWriter strWriter = new StringWriter())
            {
                xmlSerial.Serialize(strWriter, oSource);
                return strWriter.ToString();
            }
        }
        /// <summary>
        /// Parse list object to xml string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oSource"></param>
        /// <returns></returns>
        public static string SerializeListObjectToXML<T>(this List<T> oSource) where T : class, new()
        {
            if (oSource == null) return string.Empty;

            XmlSerializer xmlSerial = new XmlSerializer(typeof(List<T>));
            using (StringWriter strWriter = new StringWriter())
            {
                xmlSerial.Serialize(strWriter, oSource);
                return strWriter.ToString();
            }
        }
        /// <summary>
        /// Convert xml string to object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXML"></param>
        /// <returns></returns>
        public static T DeserializeXMLToObject<T>(this string strXML) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(strXML)) return new T();

            XmlSerializer xmlSerial = new XmlSerializer(typeof(T));
            using (StringReader strReader = new StringReader(strXML))
            {
                return (T)xmlSerial.Deserialize(strReader) ?? new T();
            }
        }
        /// <summary>
        /// Convert xml string to list object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strXML"></param>
        /// <returns></returns>
        public static List<T> DeserializeXMLToListObject<T>(this string strXML) where T : class, new()
        {
            if (string.IsNullOrEmpty(strXML)) return new List<T>();

            XmlSerializer xmlSerial = new XmlSerializer(typeof(List<T>));
            using (StringReader strReader = new StringReader(strXML))
            {
                return (List<T>)xmlSerial.Deserialize(strReader) ?? new List<T>();
            }
        }
        #endregion

        #region Control
        public static void SetError(this Control ctrMain, ErrorProvider error, string msg)
        {
            error = error ?? new ErrorProvider();
            error.SetIconAlignment(ctrMain, ErrorIconAlignment.MiddleRight);
            error.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError;
            error.SetError(ctrMain, msg);
        }
        public static void DisableEvent(this Control ctrMain, Action<object, EventArgs> delEvent)
        {
            ctrMain.SizeChanged -= (_s, _e) => delEvent?.Invoke(_s, _e);
        }
        public static void EnableEvent(this Control ctrMain, Action<object, EventArgs> delEvent)
        {
            ctrMain.SizeChanged -= (_s, _e) => delEvent?.Invoke(_s, _e);
        }
        public static void BeginInvokeExt(this Control ctrMain, Action action)
        {
            try
            {
                if (ctrMain.InvokeRequired)
                    ctrMain.EndInvoke(ctrMain.BeginInvoke(action));
                else
                    action();
            }
            catch { }
        }
        public static void InvokeExt(this Control ctrMain, Action action)
        {
            try
            {
                if (ctrMain.InvokeRequired)
                    ctrMain.Invoke(action);
                else
                    action();
            }
            catch { }
        }
        #endregion

        #region String
        public static bool IsEmpty(this string strMain)
        {
            return string.IsNullOrWhiteSpace(strMain);
        }
        #endregion
    }
}
