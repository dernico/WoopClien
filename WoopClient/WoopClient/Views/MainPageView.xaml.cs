using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woop.Xamarin.Navigaton;
using Woop.Xamarin.Navigaton.DependencyInjection;
using WoopClient.ViewModels;
using Xamarin.Forms;

namespace WoopClient.Views
{
    public partial class MainPageView : MasterDetailPage
    {
        public MainPageView()
        {
            InitializeComponent();
            var nav = DependencyResolver.Resolve<Woop.Xamarin.Navigaton.INavigation>();

            var navigationVM = DependencyResolver.CreateInstance<NavigationMenuVM>();
            var navigationMenu = new NavigationMenuView(navigationVM);
            Master = navigationMenu;
            Detail = new NavigationPage() { Title = "Hello"};
        }
    }
}
