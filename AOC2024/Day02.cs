namespace AOC2024;

public class Day02
{
    private readonly List<List<long>> _reports = [];
    
    public Day02(string inputFilePath)
    {
        var inputFile = File.ReadAllLines(inputFilePath);

        foreach (var line in inputFile)
        {
            var report = line.Split(" ").Select(long.Parse).ToList();
            _reports.Add(report);
        }
    }

    public long Execute()
    {
        var unsafeCount = 0;
        
        foreach (var report in _reports)
        {
            if (!ReportIsSafe(report))
            {
                unsafeCount++;
            }
        }
        
        return _reports.Count - unsafeCount;
    }

    public long ExecutePart2()
    {
        var unsafeCount = 0;

        foreach (var report in _reports)
        {
            if (ReportIsSafe(report)) continue;

            if (Enumerable.Range(0, report.Count).Any(index =>
            {
                var workingReport = report.ToList();
                workingReport.RemoveAt(index);

                return ReportIsSafe(workingReport);
            })) continue;
            
            unsafeCount++;
        }

        return _reports.Count - unsafeCount;
    }

    private static bool ReportIsSafe(List<long> report)
    {
        var previousItem = report[0];
        var direction = 0;

        for (var i = 1; i < report.Count; i++)
        {
            var currentItem = report[i];

            if (currentItem == previousItem)
            {
                return false;
            }

            var difference = currentItem - previousItem;

            if (Math.Abs(difference) > 3)
            {
                return false;
            }

            if (difference > 0 && direction < 0)
            {
                return false;
            }

            if (difference < 0 && direction > 0)
            {
                return false;
            }

            direction = difference switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => direction
            };

            previousItem = currentItem;
        }
        
        return true;
    }
}