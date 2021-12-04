using AdventOfCode;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventOfCodeTests;

public class Day04Tests
{
    [Fact]
    public void BoardCanIndex()
    {
        var board = new Board(new[] { 0, 1, 2, 3, 4, 10, 11, 12, 13, 14, 20, 21, 22, 23, 24, 30, 31, 32, 33, 34, 40, 41, 42, 43, 44 });

        board[0, 0].Should().Be(0);
        board[0, 1].Should().Be(10);
        board[4, 1].Should().Be(14);
    }

    [Fact]
    public void BoardCanDetectHorizontalWins()
    {
        var board = new Board(new[] { 0, 1, 2, 3, 4, 10, 11, 12, 13, 14, 20, 21, 22, 23, 24, 30, 31, 32, 33, 34, 40, 41, 42, 43, 44 });

        var markedNumbers = new HashSet<int> { 0, 1, 2, 3, 4 };

        board.CalculateWinner(markedNumbers).Should().BeTrue();
    }

    [Fact]
    public void BoardCanDetectVerticalWins()
    {
        var board = new Board(new[] { 0, 1, 2, 3, 4, 10, 11, 12, 13, 14, 20, 21, 22, 23, 24, 30, 31, 32, 33, 34, 40, 41, 42, 43, 44 });

        var markedNumbers = new HashSet<int> { 0, 10, 20, 30, 40 };

        board.CalculateWinner(markedNumbers).Should().BeTrue();
    }

    [Fact]
    public void CanGenerateBoards()
    {
        var lines = new[]
        {
            //"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
            "",
            "22 13 17 11  0",
            " 8  2 23  4 24",
            "21  9 14 16  7",
            " 6 10  3 18  5",
            " 1 12 20 15 19",
            "",
            " 3 15  0  2 22",
            " 9 18 13 17  5",
            "19  8  7 25 23",
            "20 11 10 24  4",
            "14 21 16 12  6",
            "",
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7",
        };

        var boards = Day_04.GenerateBoards(lines).ToArray();

        boards.Length.Should().Be(3);

        boards[0][0, 0].Should().Be(22);
        boards[1][0, 0].Should().Be(3);
        boards[2][0, 0].Should().Be(14);
    }
}
