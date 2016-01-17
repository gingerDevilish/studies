using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace cryptor_joiner
{
    public class Cryptools
    {
        public static Byte[] TripleDES_Encrypt(Byte [] input, Byte[] key, Byte[] IV)
        {
            MemoryStream inStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(inStream, new TripleDESCryptoServiceProvider().CreateEncryptor(key, IV), CryptoStreamMode.Write);
            //Console.WriteLine("E: Streams created");
            //Byte[] toEncrypt = Encoding.Unicode.GetBytes(input);
            // Console.WriteLine("E: after ascii");
            cStream.Write(input, 0, input.Length);
            cStream.FlushFinalBlock();
            Byte[] output = inStream.ToArray();
            //Console.WriteLine("E: before closing streams");
            cStream.Close();
            inStream.Close();
            return output;
        }
    
    }
}
