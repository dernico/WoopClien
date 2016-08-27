using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WoopClient.Common;
using WoopClient.DependencyInjection;
using Xamarin.Forms;

namespace WoopClient.Navigation
{
    public class Navigation : INavigation
    {
        private Application _application;
        private Dictionary<Type, Type> _pagesWithViewModels;
        private Type _startPage;

        public Navigation()
        {
            _pagesWithViewModels = new Dictionary<Type, Type>();
        }
        

        public void Init(Application formsApplication)
        {
            Guard.AssertIsNull(formsApplication);

            _application = formsApplication;

            if (!_pagesWithViewModels.Any())
            {
                throw new MissingMemberException("No Page was found, there must be at least one page. " +
                    "Make sure to call Navigation.RegisterPage at least once.");
            }

            var firstPage = _pagesWithViewModels.FirstOrDefault();
            var mainPageView = Activator.CreateInstance(firstPage.Key) as Page;
            if (firstPage.Key != null)
            {
                var bindingContext = DependencyResolver.Resolve(firstPage.Value);
                mainPageView.BindingContext = bindingContext;
            }

            _application.MainPage = new NavigationPage(mainPageView);

            if(_startPage != null)
            {
                NavigateTo(_startPage);
            }
        }

        public void SetStartPage<T>() where T : class
        {
            if(!_pagesWithViewModels.Any(p => p.Key == typeof(T)))
            {
                throw new KeyNotFoundException("Could not found the page that you want to set as starting page. Register the Page first.");
            }
            _startPage = _pagesWithViewModels.FirstOrDefault(p => p.Key == typeof(T)).Key;
        }

        public void ToMainPage()
        {
            _application.MainPage.Navigation.PopToRootAsync();
        }
        
        /// <summary>
        /// Navigates to the page from the T value. Navigation source is the Application.MainPage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void NavigateTo<T>() where T : class
        {
            NavigateTo(typeof(T));
        }
        
        private void NavigateTo(Type pageType)
        {
            Page navigationSourcePage = _application.MainPage;
            if (_application.MainPage is MasterDetailPage)
            {
                var masterDetails = _application.MainPage as MasterDetailPage;
                navigationSourcePage = masterDetails.Detail;
            }

            Type viewModelType = null;
            if (_pagesWithViewModels.TryGetValue(pageType, out viewModelType))
            {
                var newPage = Activator.CreateInstance(pageType) as Page;
                if (viewModelType != null)
                {
                    newPage.BindingContext = DependencyResolver.Resolve(viewModelType);
                }
                navigationSourcePage.Navigation.PushAsync(new NavigationPage(newPage));
            }
            else
            {
                throw new KeyNotFoundException("Could not navigate to the page. " +
                    "Have you added the page to the Navigation.RegisterPages ?");
            }
        }

        public void RegisterPage<T, V>()
            where T : class
            where V : class
        {
            _pagesWithViewModels.Add(typeof(T), typeof(V));
        }

        public void RegisterPage<T>()
        {
            _pagesWithViewModels.Add(typeof(T), null);
        }
                
    }
}
