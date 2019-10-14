using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class CaseViewModel
    {
        // public string[] chackboxrequirements { set; get; }
        //public string[] requirements { set; get; } btnreactivatetracking   
        public string btnactivatetracking { get; set; }
        public string atemtcount { get; set; }
        public string chkSupervisor { get; set; }
        public string chkhours { get; set; }
        public string chkaccount { get; set; }
        public string btnretrieve { get; set; }
        public string btnreactivatetracking { get; set; }
        public string PackageScandetailsrefresh { get; set; }
        public string btnreverserate { get; set; }
        public string txtemail { get; set; }
        public string txtcompany { get; set; }
        public string txtempname { get; set; }
        public string btnsend { get; set; }
        public string btnsaveandreturn { get; set; }
        public string txtspoke { set; get; }
        public string txtvmail { set; get; }
        public string txtinforeq { set; get; }
        public string txtinforec { set; get; }
        public string txtdeadline { set; get; }
        public string txtattemptsmade { set; get; }
        public string txtfollowupdate { set; get; }

        public string txtphone { set; get; }
        public string txtremarks { set; get; }
        public string datepicker1 { set; get; }
        public string datepicker2 { set; get; }
        public string datepicker3 { set; get; }
        public string cheattempt1 { set; get; }
        public string cheattempt2 { set; get; }
        public string cheattempt3 { set; get; }
        public string btnsave { set; get; }
        //public string input1 { set; get; }
        public string[] box1 { set; get; }
        public DateTime? timer1   { set; get; }
        public DateTime? timer2 { set; get; }
        public DateTime? timer3 { set; get; }
        public string alternatetitle { set; get; }
        public string  CasetoUtl { get; set; }
        public string emailid { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Deadline1 { get; set; }
        public string[] input1 { get; set; }
        public string setpriority { get; set; }
        public string setpriority2 { get; set; }
        public string setpriority3 { get; set; }

        public string send_template { get; set; }
    }
}