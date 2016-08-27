using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Services.Api;
using WoopClient.Navigation;
using System.Windows.Input;
using WoopClient.Common;
using WoopClient.Views;

namespace WoopClient.ViewModels
{
    public class MainPageVM
    {
        private readonly ISchlingel _schlingel;
        private readonly INavigation _nav;

        public MainPageVM(ISchlingel schlingel, INavigation nav) {
            _schlingel = schlingel;
            _nav = nav;

            Navigate = new ActionCommand((a) => { _nav.NavigateTo<YouTubeView>(); });
        }

        public ICommand Navigate { get; set; }

    }
}
