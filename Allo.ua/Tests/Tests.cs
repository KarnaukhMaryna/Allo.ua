using Allo.ua.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Allo.ua.Tests
{
    [TestFixture]
    public class Tests : BaseTest
    {
        public string cities = "//a[@class='geo__city']";
        public string location = "//div[@class='mh-loc']/button[@class='mh-button']";
        public string searchVariants = "//a[@class='search-models__links']";
        public string searchKeyword = "so";
        public string minPrice = "//input[@id='pricerange-from']";
        public string setFilter = "//button[@class='filter-popup__button']";
        private string backToChooseProducts = "//button[@class = 'comeback']";

        [Test]
        public void CheckThatLocationChanged()
        {
            GetMainPage().WaitVisibilityOfElement(30, location);
            var defaultLocation = GetMainPage().GetTextLocation();
            GetMainPage().LocationButtonClick();
            GetMainPage().WaitVisibilityOfElement(30, cities);
            GetMainPage().SelectOtherCity();
            GetMainPage().WaitVisibilityOfElement(30, location);
            var choosenLocation = GetMainPage().GetTextLocation();
            Assert.AreNotEqual(defaultLocation, choosenLocation);
        }

        [Test]
        public void CheckThatLanguageChanged()
        {
            GetMainPage().ChangeLanguage();
            GetMainPage().WaitForPageLoadComplete(60);
            Assert.IsFalse(GetDriver().Url.Contains("ru"));
        }

        [Test]
        public void CheckThatThemeChanged()
        {
            var firstColor = GetMainPage().HeaderColour();
            GetMainPage().ChangeTheme();
            var secondColor = GetMainPage().HeaderColour();
            Assert.AreNotEqual(firstColor, secondColor);
        }

        [Test]
        public void CheckThatSearchResultsContainsSearchWord()
        {
            GetMainPage().FillSearchField(searchKeyword);
            GetMainPage().ClickSearchField();
            GetMainPage().WaitVisibilityOfElement(30, searchVariants);
            var keyword = GetMainPage().GetFirstSearchVariant();
            GetMainPage().ChooseFirstItemFromPopup();
            var searchResults = GetSearchResultPage().GetSearchResultsList();
            foreach (var searchResault in searchResults)
            {
                Assert.True(searchResault.Text.ToLower().Contains(keyword));
            }
        }

        [Test]
        public void CheckThatPriceFilterWorkCorrectly()
        {
            GetMainPage().FillSearchField(searchKeyword);
            GetMainPage().ClickSearchField();
            GetMainPage().WaitVisibilityOfElement(30, searchVariants);
            GetMainPage().ChooseFirstItemFromPopup();
            GetSearchResultPage().MoveToPriceFilter();
            GetSearchResultPage().WaitVisibilityOfElement(60, minPrice);
            GetSearchResultPage().ClearMinimalPrice();
            GetSearchResultPage().SetMinimalPrice("1000");
            GetSearchResultPage().WaitElementToBeClickable(30, setFilter);
            GetSearchResultPage().SetFilter();
        

        }

        [Test]
        public void CheckThatBrandFilterWorkCorrectly()
        {
            GetMainPage().FillSearchField(searchKeyword);
            GetMainPage().ClickSearchField();
            GetMainPage().WaitVisibilityOfElement(30, searchVariants);
            GetMainPage().ChooseFirstItemFromPopup();
            var brandName = GetSearchResultPage().GetBrandName();
            GetSearchResultPage().ClickFirstBrandFilter();
            GetSearchResultPage().WaitElementToBeClickable(60, setFilter);
            GetSearchResultPage().SetFilter();
            GetSearchResultPage().WaitForPageLoadComplete(30);
            //var searchResults = GetSearchResultPage().GetSearchResultsList();
            //foreach (var searchResault in searchResults)
            //{
            //    Assert.True(searchResault.Text.Contains(brandName));
            //}
        }

        [Test]
        public void CheckThatHeaderAndDescriptionContainSearchWord()
        {
            GetMainPage().FillSearchField(searchKeyword);
            GetMainPage().ClickSearchField();
            GetMainPage().WaitVisibilityOfElement(30, searchVariants);
            var keyword = GetMainPage().GetFirstSearchVariant();
            GetMainPage().ChooseFirstItemFromPopup();
            GetSearchResultPage().WaitForPageLoadComplete(30);
            GetSearchResultPage().FirstElementClick();
            GetProductPage().WaitForPageLoadComplete(30);
            Assert.True(GetProductPage().GetHeaderText().ToLower().Contains(keyword));
            Assert.True(GetProductPage().GetDescriptionText().ToLower().Contains(keyword));
        }

        [Test]
        public void CheckThatColorOfDeviceChange()
        {
            GetMainPage().FillSearchField(searchKeyword);
            GetMainPage().ClickSearchField();
            GetMainPage().WaitVisibilityOfElement(30, searchVariants);
            GetMainPage().ChooseFirstItemFromPopup();
            GetSearchResultPage().WaitForPageLoadComplete(30);
            GetSearchResultPage().FirstElementClick();
            GetProductPage().WaitForPageLoadComplete(30);
            var colour = GetProductPage().GetLastColour();
            GetProductPage().ClickLastColour();
            Assert.True(GetProductPage().GetHeaderText().Contains(colour));
        }

        [Test]
        public void CheckPopupAndFields()
        {
            GetMainPage().FillSearchField(searchKeyword);
            GetMainPage().ClickSearchField();
            GetMainPage().WaitVisibilityOfElement(30, searchVariants);
            GetMainPage().ChooseFirstItemFromPopup();
            GetSearchResultPage().WaitForPageLoadComplete(30);
            GetSearchResultPage().FirstElementClick();
            GetProductPage().AddToCart();
            GetProductPage().WaitForPageLoadComplete(30);
            Assert.IsTrue(GetProductPage().PopupIsDisplayed());
            GetProductPage().WaitForPageLoadComplete(90);
            var elementsInPopup = GetProductPage().ElementsInPopup();
            foreach (var element in elementsInPopup)
            {
                Assert.True(element.Displayed);
            }
        }

        [Test]
        public void CheckAddToCard()
        {
            GetMainPage().FillSearchField(searchKeyword);
            GetMainPage().ClickSearchField();
            GetMainPage().WaitVisibilityOfElement(30, searchVariants);
            GetMainPage().ChooseFirstItemFromPopup();
            GetSearchResultPage().WaitForPageLoadComplete(30);
            GetSearchResultPage().FirstElementClick();
            GetProductPage().AddToCart();
            GetProductPage().WaitForPageLoadComplete(30);
            GetMainPage().WaitVisibilityOfElement(30, backToChooseProducts);
            GetProductPage().BackToChooseProducts();
            GetDriver().Navigate().Back();
            GetSearchResultPage().ClickOnThirdProduct();
            GetProductPage().WaitForPageLoadComplete(30);
            GetProductPage().AddToCart();
            GetProductPage().WaitForPageLoadComplete(60);
            Assert.IsTrue(GetProductPage().PopupIsDisplayed());
            GetProductPage().WaitForPageLoadComplete(90);
            var elementsInPopup = GetProductPage().ElementsInPopup();
            foreach (var element in elementsInPopup)
            {
                Assert.True(element.Displayed);
            }
            GetProductPage().ClosePopup();
            GetProductPage().WaitForPageLoadComplete(90);
            Assert.AreEqual("2", GetProductPage().AmountOfProductsInCart());
        }



    }
}