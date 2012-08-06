using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinkDev.Windows8.Data;

namespace LinkDev.Windows8.HTML
{
    public class HTMLUtility
    {
        /// <summary>
        /// Get's an HTML page and execute a regex to return a specific content
        /// </summary>
        /// <param name="url"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static async Task<string> GetContentFromHTML(string url, string regex)
        {
            WebRequest request = WebRequest.CreateHttp(url);
            WebResponse response = await request.GetResponseAsync();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            string html = reader.ReadToEnd().Replace("\r\n", " ");
            Match ma = Regex.Match(html, regex);
            if (ma.Success)
            {
                string content = ConvertHtmlToString(ma.Groups[1].Value);
                content = Regex.Replace(content, @"<[^>]*>", String.Empty);
                return content;
            }
            else
            {
                return null;
            }
        }

        public static string ConvertHtmlToString(string html)
        {
            return Windows.Data.Html.HtmlUtilities.ConvertToText(
                html.Replace("<p>", "")
                .Replace("</p>", "\r\n\r\n")
                .Replace("<br>", "\r\n"));           
        }
    }
}
