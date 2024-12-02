using AOC2024;

namespace AdventOfCodeTests;

public class Day02Tests
{
    private string _inputFilePath = "";
    
    [SetUp]
    public void Setup()
    {
        _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test02.txt";
    }

    [Test]
    public void Day02Part01ProducesExampleOutput()
    {
        var day2Part1 = new Day02(_inputFilePath);
        var result = day2Part1.Execute();

        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void Day02Part02ProducesExampleOutput()
    {
        var day2Part2 = new Day02(_inputFilePath);
        var result = day2Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(4));
    }
}