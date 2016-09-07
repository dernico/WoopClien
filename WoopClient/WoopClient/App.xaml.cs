﻿using Woop.Xamarin.Navigaton;
using Woop.Xamarin.Navigaton.DependencyInjection;
using WoopClient.Services.Api;
using WoopClient.ViewModels;
using WoopClient.ViewModels.Streams;
using WoopClient.Views;
using WoopClient.Views.Streams;
using Xamarin.Forms;
using INavigation = Woop.Xamarin.Navigaton.INavigation;

namespace WoopClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            DependencyResolver.RegisterService<SchlingelApi>();
            DependencyResolver.RegisterService<StreamsApi>();


            var navigation = DependencyResolver.Resolve<INavigation>();

            navigation.RegisterPage<MainPageView, MainPageVM>();
            navigation.RegisterPage<YouTubeView, MainPageVM>();
            navigation.RegisterPage<NavigationMenuView, NavigationMenuVM>();
            navigation.RegisterPage<FavoritesView, FavoritsVM>();
            navigation.RegisterPage<SearchView, SearchVM>();

            navigation.SetMainPage<MainPageView>();
            navigation.SetStartPage<FavoritesView>();
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
