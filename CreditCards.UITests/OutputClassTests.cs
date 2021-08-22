using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    public class OutputClassTests 
    {
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private const string HomePageUrl = "http://localhost:44108/";
        private const string CreditCardPageTile = "Credit Card Application - Credit Cards";

        public ChromeDriverFixture ChromeDriverFixture;

        private readonly ITestOutputHelper output;

         public OutputClassTests(ITestOutputHelper output)
           {
               this.output = output;
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

                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding Element");
                    IWebElement clickApplyNowButton = ChromeDriverFixture.Driver.FindElement(By.ClassName("customer-service-apply-now"));
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found Element Displayed:{clickApplyNowButton.Displayed} and Enabled:{clickApplyNowButton.Enabled}");
                    output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking Element");
                    clickApplyNowButton.Click();

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


    }
}
