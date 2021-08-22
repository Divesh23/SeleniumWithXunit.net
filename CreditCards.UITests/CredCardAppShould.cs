﻿using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    public class CredCardAppShould : IClassFixture<ChromeDriverFixture>
    {
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private const string HomePageUrl = "http://localhost:44108/";
        private const string CreditCardPageTile = "Credit Card Application - Credit Cards";

        public ChromeDriverFixture ChromeDriverFixture;

      // private readonly ITestOutputHelper output;

     /* public CredCardAppShould(ITestOutputHelper output)
        {
            this.output = output;
        }*/

        public CredCardAppShould(ChromeDriverFixture chromeDriverFixture)
        {
            ChromeDriverFixture = chromeDriverFixture;
            ChromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
            ChromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");
        }


        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageOnClickingApplyNow()
        {
            var homepage = new HomePage(ChromeDriverFixture.Driver);
            homepage.NavigateTo();
            ApplicationPage applicationPage = homepage.clickApplyNowButton();
            applicationPage.EnsurePageLoads();
            
        }

        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageWithLinkText()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();
                IWebElement next = driver.FindElement(By.CssSelector("[data-slide='next']"));
                next.Click();
                DemoHelper.Pause();//wait time for carasoul
                IWebElement clickApplyNowButton = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                clickApplyNowButton.Click();
                DemoHelper.Pause();
                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(CreditCardPageTile, driver.Title);


            }
        }

        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageWithClassName()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                DemoHelper.Pause();
                IWebElement next = driver.FindElement(By.CssSelector("[data-slide='next']"));
                next.Click();
                DemoHelper.Pause();//wait time for carasoul
                next.Click();
                DemoHelper.Pause();//wait time for carasoul

                IWebElement clickApplyNowButton = driver.FindElement(By.ClassName("customer-service-apply-now"));
                clickApplyNowButton.Click();
                DemoHelper.Pause();
                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(CreditCardPageTile, driver.Title);
            }

        }

        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageWithPartialLinkText()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                driver.FindElement(By.PartialLinkText("- Apply Now!")).Click();
                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(CreditCardPageTile, driver.Title);
            }
        }

        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageWithAbsoluteXpath()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a")).Click();
                Assert.Equal(CreditCardPageTile, driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }

        }

        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageWithRelativeXpath()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                driver.FindElement(By.XPath(".//a[text()[contains(., '- Apply Now')]]")).Click();
                Assert.Equal(CreditCardPageTile, driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }


        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageWithExplicitWait()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                IWebElement clickLink = wait.Until((d) => d.FindElement(By.Name("ApplyLowRate")));
                clickLink.Click();
                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(CreditCardPageTile, driver.Title);

            }
        }

        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageWithPreBuiltFunctions()
        {
                var homepage = new HomePage(ChromeDriverFixture.Driver);
                homepage.NavigateTo();
                ApplicationPage applicationPage = homepage.clickApplyLowRateButton();
                applicationPage.EnsurePageLoads();
            
        }

        

        [Fact]
        [Trait("Category", "Application")]
        public void ExplicitWaitsWithPreBuiltFunctions()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));
                IWebElement clickApply = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("customer-service-apply-now")));
                clickApply.Click();
            }
        }

        [Fact]
        [Trait("Category", "Application")]
        public void EnterValuesForForm()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                const string fname = "Divesh";
                const string lname = "David";
                const string flyerNumber = "1234-T";
                const string age = "30";
                const string income = "100000";
                const string status = "Single";
                const string option = "Word of Mouth";
                const string decision = "AutoAccepted";
                var applicationPage = new ApplicationPage(driver);
                applicationPage.NavigateTo();


                applicationPage.EnterFirstName(fname);
                applicationPage.EnterLastName(lname);
                applicationPage.EnterFlyerNumber(flyerNumber);
                applicationPage.EnterAge(age);
                applicationPage.EnterGrossAnnualIncome(income);
                applicationPage.EnterStatus(status);
                applicationPage.SelectOption(option);
                applicationPage.AcceptTerms();
                applicationPage.SubmitForm();

                var applicationCompletePage = new ApplicationCompletePage(driver);
                applicationCompletePage.EnsurePageLoads();
                Assert.Equal(decision, applicationCompletePage.decision);
                Assert.NotEmpty(applicationCompletePage.referenceNumber);
                Assert.Equal($"{fname} {lname}", applicationCompletePage.fullName);
                Assert.Equal(age, applicationCompletePage.age);
                Assert.Equal(income, applicationCompletePage.income);
                Assert.Equal(status, applicationCompletePage.status);
                Assert.Equal(option, applicationCompletePage.source);

                //To View All Options in select 
                /*foreach (IWebElement options in eachValue.Options )
                {
                    output.WriteLine($"Value:{options.GetAttribute("value")} Text:{options.Text}");
                
                }
                Assert.Equal("I'd Rather Not Say",eachValue.SelectedOption.Text);
                Assert.Equal(5,eachValue.Options.Count);
                //Selecting the option by value
                eachValue.SelectByValue("Internet");
                DemoHelper.Pause();
                //Selecting the option by Text
                eachValue.SelectByText("Word of Mouth");
                DemoHelper.Pause();
                //Selecting the option by Index
                eachValue.SelectByIndex(4);
                DemoHelper.Pause();
                driver.FindElement(By.Id("TermsAccepted")).Click();
                DemoHelper.Pause();
                //Submit by click
                //driver.FindElement(By.Id("SubmitApplication")).Click();
                //Submit by function
                //driver.FindElement(By.Id("Single")).Submit();*/


                /* Assert.Equal("Application Complete - Credit Cards", driver.Title);
                 Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                 Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                 Assert.Equal("Divesh David", driver.FindElement(By.Id("FullName")).Text);
                 Assert.Equal("30", driver.FindElement(By.Id("Age")).Text);
                 Assert.Equal("85000", driver.FindElement(By.Id("Income")).Text);
                 Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                 Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);*/

            }
        }

        [Fact]
        [Trait("Category", "Application")]
        public void FixErrorsOnForm()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {

                const string fname = "Divesh";
                const string lname = "David";
                const string flyerNumber = "1234-T";
                const string age = "30";
                const string invalidage = "17";
                const string income = "100000";
                const string status = "Single";
                const string option = "Word of Mouth";
                const string decision = "AutoAccepted";
                var applicationPage = new ApplicationPage(driver);
                applicationPage.NavigateTo();


                applicationPage.EnterFirstName(fname);
                //applicationPage.EnterLastName(lname);
                applicationPage.EnterFlyerNumber(flyerNumber);
                applicationPage.EnterAge(invalidage);
                applicationPage.EnterGrossAnnualIncome(income);
                applicationPage.EnterStatus(status);
                applicationPage.SelectOption(option);
                applicationPage.AcceptTerms();
                applicationPage.SubmitForm();

                Assert.Equal(2, applicationPage.ValidationErrorMessages.Count);
                Assert.Contains("Please provide a last name",applicationPage.ValidationErrorMessages);
                Assert.Contains("You must be at least 18 years old", applicationPage.ValidationErrorMessages);


                //find out validation errors
                /*var vaidationErrors = driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));
                Assert.Equal(2, vaidationErrors.Count);
                Assert.Equal("Please provide a last name", vaidationErrors[0].Text);
                Assert.Equal("You must be at least 18 years old", vaidationErrors[1].Text);*/

                //fix validation errors
                applicationPage.EnterLastName(lname);
                applicationPage.clearAge();
                applicationPage.EnterAge(age);
                ApplicationCompletePage applicationCompletePage=applicationPage.SubmitForm();

                //Asserts
                applicationCompletePage.EnsurePageLoads();
            }
        }
    
        
    
    }
}
