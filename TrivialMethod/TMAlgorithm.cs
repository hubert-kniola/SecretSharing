using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace TrivialMethod
{
    public class TMAlgorithm
    {
        private static Random _rnx = new Random();

        public static BigInteger randomNumber(int min, int max)
        {
            _rnx = new Random();
            return _rnx.Next(min, max);
        }
        public static List<BigInteger> nthNumberGenerator(BigInteger n, BigInteger k)
        {
            var list = new List<BigInteger>();
            for (int i = 0; i < n - 1; i++)
                list.Add(randomNumber(0, (int)k));
            return list;
        }
        private static List<BigInteger> DivisonSecret(List<BigInteger> list, BigInteger n, BigInteger k, BigInteger s)
        {
            var sumOfList = list.Aggregate(BigInteger.Add);
            list.Add((s - sumOfList) % k);
            return list;
        }

        private static BigInteger RecreateSecret(List<BigInteger> list, BigInteger k)
        {
            var sumOfList = list.Aggregate(BigInteger.Add);
            var s = sumOfList % k;
            return s;
        }

        public static void TrivialMeth()
        {
            Console.WriteLine("=== Trivial Method ===");
            Console.Write("Enter k value: ");
            BigInteger _k = BigInteger.Parse(Console.ReadLine());
            Console.Write("Enter n value: ");
            BigInteger _n = BigInteger.Parse(Console.ReadLine());

            Stopwatch sw = Stopwatch.StartNew();      
            sw.Start();     
            BigInteger _secret = randomNumber(0, (int)_k);
            sw.Stop();
            Console.WriteLine($"Value of k: {_k}, n: {_n} ");
            Console.WriteLine($"Value of secret: {_secret}| Generation time [ms]: {sw.Elapsed}");


            var shList = nthNumberGenerator(_n, _k);
            var shareholderList = DivisonSecret(shList, _n, _k, _secret);

            Console.Write("Shareholders: ");
            foreach (var element in shareholderList)
                Console.Write($" {element}");
            Console.WriteLine();

            var secretResult = RecreateSecret(shareholderList, _k);
            Console.WriteLine($"Recreate secret: {secretResult}");
        }
    }
}
