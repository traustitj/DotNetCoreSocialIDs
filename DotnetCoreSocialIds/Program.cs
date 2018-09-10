using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreSocialIds
{
    class Program
    {
        public static async Task MainAsync(string[] args)
        {
            var s = new SocialIDS();

            List<List<string>> tasks = new List<List<String>>();

            for (int i = 1900; i <= 2000; i++)
            {
                var longRun = await Task.Run(() => s.GenerateAllWithChecksum(i));
                tasks.Add(longRun);
            }


            var year = 1900;
            foreach (var item in tasks)
            {
                Console.WriteLine($"I generated {item.Count} for year {year}");
                    year++;
            }

        }

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

        }
    }
}
