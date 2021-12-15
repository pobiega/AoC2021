using AdventOfCode;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTests;

public class Day12Tests
{
    [Fact]
    public void Part1_Sample()
    {
        var input = new[]
        {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end",
        };

        var data = Day12.MakeGraph(input);

        var actual = Day12.Part1(data);

        actual.Should().Be(10);
    }

    [Fact]
    public void CanMakeGraph()
    {
        var input = new[]
        {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end",
        };

        var data = Day12.MakeGraph(input);

        data.Should().NotBeNull();
        data.Should().NotBeEmpty();

        data["start"].Should().NotBeEmpty();
        data["end"].Should().NotBeEmpty();

    }
}
