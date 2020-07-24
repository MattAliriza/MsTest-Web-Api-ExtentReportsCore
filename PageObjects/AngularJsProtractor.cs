using AventStack.ExtentReports;
using HackaThon.Utilities;
using PageObjects;

namespace HackaThon.PageObjects
{
    public class AngularJsProtractor
    {
        public AngularJsProtractor(WebUtils webUtil, ExtentTest currentTest)
        {
            AddUserFormInstance = new AddUserForm(webUtil, currentTest);
            WebTablePageInstance = new WebTablePage(webUtil, currentTest);
        }

        public AddUserForm AddUserFormInstance;
        public WebTablePage WebTablePageInstance;
    }
}