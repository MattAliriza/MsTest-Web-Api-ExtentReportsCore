using System.Reflection;
using HackaThon.PageObjects;
using HackaThon.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackaThon
{
    [TestClass]
    public class WebTask
    {
        


        [TestCleanup]
        public void CleanUp()
        {
            //In order to be able to execute in Paralell, it can only flush after every test method
            Core.ExtentReport.Flush();
        }

        [TestMethod]
        public void Web_Test()
        {
            var currentTest = Core.ExtentReport.CreateTest(
                MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", ""),
                "This is a demo test for a basic Web UI test using selenium."
            );

            //Creates a driver instance & navigates to the URL
            WebUtils seleniumInstance = new WebUtils("http://www.way2automation.com/angularjs-protractor/webtables/");

            //validates that the correct page is displayed
            if (!seleniumInstance.CheckElementIsPresent(WebTablePage.table))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the correct page not being shown.");

            //Clicks the add user button
            if (!seleniumInstance.clickeElement(WebTablePage.addUserButton))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed to click the add user button.");

            //Closes the instance of the driver
            seleniumInstance.GetDriver.Quit();
        }


    }
}