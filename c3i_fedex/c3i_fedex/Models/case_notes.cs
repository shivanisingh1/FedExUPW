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
    
    public partial class case_notes
    {
        public string case_id { get; set; }
        public string case_notes1 { get; set; }
        public Nullable<System.DateTime> creation_date { get; set; }
        public Nullable<System.DateTime> modification_date { get; set; }
        public string status { get; set; }
        public long id { get; set; }
        public Nullable<bool> send_to { get; set; }
        public string email_id { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
    
        public virtual case_details case_details { get; set; }
    }
}
