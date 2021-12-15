using AdventOfCode;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AdventOfCodeTests;

public class Day09Tests
{
    [Fact]
    public void Part1_Sample()
    {
        var input = new[]
        {
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678",
        };

        var data = Day09.ParseInput(input);

        data[0][0].Should().Be(2);
        data[1][0].Should().Be(3);
        data[4][9].Should().Be(8);

        var actual = Day09.Part1(data);

        actual.Should().Be(15);
    }

    [Fact]
    public void Part2_Sample()
    {
        var input = new[]
        {
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678",
        };

        var data = Day09.ParseInput(input);

        var actual = Day09.Part2(data);

        actual.Should().Be(1134);
    }

    [Fact]
    public void Floodfill_works()
    {
        var input = new[]
        {
            "2199943210",
            "3987894921",
            "9856789892",
            "8767896789",
            "9899965678",
        };

        var data = Day09.ParseInput(input);

        var actual = Day09.FloodFill(new Point(9,0), data);

        actual.Should().HaveCount(9);
    }
}

