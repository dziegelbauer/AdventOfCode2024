using AOC2024;

namespace AdventOfCodeTests;

public class Day04Tests
{
    private string _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test04.txt";

    [Test]
    public void Day04Part01ProducesExampleOutput()
    {
        var day4Part1 = new Day04(_inputFilePath);
        var result = day4Part1.Execute();

        Assert.That(result, Is.EqualTo(18));
    }
    
    [Test]
    public void Day04Part02ProducesExampleOutput()
    {
        var day4Part2 = new Day04(_inputFilePath);
        var result = day4Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(9));
    }
}