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

        public static string MaskEmailAddress(string email, int visibleStart = 4, int visibleEnd = 2)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email format.");

            var atIndex = email.IndexOf('@');
            var localPart = email.Substring(0, atIndex);
            var domainPart = email.Substring(atIndex);

            // If the local part is shorter than the visible characters, mask the whole part
            if (localPart.Length <= visibleStart + visibleEnd)
                return new string('*', localPart.Length) + domainPart;

            // Leave the first 'visibleStart' characters and the last 'visibleEnd' characters visible
            var start = localPart.Substring(0, visibleStart);
            var end = localPart.Substring(localPart.Length - visibleEnd);

            // Mask the middle part
            var maskedMiddle = new string('*', localPart.Length - visibleStart - visibleEnd);

            return $"{start}{maskedMiddle}{end}{domainPart}";
        }
    }
}
