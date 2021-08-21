
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit.Abstractions;

namespace CreditCards.UITests.PageObjectModels
{
    class ApplicationCompletePage
    {
        private readonly IWebDriver Driver;
        private const string applicationUrl = "http://localhost:44108/Apply";
        private const string applicationTitle = "Application Complete - Credit Cards";


        public ApplicationCompletePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public string decision => Driver.FindElement(By.Id("Decision")).Text;
        public string referenceNumber => Driver.FindElement(By.Id("ReferenceNumber")).Text;
        public string fullName => Driver.FindElement(By.Id("FullName")).Text;
        public string income=> Driver.FindElement(By.Id("Income")).Text;
        public string status=> Driver.FindElement(By.Id("RelationshipStatus")).Text;
        public string source => Driver.FindElement(By.Id("BusinessSource")).Text;
        public string age => Driver.FindElement(By.Id("Age")).Text;

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(applicationUrl);
            EnsurePageLoads();
        }
        public void EnsurePageLoads(bool isCheckStart = true)
        {
            bool urlIsCorrect;
            if (isCheckStart)
            {
                urlIsCorrect = Driver.Url.StartsWith(applicationUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == applicationUrl;
            }

            bool isLoaded = (Driver.Title == applicationTitle) && urlIsCorrect;
            if (!isLoaded)
            {
                throw new Exception($"Failed to load Page. Page URL is '{Driver.Url}' and Page Source:\r\n{Driver.PageSource}");
            }
        }

        


       
    }
}
