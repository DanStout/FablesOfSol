using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public static class StringExtensions
    {
        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
    }

