namespace AOC2024;

public class Day06
{
    private enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    private readonly List<List<char>> _map;

    public Day06(string inputFilePath)
    {
        var inputFile = File.ReadAllLines(inputFilePath);

        _map = inputFile.Select(line => line.ToCharArray().ToList()).ToList();
    }

    public long Execute()
    {
        var workingMap = _map.Select(row => row.ToList()).ToList();
        return PredictPatrol(workingMap);
    }

    public long ExecutePart2()
    {
        return FindPossibleLoops();
    }

    private Direction Turn(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private bool NextSpaceIsBlocked(int x, int y, Direction direction, List<List<char>> map)
    {
        return direction switch
        {
            Direction.Up => y > 0 && map[y - 1][x] == '#',
            Direction.Right => x + 1 < map.Count && map[y][x + 1] == '#',
            Direction.Down => y + 1 < map.Count && map[y + 1][x] == '#',
            Direction.Left => x > 0 && map[y][x - 1] == '#',
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private (int x, int y) FindGuard(List<List<char>> map)
    {
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[y].Count; x++)
            {
                if (map[y][x] == '^')
                {
                    return (x, y);
                }
            }
        }

        throw new Exception("Guard not found");
    }

    private long FindPossibleLoops()
    {
        var map = _map.Select(row => row.ToList()).ToList();
        var (x, y) = FindGuard(map);
        var (startX, startY) = (x, y);
        var loopPoints = new HashSet<(int x, int y)>();
        var coordsInPath = new HashSet<(int x, int y)>();

        var direction = Direction.Up;

        while (x < map[0].Count && y < map.Count && x >= 0 && y >= 0)
        {
            if (NextSpaceIsBlocked(x, y, direction, map))
            {
                direction = Turn(direction);
            }
            else
            {
                coordsInPath.Add((x, y));
                Func<int, int, (int, int)> move = direction switch
                {
                    Direction.Up => (xCoord, yCoord) => (xCoord, yCoord - 1),
                    Direction.Right => (xCoord, yCoord) => (xCoord + 1, yCoord),
                    Direction.Down => (xCoord, yCoord) => (xCoord, yCoord + 1),
                    Direction.Left => (xCoord, yCoord) => (xCoord - 1, yCoord),
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                };

                (x, y) = move(x, y);
            }
        }

        foreach (var coords in coordsInPath)
        {
            if ((coords.x, coords.y) == (startX, startY)) continue;

            var workingMap = _map.Select(row => row.ToList()).ToList();
            workingMap[coords.y][coords.x] = '#';
            if (BarrierCreatesLoop(startX, startY, Direction.Up, workingMap))
            {
                loopPoints.Add((coords.x, coords.y));
            }
        }
        
        return loopPoints.Count;
    }

    private bool BarrierCreatesLoop(int startX, int startY, Direction startDirection, List<List<char>> map)
    {
        var x = startX;
        var y = startY;
        var direction = startDirection;
        var spacesVisited = new HashSet<(int x, int y, Direction direction)>();

        while (x < map[0].Count && y < map.Count && x >= 0 && y >= 0)
        {
            if (NextSpaceIsBlocked(x, y, direction, map))
            {
                direction = Turn(direction);
            }
            else
            {
                if (!NextStepIsOffMap(x, y, direction, map) && spacesVisited.Contains((x, y, direction)))
                {
                    return true;
                }

                spacesVisited.Add((x, y, direction));
                Func<int, int, (int, int)> move = direction switch
                {
                    Direction.Up => (xCoord, yCoord) => (xCoord, yCoord - 1),
                    Direction.Right => (xCoord, yCoord) => (xCoord + 1, yCoord),
                    Direction.Down => (xCoord, yCoord) => (xCoord, yCoord + 1),
                    Direction.Left => (xCoord, yCoord) => (xCoord - 1, yCoord),
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                };

                (x, y) = move(x, y);
            }
        }

        return false;
    }

    private bool NextStepIsOffMap(int x, int y, Direction startDirection, List<List<char>> map)
    {
        return startDirection switch
        {
            Direction.Up => y == 0,
            Direction.Right => x == map[0].Count - 1,
            Direction.Down => y == map.Count - 1,
            Direction.Left => x == 0,
            _ => throw new ArgumentOutOfRangeException(nameof(startDirection), startDirection, null)
        };
    }

    private long PredictPatrol(List<List<char>> map)
    {
        var (x, y) = FindGuard(map);

        var direction = Direction.Up;
        var spacesVisited = new HashSet<(int x, int y)>();

        while (x < map[0].Count && y < map.Count && x >= 0 && y >= 0)
        {
            if (NextSpaceIsBlocked(x, y, direction, map))
            {
                direction = Turn(direction);
            }
            else
            {
                spacesVisited.Add((x, y));
                Func<int, int, (int, int)> move = direction switch
                {
                    Direction.Up => (xCoord, yCoord) => (xCoord, yCoord - 1),
                    Direction.Right => (xCoord, yCoord) => (xCoord + 1, yCoord),
                    Direction.Down => (xCoord, yCoord) => (xCoord, yCoord + 1),
                    Direction.Left => (xCoord, yCoord) => (xCoord - 1, yCoord),
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                };

                (x, y) = move(x, y);
            }
        }

        return spacesVisited.Count;
    }
}