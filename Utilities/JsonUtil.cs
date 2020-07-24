using System;
using HackaThon.Models;
using Newtonsoft.Json;

namespace HackaThon.Utilities
{
    public class JsonUtil
    {
        public User getUserInformation(string fake_jsonResponse)
        {
            try
            {
                return JsonConvert.DeserializeObject<User>(fake_jsonResponse);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                throw exc;
            }
        }

    }
}