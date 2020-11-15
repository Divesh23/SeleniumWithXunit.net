using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.ComponentModel;
using System.Collections.ObjectModel;

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
		public void BrowserManipulation()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{

				driver.Navigate().GoToUrl(HomePageUrl);
				DemoHelper.Pause(3000);
				driver.Manage().Window.Maximize();
				DemoHelper.Pause(3000);
				driver.Manage().Window.Minimize();
				DemoHelper.Pause(3000);
				driver.Manage().Window.Size = new System.Drawing.Size(300,400);
				DemoHelper.Pause(3000);
				driver.Manage().Window.Position = new System.Drawing.Point(1,1);
				DemoHelper.Pause(3000);
				driver.Manage().Window.Position = new System.Drawing.Point(100,100);
				DemoHelper.Pause(3000);
				driver.Manage().Window.Position = new System.Drawing.Point(200,200);
				DemoHelper.Pause(3000);
				driver.Manage().Window.Maximize();
				DemoHelper.Pause(3000);
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

		[Fact]
		[Trait("Category", "Smoke")]

		public void DisplayMultipleProductsByHtml()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				driver.Navigate().GoToUrl(HomePageUrl);
				ReadOnlyCollection<IWebElement> productelements = driver.FindElements(By.TagName("td"));
				Assert.Equal("Easy Credit Card", productelements[0].Text);
				Assert.Equal("20% APR", productelements[1].Text);
				Assert.Equal("Silver Credit Card", productelements[2].Text);
				Assert.Equal("18% APR",productelements[3].Text);
				Assert.Equal("Gold Credit Card", productelements[4].Text);
				Assert.Equal("17% APR", productelements[5].Text);
				
			}
		}

	}

		
}
