using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PactDllCopyTest
{
    public class Client
    {
        private readonly HttpClient _client;

        public Client(Uri baseUri)
        {
            _client = new HttpClient { BaseAddress = baseUri };
        }

        public async Task<IsOddObject> IsOdd(int number)
        {
            string formattedUrl = $"/api/IsOdd?value={number}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, formattedUrl);
            request.Headers.Add("Accept", "application/json");

            HttpResponseMessage response = await _client.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();

            request.Dispose();
            response.Dispose();

            return JsonConvert.DeserializeObject<IsOddObject>(content);
        }
    }

    public struct IsOddObject
    {
        public bool isOdd;
    }
}
