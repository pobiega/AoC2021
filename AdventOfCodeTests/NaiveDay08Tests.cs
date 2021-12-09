using AdventOfCode;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace AdventOfCodeTests;

public class NaiveDay08Tests
{
    [Fact]
    public void ParseLine_Works()
    {
        var line = "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf";

        var actual = NaiveDay08.ParseLine(line);

        var expectedPattern = new[]
        {
            "acedgfb",
            "cdfbe",
            "gcdfa",
            "fbcad",
            "dab",
            "cefabd",
            "cdfgeb",
            "eafb",
            "cagedb",
            "ab",
        };

        var expectedOutput = new[]
        {
            "cdfeb",
            "fcadb",
            "cdfeb",
            "cdbaf"
        };

        actual.Patterns.Should().BeEquivalentTo(expectedPattern);
        actual.OutputValues.Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void Part1_Sample()
    {
        var inputStrings = new[]
        {
            "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
            "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
            "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
            "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
            "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
            "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
            "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
            "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
            "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
            "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce",
        };

        var inputs = inputStrings.Select(NaiveDay08.ParseLine).ToArray();

        var actual = NaiveDay08.Part1(inputs);

        actual.Should().Be(26);
    }

    [Fact]
    public void Part2_Sample()
    {
        var inputStrings = new[]
        {
            "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
            "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
            "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
            "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
            "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
            "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
            "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
            "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
            "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
            "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce",
        };

        var inputs = inputStrings.Select(NaiveDay08.ParseLine).ToArray();

        var actual = NaiveDay08.Part2(inputs);

        actual.Should().Be(61229);
    }

    [Fact]
    public void CanDetermineFullSegmentMap()
    {
        var line = NaiveDay08.ParseLine("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");

        var segmentMap = NaiveDay08.CalculateSegmentMap(line);

        segmentMap.GetMappingFor('d').Should().Be(SegmentIdentifer.aaaa);
        segmentMap.GetMappingFor('e').Should().Be(SegmentIdentifer.bb);
        segmentMap.GetMappingFor('a').Should().Be(SegmentIdentifer.cc);
        segmentMap.GetMappingFor('f').Should().Be(SegmentIdentifer.dddd);
        segmentMap.GetMappingFor('g').Should().Be(SegmentIdentifer.ee);
        segmentMap.GetMappingFor('b').Should().Be(SegmentIdentifer.ff);
        segmentMap.GetMappingFor('c').Should().Be(SegmentIdentifer.gggg);
    }

    [Fact]
    public void CanDeterminePartialSegmentMap()
    {
        var line = NaiveDay08.ParseLine("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");

        var segmentMap = NaiveDay08.CalculateSegmentMap(line);

        segmentMap.GetMappingFor('d').Should().Be(SegmentIdentifer.aaaa);
        segmentMap.GetMappingFor('e').Should().Be(SegmentIdentifer.bb);
        segmentMap.GetMappingFor('g').Should().Be(SegmentIdentifer.ee);
        segmentMap.GetMappingFor('b').Should().Be(SegmentIdentifer.ff);
    }

    [Fact]
    public void CanDetermineUsageMap()
    {
        var line = NaiveDay08.ParseLine("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");

        var actual = NaiveDay08.CalculateUsageMap(line);

        actual['a'].Should().Be(8);
        actual['b'].Should().Be(9);
    }

    [Fact]
    public void CanTranslateOutput()
    {
        var line = NaiveDay08.ParseLine("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");

        var segmentMap = NaiveDay08.CalculateSegmentMap(line);

        var actual = NaiveDay08.TranslateOutput(line.OutputValues, segmentMap);

        actual.Should().Be(5353);
    }

    [Fact]
    public void CanConvertToDigit()
    {
        var line = NaiveDay08.ParseLine("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");

        var segmentMap = NaiveDay08.CalculateSegmentMap(line);

        var actual = NaiveDay08.ConvertToDigit("cdfeb", segmentMap);

        actual.Should().Be(5);
    }
}
