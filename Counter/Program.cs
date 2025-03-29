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
    static void Main(string[] args)
    {
        int maxPulls = 20;
        Console.WriteLine("Hello gongeous!");
        double[] successChance = ProbabilityTable(maxPulls);
        for (int i = 0; i < maxPulls; i++)
        {
            Console.WriteLine(100*successChance[i]);
        }

    }
}