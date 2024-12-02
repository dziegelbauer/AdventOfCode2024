using System.Text.RegularExpressions;

namespace AOC2024;

public partial class Day01
{
    private readonly List<long> _locationListOne = [];
    private readonly List<long> _locationListTwo = [];
    
    public Day01(string inputFilePath)
    {
        var inputFile = File.ReadAllLines(inputFilePath);

        var inputRegex = ColumnSplitRegex();

        foreach (var line in inputFile)
        {
            var match = inputRegex.Match(line);
            
            if (match.Success)
            {
                var locationOne = long.Parse(match.Groups[1].Value);
                var locationTwo = long.Parse(match.Groups[2].Value);
                
                _locationListOne.Add(locationOne);
                _locationListTwo.Add(locationTwo);
            }
        }
        
        if (_locationListOne.Count != _locationListTwo.Count)
        {
            throw new Exception("Lists are not the same length");
        }
    }
    
    public long Execute()
    {
        _locationListOne.Sort();
        _locationListTwo.Sort();

        var totalDistance = 0L;

        for (var i = 0; i < _locationListOne.Count; i++)
        {
            var distance = Math.Abs(_locationListTwo[i] - _locationListOne[i]);
            totalDistance += distance;
        }

        return totalDistance;
    }
    
    public long ExecutePart2()
    {
        var similarityScore = 0L;

        foreach (var location in _locationListOne)
        {
            var appearanceCount = _locationListTwo.Count(l => l == location);
            similarityScore += location * appearanceCount;
        }

        return similarityScore;
    }

    [GeneratedRegex(@"(\d*)[ \t]*(\d*)")]
    private static partial Regex ColumnSplitRegex();
}