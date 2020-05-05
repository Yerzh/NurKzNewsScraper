using System;
using ScrapySharp;
using HtmlAgilityPack;
using ScrapySharp.Network;
using System.Collections.Generic;
using ScrapySharp.Extensions;

namespace NurKzNewsScraper
{
    class Program
    {
        private static ScrapingBrowser _scrapingBrowser = new ScrapingBrowser();

        static void Main(string[] args)
        {
            var popularNews = GetPopularNewsLinks("https://www.nur.kz/");
        }

        private static HtmlNode GetHtml(string url)
        {
            WebPage webPage = _scrapingBrowser.NavigateToPage(new Uri(url));
            return webPage.Html;
        }

        private static List<string> GetPopularNewsLinks(string url)
        {
            var popularNewsLinks = new List<string>();
            var html = GetHtml(url);
            var links = html.CssSelect("div.block-top-latest a");
             
            foreach(var link in links)
            {
                if (link.Attributes["href"].Value.Contains(".html"))
                {
                    popularNewsLinks.Add(link.Attributes["href"].Value);
                }
            }
            return popularNewsLinks;
        }

        private static List<PageDetail> GetPageDetails(List<string> urls)
        {
            var pageDetails = new List<PageDetail>();
            foreach(var url in urls)
            {
                var htmlNode = GetHtml(url);
                var detail = new PageDetail();

            }
            return pageDetails;
        }
    }
}
