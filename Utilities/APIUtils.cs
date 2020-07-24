using System.Collections.Generic;
using RestSharp;

namespace HackaThon.Utilities
{
    public class APIUtils
    {

        private string _baseUrl;
        private IRestResponse _response;

        public APIUtils(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public IRestResponse GetRequest(string EndPoint, Dictionary<string, string> Headers)
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
            _response = client.Execute(request);

            //Returns the response
            return _response;
        }
    }
}