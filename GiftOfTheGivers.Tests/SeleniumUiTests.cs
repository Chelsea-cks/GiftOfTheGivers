using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace GiftOfTheGivers.Tests
{
    public class SeleniumUiTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl = "http://localhost:5110";

        public SeleniumUiTests()
        {
            var options = new ChromeOptions();
            // Run headless so tests can execute seamlessly in terminal or Azure Pipelines
            options.AddArgument("--headless=new");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Fact]
        public void HomePage_LoadsSuccessfully_AndHasCorrectTitle()
        {
            // Act
            _driver.Navigate().GoToUrl(_baseUrl);

            // Assert
            Assert.Contains("Gift", _driver.Title, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Navigation_ToDonationPage_FunctionsCorrectly()
        {
            // Act
            _driver.Navigate().GoToUrl($"{_baseUrl}/Donations/Create");

            // Assert
            IWebElement heading = _driver.FindElement(By.TagName("h1"));
            Assert.NotNull(heading);
        }

        public void Dispose()
        {
            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}