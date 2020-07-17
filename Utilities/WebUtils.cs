using System;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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

        //
        //Example of faster execution of xpaths
        //

        // public bool CheckElementIsPresent(string areaToSearh, string xpath)
        // {
        //     try
        //     {
        //         IWebElement ele = _driver.FindElement(By.XPath(areaToSearch));
        //         IWebElement actualEle = ele.FindElement(By.XPath(xpath));

        //         if (!actualEle.Displayed)
        //             return false;

        //         return true;
        //     }
        //     catch (Exception exc)
        //     {
        //         Console.WriteLine(exc.Message);
        //         return false;
        //     }
        // }

        public bool CheckElementIsPresent(string xpath)
        {
            try
            {
                IWebElement ele = _driver.FindElement(By.XPath(xpath));

                return ele.Displayed;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
        }

        public bool clickElement(string xpath)
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

        public bool SendKeysTo(string xpath, string keys)
        {
            try
            {
                IWebElement ele = _driver.FindElement(By.XPath(xpath));

                if (!ele.Displayed)
                    return false;

                ele.SendKeys(keys);
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
        }

        public bool SelectValueFromComboBox(string xpath, string comboBoxValue)
        {
            try
            {
                IWebElement ele = _driver.FindElement(By.XPath(xpath));

                if (!ele.Displayed)
                    return false;

                //Creates select object
                var selectElement = new SelectElement(ele);

                //Selects the value
                selectElement.SelectByText(comboBoxValue);

                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;
            }
        }

        public ArrayList GetAllValuesFrom(string xpath)
        {
            try
            {
                var elementList = _driver.FindElements(By.XPath(xpath));
                ArrayList tempList = new ArrayList();

                //Extrats text values
                foreach (IWebElement currentElement in elementList)
                {
                    if (!String.IsNullOrEmpty(currentElement.Text))
                    {
                        tempList.Add(currentElement.Text);
                        continue;
                    }

                    tempList.Add("N/A");
                }

                return tempList;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
        }
    }
}