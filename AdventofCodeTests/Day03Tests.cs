using AOC2024;

namespace AdventOfCodeTests;

public class Day03Tests
{
    private string _inputFilePath = "";
    
    [SetUp]
    public void Setup()
    {
        _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test03.txt";
    }

    [Test]
    public void Day03Part01ProducesExampleOutput()
    {
        var day3Part1 = new Day03(_inputFilePath);
        var result = day3Part1.Execute();

        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void Day03Part02ProducesExampleOutput()
    {
        var day3Part2 = new Day03(_inputFilePath);
        var result = day3Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(4));
    }
}