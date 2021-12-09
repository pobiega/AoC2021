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

        var data = Day_09.ParseInput(input);

        data[0][0].Should().Be(2);
        data[1][0].Should().Be(3);
        data[4][9].Should().Be(8);

        var p1 = Day_09.Part1(data);

        p1.Should().Be(15);
    }
}
