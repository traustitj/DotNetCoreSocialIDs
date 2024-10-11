using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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
                Parallel.For(1900, 2024, i =>
                {
                    var x = s.GenerateAllWithChecksum(i);
                    results.Add(x);
                    //Console.WriteLine($"Generated for year {i} total count {x.Count}");
                });
            });

            stopWatch.Stop();
            var total = results.SelectMany(x => x).Count();
            Console.WriteLine($"This took {stopWatch.ElapsedMilliseconds} ms and created {total} social ids");
        }

        public static async Task NewMainSyncParallel(string[] args)
        {
            var s = new NewSocialIds();
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            
            var results = new List<List<List<int>>>();

            await Task.Run(() =>
            {
                Parallel.For(1900, 2024, i =>
                {
                    var x = s.GenerateAllPossibleIds(i);
                    results.Add(x);
                    //Console.WriteLine($"Generated for year {i} total count {x.Count}");
                });
            });

            stopWatch.Stop();
            var total = results.SelectMany(x => x).Count();
            Console.WriteLine($"New way took {stopWatch.ElapsedMilliseconds} ms and created {total} social ids");
        }


        public static async Task MainAsyncParallel(string[] args)
        {
            var s = new SocialIDS();
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();
            List<Task<List<string>>> tasks = new List<Task<List<String>>>();

            for (int i = 1900; i <= 2024; i++)
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
            //MainSyncParallel(args).GetAwaiter().GetResult();
            //NewMainSyncParallel(args).GetAwaiter().GetResult();
            //var s = new SocialIDS();
            //var ns = new NewSocialIds();
            //var stopw1 = System.Diagnostics.Stopwatch.StartNew();
            //var x = s.GenerateAllWithChecksum(2024);
            //stopw1.Stop();
            //Console.WriteLine($"Old way took {stopw1.ElapsedMilliseconds} ms, creating a total of {x.Count} days");
            //var stopw2 = System.Diagnostics.Stopwatch.StartNew();
            //var y = ns.GenerateAllPossibleIds(2024);
            //stopw2.Stop();
            
            //Console.WriteLine($"New way took {stopw2.ElapsedMilliseconds} ms, creating a total of {y.Count} days");
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
