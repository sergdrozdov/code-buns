using CodeBuns.Dotnet.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeBuns.Dotnet.Common
{
    public static class TextUtils
    {
        public static string ClearText(string sourceText)
        {
            if (string.IsNullOrWhiteSpace(sourceText))
                return string.Empty;

            var text = sourceText.DeleteHTMLTags();
            text = text.Replace(Environment.NewLine, " ")
                       .Replace("\n", " ")
                       .Replace("\r", " ")
                       .Replace("\"", "")
                       .Replace("\t", " ");

            // Remove all non-printable characters
            text = Regex.Replace(text, @"[ ]+", " ");

            // Remove multiple spaces and trim the result
            text = Regex.Replace(text, @"\s+", " ").Trim();

            return text;
        }


    }
}
