using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WoopClient.Common;
using Xamarin.Forms;

namespace WoopClient.DependencyInjection
{
    public class DependencyResolver
    {
        private static Dictionary<Type, object> _instances = new Dictionary<Type, object>();


        public static void RegisterService<T>() where T : class
        {
            DependencyService.Register<T>();
        }
        
        internal static T Resolve<T>() where T : class
        {
            return (T) Resolve(typeof(T));
        }


        internal static object Resolve(Type resolve)
        {
            return GetInstanceOf(resolve);
        }

        internal static T CreateInstance<T>()
        {
            return (T) GetInstanceOf(typeof(T));
        }

        private static object GetInstanceOf(Type type)
        {
            Guard.AssertIsNull(type);
            
            object instance = null;
            if (!_instances.TryGetValue(type, out instance))
            {

                if (type.GetTypeInfo().IsInterface)
                {
                    instance = GetInstanceForInterface(type);
                }
                else
                {
                    var ctorParams = new List<object>();
                    var ctor = type
                        .GetTypeInfo()
                        .DeclaredConstructors
                        .Where(c => c.GetParameters().Any())
                        .FirstOrDefault();

                    if (ctor != null)
                    {
                        var parameters = ctor.GetParameters();

                        foreach (var p in parameters)
                        {
                            var service = GetInstanceForInterface(p.ParameterType);
                            ctorParams.Add(service);
                        }
                    }
                    instance = Activator.CreateInstance(type, ctorParams.ToArray());
                    _instances.Add(type, instance);
                }

            }
            return instance;
        }

        private static object GetInstanceForInterface(Type interfaceType)
        {
            object service = null;

            //if (parameterInfo.ParameterType == typeof(INavigation))
            //{
            //    return this;
            //}

            if (!_instances.TryGetValue(interfaceType, out service))
            {

                MethodInfo method = typeof(DependencyService)
                                    .GetRuntimeMethods()
                                    .Where(m => m.Name == "Get").FirstOrDefault();

                if (method != null)
                {
                    MethodInfo generic = method.MakeGenericMethod(interfaceType);
                    var fetchTarget = DependencyFetchTarget.GlobalInstance;
                    service = generic.Invoke(null, new object[] { fetchTarget });

                    var type = service.GetType();
                    service = GetInstanceOf(type);
                    _instances.Add(interfaceType, service);
                }
            }

            return service;
        }
    }
}
