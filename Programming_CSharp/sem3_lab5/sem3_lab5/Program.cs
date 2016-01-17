using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace sem3_lab5
{
    class Program
    {
        // Функция шифрования по алгоритму 3DES
        static Byte[] TripleDES_Encrypt(String input, Byte[] key, Byte[] IV)
        {
            MemoryStream inStream = new MemoryStream();
            CryptoStream cStream= new CryptoStream(inStream, new TripleDESCryptoServiceProvider().CreateEncryptor(key, IV), CryptoStreamMode.Write);
            Byte[] toEncrypt = Encoding.Unicode.GetBytes(input);
            cStream.Write(toEncrypt, 0, toEncrypt.Length);
            cStream.FlushFinalBlock();
            Byte[] output = inStream.ToArray();
            cStream.Close();
            inStream.Close();
            return output;
        }

        // Функция расшифровки по алгоритму 3DES
        static String TripleDES_Decrypt(Byte[] input, Byte[] key, Byte [] IV)
        {
            MemoryStream outStream = new MemoryStream(input);
            CryptoStream cStream = new CryptoStream(outStream, new TripleDESCryptoServiceProvider().CreateDecryptor(key, IV), CryptoStreamMode.Read);
            Byte[] fromEncrypt = new Byte[input.Length];;
            cStream.Read(fromEncrypt, 0, fromEncrypt.Length);
            outStream.Close();
            return Encoding.Unicode.GetString(fromEncrypt);
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter your text:");
                String initial = Console.ReadLine(); // Исходная строка
                Console.WriteLine("Enter the key length (bits). It can be 128 or 192:");
                Int32 keyLength = Int32.Parse(Console.ReadLine());
                Console.WriteLine("For this algorithm, block size is fixed as 64 bits.");
                TripleDESCryptoServiceProvider sample = new TripleDESCryptoServiceProvider();
                sample.KeySize = keyLength;
                // Сохраняем ключ и вектор в переменных, чтобы объект не нарандомил что-то свое
                // Без этого программа выдавала крякозябры
                Byte[] key = sample.Key;
                Byte[] IV = sample.IV;
                Byte[] encrypted = TripleDES_Encrypt(initial, key, IV);
                String decrypted = TripleDES_Decrypt(encrypted, key, IV);
                Console.WriteLine("Encrypted text:");
                Console.WriteLine(Encoding.Unicode.GetString(encrypted));
                Console.WriteLine("Decrypted text:");
                // После цикла шифровки-расшифровки длина строки может увеличиться за счет добавленных байт
                Console.WriteLine(decrypted.Substring(0,initial.Length));
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
