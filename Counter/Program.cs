// See https://aka.ms/new-console-template for more information
using System;
using System.Threading;
using System.IO;

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

    static double[] ProbabilityTableX(double[] PreviousTable, double[] singlePieceChance, int piece, int maxPieces)
    {
        if (piece == maxPieces)
        {
            return PreviousTable;
        }
        else
        {
            double[] successChance = new double[20 * (piece+1)];

            for (int i = 0; i < 20; i++) // count through the possibilties of the current piece
            {
                for (int j = 0; j < PreviousTable.Length; j++)
                {
                    successChance[i+j] += singlePieceChance[i] * PreviousTable[j];
                }
            }

            return ProbabilityTableX(successChance, singlePieceChance, piece+1, maxPieces);
        }
    }



    static void Main(string[] args)
    {
        int maxPulls = 20;
        int pieces = 20;
        double[] successChance = ProbabilityTable(maxPulls);
        double[] totalPulls = ProbabilityTableX(successChance, successChance, 1, pieces);
        double cumulativeChance = 0;

        using (var writer = new StreamWriter("output.csv"))
        {
            for (int i = 1; i < pieces; i++)
            {
                writer.WriteLine(i + "," + 0 + "," + 0);
            }
            for (int j = 0; j <= totalPulls.Length-pieces; j++)
            {
                cumulativeChance += totalPulls[j];
                writer.WriteLine(j+pieces + "," + totalPulls[j] + "," + cumulativeChance);
            }
        }
        Console.WriteLine("Hello gongeous!");
    }
}