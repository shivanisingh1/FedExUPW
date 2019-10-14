using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class SupervisorModel
    {
        public string case_id { get; set; }

        public string tracking { get; set; }

        public string title { get; set; }

        public string alternate_title { get; set; }

        public string attempts_made { get; set; }

        public DateTime commitment_date { get; set; }

       // public Filterdata FilterItems { get; set; }

        public SelectId2 SelectData { get; set; }

        public DateTime follow_up_date { get; set; }
        //public DateTime   shippers_local_time { get; set; }

        //public string attempts_made { get; set; }

    }

    //public enum Filterdata1
    //{
    //    Attempt_1 = 1, Attempt_2 = 2, Attempt_3 = 3
    //}

    public enum SelectId2
    {
        Agent_Id, Tracking, Case_ID,UTL

    }
    //public enum Filterdata
    //{
    //    1stAttempt,
    //    2stAttempt,
    //    3stAttempt
    //}

}

