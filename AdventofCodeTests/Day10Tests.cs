using AOC2024;

namespace AdventOfCodeTests;

public class Day10Tests
{
    private readonly string _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test10.txt";

    [Test]
    public void Day10Part01ProducesExampleOutput()
    {
        var day10Part1 = new Day10(_inputFilePath);
        var result = day10Part1.Execute();

        Assert.That(result, Is.EqualTo(36));
    }
    
    [Test]
    public void Day10Part02ProducesExampleOutput()
    {
        var day10Part2 = new Day10(_inputFilePath);
        var result = day10Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(81));
    }
}