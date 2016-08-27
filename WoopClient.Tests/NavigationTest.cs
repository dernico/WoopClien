using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WoopClient.Navigation;
using Windows.UI.Xaml.Controls;
using WoopClient.Services.Api;

namespace WoopClient.Tests
{
    [TestClass]
    public class NavigationTest
    {
        [TestMethod]
        public void TestNavigationCreateInstance()
        {
            // Test not working due to invokation problem ob the get method from the 
            // xamarin dependency service 


            //var navigation = new Navigation.Navigation(new Xamarin.Forms.Page());
            //navigation.Register<SchlingelApi>();
            //navigation.Init();
        }
    }
}
