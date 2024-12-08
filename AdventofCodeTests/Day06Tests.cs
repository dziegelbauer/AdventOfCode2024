using AOC2024;

namespace AdventOfCodeTests;

public class Day06Tests
{
    private readonly string _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test06.txt";

    [Test]
    public void Day06Part01ProducesExampleOutput()
    {
        var day6Part1 = new Day06(_inputFilePath);
        var result = day6Part1.Execute();

        Assert.That(result, Is.EqualTo(41));
    }
    
    [Test]
    public void Day06Part02ProducesExampleOutput()
    {
        var day6Part2 = new Day06(_inputFilePath);
        var result = day6Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(6));
    }
}