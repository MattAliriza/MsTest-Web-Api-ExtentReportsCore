using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace HackaThon.Utilities
{
    public class Reporting
    {
        private AventStack.ExtentReports.ExtentReports _extent;
        private string _reportLocation = Environment.CurrentDirectory + @"\..\..\..\Reports\" + DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss") + @"\";
        private int _screenShotCounter = 0;

        public Reporting()
        {
            //Creates the folder
            CreateFileAtLocation(_reportLocation);

            //Creates a reports object
            var Htmlreporter = new ExtentHtmlReporter(_reportLocation);
            _extent = new AventStack.ExtentReports.ExtentReports();

            //Attachs the html report
            _extent.AttachReporter(Htmlreporter);

            //Flushs the report
            _extent.Flush();
        }

        public ExtentTest CreateTest(string testName, string description)
        {
            return _extent.CreateTest(testName, description);
        }

        public void Flush()
        {
            _extent.Flush();
        }

        private void CreateFileAtLocation(string location)
        {
            Directory.CreateDirectory(location);
        }

        public void StepPassed(ExtentTest currentTest, string message)
        {
            //Logs the message
            currentTest.Log(Status.Pass, message);
        }

        public void LogResponse(ExtentTest currentTest, string response, CodeLanguage codeFormat)
        {
            //Adds a label for visual purposes
            var formattedLabel = MarkupHelper.CreateLabel("Response:", ExtentColor.Lime);
            currentTest.Log(Status.Info, formattedLabel);

            //Logs the response in the codeFormat
            var formattedMessage = MarkupHelper.CreateCodeBlock(response, codeFormat);
            currentTest.Log(Status.Info, formattedMessage);
        }

        public void LogUrlRequest(ExtentTest currentTest, string request, CodeLanguage codeFormat)
        {
            //Adds a label for visual purposes
            var formattedLabel = MarkupHelper.CreateLabel("Request:", ExtentColor.Lime);
            currentTest.Log(Status.Info, formattedLabel);

            //Logs the response in the codeFormat
            var formattedMessage = MarkupHelper.CreateCodeBlock(request, codeFormat);
            currentTest.Log(Status.Info, formattedMessage);
        }

        public void TestFailed(ExtentTest currentTest, string message)
        {
            //Logs the message
            currentTest.Log(Status.Fail, message);

            //Fails the test
            Assert.Fail(message);
        }

        public void TestFailedWithScreenShot(ExtentTest currentTest, IWebDriver driver, string message)
        {
            //Creates the screenshot file
            string screenShotPath = Capture(driver);

            //Logs the screenshot to the report
            currentTest.Log(Status.Fail, message + currentTest.AddScreenCaptureFromPath(screenShotPath));

            //Shuts down the driver
            driver.Quit();

            //Fails the test
            Assert.Fail(message);
        }

        public void InjectPictureFrom(ExtentTest currentTest, string url)
        {
            //Embeds a picture
            currentTest.Log(Status.Pass, "<img src=" + url + " width=250px height=250px />");
        }

        private string Capture(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string fullPath = _reportLocation + "ScreenShot" + _screenShotCounter + ".png";
            screenshot.SaveAsFile(new Uri(fullPath).LocalPath);

            //Increments the screenshot number
            _screenShotCounter++;

            return fullPath;
        }

    }
}