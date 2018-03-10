using System;
using System.Collections.Generic;
using System.Text;

namespace Jc.Scripts.Cryptography
{
    public static class Base64
    {
        public static string Encrypt(string plainText)
        {
            return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(plainText));
        }

        public static string Decrypt(string cipherText)
        {
            return ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(cipherText));
        }
    }
}
