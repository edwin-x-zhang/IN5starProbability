/**
    Main program. Generates probability tables for individual pieces based on hard coded
    pity values, then counts the probability distribution for multiple pieces, and finally
    exports the results to a csv.

    Copyright (c) 2025 Edwin Xingyun Zhang

    Permission is hereby granted, free of charge, to any person obtaining a copy of this 
    software and associated documentation files (the "Software"), to deal in the Software
    without restriction, including without limitation the rights to use, copy, modify, merge,
    publish, distribute, sublicense, and/or sell copies of the Software, and to permit 
    persons to whom the Software is furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all copies
    or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
    PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
    OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
    DEALINGS IN THE SOFTWARE.
**/

namespace Counter
{
    class Counter
    {
        // TODO:
        // Create methods to generate probability distributions for 4*
        // pieces within the 5* banner as well as standalone 4* banners
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
            string label = "FiveStarOutfit" + totalPieces + "Pieces"; // File label

            // Use the Banner class to generate a probability table
            Banner bannerOutfit = new Banner(pullChance, totalPieces, label);
            double[] totalPulls = bannerOutfit.GenerateProbabilityTable();

            bannerOutfit.GenerateCsv(totalPulls);
            Console.WriteLine("Hello gongeous!");
        }
    }
}