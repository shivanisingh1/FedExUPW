using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    [Table("agent_details")]
    public class AgentDetails
    {
 
        public string agent_id { get; set; }
        public string name { get; set; }
        public string fedex_id { get; set; }

        public DateTime  date_assigned { get; set; }
        public string queue_type { get; set; }
        public string loc_id { get; set; }
        public string alt_title { get; set; }

        public DateTime creation_date { get; set; }

        public DateTime modification_date { get; set; }

        public string status { get; set; }
        
    }
}