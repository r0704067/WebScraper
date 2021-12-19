using DevopsWebScraper.DAL;
using DevopsWebScraper.Models;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace DevopsWebScraper
{
    public class Program
    {
        internal static SortedSet<YouTubeVideo> youTubeVideos;


        static void Main(string[] args)
        {
            YouTubeSql youtubeSql = new YouTubeSql();
            youTubeVideos = new SortedSet<YouTubeVideo>(youtubeSql.GetYouTubeVideos());
            do
            {


                Console.WriteLine("=====================================================================================================================" + Environment.NewLine +
                    "======================================= Webscraper made by Pieter-Jan Vliegen =======================================" + Environment.NewLine +
                    "=====================================================================================================================" + Environment.NewLine);
                Thread.Sleep(500);

                Console.WriteLine("Choose a website to scrape" + Environment.NewLine + "A) YouTube" + Environment.NewLine + "B) Jobs" + Environment.NewLine + "C) Mx24");
                Thread.Sleep(500);

                string keuzeWebsite = Console.ReadLine().Substring(0,1).ToUpper();
                Thread.Sleep(500);

                while (string.IsNullOrEmpty(keuzeWebsite))
                {
                    Console.WriteLine("Choose a website to scrape" + Environment.NewLine + "1) YouTube" + Environment.NewLine + "2) Jobs" + Environment.NewLine + "3) Mx24");
                    Thread.Sleep(500);

                    Console.ReadLine();
                    Thread.Sleep(500);
                }
                if (keuzeWebsite == "A")
                {
                    Console.WriteLine("Enter trefwoord:");
                    Thread.Sleep(1000);

                    string trefwoord = Console.ReadLine();
                    Thread.Sleep(1000);



                    while (string.IsNullOrEmpty(trefwoord))
                    {

                        Console.WriteLine("Enter trefwoord:");
                        Thread.Sleep(500);

                        Console.ReadLine();
                        Thread.Sleep(500);

                    }

                    if (!string.IsNullOrEmpty(trefwoord))
                    {
                        //open chrome browser
                        ChromeDriver driver = new ChromeDriver(@"C:\Users\vlieg\Documents\devops\Project\DevopsWebScraper\DevopsWebScraper\Drivers\");

                        //maximize window size
                        driver.Manage().Window.Maximize();

                        //navigate to youtube
                        driver.Navigate().GoToUrl("https://youtube.com");

                        //click on agree
                        driver.FindElement(By.XPath("//*[@id='content']/div[2]/div[5]/div[2]/ytd-button-renderer[2]/a")).Click();
                        Thread.Sleep(2000);

                        //put trefwoord inside searchbar
                        driver.FindElement(By.XPath("/html/body/ytd-app/div/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/form/div[1]/div[1]/input")).SendKeys(trefwoord);
                        Thread.Sleep(5000);

                        //click search
                        driver.FindElement(By.XPath("/html/body/ytd-app/div/div/ytd-masthead/div[3]/div[2]/ytd-searchbox/button")).Click();
                        Thread.Sleep(2000);

                        //click filters
                        driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/div/ytd-toggle-button-renderer/a/tp-yt-paper-button")).Click();
                        Thread.Sleep(2000);

                        //click on recent upload
                        driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[5]/ytd-search-filter-renderer[2]/a/div/yt-formatted-string")).Click();
                        Thread.Sleep(2000);


                        // lijst met videos maken
                        
                        IList<IWebElement> videos = driver.FindElements(By.XPath("//*[@id='contents']/ytd-video-renderer/div[1]"));
                        Console.WriteLine("Total number of videos are " + videos.Count);
                        Thread.Sleep(2000);

                        for (int index = 0; index < 5; index++)
                        {
                            string videoTitle, videoViews, videoUploader;

                            IWebElement link = (IWebElement)videos[index].FindElement(By.Id("thumbnail"));
                            Thread.Sleep(500);
                            Console.WriteLine("Link: " + link.GetAttribute("href"));


                            IWebElement title = (IWebElement)videos[index].FindElement(By.CssSelector("#video-title"));
                            Thread.Sleep(500);
                            videoTitle = title.Text;
                            Console.WriteLine("title: " + videoTitle);

                            IWebElement uploader = (IWebElement)videos[index].FindElement(By.CssSelector("div[id='dismissible'] div div[id='channel-info'] ytd-channel-name a"));
                            Thread.Sleep(500);
                            videoUploader = uploader.Text;
                            Console.WriteLine("Uploader: " + videoUploader);

                            IWebElement views = (IWebElement)videos[index].FindElement(By.XPath(".//*[@id='metadata-line']/span[1]"));
                            Thread.Sleep(500);
                            videoViews = views.Text;
                            Console.WriteLine("views: " + videoViews);

                            YouTubeVideo vid = new YouTubeVideo(videoTitle, videoUploader, videoViews, link.GetAttribute("href"));

                            YouTubeSql.InsertYouTubeVideo(vid);
                            youTubeVideos.Add(vid);


                        }
                    }
                }
                else if (keuzeWebsite == "B")
                {


                    Console.WriteLine("Enter trefwoord:");
                    Thread.Sleep(1000);

                    string trefwoord = Console.ReadLine();
                    Thread.Sleep(1000);



                    while (string.IsNullOrEmpty(trefwoord))
                    {

                        Console.WriteLine("Enter trefwoord:");
                        Thread.Sleep(500);

                        Console.ReadLine();
                        Thread.Sleep(500);

                    }

                    if (!string.IsNullOrEmpty(trefwoord))
                    {
                        //open chrome browser
                        ChromeDriver driver = new ChromeDriver(@"C:\Users\vlieg\Documents\devops\Project\DevopsWebScraper\DevopsWebScraper\Drivers\");

                        //maximize window size
                        driver.Manage().Window.Maximize();

                        //navigate to youtube
                        driver.Navigate().GoToUrl("https://be.indeed.com/");
                        Thread.Sleep(5000);

                        // cookie voorwaarden accepteren
                        try
                        {
                            driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[1]/div/div[2]/div/button[2]")).Click();
                        }
                        catch { /*Do nothing*/}
                        Thread.Sleep(2000);

                        //put trefwoord inside searchbar
                        driver.FindElement(By.Id("text-input-what")).SendKeys(trefwoord);
                        Thread.Sleep(3000);

                        //click on find
                        try
                        {
                            driver.FindElement(By.CssSelector("#jobsearch > button")).Click();
                            Thread.Sleep(2000);

                        }
                        catch
                        {
                            driver.FindElement(By.CssSelector("#whatWhereFormId > div.icl-WhatWhere-buttonWrapper > button")).Click();
                            Thread.Sleep(2000);
                        }



                        //click on date posted
                        driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/div/div[2]/div/div[1]/button")).Click();
                        Thread.Sleep(2000);

                        //click on 3 days
                        driver.FindElement(By.CssSelector("#filter-dateposted-menu > li:nth-child(2) > a")).Click();
                        Thread.Sleep(2000);

                        Int64 last_height = (Int64)(((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight"));
                        while (true)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight);");
                            /* Wait to load page */
                            Thread.Sleep(2000);
                            /* Calculate new scroll height and compare with last scroll height */
                            Int64 new_height = (Int64)((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight");
                            if (new_height == last_height)
                                /* If heights are the same it will exit the function */
                                break;
                            last_height = new_height;
                        }
                        
                        IList<IWebElement> Jobs = driver.FindElements(By.XPath("//a[contains(@class, 'result')]"));
                        Console.WriteLine("Total number of jobs are " + Jobs.Count);
                        foreach (IWebElement job in Jobs)
                        {

                            IWebElement Title = job.FindElement(By.CssSelector("h2 > span"));
                            Console.WriteLine("Title: " + Title.GetAttribute("title"));
                            Thread.Sleep(2000);

                            IWebElement Bedrijf = job.FindElement(By.XPath("//div[1]/div/div[1]/div/table[1]/tbody/tr/td/div[2]/pre/span[1]"));
                            Console.WriteLine("Bedrijf: " + Bedrijf.GetAttribute("innerText"));
                            Thread.Sleep(2000);

                            IWebElement Locatie = job.FindElement(By.CssSelector("pre > div[class='companyLocation']"));
                            Console.WriteLine("Locatie: " + Locatie.GetAttribute("outerText"));
                            Thread.Sleep(2000);

                            string Link = job.GetAttribute("href");
                            Console.WriteLine("Link: " + Link);
                        }
                    }

                }

                else if (keuzeWebsite == "C")
                {
                    Console.WriteLine("Enter trefwoord:");
                    Thread.Sleep(1000);

                    string trefwoord = Console.ReadLine();
                    Thread.Sleep(1000);



                    while (string.IsNullOrEmpty(trefwoord))
                    {

                        Console.WriteLine("Enter trefwoord:");
                        Thread.Sleep(500);

                        Console.ReadLine();
                        Thread.Sleep(500);

                    }

                    if (!string.IsNullOrEmpty(trefwoord))
                    {

                        //open chrome browser
                        ChromeDriver driver = new ChromeDriver(@"C:\Users\vlieg\Documents\devops\Project\DevopsWebScraper\DevopsWebScraper\Drivers\");

                        //maximize window size
                        driver.Manage().Window.Maximize();

                        //navigate to youtube
                        driver.Navigate().GoToUrl("https://www.24mx.be/");
                        Thread.Sleep(5000);

                        //click op cookies
                        driver.FindElement(By.XPath("/html/body/app-root/div[3]/p-consent/div/div[1]/div[2]/div")).Click();
                        Thread.Sleep(3000);

                        //enter trefwoord inside search
                        driver.FindElement(By.TagName("input")).SendKeys(trefwoord + Keys.Enter);
                        Thread.Sleep(10000);

                        Int64 last_height = (Int64)(((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight"));
                        while (true)
                        {
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight);");
                            /* Wait to load page */
                            Thread.Sleep(2000);
                            /* Calculate new scroll height and compare with last scroll height */
                            Int64 new_height = (Int64)((IJavaScriptExecutor)driver).ExecuteScript("return document.documentElement.scrollHeight");
                            if (new_height == last_height)
                                /* If heights are the same it will exit the function */
                                break;
                            last_height = new_height;
                        }
                        Thread.Sleep(5000);

                        IList<IWebElement> Products = driver.FindElements(By.XPath("//p-search-products/div/div[2]/div[contains(@class, 'col--product-card')]"));
                        Console.WriteLine("Total number of products are " + Products.Count);
                        foreach (IWebElement product in Products)
                        {

                            IWebElement Title = product.FindElement(By.CssSelector("div[class='m-product-card-info__title ng-star-inserted']"));
                            Console.WriteLine("Product: " + Title.GetAttribute("innerText"));
                            Thread.Sleep(2000);

                            IWebElement Prijs = product.FindElement(By.CssSelector("div[class='m-product-card-info__container'] > div:nth-child(1)"));
                            Console.WriteLine("Prijs: " + Prijs.GetAttribute("innerText"));
                            Thread.Sleep(2000);



                            IWebElement Link = product.FindElement(By.TagName("a"));
                            Console.WriteLine("Link: " + Link.GetAttribute("href"));
                        }
                    }
                }



            } while (true);




        }
    }
}
            


        

        

    

    
