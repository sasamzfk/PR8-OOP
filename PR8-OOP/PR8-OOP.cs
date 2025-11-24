using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp28
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Синхронна / Асинхронна обробка \n");

            // СИНХРОН
            Console.WriteLine("--- Початок СИНХРОННОГО виконання ---");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Method1();
            Method2();
            Method3();

            stopwatch.Stop();
            long syncTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Кінець синхронного виконання. Час: {syncTime} мс \n");

            //асинхрон
            Console.WriteLine("--- Початок АСИНХРОННОГО виконання ---");
            stopwatch.Restart();

            try
            {
                Task task1 = Method1Async();
                Task task2 = Method2Async();
                Task task3 = Method3Async();

                await Task.WhenAll(task1, task2, task3);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Головний потік] Перехоплено виключення: {ex.Message}");
            }

            stopwatch.Stop();
            long asyncTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"- Кінець асинхронного виконання. Час: {asyncTime} мс\n");

            Console.WriteLine("--- Тест ОБРОБКИ ВИКЛЮЧЕННЯ (Асинхронно) ---");
            try
            {
                await MethodWithExceptionAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"! Перехоплено помилку: {ex.Message}");
            }

            Console.WriteLine("\n=== Аналіз ефективності ===");
            Console.WriteLine($"Синхронний час: {syncTime} мс");
            Console.WriteLine($"Асинхронний час: {asyncTime} мс");

            double speedUp = (double)syncTime / asyncTime;
            Console.WriteLine($"Прискорення: у {speedUp:F2} разів");

            Console.ReadKey();
        }


        // синхрон
        static void Method1()
        {
            Console.WriteLine("Sync Метод 1: Початок (тривалість 2 с)");
            Thread.Sleep(2000); // Імітація важкої роботи
            Console.WriteLine("Sync Метод 1: Завершено");
        }

        static void Method2()
        {
            Console.WriteLine("Sync Метод 2: Початок (тривалість 3 с)");
            Thread.Sleep(3000);
            Console.WriteLine("Sync Метод 2: Завершено");
        }

        static void Method3()
        {
            Console.WriteLine("Sync Метод 3: Початок (тривалість 1 с)");
            Thread.Sleep(1000);
            Console.WriteLine("Sync Метод 3: Завершено");
        }

        // асинхрон
        static async Task Method1Async()
        {
            Console.WriteLine("Async Метод 1: Початок");
            await Task.Delay(2000); // Асинхронне очікування
            Console.WriteLine("Async Метод 1: Завершено");
        }

        static async Task Method2Async()
        {
            Console.WriteLine("Async Метод 2: Початок");
            await Task.Delay(3000);
            Console.WriteLine("Async Метод 2: Завершено");
        }

        static async Task Method3Async()
        {
            Console.WriteLine("Async Метод 3: Початок");
            await Task.Delay(1000);
            Console.WriteLine("Async Метод 3: Завершено");
        }

        static async Task MethodWithExceptionAsync()
        {
            Console.WriteLine("Async Error Method: Старт...");
            await Task.Delay(500);
            throw new InvalidOperationException("Сталася критична помилка в асинхронному методі!");
        }
    }
}