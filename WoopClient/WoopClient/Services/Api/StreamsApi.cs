using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);
                if (json != null && json["streams"].HasValues)
                {
                    var result = JsonConvert.DeserializeObject<StreamModel[]>(json["streams"].ToString());
                    return result;
                }
            }
            return null;
        }
    }
}
