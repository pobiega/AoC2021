namespace AdventOfCode;

public class Day_11 : BaseDay
{
    private readonly Dictionary<Point, byte> _data;
    private readonly Dictionary<Point, byte> _data2;

    public Day_11()
    {
        _data = ParseInput(File.ReadAllLines(InputFilePath));
        _data2 = ParseInput(File.ReadAllLines(InputFilePath));
    }

    public static Dictionary<Point, byte> ParseInput(IEnumerable<string> input)
    {
        var dict = new Dictionary<Point, byte>();

        input.Tap((s, y) =>
            s.Tap((c, x) =>
                dict.Add(new Point(x, y), c.ToByte())).ToArray()).ToArray();

        return dict;
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_data).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_data2).ToString());
    }

    public static int Part2(Dictionary<Point, byte> data)
    {
        for (int i = 0; i < 1000; i++)
        {
            PerformStepInplace(data);

            if (data.Values.All(b => b == 0))
            {
                return i + 1;
            }
        }

        return -1;
    }

    public static int Part1(Dictionary<Point, byte> data)
    {
        var total = 0;

        for (int i = 0; i < 100; i++)
        {
            total += PerformStepInplace(data);
        }

        return total;
    }

    public static int PerformStepInplace(Dictionary<Point, byte> data)
    {
        var flashedOctopi = new HashSet<Point>();

        foreach (var kvp in data)
        {
            data[kvp.Key] += 1;
        }

        foreach (var key in data.Keys)
        {
            if (data[key] > 9)
            {
                Flash(key, data, flashedOctopi);
            }
        }

        foreach (var loc in flashedOctopi)
        {
            data[loc] = 0;
        }

        return flashedOctopi.Count;
    }

    private static void Flash(Point loc, Dictionary<Point, byte> data, HashSet<Point> flashed)
    {
        if (!(data[loc] > 9))
            return;

        if (!flashed.Add(loc))
            return;

        foreach (var adjacent in loc.GetAdjacent8())
        {
            if (data.ContainsKey(adjacent))
            {
                data[adjacent] += 1;
                Flash(adjacent, data, flashed);
            }
        }
    }
}
