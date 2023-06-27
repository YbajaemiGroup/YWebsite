using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YCore.Crypto
{
    public static class YCoreCrypto
    {
        public static string GetHash(string key)
        {
            byte[] input = Encoding.ASCII.GetBytes(key);
            byte[] hash = SHA256.HashData(input);
            return Convert.ToHexString(hash);
        }
    }
}
