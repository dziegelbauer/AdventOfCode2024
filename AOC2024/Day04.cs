namespace AOC2024;

public class Day04
{
    private readonly char[][] _input;
    
    public Day04(string inputFilePath)
    {
        var inputFile = File.ReadAllLines(inputFilePath);
        _input = inputFile.Select(line => line.ToCharArray()).ToArray();
    }

    public long Execute()
    {
        var xmasCount = 0;
        
        for (var i = 0; i < _input.Length; i++)
        {
            for (int j = 0; j < _input[i].Length; j++)
            {
                if (_input[i][j] == 'X')
                {
                    if (i + 3 < _input.Length && _input[i + 1][j] == 'M' && _input[i + 2][j] == 'A' && _input[i + 3][j] == 'S')
                    {
                        xmasCount++;
                    }

                    if (i + 3 < _input.Length && j + 3 < _input[i].Length && _input[i + 1][j + 1] == 'M' &&
                        _input[i + 2][j + 2] == 'A' && _input[i + 3][j + 3] == 'S')
                    {
                        xmasCount++;
                    }

                    if (j + 3 < _input[i].Length && _input[i][j + 1] == 'M' && _input[i][j + 2] == 'A' &&
                        _input[i][j + 3] == 'S')
                    {
                        xmasCount++;
                    }

                    if (i > 2 && j + 3 < _input[i].Length && _input[i - 1][j + 1] == 'M' &&
                        _input[i - 2][j + 2] == 'A' &&
                        _input[i - 3][j + 3] == 'S')
                    {
                        xmasCount++;
                    }

                    if (i > 2 && _input[i - 1][j] == 'M' && _input[i - 2][j] == 'A' && _input[i - 3][j] == 'S')
                    {
                        xmasCount++;
                    }

                    if (i > 2 && j > 2 && _input[i - 1][j - 1] == 'M' && _input[i - 2][j - 2] == 'A' &&
                        _input[i - 3][j - 3] == 'S')
                    {
                        xmasCount++;
                    }

                    if (j > 2 && _input[i][j - 1] == 'M' && _input[i][j - 2] == 'A' && _input[i][j - 3] == 'S')
                    {
                        xmasCount++;
                    }

                    if (i < _input.Length - 3 && j > 2 && _input[i + 1][j - 1] == 'M' && _input[i + 2][j - 2] == 'A' &&
                        _input[i + 3][j - 3] == 'S')
                    {
                        xmasCount++;
                    }
                }
            }
        }
        return xmasCount;
    }

    public long ExecutePart2()
    {
        var xmasCount = 0;

        for (var i = 1; i + 1 < _input.Length; i++)
        {
            for (int j = 1; j + 1 < _input[i].Length; j++)
            {
                var xmasFoundAtPosition = 0;
                
                if (_input[i][j] == 'A')
                {
                    if (_input[i - 1][j - 1] == 'M' && _input[i + 1][j + 1] == 'S')
                    {
                        xmasFoundAtPosition++;
                    }

                    if (_input[i - 1][j + 1] == 'M' && _input[i + 1][j - 1] == 'S')
                    {
                        xmasFoundAtPosition++;
                    }

                    if (_input[i + 1][j - 1] == 'M' && _input[i - 1][j + 1] == 'S')
                    {
                        xmasFoundAtPosition++;
                    }

                    if (_input[i + 1][j + 1] == 'M' && _input[i - 1][j - 1] == 'S')
                    {
                        xmasFoundAtPosition++;
                    }

                    if (xmasFoundAtPosition == 2)
                    {
                        xmasCount++;
                    }
                }
            }
        }

        return xmasCount;
    }
}