using c3i_fedex.Models;
using c3i_fedex.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace c3i_fedex.services
{
    public class Indexservices
    {
        IndexPersistenceVariables allvariables = new IndexPersistenceVariables();
        ViewModel objvm = new ViewModel();
       // ViewModel objvm = new ViewModel();
        shippersname shippersname = new shippersname();
        //public static List<T> ConvertToList<T>(DataTable dt)
        //{
        //    var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
        //    var properties = typeof(T).GetProperties();
        //    return dt.AsEnumerable().Select(row => {
        //        var objT = Activator.CreateInstance<T>();
        //        foreach (var pro in properties)
        //        {
        //            if (columnNames.Contains(pro.Name.ToLower()))
        //            {
        //              try
        //                {
        //                    pro.SetValue(objT, Convert.ToString(row[pro.Name]));
        //                }
        //                catch (Exception ex) {

        //                }
        //            }
        //        }
        //        return objT;
        //    }).ToList();
        //}

        // string strattempts = null;
        // string cheattempt = null;

        public int selectattempts(string attempts)
        {
            if (attempts == "Attempt 1")
            {
                allvariables.strattempts = 1;
            }
            else if (attempts == "Attempt 2")
            {
                allvariables.strattempts = 2;
            }
            else if (attempts == "Attempt 3")
            {
                allvariables.strattempts = 3;
            }
            else if (attempts == "Attempt 4 and more")
            {
                allvariables.strattempts = 4;
            }

            return allvariables.strattempts;
        }

        
        //select attempts 
        public string selectattempt(string cheattempt1,string cheattempt2,string cheattempt3)
        {
            if (cheattempt1 == "on" || cheattempt1 == "ON")
            {
                allvariables.cheattempt = "1";
            }
            if (cheattempt2 == "on" || cheattempt2 == "ON")
            {
                allvariables.cheattempt = "2";
            }
            if (cheattempt3 == "on" || cheattempt3 == "ON")
            {
                allvariables.cheattempt = "3";
            }
           

            return allvariables.cheattempt;
        }
        //change the number to utl
        public string attemptsmodecahnge(string attemps)
        {
            if (attemps.Trim() == "-51")
            {
                attemps = "UTL";

            }
            else if (attemps.Trim() == "-52")
            {
                attemps = "Under Supervisor";

            }
            else if (attemps.Trim() == "-53")
            {
                attemps = "24 Hours";

            }
            else if (attemps.Trim() == "-54")
            {
                attemps = "Blocked Accounts";

            }



            return attemps;
        }
        //clear  activites
        public void clearactivites()
        {
            objvm.allactive = "";
            objvm.newactive = "";
            objvm.utlactive = "";
            objvm.wipactive = "";
            objvm.gedaactive = "";
            objvm.undersupervisor  = "";
            objvm.blockedaccounts  = "";
            objvm.hours24  = "";
        }

        //Select which folder is activetd
        public string  folderactive(string folder)
        {
            allvariables.stractive = null ;
            if(folder ==""|| folder==null)
            {
                clearactivites();
                allvariables.stractive = "ALL";
            }
            else if(folder== "New")
            {
                clearactivites();
                //objvm.allactive = "active";
                allvariables.stractive = "New";
            }
            else if (folder == "WIP")
            {
                clearactivites();
                //objvm.allactive = "active";
                allvariables.stractive = "WIP";
            }
            else if (folder == "UTL")
            {
                clearactivites();
                //objvm.allactive = "active";
                allvariables.stractive = "UTL";
            }
            else if (folder == "Undersupervisor")
            {
                clearactivites();
                //objvm.allactive = "active";
                allvariables.stractive = "Undersupervisor";
            }
            else if (folder == "Blockedaccounts")
            {
                clearactivites();
                //objvm.allactive = "active";
                allvariables.stractive = "Blockedaccounts";
            }
            else if (folder == "24hours")
            {
                clearactivites();
                //objvm.allactive = "active";
                allvariables.stractive = "24hours";
            }
            else if (folder == "GEDA")
            {
                clearactivites();
                //objvm.allactive = "active";
                allvariables.stractive = "GEDA";
            }
            return allvariables.stractive;
        }
        //int attemts = 0;
        //convert int attempt 
        public int selectattempt(string cheattempt)
        {
            allvariables.attempts = 0;
            if (cheattempt == "on" || cheattempt == "ON")
            {
                allvariables.attempts = 1;
            }
            
            return allvariables.attempts;
        }
        //insert a date  primary requirements table 
        public void caseviewrequirements(string case_id, string[] requirements,string [] chackboxrequirements)
        {
            CaseviewPersistence caseviewpersistence = new CaseviewPersistence();
            for (int i = 0; i < requirements.Length; i++)
            {
                int itm = 0;
                if (requirements[i] != "")
                {
                    if (chackboxrequirements != null)
                    {

                        
                        if (chackboxrequirements.Contains(i.ToString()) )
                        {
                         // int requirement = selectattempt(chackboxrequirements[i]);
                          itm = 1;
                          string strqu = "'" + case_id + "','" + requirements[i].Trim() + "','" +true + "','" + DateTime.Now + "','" + DateTime.Now + "')";
                          caseviewpersistence.insertprimarytable(strqu ,itm);
                        
                        }
                        else
                        {
                             itm = 2;
                             string strqu = "'" + case_id + "' ,'" + requirements[i] + "' ,'" + false + "' ,'" + DateTime.Now + "','" + DateTime.Now + "')";
                             caseviewpersistence.insertprimarytable(strqu,itm);
                            //ctx.Database.ExecuteSqlCommand(strqu);
                        }
                    }
                    else
                    {
                        itm = 3;
                        string strqu = "'" + case_id + "' ,'" + requirements[i] + "' ,'" + false + "' ,'" + DateTime.Now + "','" + DateTime.Now + "')";
                        caseviewpersistence.insertprimarytable(strqu, itm);
                    }

                }
                //if(amounts[i] != "" && chackbox[i] == null)
                //{
                //    //int attemts = indexservices.selectattempt(chackbox[i]);

                //}


            }
        }
        //insert a DATA agent_case_attempts_made AND add_case_notes ,update follow_up_date as case_details table       
        public void insertcasenotes(string case_id, string agent_id, CaseViewModel caseviewmodel)
        {
            CaseviewPersistence caseviewpersistence = new CaseviewPersistence();
            DateTime dt = new DateTime();
            DateTime dt1 = new DateTime();
            string attempt = selectattempt(caseviewmodel.cheattempt1, caseviewmodel.cheattempt2, caseviewmodel.cheattempt3);
            int priority = 0;
            if (caseviewmodel.setpriority == "1")
                priority = 1;
            if (caseviewmodel.timer1 == null) { }
            else
            {
                string date = caseviewmodel.timer1?.ToString("hh:mm tt");
                caseviewmodel.datepicker1 = caseviewmodel.datepicker1 + " " + date;
                dt = Convert.ToDateTime(caseviewmodel.datepicker1);
                if (caseviewmodel.setpriority == "1")
                    priority = 1;

            }
            if (caseviewmodel.timer2 == null) { }
            else
            {
                string date = caseviewmodel.timer2?.ToString("hh:mm tt");
                caseviewmodel.datepicker2 = caseviewmodel.datepicker2 + " " + date;
                dt = Convert.ToDateTime(caseviewmodel.datepicker2);
                if (caseviewmodel.setpriority2 == "1")
                    priority = 1;
            }
            if (caseviewmodel.timer3 == null) { }
            else
            {
                string date = caseviewmodel.timer3?.ToString("hh:mm tt");
                caseviewmodel.datepicker3 = caseviewmodel.datepicker3 + " " + date;
                dt = Convert.ToDateTime(caseviewmodel.datepicker3);
                if (caseviewmodel.setpriority3== "1")
                    priority = 1;

            }
            if (caseviewmodel.txtfollowupdate != null)
               dt1= Convert.ToDateTime(caseviewmodel.txtfollowupdate);


            AddCasenotesModel addcase = caseviewpersistence.selecttrigerandtraking(case_id );   //insertsaveandupdate        

            if (priority != 0)
                caseviewpersistence.updatecasedetailspriority(case_id, priority);

            //insertprimarytable
            if (caseviewmodel.txtremarks != null)
            {
                if (attempt != null)
                {

                     int itm1 = 1;
                    string strqu1 = "'" + case_id + "' ,'" + agent_id + "' ,'" + attempt + "' ,'" + dt + "' ,'" + priority + "')";
                    //string strqu1=
                    caseviewpersistence.insertsaveandupdate(strqu1, itm1, case_id, caseviewmodel.txtfollowupdate);
                    

                }
                if(caseviewmodel.Deadline1 != Convert.ToDateTime ("1/1/0001 12:00:00 AM"))
                {
                    caseviewpersistence.updatedeadline(case_id, caseviewmodel.Deadline1);
                }
               
                int itm = 2;
                int attempts = caseviewpersistence.attemptsmadecount(case_id);
                if (attempts == -51) { }
                else if(caseviewmodel.atemtcount!=null)
                {
                    caseviewpersistence.updatecasetable(attempts + 1, case_id);
                }
                if (caseviewmodel.atemtcount != null)
                {
                    //addcase.trigger_phone---->caseviewmodel.txtphone
                    attempts = attempts + 1;
                    string strqu = "'" + case_id + "' ,'" + caseviewmodel.txtphone + "' ,'" + caseviewmodel.txtspoke +
                    "' ,'" + caseviewmodel.txtvmail + "','" + caseviewmodel.txtinforeq + "','" + caseviewmodel.txtinforec + "','" + addcase.tracking + "','" + caseviewmodel.txtdeadline
                   + "','" + attempts + "','" + dt1 + "','" + caseviewmodel.txtremarks + "','" + DateTime.Now + "','" + DateTime.Now + "','" + agent_id + "')";


                    caseviewpersistence.insertsaveandupdate(strqu, itm, case_id, caseviewmodel.txtfollowupdate);
                }else if(caseviewmodel.txtremarks!= null)
                {
                    string strqu= "'" + case_id + "' ,'" + caseviewmodel.txtremarks + "','" + DateTime.Now + "','" + DateTime.Now + "','" + agent_id + "')";
                    caseviewpersistence.insertremarks(strqu);
                }

                //insertbottrigger(case_id, "save and update");
                //ctx.Database.ExecuteSqlCommand(strqu);
                if (caseviewmodel.btnsave == "Save & Update")
                    insertbottrigger(case_id, "save_update");
                if (caseviewmodel.btnsaveandreturn == "Save & Return")
                {
                    string strq = "" + 1 + ",'" + case_id + "','" + DateTime.Now + "','" + DateTime.Now + "')";
                    caseviewpersistence.insertstatustable(strq);
                    insertbottrigger(case_id, "save_return");
                }

            }

            if (caseviewmodel.alternatetitle!= null)
            {
                
                caseviewpersistence.updatecasealternatetitle(caseviewmodel.alternatetitle , case_id);

            }
            if(caseviewmodel.CasetoUtl != null)
            {
             if(caseviewmodel.CasetoUtl=="on"|| caseviewmodel.CasetoUtl=="ON")
                {
                    caseviewmodel.CasetoUtl = "UTL";
                }
                caseviewpersistence.updatecasetable(caseviewmodel.CasetoUtl, case_id);
            }
            if (caseviewmodel.chkSupervisor  != null)
            {
                if (caseviewmodel.chkSupervisor == "Supervisor" )
                {
                    caseviewpersistence.updatecasetable("Supervisor", case_id);
                }
                else if (caseviewmodel.chkSupervisor == "Blockedaccounts")
                {
                    caseviewpersistence.updatecasetable("Blockedaccounts", case_id);
                }else if (caseviewmodel.chkSupervisor == "Hours")
                {
                    caseviewpersistence.updatecasetable("24Hours", case_id);
                }else if (caseviewmodel.chkSupervisor== "clearselection")
                {
                    caseviewpersistence.updatecasetable("clearselection", case_id);
                }

            }
            //if (caseviewmodel.chkSupervisor != null)
            //{
            //    if (caseviewmodel.chkSupervisor == "Blockedaccounts")
            //    {
            //        caseviewpersistence.updatecasetable("Blockedaccounts", case_id);
            //    }
                
            //}
            //if (caseviewmodel.chkhours  != null)
            //{
            //    if (caseviewmodel.chkhours == "Hours")
            //    {
            //        caseviewpersistence.updatecasetable("24Hours", case_id);
            //    }

            //}

        }        
            //change the time of the shipper local time 
        public DateTime mtdtimezone(string timezone)
        {
            IndexPersistence indexpersistence = new IndexPersistence();
            string timezo = indexpersistence.timezones(timezone);           
            var inTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezo);
            DateTime dttime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, inTimeZone);

            //TimeZoneInfo infotime = TimeZoneInfo.FindSystemTimeZoneById(timezo);
            //DateTime thisDate = TimeZoneInfo.ConvertTimeToUtc(infotime);

            //var inTimeZone = TimeZoneInfo.FindSystemTimeZoneById(allvariables.timezones);
            //DateTime inTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, inTimeZone);
            //string time = inTime.ToString("HH:mm");

            //TimeSpan TS = thisDate- inTime ;
            //int hour = TS.Hours;
            //int mins = TS.Minutes;
            //int secs = TS.Seconds;
            //DateTime dttime= dttimezone.Add(TS) ;
            //file.Close();

            return dttime;
        }

        public string timeinworkighours(string timezone)
        {
          //  string timeinworkighours = null, timenotworkighours =null;
            

            IndexPersistence indexpersistence = new IndexPersistence();
            string timezo = indexpersistence.timezones(timezone);
            var inTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezo);
            DateTime dttime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, inTimeZone);

            int time = dttime.Hour;
            if(time >= 8 && time < 18)
            {
                return timezone;
            }else
            {
                return null;
            }

           
        }
        public string timenotworkighours(string timezone)
        {
            //  string timeinworkighours = null, timenotworkighours =null;


            IndexPersistence indexpersistence = new IndexPersistence();
            string timezo = indexpersistence.timezones(timezone);
            var inTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezo);
            DateTime dttime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, inTimeZone);

            int time = dttime.Hour;
            if (time >= 8 && time < 18)
            {
                return null;
            }
            else
            {
                return timezone;
            }

           
        }
        public DateTime? mtdcunvertdatetime(string  followupdate)
        {
            DateTime? datetime;
            if (followupdate != null)
                datetime= Convert.ToDateTime(followupdate);

            datetime = null;
            return datetime;
        }


        public void insertbottrigger(string case_id,string sendtemplate)
        {
            CaseviewPersistence caseviewpersistence = new CaseviewPersistence();
            string strqu ="'"+case_id +"','"+ sendtemplate + "','"+DateTime.Now+"','"+ DateTime .Now + "',0)";

            
            caseviewpersistence.insertbottriggertable(strqu);
           // return strqu;
        }

       
        public List<Indexmodel> casedetailstable(DataTable dt)
        {
            if (dt.Rows.Count == 0) { return null; }
            objvm.indexmodel = (from DataRow dr in dt.Rows
                                select new Indexmodel()
                                {
                                    case_id = dr["case_id"].ToString(),
                                    // attempts_made = ,
                                    attempts_made = attemptsmodecahnge(dr["attempts_made"].ToString()),
                                    tracking = dr["tracking"].ToString(),
                                    title = dr["title"].ToString(),
                                    alternate_title = dr["alternate_title"].ToString(),
                                    commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                                    shippers_local_time = mtdtimezone((dr["timezone"].ToString())),
                                    follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString()),
                                    shippername = (dr["name"].ToString()),
                                    accountnumber= (dr["account_number"].ToString()),
                                    assignment= Convert.ToDateTime((dr["assignment"]).ToString()),
                                    foldername= (dr["foldername"].ToString())
                                    //Convert.ToDateTime(dr["follow_up_date"].ToString()) account_number.Value.ToString("MM/dd/yyyy") 
                                    //indexservices.mtdcunvertdatetime (dr["follow_up_date"].ToString())
                                }).ToList();
            return objvm.indexmodel;
        }
        public List<string> shipperdetailsname(DataTable dt)
        {
           // objvm.shippersname shpname = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                objvm.shippersname.Add( (dr["name"].ToString()));
            }
            //objvm.shippersname  = (from DataRow dr in dt.Rows
            //                    select new shippersname ()
            //                    {
            //                        //case_id = dr["case_id"].ToString(),
            //                        //// attempts_made = ,
            //                        //attempts_made = attemptsmodecahnge(dr["attempts_made"].ToString()),
            //                        //tracking = dr["tracking"].ToString(),
            //                        //title = dr["title"].ToString(),
            //                        //alternate_title = dr["alternate_title"].ToString(),
            //                        //commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
            //                        //shippers_local_time = mtdtimezone((dr["timezone"].ToString())),
            //                        //follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString()),
            //                        shippername = (dr["name"].ToString())

            //                        //Convert.ToDateTime(dr["follow_up_date"].ToString()) 
            //                        //indexservices.mtdcunvertdatetime (dr["follow_up_date"].ToString())
            //                    }).ToList();
            return objvm.shippersname;
        }
        public List<Indexmodel> casedetailssupervisor(DataTable dt)
        {
            objvm.indexmodel = (from DataRow dr in dt.Rows
                                select new Indexmodel()
                                {
                                    case_id = dr["case_id"].ToString(),
                                    // attempts_made = ,
                                    attempts_made = attemptsmodecahnge(dr["attempts_made"].ToString()),
                                    tracking = dr["tracking"].ToString(),
                                    title = dr["title"].ToString(),
                                    alternate_title = dr["alternate_title"].ToString(),
                                    commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                                    shippers_local_time = mtdtimezone((dr["timezone"].ToString())),
                                    follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString()),
                                    name = dr["name"].ToString()

                                    //Convert.ToDateTime(dr["follow_up_date"].ToString()) 
                                    //indexservices.mtdcunvertdatetime (dr["follow_up_date"].ToString())
                                }).ToList();
            return objvm.indexmodel;
        }

        public void insertsendtemp(string case_id, string agent_id, CaseViewModel caseviewmodel)
        {
            CaseviewPersistence caseviewpersistence = new CaseviewPersistence();
            string strqu= "'" + case_id + "','" + caseviewmodel.txtempname  + "','" + caseviewmodel.txtcompany  + "','" + caseviewmodel.txtemail + "','" + caseviewmodel.send_template  + "','" + DateTime.Now + "','" + DateTime.Now + "')";
            caseviewpersistence.insertsendtemptable(strqu);
            insertbottrigger(case_id, "send_template");
        }

        public void insertdetailsofagetns(string agentid,string agetname,string agenteamil)
        {
            SupervisorPersistence supervisorpersistence = new SupervisorPersistence();
            string strqu1= "'" + agentid + "' ,'" + agetname + "' ,'" + agenteamil + "' ,'" + DateTime.Now + "' ,'" + DateTime.Now + "')";
            if(strqu1 !=null)
            supervisorpersistence.insertagetdetails(strqu1);
            //string strqu1=
            //caseviewpersistence.insertsaveandupdate(strqu1, itm1, case_id, caseviewmodel.txtfollowupdate);
        }
        public void insertbotpassword(string agentid, string oldpassword, string newpassord)
        {
            SupervisorPersistence supervisorpersistence = new SupervisorPersistence();
            string strqu1 = "'" + agentid + "' ,'"  + newpassord + "' ,'" + DateTime.Now + "' ,'" + DateTime.Now + "',0)";
            if (strqu1 != null)
                supervisorpersistence.insertbotpassword(strqu1);
            //string strqu1=
            //caseviewpersistence.insertsaveandupdate(strqu1, itm1, case_id, caseviewmodel.txtfollowupdate);
        }
    }

    }



