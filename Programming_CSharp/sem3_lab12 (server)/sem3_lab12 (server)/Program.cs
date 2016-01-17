using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace sem3_lab12__server_
{
    class Program
    {
        public static void ArgsToHashtable(string[] args, ref Hashtable argsValue)
        {
            if (args.Length == 0) return;
            bool[] argsKnow = new bool[args.Length];
            for (int i = 0; i < argsKnow.Length; i++) { argsKnow[i] = false; }
            string[] keys = new string[argsValue.Count];
            int k = 0;
            foreach (string key in argsValue.Keys) { keys[k] = key; k++; }
            for (int ikey = 0; ikey < keys.Length; ikey++)
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
        
        public static void argsParse(string[] args, int minNumberArgs, int maxNumberArgs, ref Hashtable argsValue)
        {
            // Количество аргументов больше максимального и меньше минимального
            if ((args.Length < minNumberArgs) || (args.Length > maxNumberArgs))
            {
                throw new ArgumentException("Непредусмотренное количество аргументов");
            }
            // Создание хэш-таблицы значений аргументов
            ArgsToHashtable(args, ref argsValue);
            // Проверка на один аргумент
            // Паттерн "/?" -> значение
            if (argsValue[@"/\?"] != null)
            {
                if (args.Length == 1) { argsValue[@"/\?"] = "/?"; return; }
                else { throw new ArgumentException("Неверный(ые) аргумент(ы)"); }
            }
            foreach (string ptrn in argsValue.Keys)
            {
                if (ptrn != @"/\?")
                {
                    if (argsValue[ptrn] == null) { throw new ArgumentException("Отсутствует(ют) обязательный(ые) аргумент(ы)"); }
                }
            }
        }

        static void ErrorCmdArgs()
        {
            Console.WriteLine("ИСПОЛЬЗОВАНИЕ:\n\tsem3_lab12(server).exe /?\n\tsem3_lab12(server).exe [/ip:ip_address] [/port:number]\n");
        }

        static void HelpCmd()
        {
            Console.WriteLine("Сервер, принимающий скриншоты экрана сервера и дату и время их съемки\n");
            ErrorCmdArgs();
            Console.WriteLine("/ip:\tIP-адрес сервера. По умолчанию 127.0.0.1\n" +
                "/port:\tПорт для передачи данных. По умолчанию 11000\n" +
                "Для корректной работы программы запускайте сначала сервер, а потом клиент.\n");
        }

        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                Hashtable argsValue = new Hashtable();
                argsValue.Add(@"/\?", null);
                argsValue.Add(@"\A/ip:", "127.0.0.1");
                argsValue.Add(@"\A/port:", "11000");
                // Разбор аргументов
                argsParse(args, 0, 2, ref argsValue);
                if ((String)argsValue[@"/\?"] == "/?") 
                { 
                    HelpCmd(); 
                    return; 
                }

                Int32 port = Int32.Parse((String)argsValue[@"\A/port:"]);
                IPAddress address = IPAddress.Parse((String)argsValue[@"\A/ip:"]);
                server = new TcpListener(address, port);
                server.Start();
                Byte[] bytes = new Byte[256];
                Bitmap image = null;
                Int32 size=0;
                String response = null;
                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected to client with IP="+address.ToString()+", port="+ port);
                    image = null;
                    NetworkStream stream = client.GetStream();
                    Console.WriteLine("Stream created");
     
                    image = new Bitmap(stream);

                    response = "Image downloaded"; 
                    Console.WriteLine(response);   
                    Byte[] msg = Encoding.ASCII.GetBytes(response);
                    stream.Close();
                    client = server.AcceptTcpClient();
                    stream = client.GetStream();
                    stream.Write(msg, 0, msg.Length);

                    stream.Flush();
                    size = stream.Read(bytes, 0, bytes.Length);
                    
                    response = "Date received";
                    Console.WriteLine(response);
                    msg = Encoding.ASCII.GetBytes(response);
                    stream.Flush();
                    stream.Write(msg, 0, msg.Length);

                    String filename = Encoding.ASCII.GetString(bytes, 0, size);
                    
                    image.Save(filename + ".jpg", ImageFormat.Jpeg);

                    response = "Image saved";
                    Console.WriteLine(response);
                    msg = Encoding.ASCII.GetBytes(response);
                    stream.Flush();
                    stream.Write(msg, 0, msg.Length);

                    client.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
