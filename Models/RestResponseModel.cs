using System;
using System.Linq;
using System.Net;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using HackaThon.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HackaThon.Models
{
    public class RestResponseModel
    {
        public string responseData { get; set; }
        public string endPoint { get; set; }
        public TimeSpan duration { get; set; }
        public HttpStatusCode statusCode { get; set; }


        public void ValidateResponseIs(HttpStatusCode expectedHttpsStatus, ExtentTest currentTest)
        {
            //Verifys that 200 response
            if (statusCode != expectedHttpsStatus)
            {
                Core.ExtentReport.TestFailed(currentTest, "Failed as the response recieved was '" + statusCode + "', but expected 200.");
                Assert.Fail("Failed as the response recieved was '" + statusCode + "', but expected " + expectedHttpsStatus + ".");
            }

            Core.ExtentReport.StepPassed(currentTest, "Successfully validated that the response was " + expectedHttpsStatus + ".");
        }

        public void ValidateResponseIsJson(string response, ExtentTest currentTest)
        {
            try
            {
                var jsonContent = JObject.Parse(response);
                Core.ExtentReport.StepPassed(currentTest, "Successfully retireved a Json response.");
            }
            catch (Exception exc)
            {
                Core.ExtentReport.TestFailed(currentTest, "Failed due to the response not being Json format.");
                throw exc;
            }
        }

        public void ValidateResponseContains(string expectedValue, ExtentTest currentTest)
        {
            //Verfiy that the response contains the above value
            JObject json = JObject.Parse(responseData.ToString());
            var match = json["message"].Values<JProperty>().Where(m => m.Name == expectedValue).FirstOrDefault();

            if (match == null)
                Core.ExtentReport.TestFailed(currentTest, "Failed as the response did not contain the value '" + expectedValue + "'.");

        }
    }
}