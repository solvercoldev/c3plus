using System;
using System.Text.RegularExpressions;

namespace Infrastructure.CrossCutting.NetFramework.Util
{
    public class Text
    {
        private Text()
        {
        }

        /// <summary>
        /// Truncate a given text to the given number of characters. 
        /// Also any embedded html is stripped.
        /// </summary>
        /// <param name="fullText"></param>
        /// <param name="numberOfCharacters"></param>
        /// <returns></returns>
        public static string TruncateText(string fullText, int numberOfCharacters)
        {
            string text;
            if (fullText.Length > numberOfCharacters)
            {
                int spacePos = fullText.IndexOf(" ", numberOfCharacters);
                if (spacePos > -1)
                {
                    text = fullText.Substring(0, spacePos) + "...";
                }
                else
                {
                    text = fullText;
                }
            }
            else
            {
                text = fullText;
            }
            var regexStripHtml = new Regex("<[^>]+>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            text = regexStripHtml.Replace(text, " ");
            return text;
        }

        /// <summary>
        /// Ensure that the given string has a trailing slash.
        /// </summary>
        /// <param name="stringThatNeedsTrailingSlash"></param>
        /// <returns></returns>
        public static string EnsureTrailingSlash(string stringThatNeedsTrailingSlash)
        {
            if (!stringThatNeedsTrailingSlash.EndsWith("/"))
            {
                return stringThatNeedsTrailingSlash + "/";
            }
            else
            {
                return stringThatNeedsTrailingSlash;
            }
        }

        public static int[] ExtractIntegersFromCommaSeparatedString(string values)
        {
            if (String.IsNullOrEmpty(values))
            {
                throw new ArgumentException(@"Values may not be null or empty", "values");
            }
            var valueArray = values.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (valueArray.Length > 0)
            {
                var integerValues = new int[valueArray.Length];
                var i = 0;
                foreach (var stringValue in valueArray)
                {
                    integerValues[i] = Int32.Parse(stringValue);
                    i++;
                }
                return integerValues;
            }
            else
            {
                return new int[0];
            }
        }
    }
}