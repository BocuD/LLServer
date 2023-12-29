namespace LLServer.Common;

public static class StringFlagsExtensions
{
    public static string SetFlag(this string s, int index)
    {
        var charArray = s.ToCharArray();
        charArray[index] = '1';
        return new string(charArray);
    }
    
    public static string ClearFlag(this string s, int index)
    {
        var charArray = s.ToCharArray();
        charArray[index] = '0';
        return new string(charArray);
    }
    
    public static bool GetFlag(this string s, int index)
    {
        return s[index] == '1';
    }
    
    public static string SetFlag(this string s, int index, bool value)
    {
        return value ? s.SetFlag(index) : s.ClearFlag(index);
    }
}