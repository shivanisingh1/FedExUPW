using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    [Table("primary_rep_details")]
    public class PrimaryRepDetails
    {

 
        public string primary_rep_id { get; set; }

        public string name { get; set; }
        public string fedex_id { get; set; }

        public string queue_type { get; set; }
       
        public DateTime date_assigned { get; set; }
        public string loc_id { get; set; }
        public string email_id { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime modification_date { get; set; }

        public string status { get; set; }
        
    }
}