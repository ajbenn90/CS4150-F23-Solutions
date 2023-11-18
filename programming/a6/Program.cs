using System;

class Solution
{
    public static void Main()
    {
        while (true)
        {
            int num = int.Parse(Console.ReadLine());
            if (num == 4)
                break;
            PrimeReduction(num);
        }
    }

    private static void PrimeReduction(int num, int counter = 1)
    {
        if (IsPrime(num))
            Console.WriteLine(num + " " + counter);
        else
            PrimeReduction(PrimeSum(num), counter + 1);
    }

    private static int PrimeSum(int num)
    {
        int sum = 0;
        int i = 2;
        while (i * i <= num)
            if (num % i == 0)
            {
                num /= i;
                sum += i;
            }
            else
                i++;
        if (num > 1)
            sum += num;
        return sum;
    }

    private static bool IsPrime(int num)
    {
        if (num % 2 == 0)
            return false;
        for (int i = 3; i <= Math.Sqrt(num); i += 2)
            if (num % i == 0)
                return false;
        return true;
    }
}