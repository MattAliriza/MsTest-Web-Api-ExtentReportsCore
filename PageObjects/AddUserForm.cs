namespace PageObjects
{
    public class AddUserForm
    {
        public static string firstNameInputField = "//td[text()='First Name']/..//input";
        public static string lastNameInputField = "//td[text()='Last Name']/..//input";
        public static string userNameInputField = "//td[text()='User Name']/..//input";
        public static string passwordInputField = "//td[text()='Password']/..//input";
        public static string customerRadioButton(string customerValue) => "//label[text()='" + customerValue + "']/input";
        public static string roleComboBox = "//select[@name='RoleId']";
        public static string emailInputField = "//td[text()='E-mail']/..//input";
        public static string cellPhoneInputField = "//td[text()='Cell Phone']/..//input";
        public static string saveButton = "//button[text()='Save']";
    }
}