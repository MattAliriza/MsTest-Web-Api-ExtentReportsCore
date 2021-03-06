using System;
using System.Collections;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace HackaThon.Utilities
{

       //
       //Example of faster execution of xpaths
       //

       // public bool CheckElementIsDisplayed(string areaToSearh, string xpath)
       // {
       //     try
       //     {
       //         IWebElement ele = _driver.FindElement(By.XPath(areaToSearch));
       //         IWebElement actualEle = ele.FindElement(By.XPath(xpath));
       //         return actualEle.Displayed;
       //     }
       //     catch (Exception exc)
       //     {
       //         Console.WriteLine(exc.Message);
       //         return false;
       //     }
       // }

       //Send through stackTrace
       public class WebUtils
       {
              private IWebDriver _driver;
              public IWebDriver GetDriver => _driver;

              public WebUtils(string chromeVersion = "84.0.4147.30")
              {
                     //Configures the driver
                     new DriverManager().SetUpDriver(new ChromeConfig(), chromeVersion);
                     _driver = new ChromeDriver();
              }

              public By find(By locator, int timeout = 5)
              {
                     var wait = new WebDriverWait(_driver, (TimeSpan.FromSeconds(timeout)));
                     wait.Until(drv => drv.FindElement(locator));
                     return locator;
              }

              public bool CheckElementIsPresent(string xpath, ExtentTest currentTest, bool softAssert = false)
              {
                     try
                     {
                            IWebElement ele = _driver.FindElement(find(By.XPath(xpath)));

                            return ele.Displayed;
                     }
                     catch (Exception exc)
                     {
                            Core.ExtentReport.TestFailedWithScreenShot(currentTest, _driver, "Failed due to the correct page not being shown. Stack trace -" + exc, softAssert);
                            throw exc;
                     }
              }

              public bool clickElement(string xpath, ExtentTest currentTest, bool softAssert = false)
              {
                     try
                     {
                            IWebElement ele = _driver.FindElement(find(By.XPath(xpath)));

                            if (!ele.Displayed)
                                   return false;

                            ele.Click();
                            return true;
                     }
                     catch (Exception exc)
                     {
                            Core.ExtentReport.TestFailedWithScreenShot(currentTest, _driver, "Failed to click. Stack trace - " + exc, softAssert);
                            throw exc;
                     }
              }

              public bool SendKeysTo(string xpath, string keys, ExtentTest currentTest, bool softAssert = false)
              {
                     try
                     {
                            IWebElement ele = _driver.FindElement(find(By.XPath(xpath)));

                            if (!ele.Displayed)
                                   return false;

                            ele.SendKeys(keys);
                            return true;
                     }
                     catch (Exception exc)
                     {
                            Core.ExtentReport.TestFailedWithScreenShot(currentTest, _driver, "Failed enter '" + keys + "'. Stack trace - " + exc, softAssert);
                            throw exc;
                     }
              }

              public bool SelectValueFromComboBox(string xpath, string comboBoxValue, ExtentTest currentTest, bool softAssert = false)
              {
                     try
                     {
                            IWebElement ele = _driver.FindElement(find(By.XPath(xpath)));

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
                            Core.ExtentReport.TestFailedWithScreenShot(currentTest, _driver, "Failed select '" + comboBoxValue + "' from the combo box.", softAssert);
                            throw exc;
                     }
              }

              public ArrayList GetAllValuesFrom(string xpath)
              {
                     try
                     {
                            var elementList = _driver.FindElements(find(By.XPath(xpath)));
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