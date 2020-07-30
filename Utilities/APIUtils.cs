using System;
using System.Collections.Generic;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using HackaThon.Models;
using RestSharp;

namespace HackaThon.Utilities
{
    public class APIUtils
    {
        private string _baseUrl;
        public APIUtils(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public RestResponseModel GetRequest(string EndPoint, Dictionary<string, string> Headers, ExtentTest currentTest)
        {
            //Creating the Http client
            var client = new RestClient(_baseUrl + EndPoint);

            //Sets the time out
            client.Timeout = -1;

            //Creates the request type
            var request = new RestRequest(Method.GET);

            //Adds headers if there are any
            if (Headers != null)
            {
                foreach (KeyValuePair<string, string> header in Headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            //Executes the request & saves it
            DateTime timeStarted = DateTime.Now;
            IRestResponse _response = client.Execute(request);
            DateTime timeEnded = DateTime.Now;

            //Logs the request
            Core.ExtentReport.LogUrlRequest(currentTest, this._baseUrl + EndPoint, CodeLanguage.Xml);

            //Logs the response in the report
            Core.ExtentReport.LogResponse(currentTest, _response.Content, CodeLanguage.Json);


            RestResponseModel apiModel = new RestResponseModel()
            {
                endPoint = EndPoint,
                responseData = _response,
                duration = timeEnded - timeStarted,
                statusCode = _response.StatusCode
            };

            //Returns the response
            return apiModel;
        }
    }
}