using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeBuns.Dotnet.Common.Extensions
{
    public static class ExtensionMethods
    {
        public static string ToLocalDirFormat(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            var formattedPath = str.Replace('/', Path.DirectorySeparatorChar)
                                   .Replace('\\', Path.DirectorySeparatorChar);

            while (formattedPath.Contains($"{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}"))
            {
                formattedPath = formattedPath.Replace($"{Path.DirectorySeparatorChar}{Path.DirectorySeparatorChar}", $"{Path.DirectorySeparatorChar}");
            }

            return formattedPath;
        }

        public static string ToWebDirFormat(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            var formattedPath = str.Replace('\\', Path.AltDirectorySeparatorChar)
                                   .Replace('/', Path.AltDirectorySeparatorChar);

            while (formattedPath.Contains($"{Path.AltDirectorySeparatorChar}{Path.AltDirectorySeparatorChar}"))
            {
                formattedPath = formattedPath.Replace($"{Path.AltDirectorySeparatorChar}{Path.AltDirectorySeparatorChar}", $"{Path.AltDirectorySeparatorChar}");
            }

            return formattedPath;
        }

        public static int WordsCount(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return 0;

            // Use regex to match words with at least 3 characters
            var words = Regex.Matches(text, @"\b\w{3,}\b");

            return words.Count;
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[(int)(DateTime.Now.Ticks % list.Count)];
        }

        public static string MakeCapitalize(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.Trim();

            if (text.Length == 1)
                return text.ToUpper(CultureInfo.CurrentCulture);

            return char.ToUpper(text[0], CultureInfo.CurrentCulture) + text.Substring(1);
        }

        public static string RemoveDuplicateCharacter(this string text, char symbol = ' ')
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var sb = new StringBuilder();
            char? previousChar = null;

            foreach (var currentChar in text)
            {
                if (currentChar == symbol && previousChar == symbol)
                    continue;

                sb.Append(currentChar);
                previousChar = currentChar;
            }

            return sb.ToString();
        }

        public static string ObjectProperties(this object obj, int indentLevel = 0)
        {
            if (obj == null)
                return string.Empty;

            var sb = new StringBuilder();
            var indent = new string(' ', indentLevel * 4);
            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                try
                {
                    var value = prop.GetValue(obj, null);
                    sb.AppendLine($"{indent}{prop.Name}: {value}");

                    if (value != null && !(value is string) && !(value is ValueType))
                    {
                        if (value is ICollection collectionItems)
                        {
                            sb.AppendLine($"{indent}{prop.Name} (Collection):");
                            foreach (var item in collectionItems)
                            {
                                sb.AppendLine(ObjectProperties(item, indentLevel + 1));
                            }
                        }
                        else
                        {
                            sb.AppendLine(ObjectProperties(value, indentLevel + 1));
                        }
                    }
                }
                catch (Exception ex)
                {
                    sb.AppendLine($"{indent}{prop.Name}: Error retrieving value ({ex.Message})");
                }
            }

            return sb.ToString();
        }
    }
}
