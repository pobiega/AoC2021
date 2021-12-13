using AdventOfCode;
using Xunit;
using FluentAssertions;

namespace AdventOfCodeTests;

public class Day13Tests
{
    [Fact]
    public void CanParse()
    {
        var input = new[]
        {
            "0,0",
            "0,2",
            "",
            "fold along x=1"
        };

        var (points, folds) = Day_13.ParseInput(input);

        points.Should().HaveCount(2);
        folds.Should().HaveCount(1);

        points[0].Should().BeEquivalentTo(new Point(0, 0));
        folds[0].Should().BeEquivalentTo(new FoldLine(XY.X, 1));
    }

    [Fact]
    public void CanFoldX()
    {
        var input = new[]
        {
            "4,0",
            "3,0",
            "4,1",
            "",
            "fold along x=2"
        };

        var (points, folds) = Day_13.ParseInput(input);

        var actual = Day_13.Fold(points, XY.X, 2);

        actual.Should().HaveCount(3);
        actual.Should().ContainEquivalentOf(new Point(0, 0));
        actual.Should().ContainEquivalentOf(new Point(1, 0));
        actual.Should().ContainEquivalentOf(new Point(0, 1));
    }

    [Fact]
    public void CanFoldY()
    {
        var input = new[]
        {
            "0,4",
            "0,3",
            "1,4",
            "",
            "fold along y=2"
        };

        var (points, folds) = Day_13.ParseInput(input);

        var actual = Day_13.Fold(points, XY.Y, 2);

        actual.Should().HaveCount(3);
        actual.Should().ContainEquivalentOf(new Point(0, 0));
        actual.Should().ContainEquivalentOf(new Point(1, 0));
        actual.Should().ContainEquivalentOf(new Point(0, 1));
    }

    [Fact]
    public static void Part1_Sample()
    {
        var input = new[]
        {
            "6,10",
            "0,14",
            "9,10",
            "0,3",
            "10,4",
            "4,11",
            "6,0",
            "6,12",
            "4,1",
            "0,13",
            "10,12",
            "3,4",
            "3,0",
            "8,4",
            "1,10",
            "2,14",
            "8,10",
            "9,0",
            "",
            "fold along y=7",
        };

        var (points, folds) = Day_13.ParseInput(input);

        var actual = Day_13.Part1(points, folds[0]);

        actual.Should().Be(17);
    }
}
