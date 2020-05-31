using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;

namespace sr4._2
{
    class Program
    {
        /// <summary>
        /// Словарь с правильной транслитерацией.
        /// </summary>
        static Dictionary<char, string> translit = new Dictionary<char, string>
        {
            ['A'] = "А",
            ['B'] = "Б",
            ['C'] = "Ц",
            ['D'] = "Д",
            ['E'] = "Е",
            ['F'] = "Ф",
            ['G'] = "Г",
            ['H'] = "Х",
            ['I'] = "И",
            ['J'] = "Ж",
            ['K'] = "К",
            ['L'] = "Л",
            ['M'] = "М",
            ['N'] = "Н",
            ['O'] = "О",
            ['P'] = "П",
            ['Q'] = "КУ",
            ['R'] = "Р",
            ['S'] = "С",
            ['T'] = "Т",
            ['U'] = "У",
            ['V'] = "В",
            ['W'] = "У",
            ['X'] = "КС",
            ['Y'] = "Ы",
            ['Z'] = "З",
            ['a'] = "а",
            ['b'] = "б",
            ['c'] = "ц",
            ['d'] = "д",
            ['e'] = "е",
            ['f'] = "ф",
            ['g'] = "г",
            ['h'] = "х",
            ['i'] = "и",
            ['j'] = "ж",
            ['k'] = "к",
            ['l'] = "л",
            ['m'] = "м",
            ['n'] = "н",
            ['o'] = "о",
            ['p'] = "п",
            ['q'] = "ку",
            ['r'] = "р",
            ['s'] = "с",
            ['t'] = "т",
            ['u'] = "у",
            ['v'] = "в",
            ['w'] = "у",
            ['x'] = "кс",
            ['y'] = "ы",
            ['z'] = "з",
        };

        /// <summary>
        /// Возвращает строку с отформатированным временем секундомера.
        /// </summary>
        /// <param name="stopwatch">Таймер</param>
        /// <returns></returns>
        static string FormatStopwatch(Stopwatch stopwatch)
        {
            TimeSpan timeSpan = stopwatch.Elapsed;
            return String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }

        /// <summary>
        /// Транслитирирует файл, имя которого передаётся, и записывает результат в файл “new_<старое имя>”.
        /// </summary>
        /// <param name="fileName">Имя исходного файла</param>
        static void Translit(string fileName)
        {

            // Создаём секундомер и запускаем её.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string data = "";
            try
            {
                data = File.ReadAllText($"../../../../books/{fileName}");
            }
            // Обрабатываем ошибки
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine($"Ошибка безопасности: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка доступа: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Можно было использовать через пустую строку и прибавление к ней.
            StringBuilder sbResult = new StringBuilder();


            for (int i = 0; i < data.Length; i++)
            {
                if (!Char.IsLetter(data[i]))
                    sbResult.Append(data[i]);
                if (translit.ContainsKey(data[i]))
                    sbResult.Append(translit[data[i]]);
            }

            try
            {
                File.WriteAllText($"../../../../books/new_{fileName}", sbResult.ToString());
            }
            // Обрабатываем ошибки
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine($"Ошибка безопасности: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка доступа: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            stopwatch.Stop();
            Console.WriteLine($"Количество символов до преобразования файла {fileName}: {data.Length}; " +
                $"после преобразования: {sbResult.ToString().Length}; время преобразования: {FormatStopwatch(stopwatch)}");
        }

        /// <summary>
        /// Транслитирирует строку и записывает результат в файл, имя которого передаётся.
        /// </summary>
        /// <param name="data">Исходная строка</param>
        /// // <param name="newFileName">Имя файла, в который записывается результат</param>
        static void TranslitFromString(string data, string newFileName)
        {
            // Создаём секундомер и запускаем её.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Можно было использовать через пустую строку и прибавление к ней.
            StringBuilder sbResult = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                if (!Char.IsLetter(data[i]))
                    sbResult.Append(data[i]);
                if (translit.ContainsKey(data[i]))
                    sbResult.Append(translit[data[i]]);
            }

            try
            {
                File.WriteAllText($"../../../../books/new_{newFileName}", sbResult.ToString());
            }
            // Обрабатываем ошибки
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
            }
            catch (System.Security.SecurityException ex)
            {
                Console.WriteLine($"Ошибка безопасности: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Ошибка доступа: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            stopwatch.Stop();

            Console.WriteLine($"Количество символов до преобразования файла {newFileName}: {data.Length}; " +
                $"после преобразования: {sbResult.ToString().Length}; время преобразования: { FormatStopwatch(stopwatch)}");
        }

        static void Main(string[] args)
        {
            // Массив имён файлов с книгами.
            string[] filesNames = { "121-0.txt", "1727-0.txt", "4200-0.txt", "58975-0.txt", "pg972.txt",
            "pg3207.txt", "pg19942.txt", "pg27827.txt", "pg43936.txt"};

            // Создаём секундомер и запускаем её.
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Выполняем синхронную транслитерацию.
            for (int i = 0; i < filesNames.Length; i++)
            {
                Translit(filesNames[i]);
            }

            // Останавливаем секундомер и выводим время работы.
            stopwatch.Stop();
            Console.WriteLine($"\nОбщее время синхронной транслитерации всех книг: {FormatStopwatch(stopwatch)}\n");

            // Перезапускаем секундомер.
            stopwatch.Restart();

            // Выполняем распараллеленную транслитерацию.
            ParallelLoopResult asyncTranslitingResult = Parallel.For(0, filesNames.Length, i => Translit(filesNames[i]));

            // Останавливаем секундомер и выводим время работы.
            stopwatch.Stop();
            Console.WriteLine($"\nОбщее время распараллеленной транслитерации всех книг: {FormatStopwatch(stopwatch)}\n");

            // Получаем строку с содержимым файла по ссылке через асинхронный get-запроc.
            HttpClient httpClient = new HttpClient();
            var response = httpClient.GetStringAsync("https://www.gutenberg.org/files/1342/1342-0.txt");

            // Ждём завершение обработки get-запроса.
            response.Wait();

            // Выполняем транслитерацию книги из веба.
            TranslitFromString(response.Result, "new_book_from_web.txt");
        }
    }
}
