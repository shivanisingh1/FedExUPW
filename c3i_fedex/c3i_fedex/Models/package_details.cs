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
    
    public partial class package_details
    {
        public string case_id { get; set; }
        public Nullable<System.DateTime> ship_date { get; set; }
        public string pieces { get; set; }
        public string commodity { get; set; }
        public string origin { get; set; }
        public string weight_KG_ { get; set; }
        public string dest { get; set; }
        public string customs_value_USD_ { get; set; }
        public string view_airbill { get; set; }
        public string declared_value { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
        public Nullable<System.DateTime> modification_date { get; set; }
        public string status { get; set; }
        public long id { get; set; }
        public string reverse_rate { get; set; }
    
        public virtual case_details case_details { get; set; }
    }
}
