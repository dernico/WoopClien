using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WoopClient.Navigation
{
    public interface INavigation
    {
        void Init(Application formsApplication);
        void SetStartPage<T>() where T : class;
        void ToMainPage();

        void NavigateTo<T>() where T : class;

        void RegisterPage<T, V>() where T : class where V : class;
        void RegisterPage<T>();
        
        
    }
}
