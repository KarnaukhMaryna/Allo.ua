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
 
        public string searchKeyword = "so";
        
        [Test]
        public void CheckThatLocationChanged()
        {
            var defaultLocation = GetMainPage().Header.GetCurrentLocation();
            GetMainPage().Header.LocationButtonClick();
            GetMainPage().Header.SelectOtherCity();
            var choosenLocation = GetMainPage().Header.GetCurrentLocation();
            Assert.AreNotEqual(defaultLocation, choosenLocation);
        }

        [Test]
        public void CheckThatLanguageChanged()
        {
            GetMainPage().Header.SwitchLanguageButtonClick();
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
            GetMainPage().Header.FillSearchField(searchKeyword);
            GetMainPage().Header.ClickSearchField();
            var keyword = GetMainPage().Header.GetFirstSearchVariant();
            GetMainPage().Header.ChooseFirstItemFromPopup();
            var searchResults = GetSearchResultPage().MainContainer.ProductCards;
            foreach (var searchResault in searchResults)
            {
                Assert.True(searchResault.Text.ToLower().Contains(keyword));
            }
        }

        [Test]
        public void CheckThatPriceFilterWorkCorrectly()
        {
            GetMainPage().Header.FillSearchField(searchKeyword);
            GetMainPage().Header.ClickSearchField();
            GetMainPage().Header.ChooseFirstItemFromPopup();
            GetSearchResultPage().SideBar.MoveToPriceFilter();
            GetSearchResultPage().SideBar.ClearMinimalPrice();
            GetSearchResultPage().SideBar.SetMinimalPrice("1000");
            GetSearchResultPage().SideBar.SetFilter();
            var prices = GetSearchResultPage().MainContainer.GetPrices();
            foreach(var price in prices)
            {
                Assert.True(price > 1000);
            }
        }

        [Test]
        public void CheckThatBrandFilterWorkCorrectly()
        {
            GetMainPage().Header.FillSearchField(searchKeyword);
            GetMainPage().Header.ClickSearchField();
            GetMainPage().Header.ChooseFirstItemFromPopup();
            var brandName = GetSearchResultPage().SideBar.GetBrandName();
            GetSearchResultPage().SideBar.ClickFirstBrandFilter();
            GetSearchResultPage().SideBar.SetFilter();
            var searchResults = GetSearchResultPage().MainContainer.ProductCards;
            foreach (var searchResault in searchResults)
            {
                Assert.True(searchResault.Text.Contains(brandName));
            }
        }

        [Test]
        public void CheckThatHeaderAndDescriptionContainSearchWord()
        {
            GetMainPage().Header.FillSearchField(searchKeyword);
            GetMainPage().Header.ClickSearchField();
            var keyword = GetMainPage().Header.GetFirstSearchVariant();
            GetMainPage().Header.ChooseFirstItemFromPopup();
            GetSearchResultPage().MainContainer.FirstProductClick();
            Assert.True(GetProductPage().MainBlock.GetHeaderText().ToLower().Contains(keyword));
            Assert.True(GetProductPage().Description.GetDescriptionText().ToLower().Contains(keyword));
        }

        [Test]
        public void CheckThatColorOfDeviceChange()
        {
            GetMainPage().Header.FillSearchField(searchKeyword);
            GetMainPage().Header.ClickSearchField();
            GetMainPage().Header.ChooseFirstItemFromPopup();
            GetSearchResultPage().MainContainer.FirstProductClick();
            var colour = GetProductPage().MainBlock.GetLastColour();
            GetProductPage().MainBlock.ClickLastColour();
            Assert.True(GetProductPage().MainBlock.GetHeaderText().Contains(colour));
        }

        [Test]
        public void CheckPopupAndFields()
        {
            GetMainPage().Header.FillSearchField(searchKeyword);
            GetMainPage().Header.ClickSearchField();
            GetMainPage().Header.ChooseFirstItemFromPopup();
            GetSearchResultPage().MainContainer.FirstProductClick();
            GetProductPage().MainBlock.AddToCart();
            Assert.IsTrue(GetProductPage().PopupIsDisplayed());
            GetProductPage().WaitForPageLoadComplete(90);
            var elementsInPopup = GetProductPage().PopupForAddToCart.ElementsInPopup();
            foreach (var element in elementsInPopup)
            {
                Assert.True(element.Displayed);
            }
        }

        [Test]
        public void CheckAddToCard()
        {
            GetMainPage().Header.FillSearchField(searchKeyword);
            GetMainPage().Header.ClickSearchField();
            GetMainPage().Header.ChooseFirstItemFromPopup();
            GetSearchResultPage().MainContainer.FirstProductClick();
            GetProductPage().MainBlock.AddToCart();
            GetProductPage().PopupForAddToCart.BackToChooseProducts();
            GetDriver().Navigate().Back();
            GetSearchResultPage().MainContainer.ThirdProductClick();
            GetProductPage().MainBlock.AddToCart();
            Assert.IsTrue(GetProductPage().PopupIsDisplayed());
            GetProductPage().WaitForPageLoadComplete(90);
            var elementsInPopup = GetProductPage().PopupForAddToCart.ElementsInPopup();
            foreach (var element in elementsInPopup)
            {
                Assert.True(element.Displayed);
            }
            GetProductPage().PopupForAddToCart.ClosePopup();
            GetProductPage().WaitForPageLoadComplete(90);
            Assert.AreEqual("2", GetProductPage().Header.AmountOfProductsInCart());
        }



    }
}