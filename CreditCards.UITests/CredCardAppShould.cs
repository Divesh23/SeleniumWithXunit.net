using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace CreditCards.UITests
{
    public class CredCardAppShould
    {
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private const string HomePageUrl = "http://localhost:44108/";
        private const string CreditCardPageTile = "Credit Card Application - Credit Cards";

        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageOnClickingApplyNow()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                IWebElement clickApplyNowButton = driver.FindElement(By.Name("ApplyLowRate"));
                clickApplyNowButton.Click();
                DemoHelper.Pause();
                Assert.Equal(ApplyUrl, driver.Url);
                Assert.Equal(CreditCardPageTile, driver.Title);


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



    }
}
