using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace c3i_fedex.Models
{
    public class ViewModel
    {
        public List<string > filterdata1 { get; set; }
        public ViewModel()
        {
            filterdata1 = new List<string>()
            {
                //Attempt_1
                { "Attempt 1"},
                { "Attempt 2"},
                { "Attempt 3"},
                { "Attempt 4 and more"}
               
            };
        }
        public List<string> shippersname { get; set; }
        // public List<Indexmodel> shippersname { get; set; }
        public Filterdata FilterItems { get; set; }
        public SelectId2 SelectData { get; set; }
        public int Newly_Assigned { set; get; }
        public string newactive { set; get; }
        public string allactive { set; get; }
        public string wipactive { set; get; }
        public string utlactive { set; get; }
        public string gedaactive { set; get; }
        public string undersupervisor { set; get; }
        public string blockedaccounts { set; get; }
        public string hours24 { set; get; }
        public int All { set; get; }
        public int WIP { set; get; }
        public int UTL { set; get; }
        public int GEDA { set; get; }
        public int supervisor { set; get; }
        public int accounts { set; get; }
        public int hours { set; get; }



        public string origin { get; set; }
        public string dest { get; set; }
      
        public DateTime origindatetime { get; set; }

        public DateTime destdatetime { get; set; }
        public string locttime { get; set; }
        public int gedanotificationcount { get; set; }

        // public string origin { get; set; }
        public List<geda_notifications > gedanotifications { get; set; }
        public List<location_details_dest> locationdetailsdest { get; set; }
        public List<location_details_origion> locationdetailsorigion { get; set; }
        public List<agent_case_attempts_made> agentcaseattemptsmade { set; get; }
        public List<Indexmodel> indexmodel { get; set; }
        public List<folder_details> folderdetails { get; set; }
        public List<folder_ids> folderids { get; set; }
        public List<currency_codes> currencycodes { get; set; }
        public List<send_temp > sendtemp { get; set; }
        //public string objpackage { get; set; }
        public DataTable dttable { set; get; }
        public List<case_details> casedatils { get; set; }
        public List<case_details> casedatilsnw { get; set; }
        public List<case_details> casedatilsu { get; set; }
        public List<case_details> casedatilsw { get; set; }
        public List<case_details> casedatilsall { get; set; }
        public List<case_details> casedatilsGEDA { get; set; }
        public List<case_details> casedatilsfax { get; set; }
        public List<package_details> packageDetails { get; set; }
        public List<shipper_details> ShipperDetails { get; set; }
        public List<primary_case_details> primarycaseDetails { get; set; }
        public List<primary_rep_details> PrimaryRepDetails { get; set; }
        public List<recipient_details> RecipientDetails { get; set; }
        public List<agent_details> agentDetails { get; set; }
        public List<agent_case_details> agentCaseDetails { get; set; }
        public List<agentiddetails> agentiddetails { get; set; }
        public List<package_scan_details> packagescandetails { get; set; }
        public List<case_notesmail> casenotesmail { get; set; }
      //  public List<us_holidays> usholidays { get; set; }
        public List<case_notesdeadline> case_notesdeadline { get; set; }

        public List<alternate_phone_numbers > alternatephonenumbers { get; set; }
        public DateTime casenotedeadline { get; set; }
        //case_notesdeadline
        public List<case_notes> casenotes { get; set; }
        public string casenote { get; set; }
        
        public IEnumerable<AddCasenotesModel> addcasen { get; set; }
        public List<add_case_notes> addcasenotes { get; set; }

        public List<primary_requirements> primaryrequirements { get; set; }

        public List<agentnameandid> agentnameandid { get; set; }
    }

  
}