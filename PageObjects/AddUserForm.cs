using AventStack.ExtentReports;
using HackaThon.Models;
using HackaThon.Utilities;
using OpenQA.Selenium;

namespace PageObjects
{
    public class AddUserForm
    {
        private IWebDriver _driver;
        private WebUtils _seleniumInstance;
        private ExtentTest _currentTest;

        public AddUserForm(WebUtils webUtil, ExtentTest currentTest)
        {
            _driver = webUtil.GetDriver;
            _seleniumInstance = webUtil;
            _currentTest = currentTest;
        }

    }
}