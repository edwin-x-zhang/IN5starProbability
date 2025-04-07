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

    static double[] pullCounter(double[] successChance, int pieces)
    {
        double[] totalPulls = new double[20 * pieces];
        double[] totalPulls1 = new double[20 * pieces];
        double[] totalPulls2 = new double[20 * pieces];
        double[] totalPulls3 = new double[20 * pieces];
        double[] totalPulls4 = new double[20 * pieces];
        double[] totalPulls5 = new double[20 * pieces];
        double[] totalPulls6 = new double[20 * pieces];
        double[] totalPulls7 = new double[20 * pieces];
        double[] totalPulls8 = new double[20 * pieces];
        double[] totalPulls9 = new double[20 * pieces];
        double[] totalPulls10 = new double[20 * pieces];
        double[] totalPulls11 = new double[20 * pieces];
        double[] totalPulls12 = new double[20 * pieces];
        double[] totalPulls13 = new double[20 * pieces];
        double[] totalPulls14 = new double[20 * pieces];
        double[] totalPulls15 = new double[20 * pieces];
        double[] totalPulls16 = new double[20 * pieces];
        double[] totalPulls17 = new double[20 * pieces];
        double[] totalPulls18 = new double[20 * pieces];
        double[] totalPulls19 = new double[20 * pieces];
        double[] totalPulls20 = new double[20 * pieces];

        Thread pull1 = new Thread(() => countPieces(totalPulls1, successChance, pieces, 0));
        Thread pull2 = new Thread(() => countPieces(totalPulls2, successChance, pieces, 1));
        Thread pull3 = new Thread(() => countPieces(totalPulls3, successChance, pieces, 2));
        Thread pull4 = new Thread(() => countPieces(totalPulls4, successChance, pieces, 3));
        Thread pull5 = new Thread(() => countPieces(totalPulls5, successChance, pieces, 4));
        Thread pull6 = new Thread(() => countPieces(totalPulls6, successChance, pieces, 5));
        Thread pull7 = new Thread(() => countPieces(totalPulls7, successChance, pieces, 6));
        Thread pull8 = new Thread(() => countPieces(totalPulls8, successChance, pieces, 7));
        Thread pull9 = new Thread(() => countPieces(totalPulls9, successChance, pieces, 8));
        Thread pull10 = new Thread(() => countPieces(totalPulls10, successChance, pieces, 9));
        Thread pull11 = new Thread(() => countPieces(totalPulls11, successChance, pieces, 10));
        Thread pull12 = new Thread(() => countPieces(totalPulls12, successChance, pieces, 11));
        Thread pull13 = new Thread(() => countPieces(totalPulls13, successChance, pieces, 12));
        Thread pull14 = new Thread(() => countPieces(totalPulls14, successChance, pieces, 13));
        Thread pull15 = new Thread(() => countPieces(totalPulls15, successChance, pieces, 14));
        Thread pull16 = new Thread(() => countPieces(totalPulls16, successChance, pieces, 15));
        Thread pull17 = new Thread(() => countPieces(totalPulls17, successChance, pieces, 16));
        Thread pull18 = new Thread(() => countPieces(totalPulls18, successChance, pieces, 17));
        Thread pull19 = new Thread(() => countPieces(totalPulls19, successChance, pieces, 18));
        Thread pull20 = new Thread(() => countPieces(totalPulls20, successChance, pieces, 19));

        pull1.Start();
        pull2.Start();
        pull3.Start();
        pull4.Start();
        pull5.Start();
        pull6.Start();
        pull7.Start();
        pull8.Start();
        pull9.Start();
        pull10.Start();
        pull11.Start();
        pull12.Start();
        pull13.Start();
        pull14.Start();
        pull15.Start();
        pull16.Start();
        pull17.Start();
        pull18.Start();
        pull19.Start();
        pull20.Start();

        pull1.Join();
        pull2.Join();
        pull3.Join();
        pull4.Join();
        pull5.Join();
        pull6.Join();
        pull7.Join();
        pull8.Join();
        pull9.Join();
        pull10.Join();
        pull11.Join();
        pull12.Join();
        pull13.Join();
        pull14.Join();
        pull15.Join();
        pull16.Join();
        pull17.Join();
        pull18.Join();
        pull19.Join();
        pull20.Join();
        for (int i = 0; i < 20 * pieces; i++)
        {
            totalPulls[i] = totalPulls1[i]+totalPulls2[i]+totalPulls3[i]+totalPulls4[i]+totalPulls5[i]+
                            totalPulls6[i]+totalPulls7[i]+totalPulls8[i]+totalPulls9[i]+totalPulls10[i]+
                            totalPulls11[i]+totalPulls12[i]+totalPulls13[i]+totalPulls14[i]+totalPulls15[i]+
                            totalPulls16[i]+totalPulls17[i]+totalPulls18[i]+totalPulls19[i]+totalPulls20[i];
        }

        return totalPulls;
    }

    static void countPieces(double[] totalPulls, double[] successChance, int pieces, int a)
    {
        for (int b = 0; b < 20; b++)
        {
            for (int c = 0; c < 20; c++)
            {
                for (int d = 0; d < 20; d++)
                {
                    for (int e = 0; e < 20; e++)
                    {
                        for (int f = 0; f < 20; f++)
                        {
                            for (int g = 0; g < 20; g++)
                            {
                                for (int h = 0; h < 20; h++)
                                {
                                    for (int i = 0; i < 20; i++)
                                    {
                                        for (int j = 0; j < 20; j++)
                                        {
                                            totalPulls[a+b+c+d+e+f+g+h+i+j+pieces-1] += successChance[a]*successChance[b]*
                                            successChance[c]*successChance[d]*successChance[e]*successChance[f]*
                                            successChance[g]*successChance[h]*successChance[i]*successChance[j];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }

    static void Main(string[] args)
    {
        int maxPulls = 20;
        int pieces = 10;
        double[] successChance = ProbabilityTable(maxPulls);
        double[] totalPulls = pullCounter(successChance, pieces);
        double cumulativeChance = 0;

        using (var writer = new StreamWriter("output.csv"))
        {
            for (int i = 0; i < totalPulls.Length; i++)
            {
                cumulativeChance += totalPulls[i];
                writer.WriteLine(i+1 + "," + totalPulls[i] + "," + cumulativeChance);
            }
        }
        Console.WriteLine("Hello gongeous!");
    }
}