using AOC2024;

namespace AdventOfCodeTests;

public class Day01Tests
{
    private string _inputFilePath = "";
    
    [SetUp]
    public void Setup()
    {
        _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test01.txt";
    }

    [Test]
    public void Day01Part01ProducesExampleOutput()
    {
        var day1Part1 = new Day01(_inputFilePath);
        var result = day1Part1.Execute();

        Assert.That(result, Is.EqualTo(11));
    }
    
    [Test]
    public void Day01Part02ProducesExampleOutput()
    {
        var day1Part2 = new Day01(_inputFilePath);
        var result = day1Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(31));
    }
}