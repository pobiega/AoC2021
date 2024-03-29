﻿namespace AdventOfCode;

public class Day10 : BaseDay
{
    private readonly string[] _input;

    public Day10()
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

    private static readonly Dictionary<char, int> CorruptedScore = new()
    {
        [')'] = 3,
        [']'] = 57,
        ['}'] = 1197,
        ['>'] = 25137,
    };

    private static readonly Dictionary<char, int> AutocompleteScore = new()
    {
        [')'] = 1,
        [']'] = 2,
        ['}'] = 3,
        ['>'] = 4,
    };

    private static readonly Dictionary<char, char> OpenerToCloser = new()
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
                var score = CorruptedScore[c];
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
            score += AutocompleteScore[item];
        }

        return score;
    }

    public static string Autocomplete(string line)
    {
        var stack = new Stack<char>();

        foreach (var item in line)
        {
            if (OpenerToCloser.ContainsKey(item))
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
            output += OpenerToCloser[opener];
        }

        return output;
    }

    public static bool TryFindCorruptedCharacter(string line, out char c)
    {
        var stack = new Stack<char>();

        foreach (var item in line)
        {
            if (OpenerToCloser.ContainsKey(item))
            {
                stack.Push(item);
            }
            else
            {
                var opener = stack.Pop();
                var expected = OpenerToCloser[opener];

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
