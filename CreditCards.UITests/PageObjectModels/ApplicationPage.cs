
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CreditCards.UITests.PageObjectModels
{
    class ApplicationPage
    {
        private readonly IWebDriver Driver;
        private const string applicationUrl = "http://localhost:44108/Apply";
        private const string applicationTitle = "Credit Card Application - Credit Cards";
        public ApplicationPage(IWebDriver driver)
        {
            Driver = driver;
        }

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
