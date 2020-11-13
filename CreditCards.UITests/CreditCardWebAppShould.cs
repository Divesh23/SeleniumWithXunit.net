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
				//IWebElement initialContext = driver.FindElement(By.Id("GenerationToken"));
                string initialTokenText = driver.FindElement(By.Id("GenerationToken")).Text;
				DemoHelper.Pause();
				driver.Navigate().Refresh();
				string refreshTokenText = driver.FindElement(By.Id("GenerationToken")).Text;
				Assert.Equal(HomePageTitle, driver.Title);
				Assert.Equal(HomePageUrl, driver.Url);
				Assert.NotEqual(initialTokenText, refreshTokenText);
			}
		
		}

		[Fact]
		[Trait("Category","Smoke")]
		public void ReloadPageOnBack()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				driver.Navigate().GoToUrl(HomePageUrl);
				string initalTokenText = driver.FindElement(By.Id("GenerationToken")).Text;
				DemoHelper.Pause();
				driver.Navigate().GoToUrl(AboutPage);
				DemoHelper.Pause();
				driver.Navigate().Back();
				string refreshTokenText = driver.FindElement(By.Id("GenerationToken")).Text;
				DemoHelper.Pause();
				Assert.Equal(HomePageTitle, driver.Title);
				Assert.Equal(HomePageUrl, driver.Url);
				Assert.NotEqual(initalTokenText, refreshTokenText);
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

        [Fact]
		[Trait("Category", "Smoke")]

		public void DisplayProductsByHtml()
		{
			using (IWebDriver driver = new ChromeDriver(".")) 
			{
				driver.Navigate().GoToUrl(HomePageUrl);
				string firstProductName = driver.FindElement(By.TagName("td")).Text;
				Assert.Equal("Easy Credit Card", firstProductName);
			}
		}

	}

		
}
