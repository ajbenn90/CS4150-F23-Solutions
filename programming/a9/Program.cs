using System;
using System.Collections.Generic;

class Solution
{
    private const int WidthSmall = 1;
    private const int WidthMed = 2;
    private const int WidthLarge = 3;

    public static void Main()
    {
        string[] numBooks = Console.ReadLine().Split(' ');
        int numSmall = int.Parse(numBooks[0]);
        int numMed = int.Parse(numBooks[1]);
        int numLarge = int.Parse(numBooks[2]);
        int shelfWidth = int.Parse(Console.ReadLine());

        // Console.WriteLine(GetShelvesGreedy(numSmall, numMed, numLarge, shelfWidth));
        Console.WriteLine(GetShelvesRecursive(numSmall, numMed, numLarge, shelfWidth));
    }

    // adopted from Benjamin Rose's submission
    // greedy solution: try fill shelves completely with largest books possible
    public static int GetShelvesGreedy(int numSmall, int numMed, int numLarge, int shelfWidth)
    {
        int numShelves = 0;
        while (numSmall + numMed + numLarge > 0)
        {
            numShelves++;
            int widthLeft = shelfWidth;
            if (numLarge > 0)
            {
                int numUsed = NumBooksToUse(widthLeft, numLarge, WidthLarge);
                // if 1 space left, can use full shelf by replacing last large with 2 medium
                if (widthLeft - (WidthLarge * numUsed) == 1 && numMed >= 2)
                    numUsed--;
                numLarge -= numUsed;
                widthLeft -= WidthLarge * numUsed;
            }
            if (numMed > 0)
            {
                int numUsed = NumBooksToUse(widthLeft, numMed, WidthMed);
                numMed -= numUsed;
                widthLeft -= WidthMed * numUsed;
            }
            if (numSmall > 0)
                numSmall -= NumBooksToUse(widthLeft, numSmall, WidthSmall);
        }
        return numShelves;
    }

    private static int NumBooksToUse(int shelfWidth, int booksLeft, int bookWidth)
    {
        return bookWidth * booksLeft < shelfWidth ? booksLeft : shelfWidth / bookWidth;
    }

    // adpated from Aaron Schindler's solution
    // recursion + memoization
    public static int GetShelvesRecursive(int numSmall, int numMed, int numLarge, int shelfWidth)
    {
        return Recursion(numSmall, numMed, numLarge, 0, shelfWidth, new());
    }

    private static int Recursion(int numSmall, int numMed, int numLarge, int spaceUsed, int shelfWidth, Dictionary<(int numSmall, int numMed, int numLarge, int spaceUsed), int> memo)
    {
        // base case
        if (numSmall + numMed + numLarge == 0)
            return 1;
        // memoization
        if (memo.ContainsKey((numSmall, numMed, numLarge, spaceUsed)))
            return memo[(numSmall, numMed, numLarge, spaceUsed)];
        
        // try using each size and pick the one that uses the least space
        int useSmall = int.MaxValue;
        int useMed = int.MaxValue;
        int useLarge = int.MaxValue;
        if (numLarge > 0)
            if (shelfWidth - spaceUsed >= WidthLarge)
                useLarge = Recursion(numSmall, numMed, numLarge - 1, spaceUsed + WidthLarge, shelfWidth, memo);
            else
                useLarge = 1 + Recursion(numSmall, numMed, numLarge - 1, WidthLarge, shelfWidth, memo);
        if (numMed > 0)
            if (shelfWidth - spaceUsed >= WidthMed)
                useMed = Recursion(numSmall, numMed - 1, numLarge, spaceUsed + WidthMed, shelfWidth, memo);
            else
                useMed = 1 + Recursion(numSmall, numMed - 1, numLarge, WidthMed, shelfWidth, memo);
        if (numSmall > 0)
            if (shelfWidth - spaceUsed >= WidthSmall)
                useSmall = Recursion(numSmall - 1, numMed, numLarge, spaceUsed + WidthSmall, shelfWidth, memo);
            else
                useSmall = 1 + Recursion(numSmall - 1, numMed, numLarge, WidthSmall, shelfWidth, memo);
        int best = Math.Min(useSmall, Math.Min(useMed, useLarge));
        memo[(numSmall, numMed, numLarge, spaceUsed)] = best;
        return best;
    }
}