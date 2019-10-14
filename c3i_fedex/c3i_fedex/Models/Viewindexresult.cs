
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class Viewindexresult
    {
        public string shippername { get; set; }

        public string account_number { get; set; }
        public string fedex_id { get; set; }
        public string[] tblcaseid { get; set; }
        public string txtSearch { set; get; }
        public string SelectData { set; get; }
        public string FilterItems { set; get; }
        public string all_case { set; get; }
        public string new_ass { set; get; }
        public string Wip { set; get; }
        public string Utl { set; get; }
        public string Geda { set; get; }
        public string Faxcom { set; get; }
        public string folder { set; get; }
    }
}