namespace AOC2024;

public class Day08
{
    private readonly HashSet<(int x, int y, char frequency)> _antennas = [];
    private readonly Map _map;
    private readonly char[][] _baseMap;

    private class Map
    {
        public int LeftBoundary { get; set; }
        public int RightBoundary { get; set; }
        public int TopBoundary { get; set; }
        public int BottomBoundary { get; set; }
    }

    public Day08(string inputFilePath)
    {
        var inputFile = File.ReadAllLines(inputFilePath);
        
        _baseMap = inputFile.Select(x => x.ToCharArray()).ToArray();
        
        _map = new Map
        {
            LeftBoundary = 0,
            RightBoundary = inputFile[0].Length - 1,
            TopBoundary = 0,
            BottomBoundary = inputFile.Length - 1,
        };

        for (var y = 0; y < inputFile.Length; y++)
        {
            var line = inputFile[y].ToCharArray();
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] != '.')
                {
                    _antennas.Add((x, y, line[x]));
                }
            }
        }
    }

    private bool LocationIsWithinMap(int x, int y)
    {
        return x >= _map.LeftBoundary && x <= _map.RightBoundary && y >= _map.TopBoundary && y <= _map.BottomBoundary;
    }

    private void PrintMap(HashSet<(int x, int y)> antiNodes)
    {
        var workingMap = _baseMap.Select(row => row.ToArray()).ToArray();

        foreach (var node in antiNodes)
        {
            workingMap[node.y][node.x] = workingMap[node.y][node.x] == '.' ? '#' : workingMap[node.y][node.x];
        }

        foreach (var row in workingMap)
        {
            Console.WriteLine(string.Join("", row));
        }
    }

    public long Execute()
    {
        HashSet<(int x, int y)> antiNodes = [];
        
        foreach (var firstAntenna in _antennas)
        {
            foreach (var secondAntenna in _antennas.Where(antenna => antenna != firstAntenna && antenna.frequency == firstAntenna.frequency))
            {
                var horizontalDistance = Math.Abs(firstAntenna.x - secondAntenna.x);
                var verticalDistance = Math.Abs(firstAntenna.y - secondAntenna.y);

                int firstAntiNodeX;
                int firstAntiNodeY;
                int secondAntiNodeX;
                int secondAntiNodeY;
                
                if (firstAntenna.x > secondAntenna.x)
                {
                    firstAntiNodeX = firstAntenna.x + horizontalDistance;
                    secondAntiNodeX = secondAntenna.x - horizontalDistance;
                }
                else if (firstAntenna.x < secondAntenna.x)
                {
                    firstAntiNodeX = firstAntenna.x - horizontalDistance;
                    secondAntiNodeX = secondAntenna.x + horizontalDistance;
                }
                else
                {
                    firstAntiNodeX = firstAntenna.x;
                    secondAntiNodeX = firstAntenna.x;
                }

                if (firstAntenna.y > secondAntenna.y)
                {
                    firstAntiNodeY = firstAntenna.y + verticalDistance;
                    secondAntiNodeY = secondAntenna.y - verticalDistance;
                }
                else if (firstAntenna.y < secondAntenna.y)
                {
                    firstAntiNodeY = firstAntenna.y - verticalDistance;
                    secondAntiNodeY = secondAntenna.y + verticalDistance;
                }
                else
                {
                    firstAntiNodeY = firstAntenna.y;
                    secondAntiNodeY = firstAntenna.y;
                }

                if (LocationIsWithinMap(firstAntiNodeX, firstAntiNodeY))
                {
                    antiNodes.Add((firstAntiNodeX, firstAntiNodeY));
                }

                if (LocationIsWithinMap(secondAntiNodeX, secondAntiNodeY))
                {
                    antiNodes.Add((secondAntiNodeX, secondAntiNodeY));
                }
            }
        }
        
        return antiNodes.Count;
    }

    private static (double m, double b) FindLine(double x1, double y1, double x2, double y2)
    {
        var m = (y2 - y1) / (x2 - x1);
        var b = y1 - m * x1;
        
        return (m, b);
    }

    private static bool PointIsOnLine(double m, double b, double x, double y)
    {
        return Math.Abs(m * x + b - y) < 0.0005;
    }

    public long ExecutePart2()
    {
        HashSet<(int x, int y)> antiNodes = [];

        foreach (var firstAntenna in _antennas)
        {
            foreach (var secondAntenna in _antennas.Where(antenna =>
                         antenna != firstAntenna && antenna.frequency == firstAntenna.frequency))
            {
                var line = FindLine(firstAntenna.x, firstAntenna.y, secondAntenna.x, secondAntenna.y);
                for (var x = _map.LeftBoundary; x <= _map.RightBoundary; x++)
                {
                    for (int y = _map.TopBoundary; y <= _map.BottomBoundary; y++)
                    {
                        if (PointIsOnLine(line.m, line.b, x, y))
                        {
                            antiNodes.Add((x, y));
                        }
                    }
                }
            }
        }

        return antiNodes.Count;;
    }
}