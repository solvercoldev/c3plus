using System;

namespace Infrastructure.CrossCutting.NetFramework.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsField(this string source, string target, StringComparison comparer)
        {
            if (source == null) //Source no puede ser null
                throw new ArgumentNullException("source");
            return source.IndexOf(target, comparer) != -1;
        }


       
    }
}