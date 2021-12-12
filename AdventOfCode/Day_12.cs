using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode;

public struct Day12Line
{
    public string Full;
    public string Item1;
    public string Item2;
}

public class Day_12 : BaseDay
{
    private readonly Dictionary<string, List<string>> _graph;

    public Day_12()
    {
        _graph = MakeGraph(File.ReadAllLines(InputFilePath));
    }

    public override ValueTask<string> Solve_1()
    {
        return new(Part1(_graph).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new(Part2(_graph).ToString());
    }

    public static Dictionary<string, List<string>> MakeGraph(string[] input)
    {
        var graph = new Dictionary<string, List<string>>();

        foreach (var line in input)
        {
            var split = line.Split('-');
            var a = split[0];
            var b = split[1];

            graph.SetIfNotExists(a, () => new List<string>());
            graph.SetIfNotExists(b, () => new List<string>());

            graph[a].Add(b);
            graph[b].Add(a);
        }

        return graph;
    }

    public static int Part1(Dictionary<string, List<string>> graph)
    {
        return CountPaths(graph, "start", new HashSet<string>(), false);
    }

    public static int Part2(Dictionary<string, List<string>> graph)
    {
        return CountPaths(graph, "start", new HashSet<string>(), true);
    }

    public static int CountPaths(Dictionary<string, List<string>> graph, string node, HashSet<string> visited, bool canVisitTwice)
    {
        if (node == "end")
            return 1;

        var seen = visited.Contains(node);

        if (node == "start" && seen)
            return 0;

        if (seen && !canVisitTwice)
            return 0;

        var newVisited = new HashSet<string>(visited);

        if (!seen && node.ToLower() == node)
        {
            newVisited.Add(node);
        }

        return graph[node].Sum(next => CountPaths(graph, next, newVisited, canVisitTwice && !seen));
    }
}
