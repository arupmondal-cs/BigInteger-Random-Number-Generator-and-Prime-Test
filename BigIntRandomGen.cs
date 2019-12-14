using System;
using System.Numerics;
using System.Security.Cryptography;

namespace BigIntRandNum
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("|        RANDOM BIG INTEGER GENERATOR AND PRIME TEST        |");
                Console.WriteLine("-------------------------------------------------------------\n");

                string opt = "y";
                while(opt=="y")
                {
                    Console.Write("\nEnter Bit-Length of the Random Big Integer: ");
                    int bitlen = Convert.ToInt32(Console.ReadLine());
                    RandomBigIntegerGenerator RBI = new RandomBigIntegerGenerator();
                    BigInteger RandomNumber = RBI.NextBigInteger(bitlen);

                    Console.WriteLine("\n----------------------------------");
                    Console.WriteLine("|      Generated Big Integer      |");
                    Console.WriteLine("----------------------------------\n");

                    Console.WriteLine(RandomNumber);


                    BigIntegerPrimeTest BIPT = new BigIntegerPrimeTest();
                    if (BIPT.IsProbablePrime(RandomNumber, 100) == true)
                        Console.WriteLine("\nGenerated Random number is prime!");
                    else
                        Console.WriteLine("\nGenerated Random number is not prime!!");

                    Console.WriteLine("-------------------------------------------------------------\n");

                    Console.Write("Do you want to play more? (y/n): ");
                    opt = Console.ReadLine();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("\n\nException Occured: {0} !!",e.Message);
            }

        }
    }

    class RandomBigIntegerGenerator
    {
        public BigInteger NextBigInteger(int bitLength)
        {
            if (bitLength < 1) return BigInteger.Zero;

            int bytes = bitLength / 8;
            int bits = bitLength % 8;

            // Generates enough random bytes to cover our bits.
            Random rnd = new Random();
            byte[] bs = new byte[bytes + 1];
            rnd.NextBytes(bs);

            // Mask out the unnecessary bits.
            byte mask = (byte)(0xFF >> (8 - bits));
            bs[bs.Length - 1] &= mask;

            return new BigInteger(bs);
        }

        // Random Integer Generator within the given range
        public BigInteger RandomBigInteger(BigInteger start, BigInteger end)
        {
            if (start == end) return start;

            BigInteger res = end;

            // Swap start and end if given in reverse order.
            if (start > end)
            {
                end = start;
                start = res;
                res = end - start;
            }
            else
                // The distance between start and end to generate a random BigIntger between 0 and (end-start) (non-inclusive).
                res -= start;

            byte[] bs = res.ToByteArray();

            // Count the number of bits necessary for res.
            int bits = 8;
            byte mask = 0x7F;
            while ((bs[bs.Length - 1] & mask) == bs[bs.Length - 1])
            {
                bits--;
                mask >>= 1;
            }
            bits += 8 * bs.Length;

            // Generate a random BigInteger that is the first power of 2 larger than res, 
            // then scale the range down to the size of res,
            // finally add start back on to shift back to the desired range and return.
            return ((NextBigInteger(bits + 1) * res) / BigInteger.Pow(2, bits + 1)) + start;
        }
    }


    // Miller-Rabin primality test as an extension method on the BigInteger type.
    // Based on the Ruby implementation on this page.
    class BigIntegerPrimeTest
    {
        public bool IsProbablePrime(BigInteger source, int certainty)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            // There is no built-in method for generating random BigInteger values.
            // Instead, random BigIntegers are constructed from randomly generated
            // byte arrays of the same length as the source.
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[source.ToByteArray().LongLength];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    // This may raise an exception in Mono 2.10.8 and earlier.
                    // http://bugzilla.xamarin.com/show_bug.cgi?id=2761
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }
    }
}
