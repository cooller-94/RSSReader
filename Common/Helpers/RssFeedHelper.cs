using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
    public static class RssFeedHelper
    {
        public static DateTime? ConvertRssDateStringToDateTime(string rssDateString)
        {
            if (string.IsNullOrEmpty(rssDateString))
            {
                return null;
            }

            if (DateTime.TryParse(rssDateString, out DateTime result))
            {
                return result;
            }

            return null;
        }

        public static string EncryptPost(string title, string description, string url)
        {
            string resultString = (title?.ToLower()?.Trim() ?? string.Empty) + (description?.ToLower()?.Trim() ?? string.Empty) + (url?.ToLower()?.Trim() ?? string.Empty);

            HashAlgorithm algorithm = SHA256.Create();

            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(GetComputedString(title, description, url)));

            return BitConverter.ToString(hash);
        }

        private static string GetComputedString(string title, string description, string url)
        {
            return (title?.ToLower()?.Trim() ?? string.Empty) + (description?.ToLower()?.Trim() ?? string.Empty) + (url?.ToLower()?.Trim() ?? string.Empty);
        }
    }
}
