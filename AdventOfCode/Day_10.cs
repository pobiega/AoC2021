namespace AdventOfCode;

public class Day_10 : BaseDay
{
    private readonly string[] _input;

    public Day_10()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_input).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_input).ToString());
    }

    private static readonly Dictionary<char, int> _corruptedScore = new()
    {
        [')'] = 3,
        [']'] = 57,
        ['}'] = 1197,
        ['>'] = 25137,
    };

    private static readonly Dictionary<char, int> _autocompleteScore = new()
    {
        [')'] = 1,
        [']'] = 2,
        ['}'] = 3,
        ['>'] = 4,
    };

    private static readonly Dictionary<char, char> _openerToCloser = new()
    {
        ['('] = ')',
        ['['] = ']',
        ['{'] = '}',
        ['<'] = '>',
    };

    public static int Part1(IEnumerable<string> data)
    {
        var total = 0;
        foreach (var line in data)
        {
            if (TryFindCorruptedCharacter(line, out var c))
            {
                var score = _corruptedScore[c];
                total += score;
            }
        }

        return total;
    }

    public static long Part2(IEnumerable<string> data)
    {
        var notCorrupted = data.Where(l => !TryFindCorruptedCharacter(l, out var _)).ToArray();

        var scores = new List<long>(notCorrupted.Length);

        foreach (var line in notCorrupted)
        {
            var ending = Autocomplete(line);

            scores.Add(CalculateAutocompleteScore(ending));
        }

        scores.Sort();

        return scores[scores.Count / 2];
    }

    public static long CalculateAutocompleteScore(string ending)
    {
        var score = 0L;

        foreach (var item in ending)
        {
            score *= 5;
            score += _autocompleteScore[item];
        }

        return score;
    }

    public static string Autocomplete(string line)
    {
        var stack = new Stack<char>();

        foreach (var item in line)
        {
            if (_openerToCloser.ContainsKey(item))
            {
                stack.Push(item);
            }
            else
            {
                if (!stack.TryPop(out var opener))
                {
                    throw new Exception("Stack unwinded");
                }
            }
        }

        var output = "";

        while (stack.TryPop(out var opener))
        {
            output += _openerToCloser[opener];
        }

        return output;
    }

    public static bool TryFindCorruptedCharacter(string line, out char c)
    {
        var stack = new Stack<char>();

        foreach (var item in line)
        {
            if (_openerToCloser.ContainsKey(item))
            {
                stack.Push(item);
            }
            else
            {
                if (!stack.TryPop(out var opener))
                {
                    throw new Exception("Stack unwinded");
                }
                var expected = _openerToCloser[opener];

                if (item != expected)
                {
                    c = item;
                    return true;
                }
            }
        }

        c = char.MinValue;
        return false;
    }
}
