using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAppWebApi.EncryptDecrypt
{
    public static class EncryptingDecrypting
    {
        public static string key = "smartpay!@#$%^";

        public static string ConvertToEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }
            password += key;
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordToDataBase = Convert.ToBase64String(passwordBytes);
            return passwordToDataBase;

        }

        public static string ConvertToDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData))
            {
                return "";
            }
            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - key.Length);
            return result;
        }
    }
}
