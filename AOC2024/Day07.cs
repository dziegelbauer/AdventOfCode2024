using System.Security.Cryptography.X509Certificates;

namespace AOC2024;

public class Day07
{
    private record Equation(long Result, List<long> Values);

    private List<Equation> _equations = [];

    private enum Op
    {
        Add,
        Multiply,
    }

    private enum Op2
    {
        Add,
        Multiply,
        Concatenate
    }

    public Day07(string inputFilePath)
    {
        var inputFile = File.ReadAllLines(inputFilePath);

        foreach (var equation in inputFile)
        {
            var resultAndValues = equation.Split(":");

            var parsedEquation = new Equation(
                long.Parse(resultAndValues[0]),
                resultAndValues[1].Trim().Split(" ").Select(long.Parse).ToList());

            _equations.Add(parsedEquation);
        }
    }

    public long Execute()
    {
        var runningTotal = 0L;
        
        foreach (var equation in _equations)
        {
            var opPositions = equation.Values.Count - 1;
            var desiredResult = equation.Result;

            if (GetEquationValue(desiredResult, equation.Values))
            {
                runningTotal += desiredResult;
            }
        }

        return runningTotal;
    }

    private bool GetEquationValue(long desiredOutcome, List<long> values)
    {
        if (values.Count < 2) throw new ArgumentException("Values must be at least 2.");

        foreach (var op in Enum.GetValues<Op>())
        {
            var firstResult = op switch
            {
                Op.Add => values[0] + values[1],
                Op.Multiply => values[0] * values[1],
                _ => throw new ArgumentOutOfRangeException()
            };

            if (values.Count == 2)
            {
                if (firstResult == desiredOutcome)
                {
                    return true;
                }
            }
            else
            {
                List<long> newValues = [ firstResult ];
                newValues.AddRange(values.Skip(2));
                if (GetEquationValue(desiredOutcome, newValues))
                {
                    return true;
                }
            }    
        }

        return false;
    }

    public long ExecutePart2()
    {
        var runningTotal = 0L;
        
        foreach (var equation in _equations)
        {
            var desiredResult = equation.Result;

            if (GetEquationValue2(desiredResult, equation.Values))
            {
                runningTotal += desiredResult;
            }
        }

        return runningTotal;
    }
    
    private bool GetEquationValue2(long desiredOutcome, List<long> values)
    {
        if (values.Count < 2) throw new ArgumentException("Values must be at least 2.");

        foreach (var op in Enum.GetValues<Op2>())
        {
            var firstResult = op switch
            {
                Op2.Add => values[0] + values[1],
                Op2.Multiply => values[0] * values[1],
                Op2.Concatenate => long.Parse($"{values[0]}{values[1]}"),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (values.Count == 2)
            {
                if (firstResult == desiredOutcome)
                {
                    return true;
                }
            }
            else
            {
                List<long> newValues = [ firstResult ];
                newValues.AddRange(values.Skip(2));
                if (GetEquationValue2(desiredOutcome, newValues))
                {
                    return true;
                }
            }    
        }

        return false;
    }
}