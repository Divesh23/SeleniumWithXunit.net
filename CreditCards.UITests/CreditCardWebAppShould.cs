using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.ComponentModel;

namespace CreditCards.UITests
{
	public class CreditCardWebAppShould
	{
		private const string HomePageUrl = "http://localhost:44108/";
		private const string HomePageTitle = "Home Page - Credit Cards";
		private const string AboutPage = "http://localhost:44108/Home/About";
		private const string AboutPageTitle = "About - Credit Cards";

		[Fact]
		[Trait("Category", "Smoke")]
		public void LoadApplicationPage()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				
				driver.Navigate().GoToUrl(HomePageUrl);
				DemoHelper.Pause(3000);
				Assert.Equal(HomePageTitle, driver.Title);
				Assert.Equal(HomePageUrl, driver.Url);
			}

		}

		[Fact]
		[Trait("Category", "Smoke")]

		public void ReloadPage() 
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				driver.Navigate().GoToUrl(HomePageUrl);
				DemoHelper.Pause();
				driver.Navigate().Refresh();
				Assert.Equal(HomePageTitle, driver.Title);
				Assert.Equal(HomePageUrl, driver.Url);
			}
		
		}

		[Fact]
		[Trait("Category","Smoke")]
		public void ReloadPageOnBack()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				driver.Navigate().GoToUrl(HomePageUrl);
				DemoHelper.Pause();
				driver.Navigate().GoToUrl(AboutPage);
				DemoHelper.Pause();
				driver.Navigate().Back();
				DemoHelper.Pause();
				Assert.Equal(HomePageTitle, driver.Title);
				Assert.Equal(HomePageUrl, driver.Url);
			}
		}

		[Fact]
		[Trait("Category", "Smoke")]
		public void ReloadPageOnForward()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				driver.Navigate().GoToUrl(HomePageUrl);
				DemoHelper.Pause();
				driver.Navigate().GoToUrl(AboutPage);
				DemoHelper.Pause();
				driver.Navigate().Back();
				DemoHelper.Pause();
				driver.Navigate().Forward();
				DemoHelper.Pause();
				Assert.Equal(AboutPage,driver.Url);
				Assert.Equal(AboutPageTitle,driver.Title);
			}
		
		}

	}
}
