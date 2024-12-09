using AOC2024;

namespace AdventOfCodeTests;

public class Day07Tests
{
    private readonly string _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test07.txt";

    [Test]
    public void Day07Part01ProducesExampleOutput()
    {
        var day7Part1 = new Day07(_inputFilePath);
        var result = day7Part1.Execute();

        Assert.That(result, Is.EqualTo(3749));
    }
    
    [Test]
    public void Day07Part02ProducesExampleOutput()
    {
        var day7Part2 = new Day07(_inputFilePath);
        var result = day7Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(11387));
    }
}