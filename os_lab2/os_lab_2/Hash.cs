using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace os_lab_2
{
    class Hash
    {
        internal static string GetHash(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            //using (var sha = new SHA256Managed())  //MD5CryptoServiceProvider
            using (var hash = new SHA256Managed())//MD5.Ceate
            {
                //var result = "";
                byte[] textData = Encoding.UTF8.GetBytes(password);
                byte[] getHash = hash.ComputeHash(textData);
                
                //return result;
                return BitConverter.ToString(getHash).Replace("-", string.Empty);
            }
        }
    }
}
