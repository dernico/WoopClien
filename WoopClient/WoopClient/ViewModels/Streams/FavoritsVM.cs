using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Services.Api;

namespace WoopClient.ViewModels.Streams
{
    public class FavoritsVM
    {
        private readonly IStreamsApi _streams;

        public FavoritsVM(IStreamsApi streams)
        {
            _streams = streams;
            Init();
        }

        private async void Init()
        {
            await _streams.GetFavorites();
        }
    }
}
