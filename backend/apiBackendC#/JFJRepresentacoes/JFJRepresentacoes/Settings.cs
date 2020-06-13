using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JFJRepresentacoes
{
    public static class Settings
    {
        public static string Secret = "0E7ABB89019388DAC739E3CDBA66BF045D8A7284346131487992A1A9C065637F8D64D8CE23BE410D111B33724B202C67265E90E037A7BAAA32510484472032CF";

        public static string HashPassword(string password)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(password));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }


    }
}
