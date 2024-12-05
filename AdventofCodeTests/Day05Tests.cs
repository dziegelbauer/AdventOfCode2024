using AOC2024;

namespace AdventOfCodeTests;

public class Day05Tests
{
    private string _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test05.txt";

    [Test]
    public void Day05Part01ProducesExampleOutput()
    {
        var day5Part1 = new Day05(_inputFilePath);
        var result = day5Part1.Execute();

        Assert.That(result, Is.EqualTo(143));
    }
    
    [Test]
    public void Day05Part02ProducesExampleOutput()
    {
        var day5Part2 = new Day05(_inputFilePath);
        var result = day5Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(123));
    }
}