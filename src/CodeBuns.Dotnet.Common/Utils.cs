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

        public static bool IsCompleteIpAddress(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return false;

            if (!System.Net.IPAddress.TryParse(ipAddress, out var address))
                return false;

            // check if IPv4
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                var octets = ipAddress.Split('.').Select(int.Parse).ToArray();
                return octets.Length == 4 && octets[3] != 0;
            }

            // check if IPv6
            return address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
        }

        public static bool IsIpAddress(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return System.Net.IPAddress.TryParse(input, out _);
        }

        public static bool IsStringCanBeIpAddress(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                return false;

            var octets = ipAddress.Split('.');
            if (octets.Length < 2 || octets.Length > 4)
                return false;

            foreach (var octet in octets)
            {
                if (!int.TryParse(octet, out int value))
                    return false;
                if (value < 0 || value > 255)
                    return false;
            }

            return true;
        }

        public static string GetIpThreeOctets(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return "";

            var items = ipAddress.Split('.');
            if (items.Length < 3)
                return "";

            return $"{items[0]}.{items[1]}.{items[2]}";
        }

        public static string GetIpTwoOctets(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return "";

            var items = ipAddress.Split('.');
            if (items.Length < 2)
                return "";

            return $"{items[0]}.{items[1]}";
        }
    }
}
