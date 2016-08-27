using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Services.Api;

[assembly: Xamarin.Forms.Dependency(typeof(StreamsApi))]
namespace WoopClient.Services.Api
{
    public class StreamsApi: IStreamsApi
    {
    }
}
