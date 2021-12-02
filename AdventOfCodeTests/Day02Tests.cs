using AdventOfCode;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace AdventOfCodeTests;

public class Day02Tests
{
    [Fact]
    public void Part1_Sample()
    {
        var data = new[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };

        var parsed = data
            .Select(x =>
            {
                var data = x.Split();
                return (data[0], int.Parse(data[1]));
            })
            .ToList();

        var actual = Day_02.Part1(parsed);

        actual.Should().Be(150);
    }

    [Fact]
    public void Part2_Sample()
    {
        var data = new[] { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };

        var parsed = data
           .Select(x =>
           {
               var data = x.Split();
               return (data[0], int.Parse(data[1]));
           })
           .ToList();

        var actual = Day_02.Part2(parsed);

        actual.Should().Be(900);
    }
}
