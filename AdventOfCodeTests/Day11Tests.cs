using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AdventOfCode;

namespace AdventOfCodeTests;

public class Day11Tests
{
    [Fact]
    public void Part1_Sample()
    {
        var input = new[]
        {
            "5483143223",
            "2745854711",
            "5264556173",
            "6141336146",
            "6357385478",
            "4167524645",
            "2176841721",
            "6882881134",
            "4846848554",
            "5283751526",
        };

        var data = Day_11.ParseInput(input);

        var actual = Day_11.Part1(data);

        actual.Should().Be(1656);
    }

    [Fact]
    public void Part2_Sample()
    {
        var input = new[]
        {
            "5483143223",
            "2745854711",
            "5264556173",
            "6141336146",
            "6357385478",
            "4167524645",
            "2176841721",
            "6882881134",
            "4846848554",
            "5283751526",
        };

        var data = Day_11.ParseInput(input);

        var actual = Day_11.Part2(data);

        actual.Should().Be(195);
    }

    [Fact]
    public void ParseInputTest()
    {
        var input = new[]
        {
            "11111",
            "19991",
            "19191",
            "19991",
            "11111",
        };

        var actual = Day_11.ParseInput(input);

        actual[new Point(0, 0)].Should().Be(1);
    }

    [Fact]
    public void CanPerformStep()
    {
        var input = new[]
        {
            "11111",
            "19991",
            "19191",
            "19991",
            "11111",
        };

        var actual = Day_11.ParseInput(input);

        var flashes = Day_11.PerformStepInplace(actual);

        actual[new Point(0, 0)].Should().Be(3);
        actual[new Point(1, 1)].Should().Be(0);

        flashes.Should().Be(9);

    }
}
