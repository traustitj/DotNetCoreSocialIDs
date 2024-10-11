using DotnetCoreSocialIds;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
namespace DotnetCoreSocialIds
{
    public class Benchmarks
    {
        [Benchmark]
        public void OldGenerateDaysForYear()
        {
            var s = new SocialIDS();
            s.GenerateAllWithChecksum(2024);
        }

        [Benchmark]
        public void NewGenerateDaysOfYear()
        {
            var ns = new NewSocialIds();
            ns.GenerateAllPossibleIds(2024);
        }

        

    }
}
