using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Threading;

namespace ShamirMethod
{
    public class SMAlgorithm
    {
        private static Random _rnx = new Random();

        public static List<KeyValuePair<int, int>> DivisonSecret(List<int> shList, int _n, int _t, int _s, int _p)
        {
            List<KeyValuePair<int, int>> sList = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < _n; i++)
            {
                double sum = (int)_s;
                for (int j = 0; j < _t - 1; j++)
                {
                    sum += shList[j] * (Math.Pow(Convert.ToDouble(i) + 1, j + 1));
                }
                sList.Add(new KeyValuePair<int, int>(i + 1, (int)sum % _p));
            }
            return sList;
        }

        public static int RecreateSecret(List<KeyValuePair<int, int>> aList, int _t, int _p)
        {
            double sum = 0;
            for (int i = 0; i < _t; i++)
            {
                double licznik = 1;
                double mianownik = 1;
                double x = 1;
                for (int j = 0; j < _t; j++)
                {
                    if (aList[i].Key == aList[j].Key)
                        continue;
                    licznik *= -aList[j].Key;
                    mianownik *= aList[i].Key - aList[j].Key;
                }
                if (licznik % mianownik == 0)
                    x *= (licznik / mianownik);
                else
                {
                    if (mianownik < 0 && licznik < 0)
                    {
                        mianownik *= -1;
                        licznik *= -1;
                    }
                    int temp = 1;
                    while (temp * mianownik % _p != licznik)
                        temp++;
                    x *= temp;
                }
                Console.WriteLine(x * (int)aList[i].Value % _p);
                sum += ((x * (int)aList[i].Value % _p) + _p) % _p;
            }
            return (((int)sum + _p) % _p);
        }

        public static List<int> nthNumberGenerator(int t, int k)
        {
            var list = new List<int>();
            for (int i = 0; i < t - 1; i++)
            {
                Thread.Sleep(100);
                list.Add(SMCalculations.randomNumber(0, k));
            }
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
            //int _s = 954;
            var _s = SMCalculations.randomNumber(0, (int)_k);
            sw.Stop();
            Console.WriteLine($"Value of secret: {_s}| Generation time [ms]: {sw.Elapsed}");

            sw.Start();
            int _p = _s - 1;
            do
            {
                _p = SMCalculations.randomIntegerPrime(0, (int)_k);
            } while (_p <= _s || _p <= _n);

            sw.Stop();
            Console.WriteLine($"Value of p: {_p}| Generation time [ms]: {sw.Elapsed}");

            var shList = nthNumberGenerator(_t, _k);

            var shareholderDict = DivisonSecret(shList, _n, _t, _s, _p);
            foreach (var element in shareholderDict)
                Console.Write($" {element.Value}");
            Console.WriteLine();

            var recreateSecret = RecreateSecret(shareholderDict, _t, _p);
            Console.WriteLine($"Recreate Secret: {recreateSecret}");
        }
    }
}
