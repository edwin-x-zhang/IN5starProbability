// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
using System.IO;

namespace Counter
{
    class Counter
    {
        // TODO: Create methods to generate probability distributions for 4* pieces
        //       within the 5* banner as well as standalone 4* banners
        static double[] FiveStarPieceProbabilityTable()
        {
            int maxPulls = 20;
            double baseProbability = 0.015;
            double softPity18 = 0.4;
            double softPity19 = 0.7;
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
            // TODO: Populate these based on user arguments
            // Hard code to 10-piece 5* evo for demo purposes
            int totalPieces = 20; // 20 pieces for 10-piece 5* full evolution
            double[] pullChance = FiveStarPieceProbabilityTable(); // Probability distribution for 5* pieces
            string label = "FiveStarOutfitEvo";

            Banner bannerOutfit = new Banner(pullChance, totalPieces, label);
            double[] totalPulls = bannerOutfit.GenerateProbabilityTable();

            bannerOutfit.GenerateCsv(totalPulls);
            Console.WriteLine("Hello gongeous!");
        }
    }
}