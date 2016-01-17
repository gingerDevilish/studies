using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Text.RegularExpressions;


namespace sem3_lab11_client_
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
            Console.WriteLine("ИСПОЛЬЗОВАНИЕ:\n\tsem3_lab11(client).exe /?\n\tsem3_lab11(client).exe [/server:IP_Address] [/port:number]\n");
        }

        static void HelpCmd()
        {
            Console.WriteLine("Клиент для передачи скриншотов экрана и даты и времени их съемки\n");
            ErrorCmdArgs();
            Console.WriteLine("/server:\tIP-адрес сервера. По умолчанию 127.0.0.1\n" +                
                "/port:\tПорт для передачи данных. По умолчанию 11000\n" +               
                "Для корректной работы программы запускайте сначала сервер, а потом клиент.\n" );
        }
        
        static void Main(string[] args)
        {
            try
            {
                Hashtable argsValue = new Hashtable();
                argsValue.Add(@"/\?", null);
                argsValue.Add(@"\A/server:", "127.0.0.1");
                argsValue.Add(@"\A/port:", "11000");
                // Разбор аргументов
                argsParse(args, 0, 2, ref argsValue);
                if ((String)argsValue[@"/\?"] == "/?")
                {
                    HelpCmd();
                    return;
                }
                
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.CopyFromScreen(0, 0, Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                DateTime dt = DateTime.UtcNow;
                String filename = dt.Year.ToString("D4") + dt.Month.ToString("D2") + dt.Day.ToString("D2") + dt.Hour.ToString("D2") + dt.Minute.ToString("D2") + dt.Second.ToString("D2");
                bmp.Save(filename+".jpg", ImageFormat.Jpeg);

                String server = (String)argsValue[@"\A/server:"];
                Int32 port = Int32.Parse((String)argsValue[@"\A/port:"]);
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                Console.WriteLine("Connection established.");
                bmp.Save(stream, ImageFormat.Jpeg);
                Console.WriteLine("Data successfully sent.");
                stream.Close();
                client = new TcpClient(server, port);
                stream = client.GetStream();
                Byte [] data = new Byte[256];
                String responseData = String.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Response received: " + responseData);

                stream.Flush();
                data = Encoding.ASCII.GetBytes(filename);
                stream.Write(data, 0, data.Length);
                
                stream.Flush();
                bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Response received: " + responseData);
                
                stream.Flush();
                bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Response received: " + responseData);
                stream.Close();
                client.Close();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
