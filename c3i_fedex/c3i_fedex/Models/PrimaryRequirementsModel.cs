using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class PrimaryRequirementsModel
    {
        public string case_id { get; set; }
        public string case_requirements { get; set; }
        public bool is_received { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
        public Nullable<System.DateTime> modification_date { get; set; }
        public string status { get; set; }
        public long id { get; set; }

        public virtual case_details case_details { get; set; }
    }
}