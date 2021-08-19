using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace UnionVsConcat
{
    class Program
    {
        static void Main(string[] args)
        {

            BenchmarkRunner.Run<AccumulatorTests>();

            //var s = new AccumulatorTests();
            //var t = s.StraightUpAddThenDistinct();

            return;
        }
    }


    [MemoryDiagnoser]
    public class AccumulatorTests
    {
        static List<long> GetLongList(int howLong)
        {
            var random = new Random();

            var ret = new List<long>();

            for (var i = 0; i < howLong; i++)
            {
                ret.Add(random.Next(0, Int32.MaxValue));
            }

            return ret;
        }

        private readonly List<long> _longList;
        private readonly List<long> _mediumList;
        private readonly List<long> _smallList;

        public AccumulatorTests()
        {
            _longList = GetLongList(1000000);

            _mediumList = GetLongList(100000);

            _smallList = GetLongList(10000);
        }

        [Benchmark]
        public List<long> UnionAll()
        {
            var finalList = _longList.Union(_mediumList).Union(_smallList).ToList();

            //Console.WriteLine($"Final list size: {finalList.Count}");
            return finalList;
        }

        [Benchmark]
        public List<long> ConcatAll()
        {
            var finalList = _longList.Concat(_mediumList).Concat(_smallList).Distinct().ToList();

            //Console.WriteLine($"Final list size: {finalList.Count}");
            return finalList;
        }

        [Benchmark]
        public List<long> UnionThenConcatAll()
        {
            var finalList = _longList.Union(_mediumList).Concat(_smallList).ToList();

            //Console.WriteLine($"Final list size: {finalList.Count}");
            return finalList;
        }

        [Benchmark]
        public List<long> StraightUpAddThenDistinct()
        {
            var finalList = new List<long>(_longList);
            finalList.AddRange(_mediumList);
            finalList.AddRange(_smallList);
            
            finalList = finalList.Distinct().ToList();

            //Console.WriteLine($"Final list size: {finalList.Count}");
            return finalList;
        }
    }
}
