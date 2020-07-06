using System;
using System.Collections;
using System.Reflection;
using HackaThon.PageObjects;
using HackaThon.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PageObjects;

namespace HackaThon
{
    [TestClass]
    public class WebTask
    {
        //Example of test data being in the automation script
        string[] testDataFromBeingHarcoded = new string[] { "Fname1", "Lname1", "User1", "Pass1", "Company AAA", "Admin", "admin@mail.com", "082555" };
        string[,] testDataFromCsvFile;

        [TestCleanup]
        public void CleanUp()
        {
            //In order to be able to execute in Paralell, it can only flush after every test method
            Core.ExtentReport.Flush();
        }

        [TestMethod]
        public void Web_Test_HardCodedTestData()
        {
            //Populates test data object from hard coded values
            testDataObject testData = new testDataObject()
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

            //Website url
            string websiteUrl = "http://www.way2automation.com/angularjs-protractor/webtables/";

            //Creates a driver instance & navigates to the URL
            WebUtils seleniumInstance = new WebUtils(websiteUrl);

            //validates that the correct page is displayed
            if (!seleniumInstance.CheckElementIsPresent(WebTablePage.table))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the correct page not being shown.");

            Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully navigated to '" + websiteUrl + "'.");

            //Stores usernames for later validation
            ArrayList currentlyStoredUserNames = new ArrayList(seleniumInstance.GetAllValuesFrom(WebTablePage.tableUsernameValues));

            //Clicks the add user button
            if (!seleniumInstance.clickElement(WebTablePage.addUserButton))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed to click the add user button.");

            //Validates that the add user pop up appears
            if (!seleniumInstance.CheckElementIsPresent(WebTablePage.addUserForm))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the 'add user' form not being present");

            Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully navigated to the 'add user' form.");


            //Inserts the firstname
            if (!seleniumInstance.SendKeysTo(AddUserForm.firstNameInputField, testData.fname))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.fname + "' into the first name field.");

            //Inserts the lastname
            if (!seleniumInstance.SendKeysTo(AddUserForm.lastNameInputField, testData.lname))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.lname + "' into the last name field.");

            //Inserts the username
            if (!seleniumInstance.SendKeysTo(AddUserForm.userNameInputField, testData.username))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.username + "' into the user name field.");

            //Inserts the password
            if (!seleniumInstance.SendKeysTo(AddUserForm.passwordInputField, testData.password))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.password + "' into the password field.");

            //Selects the customer
            if (!seleniumInstance.clickElement(AddUserForm.customerRadioButton(testData.customer)))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed to select the '" + testData.customer + "' radio button.");

            //Selects the role
            if (!seleniumInstance.SelectValueFromComboBox(AddUserForm.roleComboBox, testData.role))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed select '" + testData.role + "' from the role combo box.");

            //Inserts the email
            if (!seleniumInstance.SendKeysTo(AddUserForm.emailInputField, testData.email))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.email + "' into the email field.");

            //Inserts the cellphone
            if (!seleniumInstance.SendKeysTo(AddUserForm.cellPhoneInputField, testData.cellnumber))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.cellnumber + "' into the cell phone field.");

            //logs a step passed
            Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully filled out the 'add user' form.");

            //Checks that the username is unique
            foreach (string existingUsernames in currentlyStoredUserNames)
            {
                if (existingUsernames.Equals(testData.username))
                    Core.ExtentReport.TestFailed(currentTest, seleniumInstance.GetDriver, "Failed due to the user name '" + testData.username + "' already existing in the application.");
            }

            Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully validated that the username '" + testData.username + "' is unique.");

            //Submits the form
            if (!seleniumInstance.clickElement(AddUserForm.saveButton))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed click the 'save' button.");

            //Ensures that the user has been added to the top of the list with the exact details
            //Stores usernames for later validation
            ArrayList newestAdditionToTheTable = new ArrayList(seleniumInstance.GetAllValuesFrom(WebTablePage.tableFirstLineValues));

            //Validates firstname
            if (!newestAdditionToTheTable[0].Equals(testData.fname))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table column 'First Name' not matching '" + testData.fname + "'.");

            //Validates lastname
            if (!newestAdditionToTheTable[1].Equals(testData.lname))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table column 'Last Name' not matching '" + testData.lname + "'.");

            //Validates username
            if (!newestAdditionToTheTable[2].Equals(testData.username))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table column 'User Name' not matching '" + testData.username + "'.");

            //Validates customer
            if (!newestAdditionToTheTable[3].Equals(testData.customer))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table column 'Customer' not matching '" + testData.customer + "'.");

            //Validates role
            if (!newestAdditionToTheTable[4].Equals(testData.role))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table column 'Role' not matching '" + testData.role + "'.");

            //Validates email
            if (!newestAdditionToTheTable[5].Equals(testData.email))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table column 'E-mail' not matching '" + testData.email + "'.");

            //Validates cellnumber
            if (!newestAdditionToTheTable[6].Equals(testData.cellnumber))
                Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table column 'Cell Number' not matching '" + testData.cellnumber + "'.");

            //Logs a passing step to the report
            Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully validated that the user '" + testData.fname + "' was present");

            //Closes the instance of the driver
            seleniumInstance.GetDriver.Quit();
        }

        [TestMethod]
        public void Web_Test_DynamiclyFetchingTestData()
        {
            //Variable used to fail mstest & avoid confusion due to the soft asserts
            bool testOutcome = true;

            //Reads CSV file 
            testDataFromCsvFile = Core.getTestDataFromCsv(Environment.CurrentDirectory + @"\..\..\..\TestDataArtifacts\DummyData.csv");

            //Website url
            string websiteUrl = "http://www.way2automation.com/angularjs-protractor/webtables/";

            //Iterates as may rows of data from the CSV file
            int totalRows = testDataFromCsvFile.GetUpperBound(0) + 1;
            int totalColumns = testDataFromCsvFile.GetUpperBound(1) + 1;
            for (int row = 0; row < totalRows; row++)
            {
                //Creates a test per iteration 
                var currentTest = Core.ExtentReport.CreateTest(
                    "Dynamic Iteration - " + (row + 1),
                    "This is a demo test for a basic Web UI test using selenium. <br/> "
                    + "<br/><b>Test data being used: </b><br/>"
                    + " first name = " + testDataFromCsvFile[row, 0] + "<br/> "
                    + " last name = " + testDataFromCsvFile[row, 1] + "<br/> "
                    + "username = " + testDataFromCsvFile[row, 2] + "<br/> "
                    + " password = " + testDataFromCsvFile[row, 3] + "<br/> "
                    + " customer = " + testDataFromCsvFile[row, 4] + "<br/> "
                    + " role = " + testDataFromCsvFile[row, 5] + "<br/> "
                    + " email = " + testDataFromCsvFile[row, 6] + "<br/> "
                    + " cell number = " + testDataFromCsvFile[row, 7] + "<br/> "
                );

                //Populates test data object from csv values
                testDataObject testData = new testDataObject()
                {
                    fname = testDataFromCsvFile[row, 0],
                    lname = testDataFromCsvFile[row, 1],
                    username = testDataFromCsvFile[row, 2],
                    password = testDataFromCsvFile[row, 3],
                    customer = testDataFromCsvFile[row, 4],
                    role = testDataFromCsvFile[row, 5],
                    email = testDataFromCsvFile[row, 6],
                    cellnumber = testDataFromCsvFile[row, 7],
                };

                //Creates a driver instance & navigates to the URL
                WebUtils seleniumInstance = new WebUtils(websiteUrl);

                //validates that the correct page is displayed
                if (!seleniumInstance.CheckElementIsPresent(WebTablePage.table))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the correct page not being shown.");

                Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully navigated to '" + websiteUrl + "'.");

                //Stores usernames for later validation
                ArrayList currentlyStoredUserNames = new ArrayList(seleniumInstance.GetAllValuesFrom(WebTablePage.tableUsernameValues));

                //Clicks the add user button
                if (!seleniumInstance.clickElement(WebTablePage.addUserButton))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed to click the add user button.");

                //Validates that the add user pop up appears
                if (!seleniumInstance.CheckElementIsPresent(WebTablePage.addUserForm))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the 'add user' form not being present");

                Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully navigated to the 'add user' form.");

                //Inserts the firstname
                if (!seleniumInstance.SendKeysTo(AddUserForm.firstNameInputField, testData.fname))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.fname + "' into the first name field.");

                //Inserts the lastname
                if (!seleniumInstance.SendKeysTo(AddUserForm.lastNameInputField, testData.lname))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.lname + "' into the last name field.");

                //Inserts the username
                if (!seleniumInstance.SendKeysTo(AddUserForm.userNameInputField, testData.username))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.username + "' into the user name field.");

                //Inserts the password
                if (!seleniumInstance.SendKeysTo(AddUserForm.passwordInputField, testData.password))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.password + "' into the password field.");

                //Selects the customer
                if (!seleniumInstance.clickElement(AddUserForm.customerRadioButton(testData.customer)))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed to select the '" + testData.customer + "' radio button.");

                //Selects the role
                if (!seleniumInstance.SelectValueFromComboBox(AddUserForm.roleComboBox, testData.role))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed select '" + testData.role + "' from the role combo box.");

                //Inserts the email
                if (!seleniumInstance.SendKeysTo(AddUserForm.emailInputField, testData.email))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.email + "' into the email field.");

                //Inserts the cellphone
                if (!seleniumInstance.SendKeysTo(AddUserForm.cellPhoneInputField, testData.cellnumber))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed enter '" + testData.cellnumber + "' into the cell phone field.");

                //logs a step passed
                Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully filled out the 'add user' form.");

                //Checks that the username is unique
                foreach (string existingUsernames in currentlyStoredUserNames)
                {
                    if (existingUsernames.Equals(testData.username))
                        Core.ExtentReport.TestFailed(currentTest, seleniumInstance.GetDriver, "Failed due to the user name '" + testData.username + "' already existing in the application.");
                }

                Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully validated that the username '" + testData.username + "' is unique.");

                //Submits the form
                if (!seleniumInstance.clickElement(AddUserForm.saveButton))
                    Core.ExtentReport.TestFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed click the 'save' button.");

                //Ensures that the user has been added to the top of the list with the exact details
                //Stores usernames for later validation
                ArrayList newestAdditionToTheTable = new ArrayList(seleniumInstance.GetAllValuesFrom(WebTablePage.tableFirstLineValues));

                //Validates firstname
                if (!newestAdditionToTheTable[0].Equals(testData.fname))
                {
                    Core.ExtentReport.TestSoftFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table column 'First name' not matching '" + testData.fname + "'.");
                    testOutcome = false;
                    continue;
                }

                //Validates lastname
                if (!newestAdditionToTheTable[1].Equals(testData.lname))
                {
                    Core.ExtentReport.TestSoftFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table  column 'Last name' not matching '" + testData.lname + "'.");
                    testOutcome = false;
                    continue;
                }

                //Validates username
                if (!newestAdditionToTheTable[2].Equals(testData.username))
                {
                    Core.ExtentReport.TestSoftFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table  column 'User name' not matching '" + testData.username + "'.");
                    testOutcome = false;
                    continue;
                }

                //Validates customer
                if (!newestAdditionToTheTable[3].Equals(testData.customer))
                {
                    Core.ExtentReport.TestSoftFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table  column 'Customer' not matching '" + testData.customer + "'.");
                    testOutcome = false;
                    continue;
                }

                //Validates role
                if (!newestAdditionToTheTable[4].Equals(testData.role))
                {
                    Core.ExtentReport.TestSoftFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table  column 'Role' not matching '" + testData.role + "'.");
                    testOutcome = false;
                    continue;
                }

                //Validates email
                if (!newestAdditionToTheTable[5].Equals(testData.email))
                {
                    Core.ExtentReport.TestSoftFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table value  column 'Email' not matching '" + testData.email + "'.");
                    testOutcome = false;
                    continue;
                }

                //Validates cellnumber
                if (!newestAdditionToTheTable[6].Equals(testData.cellnumber))
                {
                    Core.ExtentReport.TestSoftFailedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Failed due to the table  column 'Cell Phone' not matching '" + testData.cellnumber + "'.");
                    testOutcome = false;
                    continue;
                }

                //Logs a passing step to the report
                Core.ExtentReport.StepPassedWithScreenShot(currentTest, seleniumInstance.GetDriver, "Successfully validated that the user '" + testData.fname + "' was present");

                //Closes the instance of the driver
                if (seleniumInstance.GetDriver != null)
                    seleniumInstance.GetDriver.Quit();
            }

            //Assert fails other wise mstest thinks the test passed
            Assert.IsTrue(testOutcome);
        }
    }


    //Test data model
    public class testDataObject
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string customer { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string cellnumber { get; set; }

    }
}