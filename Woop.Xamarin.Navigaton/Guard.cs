using System;

namespace Woop.Xamarin.Navigaton
{
    public static class Guard
    {
        public static void AssertIsNull(object toAssert)
        {
            if(toAssert == null)
            {
                throw new ArgumentNullException(nameof(toAssert) + " is not allowed to be null");
            }
        }
    }
}
