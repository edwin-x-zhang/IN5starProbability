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
    enum BannerType
    {
        FiveStar,
        FourStarWithinFive,
        FourStarStandlone
    }

    class Counter
    {
        // TODO:
        // Create methods to generate probability distributions for 4*
        // pieces within the 5* banner as well as standalone 4* banners
        static double[] FiveStarPieceProbabilityTable()
        {
            int maxPulls = 20;
            double baseProbability = 0.015;
            double softPity18 = 0.365;
            double softPity19 = 0.715;
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

        static double[] FourStarWithinFiveProbabilityTable()
        {
            int maxPulls = 10;
            double baseProbability = 0.0329;
            double hardPity10 = 1;

            double[] successChance = new double[maxPulls];
            double[] failureChance = new double[maxPulls];

            successChance[0] = baseProbability;
            failureChance[0] = 1 - baseProbability;
            for (int i = 1; i < 9; i++)
            {
                failureChance[i] = Math.Pow(1 - baseProbability, i + 1);
                successChance[i] = baseProbability * failureChance[i-1];
            }
            failureChance[9] = 0;
            successChance[9] = failureChance[8] * hardPity10;

            return successChance;
        }

        static double[] FourStarStandaloneProbabilityTable()
        {
            int maxPulls = 5;
            double baseProbability = 0.1;
            double softPity4 = 0.2;
            double hardPity5 = 1;

            double[] successChance = new double[maxPulls];
            double[] failureChance = new double[maxPulls];

            successChance[0] = baseProbability;
            failureChance[0] = 1 - baseProbability;
            for (int i = 1; i < 3; i++)
            {
                failureChance[i] = Math.Pow(1 - baseProbability, i + 1);
                successChance[i] = baseProbability * failureChance[i-1];
            }
            failureChance[3] = failureChance[2] * (1 - softPity4);
            failureChance[4] = 0;
            successChance[3] = failureChance[2] * softPity4;
            successChance[4] = failureChance[3] * hardPity5;

            return successChance;
        }

        static void Main(string[] args)
        {
            // TODO: Populate these based on user arguments
            // Hard code for demo purposes
            int totalPieces;
            BannerType Banner = BannerType.FiveStar;

            double[] pullChance;
            string label;

            switch(Banner)
            {
                case BannerType.FourStarStandlone:
                    // Four star standalone banner
                    totalPieces = 9;
                    pullChance = FourStarStandaloneProbabilityTable();
                    label = "FourStarStandaloneOutfit" + totalPieces + "Pieces";
                    break;
                case BannerType.FourStarWithinFive:
                    // Four star outfit within five star banner
                    totalPieces = 9;
                    pullChance = FourStarWithinFiveProbabilityTable();
                    label = "FourStarCompanionOutfit" + totalPieces + "Pieces";
                    break;
                case BannerType.FiveStar:
                default:
                    // Five star banner
                    totalPieces = 10;
                    pullChance = FiveStarPieceProbabilityTable();
                    label = "FiveStarOutfit" + totalPieces + "Pieces";
                    break;
            }

            // Use the Banner class to generate a probability table
            Banner bannerOutfit = new Banner(pullChance, totalPieces, label);
            double[] totalPulls = bannerOutfit.GenerateProbabilityTable();

            bannerOutfit.GenerateCsv(totalPulls);
            Console.WriteLine("Hello gongeous!");
        }
    }
}