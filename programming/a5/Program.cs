using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Find the current route he takes using Dijkstra's with no constraints
/// Calculate the max total length our new route can be.
/// Find the minimum maximum highway length by binary searching through every highway length in the map
///     Run Dijkstra's with the constraint that no highway on the path can be longer than our current binary search value
///     If the path from home to work doesn't exist or is longer than the maximum allowed, increase the lower bound
///     Else, reduce the upper bound to see if we can find a better solution
/// The lower bound is the solution
/// </summary>
class Solution
{
    public static void Main()
    {
        string[] metadata = Console.ReadLine().Split(' ');
        int numCities = int.Parse(metadata[0]);
        int numHighways = int.Parse(metadata[1]);
        int allowedPercentIncrease = int.Parse(metadata[2]);

        // get highway data ready for map constructor and build sorted set of highway lengths for binary search
        (int city1, int city2, int length)[] highways = new (int, int, int)[numHighways];
        int[] allHighwayLengths = new int[numHighways];
        for (int i = 0; i < numHighways; i++)
        {
            // city 1, city 2, length
            string[] highwayData = Console.ReadLine().Split(' ');
            // subtract 1 from city name, so it's 0-indexed
            highways[i] = (int.Parse(highwayData[0]) - 1, int.Parse(highwayData[1]) - 1, int.Parse(highwayData[2]));
            allHighwayLengths[i] = highways[i].length;
        }
        // O(n) way to get sorted set
        int[] uniqueHighwayLengths = new SortedSet<int>(allHighwayLengths).ToArray();

        CityMap map = new(numCities, highways);

        // the maximum total distance the new route can be
        long maxDist = map.FindBestRoute() * (100 + allowedPercentIncrease) / 100;

        // binary search for a route with the min max highway length and whose total distance <= maxDist
        int lowerBoundIdx = 0;
        int upperBoundIdx = uniqueHighwayLengths.Length - 1;
        while (lowerBoundIdx <= upperBoundIdx)
        {
            int currIdx = lowerBoundIdx + ((upperBoundIdx - lowerBoundIdx) / 2);
            if (map.FindBestRoute(uniqueHighwayLengths[currIdx]) <= maxDist)
                // valid path. check if there's a lower valid max highway length
                upperBoundIdx = currIdx - 1;
            else
                // invalid path. try a higher max highway length
                lowerBoundIdx = currIdx + 1;
        }

        Console.WriteLine(uniqueHighwayLengths[lowerBoundIdx]);
    }

    private class CityMap
    {
        private City[] Cities {get;}

        /// <summary>
        /// Builds map with given number of cities and highway data
        /// </summary>
        /// <param name="numCities">The number of cities</param>
        /// <param name="highways">Array of tuples representing highways</param>
        public CityMap(int numCities, (int city1Idx, int city2Idx, int length)[] highways)
        {
            Cities = new City[numCities];
            for (int i = 0; i < Cities.Length; i++)
                Cities[i] = new();
            foreach (var (city1Idx, city2Idx, length) in highways)
            {
                City city1 = Cities[city1Idx];
                City city2 = Cities[city2Idx];
                city1.Neighbors.Add((city2, length));
                city2.Neighbors.Add((city1, length));
            }
        }

        /// <summary>
        /// Returns (the length of the shortest route from home to work that doesn't use highways longer than maxHighwayLength) or (long.MaxValue if no such path exists)
        /// </summary>
        /// <param name="maxHighwayLength">The longest highway allowed in the path. default: int.MaxValue (i.e. no constraint)</param>
        /// <returns>The length of the path or long.MaxValue if no path exists</returns>
        public long FindBestRoute(int maxHighwayLength = int.MaxValue)
        {
            // dijkstra prep (long.MaxValue = infinity)
            foreach (City city in Cities)
                city.DistFromHome = long.MaxValue;

            // dijkstra's w/ constraint that no edge on path can be longer than maxHighwayLength
            // Cities[0] is home
            // Cities[^1] is work
            PriorityQueue<City, long> pq = new();
            Cities[0].DistFromHome = 0;
            pq.Enqueue(Cities[0], 0);
            while (pq.TryDequeue(out City currCity, out long distFromHomeWhenInserted))
            {
                // this happens when a city was added more than once. only the copy with the smaller priority matters
                if (distFromHomeWhenInserted > currCity.DistFromHome)
                    continue;
                foreach ((City neighbor, int distToNeighbor) in currCity.Neighbors)
                {
                    if (distToNeighbor > maxHighwayLength)
                        continue;
                    long newDist = currCity.DistFromHome + distToNeighbor;
                    if (newDist < neighbor.DistFromHome)
                    {
                        neighbor.DistFromHome = newDist;
                        pq.Enqueue(neighbor, newDist);
                    }
                }
            }
            return Cities[^1].DistFromHome;
        }

        private class City
        {
            public List<(City neighbor, int dist)> Neighbors {get;}
            public long DistFromHome {get; set;}

            public City()
            {
                Neighbors = new();
            }
        }
    }
}