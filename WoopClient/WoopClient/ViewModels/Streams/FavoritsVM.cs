using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Common;
using WoopClient.Models;
using WoopClient.Services.Api;

namespace WoopClient.ViewModels.Streams
{
    public class FavoritsVM : BaseViewModel
    {
        private readonly IStreamsApi _streams;

        public FavoritsVM(IStreamsApi streams)
        {
            _streams = streams;
            Init();
        }

        private async void Init()
        {
            FavoriteStreams = await _streams.GetFavorites();
        }

        private StreamModel[] _favoriteStreams;
        public StreamModel[] FavoriteStreams
        {
            get
            {
                return _favoriteStreams;
            }
            set
            {
                _favoriteStreams = value;
                Changed("FavoriteStreams");
            }
        }
    }
}
