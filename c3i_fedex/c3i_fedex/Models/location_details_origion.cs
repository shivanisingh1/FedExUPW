//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace c3i_fedex.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class location_details_origion
    {
        public string case_id { get; set; }
        public string loc_id { get; set; }
        public string loc_name { get; set; }
        public string local_time { get; set; }
        public Nullable<System.DateTime> express_holidays_date { get; set; }
        public Nullable<System.DateTime> local_date { get; set; }
        public string express_holidays_description { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
        public Nullable<System.DateTime> modification_date { get; set; }
        public string status { get; set; }
        public string location_type { get; set; }
        public long id { get; set; }
        public string TimeZone { get; set; }
        public string dst { get; set; }
        public string city { get; set; }
    
        public virtual case_details case_details { get; set; }
        public virtual case_details case_details1 { get; set; }
    }
}
