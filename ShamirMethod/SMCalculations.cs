using System;
using System.Numerics;

namespace ShamirMethod
{
    public class SMCalculations
    {
        private static Random _rnx = new Random();
        public static bool checkIfPrime(int n)
        {
            var isPrime = true;
            var sqrt = Math.Sqrt((int)n);
            for (var i = 2; i <= sqrt; i++)
                if ((n % i) == 0) isPrime = false;
            return isPrime;
        }

        public static int randomIntegerPrime(int min,int max)
        {
            int x = 0;
            do
            {
                x = randomNumber((int)min, (int)max);
            } while (!checkIfPrime(x));
            return x;
        }

        public static int randomNumber(int min, int max)
        {
            _rnx = new Random();
            return _rnx.Next(min, max);
        }
    }
}
