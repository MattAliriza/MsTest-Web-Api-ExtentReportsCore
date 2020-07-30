using System;
using System.Net;
using AventStack.ExtentReports;
using HackaThon.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace HackaThon.Models
{
    public class RestResponseModel
    {
        public IRestResponse responseData { get; set; }
        public string endPoint { get; set; }
        public TimeSpan duration { get; set; }
        public HttpStatusCode statusCode { get; set; }


        public void validateResponseIs(HttpStatusCode expectedHttpsStatus, ExtentTest currentTest)
        {
            //Verifys that 200 response
            if (statusCode != expectedHttpsStatus)
            {
                Core.ExtentReport.TestFailed(currentTest, "Failed as the response recieved was '" + statusCode + "', but expected 200.");
                Assert.Fail("Failed as the response recieved was '" + statusCode + "', but expected " + expectedHttpsStatus + ".");
            }

            Core.ExtentReport.StepPassed(currentTest, "Successfully validated that the response was " + expectedHttpsStatus + ".");
        }
    }
}