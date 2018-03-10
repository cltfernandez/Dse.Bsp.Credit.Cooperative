using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Jc.Scripts.Cryptography
{
    public static class Symmetric
    {
        public enum Processes
        {
            Encrypt,
            Decrypt
        }

        public enum Algorithms
        {
            Aes,
            Des,
            Rc2,
            Rijndael,
            TripleDes
        }

        public static string Encrypt(Algorithms algorithm, string plainText, string key, string iv)
        {
            SymmetricAlgorithm sa = GetServiceProvider(algorithm);
            return Process(Processes.Encrypt, sa, plainText, key, iv);
        }

        public static string Decrypt(Algorithms algorithm, string cipherText, string key, string iv)
        {
            SymmetricAlgorithm sa = GetServiceProvider(algorithm);
            return Process(Processes.Decrypt, sa, cipherText, key, iv);
        }

        private static SymmetricAlgorithm GetServiceProvider(Algorithms algorithm)
        {
            switch (algorithm)
            {
                case Algorithms.Aes:
                    return new AesCryptoServiceProvider();
                case Algorithms.Des:
                    return new DESCryptoServiceProvider();
                case Algorithms.Rc2:
                    return new RC2CryptoServiceProvider();
                case Algorithms.Rijndael:
                    return new RijndaelManaged();
                case Algorithms.TripleDes:
                    return new TripleDESCryptoServiceProvider();
                default:
                    return null;
            }
        }

        private static string Process(Processes process, SymmetricAlgorithm sa, string text, string key, string iv)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            ICryptoTransform cryptoTransform;
            byte[] binKey = SetBytes(encoder.GetBytes(key), (int)(sa.LegalKeySizes[0].MaxSize / 8));
            byte[] binIv = SetBytes(encoder.GetBytes(iv), (int)(sa.BlockSize / 8));
            byte[] bin;

            switch (process)
            {
                case Processes.Encrypt:
                    cryptoTransform = sa.CreateEncryptor(binKey, binIv);
                    bin = encoder.GetBytes(text);
                    bin = cryptoTransform.TransformFinalBlock(bin, 0, bin.Length);
                    text = Convert.ToBase64String(bin);
                    break;
                case Processes.Decrypt:
                    cryptoTransform = sa.CreateDecryptor(binKey, binIv);
                    bin = Convert.FromBase64String(text);
                    bin = cryptoTransform.TransformFinalBlock(bin, 0, bin.Length);
                    text = encoder.GetString(bin);
                    break;
            }

            return text;
        }

        private static byte[] SetBytes(byte[] bin, int max)
        {
            byte[] binaries = new byte[max];
            if (bin.Length < max)
            {
                Array.Copy(bin, binaries, bin.Length);
            }
            else if (bin.Length >= max)
            {
                Array.Copy(bin, binaries, binaries.Length);
            }
            return binaries;
        }
    }
}
