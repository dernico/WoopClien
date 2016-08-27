using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WoopClient.ViewModels;
using Xamarin.Forms;

namespace WoopClient.Views
{
    public partial class NavigationMenuView : ContentPage
    {
        public NavigationMenuView(NavigationMenuVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
