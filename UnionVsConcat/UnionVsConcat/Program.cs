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
            Console.WriteLine("Hello World!");

//            var longList = GetLongList(100000000);

//            var mediumList = GetLongList(10000000);

//            var smallList = GetLongList(10000);


////            var finalList = longList.Union(mediumList).Union(smallList);
//            //var finalList = smallList.Union(mediumList).Union(longList);
//            //var finalList = smallList.Concat(mediumList).Concat(longList).Distinct();
//            var finalList = longList.Concat(mediumList).Concat(smallList).Distinct();


            BenchmarkRunner.Run<AccumulatorTests>();

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

        private readonly List<long> longList;
        private readonly List<long> mediumList;
        private readonly List<long> smallList;

        public AccumulatorTests()
        {
            longList = GetLongList(1000000);

            mediumList = GetLongList(100000);

            smallList = GetLongList(10000);
        }

        [Benchmark]
        public List<long> UnionAll()
        {
            //var longList = GetLongList(1000000);

            //var mediumList = GetLongList(100000);

            //var smallList = GetLongList(10000);

            var finalList = longList.Union(mediumList).Union(smallList).ToList();

            return finalList;
        }

        [Benchmark]
        public List<long> ConcatAll()
        {
            //var longList = GetLongList(1000000);

            //var mediumList = GetLongList(100000);

            //var smallList = GetLongList(10000);

            var finalList = longList.Concat(mediumList).Concat(smallList).Distinct().ToList();

            return finalList;
        }

        [Benchmark]
        public List<long> UnionThenConcatAll()
        {
            //var longList = GetLongList(1000000);

            //var mediumList = GetLongList(100000);

            //var smallList = GetLongList(10000);

            var finalList = longList.Union(mediumList).Concat(smallList).ToList();

            return finalList;
        }


    }
}
