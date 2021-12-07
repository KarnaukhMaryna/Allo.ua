using Allo.ua.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Allo.ua.Tests
{
    public class BaseTest
    {
        private WebDriver driver;
        private static string _allo = "https://allo.ua/ru";



        [SetUp]
        public void TestsSetUp()
        {
            driver = new ChromeDriver("D:\\Project\\Automation\\Allo\\Allo.ua\\Resources\\Driver");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(_allo);
        }

        [TearDown]
        public void TearDown() => driver.Close();

        public WebDriver GetDriver() => driver;

        public MainPage GetMainPage() => new(driver);
        public SearchResultPage GetSearchResultPage() => new(driver);
        public ProductPage GetProductPage() => new(driver);


    }
}
