using System;
using System.Collections.Generic;

class Solution
{
    public static void Main()
    {
        string[] metadata = Console.ReadLine().Split(' ');
        int numCountries = int.Parse(metadata[0]);
        int numPartnerships = int.Parse(metadata[1]);
        int homeCountry = int.Parse(metadata[2]);
        int firstToLeave = int.Parse(metadata[3]);

        // create trade map
        Country[] countries = new Country[numCountries + 1];
        for (int i = 1; i <= numCountries; i++)
            countries[i] = new();
        for (int i = 0; i < numPartnerships; i++)
        {
            string[] partnersStr = Console.ReadLine().Split(' ');
            int country1 = int.Parse(partnersStr[0]);
            int country2 = int.Parse(partnersStr[1]);
            countries[country1].InitPartners.Add(countries[country2]);
            countries[country2].InitPartners.Add(countries[country1]);
        }

        // the first country leaves (which updates the others)
        countries[firstToLeave].Leave();

        // print if home country left
        if (countries[homeCountry].DidLeave)
            Console.WriteLine("leave");
        else
            Console.WriteLine("stay");
    }

    private class Country
    {
        public List<Country> InitPartners {get;}
        public bool DidLeave {get; private set;}
        public int NumPartnersGone {get; private set;}

        public Country()
        {
            InitPartners = new();
            DidLeave = false;
            NumPartnersGone = 0;
        }

        public void Leave()
        {
            Queue<Country> leavingQueue = new();
            leavingQueue.Enqueue(this);
            while (leavingQueue.Count > 0)
            {
                Country countryLeaving = leavingQueue.Dequeue();
                if (countryLeaving.DidLeave) // we already processed it
                    continue;
                countryLeaving.DidLeave = true;
                // when a country leaves, its partners need to be updated
                foreach (Country partner in countryLeaving.InitPartners)
                {
                    partner.NumPartnersGone++;
                    // country leaves if at least half of its partners have left
                    if (2 * partner.NumPartnersGone >= partner.InitPartners.Count)
                        leavingQueue.Enqueue(partner);
                }
            }
        }
    }
}