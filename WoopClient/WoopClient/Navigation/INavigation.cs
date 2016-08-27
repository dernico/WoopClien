using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoopClient.Navigation
{
    public interface INavigation
    {
        void Init();
        void ToMainPage();
        void NavigateTo<T>() where T : class;
        void RegisterPages<T, V>() where T : class where V : class;
        void RegisterService<T>() where T : class;
        void RegisterPages<T>();
    }
}
