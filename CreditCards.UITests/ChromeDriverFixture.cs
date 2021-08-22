using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
    

namespace CreditCards.UITests
{
    public sealed class ChromeDriverFixture: IDisposable
    {
        public ChromeDriverFixture()
        {
            Driver = new ChromeDriver(".");
        }

        public IWebDriver Driver { get; private set; }

        public void Dispose()
        {
            Driver.Dispose();
        }

    }
}
