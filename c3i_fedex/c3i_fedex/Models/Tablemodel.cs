using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class Tablemodel
    {
        public string Case_ID { get; set; }
        public string Tracking { get; set; }
        public string Title { get; set; }
        public string Alternate { get; set; }
        public DateTime  Commitment { get; set; }
        public DateTime Shipper { get; set; }
        public int Attempts { get; set; }
        public bool Accepted { get; set; }
        public bool NotAccepted { get; set; }


        
                               
        
    }
}