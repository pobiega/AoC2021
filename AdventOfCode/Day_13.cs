using Spectre.Console;

namespace AdventOfCode;

public enum Xy
{
    X,
    Y
}

public record struct FoldLine(Xy Direction, int Position);

public class Day13 : BaseDay
{
    private readonly Point[] _points;
    private readonly FoldLine[] _folds;

    public Day13()
    {
        var (points, folds) = ParseInput(File.ReadAllLines(InputFilePath));

        _points = points;
        _folds = folds;
    }

    public static (Point[] points, FoldLine[] folds) ParseInput(string[] lines)
    {
        var points = new List<Point>();
        var folds = new List<FoldLine>();

        foreach (var item in lines)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                continue;
            }

            if (item.StartsWith("fold"))
            {
                var remainder = item.Replace("fold along ", "");
                var values = remainder.Split('=');

                folds.Add(new FoldLine(values[0] == "x" ? Xy.X : Xy.Y, int.Parse(values[1])));
                continue;
            }

            var split = item.Split(',');
            var x = int.Parse(split[0]);
            var y = int.Parse(split[1]);

            points.Add(new Point(x, y));

        }

        return (points.ToArray(), folds.ToArray());
    }

    public static Point[] Fold(Point[] points, Xy direction, int position)
    {
        var result = new HashSet<Point>();

        if (direction == Xy.X)
        {
            var maxX = points.Max(p => p.X);

            foreach (var point in points)
            {
                if (point.X < position)
                {
                    result.Add(point);
                    continue;
                }
                else if (point.X > position)
                {
                    var newx = maxX - point.X;
                    result.Add(point with { X = newx });
                }
            }
        }
        else if (direction == Xy.Y)
        {
            var maxY = points.Max(p => p.Y);

            foreach (var point in points)
            {
                if (point.Y < position)
                {
                    result.Add(point);
                    continue;
                }
                else if (point.Y > position)
                {
                    var newY = maxY - point.Y;
                    result.Add(point with { Y = newY });
                }
            }
        }

        return result.ToArray();
    }

    public static int Part1(Point[] points, FoldLine fold)
    {
        var actual = Fold(points, fold.Direction, fold.Position);

        return actual.Length;
    }

    public static void Part2(Point[] points, FoldLine[] folds)
    {
        var actual = points;

        foreach (var fold in folds)
        {
            actual = Fold(actual, fold.Direction, fold.Position);
        }

        var count = actual.Length;

        var width = actual.Max(p => p.X) + 1;
        var height = actual.Max(p => p.Y) + 1;

        var canvas = new Canvas(width, height);

        foreach (var item in actual)
        {
            canvas.SetPixel(item.X, item.Y, Color.Red);
        }

        AnsiConsole.Write(canvas);
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_points, _folds[0]).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        Part2(_points, _folds);

        return new("Printed to console.");
    }
}
