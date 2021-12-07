using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages
{
    public class ProductPage : BasePage
    {
        public ProductPage(WebDriver driver) : base(driver) => this.driver = driver;

        private string _productHeader = "//div[contains(@class, 'header product')]";
        private string _colourFilter = "//a[contains(@class, 'color__link')]";
        private string _productDescription = "//div[contains(@class, 'description-suites')]";
        private string _buyButton = "//div[contains(@class, 'main')]//button[contains(@class, 'buy-button')]";
        private string _popup = "//div[contains(@class, 'md cart-popup')]";
        private string _productsInCart = "//span[@class='c-counter']";

        private string _productItemInPopup = "//li[@class='products_list_item']";
        private string _totalPrice = "//p[@class='total-box']";
        private string _closePopupButton = "//div[contains(@class, 'close')]";
        private string _backToChooseProducts = "//button[@class = 'comeback']";
        private string _orderNowButton = "//button[@class= 'order-now']";

        public string GetHeaderText() => driver.FindElement(By.XPath(_productHeader)).Text;
        public string GetLastColour() => driver.FindElements(By.XPath(_colourFilter)).Last().Text;
        public void ClickLastColour() => driver.FindElements(By.XPath(_colourFilter)).Last().Click();
        public string GetDescriptionText() => driver.FindElement(By.XPath(_productDescription)).Text;
        public void AddToCart() => driver.FindElement(By.XPath(_buyButton)).Click();
        public bool PopupIsDisplayed() => driver.FindElement(By.XPath(_popup)).Displayed;
        public void BackToChooseProducts()=> driver.FindElement(By.XPath(_backToChooseProducts)).Click();
        public void ClosePopup() => driver.FindElement(By.XPath(_closePopupButton)).Click();
        public string AmountOfProductsInCart() => driver.FindElement(By.XPath(_productsInCart)).Text;

        public List<IWebElement> ElementsInPopup()
        {
            List<IWebElement> listOfPopupElements = new();
            listOfPopupElements.Add(driver.FindElement(By.XPath(_productItemInPopup)));
            listOfPopupElements.Add(driver.FindElement(By.XPath(_totalPrice)));
            listOfPopupElements.Add(driver.FindElement(By.XPath(_closePopupButton)));
            listOfPopupElements.Add(driver.FindElement(By.XPath(_backToChooseProducts)));
            listOfPopupElements.Add(driver.FindElement(By.XPath(_orderNowButton)));
            return listOfPopupElements;
        }

    }
}
