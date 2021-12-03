using System.Text;

namespace AdventOfCode;

public class Day_03 : BaseDay
{
    private readonly string[] _input;

    public Day_03()
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

    public static int[] BitDiff(string[] values)
    {
        var count = new int[values[0].Length];

        foreach (var item in values)
        {
            for (int i = 0; i < item.Length; i++)
            {
                count[i] += item[i] == '0' ? -1 : 1;
            }
        }

        return count;
    }

    public static int ConvertBinaryStringToInt(string binaryString)
    {
        return Convert.ToInt32(binaryString, 2);
    }

    public static string InvertBinaryString(string binaryString)
    {
        var sb = new StringBuilder();

        foreach (var item in binaryString)
        {
            if (item == '0')
                sb.Append('1');
            else if (item == '1')
                sb.Append('0');
            else
            {
                throw new Exception($"Invalid character in binary string: {item}");
            }
        }

        return sb.ToString();
    }

    public static int Part1(string[] values)
    {
        var bitDiff = BitDiff(values);

        var gammaBuilder = new StringBuilder();
        foreach (var item in bitDiff)
        {
            gammaBuilder.Append(item > 0 ? '1' : '0');
        }

        var gammaBinary = gammaBuilder.ToString();
        var epsilonBinary = InvertBinaryString(gammaBinary);

        var gamma = ConvertBinaryStringToInt(gammaBinary);
        var epsilon = ConvertBinaryStringToInt(epsilonBinary);

        return gamma * epsilon;
    }

    public static int Part2(IEnumerable<string> input)
    {
        var oxygenBinary = Find(input, c => c < 0);
        var co2Binary = Find(input, c => c >= 0);

        var oxygen = ConvertBinaryStringToInt(oxygenBinary);
        var co2 = ConvertBinaryStringToInt(co2Binary);

        return oxygen * co2;
    }

    public static string Find(IEnumerable<string> input, Func<int, bool> predicate)
    {
        var haystack = input.ToArray();
        var needle = "";
        var index = 0;

        while (haystack.Length > 1)
        {
            var commonBits = BitDiff(haystack);

            needle += predicate(commonBits[index]) ? '0' : '1';
            haystack = haystack.Where(s => s.StartsWith(needle)).ToArray();
            index++;
        }

        return haystack[0];
    }
}
