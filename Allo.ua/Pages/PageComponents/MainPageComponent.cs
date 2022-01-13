using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages.PageComponents
{
    public class MainPageComponent : BasePage
    {
        public MainPageComponent MainContainer { get; set; }

        public MainPageComponent(WebDriver driver, string locator) : base(driver) => driver.FindElement(By.XPath(locator));

        public List<IWebElement> ProductCards => driver.FindElements(By.XPath("//div[@class='product-card']")).ToList();
        public List<IWebElement> ProductPrices => driver.FindElements(By.XPath("//div[contains(@class, 'cur')]/span[@class='sum']")).ToList();

        public void FirstProductClick()
        {
            ProductCards.First().Click();
            WaitForPageLoadComplete(30);
        }
        public void ThirdProductClick()
        { 
            ProductCards[3].Click();
            WaitForPageLoadComplete(30);
        }
        
        public List<int> GetPrices()
        {
            List<int> prices = new();
            foreach (var price in ProductPrices)
            {
                var stringPrice = price.Text.Replace(" ", "");
                prices.Add(int.Parse(stringPrice));
            }
            return prices;
        }
    }
}
