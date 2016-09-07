using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Services.Api;

namespace WoopClient.Services.Api
{
    public class SchlingelApi : ISchlingel
    {
        private readonly IStreamsApi _streams;

        public SchlingelApi()
        {

        }

        public SchlingelApi(IStreamsApi streams)
        {
            _streams = streams;
        }

        public void loadStreams()
        {
            
        }
    }
}
