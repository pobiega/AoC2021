using AdventOfCode;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTests;

public class Day14Tests
{
    [Fact]
    public void CanParseInput()
    {
        var input = new[]
        {
            "NNCB",
            "",
            "CH -> B",
            "HH -> N",
            "CB -> H",
            "NH -> C",
            "HB -> C",
            "HC -> B",
            "HN -> C",
            "NN -> C",
            "BH -> H",
            "NC -> B",
            "NB -> B",
            "BN -> B",
            "BB -> N",
            "BC -> B",
            "CC -> N",
            "CN -> C",
        };

        var solver = new Day14Solver(input);
    }

    [Fact]
    public void Part1_sample()
    {
        var input = new[]
        {
            "NNCB",
            "",
            "CH -> B",
            "HH -> N",
            "CB -> H",
            "NH -> C",
            "HB -> C",
            "HC -> B",
            "HN -> C",
            "NN -> C",
            "BH -> H",
            "NC -> B",
            "NB -> B",
            "BN -> B",
            "BB -> N",
            "BC -> B",
            "CC -> N",
            "CN -> C",
        };

        var solver = new Day14Solver(input);

        var actual = solver.Solve(10);

        actual.Should().Be(1588L);
    }
}
