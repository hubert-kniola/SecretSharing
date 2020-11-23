using System;
using TrivialMethod;
using ShamirMethod;

namespace SecretSharing
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.Write("1 - Trivial Method\n2 - Shamir Scheme\nEnter number: ");
                string menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        TMAlgorithm.TrivialMeth();
                        break;

                    case "2":
                        SMAlgorithm.ShamirMeth();
                        break;

                    default:
                        Console.WriteLine("Wrong number!");
                        continue;
                }
            } while (true);

            Console.ReadKey();
        }
    }
}
