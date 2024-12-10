namespace AOC2024;

public class Day09
{
    private readonly char[] _inputFile;
    
    public Day09(string inputFilePath)
    {
        _inputFile = File.ReadAllText(inputFilePath).ToCharArray();
    }

    public long Execute()
    {
        Stack<long> startingDisk = new();
        List<long?> finalDisk = [];
        
        var fileId = 0L;
        var nextFieldIsFile = true;

        foreach (var fileKey in _inputFile)
        {
            if (long.TryParse(fileKey.ToString(), out var sectionLength))
            {
                if (nextFieldIsFile)
                {
                    for (var i = 0; i < sectionLength; i++)
                    {
                        startingDisk.Push(fileId);
                        finalDisk.Add(fileId);
                    }
                    
                    fileId++;
                }
                else
                {
                    for (var i = 0; i < sectionLength; i++)
                    {
                        finalDisk.Add(null);
                    }
                }
                nextFieldIsFile = !nextFieldIsFile;
            }
        }
        
        var finalLength = finalDisk.Count(fileKey => fileKey is not null);

        while (finalDisk[..finalLength].Contains(null))
        {
            var firstBlank = finalDisk.IndexOf(null);
            
            finalDisk[firstBlank] = startingDisk.Pop();
        }
        
        return CalculateChecksum(finalDisk.Take(finalLength).ToList());
    }

    private static long CalculateChecksum(List<long?> finalDisk)
    {
        var checksum = 0L;
        
        for (var i = 0; i < finalDisk.Count; i++)
        {
            if (finalDisk[i] is not null)
            {
                checksum += finalDisk[i]!.Value * i;
            }
        }
        
        return checksum;
    }

    public long ExecutePart2()
    {
        List<long?> finalDisk = [];
        long targetFileId = 0;
        
        var fileId = 0L;
        var nextFieldIsFile = true;

        foreach (var fileKey in _inputFile)
        {
            var sectionLength = long.Parse(fileKey.ToString());
            
            if (nextFieldIsFile)
            {
                for (var i = 0; i < sectionLength; i++)
                {
                    finalDisk.Add(fileId);
                }

                targetFileId = fileId;
                fileId++;
            }
            else
            {
                for (var i = 0; i < sectionLength; i++)
                {
                    finalDisk.Add(null);
                }
            }
            nextFieldIsFile = !nextFieldIsFile;
        }

        while (targetFileId >= 0)
        {
            var startOfTargetFile = finalDisk.IndexOf(targetFileId);
            var endOfTargetFile = finalDisk.LastIndexOf(targetFileId);
            var fileLength = endOfTargetFile - startOfTargetFile + 1;
            
            var firstAvailablePosition = FindFirstFreeLocation(finalDisk, fileLength, startOfTargetFile);
            
            if (firstAvailablePosition != -1)
            {
                for (var i = 0; i < fileLength; i++)
                {
                    finalDisk[firstAvailablePosition + i] = targetFileId;
                    finalDisk[startOfTargetFile + i] = null;
                }
            }

            targetFileId--;
        }

        return CalculateChecksum(finalDisk);
    }

    private static int FindFirstFreeLocation(List<long?> finalDisk, int fileLength, int highestValidIndex)
    {
        var lastFoundIndex = 0;

        while (lastFoundIndex < highestValidIndex && lastFoundIndex != -1)
        {
            lastFoundIndex = finalDisk.IndexOf(null, lastFoundIndex);
            if (lastFoundIndex != -1 && lastFoundIndex + fileLength - 1 < highestValidIndex)
            {
                if(Enumerable.Range(lastFoundIndex, fileLength).All(index => finalDisk[index] is null))
                {
                    return lastFoundIndex;
                }
            }
            
            if (lastFoundIndex != -1)
            {
                lastFoundIndex++;
            }
        }
        
        return -1;
    }
}