using System;
using System.Collections.Generic;
using System.Linq;
using Woop.Xamarin.Navigaton.DependencyInjection;
using Xamarin.Forms;

namespace Woop.Xamarin.Navigaton
{
    public class MasterDetailTypes
    {
        public Type Master { get; set; }
        public Type Detail { get; set; }
    }

    public class Navigation : INavigation
    {
        private Application _application;
        private readonly Dictionary<Type, Type> _pagesWithViewModels;
        private readonly Dictionary<Type, MasterDetailTypes> _masterDetailPages;
        private Type _startPageType;
        private Type _mainPageType;

        public Navigation()
        {
            _pagesWithViewModels = new Dictionary<Type, Type>();
            _masterDetailPages = new Dictionary<Type, MasterDetailTypes>();
            // register myself so otheres can get me injected.
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


            if (_mainPageType != null)
            {
                var mainPageView = CreatePage(_mainPageType);
                _application.MainPage = mainPageView;
            }


            if(_startPageType != null)
            {
                NavigateTo(_startPageType);
            }
        }

        public void SetMainPage<TPage>() where TPage : class
        {
            AssertPageExist(typeof(TPage));

            _mainPageType = typeof(TPage);
        }

        public void SetStartPage<TPage>() where TPage : class
        {
            AssertPageExist(typeof(TPage));

            _startPageType = typeof(TPage);
        }

        public void ToMainPage()
        {
            _application.MainPage.Navigation.PopToRootAsync();
        }

        /// <summary>
        /// Navigates to the page from the T value. Navigation source is the Application.MainPage
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        public void NavigateTo<TPage>() where TPage : class
        {
            NavigateTo(typeof(TPage));
        }
        
        public void NavigateTo(Type pageType)
        {
            Page navigationSourcePage = _application.MainPage;
            if (_application.MainPage is MasterDetailPage)
            {
                var masterDetails = _application.MainPage as MasterDetailPage;
                navigationSourcePage = masterDetails.Detail;
            }

            var newPage = CreatePage(pageType);
            if (newPage != null)
            {
                navigationSourcePage.Navigation.PushAsync(new NavigationPage(newPage));
            }
            else
            {
                throw new KeyNotFoundException("Could not navigate to the page. " +
                    "Have you added the page to the Navigation.RegisterPages ?");
            }
        }

        public void RegisterPage<TPage, TViewModel>()
            where TPage : class
            where TViewModel : class
        {
            _pagesWithViewModels.Add(typeof(TPage), typeof(TViewModel));
        }

        public void RegisterPage<TPage>()
        {
            _pagesWithViewModels.Add(typeof(TPage), null);
        }

        public void RegisterMasterDetailPage<TMasterDetailPage, TMaster, TDetail>()
        {
            var masterDetailType = typeof(TMasterDetailPage);
            var masterType = typeof(TMaster);
            var detailType = typeof(TDetail);

            AssertPageExist(masterDetailType);
            AssertPageExist(masterType);
            AssertPageExist(detailType);

            _masterDetailPages.Add(masterDetailType, new MasterDetailTypes {Master = masterType, Detail = detailType });
            
        }

        private Page CreatePage(Type pageType)
        {
            Page page;

            if (_masterDetailPages.ContainsKey(pageType))
            {
                var masterDetail = _masterDetailPages[pageType];
                page = CreateMasterDetailPage(pageType, masterDetail.Master, masterDetail.Detail);
            }
            else
            {
                page = CreatePageWithViewModel(pageType);
            }
            return page;
        }


        private Page CreatePageWithViewModel(Type pageType)
        {
            Page newPage = null;
            Type viewModelType;
            if (_pagesWithViewModels.TryGetValue(pageType, out viewModelType))
            {
                newPage = Activator.CreateInstance(pageType) as Page;
                if (newPage != null && viewModelType != null)
                {
                    newPage.BindingContext = DependencyResolver.Resolve(viewModelType);
                }
            }
            return newPage;
        }

        private MasterDetailPage CreateMasterDetailPage(Type masterDetailType, Type masterType, Type detailType)
        {
            var masterDetailPage = CreatePageWithViewModel(masterDetailType) as MasterDetailPage;
            if (masterDetailPage != null)
            {
                var masterPage = CreatePageWithViewModel(masterType);
                var detailPage = CreatePageWithViewModel(detailType);

                masterDetailPage.Master = masterPage;
                masterDetailPage.Detail = new NavigationPage( detailPage );
            }
            return masterDetailPage;
        }

        private void AssertPageExist(Type pageType)
        {
            if (!_pagesWithViewModels.Any(p => p.Key == pageType))
            {
                throw new KeyNotFoundException("Could not found the page that you want to set. Register the Page first.");
            }
        }
                
    }
}
