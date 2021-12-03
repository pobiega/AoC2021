using AdventOfCode;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTests;

public class Day03Tests
{
    [Theory]
    [InlineData("00100", "00100")]
    [InlineData("11100", "11100")]
    [InlineData("11111", "11111")]
    public void CanFindMostCommonBits(string input, string expected)
    {
        //var actual = Day_03.GetMostCommonBits(new[] { input });
        //actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("00100", "11011")]
    [InlineData("11100", "00011")]
    [InlineData("11111", "00000")]
    public void CanInvertBinaryString(string input, string expected)
    {
        var actual = Day_03.InvertBinaryString(input);
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("10110", 22)]
    public void CanConvertBinaryStringToInt(string input, int expected)
    {
        var actual = Day_03.ConvertBinaryStringToInt(input);
        actual.Should().Be(expected);
    }

    [Fact]
    public void CanCalculateGamma()
    {
        var input = new[]
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        //var actual = Day_03.GetMostCommonBits(input);
        //actual.Should().BeEquivalentTo("10110");
    }

    [Fact]
    public void Part1_Sample()
    {
        var input = new[]
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        var actual = Day_03.Part1(input);
        actual.Should().Be(198);
    }

    [Fact]
    public void FindOxygen()
    {
        var input = new[]
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        var actual = Day_03.Find(input, c => c < 0);

        actual.Should().BeEquivalentTo("10111");
    }

    [Fact]
    public void FindCO2()
    {
        var input = new[]
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        var actual = Day_03.Find(input, c => c >= 0);

        actual.Should().BeEquivalentTo("01010");
    }

    [Fact]
    public void Part2_Sample()
    {
        var input = new[]
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };

        var actual = Day_03.Part2(input);
        actual.Should().Be(230);
    }
}
