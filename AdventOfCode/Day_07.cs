namespace AdventOfCode;

public class Day_07 : BaseDay
{
    private readonly int[] _crabs;

    public Day_07()
    {
        _crabs = File.ReadAllText(InputFilePath).Split(',').Select(int.Parse).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_crabs).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_crabs).ToString());
    }

    public static int Part1(int[] positions)
    {
        //return FindBestHorizontalPosition(positions, c => c).total;
        return FindViaMedian(positions, c => c).total;
    }

    public static int Part2(int[] positions)
    {
        //return FindBestHorizontalPosition(positions, c => SumZeroToN(c)).total;
        return FindViaBinarySpacePartition(positions, c => SumZeroToN(c)).total;
    }

    public static (int position, int total) FindViaMedian(int[] positions, Func<int, int> calculateFuelCost)
    {
        Array.Sort(positions);
        
        var target = positions[positions.Length / 2];
        var total = 0;

        foreach (var crab in positions)
        {
            total += calculateFuelCost(Math.Abs(target - crab));
        }

        return (target, total);
    }

    public static (int position, int total) FindViaBinarySpacePartition(int[] positions, Func<int, int> calculateFuelCost)
    {
        var mean = positions.Sum() / positions.Length;

        var binarySpace = new[] { mean - 1, mean, mean + 1 };

        var lowestTotal = int.MaxValue;
        var lowestTarget = int.MaxValue;

        foreach (var target in binarySpace)
        {
            var total = 0;

            foreach (var crab in positions)
            {
                total += calculateFuelCost(Math.Abs(target - crab));
            }

            if (total < lowestTotal)
            {
                lowestTarget = target;
                lowestTotal = total;
            }
        }

        return (lowestTarget, lowestTotal);
    }

    public static (int position, int total) FindBestHorizontalPosition(int[] positions, Func<int, int> calculateFuelCost)
    {
        var min = positions.Min();
        var max = positions.Max();

        var lowestTotal = int.MaxValue;
        var lowestTarget = int.MaxValue;

        foreach (var target in Enumerable.Range(min, max - min))
        {
            var total = 0;

            foreach (var crab in positions)
            {
                total += calculateFuelCost(Math.Abs(target - crab));
            }

            if (total < lowestTotal)
            {
                lowestTarget = target;
                lowestTotal = total;
            }
        }

        return (lowestTarget, lowestTotal);
    }

    public static int SumZeroToN(int n)
    {
        return n * (n + 1) / 2;
    }
}
