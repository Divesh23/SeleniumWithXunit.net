using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class CredCardAppShould
    {
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private const string HomePageUrl = "http://localhost:44108/";
        private const string CreditCardPageTile = "Credit Card Application - Credit Cards";

        [Fact]
        [Trait("Category","Application")]
        public void VerifyPageOnClickingApplyNow()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(HomePageUrl);
                IWebElement clickApplyNowButton = driver.FindElement(By.Name("ApplyLowRate"));
                clickApplyNowButton.Click();
                DemoHelper.Pause();
                Assert.Equal(ApplyUrl,driver.Url);
                Assert.Equal(CreditCardPageTile, driver.Title);


            }
        }
        [Fact]
        [Trait("Category", "Application")]
        public void VerifyPageOnClickingEasyApplyNow()
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
        public void VerifyPageOnClicikingApplyNowCustomerService()
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
    }
}
