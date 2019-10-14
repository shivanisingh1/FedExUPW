using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class Supervisorview
    {
        //agentname  agentfedexid  agentmailid 

        public string agentname { get; set; }
        public string agentfedexid { get; set; }
        public string txtuserid { get; set; }

        public string agentmailid { get; set; }

        public string newpassword { get; set; }
        public string oldpassword { get; set; }
        public string fedex_id { get; set; }
        public string txtserch { get; set; }
        public string [] tblcaseid { get; set; }

        public  string lstajentname { get; set; }

        public string[] tblcaseidarray { get; set; }
    }
}