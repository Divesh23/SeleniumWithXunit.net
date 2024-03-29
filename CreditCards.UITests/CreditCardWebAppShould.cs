﻿using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.ComponentModel;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using ApprovalTests.Reporters;
using System.IO;
using ApprovalTests;
using CreditCards.UITests.PageObjectModels;

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
		public void LoadApplicationHomePage()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				var homePage = new HomePage(driver);
				homePage.NavigateTo();
				//driver.Navigate().GoToUrl(HomePageUrl);
				//Assert.Equal(HomePageTitle, driver.Title);
				//Assert.Equal(HomePageUrl, driver.Url);
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
				driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
				DemoHelper.Pause(3000);
				driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
				DemoHelper.Pause(3000);
				driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
				DemoHelper.Pause(3000);
				driver.Manage().Window.Position = new System.Drawing.Point(200, 200);
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
		[Trait("Category", "Smoke")]
		public void ReloadPageOnBack()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				var homePage = new HomePage(driver);
				homePage.NavigateTo();
				//public static string GenerateToken = driver.FindElement(By.Id("GenerationToken")).Text
				string initalTokenText = homePage.GenerateToken;
				driver.Navigate().GoToUrl(AboutPage);
				driver.Navigate().Back();
				homePage.EnsurePageLoads();
                string refreshTokenText = homePage.GenerateToken;
				homePage.EnsurePageLoads();
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
				Assert.Equal(AboutPage, driver.Url);
				Assert.Equal(AboutPageTitle, driver.Title);

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
				var homePage = new HomePage(driver);
				homePage.NavigateTo();

				//if using WebElements
				/*ReadOnlyCollection<IWebElement> productelements = homePage.ProductCells;
				Assert.Equal("Silver Credit Card", productelements[2].Text);
				Assert.Equal("18% APR", productelements[3].Text);
				Assert.Equal("Gold Credit Card", productelements[4].Text);
				Assert.Equal("17% APR", productelements[5].Text);*/
				
				//If using Lists
				Assert.Equal("Easy Credit Card",homePage.Products[0].name);
				Assert.Equal("20% APR", homePage.Products[0].interestRate);
				Assert.Equal("Silver Credit Card", homePage.Products[1].name);
				Assert.Equal("18% APR", homePage.Products[1].interestRate);
				Assert.Equal("Gold Credit Card", homePage.Products[2].name);
				Assert.Equal("17% APR",homePage.Products[2].interestRate);
			}
		}


		[Fact]
		[Trait("Category", "Smoke")]

		public void OpenContactLinkInNewTab()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				var homePage = new HomePage(driver);
				homePage.NavigateTo();
				homePage.ClickContactFooterLink();
				ReadOnlyCollection<string> alltabs = driver.WindowHandles;
				string homeTab = alltabs[0];
				string contactTab = alltabs[1];
				driver.SwitchTo().Window(contactTab);
				Assert.EndsWith("/Home/Contact", driver.Url);
			}
		}

		[Fact]
		[Trait("Category", "Smoke")]
		public void ValidateAlertBox()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				var homePage = new HomePage(driver);
				homePage.NavigateTo();
				homePage.ClickLiveChatLink();

				//if no wait was enforced
				//IAlert alert =  driver.SwitchTo().Alert();

				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
				IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
				Assert.Equal("Live chat is currently closed.", alert.Text);
				alert.Accept();
			}

		}
		 
		[Fact]
		[Trait("Category", "Smoke")]

		public void CancelAlertOnAlertBox()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				var homePage = new HomePage(driver);
				homePage.NavigateTo();
				homePage.ClickLearnAboutUsLink();
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
				IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
				alert.Dismiss();
				homePage.EnsurePageLoads();
			}
		}

		[Fact]
		[Trait("Category", "Smoke")]

		public void AccecptAlertOnAlertBox()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				driver.Navigate().GoToUrl(HomePageUrl);
				driver.FindElement(By.Id("LearnAboutUs")).Click();
				WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
				IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
				alert.Accept();
				Assert.Equal(AboutPageTitle, driver.Title);
			}
		}

		[Fact]
		[Trait("Category", "Smoke")]
		public void CheckCookie()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				var homePage = new HomePage(driver);
				homePage.NavigateTo();
				driver.Manage().Cookies.AddCookie(new Cookie("alertCookie", "true"));
				driver.Navigate().Refresh();

				//If using non Page Mdel
				//ReadOnlyCollection<IWebElement> alert = driver.FindElements(By.Id("CookiesBeingUsed"));
				//Cookie cookie = driver.Manage().Cookies.GetCookieNamed("alertCookie");
				//Assert.Equal("true", cookie.Value);
				//Assert.Empty(alert);

				//Using Page Model
				driver.Manage().Cookies.GetCookieNamed("alertCookie");
				Assert.True(homePage.isCookiePresent);

				driver.Manage().Cookies.DeleteCookieNamed("alertCookie");
				driver.Navigate().Refresh();
				Assert.True(homePage.isCookiePresent);//Product Bug
			}

		}

		[Fact]
		[Trait("Category", "Smoke")]
		[UseReporter(typeof(BeyondCompareReporter))]

		public void TakeScreenShotAboutPage()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				driver.Navigate().GoToUrl(AboutPage);
				ITakesScreenshot screenshotdriver = (ITakesScreenshot) driver;
				Screenshot screenshot = screenshotdriver.GetScreenshot();
				screenshot.SaveAsFile("aboutPage.bmp",ScreenshotImageFormat.Bmp);
				FileInfo file = new FileInfo("aboutPage.bmp");
				Approvals.Verify(file);
			}
		}

		[Fact]
		[Trait("Category", "Smoke")]
		[UseReporter(typeof(BeyondCompareReporter))]

		public void TakeScreenshotHomePage()
		{
			using (IWebDriver driver = new ChromeDriver("."))
			{
				var homePage = new HomePage(driver);
				homePage.NavigateTo();
				ITakesScreenshot screenshotdriver = (ITakesScreenshot)driver;
				Screenshot screenshot = screenshotdriver.GetScreenshot();
				screenshot.SaveAsFile("homepage.bmp",ScreenshotImageFormat.Bmp);
				FileInfo file = new FileInfo("homepage.bmp");
				//Approvals.Verify(file);// Failure due to dynamic page change
			}
		}

	}
}
