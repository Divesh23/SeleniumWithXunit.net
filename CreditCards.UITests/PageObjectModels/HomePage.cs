
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CreditCards.UITests.PageObjectModels
{
    class HomePage
    {
        private readonly IWebDriver Driver;
        public bool isCookiePresent => Driver.FindElements(By.Id("CookiesBeingUsed")).Any();
        private const string homeUrl = "http://localhost:44108/";
        private const string homeTitle = "Home Page - Credit Cards";
        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        //By using IWebElements
        /*public ReadOnlyCollection<IWebElement> ProductCells
        {
            get
            {
                return Driver.FindElements(By.TagName("td"));
            }
        }*/

        //By using Lists
        public ReadOnlyCollection<(string name, string interestRate)> Products
        {
            get 
            {
                var products = new List<(string name, string interestRate)>();
                var productCells = Driver.FindElements(By.TagName("td"));

                for (int i=0;i<=productCells.Count-1;i+=2)
                {
                    string name = productCells[i].Text;
                    string interestRate = productCells[i + 1].Text;
                    products.Add((name,interestRate));
                }

                return products.AsReadOnly();
            }        
        }

        public string GenerateToken => Driver.FindElement(By.Id("GenerationToken")).Text;

        public void  NavigateTo()
        {
            Driver.Navigate().GoToUrl(homeUrl);
            EnsurePageLoads();
        }
        public void EnsurePageLoads(bool isCheckStart=true)
        {
            bool urlIsCorrect;
            if (isCheckStart)
            {
                urlIsCorrect = Driver.Url.StartsWith(homeUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == homeUrl;        
            }
            
            bool isLoaded = (Driver.Title == homeTitle) && urlIsCorrect;
            if (!isLoaded)
            {
                throw new Exception($"Failed to load Page. Page URL is '{Driver.Url}' and Page Source:\r\n{Driver.PageSource}");
            }
        }

        public void ClickContactFooterLink() => Driver.FindElement(By.Id("ContactFooter")).Click();

        public void ClickLiveChatLink() => Driver.FindElement(By.Id("LiveChat")).Click();

        public void ClickLearnAboutUsLink() => Driver.FindElement(By.Id("LearnAboutUs")).Click();
    }
}
