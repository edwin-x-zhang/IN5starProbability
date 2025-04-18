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
