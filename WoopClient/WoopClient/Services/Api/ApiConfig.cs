using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoopClient.Services.Api
{
    public class ApiConfig
    {
        private const string BaseUrl = "http://woop:8000/";

        internal const string Streams_Favorites = BaseUrl + "api/music/streams";
        internal const string Stream_Search = BaseUrl + "api/tunein/search";
    }
}
