using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    [Table("recipient_details")]
    public class RecipientDetails
    {
 
        public string case_id { get; set; }
        public string account_number { get; set; }
        public string address1 { get; set; }

        public string name { get; set; }
        public string address2 { get; set; }

        public string company_name { get; set; }
        public string city_state_prov { get; set; }
        public string phone { get; set; }
        public string postal_country { get; set; }

        public string alternate_phone { get; set; }
        public DateTime creation_date { get; set; }

        public DateTime modification_date { get; set; }

        public string status { get; set; }
        
        
    }
}