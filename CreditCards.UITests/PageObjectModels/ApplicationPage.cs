
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit.Abstractions;

namespace CreditCards.UITests.PageObjectModels
{
    class ApplicationPage
    {
        private readonly IWebDriver Driver;
        private const string applicationUrl = "http://localhost:44108/Apply";
        private const string applicationTitle = "Credit Card Application - Credit Cards";
        private readonly ITestOutputHelper output;
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

        public void EnterFirstName(string firstname) => Driver.FindElement(By.Id("FirstName")).SendKeys(firstname);


        public void EnterLastName(string lastname)=>Driver.FindElement(By.Id("LastName")).SendKeys(lastname);
        public void EnterFlyerNumber(string num)=>Driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys(num);
        public void EnterAge(string age)=>Driver.FindElement(By.Name("Age")).SendKeys(age);
        public void EnterGrossAnnualIncome(string income)=>Driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys(income);
        public void EnterStatus(string status)=>Driver.FindElement(By.Id(status)).Click();

        public void SelectOption(string value)
        {
            IWebElement mainOption = Driver.FindElement(By.Id("BusinessSource"));
            SelectElement source = new SelectElement(mainOption);
            source.SelectByText(value);

        }

        public void AcceptTerms()=> Driver.FindElement(By.Id("TermsAccepted")).Click();

        public ApplicationCompletePage SubmitForm() 
        { 
            Driver.FindElement(By.Id("Single")).Submit();
            return new ApplicationCompletePage(Driver);
        }

        public ReadOnlyCollection<string> ValidationErrorMessages
        {
            get
            {
                return Driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li")).Select(x => x.Text).ToList().AsReadOnly();

            }
            

        }

        public void clearAge()=> Driver.FindElement(By.Name("Age")).Clear();
    }
}
