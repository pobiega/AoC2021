using AdventOfCode;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace AdventOfCodeTests;

public class Day05Tests
{
    [Fact]
    public void CanExtrapolateHorizontalLine()
    {
        var line = new Line
        {
            Start = new Point(0,5),
            End = new Point(5,5)
        };

        var expected = new[]
        {
            new Point(0,5),
            new Point(1,5),
            new Point(2,5),
            new Point(3,5),
            new Point(4,5),
            new Point(5,5),
        };

        var actual = Day_05.ExtrapolateLine(line).ToArray();

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void CanExtrapolateVerticalLine()
    {
        var line = new Line
        {
            Start = new Point(5,0),
            End = new Point(5,5)
        };

        var expected = new[]
        {
            new Point(5,0),
            new Point(5,1),
            new Point(5,2),
            new Point(5,3),
            new Point(5,4),
            new Point(5,5),
        };

        var actual = Day_05.ExtrapolateLine(line).ToArray();

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void CanExtrapolateDiagonalLine1()
    {
        var line = new Line
        {
            Start = new Point(9,7),
            End = new Point(7,9)
        };

        var expected = new[]
        {
            new Point(9,7),
            new Point(8,8),
            new Point(7,9),
        };

        var actual = Day_05.ExtrapolateLine(line).ToArray();

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void CanGetOnlyStraightLines()
    {
        var input = new[]
        {
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2",
        };

        var lines = Day_05.ParseLines(input).ToArray();

        var actual = Day_05.OnlyStraightLines(lines).ToArray();

        var diagonal = new Line
        {
            Start = new Point(8, 0),
            End = new Point(0, 8)
        };

        actual.Should().NotContain(diagonal);
    }

    [Fact]
    public void Part1_Sample()
    {
        var input = new[]
        {
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2",
        };

        var lines = Day_05.ParseLines(input).ToArray();

        var actual = Day_05.Part1(lines);

        actual.Should().Be(5);
    }

    [Fact]
    public void Part2_Sample()
    {
        var input = new[]
        {
            "0,9 -> 5,9",
            "8,0 -> 0,8",
            "9,4 -> 3,4",
            "2,2 -> 2,1",
            "7,0 -> 7,4",
            "6,4 -> 2,0",
            "0,9 -> 2,9",
            "3,4 -> 1,4",
            "0,0 -> 8,8",
            "5,5 -> 8,2",
        };

        var lines = Day_05.ParseLines(input).ToArray();

        var actual = Day_05.Part2(lines);

        actual.Should().Be(12);
    }
}
