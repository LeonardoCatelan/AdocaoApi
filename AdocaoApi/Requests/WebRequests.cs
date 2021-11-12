using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdocaoApi.Requests
{
    public static class WebRequests
    {
        public static HttpClient client = new HttpClient();

        public static HttpResponseMessage Post(string Url)
        {
            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(Url)
            };
            return client.SendAsync(message).Result;
        }
    }
}
