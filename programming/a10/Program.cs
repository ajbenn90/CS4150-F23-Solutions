using System;
using System.Collections.Generic;

// it's just topological sort
class Solution
{
    public static void Main()
    {
        string[] metadata = Console.ReadLine().Split(' ');
        int numSticks = int.Parse(metadata[0]);
        int numLines = int.Parse(metadata[1]);

        // build dependency graph and get dependency counts
        List<int>[] dependencyGraph = new List<int>[numSticks + 1];
        for (int i = 0; i < dependencyGraph.Length; i++)
            dependencyGraph[i] = new();
        int[] dependencyCounts = new int[dependencyGraph.Length];
        for (int i = 0; i < numLines; i++)
        {
            string[] dependency = Console.ReadLine().Split(' ');
            int top = int.Parse(dependency[0]);
            int bottom = int.Parse(dependency[1]);
            dependencyGraph[top].Add(bottom);
            dependencyCounts[bottom]++;
        }

        List<int> sol = PickUpSticks(dependencyGraph, dependencyCounts);
        if (sol.Count < numSticks) // impossible to pick them all up
            Console.WriteLine("IMPOSSIBLE");
        else
            foreach (int stick in sol)
                Console.WriteLine(stick);
    }

    /// <summary>
    /// Returns an order to remove as many sticks as possible without removing one with another stick on top of it
    /// </summary>
    /// <param name="dependencyGraph">A 1-indexed dependency graph where graph[i] is a list of sticks directly below stick i</param>
    /// <param name="dependencyCounts">dependencyCounts[i] = the number of sticks directly above stick i (in-degree)</param>
    /// <returns>An ordered list of all the sticks as described above</returns>
    public static List<int> PickUpSticks(List<int>[] dependencyGraph, int[] dependencyCounts)
    {
        // remove a stick with nothing on top of it, update the sticks below it, repeat.
        List<int> sol = new(dependencyGraph.Length - 1);
        Queue<int> toRemove = new();
        for (int i = 1; i < dependencyGraph.Length; i++)
            if (dependencyCounts[i] == 0)
                toRemove.Enqueue(i);
        while (toRemove.Count > 0)
        {
            int currRemove = toRemove.Dequeue();
            sol.Add(currRemove);
            foreach (int stick in dependencyGraph[currRemove])
                if (--dependencyCounts[stick] == 0)
                    toRemove.Enqueue(stick);
        }
        return sol;
    }
}