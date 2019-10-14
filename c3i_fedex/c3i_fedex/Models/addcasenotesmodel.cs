using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class AddCasenotesModel
    {

        public string case_id { get; set; }
        public string trigger_phone { get; set; }
        public string spoke_to { get; set; }
        public string left_vmail { get; set; }
        public string info_req { get; set; }
        public string info_rec { get; set; }
        public string tracking { get; set; }
        public string deadline { get; set; }
        public string attempts_made { get; set; }
        public DateTime follow_up_date { get; set; }
        public string Remarks { get; set; }
        public DateTime  creation_date { get; set; }
        public DateTime modification_date { get; set; }
        public string status { get; set; }

    }
}