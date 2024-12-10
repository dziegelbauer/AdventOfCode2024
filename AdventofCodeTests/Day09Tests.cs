using AOC2024;

namespace AdventOfCodeTests;

public class Day09Tests
{
    private readonly string _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test09.txt";

    [Test]
    public void Day09Part01ProducesExampleOutput()
    {
        var day8Part1 = new Day09(_inputFilePath);
        var result = day8Part1.Execute();

        Assert.That(result, Is.EqualTo(1928));
    }
    
    [Test]
    public void Day09Part02ProducesExampleOutput()
    {
        var day8Part2 = new Day09(_inputFilePath);
        var result = day8Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(2858));
    }
}