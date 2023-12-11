using System;
using System.Collections.Generic;

// it's just DFS from house 1
class Solution
{
    public static void Main()
    {
        string[] metadata = Console.ReadLine().Split(' ');
        int numHouses = int.Parse(metadata[0]);
        int numCables = int.Parse(metadata[1]);

        List<int>[] graph = new List<int>[numHouses + 1];
        for (int i = 0; i < graph.Length; i++)
            graph[i] = new();
        for (int i = 0; i < numCables; i++)
        {
            string[] cable = Console.ReadLine().Split(' ');
            int house1 = int.Parse(cable[0]);
            int house2 = int.Parse(cable[1]);
            graph[house1].Add(house2);
            graph[house2].Add(house1);
        }

        List<int> noInternet = GetInternetless(graph);
        if (noInternet.Count == 0)
            Console.WriteLine("Connected");
        else
            foreach (int house in noInternet)
                Console.WriteLine(house);
    }

    /// <summary>
    /// Returns an ordered list of houses without internet
    /// </summary>
    /// <param name="graph">A 1-indexed graph where graph[i] is a list of houses with a connection to house i</param>
    /// <returns></returns>
    public static List<int> GetInternetless(List<int>[] graph)
    {
        // run DFS from house 1 to find houses with internet
        bool[] hasInternet = new bool[graph.Length];
        Stack<int> stack = new();
        stack.Push(1);
        while (stack.Count > 0)
        {
            int currHouse = stack.Pop();
            // this one is already processed
            if (hasInternet[currHouse])
                continue;
            hasInternet[currHouse] = true;
            foreach (int connectedHouse in graph[currHouse])
                stack.Push(connectedHouse);
        }

        List<int> noInternet = new();
        for (int house = 1; house < hasInternet.Length; house++)
            if (!hasInternet[house])
                noInternet.Add(house);

        return noInternet;
    }
}