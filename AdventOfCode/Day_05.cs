namespace AdventOfCode;

public class Day_05 : BaseDay
{
    private readonly string[] _input;
    private readonly Line[] _lines;

    public Day_05()
    {
        _input = File.ReadAllLines(InputFilePath);
        _lines = ParseLines(_input).ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_lines).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_lines).ToString());
    }

    public static IEnumerable<Line> ParseLines(IEnumerable<string> input)
    {
        foreach (var item in input)
        {
            var split = item.Split("->", StringSplitOptions.TrimEntries);
            var first = split[0].Split(',');
            var second = split[1].Split(',');

            var x1 = int.Parse(first[0]);
            var y1 = int.Parse(first[1]);
            var x2 = int.Parse(second[0]);
            var y2 = int.Parse(second[1]);

            var p1 = new Point(x1, y1);

            var p2 = new Point(x2, y2);

            yield return new Line
            {
                Start = p1,
                End = p2
            };
        }
    }

    public static IEnumerable<Line> OnlyStraightLines(IEnumerable<Line> lines)
    {
        return lines.Where(l => (l.Start.X == l.End.X) || (l.Start.Y == l.End.Y));
    }

    public static int Part1(IEnumerable<Line> lines)
    {
        var lineDepth = new Dictionary<Point, int>();

        var allNodes = OnlyStraightLines(lines).SelectMany(l => ExtrapolateLine(l));

        var grouped = allNodes.GroupBy(p => p).ToArray();

        var aboveOne = grouped.Where(g => g.Count() > 1);

        return aboveOne.Count();
    }

    public static int Part2(IEnumerable<Line> lines)
    {
        var lineDepth = new Dictionary<Point, int>();

        var allNodes = lines.SelectMany(l => ExtrapolateLine(l));

        var grouped = allNodes.GroupBy(p => p).ToArray();

        var aboveOne = grouped.Where(g => g.Count() > 1);

        return aboveOne.Count();
    }

    public static IEnumerable<Point> ExtrapolateLine(Line line)
    {
        var tX = line.Start.X - line.End.X;
        var tY = line.Start.Y - line.End.Y;

        var dX = tX > 0 ? -1 : tX == 0 ? 0 : 1;
        var dY = tY > 0 ? -1 : tY == 0 ? 0 : 1;

        var current = line.Start;

        yield return current;

        while(current != line.End)
        {
            current = current with { X = current.X + dX, Y = current.Y + dY };
            yield return current;
        }
    }
}

public record struct Point(int X, int Y)
{
    public override string ToString()
    {
        return $"{X},{Y}";
    }
}

public record Line
{
    public Point Start;
    public Point End;

    public override string ToString()
    {
        return $"{Start} -> {End}";
    }
}
