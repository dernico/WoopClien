using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Common;
using WoopClient.Models;
using WoopClient.Navigation;
using WoopClient.Views;

namespace WoopClient.ViewModels
{
    public class NavigationMenuVM : BaseViewModel
    {

        private readonly INavigation _nav;

        public NavigationMenuVM(INavigation nav)
        {
            _nav = nav;
        }

        private List<NavigationMenuItem> _navigationItems;
        public List<NavigationMenuItem> NavigationItems
        {
            get
            {
                if (_navigationItems == null)
                {
                    _navigationItems = new List<NavigationMenuItem> {
                        new NavigationMenuItem
                        {
                            Title = "Notes",
                            IconSource = "",
                            TargetType = typeof(YouTubeView)
                        }
                    };
                }
                return _navigationItems;
            }
            private set { }
        }

        private NavigationMenuItem _selectedNavigationItem;
        public NavigationMenuItem SelectedNavigationItem
        {
            get
            {
                return _selectedNavigationItem;
            }
            set
            {
                _selectedNavigationItem = value;
                //_navigation.navigatePage(_selectedNavigationItem);
                //_selectedNavigationItem = null;
                //_nav.NavigateTo(_selectedNavigationItem);
                Changed("SelectedNavigationItem");
            }
        }
    }
}
