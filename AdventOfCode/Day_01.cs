namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly string _input;
    private readonly int[] _numbers;

    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);

        _numbers = _input
            .Split('\n')
            .Select(x => int.Parse(x))
            .ToArray();
    }

    public override ValueTask<string> Solve_1() => new(Part1(_numbers).ToString());

    public override ValueTask<string> Solve_2() => new(Part2(_numbers).ToString());

    public static int Part1(Span<int> input)
    {
        int? current = null;
        int acc = 0;

        foreach (var item in input)
        {
            if (current != null)
            {
                if (item > current)
                {
                    acc++;
                }
            }
            current = item;
        }

        return acc;
    }

    public static int Part2(int[] input)
    {
        var sums = input.SlidingWindow(3).Select(w => w.Sum()).ToArray();

        return Part1(sums);
    }
}
