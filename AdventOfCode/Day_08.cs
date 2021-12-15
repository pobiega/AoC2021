namespace AdventOfCode;

public record struct CleverDay8Line(string[] Patterns, string[] OutputValues, string FullPattern);

public class Day08 : BaseDay
{
    private readonly CleverDay8Line[] _input;

    public Day08()
    {
        _input = File.ReadAllLines(InputFilePath).Select(ParseLine).ToArray();
    }

    public static CleverDay8Line ParseLine(string line)
    {
        var split = line.Split('|', StringSplitOptions.TrimEntries);
        var patterns = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var outputValues = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var fullPattern = string.Join(string.Empty, patterns);

        return new(patterns, outputValues, fullPattern);
    }

    private static readonly Dictionary<int, int> FrequencySumToDigit = new()
    {
        [42] = 0,
        [17] = 1,
        [34] = 2,
        [39] = 3,
        [30] = 4,
        [37] = 5,
        [41] = 6,
        [25] = 7,
        [49] = 8,
        [45] = 9,
    };

    private static readonly Dictionary<int, int> EasyDigitMap = new()
    {
        [2] = 1,
        [4] = 4,
        [3] = 7,
        [7] = 8
    };

    public static int Part1(IEnumerable<CleverDay8Line> lines)
    {
        var total = 0;
        foreach (var item in lines)
        {
            foreach (var oval in item.OutputValues)
            {
                if (EasyDigitMap.ContainsKey(oval.Length))
                    total += 1;
            }
        }

        return total;
    }

    public static int Part2(IEnumerable<CleverDay8Line> data)
    {
        var total = 0;

        foreach (var item in data)
        {
            var output = HandleLine(item);
            total += int.Parse(output);
        }

        return total;
    }

    private static string HandleLine(CleverDay8Line item)
    {
        // count the frequencies across the entire pattern
        var frequencies = GetFrequencies(item.FullPattern);

        var output = "";

        foreach (var outval in item.OutputValues)
        {
            var sum = Translate(outval, frequencies);
            var digit = FrequencySumToDigit[sum];
            output += digit;
        }

        return output;
    }

    private static int Translate(string pattern, Dictionary<char, int> frequencies)
    {
        int total = 0;

        foreach (var c in pattern)
        {
            total += frequencies[c];
        }

        return total;
    }

    private static Dictionary<char, int> GetFrequencies(string fullPattern)
    {
        var dict = new Dictionary<char, int>()
        {
            ['a'] = 0,
            ['b'] = 0,
            ['c'] = 0,
            ['d'] = 0,
            ['e'] = 0,
            ['f'] = 0,
            ['g'] = 0,
        };

        foreach (var c in fullPattern)
        {
            dict[c]++;
        }

        return dict;
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_input).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_input).ToString());
    }
}
