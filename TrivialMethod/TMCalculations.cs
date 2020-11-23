using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TrivialMethod
{
    class TMCalculations
    {
        static Random rnx = new Random();

        public static BigInteger randomBigInteger(BigInteger N)
        {
            BigInteger result = 0;
            do
            {
                int length = (int)Math.Ceiling(BigInteger.Log(N, 2));
                int numBytes = (int)Math.Ceiling(length / 8.0);
                byte[] data = new byte[numBytes];
                rnx.NextBytes(data);
                result = new BigInteger(data);
            } while (result >= N || result <= 0);
            return result;
        }


    }
}
