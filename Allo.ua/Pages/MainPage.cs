using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages
{
    public class MainPage: BasePage
    {
        public MainPage(WebDriver driver) : base(driver) => this.driver = driver;

        private const string _locationButton = "//div[@class='mh-loc']/button[@class='mh-button']";
        private const string _cities = "//a[@class='geo__city']";
        private const string _switchLanguage = "//span[@class='mh-lang__item']";
        private const string _switchTheme = "//div[@class='header-theme']/div[@class='switcher-toggle']";
        private const string _mainHeader = "//div[@class='main-header-second-line-wrapper']";
        private const string _searchField = "//input[@class='search-form__input']";
        private const string _searchVariants = "//a[@class='search-models__links']";

        public IWebElement GetCity => driver.FindElement(By.XPath(_cities));
        public IWebElement GetLocation => driver.FindElement(By.XPath(_locationButton));

        public string GetTextLocation() => driver.FindElement(By.XPath(_locationButton)).GetAttribute("data-geo-label");
        public void LocationButtonClick() => driver.FindElement(By.XPath(_locationButton)).Click();
        public void SelectOtherCity() => driver.FindElements(By.XPath(_cities)).Last().Click();
        public void ChangeLanguage() => driver.FindElement(By.XPath(_switchLanguage)).Click();
        public void ChangeTheme() => driver.FindElement(By.XPath(_switchTheme)).Click();
        public string HeaderColour() => driver.FindElement(By.XPath(_mainHeader)).GetCssValue("background-color");
        public void FillSearchField(string product) => driver.FindElement(By.XPath(_searchField)).SendKeys(product);
        public void ClickSearchField() => driver.FindElement(By.XPath(_searchField)).Click();
        public void ChooseFirstItemFromPopup() => driver.FindElements(By.XPath(_searchVariants)).First().Click();
        public string GetFirstSearchVariant() => driver.FindElements(By.XPath(_searchVariants)).First().Text;
       
    }
}
