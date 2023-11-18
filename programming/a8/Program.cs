using System;
using System.Linq;

class Solution
{
    public static void Main()
    {
        while (true)
        {
            string[] input = Console.ReadLine().Split(' ');
            // last line of input
            if (input[0] == "0")
                break;
            int[] foods = new int[input.Length - 1];
            // first number in each line doesn't matter
            for (int i = 1; i < input.Length; i++)
                foods[i - 1] = int.Parse(input[i]);

            (int, int) divided = DivideFood(foods);
            Console.WriteLine(divided.Item1 + " " + divided.Item2);
        }
    }

    public static (int, int) DivideFood(int[] foods)
    {
        int sum = foods.Sum();
        int runningSum = 0;
        // get all the possible subset sums up to sum / 2 (since that's as far as we care)
        // subsetSums[i] = true IFF there exists a subset that sums to i
        bool[] subsetSums = new bool[sum / 2 + 1];
        subsetSums[0] = true;
        for (int i = 0; i < foods.Length; i++)
        {
            runningSum += foods[i];
            // must iterate down so changes for j = foods[i] don't affect changes for j = a multiple of foods[i]
            // only sums up to sum / 2 matter
            // sum[j - foods[i]] cannot be true if runningSum < j
            for (int j = Math.Min(runningSum, sum / 2); j >= foods[i]; j--)
                if (subsetSums[j - foods[i]])
                    subsetSums[j] = true;
        }

        // the closest subset_sum <= (sum / 2) since it is optimal for meal 2
        int sol = subsetSums.Length - 1;
        for (; !subsetSums[sol]; sol--) {}
        // larger calorie sum must be first
        return (sum - sol, sol);
    }
}