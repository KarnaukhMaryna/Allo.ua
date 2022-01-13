using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages.PageComponents
{
    public class DescriptionBlock: BasePage
    {
        public DescriptionBlock Description { get; set; }

        public DescriptionBlock(WebDriver driver, string locator) : base(driver) => driver.FindElement(By.XPath(locator));

        public IWebElement ProductDescription => driver.FindElement(By.XPath("//div[contains(@class, 'description-title')]"));

        public string GetDescriptionText() => ProductDescription.Text;
    }
}
