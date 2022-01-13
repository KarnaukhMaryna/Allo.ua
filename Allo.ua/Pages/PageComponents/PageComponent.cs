using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages.PageComponents
{
    public class PageComponent : BasePage
    {
        private string _minPrice = "//input[@id='pricerange-from']";
        private string _setFilter = "//button[@class='filter-popup__button']";
        
        public PageComponent SideBar { get; set; }

        public PageComponent(WebDriver driver, string locator) : base(driver) => driver.FindElement(By.XPath(locator));

        public IWebElement PriceFilter => driver.FindElement(By.XPath("//h2[@data-id='price']"));
        public IWebElement MinimalPrice => driver.FindElement(By.XPath("//input[@id='pricerange-from']"));
        public IWebElement MaximalPrice => driver.FindElement(By.XPath("//input[@id='pricerange-to']"));
        public IWebElement ShowFiltrationResultButton => driver.FindElement(By.XPath("//button[@class='filter-popup__button']"));
        public List<IWebElement> BrandFiltersButtons => driver.FindElements(By.XPath("//a[@class='filter__item']")).ToList();
        public List<IWebElement> BrandFilters => driver.FindElements(By.XPath("//a[@class='filter__item']/label")).ToList();

        public void MoveToPriceFilter() => new Actions(driver).MoveToElement(PriceFilter).Perform();
        public void ClearMinimalPrice()
        {
            WaitVisibilityOfElement(60, _minPrice);
            MinimalPrice.Clear();
        }
        public void SetMinimalPrice(string price) => MinimalPrice.SendKeys(price);
        public void ClearMaximalPrice() => MaximalPrice.Clear();
        public void SetMaximalPrice(string price) => MaximalPrice.SendKeys(price);
        public void SetFilter()
        {
            WaitElementToBeClickable(60, _setFilter);
            ShowFiltrationResultButton.Click();
            WaitForPageLoadComplete(30);
        }
        public string GetBrandName() => BrandFilters[5].Text;
        public void ClickFirstBrandFilter() => BrandFiltersButtons[5].Click();
        
        
    }
}
