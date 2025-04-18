/**
    Defines the "Banner" class. The constructor takes three arguments: the probability
    distribution table of the type of piece the user would like to count (for example
    5*, 4* within 5*, or 4* standalone), the number of pieces to count, and a label
    string for the output file. Public methods are provided to generate a probability
    table for the number of pieces and export a probability table to a csv.

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

namespace Counter;
public class Banner
{
    private double[] probabilityTable;
    private int maxPullCount;
    private int maxPieces;
    private string fileLabel;

    public Banner(double[] table, int pieces, string label)
    {
        probabilityTable = table;
        maxPullCount = probabilityTable.Length;
        maxPieces = pieces;
        fileLabel = label;
    }

    public double[] GenerateProbabilityTable()
    {
        // Start the count with 1 piece
        return ProbabilityTableHelper(this.probabilityTable, 1);
    }

    private double[] ProbabilityTableHelper(double[] PreviousTable, int piece)
    {
        if (piece == maxPieces)
        {
            // If we've counted all the pieces we want, just return the result
            return PreviousTable;
        }
        else
        {
            double[] successChance = new double[maxPullCount * (piece+1)];

            // Count through the possibilties of the current piece
            for (int i = 0; i < maxPullCount; i++)
            {
                // Generate a new probability table with the probability of the
                // current piece together with the previous probability table
                for (int j = 0; j < PreviousTable.Length; j++)
                {
                    successChance[i+j] += probabilityTable[i] * PreviousTable[j];
                }
            }

            // Use the new probability table to count the next piece
            return ProbabilityTableHelper(successChance, piece+1);
        }
    }

    public void GenerateCsv(double[] table)
    {
        double cumulativeChance = 0;
        using (var writer = new StreamWriter(fileLabel + "_output.csv"))
        {
            // Because of 0-based counting, the blank entries in the probability
            // table will be at the beginning rather than the end. Turn this
            // order around when printing the output for better readability.
            for (int i = 1; i < maxPieces; i++)
            {
                writer.WriteLine(i + "," + 0 + "," + 0);
            }
            for (int j = 0; j <= table.Length-maxPieces; j++)
            {
                cumulativeChance += table[j];
                writer.WriteLine(j+maxPieces + "," + table[j] + "," + cumulativeChance);
            }
        }
    }
}
