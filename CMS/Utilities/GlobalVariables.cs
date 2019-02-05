using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Utilities
{
    public static class GlobalVariables
    {
        public static bool IS_AUTHENTICATED = false;
        public static int USER_ID=0;
        public static string USERNAME = "";
        public static int PAGE_SIZE = 10;
    }
}