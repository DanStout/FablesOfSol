using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Extensions
{
    ///<summary>Convenience method for formatting a string with a variable number of arguments</summary>
    public static string FormatWith(this string str, params object[] args)
    {
        return string.Format(str, args);
    }

    ///<summary>Returns whether this float is within difference of the second float</summary>
    public static bool Within(this float first, float second, float difference)
    {
        return Math.Abs(first - second) <= difference;
    }
}

