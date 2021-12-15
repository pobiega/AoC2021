namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly int[] _draw;
    private readonly Board[] _boards;

    public Day04()
    {
        var lines = File.ReadAllLines(InputFilePath);

        _draw = lines.First().Split(',').Select(int.Parse).ToArray();

        _boards = GenerateBoards(lines.Skip(1)).ToArray();
    }

    public static IEnumerable<Board> GenerateBoards(IEnumerable<string> lines)
    {
        var buffer = new List<int>(Board.SideLength * Board.SideLength);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            buffer.AddRange(split.Select(int.Parse));

            if (buffer.Count == 25)
            {
                yield return new Board(buffer.ToArray());
                buffer.Clear();
            }
        }
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_draw, _boards).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_draw, _boards).ToString());
    }

    public static int Part1(int[] draw, Board[] boards)
    {
        var markedNumbers = new HashSet<int>(draw.Length);

        var first4 = draw.Take(4);
        var remainder = draw.Skip(4);

        foreach (var number in first4)
        {
            markedNumbers.Add(number);
        }

        foreach (var number in remainder)
        {
            markedNumbers.Add(number);

            foreach (var board in boards)
            {
                if (board.CalculateWinner(markedNumbers))
                {
                    return Part1ScoreCalc(board, markedNumbers, number);
                }
            }
        }

        throw new Exception("No winner and all numbers are exhausted!");
    }

    private static int Part1ScoreCalc(Board board, HashSet<int> markedNumbers, int number)
    {
        var unmarkedNumbers = board.GetUnmarkedNumbers(markedNumbers);

        var sum = unmarkedNumbers.Sum();

        return sum * number;
    }

    public static int Part2(int[] draw, Board[] boards)
    {
        var markedNumbers = new HashSet<int>(draw.Length);

        var first4 = draw.Take(4);
        var remainder = draw.Skip(4);

        Board mostRecentWinner = null;
        int numberThatWon = -1;
        int[] markedNumbersAtWin = new[] {0};

        foreach (var number in first4)
        {
            markedNumbers.Add(number);
        }

        foreach (var number in remainder)
        {
            markedNumbers.Add(number);

            foreach (var board in boards.Where(b => !b.HasWon))
            {
                if (board.CalculateWinner(markedNumbers))
                {
                    mostRecentWinner = board;
                    numberThatWon = number;
                    markedNumbersAtWin = markedNumbers.ToArray();
                }
            }
        }

        if(mostRecentWinner != null)
        {
            return Part1ScoreCalc(mostRecentWinner, markedNumbersAtWin.ToHashSet(), numberThatWon);
        }

        throw new Exception("No winner and all numbers are exhausted!");
    }
}

public class Board
{
    public const int SideLength = 5;

    private readonly int[] _numbers;

    public bool HasWon { get; private set; }

    public Board(int[] numbers)
    {
        if (numbers.Length != (SideLength * SideLength))
        {
            throw new Exception("Invalid board length");
        }
        _numbers = numbers;
    }

    public int this[int x, int y]
    {
        get
        {
            return _numbers[(y * SideLength) + x];
        }
    }

    public bool CalculateWinner(HashSet<int> markedNumbers)
    {
        for (int y = 0; y < SideLength; y++)
        {
            if (markedNumbers.Contains(this[0, y]) &&
                markedNumbers.Contains(this[1, y]) &&
                markedNumbers.Contains(this[2, y]) &&
                markedNumbers.Contains(this[3, y]) &&
                markedNumbers.Contains(this[4, y]))
            {
                HasWon = true;
                return true;
            }
        }

        for (int x = 0; x < SideLength; x++)
        {
            if (markedNumbers.Contains(this[x, 0]) &&
                markedNumbers.Contains(this[x, 1]) &&
                markedNumbers.Contains(this[x, 2]) &&
                markedNumbers.Contains(this[x, 3]) &&
                markedNumbers.Contains(this[x, 4]))
            {
                HasWon = true;
                return true;
            }
        }

        return false;
    }

    public IEnumerable<int> GetUnmarkedNumbers(HashSet<int> markedNumbers)
    {
        for (int y = 0; y < SideLength; y++)
        {
            for (int x = 0; x < SideLength; x++)
            {
                if (!markedNumbers.Contains(this[x, y]))
                    yield return this[x, y];
            }
        }
    }
}
