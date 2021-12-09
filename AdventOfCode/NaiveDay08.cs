using System.Text;

namespace AdventOfCode;

public record struct Day8Line(string[] Patterns, string[] OutputValues);

public enum SegmentIdentifer
{
    aaaa,
    bb,
    cc,
    dddd,
    ee,
    ff,
    gggg
}

public class NaiveDay08 : BaseDay
{
    private readonly IEnumerable<Day8Line> _input;

    public NaiveDay08()
    {
        _input = File.ReadAllLines(InputFilePath).Select(ParseLine);
    }

    public static Day8Line ParseLine(string line)
    {
        var split = line.Split('|', StringSplitOptions.TrimEntries);
        var patterns = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var outputValues = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return new(patterns, outputValues);
    }

    private static readonly Dictionary<int, int> _easyDigitMap = new()
    {
        [2] = 1,
        [4] = 4,
        [3] = 7,
        [7] = 8
    };

    private static readonly Dictionary<string, int> _segmentsToNumber = new()
    {
        ["abcefg"] = 0,
        ["cf"] = 1,
        ["acdeg"] = 2,
        ["acdfg"] = 3,
        ["bcdf"] = 4,
        ["abdfg"] = 5,
        ["abdefg"] = 6,
        ["acf"] = 7,
        ["abcdefg"] = 8,
        ["abcdfg"] = 9,
    };

    public static int Part1(IEnumerable<Day8Line> lines)
    {
        var total = 0;
        foreach (var item in lines)
        {
            foreach (var oval in item.OutputValues)
            {
                if (_easyDigitMap.ContainsKey(oval.Length))
                    total += 1;
            }
        }

        return total;
    }

    public static int Part2(IEnumerable<Day8Line> lines)
    {
        var total = 0;
        foreach (var line in lines)
        {
            var segmentMap = CalculateSegmentMap(line);
            var translatedOutput = TranslateOutput(line.OutputValues, segmentMap);

            total += translatedOutput;
        }

        return total;
    }

    public static int TranslateOutput(string[] outputValues, SegmentMapping segmentMap)
    {
        var sb = new StringBuilder();
        foreach (var item in outputValues)
        {
            sb.Append(ConvertToDigit(item, segmentMap));
        }

        return int.Parse(sb.ToString());
    }

    public static int ConvertToDigit(string item, SegmentMapping segmentMap)
    {
        var segments = GetSegments(item, segmentMap).ToArray();
        var sorted = SegmentIdentifiersToString(segments);

        return _segmentsToNumber[sorted];
    }

    public static IEnumerable<SegmentIdentifer> GetSegments(string item, SegmentMapping segmentMap)
    {
        foreach (var c in item)
        {
            yield return segmentMap.GetMappingFor(c);
        }
    }

    public static string SegmentIdentifiersToString(IEnumerable<SegmentIdentifer> segmentIdentifers)
    {
        var chars = segmentIdentifers.Select(SegmentIdentifierToChar).ToArray();
        Array.Sort(chars);
        return new string(chars);
    }

    private static char SegmentIdentifierToChar(SegmentIdentifer identifier)
    {
        return identifier switch
        {
            SegmentIdentifer.aaaa => 'a',
            SegmentIdentifer.bb => 'b',
            SegmentIdentifer.cc => 'c',
            SegmentIdentifer.dddd => 'd',
            SegmentIdentifer.ee => 'e',
            SegmentIdentifer.ff => 'f',
            SegmentIdentifer.gggg => 'g',
            _ => throw new Exception("Invalid identifier")
        };
    }

    public static SegmentMapping CalculateSegmentMap(Day8Line line)
    {
        var segmentMap = new SegmentMapping();
        var usageMap = CalculateUsageMap(line);

        foreach (var usage in usageMap)
        {
            if (usage.Value == 9)
            {
                // ff is unique at 9
                segmentMap.MustBe(SegmentIdentifer.ff, usage.Key);
            }
            else if (usage.Value == 6)
            {
                // bb is unique at 6
                segmentMap.MustBe(SegmentIdentifer.bb, usage.Key);
            }
            else if (usage.Value == 4)
            {
                // ee is unique at 6
                segmentMap.MustBe(SegmentIdentifer.ee, usage.Key);
            }
        }

        string seven = null;
        string one = null;
        string eight = null;
        string four = null;

        foreach (var item in line.Patterns)
        {
            if (!_easyDigitMap.TryGetValue(item.Length, out var easyDigit))
            {
                continue;
            }

            if (easyDigit == 7)
            {
                seven = item;
            }
            else if (easyDigit == 1)
            {
                one = item;
            }
            else if (easyDigit == 8)
            {
                eight = item;
            }
            else if (easyDigit == 4)
            {
                four = item;
            }
        }

        if (seven == null || one == null || eight == null || four == null)
        {
            throw new Exception("One, Four, Seven or Eight was not found, this should be impossible.");
        }

        var aaaaSegment = seven.First(c => !one.Contains(c));
        segmentMap.MustBe(SegmentIdentifer.aaaa, aaaaSegment);

        // we know aaaa, bb, ee and ff
        // we ONE, FOUR, SEVEN and EIGHT

        // dddd
        // 4 - 1 - bb == dddd
        var bb = segmentMap.GetReverseMappingFor(SegmentIdentifer.bb);
        var dddd = four.First(c => c != bb && !one.Contains(c));
        segmentMap.MustBe(SegmentIdentifer.dddd, dddd);

        // gggg
        // 8 - 4 - 7 - ee == gggg
        var ee = segmentMap.GetReverseMappingFor(SegmentIdentifer.ee);
        var gggg = eight.First(c => c != ee && !four.Contains(c) && !seven.Contains(c));
        segmentMap.MustBe(SegmentIdentifer.gggg, gggg);

        // cc
        // its the last one

        var knownMappings = segmentMap.GetAllKnownMappings();
        var cc = eight.First(c => !knownMappings.Contains(c));
        segmentMap.MustBe(SegmentIdentifer.cc, cc);

        return segmentMap;
    }

    public static Dictionary<char, int> CalculateUsageMap(Day8Line line)
    {
        Dictionary<char, int> usageMap = new()
        {
            ['a'] = 0,
            ['b'] = 0,
            ['c'] = 0,
            ['d'] = 0,
            ['e'] = 0,
            ['f'] = 0,
            ['g'] = 0,
        };

        foreach (var item in line.Patterns)
        {
            foreach (var c in item)
            {
                usageMap[c]++;
            }
        }

        return usageMap;
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

public class SegmentMapping
{
    private readonly Dictionary<char, SegmentIdentifer> _segmentMap = new();
    private readonly Dictionary<SegmentIdentifer, char> _reverseSegmentMap = new();

    public bool IsComplete => _segmentMap.Count == 7;

    public SegmentIdentifer GetMappingFor(char segmentKey)
    {
        return _segmentMap[segmentKey];
    }

    public char GetReverseMappingFor(SegmentIdentifer segmentIdentifer)
    {
        return _reverseSegmentMap[segmentIdentifer];
    }

    public void MustBe(SegmentIdentifer segmentIdentifer, char key)
    {
        _segmentMap[key] = segmentIdentifer;
        _reverseSegmentMap[segmentIdentifer] = key;
    }

    public char[] GetAllKnownMappings()
    {
        return _segmentMap.Keys.ToArray();
    }
}
