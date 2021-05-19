using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessTestClient
{
    public class Utils
    {
        /// <summary>将数组转换成16进制字符串格式</summary>
        /// <param name="data">数据</param>
        /// <returns>转换后的16进制字符串</returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            int length = data.Length;
            StringBuilder hex = new StringBuilder(length * 3);

            for (int i = 0; i < length; i++)
            {
                hex.Append(data[i].ToString("X2"));
                hex.Append(" ");
            }

            return hex.ToString();
        }

        /// <summary>将数组转换成16进制字符串格式</summary>
        /// <param name="data">数据</param>
        /// <param name="offset">数据偏移</param>
        /// <param name="length">数据长度</param>
        /// <returns>转换后的16进制字符串</returns>
        public static string ByteArrayToHexString(byte[] data, int offset, int length)
        {
            StringBuilder hex = new StringBuilder(length * 3);

            for (int i = 0; i < length; i++)
            {
                hex.Append(data[offset + i].ToString("X2"));
                hex.Append(" ");
            }

            return hex.ToString();
        }

        /// <summary>将byte数组转为字符串，例如{0x48, 0x65, 0x6C, 0x6C, 0x6F} => "Hello"</summary>
        /// <param name="data">数据</param>
        /// <param name="length">长度</param>
        /// <returns>转换后的字符串</returns>
        public static string ByteArrayToString(byte[] data)
        {
            return System.Text.Encoding.GetEncoding("UTF-8").GetString(data);
        }

        public static int GetLength(string str)
        {
            if (str.Length == 0)
                return 0;
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(str);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }
            }
            return tempLen;
        }

        public static bool IsKeyFormat(string s)
        {
            Regex regex = new Regex(@"^\[\w*\]$");
            if (regex.IsMatch(s))
            {
                return true;
            }
            return false;
        }
    }
}
