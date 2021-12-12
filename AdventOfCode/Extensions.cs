using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

public static class AocExtensions
{
    public static byte ToByte(this char c)
    {
        return c switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            _ => throw new ArgumentException("Non-numeric char found.", nameof(c)),
        };
    }

    public static byte ToInt(this char c)
    {
        return c switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            _ => throw new ArgumentException("Non-numeric char found.", nameof(c)),
        };
    }

    public static T Do<T>(this T value, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(action);

        action(value);
        return value;
    }

    public static R Let<T, R>(this T variable, Func<T, R> function)
    {
        ArgumentNullException.ThrowIfNull(function);

        return function(variable);
    }

    public static IEnumerable<T> Tap<T>(this IEnumerable<T> source, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        return TapIterator(source, action);

        static IEnumerable<T> TapIterator(IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }
    }

    public static IEnumerable<T> Tap<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        return TapIterator(source, action);

        static IEnumerable<T> TapIterator(IEnumerable<T> source, Action<T, int> action)
        {
            var index = 0;
            foreach (var item in source)
            {
                action(item, index++);
                yield return item;
            }
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        var index = 0;
        foreach (var item in source)
        {
            action(item, index++);
        }
    }

    public static IEnumerable<Point> GetAdjacent8(this Point point)
    {
        yield return point with { X = point.X + 1 };
        yield return point with { X = point.X - 1 };
        yield return point with { Y = point.Y + 1 };
        yield return point with { Y = point.Y - 1 };

        yield return point with { Y = point.Y + 1, X = point.X + 1 };
        yield return point with { Y = point.Y - 1, X = point.X + 1 };
        yield return point with { Y = point.Y + 1, X = point.X - 1 };
        yield return point with { Y = point.Y - 1, X = point.X - 1 };
    }
}
