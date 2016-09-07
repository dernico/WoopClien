using System;
using System.Collections.Generic;
using System.Linq;
using Woop.Xamarin.Navigaton.DependencyInjection;
using Xamarin.Forms;

namespace Woop.Xamarin.Navigaton
{
    public class Navigation : INavigation
    {
        private Application _application;
        private readonly Dictionary<Type, Type> _pagesWithViewModels;
        private Type _startPageType;
        private Type _mainPageType;

        public Navigation()
        {
            _pagesWithViewModels = new Dictionary<Type, Type>();
            //Register myself so that others can get injected myself
            DependencyResolver.RegisterService<Navigation>();
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


            var mainPage = _pagesWithViewModels.FirstOrDefault();
            if (_mainPageType != null)
            {
                mainPage = _pagesWithViewModels.FirstOrDefault(p => p.Key == _mainPageType);
                var mainPageView = Activator.CreateInstance(_mainPageType) as Page;
                if (mainPage.Value!= null)
                {
                    var bindingContext = DependencyResolver.Resolve(mainPage.Value);
                    mainPageView.BindingContext = bindingContext;
                }
                _application.MainPage = mainPageView;
            }


            if(_startPageType != null)
            {
                NavigateTo(_startPageType);
            }
        }

        public void SetMainPage<T>() where T : class
        {
            AssertPageExist(typeof(T));

            _mainPageType = typeof(T);
        }

        public void SetStartPage<T>() where T : class
        {
            AssertPageExist(typeof(T));

            _startPageType = typeof(T);
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
        
        public void NavigateTo(Type pageType)
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


        private void AssertPageExist(Type pageType)
        {
            if (!_pagesWithViewModels.Any(p => p.Key == pageType))
            {
                throw new KeyNotFoundException("Could not found the page that you want to set as starting page. Register the Page first.");
            }
        }
                
    }
}
