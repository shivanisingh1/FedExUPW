using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.utilities
{
    public class CaseviewUtilities
    {
        public string stringsplit(string str)
        {
            string[] strsplt = str.Split(new Char[] { ',' });


            return strsplt[0];
        }
    }
}