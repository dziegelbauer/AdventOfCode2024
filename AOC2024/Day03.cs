using System.Text.RegularExpressions;

namespace AOC2024;

public partial class Day03
{
    private readonly string _input;
    public Day03(string inputFilePath)
    {
        _input = File.ReadAllText(inputFilePath);
    }

    public long Execute()
    {
        var instructionRegex = MultiplyRegex();
        
        var matches = instructionRegex.Matches(_input);
        
        long finalResult = 0;

        foreach (Match match in matches)
        {
            var firstNumber = long.Parse(match.Groups[1].Value);
            var secondNumber = long.Parse(match.Groups[2].Value);
            var result = firstNumber * secondNumber;
            finalResult += result;
        }
        
        return finalResult;
    }

    public long ExecutePart2()
    {
        var multiplyRegex = MultiplyRegex();
        var doRegex = DoRegex();
        var dontRegex = DontRegex();
        
        var nextIndex = 0;
        var runningTotal = 0L;

        while (nextIndex < _input.Length)
        {
            var nextMultiply = multiplyRegex.Match(_input, nextIndex);
            var nextDo = doRegex.Match(_input, nextIndex);
            var nextDont = dontRegex.Match(_input, nextIndex);

            if (nextMultiply.Success && (!nextDo.Success || nextDont.Index > nextMultiply.Index) &&
                (!nextDont.Success || nextDont.Index > nextMultiply.Index))
            {
                nextIndex = nextMultiply.Index + nextMultiply.Length;
                runningTotal += long.Parse(nextMultiply.Groups[1].Value) * long.Parse(nextMultiply.Groups[2].Value);
            }
            else if (nextDont.Success && (!nextMultiply.Success || nextDont.Index < nextMultiply.Index) && (!nextDo.Success || nextDont.Index < nextDo.Index))
            {
                if (nextDo.Success)
                {
                    nextIndex = nextDo.Index + nextDo.Length;
                }
                else
                {
                    nextIndex = _input.Length;
                }
            }
            else if(nextDo.Success)
            {
                nextIndex = nextDo.Index + nextDo.Length;
            }
            else
            {
                nextIndex = _input.Length;
            }
        }
        
        return runningTotal;
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MultiplyRegex();

    [GeneratedRegex(@"do\(\)")]
    private static partial Regex DoRegex();

    [GeneratedRegex(@"don\'t\(\)")]
    private static partial Regex DontRegex();
}