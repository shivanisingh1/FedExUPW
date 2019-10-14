using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace c3i_fedex.Models
{
    public class Indexmodel
    {
        public string accountnumber { get; set; }
       public string shippername { get; set; }
        public string agentname { get; set; }
        public string agetnid { get; set; }
        
        public string case_id { get; set; }

        public string tracking { get; set; }

        public string title { get; set; }

        public string alternate_title { get; set; }

        public string attempts_made { get; set; }

        public DateTime commitment_date { get; set; }

        //public Filterdata FilterItems { get; set; }

        public SelectId SelectData { get; set; }

       public DateTime? follow_up_date {get;set;}
        public string name { get; set; }
        public DateTime   shippers_local_time { get; set; }
        public List<string> Filterdata { get; set; }
        public DateTime assignment { get; set; }
        public string foldername { get; set; }
        //public string attempts_made { get; set; }
        //  public string strdata = "sdfa";
    }

    public enum Filterdata : int
    {
        Attempt_1 = 1,
        Attempt_2 = 2,
        Attempt_3 = 3,
        [Description("4 And Above")]
        _4_and_above
    }

    public class case_notesmail
    {

        public string email_id { get; set; }
       


    }

    public class case_notesdeadline
    {

       
        public DateTime deadline { get; set; }


    }

    public enum SelectId
    {
        //Agent_Id,
        Tracking,
        Case_ID,
       // UTL
    }
    //public enum Filterdata
    //{
    //    1stAttempt,
    //    2stAttempt,
    //    3stAttempt
    //}
    public class AgentcaseDetails
    {
        public string attempts_made { get; set; }
    }

    public class agentnameandid
    {
        public string agent_id { get; set; }
        public string agent_name { get; set; }
    }
}

    