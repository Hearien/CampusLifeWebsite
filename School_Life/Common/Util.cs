using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.IO;

namespace Common
{
    public class Util
    {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="strText">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Encrypt(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        public static bool isNull(String str)
        {
            if (str == null || str == "" || str.Equals(null) || str.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
