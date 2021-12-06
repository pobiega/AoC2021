using AdventOfCode;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTests;

public class Day06Tests
{
    [Fact]
    public void Part1_Sample()
    {
        var input = new[] { 3, 4, 3, 1, 2 };

        var actual = Day_06.Part1(input);

        actual.Should().Be(5934);
    }

    [Fact]
    public void Part2_Sample()
    {
        var input = new int[] { 3, 4, 3, 1, 2 };

        var actual = Day_06.Part2(input);

        actual.Should().Be(26984457539);
    }
}
