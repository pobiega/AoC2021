namespace AdventOfCode;

public class Day_02 : BaseDay
{
    private readonly string _input;
    private readonly List<(string, int)> _parsed;

    public Day_02()
    {
        _input = File.ReadAllText(InputFilePath);
        _parsed = _input
            .Split('\n')
            .Select(x =>
            {
                var data = x.Split();
                return (data[0], int.Parse(data[1]));
            })
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_parsed).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_parsed).ToString());
    }

    public static int Part1(IReadOnlyList<(string, int)> input)
    {
        var horizontal = 0;
        var depth = 0;

        foreach (var item in input)
        {
            switch (item)
            {
                case ("up", int dd):
                    depth -= dd;
                    break;
                case ("down", int dd):
                    depth += dd;
                    break;
                case ("forward", int dx):
                    horizontal += dx;
                    break;
            }
        }

        return horizontal * depth;
    }

    public static int Part2(IReadOnlyList<(string, int)> input)
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;

        foreach (var item in input)
        {
            switch (item)
            {
                case ("up", int ax):
                    aim -= ax;
                    break;
                case ("down", int ax):
                    aim += ax;
                    break;
                case ("forward", int dx):
                    horizontal += dx;
                    depth += aim * dx;
                    break;
            }
        }

        return horizontal * depth;
    }
}