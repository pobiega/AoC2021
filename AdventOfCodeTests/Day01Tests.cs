using AdventOfCode;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTests;
public class Day01Tests
{
    [Fact]
    public void Part1_Sample()
    {
        var input = new[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

        var actual = Day_01.Part1(input);

        actual.Should().Be(7);
    }

    [Fact]
    public void Part2_Sample()
    {
        var input = new[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

        var actual = Day_01.Part2(input);

        actual.Should().Be(5);
    }
}
