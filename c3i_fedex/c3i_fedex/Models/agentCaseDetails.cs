using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    [Table("agent_case_details")]
    public class AgentCaseDetails
    {
 
        public string case_id { get; set; }

        public string agent_id { get; set; }

        public int attempts_made { get; set; }
       
    }
}