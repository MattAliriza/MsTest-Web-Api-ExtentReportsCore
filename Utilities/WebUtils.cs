using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace HackaThon.Utilities
{
    public class WebUtils
    {

        private IWebDriver _driver;

        public WebUtils(string url)
        {
            //Configures the driver
            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver();

            //Navigates to the URl and maximises the browser
            _driver.Navigate().GoToUrl(url);
            _driver.Manage().Window.Maximize();
        }

        public IWebDriver GetDriver => _driver;

        public bool CheckElementIsPresent(string xpath)
        {
            try
            {
                IWebElement ele = _driver.FindElement(By.XPath(xpath));

                if (!ele.Displayed)
                    return false;

                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
        }

        public bool clickeElement(string xpath)
        {
            try
            {
                IWebElement ele = _driver.FindElement(By.XPath(xpath));

                if (!ele.Displayed)
                    return false;

                ele.Click();
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
        }

    }

}