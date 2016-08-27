using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WoopClient.Common;
using Xamarin.Forms;

namespace WoopClient.Navigation
{
    public class Navigation : INavigation
    {
        private Application _application;
        private Dictionary<Type, Type> _pagesWithViewModels;
        private Dictionary<Type, object> _instances;

        public Navigation(Application formsApplication)
        {
            Guard.AssertIsNull(formsApplication);

            _application = formsApplication;
            _instances = new Dictionary<Type, object>();
            _pagesWithViewModels = new Dictionary<Type, Type>();
        }

        public void Init()
        {
            if (!_pagesWithViewModels.Any())
            {
                throw new MissingMemberException("No Page was found, there must be at least one page. " +
                    "Make sure to call Navigation.RegisterPage at least once.");
            }

            var firstPage = _pagesWithViewModels.FirstOrDefault();
            var mainPageView = Activator.CreateInstance(firstPage.Key) as Page;
            if (firstPage.Key != null)
            {
                var bindingContext = GetInstanceOf(firstPage.Value);
                mainPageView.BindingContext = bindingContext;
            }

            _application.MainPage = new NavigationPage(mainPageView);
        }

        public void ToMainPage()
        {
            _application.MainPage.Navigation.PopToRootAsync();
        }

        public void NavigateTo<T>() where T : class
        {
            var pageType = typeof(T);
            Type viewModelType = null;
            if (_pagesWithViewModels.TryGetValue(pageType, out viewModelType))
            {
                var page = Activator.CreateInstance(pageType) as Page;
                if (viewModelType != null)
                {
                    page.BindingContext = GetInstanceOf(viewModelType);
                }
                _application.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                throw new KeyNotFoundException("Could not navigate to the page. " +
                    "Have you added the page to the Navigation.RegisterPages ?");
            }
        }

        public void RegisterPages<T, V>()
            where T : class
            where V : class
        {
            _pagesWithViewModels.Add(typeof(T), typeof(V));
        }

        public void RegisterPages<T>()
        {
            _pagesWithViewModels.Add(typeof(T), null);
        }

        public void RegisterService<T>() where T : class
        {
            DependencyService.Register<T>();
        }

        private object GetInstanceOf(Type type)
        {
            Guard.AssertIsNull(type);

            //var name = type.FullName;

            object instance = null;
            if (!_instances.TryGetValue(type, out instance))
            {
                var ctor = type
                    .GetTypeInfo()
                    .DeclaredConstructors
                    .Where(c => c.GetParameters().Any())
                    .FirstOrDefault();

                var ctorParams = new List<object>();
                if (ctor != null)
                {
                    var parameters = ctor.GetParameters();

                    foreach (var p in parameters)
                    {
                        var service = GetInstanceOfParameter(p);
                        ctorParams.Add(service);
                    }
                }

                instance = Activator.CreateInstance(type, ctorParams.ToArray());

                _instances.Add(type, instance);
            }
            return instance;
        }

        private object GetInstanceOfParameter(ParameterInfo parameterInfo)
        {
            object service = null;

            if (parameterInfo.ParameterType == typeof(INavigation))
            {
                return this;
            }

            if (!_instances.TryGetValue(parameterInfo.ParameterType, out service))
            {

                MethodInfo method = typeof(DependencyService)
                                    .GetRuntimeMethods()
                                    .Where(m => m.Name == "Get").FirstOrDefault();

                if (method != null)
                {
                    MethodInfo generic = method.MakeGenericMethod(parameterInfo.ParameterType);
                    var fetchTarget = DependencyFetchTarget.GlobalInstance;
                    service = generic.Invoke(null, new object[] { fetchTarget });

                    var type = service.GetType();
                    service = GetInstanceOf(type);
                    _instances.Add(parameterInfo.ParameterType, service);
                }
            }

            return service;
        }
    }
}
