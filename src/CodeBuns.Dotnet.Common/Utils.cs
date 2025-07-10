using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeBuns.Dotnet.Common
{
    public class Utils
    {
        public static async Task<string> GetUrlTitleAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return string.Empty;

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url.Trim());
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var regex = @"(?<=<title.*>)([\s\S]*?)(?=</title>)";
                        var extractor = new Regex(regex, RegexOptions.IgnoreCase);
                        var match = extractor.Match(content);

                        return match.Success ? match.Value.Trim() : string.Empty;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
