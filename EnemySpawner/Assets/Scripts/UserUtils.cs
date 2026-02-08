using System;

public class UserUtils
{
    private static Random s_random = new Random();

    public static int GenerateRandomNumber(int min, int max)
    {
        return s_random.Next(min, max);
    }

    public static double GenerateFloatRandomNumber()
    {
        return s_random.NextDouble();
    }
}
