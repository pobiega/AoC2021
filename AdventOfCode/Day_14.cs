namespace AdventOfCode;

public class Day14 : BaseDay
{
	private readonly Day14Solver _solver;

	public Day14()
	{
		_solver = new Day14Solver(File.ReadAllLines(InputFilePath));
	}

	public override ValueTask<string> Solve_1()
	{
		return new(_solver.Solve(10).ToString());
	}

	public override ValueTask<string> Solve_2()
	{
		return new(_solver.Solve(40).ToString());
	}
}

public class Day14Solver
{
	private readonly Dictionary<string, char> _rules;
	private readonly Dictionary<string, List<string>> _extendedRules;
	private readonly string _polymer;

	public Day14Solver(string[] input)
	{
		_rules = new Dictionary<string, char>();
		_extendedRules = new Dictionary<string, List<string>>();
		_polymer = input[0];

		foreach (var s in input[2..])
		{
			var split = s.Split(" -> ");
			_rules.Add(split[0], split[1][0]);
		}

		foreach (var kvp in _rules)
		{
			var left = string.Concat(kvp.Key[0] + kvp.Value);
			var right = string.Concat(kvp.Value + kvp.Key[1]);
			_extendedRules[kvp.Key] = new() {left, right};
		}
	}

	public long Solve(int steps)
	{
		var counts = new Dictionary<string, long>();

		foreach (var pair in _polymer.SlidingWindow(2))
		{
			counts[pair] = counts.GetValueOrDefault(pair, 0) + 1;
		}

		foreach (var _ in Enumerable.Range(0, steps))
		{
			var newCounts = new Dictionary<string, long>();

			foreach (var (pair, count) in counts)
			{
				var left = string.Concat(pair[0], _rules[pair]);
				var right = string.Concat(_rules[pair], pair[1]);

				newCounts[left] = newCounts.GetValueOrDefault(left, 0) + count;
				newCounts[right] = newCounts.GetValueOrDefault(right, 0) + count;
			}

			counts = newCounts;
		}

		var finalCounts = new Dictionary<char, long>();
		foreach (var (key, value) in counts)
		{
			finalCounts[key[0]] = finalCounts.GetValueOrDefault(key[0], 0) + value;
		}

		finalCounts[_polymer[^1]]++;
		var max = finalCounts.Values.Max();
		var min = finalCounts.Values.Min();

		return max - min;
	}
}
