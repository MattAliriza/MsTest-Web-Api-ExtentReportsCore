using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using AventStack.ExtentReports.MarkupUtils;
using HackaThon.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
namespace HackaThon.TestCases
{
    [TestClass]
    public class ApiTask
    {

        [TestCleanup]
        public void CleanUp()
        {
            //In order to be able to execute in Paralell, it can only flush after every test method
            Core.ExtentReport.Flush();
        }

        [TestMethod, TestCategory("Api")]
        public void AllDogBreeds_Test()
        {
            var currentTest = Core.ExtentReport.CreateTest(
                MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", ""),
                "This is a demo test for Locating a value in the Json response."
            );

            //Sets the headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=df3050b40e97ce8431938fa8864b5a07b1593608688");

            //Retrieves the whole list of Dog breeds
            var response = APIUtils.GetRequest(
                "https://dog.ceo/api/breeds/list/all"
                , headers
                );

            //Logs the request
            Core.ExtentReport.LogUrlRequest(currentTest, "https://dog.ceo/api/breeds/list/all", CodeLanguage.Xml);

            //Logs the response in the report
            Core.ExtentReport.LogResponse(currentTest, response.Content, CodeLanguage.Xml);

            //Verifys that 200 response
            if (response.StatusCode != HttpStatusCode.OK)
                Core.ExtentReport.TestFailed(currentTest, "Failed as the response recieved was '" + response.StatusCode + "', but expected 200.");

            //Verify a breed is present
            string breedToSearchFor = "retriever";

            //Verfiy that the response contains the above value
            JObject json = JObject.Parse(response.Content);
            var match = json["message"].Values<JProperty>().Where(m => m.Name == breedToSearchFor).FirstOrDefault();

            if (match == null)
                Core.ExtentReport.TestFailed(currentTest, "Failed as the response did not contain the value '" + breedToSearchFor + "'.");

            //logs that the value was found
            Core.ExtentReport.StepPassed(currentTest, "Successfully located the value '" + breedToSearchFor + "' in the Json response.");

        }

        [TestMethod, TestCategory("Api")]
        public void SubBreeds_Test()
        {
            var currentTest = Core.ExtentReport.CreateTest(
                MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", ""),
                "This is a demo test for injecting a parameter into the URL of a API request."
            );

            //Value of breed to search for
            string breedToSearchFor = "retriever";

            //Sets the headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=df3050b40e97ce8431938fa8864b5a07b1593608688");

            //Retrieves the sub breeds of the above <breedToSearchFor> value
            var response = APIUtils.GetRequest(
                "https://dog.ceo/api/breed/" + breedToSearchFor + "/list"
                , headers
                );

            //Logs the request
            Core.ExtentReport.LogUrlRequest(currentTest, "https://dog.ceo/api/breed/" + breedToSearchFor + "/list", CodeLanguage.Xml);

            //Logs the response in the report
            Core.ExtentReport.LogResponse(currentTest, response.Content, CodeLanguage.Json);

            //Verifys that 200 response
            if (response.StatusCode != HttpStatusCode.OK)
                Core.ExtentReport.TestFailed(currentTest, "Failed as the response recieved was '" + response.StatusCode + "', but expected 200.");

            Core.ExtentReport.StepPassed(currentTest, "Successfully retireved the Json response for the '" + breedToSearchFor + "' sub breeds.");
        }

        [TestMethod]
        public void RandomImage_Test()
        {
            var currentTest = Core.ExtentReport.CreateTest(
               MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", ""),
               "This is a demo test for injecting a parameter into the URL of a API request."
           );

            //Sets the headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=df3050b40e97ce8431938fa8864b5a07b1593608688");

            //Retrieves the whole list of Dog breeds
            var response = APIUtils.GetRequest(
                "https://dog.ceo/api/breeds/image/random"
                , headers
                );


            //Logs the request
            Core.ExtentReport.LogUrlRequest(currentTest, "https://dog.ceo/api/breeds/image/random", CodeLanguage.Xml);

            //Logs the response in the report
            Core.ExtentReport.LogResponse(currentTest, response.Content, CodeLanguage.Json);

            //Verifys that 200 response
            if (response.StatusCode != HttpStatusCode.OK)
                Assert.Fail("Failed as the response recieved was '" + response.StatusCode + "', but expected 200.");

            Core.ExtentReport.StepPassed(currentTest, "Successfully retrieved a random image.");

            //Locating the image url
            JObject json = JObject.Parse(response.Content);
            var imageLocation = json["message"];

            //Rendering the image in the report
            Core.ExtentReport.InjectPictureFrom(currentTest, imageLocation.ToString());
        }

    }
}
