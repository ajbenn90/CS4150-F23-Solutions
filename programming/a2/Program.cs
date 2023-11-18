using System;

public class Solution
{
    public static void Main()
    {
        // read the data
        string[] metadata = Console.ReadLine().Split(' ');
        int numSections = int.Parse(metadata[0]);
        int totalTime = int.Parse(metadata[1]);
        int[] dists = new int[numSections];
        int[] speeds = new int[numSections];
        for (int i = 0; i < numSections; i++)
        {
            string[] line = Console.ReadLine().Split(' ');
            dists[i] = int.Parse(line[0]);
            speeds[i] = int.Parse(line[1]);
        }

        Console.WriteLine(CalcSpeedDiff(dists, speeds, totalTime));
    }

    private static double CalcTime(int[] dists, int[] speeds, double diff)
    {
        double time = 0;
        for (int i = 0; i < dists.Length; i++)
            time += dists[i] / (speeds[i] + diff);
        return time;
    }

    public static double CalcSpeedDiff(int[] dists, int[] speeds, int totalTime)
    {
        double lowBound = -1000; // min value given problem constraints
        double upBound = 1e6 + 1e3; // max value given problem constraints
        // find lower bound from given speeds
        // diff can't be lower than -min_speed
        for (int i = 0; i < speeds.Length; i++)
            lowBound = Math.Max(lowBound, -speeds[i]);

        // binary search until we find solution within error bounds
        double diff = 0; // temp val
        while (upBound - lowBound > 1e-6)
        {
            diff = lowBound + ((upBound - lowBound) / 2);
            // if diff is too low, we overestimated the time
            if (totalTime - CalcTime(dists, speeds, diff) < 0)
                lowBound = diff;
            else
                upBound = diff;
        }

        return diff;
    }
}