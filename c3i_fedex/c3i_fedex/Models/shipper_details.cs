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
    
    public partial class shipper_details
    {
        public string case_id { get; set; }
        public string account_number { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string company_name { get; set; }
        public string city_state_prov { get; set; }
        public string phone { get; set; }
        public string postal_country { get; set; }
        public string alternate_phone { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
        public Nullable<System.DateTime> modification_date { get; set; }
        public string status { get; set; }
        public long id { get; set; }
    
        public virtual case_details case_details { get; set; }
    }
}
