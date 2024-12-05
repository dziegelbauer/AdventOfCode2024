namespace AOC2024;

public class Day05
{
    private readonly Dictionary<long, List<long>> _rules = new();
    private readonly List<List<long>> _manuals = [];
    public Day05(string inputFilePath)
    {
        var inputFile = File.ReadAllLines(inputFilePath);

        var index = 0;
        
        while(inputFile[index].Length > 0 && index < inputFile.Length)
        {
            var rule = inputFile[index].Split("|");
            var left = long.Parse(rule[0]);
            var right = long.Parse(rule[1]);

            if (!_rules.ContainsKey(left))
            {
                _rules.Add(left, []);
            }

            _rules[left].Add(right);
            index++;
        }

        index++;

        for (var i = index; i < inputFile.Length; i++)
        {
            var manual = inputFile[i].Split(",").Select(long.Parse).ToList();
            _manuals.Add(manual);
        }
    }

    public long Execute()
    {
        var middlePageSum = 0L;

        foreach (var manual in _manuals)
        {
            if(ManualFollowsRules(manual, _rules))
            {
                middlePageSum += manual[manual.Count / 2 ];
            }
        }
        
        return middlePageSum;
    }

    private bool ManualFollowsRules(List<long> manual, Dictionary<long, List<long>> rules)
    {
        return manual.All(page => PageFollowsRules(page, manual, rules));
    }

    private bool PageFollowsRules(long page, List<long> manual, Dictionary<long, List<long>> rules)
    {
        if(!rules.TryGetValue(page, out var applicableRules))
        {
            return true;
        }

        return applicableRules.All(rule =>
        {
            var pageIndex = manual.IndexOf(page);
            var ruleIndex = manual.IndexOf(rule);

            return (ruleIndex == -1 || pageIndex < ruleIndex);
        });
    }

    public long ExecutePart2()
    {
        var middlePageSum = 0L;

        foreach (var manual in _manuals)
        {
            if(!ManualFollowsRules(manual, _rules))
            {
                var correctedManual = CorrectOrderingErrors(manual, _rules);
                middlePageSum += correctedManual[correctedManual.Count / 2 ];
            }
        }
        
        return middlePageSum;
    }

    private List<long> CorrectOrderingErrors(List<long> manual, Dictionary<long, List<long>> rules)
    {
        var correctIndices = new Dictionary<long, int>();
        var correctedManual = manual.ToList();

        do
        {
            correctIndices.Clear();
            
            var badPages = correctedManual.Where(page => !PageFollowsRules(page, correctedManual, rules)).ToList();

            var goodPages = correctedManual.Where(page => PageFollowsRules(page, correctedManual, rules)).ToList();

            foreach (var page in badPages)
            {
                var pageRules = rules[page];
                var minIndex = int.MaxValue;

                foreach (var rule in pageRules)
                {
                    var ruleIndex = goodPages.IndexOf(rule);
                    if (ruleIndex != -1 && ruleIndex < minIndex)
                    {
                        minIndex = ruleIndex;
                    }
                }

                correctIndices.Add(page, minIndex);
            }
            
            correctedManual = [];
            correctedManual.AddRange(goodPages.ToList());
            
            while (correctIndices.Count > 0)
            {
                var topKey = correctIndices.MinBy(kvp => kvp.Value).Key;
            
                correctedManual.Insert(correctIndices[topKey], topKey);
                correctIndices.Remove(topKey);
            }
        } while(!ManualFollowsRules(correctedManual, rules));

        return correctedManual;
    }
}