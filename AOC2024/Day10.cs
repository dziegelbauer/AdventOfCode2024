namespace AOC2024;

public class Day10
{
    private readonly List<List<long>> _map = [];
    
    private class HikingTrail
    {
        public (int x, int y) Start { get; init; }
        public (int x, int y) End { get; set; }
        public List<(int x, int y)> Path { get; init; } = [];
        public long Score { get; set; }
        
        public HikingTrail((int x, int y) origin)
        {
            Path.Add(origin);
            Start = origin;
        }

        private sealed class StartEndPathEqualityComparer : IEqualityComparer<HikingTrail>
        {
            public bool Equals(HikingTrail? x, HikingTrail? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x is null) return false;
                if (y is null) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Start.Equals(y.Start) && x.End.Equals(y.End) && x.Path.Equals(y.Path);
            }

            public int GetHashCode(HikingTrail obj)
            {
                return HashCode.Combine(obj.Start, obj.End, obj.Path);
            }
        }

        public static IEqualityComparer<HikingTrail> StartEndPathComparer { get; } = new StartEndPathEqualityComparer();
    }
    
    public Day10(string inputFilePath)
    {
        var inputFile = File.ReadAllLines(inputFilePath);
        foreach (var line in inputFile)
        {
            _map.Add(line.ToCharArray().Select(c => c.ToString()).Select(long.Parse).ToList());
        }
    }

    public long Execute()
    {
        var trailHeads = new Dictionary<(int x, int y), HashSet<(int x, int y)>>();

        for (var y = 0; y < _map.Count; y++)
        {
            for (var x = 0; x < _map[y].Count; x++)
            {
                if (_map[y][x] == 0)
                {
                    FindPaths((x, y), x, y, trailHeads);
                }
            }
        }

        return trailHeads.Sum(ScoreTrail);
    }

    private long ScoreTrail(KeyValuePair<(int x, int y), HashSet<(int x, int y)>> trailHead)
    {
        return trailHead.Value.Count;
    }

    private bool PositionIsOnMap(int x, int y)
    {
        return x >= 0 && x < _map[0].Count && y >= 0 && y < _map.Count;
    }

    private void FindPaths((int x, int y) origin, int x, int y, Dictionary<(int x, int y), HashSet<(int x, int y)>> trailHeads)
    {
        var startingAltitude = _map[y][x];

        if (startingAltitude == 9)
        {
            trailHeads[origin].Add((x, y));
            return;
        }
        
        if (startingAltitude == 0)
        {
            trailHeads.Add(origin, []);
        }

        List<(int x, int y)> possibleSteps =
        [
            (x - 1, y),
            (x + 1, y),
            (x, y - 1),
            (x, y + 1)
        ];

        foreach (var step in possibleSteps)
        {
            if(PositionIsOnMap(step.x, step.y) && _map[step.y][step.x] == startingAltitude + 1)
            {
                FindPaths(origin, step.x, step.y, trailHeads);
            }
        }
    }

    public long ExecutePart2()
    {
        var trails = new List<HikingTrail>();

        for (var y = 0; y < _map.Count; y++)
        {
            for (var x = 0; x < _map[y].Count; x++)
            {
                if (_map[y][x] == 0)
                {
                    FindPaths2(x, y, trails);
                }
            }
        }

        return trails.Count;
    }

    private void FindPaths2(int x, int y, List<HikingTrail> trails, HikingTrail? currentTrail  = null)
    {
        currentTrail ??= new HikingTrail((x, y));
        
        var startingAltitude = _map[y][x];

        if (startingAltitude == 9)
        {
            trails.Add(currentTrail);
            return;
        }

        List<(int x, int y)> possibleSteps =
        [
            (x - 1, y),
            (x + 1, y),
            (x, y - 1),
            (x, y + 1)
        ];

        foreach (var step in possibleSteps)
        {
            if(PositionIsOnMap(step.x, step.y) && _map[step.y][step.x] == startingAltitude + 1)
            {
                FindPaths2(step.x, step.y, trails, currentTrail);
            }
        }
    }
}