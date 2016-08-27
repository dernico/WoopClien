using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WoopClient.DependencyInjection;
using WoopClient.Navigation;
using WoopClient.Services.Api;
using WoopClient.ViewModels;
using WoopClient.Views;
using Xamarin.Forms;

namespace WoopClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyResolver.RegisterService<Navigation.Navigation>();
            DependencyResolver.RegisterService<SchlingelApi>();
            DependencyResolver.RegisterService<StreamsApi>();


            Navigation.INavigation navigation = DependencyResolver.Resolve<Navigation.INavigation>();

            navigation.RegisterPage<MainPageView, MainPageVM>();
            navigation.RegisterPage<YouTubeView, MainPageVM>();
            navigation.RegisterPage<NavigationMenuView, NavigationMenuVM>();

            navigation.SetStartPage<YouTubeView>();
            navigation.Init(this);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
