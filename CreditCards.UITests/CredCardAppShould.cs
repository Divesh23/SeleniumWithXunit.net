using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    public class CredCardAppShould
    {
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private const string HomePageUrl = "http://localhost:44108/";
        private const string CreditCardPageTile = "Credit Card Application - Credit Cards";
        private readonly ITestOutputHelper output;

        public CredCardAppShould(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageOnClickingApplyNow()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                var homepage = new HomePage(driver);
                homepage.NavigateTo();
                ApplicationPage applicationPage = homepage.clickApplyNowButton();
                applicationPage.EnsurePageLoads();
            }
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
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                IWebElement clickApplyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("ApplyLowRate")));
            }
        }

        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageWithImplicitWaitFailingTest()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                try
                {
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting Implicit Wait");
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigate to Home Page");
                    driver.Navigate().GoToUrl(HomePageUrl);
                    DemoHelper.Pause();
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding Element");
                    IWebElement clickApplyNowButton = driver.FindElement(By.ClassName("customer-service-apply-now"));
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found Element Displayed:{clickApplyNowButton.Displayed} and Enabled:{clickApplyNowButton.Enabled}");
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking Element");
                    clickApplyNowButton.Click();
                    DemoHelper.Pause();
                }
                catch (Exception e)
                {
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} {e} Reason for Failure: Button is still not Enabled or displayed");
                }

            }

        }

        [Fact]
        [Trait("Category", "Application")]
        public void ExplicitWaitsUnreachableElementFailingTest()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                try
                {
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigate to Home Page");
                    driver.Navigate().GoToUrl(HomePageUrl);
                    DemoHelper.Pause();
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding Element");
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));
                    IWebElement clickApplyNowButton = wait.Until((d) => d.FindElement(By.ClassName("customer-service-apply-now")));

                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found Element Displayed:{clickApplyNowButton.Displayed} and Enabled:{clickApplyNowButton.Enabled}");
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking Element");
                    clickApplyNowButton.Click();
                    DemoHelper.Pause();
                    Assert.Equal(ApplyUrl, driver.Url);
                    Assert.Equal(CreditCardPageTile, driver.Title);
                }
                catch (Exception e)
                {
                    output.WriteLine($"{e} Reason for Failure: Button is still not Enabled or displayed");
                }
            }

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
                driver.Navigate().GoToUrl(ApplyUrl);
                driver.FindElement(By.Id("FirstName")).SendKeys("Divesh");
                driver.FindElement(By.Id("LastName")).SendKeys("David");
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("1234-T");
                driver.FindElement(By.Name("Age")).SendKeys("30");
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("85000");
                driver.FindElement(By.Id("Single")).Click(); 
                DemoHelper.Pause(5000);
                IWebElement mainOption = driver.FindElement(By.Id("BusinessSource"));
                SelectElement eachValue = new SelectElement(mainOption);
                //To View All Options 
                foreach (IWebElement options in eachValue.Options )
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
                //Submit by clcik
                //driver.FindElement(By.Id("SubmitApplication")).Click();
                //Submit by function
                driver.FindElement(By.Id("Single")).Submit();
                DemoHelper.Pause();

                Assert.Equal("Application Complete - Credit Cards", driver.Title);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("Divesh David", driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal("30", driver.FindElement(By.Id("Age")).Text);
                Assert.Equal("85000", driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);

            }
        }

        [Fact]
        [Trait("Category", "Application")]
        public void FixErrorsOnForm()
        {
            const String firstName = "Divesh";
            const String invalidAge = "12";
            const String validAge = "30";
            
            using (IWebDriver driver = new ChromeDriver("."))
            {
               
                driver.Navigate().GoToUrl(ApplyUrl);
                driver.FindElement(By.Id("FirstName")).SendKeys(firstName);                
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("1234-T");
                driver.FindElement(By.Name("Age")).SendKeys(invalidAge);
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("85000");
                driver.FindElement(By.Id("Single")).Click();
                DemoHelper.Pause(5000);
                IWebElement mainOption = driver.FindElement(By.Id("BusinessSource"));
                SelectElement eachValue = new SelectElement(mainOption);
                
                //To View All Options 
                foreach (IWebElement options in eachValue.Options)
                {
                    output.WriteLine($"Value:{options.GetAttribute("value")} Text:{options.Text}");

                }
                Assert.Equal("I'd Rather Not Say", eachValue.SelectedOption.Text);
                Assert.Equal(5, eachValue.Options.Count);
                eachValue.SelectByIndex(4);
                DemoHelper.Pause();

                driver.FindElement(By.Id("TermsAccepted")).Click();
                DemoHelper.Pause();
                driver.FindElement(By.Id("Single")).Submit();
                DemoHelper.Pause();

                //find out validation errors
                var vaidationErrors = driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));
                Assert.Equal(2, vaidationErrors.Count);
                Assert.Equal("Please provide a last name", vaidationErrors[0].Text);
                Assert.Equal("You must be at least 18 years old", vaidationErrors[1].Text);

                //fix validation errors
                driver.FindElement(By.Id("LastName")).SendKeys("David");
                driver.FindElement(By.Name("Age")).Clear();
                driver.FindElement(By.Name("Age")).SendKeys(validAge);
                driver.FindElement(By.Id("Single")).Submit();

                //Asserts
                Assert.Equal("Application Complete - Credit Cards", driver.Title);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("Divesh David", driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal(validAge, driver.FindElement(By.Id("Age")).Text);
                Assert.Equal("85000", driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);
              

            }
        }
    
        
    
    }
}
