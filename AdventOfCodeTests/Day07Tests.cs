using AdventOfCode;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTests;

public class Day07Tests
{
    [Fact]
    public void Part1_Samples()
    {
        var input = new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

        var actual = Day_07.Part1(input);

        actual.Should().Be(37);
    }

    [Fact]
    public void Part2_Samples()
    {
        var input = new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

        var actual = Day_07.Part2(input);

        actual.Should().Be(168);
    }

    [Fact]
    public void FindBestHorizontalPositionPart1()
    {
        var input = new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

        var actual = Day_07.FindBestHorizontalPosition(input, c => c);

        actual.Should().Be((2, 37));
    }

    [Fact]
    public void FindBestHorizontalPositionPart2()
    {
        var input = new[] { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

        var actual = Day_07.FindBestHorizontalPosition(input, c => Day_07.SumZeroToN(c));

        actual.Should().Be((5, 168));
    }
}
