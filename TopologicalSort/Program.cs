using System.Text.RegularExpressions;
using TpSort;

Console.WriteLine("Enter intagers to topologicaly sort them: j < k, n < m, ... ");
string? input = Console.ReadLine();

if (input == null)
{
    throw new Exception("Console buffer overflow");
}

input = input.Replace(",", "\n");
input = input.Replace(" ", "");

Regex regex = new Regex(@"^\d+<\d+$", RegexOptions.Multiline);
MatchCollection matches = regex.Matches(input);


if(matches.Count > 0)
{
    Pair<int>[] pairs = new Pair<int>[matches.Count];

    for(int i = 0; i < matches.Count; i++)
    {
        string pair = matches[i].Value;
        int index;
        for (index = 0; index < pair.Length; index++)
        {
            if (pair[index] == '<')
            {
                break;
            }
        }
        pairs[i] = new Pair<int>(
            int.Parse(pair.Substring(0, index)),
            int.Parse(pair.Substring(index + 1))
            );
    }

    int[] result = TopologicalSort.Sort(pairs);
    foreach (int num in result)
    {
        Console.Write($"{num} ");
    }
    Console.WriteLine();
}