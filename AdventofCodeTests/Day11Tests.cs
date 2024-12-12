using AOC2024;

namespace AdventOfCodeTests;

public class Day11Tests
{
    private readonly string _inputFilePath = "/Users/dave/Projects/AdventOfCode2024/Input/test11.txt";

    [Test]
    public async Task Day11Part01ProducesExampleOutput()
    {
        var day11Part1 = new Day11(_inputFilePath);
        var result = await day11Part1.Execute();

        Assert.That(result, Is.EqualTo(55312));
    }
    
    [Test]
    public async Task Day11Part02ProducesExampleOutput()
    {
        var day11Part2 = new Day11(_inputFilePath);
        var result = await day11Part2.ExecutePart2();
        
        Assert.That(result, Is.EqualTo(55312));
    }
}