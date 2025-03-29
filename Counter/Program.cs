// See https://aka.ms/new-console-template for more information
class Counter
{
    static double[] ProbabilityTable(int maxPulls)
    {
        if (maxPulls != 20)
        {
            throw new ArgumentException(
            "Pull count must be 20.");
        }
        double baseProbability = 0.015;
        double softPity18 = 0.45;
        double softPity19 = 0.55;
        double hardPity20 = 1;

        double[] successChance = new double[maxPulls];
        double[] failureChance = new double[maxPulls];

        successChance[0] = baseProbability;
        failureChance[0] = 1 - baseProbability;
        for (int i = 1; i < 17; i++)
        {
            failureChance[i] = Math.Pow(1 - baseProbability, i + 1);
            successChance[i] = baseProbability * failureChance[i-1];
        }
        failureChance[17] = failureChance[16] * (1 - softPity18);
        failureChance[18] = failureChance[17] * (1 - softPity19);
        failureChance[19] = 0;
        successChance[17] = failureChance[16] * softPity18;
        successChance[18] = failureChance[17] * softPity19;
        successChance[19] = failureChance[18] * hardPity20;

        return successChance;
    }

    static void twoPullCounter(double[] successChance)
    {
        double[] totalPulls = new double[40];
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                totalPulls[i+j] += successChance[i] * successChance[j];
            }
        }
        double sanityCheck = 0;
        for (int i = 0; i < 40; i++)
        {
            Console.WriteLine(100*totalPulls[i]);
            sanityCheck += totalPulls[i];
        }
        Console.WriteLine("Sanity check: all probabilities should add to 1!");
        Console.WriteLine(sanityCheck);
    }
    static void Main(string[] args)
    {
        int maxPulls = 20;
        Console.WriteLine("Hello gongeous!");
        double[] successChance = ProbabilityTable(maxPulls);
        twoPullCounter(successChance);
        /*
        for (int i = 0; i < maxPulls; i++)
        {
            Console.WriteLine(100*successChance[i]);
        }
        */
    }
}