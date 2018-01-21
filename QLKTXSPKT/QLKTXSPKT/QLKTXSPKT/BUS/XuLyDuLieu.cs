using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class XuLyDuLieu
    {
        public static bool IsNumber(string str)
        {
            bool error = false;
            int i = 0;
            for (i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    break;
            }
            if (i == str.Length && str != "")
                error = true;
            return error;
        }
        public static string ChuyenDauThanhKhongDau(string str)
        {
            //Chữ thường
            str = str.Replace("ă", "a");
            str = str.Replace("ắ", "a");
            str = str.Replace("ằ", "a");
            str = str.Replace("ẳ", "a");
            str = str.Replace("ẵ", "a");
            str = str.Replace("ặ", "a");
            str = str.Replace("â", "a");
            str = str.Replace("ấ", "a");
            str = str.Replace("ầ", "a");
            str = str.Replace("ẩ", "a");
            str = str.Replace("ẫ", "a");
            str = str.Replace("ậ", "a");
            str = str.Replace("á", "a");
            str = str.Replace("à", "a");
            str = str.Replace("ả", "a");
            str = str.Replace("ã", "a");
            str = str.Replace("ạ", "a");
            str = str.Replace("đ", "d");
            str = str.Replace("e", "e");
            str = str.Replace("é", "e");
            str = str.Replace("è", "e");
            str = str.Replace("ẻ", "e");
            str = str.Replace("ẽ", "e");
            str = str.Replace("ẹ", "e");
            str = str.Replace("ê", "e");
            str = str.Replace("ế", "e");
            str = str.Replace("ề", "e");
            str = str.Replace("ể", "e");
            str = str.Replace("ễ", "e");
            str = str.Replace("ệ", "e");
            str = str.Replace("í", "i");
            str = str.Replace("ì", "i");
            str = str.Replace("ỉ", "i");
            str = str.Replace("ĩ", "i");
            str = str.Replace("ị", "i");
            str = str.Replace("ó", "o");
            str = str.Replace("ò", "o");
            str = str.Replace("ỏ", "o");
            str = str.Replace("õ", "o");
            str = str.Replace("ọ", "o");
            str = str.Replace("ô", "o");
            str = str.Replace("ố", "o");
            str = str.Replace("ồ", "o");
            str = str.Replace("ổ", "o");
            str = str.Replace("ỗ", "o");
            str = str.Replace("ộ", "o");
            str = str.Replace("ơ", "o");
            str = str.Replace("ớ", "o");
            str = str.Replace("ờ", "o");
            str = str.Replace("ở", "o");
            str = str.Replace("ỡ", "o");
            str = str.Replace("ợ", "o");
            str = str.Replace("ú", "u");
            str = str.Replace("ù", "u");
            str = str.Replace("ủ", "u");
            str = str.Replace("ũ", "u");
            str = str.Replace("ụ", "u");
            str = str.Replace("ư", "u");
            str = str.Replace("ứ", "u");
            str = str.Replace("ừ", "u");
            str = str.Replace("ử", "u");
            str = str.Replace("ữ", "u");
            str = str.Replace("ự", "u");
            str = str.Replace("ý", "y");
            str = str.Replace("ỳ", "y");
            str = str.Replace("ỷ", "y");
            str = str.Replace("ỹ", "y");
            str = str.Replace("ỵ", "y");
            //Chữ hoa
            str = str.Replace("Ă", "A");
            str = str.Replace("Ắ", "A");
            str = str.Replace("Ằ", "A");
            str = str.Replace("Ẳ", "A");
            str = str.Replace("Ẵ", "A");
            str = str.Replace("Ặ", "A");
            str = str.Replace("Â", "A");
            str = str.Replace("Ấ", "A");
            str = str.Replace("Ầ", "A");
            str = str.Replace("Ẩ", "A");
            str = str.Replace("Ẫ", "A");
            str = str.Replace("Ậ", "A");
            str = str.Replace("Á", "A");
            str = str.Replace("À", "A");
            str = str.Replace("Ả", "A");
            str = str.Replace("Ã", "A");
            str = str.Replace("Ạ", "A");
            str = str.Replace("Đ", "D");
            str = str.Replace("E", "E");
            str = str.Replace("É", "E");
            str = str.Replace("È", "E");
            str = str.Replace("Ẻ", "E");
            str = str.Replace("Ẽ", "E");
            str = str.Replace("Ẹ", "E");
            str = str.Replace("Ê", "E");
            str = str.Replace("Ế", "E");
            str = str.Replace("Ề", "E");
            str = str.Replace("Ể", "E");
            str = str.Replace("Ễ", "E");
            str = str.Replace("Ệ", "E");
            str = str.Replace("Í", "I");
            str = str.Replace("Ì", "I");
            str = str.Replace("Ỉ", "I");
            str = str.Replace("Ĩ", "I");
            str = str.Replace("Ị", "I");
            str = str.Replace("Ó", "O");
            str = str.Replace("Ò", "O");
            str = str.Replace("Ỏ", "O");
            str = str.Replace("Õ", "O");
            str = str.Replace("Ọ", "O");
            str = str.Replace("Ô", "O");
            str = str.Replace("Ố", "O");
            str = str.Replace("Ồ", "O");
            str = str.Replace("Ổ", "O");
            str = str.Replace("Ỗ", "O");
            str = str.Replace("Ộ", "O");
            str = str.Replace("Ơ", "O");
            str = str.Replace("Ớ", "O");
            str = str.Replace("Ờ", "O");
            str = str.Replace("Ở", "O");
            str = str.Replace("Ỡ", "O");
            str = str.Replace("Ợ", "O");
            str = str.Replace("Ú", "U");
            str = str.Replace("Ù", "U");
            str = str.Replace("Ủ", "U");
            str = str.Replace("Ũ", "U");
            str = str.Replace("Ụ", "U");
            str = str.Replace("Ư", "U");
            str = str.Replace("Ứ", "U");
            str = str.Replace("Ừ", "U");
            str = str.Replace("Ử", "U");
            str = str.Replace("Ữ", "U");
            str = str.Replace("Ự", "U");
            str = str.Replace("Ý", "Y");
            str = str.Replace("Ỳ", "Y");
            str = str.Replace("Ỷ", "Y");
            str = str.Replace("Ỹ", "Y");
            str = str.Replace("Ỵ", "Y");

            str = str.Replace(" ", "");
            return str;
        }
        private static string _sKeyEncrypt = "TestEncode";
        public static string sEncrypt(string szToEncrypt)
        {
            try
            {
                if (szToEncrypt == "")
                    return szToEncrypt;
                if (_sKeyEncrypt.Length > 8)
                    _sKeyEncrypt = _sKeyEncrypt.Substring(0, 8);

                DESCryptoServiceProvider cspDES = new DESCryptoServiceProvider();

                // Put the string into a byte array
                // Chuyen chuoi ky tu thanh chuoi byte
                byte[] inputByteArray = Encoding.UTF8.GetBytes(szToEncrypt);

                // Create the crypto objects, with the key, as passed in
                // Tao objects mat ma hoa thuat toan DES voi khoa dua vao
                cspDES.Key = ASCIIEncoding.ASCII.GetBytes(_sKeyEncrypt);
                cspDES.IV = ASCIIEncoding.ASCII.GetBytes(_sKeyEncrypt);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cspDES.CreateEncryptor(), CryptoStreamMode.Write);

                // Write the byte array into the crypto stream
                // (It will end up in the memory stream)
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                // Get the data back from the memory stream, and into a string
                StringBuilder szRet = new StringBuilder();

                // Chuyen sang dang ky tu hexa
                foreach (byte b in ms.ToArray())
                {
                    // Format as hex
                    szRet.AppendFormat("{0:X2}", b);
                }

                return szRet.ToString();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public static string MD5(string str)
        {
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(str))).Replace("-", string.Empty);
        }
        public static string sDecrypt(string szToDecrypt)
        {
            try
            {
                if (szToDecrypt == "") return szToDecrypt;
                if (_sKeyEncrypt.Length > 8) _sKeyEncrypt = _sKeyEncrypt.Substring(0, 8);

                DESCryptoServiceProvider cspDES = new DESCryptoServiceProvider();

                // Create the crypto objects
                cspDES.Key = ASCIIEncoding.ASCII.GetBytes(_sKeyEncrypt);
                cspDES.IV = ASCIIEncoding.ASCII.GetBytes(_sKeyEncrypt);

                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, cspDES.CreateDecryptor(), CryptoStreamMode.Write);
                StringBuilder szRet = new StringBuilder();

                // Put the input string into the byte array
                byte[] inputByteArray = new byte[szToDecrypt.Length / 2];
                int x;

                for (int i = 0; i < szToDecrypt.Length / 2; i++)
                {
                    x = Convert.ToInt32(szToDecrypt.Substring(i * 2, 2), 16);
                    inputByteArray[i] = (byte)x;
                }

                // Flush the data through the crypto stream into the memory stream
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                // Get the decrypted data back from the memory stream
                foreach (byte b in ms.ToArray())
                {
                    szRet.Append((char)b);
                }

                return szRet.ToString();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
