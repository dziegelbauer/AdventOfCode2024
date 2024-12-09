using AOC2024;

namespace AdventOfCodeTests;

public class Day08Tests
{
    private readonly string _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test08.txt";

    [Test]
    public void Day08Part01ProducesExampleOutput()
    {
        var day8Part1 = new Day08(_inputFilePath);
        var result = day8Part1.Execute();

        Assert.That(result, Is.EqualTo(14));
    }
    
    [Test]
    public void Day08Part02ProducesExampleOutput()
    {
        var day8Part2 = new Day08(_inputFilePath);
        var result = day8Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(34));
    }
}