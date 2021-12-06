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
        var dict = CreatePopulationDict();

        foreach (var item in initialState)
        {
            dict[item]++;
        }

        for (int i = 0; i < days; i++)
        {
            var newDict = CreatePopulationDict();

            foreach (var item in dict)
            {
                if(item.Key == 0)
                {
                    newDict[8] += item.Value;
                    newDict[6] += item.Value;
                }
                else
                {
                    newDict[item.Key - 1] += item.Value;
                }
            }

            dict = newDict;
        }

        long sum = 0;

        foreach (var item in dict)
        {
            sum += item.Value;
        }

        return sum;
    }

    private static Dictionary<int,long> CreatePopulationDict()
    {
        var population = new Dictionary<int, long>();

        foreach (var key in Enumerable.Range(0, 9))
        {
            population.Add(key, 0L);
        }

        return population;
    }
}
