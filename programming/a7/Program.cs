using System;
using System.Collections.Generic;

class Solution
{
    public static void Main()
    {
        while (true)
        {
            // get the input
            string[] metadata = Console.ReadLine().Split(' ');
            int numHeads = int.Parse(metadata[0]);
            int numKnights = int.Parse(metadata[1]);
            // last line of input is "0 0"
            if (numHeads == 0)
                break;

            List<int> heads = new();
            List<int> knights = new();
            for (int i = 0; i < numHeads; i++)
                heads.Add(int.Parse(Console.ReadLine()));
            for (int i = 0; i < numKnights; i++)
                knights.Add(int.Parse(Console.ReadLine()));
            heads.Sort();
            knights.Sort();

            // calc and print
            long money = SlayTheGoose(heads, knights);
            if (money == -1)
                Console.WriteLine("Loowater is doomed!");
            else
                Console.WriteLine(money);
        }
    }

    /// <summary>
    /// Returns the minimum required money to slay the goose if possible and -1 otherwise.
    /// </summary>
    /// <param name="heads"></param>
    /// <param name="knights"></param>
    /// <returns></returns>
    public static long SlayTheGoose(List<int> heads, List<int> knights)
    {
        long totalMoney = 0;
        int headIdx = 0;
        int knightIdx = 0;
        while (headIdx < heads.Count && knightIdx < knights.Count)
        {
            if (knights[knightIdx] >= heads[headIdx])
            {
                totalMoney += knights[knightIdx];
                headIdx++;
            }
            knightIdx++;
        }
        if (headIdx == heads.Count)
            return totalMoney;
        return -1;
    }
}