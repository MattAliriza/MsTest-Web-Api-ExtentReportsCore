using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using CsvHelper;
using HackaThon.Models;
using HackaThon.PageObjects;
using HackaThon.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HackaThon.TestCases
{
    [TestClass]
    public class WebTask
    {
        //Example of test data being in the automation script
        string[] testDataFromBeingHarcoded = new string[] { "Fname1", "Lname1", "User1", "Pass1", "Company AAA", "Admin", "admin@mail.com", "082555" };
        string[,] testDataFromCsvFile;
        string jsonResponse = "{" +
            "\n\"fname\": \"Fname1\"," +
            "\n\"Lname\": \"Lname1\"," +
            "\n\"Username\": \"User1\"," +
            "\n\"Password\": \"Pass1\"," +
            "\n\"Customer\": \"Company AAA\"," +
            "\n\"Role\": \"Admin\"," +
            "\n\"Email\": \"admin@mail.com\"," +
            "\n\"CellNumber\": \"082555\"\n}";

        [TestCleanup]
        public void CleanUp()
        {
            //In order to be able to execute in Paralell, it can only flush after every test method
            Core.ExtentReport.Flush();
        }

        [TestMethod, TestCategory("Web")]
        public void Web_Test_FromHardCodedTestData()
        {
            //Populates test data object from hard coded values
            User testData = new User()
            {
                fname = testDataFromBeingHarcoded[0],
                lname = testDataFromBeingHarcoded[1],
                username = testDataFromBeingHarcoded[2],
                password = testDataFromBeingHarcoded[3],
                customer = testDataFromBeingHarcoded[4],
                role = testDataFromBeingHarcoded[5],
                email = testDataFromBeingHarcoded[6],
                cellnumber = testDataFromBeingHarcoded[7]
            };

            //Creates a test per iteration 
            var currentTest = Core.ExtentReport.CreateTest(
                MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", ""),
                "This is a demo test for a basic Web UI test using selenium. <br/> "
                + "<br/><b>Test data being used: </b><br/>"
                + " first name = " + testDataFromBeingHarcoded[0] + "<br/> "
                + " last name = " + testDataFromBeingHarcoded[1] + "<br/> "
                + "username = " + testDataFromBeingHarcoded[2] + "<br/> "
                + " password = " + testDataFromBeingHarcoded[3] + "<br/> "
                + " customer = " + testDataFromBeingHarcoded[4] + "<br/> "
                + " role = " + testDataFromBeingHarcoded[5] + "<br/> "
                + " email = " + testDataFromBeingHarcoded[6] + "<br/> "
                + " cell number = " + testDataFromBeingHarcoded[7] + "<br/> "
            );

            //Creates Selenium instance
            WebUtils seleniumInstance = new WebUtils();

            //Creates a driver instance & reporting instance
            AngularJsProtractor angularSystem = new AngularJsProtractor(seleniumInstance, currentTest);

            angularSystem.WebTablePageInstance.NavigateToWebPage("http://www.way2automation.com/angularjs-protractor/webtables/");
            angularSystem.WebTablePageInstance.AddUserToTable(testData);
            angularSystem.WebTablePageInstance.ValidateUserWasAdded(testData);

            //Closes the instance of the driver
            seleniumInstance.GetDriver.Quit();
        }

        [TestMethod, TestCategory("Web")]
        public void Web_Test_FromJsonResponseTestData()
        {
            JsonUtil jsonItil = new JsonUtil();
            //Populates test data object from hard coded values
            User testData = jsonItil.getUserInformation(jsonResponse);

            //Creates a test per iteration 
            var currentTest = Core.ExtentReport.CreateTest(
                MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", ""),
                "This is a demo test for a basic Web UI test using selenium. <br/> "
                + "<br/><b>Test data being used: </b><br/>"
                + " first name = " + testDataFromBeingHarcoded[0] + "<br/> "
                + " last name = " + testDataFromBeingHarcoded[1] + "<br/> "
                + "username = " + testDataFromBeingHarcoded[2] + "<br/> "
                + " password = " + testDataFromBeingHarcoded[3] + "<br/> "
                + " customer = " + testDataFromBeingHarcoded[4] + "<br/> "
                + " role = " + testDataFromBeingHarcoded[5] + "<br/> "
                + " email = " + testDataFromBeingHarcoded[6] + "<br/> "
                + " cell number = " + testDataFromBeingHarcoded[7] + "<br/> "
            );

            //Creates Selenium instance
            WebUtils seleniumInstance = new WebUtils();

            //Creates a driver instance & reporting instance
            AngularJsProtractor angularSystem = new AngularJsProtractor(seleniumInstance, currentTest);

            angularSystem.WebTablePageInstance.NavigateToWebPage("http://www.way2automation.com/angularjs-protractor/webtables/");
            angularSystem.WebTablePageInstance.AddUserToTable(testData);
            angularSystem.WebTablePageInstance.ValidateUserWasAdded(testData);

            //Closes the instance of the driver
            seleniumInstance.GetDriver.Quit();
        }

        [TestMethod, TestCategory("Web")]
        public void Web_Test_FromCsvTestData()
        {
            using (var reader = new StreamReader(Environment.CurrentDirectory + @"\..\..\..\TestDataArtifacts\DummyData.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var record = new User();
                var records = csv.EnumerateRecords(record);
                foreach (var columnData in records)
                {
                    //Populates test data object from csv values
                    User testData = new User()
                    {
                        fname = columnData.fname.Trim(),
                        lname = columnData.lname.Trim(),
                        username = columnData.username.Trim(),
                        password = columnData.password.Trim(),
                        customer = columnData.customer.Trim(),
                        role = columnData.role.Trim(),
                        email = columnData.email.Trim(),
                        cellnumber = columnData.cellnumber.Trim(),
                    };

                    //For reporting purposes only
                    int iteration = 1;

                    //Creates a test per iteration
                    var currentTest = Core.ExtentReport.CreateTest(
                         MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", "") + (iteration),
                        "This is a demo test for a basic Web UI test using selenium. <br/> "
                        + "<br/><b>Test data being used: </b><br/>"
                        + " first name = " + testData.fname + "<br/> "
                        + " last name = " + testData.lname + "<br/> "
                        + "username = " + testData.username + "<br/> "
                        + " password = " + testData.password + "<br/> "
                        + " customer = " + testData.customer + "<br/> "
                        + " role = " + testData.role + "<br/> "
                        + " email = " + testData.email + "<br/> "
                        + " cell number = " + testData.cellnumber + "<br/> "
                    );

                    //Reporting purposes only
                    iteration++;

                    //Creates Selenium instance
                    WebUtils seleniumInstance = new WebUtils();

                    //Creates a driver instance & reporting instance
                    AngularJsProtractor angularSystem = new AngularJsProtractor(seleniumInstance, currentTest);

                    angularSystem.WebTablePageInstance.NavigateToWebPage("http://www.way2automation.com/angularjs-protractor/webtables/");
                    angularSystem.WebTablePageInstance.AddUserToTable(testData);
                    angularSystem.WebTablePageInstance.ValidateUserWasAdded(testData, true);

                    //Closes the instance of the driver
                    seleniumInstance.GetDriver.Quit();
                }
            }
        }
    }
}