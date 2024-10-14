using System.IO;
namespace AnnLesson7
{
    internal class Program
    {        
        static int strPos = 0;               
        static void Main(string[] args)
        {
            string[] strMas = new string[2];
            Console.WriteLine($"Введите текст... Для вызова меню введите \"Q\"");
            while (true)
            {
                string? str = Console.ReadLine();
                if (str != null)                        
                {
                    if (str.ToUpper() == "Q")
                    {
                        PrintMasToFile(strMas);
                        break;
                    }
                    AddStrToMas(ref strMas, str);
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Запись массива строк в файл
        /// </summary>        
        public static void PrintMasToFile(string[] str)
        {
            string path = Path.GetTempPath();
            PrintFiles(path);                   //выводим содержимое папки на экран
            Console.WriteLine($"Чтобы сохранить текст введите имя файла в текущей дирректории пользователя {path}. Если файл есть, он будет дописан, иначе создан с введенным выше содержимым).");
            
            string? fileName = Console.ReadLine();
            try
            {
                string fullFileName = Path.Combine(path, fileName);
                PrepareStringForWriting(ref str);
                File.AppendAllLines(fullFileName, str);
                string[] result = File.ReadAllLines(fullFileName);
                Console.WriteLine($"Выводим содержимое файла {fullFileName}...");
                for (int i = 0; i < result.Length; i++)
                {
                    Console.WriteLine(result[i]);
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Произошла ошибка: {ex.Message}");                
            }
        }

        /// <summary>
        /// Добавление строки к массиву
        /// </summary>        
        public static void AddStrToMas(ref string[] strMas, string str)
        {
            if (strMas.Length == strPos)
            { 
                ResizeString(ref strMas);
            }
            strMas[strPos] = str;
            strPos++;
        }

        /// <summary>
        /// Увеличение массива
        /// </summary>        
        public static void ResizeString(ref string[] str)
        { 
            string[] tmp = new string[str.Length*2];
            for (int i = 0; i < str.Length; i++)
            {
                tmp[i] = str[i];
            }            
            str = tmp;
        }

        /// <summary>
        /// Подготовка строки для записи в файл: удаление пустых строк в массиве
        /// </summary>        
        public static void PrepareStringForWriting(ref string[] str)
        {
            string[] tmp = new string[strPos];
            for (int i = 0; i < strPos; i++)
            {
                tmp[i] = str[i];
            }
            str = tmp;
        }

        /// <summary>
        /// Вывод на экран содержимого папки
        /// </summary>        
        public static void PrintFiles(string path)
        {
            string[] allFiles = Directory.GetFiles(path, "*.txt");              //будем выводить только текстовые файлы
            string result = "";
            foreach (string file in allFiles)
            {
                string fileName = new FileInfo(file).Name;
                result += $"  {fileName}";
            }
            Console.WriteLine($"Вывожу все файлы в каталоге пользователя:{result}");            
        }
    }
}
