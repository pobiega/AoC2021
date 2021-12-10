using AdventOfCode;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTests;

public class Day10Tests
{
    [Fact]
    public void Part1_Samples()
    {
        var data = new[]
        {
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]",
        };

        var actual = Day_10.Part1(data);

        actual.Should().Be(26397);
    }

    [Fact]
    public void Part2_Samples()
    {
        var data = new[]
        {
            "[({(<(())[]>[[{[]{<()<>>",
            "[(()[<>])]({[<{<<[]>>(",
            "{([(<{}[<>[]}>{[]{[(<()>",
            "(((({<>}<{<{<>}{[]{[]{}",
            "[[<[([]))<([[{}[[()]]]",
            "[{[{({}]{}}([{[{{{}}([]",
            "{<[[]]>}<{[{[{[]{()[[[]",
            "[<(<(<(<{}))><([]([]()",
            "<{([([[(<>()){}]>(<<{{",
            "<{([{{}}[<[[[<>{}]]]>[]]",
        };

        var actual = Day_10.Part2(data);

        actual.Should().Be(288957);
    }

    [Theory]
    [InlineData("<)", true)]
    [InlineData("((())>", true)]
    [InlineData("()", false)]
    [InlineData("[<(]>)", true)]
    [InlineData("[>", true)]
    [InlineData("<}", true)]
    public void CanFindCorruptedLine(string line, bool expected)
    {
        var corrupted = Day_10.TryFindCorruptedCharacter(line, out var c);

        corrupted.Should().Be(expected);
    }

    [Theory]
    [InlineData("(((", ")))")]
    [InlineData("(<[{", "}]>)")]
    public void CanAutocomplete(string line, string expected)
    {
        var actual = Day_10.Autocomplete(line);
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("}}]])})]", 288957)]
    [InlineData(")}>]})", 5566)]
    public void AutocompleteScores(string ending, int score)
    {
        var actual = Day_10.CalculateAutocompleteScore(ending);
        actual.Should().Be(score);
    }
}
