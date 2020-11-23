using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace ShamirMethod
{
    public class SMAlgorithm
    {
        Random _rnx = new Random();

        public static List<KeyValuePair<int, int>> DivisonSecret(List<int> shList, int _n, int _t, int _k, int _s, int _p)
        {
            List<KeyValuePair<int, int>> sList = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < _n; i++)
            {
                double sum = (int)_s;
                for (int j = 0; j < _t - 1; j++)
                {
                    sum += (int)shList[j] * (Math.Pow(Convert.ToDouble(i) + 1, j + 1));
                }
                sList.Add(new KeyValuePair<int, int>(i, (int)sum % _p));
            }
            return sList;
        }

        public static int RecreateSecret(List<KeyValuePair<int, int>> sDict, int _t, int _p)
        {
            double sum = 0;

            foreach (var e in sDict)
            {
                double x = 1;
                double y = 1;
                for (int j = 0; j < _t - 1; j++)
                {
                    if (j == e.Key)
                        continue;
                    x *= 0 - (double)sDict[j].Key;
                    y *= ((double)e.Value - (double)sDict[j].Key);
                }

                sum += (x / y * ((int)e.Value + 1)) % (int)_p;
            }     
            return (int)sum;
        }

        public static List<int> nthNumberGenerator(int n, int k)
        {
            var list = new List<int>();
            for (int i = 0; i < n - 1; i++)
                list.Add(SMCalculations.randomNumber(0,k));
            return list;
        }

        public static void ShamirMeth()
        {
            Console.WriteLine("=== Shamir Scheme ===");
            Console.Write("Enter k value: ");
            int _k = int.Parse(Console.ReadLine());
            Console.Write("Enter n value: ");
            int _n = int.Parse(Console.ReadLine());
            Console.Write("Enter t value: ");
            int _t = int.Parse(Console.ReadLine());
            Console.WriteLine($"Value of k: {_k}, n: {_n}, t: {_t}");

            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            int _s = 954;
            //BigInteger _s = SMCalculations.randomNumber(0, (int)_k);
            sw.Stop();
            Console.WriteLine($"Value of secret: {_s}| Generation time [ms]: {sw.Elapsed}");

            sw.Start();
            int _p;
            do
                _p = SMCalculations.randomIntegerPrime(0, (int)_k);
            while (_p <= _s && _p <= _n);
            sw.Stop();
            Console.WriteLine($"Value of p: {_p}| Generation time [ms]: {sw.Elapsed}");

            var shList = nthNumberGenerator(_n, _k);

            var shareholderDict = DivisonSecret(shList, _n, _t, _k, _s, _p);
            foreach (KeyValuePair<int, int> element in shareholderDict)
                Console.Write($" {element.Value}");
            Console.WriteLine();
            var recreateSecret = RecreateSecret(shareholderDict, _t, _p);
            Console.WriteLine($"Recreate Secret: {recreateSecret}");
        }
    }
}
