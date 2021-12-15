namespace AdventOfCode;

public class Day09 : BaseDay
{
    private readonly int[][] _input;

    public Day09()
    {
        _input = ParseInput(File.ReadAllLines(InputFilePath));
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_input).ToString());
    }

    public static int[][] ParseInput(string[] input)
    {
        return input.Select(s => s
                .Select(c =>
                    int.Parse(new[] { c }))
                .ToArray())
            .ToArray();
    }

    public static int Part1(int[][] data)
    {
        var lowpoints = GetLowpoints(data);

        return lowpoints.Sum(x => x.value + 1);
    }

    public static (Point point, int value)[] GetLowpoints(int[][] data)
    {
        var lowpoints = new List<(Point point, int value)>();

        for (int y = 0; y < data.Length; y++)
        {
            for (int x = 0; x < data[y].Length; x++)
            {
                var current = data[y][x];

                if (y != 0)
                {
                    if (current >= data[y - 1][x])
                    {
                        continue;
                    }
                }

                if (y <= data.Length - 2)
                {
                    if (current >= data[y + 1][x])
                    {
                        continue;
                    }
                }
                if (x != 0)
                {
                    if (current >= data[y][x - 1])
                    {
                        continue;
                    }
                }
                if (x <= data[y].Length - 2)
                {
                    if (current >= data[y][x + 1])
                    {
                        continue;
                    }
                }
                lowpoints.Add(new(new Point(x, y), current));
            }
        }

        return lowpoints.ToArray();
    }

    public static int Part2(int[][] data)
    {
        var lowpoints = GetLowpoints(data);

        var basins = new List<Point[]>();

        foreach (var (point, value) in lowpoints)
        {
            // each lowpoint should be the source of a basin.

            var basin = FloodFill(point, data);
            basins.Add(basin);
        }

        var top3 = basins.OrderByDescending(p => p.Length).Take(3);

        var total = 1;

        foreach (var item in top3)
        {
            total *= item.Length;
        }

        return total;
    }

    public static bool CanFlood(int x, int y, int height, int width, int[][] data, HashSet<Point> stack)
    {
        if (stack.Contains(new Point(x, y)))
        {
            return false;
        }

        if (x < 0 || x >= width)
            return false;

        if (y < 0 || y >= height)
            return false;

        var value = data[y][x];

        if (value == 9)
            return false;

        return true;
    }

    public static Point[] FloodFill(Point startingPoint, int[][] data)
    {
        var stack = new HashSet<Point>();
        var queue = new Queue<Point>();

        queue.Enqueue(startingPoint);
        stack.Add(startingPoint);

        while (queue.TryDequeue(out var item))
        {
            var x = item.X;
            var y = item.Y;

            if (CanFlood(x, y + 1, data.Length, data[y].Length, data, stack))
            {
                var p = new Point(x, y + 1);
                stack.Add(p);
                queue.Enqueue(p);
            }

            if (CanFlood(x, y - 1, data.Length, data[y].Length, data, stack))
            {
                var p = new Point(x, y - 1);
                stack.Add(p);
                queue.Enqueue(p);
            }

            if (CanFlood(x + 1, y, data.Length, data[y].Length, data, stack))
            {
                var p = new Point(x + 1, y);
                stack.Add(p);
                queue.Enqueue(p);
            }

            if (CanFlood(x - 1, y, data.Length, data[y].Length, data, stack))
            {
                var p = new Point(x - 1, y);
                stack.Add(p);
                queue.Enqueue(p);
            }
        }

        return stack.ToArray();
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_input).ToString());
    }
}
