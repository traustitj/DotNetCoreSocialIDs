using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSocialIds
{
    class Program
    {
        public static async Task MainSyncParallel(string[] args)
        {
            var s = new SocialIDS();
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            List<Task<List<string>>> tasks = new List<Task<List<String>>>();

            List<List<string>> results = new List<List<string>>();


            await Task.Run(() =>
            {
                Parallel.For(1900, 2000, i =>
                {
                    var x = s.GenerateAllWithChecksum(i);
                    results.Add(x);
                    Console.WriteLine($"Generated for year {i} total count {x.Count}");
                });
            });

            stopWatch.Stop();
            Console.WriteLine($"This took {stopWatch.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }


        public static async Task MainAsyncParallel(string[] args)
        {
            var s = new SocialIDS();
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            List<Task<List<string>>> tasks = new List<Task<List<String>>>();

            for (int i = 1900; i <= 2000; i++)
            {
                tasks.Add(Task.Run(() => s.GenerateAllWithChecksum(i)));
                
            }


            var results = await Task.WhenAll(tasks);
            foreach (var item in results)
            {
                Console.WriteLine($"I generated {item.Count}");
            }

            stopWatch.Stop();
            Console.WriteLine($"This took {stopWatch.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }

        public static async Task MainAsync(string[] args)
        {
            var s = new SocialIDS();
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            List<List<string>> tasks = new List<List<String>>();

            for (int i = 1900; i <= 2000; i++)
            {
                tasks.Add(await Task.Run(() => s.GenerateAllWithChecksum(i)));
            }

            foreach (var item in tasks)
            {
                Console.WriteLine($"I generated {item.Count}");
            }

            stopWatch.Stop();
            Console.WriteLine($"This took {stopWatch.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }

        public static void Main(string[] args)
        {
            MainSyncParallel(args).GetAwaiter().GetResult();

        }
    }
}
