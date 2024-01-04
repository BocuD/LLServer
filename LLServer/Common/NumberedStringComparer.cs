using System.Text.RegularExpressions;

namespace LLServer.Common;

public class NumberedStringComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        //get the substrings before the numbers at the end
        string sX = Regex.Match(x, @"[^\d]*").Value;
        string sY = Regex.Match(y, @"[^\d]*").Value;

        int stringComparison = string.Compare(sX, sY, StringComparison.Ordinal);

        if (stringComparison == 0)
        {
            // If strings are equal, compare the numbers
            int xNumbers = GetNumbers(x);
            int yNumbers = GetNumbers(y);

            return xNumbers.CompareTo(yNumbers);
        }

        return stringComparison;
    }

    private int GetNumbers(string input)
    {
        var match = Regex.Match(input, @"\d+");
        
        if (match.Success)
        {
            return int.Parse(match.Value);
        }

        return int.MaxValue;
    }
}