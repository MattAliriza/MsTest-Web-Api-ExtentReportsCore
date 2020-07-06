
namespace HackaThon.PageObjects
{
    public class WebTablePage
    {
        public static string table => "//table[@class='smart-table table table-striped']";
        public static string addUserButton => "//button[@class='btn btn-link pull-right']";
        public static string addUserForm => "//div[@class='modal ng-scope']";
        public static string tableUsernameValues => "//tr[@ng-repeat='dataRow in displayedCollection']/td[3]";
        public static string tableFirstLineValues => "//tr[@ng-repeat='dataRow in displayedCollection'][1]/td[not(@class='smart-table-data-cell ng-hide')]";
    }
}