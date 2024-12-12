using System.Collections.Concurrent;

namespace AOC2024;

public class Day11
{
    private List<long> _stones;
    
    public Day11(string inputFilePath)
    {
        var inputFile = File.ReadAllText(inputFilePath);
        
        _stones = inputFile.Split(" ").Select(long.Parse).ToList();
    }

    public async Task<long> Execute()
    {
        ConcurrentDictionary<(long stoneValue, int blinkCount), long> resultCache = new();
        List<Task<long>> tasks = [];
        const int maxBlinks = 25;

        for (var i = 0; i < _stones.Count; i++)
        {
            var currentIndex = i;
            tasks.Add(Task.Run(() => GetStoneReplication(_stones[currentIndex], 1, maxBlinks, resultCache)));
        }
        
        await Task.WhenAll(tasks);
        
        return tasks.Sum(task => task.Result);
    }

    private static List<long> MutateStone(long stone)
    {
        if (stone == 0)
        {
            return [1];
        }

        var number = stone.ToString();

        var numberOfDigits = number.Length;
        
        if (numberOfDigits % 2 == 0)
        {
            var newNumberDigits = numberOfDigits / 2;
            return [uint.Parse(number[..newNumberDigits]), uint.Parse(number[newNumberDigits..])];
        }

        return [stone * 2024];
    }

    public async Task<long> ExecutePart2()
    {
        ConcurrentDictionary<(long stoneValue, int blinkCount), long> resultCache = new();
        List<Task<long>> tasks = [];
        const int maxBlinks = 75;

        for (var i = 0; i < _stones.Count; i++)
        {
            var currentIndex = i;
            tasks.Add(Task.Run(() => GetStoneReplication(_stones[currentIndex], 1, maxBlinks, resultCache)));
        }
        
        await Task.WhenAll(tasks);
        
        return tasks.Sum(task => task.Result);
    }

    private long GetStoneReplication(long stone, int currentBlink, int maxBlinks, ConcurrentDictionary<(long stoneValue, int blinkCount), long> resultCache)
    {
        if (currentBlink == maxBlinks)
        {
            if (!resultCache.TryGetValue((stone, currentBlink), out var finalResult))
            {
                var mutationResult = MutateStone(stone);
                finalResult = mutationResult.Count;
                resultCache[(stone, currentBlink)] = finalResult;
            }
            
            return finalResult;
        }
        
        if (!resultCache.TryGetValue((stone, currentBlink), out var result))
        {
            var mutationResult = MutateStone(stone);
            if (mutationResult.Count == 1)
            {
                result = GetStoneReplication(mutationResult[0], currentBlink + 1, maxBlinks, resultCache);
            }
            else if (mutationResult.Count == 2)
            {
                result = GetStoneReplication(mutationResult[0], currentBlink + 1, maxBlinks, resultCache) + 
                         GetStoneReplication(mutationResult[1], currentBlink + 1, maxBlinks, resultCache);
            }
            else
            {
                throw new Exception("Invalid stone count");
            }
            resultCache[(stone, currentBlink)] = result;
        }
        
        return result;
    }
}