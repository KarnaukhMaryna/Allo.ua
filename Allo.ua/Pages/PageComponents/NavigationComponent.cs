using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages.PageComponents
{
    public class NavigationComponent : BasePage
    {
        private string _location = "//div[@class='mh-loc']/button[@class='mh-button']";
        private string _cities = "//a[@class='geo__city']";
        private string _searchVariants = "//a[@class='search-models__links']";

        public NavigationComponent Header { get; set; }

        public NavigationComponent(WebDriver driver, string locator) : base(driver) => driver.FindElement(By.XPath(locator));

        public IWebElement LocationButton => driver.FindElement(By.XPath("//div[@class='mh-loc']/button[@class='mh-button']"));
        public List<IWebElement> Cities => driver.FindElements(By.XPath("//a[@class='geo__city']")).ToList();
        public IWebElement SwitchLanguageButton => driver.FindElement(By.XPath("//span[@class='mh-lang__item']"));
        public IWebElement SearchField => driver.FindElement(By.XPath("//input[@class='search-form__input']"));
        public List<IWebElement> SearchVariantsInPopup => driver.FindElements(By.XPath("//a[@class='search-models__links']")).ToList();
        public IWebElement Cart => driver.FindElement(By.XPath("//span[@class='c-counter']"));

        public string GetCurrentLocation()
        {
            WaitVisibilityOfElement(30, _location);
            return LocationButton.GetAttribute("data-geo-label");
        }
        public void LocationButtonClick() => LocationButton.Click();
        public void SelectOtherCity() 
        {
            WaitVisibilityOfElement(30, _cities);
            Cities.Last().Click();
        }

        public void SwitchLanguageButtonClick()
        {
            SwitchLanguageButton.Click();
            WaitForPageLoadComplete(60);
        }
        public void FillSearchField(string product) => SearchField.SendKeys(product);
        public void ClickSearchField() => SearchField.Click();
        public void ChooseFirstItemFromPopup()
        {
            WaitVisibilityOfElement(30, _searchVariants);
            SearchVariantsInPopup.First().Click();
            WaitForPageLoadComplete(30);
        }
        public string GetFirstSearchVariant()
        {
            WaitVisibilityOfElement(30, _searchVariants);
            return SearchVariantsInPopup.First().Text;
        }
        public string AmountOfProductsInCart() => Cart.Text;

    }
}
