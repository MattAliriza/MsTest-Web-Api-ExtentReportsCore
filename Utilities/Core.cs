using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json.Linq;

namespace HackaThon.Utilities
{
    public class Core
    {
        public static Reporting ExtentReport = new Reporting();

        public static CodeLanguage CheckIfJson(string responseToCheck)
        {
            try
            {
                var jsonContent = JObject.Parse(responseToCheck);
                return (jsonContent.Count <= 8) ? CodeLanguage.Json : CodeLanguage.Xml; ;
            }
            catch
            {
                return CodeLanguage.Xml;
            }
        }
    }
}