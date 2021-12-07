﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allo.ua.Pages
{
    public class BasePage
    {
        public WebDriver driver;

        public BasePage(WebDriver driver) => this.driver = driver;

        public void WaitForPageLoadComplete(int timeToWait)
        {
            var initialSource = driver.PageSource;
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWait))
                .Until(drvr => drvr.PageSource != initialSource);
        }

        public void WaitVisibilityOfElement(int timeToWait, string webElement)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWait))
             .Until(driver => driver.FindElement(By.XPath(webElement)));
        }

        //public void WaitVisibilityOfElement(int timeToWait, IWebElement webElement)
        //{
        //    new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWait))
        //     .Until(ExpectedConditions.ElementToBeClickable(webElement));
        //}

        public void WaitElementToBeClickable(int timeToWait, string webElement)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeToWait))
             .Until(ExpectedConditions.ElementToBeClickable(By.XPath(webElement)));
        }
    }
}
