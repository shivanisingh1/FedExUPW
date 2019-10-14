
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    [Table("primary_case_details")]
    public class PrimarycaseDetails
    {
        public string case_id { get; set; }

        public string primary_rep_id { get; set; }

       
    }
}