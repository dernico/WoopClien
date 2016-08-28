using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Common;
using WoopClient.Models;
using WoopClient.Navigation;
using WoopClient.Views;
using WoopClient.Views.Streams;

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
                            Title = "Streams - Favorites",
                            IconSource = "",
                            TargetType = typeof(FavoritesView)
                        },
                        new NavigationMenuItem
                        {
                            Title = "Streams - Search",
                            IconSource = "",
                            TargetType = typeof(SearchView)
                        },
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
                if(_selectedNavigationItem != null)
                {
                    _nav.NavigateTo(_selectedNavigationItem.TargetType);
                }
                _selectedNavigationItem = null;
                Changed("SelectedNavigationItem");
            }
        }
    }
}
