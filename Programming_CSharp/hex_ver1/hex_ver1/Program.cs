using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace hex_ver1
{
    class Program
    {
        
        // Функция сопоставления назначения строки с его формулировкой
        // Формулировки взяты из английской Википедии
        static String CC_ToString(Byte CC)
        {
            switch (CC)
            {
                case 0x00:
                    return "Data";
                case 0x01:
                    return "End Of File";
                case 0x02:
                    return "Extended Segment Adress";
                case 0x03:
                    return "Start Segment Adress";
                case 0x04:
                    return "Extended Linear Adress";
                case 0x05:
                    return "Start Linear Adress";
                default:
                    return "ERROR!";
            }
        }

        // Функция проверки двоеточия в начале строки
        // Несмотря на существование делегатов и лямбда-выражений, в данном случае мне удобнее использовать функции
        static Boolean CheckColons(String line)
        {
            return line.Substring(0, 1)==":";
        }

        // Длина строки жестко связана с числом NN, если строка не повреждена
        static Boolean CheckNN(String line, Byte NN)
        {
            return line.Length == (11 + 2 * NN);
        }

        // Подсчет полной контрольной суммы, включая заключительный байт SS
        static Boolean CheckLRC(String line)
        {
            Byte Sum = 0;
            String S = line.Substring(1, line.Length - 1);
            for (int i = 0; i < S.Length; i += 2)
            {
                Sum+=Byte.Parse(S.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return Sum == 0;
        }

        // Краткая справка по синтаксису программы
        static void ErrorCmdArgs()
        {
            Console.WriteLine("ИСПОЛЬЗОВАНИЕ:\n\tHEXReader.exe /?\n\tHEXReader.exe /fi:[путь]имя_файла [/saveout:yes|no|[путь]имя_файла] [/onscreen:yes|no]\n");
        }

        // Развернутая справка
        // Справочная информация является встроенной документацией к программе
        static void HelpCmd()
        {
            Console.WriteLine("Чтение файлов Intel HEX со структурированием.\n");
            ErrorCmdArgs();
            Console.WriteLine("/fi:\tСчитывание исходного файла\n" + 
                "\t[путь]имя_файла\tУказывает на файл, подвергаемый анализу.\n" +
                "\tАргумент обязательный\n" +
                "/saveout:\tУказывает на необходимость сохранения результатов анализа структуры.\n" +
                "\tyes\tСтруктура сохраняется в файле с именем по умолчанию \"output.txt\" в папке программы.\n" +
                "\tno\tСтруктура не сохраняется.\n" +
                "\t[путь]имя_файла\tСтруктура сохраняется в файле с выбранным названием по указанному пути\n"+
                "\tЕсли аргумент не указан, то структура не сохраняется.\n" +
                "/onscreen:\tВыведение на экран результатов анализа.\n" +
                "\tyes\tСтруктура файла выводится на экран\n" +
                "\tno\tСтруктура файла не выводится на экран\n" +
                "\tЕсли аргумент не указан, то результаты анализа выводятся в консоль.\n" +
                "При использовании одновременно ключей /saveout:no /onscreen:no произойдет проверка ошибок в файле по контрольным суммам строк и будет выведено их количество. \n");
        }

        // Организация входных аргументов в хэш-таблицу. Благодаря этому, аргументы можно вводить в любом порядке
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

        public static void Main(string[] args)
        {         
            // Переменные
            StringBuilder Output = new StringBuilder(); // Сюда будет помещаться полный текст разбора
            String LineOut = ""; // Строка для формирования текста разбора каждой строки отдельно
            Byte NN = 0;    // Количество байт значимой информации в строке
            UInt16 AAAA = 0; // Относительный адрес, с которого начинает писаться содержимое строки
            Byte CC = 0;    // Назначение строки
            Byte SS = 0;    // Контрольная сумма
            Boolean CheckLRC_OK = false; // Флаг соответствия контрольной суммы строке
            Int32 countLine = 0;    // Счетчик номера обрабатываемой строки
            Int32 lineNumber = 0;   // Количество строк
            Int32 validityCounter = 0;  // Счетчик количества неповрежденных строк
            try
            {
                // Обработка аргументов командной строки
                Hashtable argsValue = new Hashtable();
                argsValue.Add(@"/\?", null);
                argsValue.Add(@"\A/fi:", null);
                argsValue.Add(@"\A/saveout:", "no");
                argsValue.Add(@"\A/onscreen:", "yes");
                // Разбор аргументов
                argsParse(args, 1, 3, ref argsValue);
                // Использование аргументов
                if ((String)argsValue[@"/\?"] == "/?") { HelpCmd(); return; }
                String pathIn = (String)argsValue[@"\A/fi:"];
                String pathOut = "output.txt";
                String save = (String)argsValue[@"\A/saveout:"];
                String onScreen = (String)argsValue[@"\A/onscreen:"];
                if (!((onScreen == "yes") || (onScreen == "no")))
                    throw new ArgumentException("Указано недопустимое значение для \"/onscreen:\"");
                if (save != "no")
                {
                    if (!(save == "yes"))
                        pathOut = save;
                    pathOut = Path.GetFullPath(pathOut);
                    String outDir = Path.GetDirectoryName(pathOut);
                    if (!Directory.Exists(outDir))
                        Directory.CreateDirectory(outDir);
                }

                // Обработка файла
                lineNumber = File.ReadAllLines(pathIn).Length;
                foreach (string line in File.ReadLines(pathIn))
                {
                    countLine++;
                    // Любой HEX-файл и любая его строка начинаются с символа ':'
                    if (!CheckColons(line))
                        throw new SystemException("No colon (\':\') in the begining of the line");
                    NN = Byte.Parse(line.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                    // Проверка соответствия количества значимой информации заявленному
                    if (!CheckNN(line, NN))
                        throw new SystemException("Wrong Byte Count");
                    CheckLRC_OK = CheckLRC(line);
                    AAAA = UInt16.Parse(line.Substring(3, 4), System.Globalization.NumberStyles.AllowHexSpecifier);
                    CC = Byte.Parse(line.Substring(7, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                    if (CheckLRC_OK&&(CC>=0)&&(CC<6))
                        validityCounter++;
                    SS = Byte.Parse(line.Substring(line.Length - 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                    // Назначение, соответствующее концу файла, должно быть только в конце файла
                    if (countLine == lineNumber)
                    {
                        if (CC != 0x01)
                            throw new SystemException("Record type: " + CC_ToString(CC) + ". End of File record type expected.");
                    }
                    else
                    {
                        if (CC == 0x01)
                            throw new SystemException("Record type: End of File. Any other record type expected.");
                    }
                    // Компоновка сведений о строке
                    // Enviroment.NewLine используется для сохранения читабельности в текстовом файле результатов
                    LineOut = "Line #" + countLine + Environment.NewLine + line + Environment.NewLine +
                        "Byte Count: " + NN + " bytes" + Environment.NewLine +
                        "Address: " + AAAA.ToString("X4") + Environment.NewLine +
                        "Purpose: " + CC_ToString(CC) + Environment.NewLine +
                        "LRC: " + (CheckLRC_OK ? "OK" : "Failed") + "(" + SS.ToString("X2") + ")" + Environment.NewLine + Environment.NewLine;
                    Output.Append(LineOut);
                }
                // Обработка опций выведения на экран и сохранения
                if (onScreen == "yes")
                    Console.WriteLine(Output.ToString());
                if (save != "no")
                    File.WriteAllText(pathOut, Output.ToString());
                // Статистика работы программы выводится в консоль
                Console.WriteLine("/Total lines: " + lineNumber+ "\nCorrect lines: " + validityCounter+ "\nIncorrect lines: " + (lineNumber-validityCounter));

            }
            catch(ArgumentException aEx)
            {
                Console.WriteLine(aEx.Message);
                ErrorCmdArgs();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
