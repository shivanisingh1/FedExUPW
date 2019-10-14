using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    [Table("package_details")]
    public class packageDetails
    {
       
        public string case_id { get; set; }

        public DateTime ship_date { get; set; }


        public decimal  pieces { get; set; }

        public DateTime commitment { get; set; }

        public string commodity { get; set; }

        public string origin { get; set; }
        public float weight { get; set; }

        public string dest { get; set; }

        public float customs_value { get; set; }
        public string view_airbill { get; set; }
        public int declared_value { get; set; }

        public DateTime  creation_date { get; set; }

        public DateTime modification_date { get; set; }

        public string status { get; set; }
        
    }
}