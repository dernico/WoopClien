using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WoopClient.Common;
using WoopClient.Models;
using WoopClient.Services.Api;

namespace WoopClient.ViewModels.Streams
{
    public class SearchVM : BaseViewModel
    {

        private readonly IStreamsApi _streams;

        public SearchVM(IStreamsApi streams) {
            _streams = streams;

            SearchCommand = new ActionCommand(Search);
        }

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                Changed("SearchText");
            }
        }


        private StreamModel[] _searchResult;
        public StreamModel[] SearchResult
        {
            get
            {
                return _searchResult;
            }
            set
            {
                _searchResult = value;
                Changed("SearchResult");
            }
        }

        public ICommand SearchCommand { get; set; }

        private async void Search(object p)
        {
            SearchResult = await _streams.Search(SearchText);
        }
    }
}
