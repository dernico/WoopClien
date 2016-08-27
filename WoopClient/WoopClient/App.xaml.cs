using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            Navigation.INavigation navigation = new Navigation.Navigation(this);

            navigation.RegisterPages<MainPageView, MainPageVM>();
            navigation.RegisterPages<YouTubeView, MainPageVM>();

            navigation.RegisterService<SchlingelApi>();
            navigation.RegisterService<StreamsApi>();
            navigation.Init();
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
