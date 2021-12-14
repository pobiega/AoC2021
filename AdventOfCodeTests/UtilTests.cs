using AdventOfCode;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace AdventOfCodeTests;

public class UtilTests
{
    [Fact]
    public void String_SlidingWindow()
    {
        var input = "ABCDE";

        var actual = input.SlidingWindow(2).ToArray();

        actual[0].Should().BeEquivalentTo("AB");
        actual[1].Should().BeEquivalentTo("BC");
        actual[2].Should().BeEquivalentTo("CD");
        actual[3].Should().BeEquivalentTo("DE");
    }

    [Fact]
    public void Enumerable_SlidingWindow()
    {
        var input = new[] { 1, 2, 3, 4, 5 };

        var actual = input.SlidingWindow(2).ToArray();

        actual[0].Should().BeEquivalentTo(new[] { 1, 2 });
        actual[1].Should().BeEquivalentTo(new[] { 2, 3 });
        actual[2].Should().BeEquivalentTo(new[] { 3, 4 });
        actual[3].Should().BeEquivalentTo(new[] { 4, 5 });
    }
}
