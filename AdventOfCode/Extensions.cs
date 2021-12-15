namespace AdventOfCode;

public static class AocExtensions
{
    public static void SetIfNotExists<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> init)
    {
        if (!dict.ContainsKey(key))
        {
            dict[key] = init();
        }
    }

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

    public static TR Let<T, TR>(this T variable, Func<T, TR> function)
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

    public static IEnumerable<T[]> SlidingWindow<T>(this IReadOnlyList<T> source, int windowSize)
    {
        for (int i = 0; i < source.Count - windowSize + 1; i++)
        {
            var temp = new T[windowSize];
            for (int j = 0; j < windowSize; j++)
            {
                temp[j] = source[i + j];
            }
            yield return temp;
        }
    }

    public static IEnumerable<string> SlidingWindow(this string source, int windowSize)
    {
        for (int i = 0; i < source.Length - windowSize + 1; i++)
        {
            yield return source.Substring(i, windowSize);
        }
    }

    public static Dictionary<char, int> Frequency(this string input)
    {
        var result = new Dictionary<char, int>();

        foreach (var ch in input)
        {
            result[ch] = result.GetValueOrDefault(ch, 0) + 1;
        }

        return result;
    }
}
