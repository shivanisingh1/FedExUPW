using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class IndexPersistenceVariables
    {

        //IndexPersistenceVariables
        public int count { get; set; }
        public int attempts { get; set; }
        public string stractive { get; set; }
        public int strattempts { get; set; }
        public string cheattempt { get; set; }

        public string timezones { get; set; }
        public string timezone { get; set; }
        public string timezonepath = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + "time\\" + "TimeZone.config";
    }
}