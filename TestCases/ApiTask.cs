using System.Collections.Generic;
using System.Net;
using System.Reflection;
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

            APIUtils apiInstance = new APIUtils("https://dog.ceo");

            //Sets the headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=df3050b40e97ce8431938fa8864b5a07b1593608688");

            //Retrieves the whole list of Dog breeds
            var response = apiInstance.GetRequest(
                "/api/breeds/list/all"
                , headers
                , currentTest
                );

            //Verifys that 200 response
            response.ValidateResponseIs(HttpStatusCode.OK, currentTest);
            response.ValidateResponseIsJson(response.responseData, currentTest);

            //Verify a breed is present
            string breedToSearchFor = "retriever";
            response.ValidateResponseContains(breedToSearchFor, currentTest);

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

            APIUtils apiInstance = new APIUtils("https://dog.ceo");

            //Value of breed to search for
            string breedToSearchFor = "retriever";

            //Sets the headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=df3050b40e97ce8431938fa8864b5a07b1593608688");

            //Retrieves the sub breeds of the above <breedToSearchFor> value
            var response = apiInstance.GetRequest(
                "/api/breed/" + breedToSearchFor + "/list"
                , headers
                , currentTest
                );

            //Verifys that 200 response
            response.ValidateResponseIs(HttpStatusCode.OK, currentTest);
            response.ValidateResponseIsJson(response.responseData, currentTest);
        }

        [TestMethod]
        public void RandomImage_Test()
        {
            var currentTest = Core.ExtentReport.CreateTest(
               MethodBase.GetCurrentMethod().ToString().Replace("Void", "").Replace("()", ""),
               "This is a demo test for injecting a parameter into the URL of a API request."
           );

            APIUtils apiInstance = new APIUtils("https://dog.ceo");

            //Sets the headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Cookie", "__cfduid=df3050b40e97ce8431938fa8864b5a07b1593608688");

            //Retrieves the whole list of Dog breeds
            var response = apiInstance.GetRequest(
                "/api/breeds/image/random"
                , headers
                , currentTest
                );

            //Verifys that 200 response
            response.ValidateResponseIs(HttpStatusCode.OK, currentTest);
            response.ValidateResponseIsJson(response.responseData, currentTest);

            //Locating the image url
            JObject json = JObject.Parse(response.responseData.ToString());
            var imageLocation = json["message"];

            //Rendering the image in the report
            Core.ExtentReport.InjectPictureFrom(currentTest, imageLocation.ToString());
        }
    }
}
