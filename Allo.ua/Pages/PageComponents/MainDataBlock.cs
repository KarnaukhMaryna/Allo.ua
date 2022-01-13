using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages.PageComponents
{
    public class MainDataBlock : BasePage
    {
        public MainDataBlock MainData { get; set; }

        public MainDataBlock(WebDriver driver, string locator) : base(driver) => driver.FindElement(By.XPath(locator));

        public IWebElement ProductHeader => driver.FindElement(By.XPath("//div[contains(@class, 'header product')]"));
        public List<IWebElement> ColorFilter => driver.FindElements(By.XPath("//a[contains(@class, 'color__link')]")).ToList();
        public IWebElement BuyButton => driver.FindElement(By.XPath("//div[contains(@class, 'main')]//button[contains(@class, 'buy-button')]"));


        public string GetHeaderText() => ProductHeader.Text;
        public string GetLastColour() => ColorFilter.Last().Text;
        public void ClickLastColour() => ColorFilter.Last().Click();
        public void AddToCart()
        {
            BuyButton.Click();
            WaitForPageLoadComplete(60);
        }
    }
}
