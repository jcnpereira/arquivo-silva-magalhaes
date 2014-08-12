using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Utilitites
{
    /// <summary>
    /// 
    /// An IComparer that sorts numeric strings in a natural
    /// way, i.e., "2" < "10", and "2a" > "2".
    /// 
    /// Modified so that strings with leading zeros don't produce
    /// erroneous results.
    /// 
    /// From http://www.dotnetperls.com/alphanumeric-sorting
    /// </summary>
    public class AlphaNumericComparator : IComparer
    {
        public int Compare(object x, object y)
        {
            string s1 = x as string;
            if (s1 == null)
            {
                return 0;
            }

            string s2 = y as string;
            if (s2 == null)
            {
                return 0;
            }

            // Modification: The original comparer does not like
            // strings with leading zeros in it.
            // Therefore, we'll remove them from both strings.
            s1 = s1.TrimStart('0');
            s2 = s2.TrimStart('0');

            int len1 = s1.Length;
            int len2 = s2.Length;
            int marker1 = 0;
            int marker2 = 0;

            // Walk through two the strings with two markers.
            while (marker1 < len1 && marker2 < len2)
            {
                char ch1 = s1[marker1];
                char ch2 = s2[marker2];

                // Some buffers we can build up characters in for each chunk.
                char[] space1 = new char[len1];
                int loc1 = 0;
                char[] space2 = new char[len2];
                int loc2 = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    space1[loc1++] = ch1;
                    marker1++;

                    if (marker1 < len1)
                    {
                        ch1 = s1[marker1];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                do
                {
                    space2[loc2++] = ch2;
                    marker2++;

                    if (marker2 < len2)
                    {
                        ch2 = s2[marker2];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                string str1 = new string(space1);
                string str2 = new string(space2);

                int result;

                if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                {
                    int thisNumericChunk = int.Parse(str1);
                    int thatNumericChunk = int.Parse(str2);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                {
                    result = str1.CompareTo(str2);
                }

                if (result != 0)
                {
                    return result;
                }
            }
            return len1 - len2;
        }
    }

    public class CodeComparer : IComparer<string>
    {
        public char Separator { get; private set; }

        public CodeComparer() : this('-')
        {
                
        }

        public CodeComparer(char separator)
        {
            this.Separator = separator;
        }

        /// <summary>
        /// Compares two codes which have the same number
        /// of elements, delimited by the separator used to
        /// instantiate this comparer.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(string x, string y)
        {
            var comp = new AlphaNumericComparator();

            var xCodeChunks = x.Split(Separator);
            var yCodeChunks = y.Split(Separator);

            if (xCodeChunks.Count() != yCodeChunks.Count())
            {
                throw new InvalidOperationException(
                    String.Format("Codes must contain the same number of elements. However, one has {0} and the other has {1}.", xCodeChunks.Count(), yCodeChunks.Count()));
            }

            for (int i = 0; i < xCodeChunks.Count(); i++)
            {
                var result = comp.Compare(xCodeChunks[i], yCodeChunks[i]);

                if (result != 0)
                {
                    return result;
                }
            }

            return 0;
        }
    }
}