using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages.PageComponents
{
    public class PopupComponent :BasePage
    {
        private string _backToChooseProducts = "//button[@class = 'comeback']";

        public PopupComponent PopupForAddToCart { get; set; }

        public PopupComponent(WebDriver driver, string locator) : base(driver) => driver.FindElement(By.XPath(locator));

        public IWebElement ProductItemInPopup => driver.FindElement(By.XPath("//li[@class='products_list_item']")); 
        public IWebElement TotalPrice => driver.FindElement(By.XPath("//p[@class='total-box']"));
        public IWebElement ClosePopupButton => driver.FindElement(By.XPath("//div[contains(@class, 'close')]"));
        public IWebElement BackToChooseProductsButton => driver.FindElement(By.XPath("//button[@class = 'comeback']"));
        public IWebElement OrderNowButton => driver.FindElement(By.XPath("//button[@class= 'order-now']"));

        public void BackToChooseProducts()
        {
            WaitVisibilityOfElement(30, _backToChooseProducts);
            BackToChooseProductsButton.Click();
        }
        public void ClosePopup() => ClosePopupButton.Click();

        public List<IWebElement> ElementsInPopup()
        {
            List<IWebElement> listOfPopupElements = new();
            listOfPopupElements.Add(ProductItemInPopup);
            listOfPopupElements.Add(TotalPrice);
            listOfPopupElements.Add(ClosePopupButton);
            listOfPopupElements.Add(BackToChooseProductsButton);
            listOfPopupElements.Add(OrderNowButton);
            return listOfPopupElements;
        }

      

    }
}
