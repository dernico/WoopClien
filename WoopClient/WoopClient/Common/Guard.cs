using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoopClient.Common
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
