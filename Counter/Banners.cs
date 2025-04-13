using System;

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
        return ProbabilityTableHelper(this.probabilityTable, 1);
    }

    private double[] ProbabilityTableHelper(double[] PreviousTable, int piece)
    {
        if (piece == maxPieces)
        {
            return PreviousTable;
        }
        else
        {
            double[] successChance = new double[maxPullCount * (piece+1)];

            for (int i = 0; i < maxPullCount; i++) // count through the possibilties of the current piece
            {
                for (int j = 0; j < PreviousTable.Length; j++)
                {
                    successChance[i+j] += probabilityTable[i] * PreviousTable[j];
                }
            }

            return ProbabilityTableHelper(successChance, piece+1);
        }
    }

    public void GenerateCsv(double[] table)
    {
        double cumulativeChance = 0;
        using (var writer = new StreamWriter(fileLabel + "_output.csv"))
        {
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
