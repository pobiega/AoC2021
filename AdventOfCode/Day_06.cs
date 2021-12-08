namespace AdventOfCode;

public class Day_06 : BaseDay
{
    private readonly int[] _lanternfish;

    public Day_06()
    {
        _lanternfish = File.ReadAllText(InputFilePath).Split(',').Select(int.Parse).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_lanternfish).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_lanternfish).ToString());
    }

    public static int Part1(int[] input)
    {
        return (int)BreedLanternfishLong(input, 80);
    }

    public static long Part2(int[] input)
    {
        return BreedLanternfishLong(input, 256);
    }

    private static long BreedLanternfishLong(int[] initialState, int days)
    {
        var dict = new long[9];

        foreach (var item in initialState)
        {
            dict[item]++;
        }

        for (int day = 0; day < days; day++)
        {
            var newDict = new long[9];

            for (int i = 0; i < dict.Length; i++)
            {
                if (i == 0)
                {
                    newDict[8] += dict[i];
                    newDict[6] += dict[i];
                }
                else
                {
                    newDict[i - 1] += dict[i];
                }
            }

            dict = newDict;
        }

        long sum = 0;

        foreach (var item in dict)
        {
            sum += item;
        }

        return sum;
    }
}
