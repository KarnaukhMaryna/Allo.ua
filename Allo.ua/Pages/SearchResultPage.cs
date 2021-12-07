using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages
{
    public class SearchResultPage: BasePage
    {
        public SearchResultPage(WebDriver driver) : base(driver) => this.driver = driver;

        private string _productCards = "//div[@class='product-card']";
        private string _priceFilter = "//h2[@data-id='price']";
        private string _minimalPrice = "//input[@id='pricerange-from']";
        private string _maximalPrice = "//input[@id='pricerange-to']";
        private string _showFiltrationResult = "//button[@class='filter-popup__button']";
        private string _brandFilter = "//a[@class='filter__item']/label";
        private string _productPrice = "//div[contains(@class, 'cur')]/span[@class='sum']";

        public List<IWebElement> GetSearchResultsList() => driver.FindElements(By.XPath(_productCards)).ToList();
        public List<IWebElement> GetPriceList() => driver.FindElements(By.XPath(_productPrice)).ToList();
        public void ClearMinimalPrice() => driver.FindElement(By.XPath(_minimalPrice)).Clear();
        public void SetMinimalPrice(string price) => driver.FindElement(By.XPath(_minimalPrice)).SendKeys(price);
        public void ClearMaximalPrice() => driver.FindElement(By.XPath(_maximalPrice)).Clear();
        public void SetMaximalPrice(string price) => driver.FindElement(By.XPath(_maximalPrice)).SendKeys(price);
        public void SetFilter() => driver.FindElement(By.XPath(_showFiltrationResult)).Click();
        public void FirstElementClick() => driver.FindElements(By.XPath(_productCards)).First().Click();
        public void ClickOnThirdProduct() => driver.FindElements(By.XPath(_productCards))[3].Click();
        public void MoveToPriceFilter()
        {
            Actions mouseHover = new Actions(driver);
            mouseHover.MoveToElement(driver.FindElement(By.XPath(_priceFilter))).Perform();
        }
        public string GetBrandName() => driver.FindElements(By.XPath(_brandFilter))[5].Text;
        public void ClickFirstBrandFilter() => driver.FindElements(By.XPath(_brandFilter))[5].Click();
    }
}
