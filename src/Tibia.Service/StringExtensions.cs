using System.Text;

namespace Tibia.Service
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Repeats the specified string <paramref name="n" /> times.
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <param name="n">The number of times input string should be repeated.</param>
        /// <returns>The specified string repeated <paramref name="n" /> times.</returns>
        public static string Repeat(this string str, int n)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (n <= 0)
                return str;

            StringBuilder result = new StringBuilder(str.Length * n);
            return result.Insert(0, str, n).ToString();
        }
    }
}