using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            // Путь к файлу
            string file = @"D:\Программирование\Тестовое задание ШИ\test2\test2\bin\Debug\netcoreapp3.1\text.txt";
            //Записываем текст в переменную
            string text = File.ReadAllText(file);
            sw.Start();
            using(var client = new ServiceReference1.Service1Client())
            {
                Dictionary<string, int> Counts = client.Text_analis(text);

                // Создаём список
                List<Tuple<int, string>> statistics = Counts.Select(x => new Tuple<int, string>(x.Value, x.Key)).ToList();

                // Сортируем список по количеству слов
                statistics.Sort((x, y) => y.Item1.CompareTo(x.Item1));

                // Построчно добавляем список в новый файл 
                foreach (Tuple<int, string> x in statistics)
                    File.AppendAllText(file + ".statistics.txt", x.Item2 + " " + x.Item1 + Environment.NewLine);
                sw.Stop();
                Console.WriteLine("Время выполнения: " + sw.ElapsedMilliseconds);
                Console.WriteLine("Готово! Файл с перечслением всех уникальных слов: " + Environment.NewLine + file + ".statistics.txt");
                Console.ReadKey();
            }
        }
    }
}
