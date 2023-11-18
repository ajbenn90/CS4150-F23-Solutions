using System;
using System.Numerics;

class Solution
{
    public static void Main()
    {
        string[] metadata = Console.ReadLine().Split(' ');
        Console.WriteLine(new Solution().Batmanacci(int.Parse(metadata[0]), BigInteger.Parse(metadata[1])));
    }

    public char Batmanacci(int n, BigInteger k)
    {
        if (n > 2)
        {
            // lens[i] = length of i_th string in sequence
            // only the lengths up to n - 2 matter
            BigInteger[] lens = new BigInteger[n - 1];
            lens[0] = 0;
            lens[1] = 1;
            for (int i = 2; i <= n - 2; i++)
                lens[i] = lens[i - 2] + lens[i - 1];

            // once k is 1, it won't change
            while (k != 1)
                // char k comes from str_(n-2)
                if (k <= lens[n - 2])
                    n -= 2;
                // char k comes from str_(n-1)
                else
                {
                    k -= lens[n - 2];
                    n -= 1;
                }
        }
        // the first letter alternates for every value of n (N for odd n, A for even)
        return n % 2 == 1 ? 'N' : 'A';
    }
}