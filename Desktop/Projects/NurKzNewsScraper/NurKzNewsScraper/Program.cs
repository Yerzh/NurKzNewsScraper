using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NurKzNewsScraper
{
    class Program
    {
        private static ScrapingBrowser _scrapingBrowser = new ScrapingBrowser();

        static void Main(string[] args)
        {
            var popularNews = GetPopularNewsLinks("https://www.nur.kz/");
            var newsDetails = GetPageDetails(popularNews);

            foreach(var detail in newsDetails)
            {
                Console.WriteLine(detail.Title);
                Console.WriteLine(detail.Description);
                Console.WriteLine("\n\n");
            }
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
                detail.Title = htmlNode.CssSelect("div.fb-quotable h1").Single().InnerText;
                detail.Description = htmlNode.CssSelect("p.align-left strong").First().InnerText;
                detail.Url = url;
                pageDetails.Add(detail);
            }
            return pageDetails;
        }
    }
}