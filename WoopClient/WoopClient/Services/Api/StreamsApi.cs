using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Models;
using WoopClient.Services.Api;

[assembly: Xamarin.Forms.Dependency(typeof(StreamsApi))]
namespace WoopClient.Services.Api
{
    public class StreamsApi : IStreamsApi
    {

        public async Task<StreamModel[]> GetFavorites()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(ApiConfig.Streams_Favorites);
            if (response.IsSuccessStatusCode)
            {
                return await ParseStreamResponse(response, "streams");
            }
            return null;
        }

        public async Task<StreamModel[]> Search(string query)
        {
            query = WebUtility.UrlEncode(query);
            var client = new HttpClient();
            var response = await client.GetAsync(ApiConfig.Stream_Search + "?search="+query);
            if (response.IsSuccessStatusCode)
            {
                return await ParseStreamResponse(response, "result");
            }
            return null;
        }

        private async Task<StreamModel[]> ParseStreamResponse(HttpResponseMessage response, string streamsProperty)
        {
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            if (json != null && json[streamsProperty].HasValues)
            {
                var result = JsonConvert.DeserializeObject<StreamModel[]>(json[streamsProperty].ToString());
                return result;
            }
            return null;
        }
    }
}
