using System;
using System.Collections.Generic;
using System.Linq;

namespace FernandoPFH_Essentials_Runtime
{
    public static class StringExtension
    {
        public static string PopulateTemplate(this string template, Dictionary<string, string> replaces)
        {
            string result = template;

            foreach (string replaceKey in replaces.Keys)
                result = result.Replace("{{" + replaceKey + "}}", replaces[replaceKey]);

            return result;
        }

        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string[] SplitLast(this string s, string sub)
        {
            string[] stringParts = s.Reverse().Split(sub);
            foreach (string stringPart in stringParts)
                stringPart.Reverse();

            stringParts.Reverse();

            return stringParts;
        }
    }
}