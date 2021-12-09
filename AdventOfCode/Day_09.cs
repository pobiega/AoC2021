namespace AdventOfCode;

public class Day_09 : BaseDay
{
    private readonly int[][] _input;

    public Day_09()
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

    public static (int x, int y, int value)[] GetLowpoints(int[][] data)
    {
        var lowpoints = new List<(int x, int y, int value)>();

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
                lowpoints.Add(new(x, y, current));
            }
        }

        return lowpoints.ToArray();
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();
    }
}
