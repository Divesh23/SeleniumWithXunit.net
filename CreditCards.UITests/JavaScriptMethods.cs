using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    public class JavaScriptTests
    {
        private static string JavaURL = "http://localhost:44108/JSOverlay.html";

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadPageUsingJS()
        {
            using (IWebDriver driver = new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(JavaURL);
                //Before Using JS the script fails due to an intercept
                //driver.FindElement(By.Id("HiddenLink"));

                //After using JS
                string script = "document.getElementById('HiddenLink').click();";
                //string PSTitle = "Home | Pluralsight";
                string PSURL = "https://www.pluralsight.com/";
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);
                Assert.Equal(PSURL, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Advanced")]
        public void ReturnStringJS()
        {
            using(IWebDriver driver =new ChromeDriver("."))
            {
                driver.Navigate().GoToUrl(JavaURL);
                string script = "return document.getElementById('HiddenLink').innerHTML;";
                IJavaScriptExecutor js =(IJavaScriptExecutor) driver;
                string innerHTML =(string) js.ExecuteScript(script);
                Assert.Equal("Go to Pluralsight", innerHTML);
            }
        }
    }
}
