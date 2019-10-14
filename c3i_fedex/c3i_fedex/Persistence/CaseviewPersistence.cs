using c3i_fedex.Models;
using c3i_fedex.services;
using c3i_fedex.utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace c3i_fedex.Persistence
{
    public class CaseviewPersistence
    {
        CaseviewPersistenceVariables allvariables = new CaseviewPersistenceVariables();
        C3_FedexCasedetailsEntities db = new C3_FedexCasedetailsEntities();
       // C3_FedexCasedetailsEntities dbcase = new C3_FedexCasedetailsEntities();
        ViewModel objvm = new ViewModel();
      //  allvariables.objprimarycase = null;
        //string strquary1 = null;
        public string objagentcase;
        IndexPersistence indexPersistence = new IndexPersistence();
        Indexservices indexservices = new Indexservices();
        DataTable dt = new DataTable("case_details")
        {
            Columns = { "case_id", "attempts_made", "tracking", "title", "alternate_title", "commitment_date", "follow_up_date", "timezone", "name", "account_number", "assignment", "foldername" }
        };

        DataTable dt1 = new DataTable()
        {
            Columns = { "email_id", "deadline" }
        };

        DataTable dt2 = new DataTable()
        {
            Columns = { "deadline" }
        };
        //details of case
        public ViewModel casedet(string case_id)

        {
            CaseviewUtilities caseviewutilities = new CaseviewUtilities();
            #region details of case
            ViewModel objvm = new ViewModel();
            allvariables.objprimarycase = null;
            {
                #region case_details table and agent_case_details join 
                var objcased1 = (from ca in db.case_details
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 where ca.case_id==case_id 
                                 select new
                                 {
                                     case_id = ca.case_id,
                                     attempts_made = ca.attempts_made,
                                     tracking = ca.tracking_number,
                                     title = ca.title,
                                     alternate_title = ca.alternate_title,
                                     commitment_date = ca.commitment_date,
                                     follow_up_date = ca.follow_up_date,
                                     shipper_local_time = ca.shipper_local_time,
                                     assignment = ca.assignment,
                                     timezone = lk.TimeZone,
                                     shippername = sn.name,
                                     accountnumber = sn.account_number

                                 });
                //DataTable dt = objcased1.CopyToDataTable();

                dt.Clear();
                // objvm.casedatils = new List<case_details>();
                Indexmodel indexmodel = new Indexmodel();
                foreach (var item in objcased1)
                {

                    //indexmodel.case_id = item.case_id;
                    //indexmodel.attempts_made  = item.attempts_made;
                    //indexmodel.tracking  = item.tracking;
                    //indexmodel.title  = item.title;
                    //indexmodel.alternate_title  = item.alternate_title;
                    //indexmodel.commitment_date = Convert.ToDateTime(item.commitment_date);
                    dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);
                    //  dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date);

                }
                dt = dt.DefaultView.ToTable(true);

                objvm.indexmodel = indexservices.casedetailstable(dt);
                #endregion            
                //case details
                objvm.casedatils = db.case_details.Where((x) => x.case_id == case_id).ToList();
                //package details
                objvm.packageDetails = db.package_details.Where((x) => x.case_id == case_id).ToList();
                //shipper details
                objvm.ShipperDetails = db.shipper_details.Where((x) => x.case_id == case_id).ToList();
                objvm.folderids =db.folder_ids.Where((x) => x.case_id == case_id).ToList();
                objvm.sendtemp = db.send_temp.ToList();
                objvm.currencycodes = db.currency_codes.ToList();
                var locationrigion = db.location_details_origion.Where((x) => x.case_id == case_id).Distinct();

                foreach (var item in locationrigion)
                {
                    objvm.origin = item.loc_id;
                    objvm.origindatetime = indexservices.mtdtimezone(item.TimeZone);
                    //objvm.origindatetime = indexservices.mtdtimezone((Convert.ToDateTime(item.local_date)), item.TimeZone);

                }

               var locationdest  = db.location_details_dest .Where((x) => x.case_id == case_id).Distinct();

                foreach (var item in locationdest)
                {
                    objvm.dest  = item.loc_id;
                    if(item.local_date !=null)
                    objvm.destdatetime = Convert.ToDateTime(item.local_date);//indexservices.mtdtimezone(item.TimeZone);
                    
                }
                objvm.gedanotifications =db.geda_notifications.Where((x) => x.case_id == case_id).ToList();
                objvm.locationdetailsorigion =db.location_details_origion.Where((x) => x.case_id == case_id).ToList();
                objvm.locationdetailsdest =db.location_details_dest.Where((x) => x.case_id == case_id).ToList();
                //recipient details
                objvm.RecipientDetails = db.recipient_details.Where((x) => x.case_id == case_id).ToList();
                var recipentdetails = db.recipient_details.Where((x) => x.case_id == case_id).ToList();
                foreach(var item in recipentdetails)
                {
                    objvm.locttime = caseviewutilities.stringsplit(item.city_state_prov);
                }
                //agent case details
                objvm.agentcaseattemptsmade = db.agent_case_attempts_made .Where((x) => x.case_id == case_id).ToList();
                objvm.agentCaseDetails = db.agent_case_details.Where((x) => x.case_id == case_id && x.Reassigned == "Reassigned").ToList();
                objvm.alternatephonenumbers=db.alternate_phone_numbers.Where((x) => x.case_id == case_id).ToList();
                // objagentcase = db.agent_case_details.Where((x) => x.case_id == case_id).Select(x => x.agent_id).ToString();
                var agentcase = (from ep in db.agent_case_details
                                 // join e in db.case_details on ep.case_id equals e.case_id
                                // join t in db.recipient_details on ep.case_id equals t.case_id
                                 where ep.case_id == case_id
                                 select new
                                 {
                                     ageint_id = ep.agent_id  

                                                                         
                                 }).ToList();
                foreach(var item in agentcase)
                {
                    objagentcase = item.ageint_id;
                }
                //Session["agentid"] = objagentcase;
                //agent details 
                objvm.agentDetails = db.agent_details.Where((x) => x.fedex_id  == objagentcase).ToList();

                var agentprimari = (from ep in db.primary_case_details
                                        // join e in db.case_details on ep.case_id equals e.case_id
                                        // join t in db.recipient_details on ep.case_id equals t.case_id
                                    where ep.case_id == case_id
                                 select new
                                 {
                                     primary_rep_id = ep.primary_rep_id
                                 }).ToList();
                foreach (var item in agentprimari)
                {
                    allvariables.objprimarycase =item.primary_rep_id.ToString ();
                }
                // objprimarycase = db.primary_case_details.Where((x) => x.case_id == case_id).Select(x => x.primary_rep_id).ToString();
               //primary rep details
                objvm.PrimaryRepDetails = db.primary_rep_details.Where((x) => x.fedex_id  == allvariables.objprimarycase).ToList();
                //package scan details
                objvm.packagescandetails = db.package_scan_details.Where((x) => x.case_id == case_id).ToList();
                //case notes details
                var case_deta = db.case_details.Where((x) => x.case_id == case_id).ToList();
                string[] stringSeparators = new string[] {"**","***"};
                foreach (var item in case_deta)
                {
                    string[] strcasenotes = item.case_notes.Split(stringSeparators, StringSplitOptions.None);
                    //var strcasenotes1 = item.case_notes.Split(new Char[] { '*' });
                    //var strcasenotes = Regex.Split(item.case_notes,"***");
                    for(int i=0; i< strcasenotes.Length;i++)
                    {
                        if (strcasenotes[i] == "")
                        {

                        }
                        else
                        {
                            if(strcasenotes[i] != "___")
                            objvm.casenote ="***"+ objvm.casenote + "<br> <br> " + "***"+ strcasenotes[i];
                        }

                    }
                }

               // primary requirements details
                objvm.primaryrequirements = db.primary_requirements.Where((x) => x.case_id == case_id).ToList();
                //add case notes details
                objvm.addcasenotes = db.add_case_notes.Where((x) => x.case_id == case_id).ToList();

                var agentcasemaild = (from ep in db.case_notes 
                                     // join e in db.case_details on ep.case_id equals e.case_id
                                     // join t in db.recipient_details on ep.case_id equals t.case_id
                                 where ep.case_id == case_id && ep.email_id != null
                                      select new
                                 {
                                     email_id = ep.email_id,
                                     //deadline=ep.Deadline 
                                 }).ToList();
               // if(agentcasemaild)
                foreach (var item in agentcasemaild)
                {
                    dt1.Rows.Add (item.email_id);
                }
                if (dt1.Rows.Count == 0)
                { }
                else
                {
                    objvm.casenotesmail = (from DataRow dr in dt1.Rows
                                           select new case_notesmail()
                                           {
                                               email_id = dr["email_id"].ToString(),

                                              /// deadline = Convert.ToDateTime(dr["deadline"].ToString())
                                           }).ToList();
                }
               // objvm.casenotedeadline = null ;
                var agentcasedeadline = (from ep in db.add_case_notes 
                                          // join e in db.case_details on ep.case_id equals e.case_id
                                          // join t in db.recipient_details on ep.case_id equals t.case_id
                                          orderby ep.deadline ascending 
                                         where ep.case_id == case_id && ep.deadline  != null
                                      select new
                                      {
                                         // email_id = ep.email_id,
                                          deadline=ep.deadline 
                                      }).ToList();
                // if(agentcasemaild)
                //dt.Clear();
                if (agentcasedeadline.Count == 0) { }
                else
                {
                    foreach (var item in agentcasedeadline)
                    {
                        objvm.casenotedeadline = Convert.ToDateTime(item.deadline);
                        //dt2.Rows.Add(item.deadline);
                    }
                }
              //  objvm.usholidays = db.us_holidays.ToList();


            }

            #endregion

            return objvm;
        }
        public bool inserttable(string case_id,bool insert)
        {
            C3_FedexCasedetailsEntities dbcase = new C3_FedexCasedetailsEntities();
            ViewModel objvm = new ViewModel();
           // objvm.primaryrequirements = dbcase.primary_requirements.Where((x) => x.case_id == case_id).ToList();
            var primaryrequirements = dbcase.primary_requirements.Where((x) => x.case_id == case_id).ToList();
            if (primaryrequirements.Count != 0)
            {
                //PrimaryRequirementsModel PrimaryRequirementsModel=new PrimaryRequirementsModel()
                string quary = "delete from primary_requirements where case_id='" + case_id + "'";
                dbcase.Database.ExecuteSqlCommand(quary);
                insert = true;

            }
            return insert;

        }
        //insert data primary requirements table
        public int insertprimarytable(string strquary , int i)
        {
            string strqu = null;
            
            if (i == 1)
            {
                strqu = "insert into primary_requirements (case_id,case_requirements,is_received,creation_date ,modification_date ) values (" + strquary + "";
            }
           else if (i == 2)
            {
                strqu = "insert into primary_requirements (case_id,case_requirements,is_received,creation_date ,modification_date ) values (" + strquary + "";
            }
            else if (i == 3)
            {
                strqu = "insert into primary_requirements (case_id,case_requirements,is_received,creation_date ,modification_date ) values (" + strquary + "";
            }
         //   string strqu1 = "insert into bot_trigger(case_id,bot_trigger,creationdate,modificationdate) values(";
            if(strqu!= null)
            db.Database.ExecuteSqlCommand(strqu);

            return 0;
        }
        //select the triger phone and traking 
        public AddCasenotesModel selecttrigerandtraking(string case_id)
        {
            var agentcase = (from ep in db.case_details
                            // join e in db.case_details on ep.case_id equals e.case_id
                             join t in db.recipient_details on ep.case_id equals t.case_id
                             where ep.case_id==case_id 
                             select new
                             {                                 
                                 tracking = ep.tracking_number ,
                                                    
                                 trigger_phone = t.phone
                             }).ToList();
            AddCasenotesModel addcase = new AddCasenotesModel();
            foreach (var item in agentcase)
            {
                addcase.trigger_phone = item.trigger_phone;
                addcase.tracking = item.tracking;
            }
            
            return addcase;
        }

        //update case detales table alternate_title
        public void updatecasealternatetitle(string alternatetitle, string case_id)
        {
            allvariables.strquary1 = null;
            allvariables.folder_id = 0;
            allvariables.strquary = null;
            if (alternatetitle != "UTL")
            {
                allvariables.strquary = "update case_details set alternate_title='" + alternatetitle + "'where case_id='" + case_id + "'";
                indexservices.insertbottrigger(case_id, "change_alternate_title");

                if(allvariables.strquary != null)
                    db.Database.ExecuteSqlCommand(allvariables.strquary);

            }
        }

        //update case detales table folder_id ,attempts_made
        public void updatecasetable(string alternatetitle,string case_id)
        {
            allvariables.strquary1 = null;
            allvariables.folder_id = 0;
            allvariables.strquary = null;
            //if (alternatetitle != "UTL")
            //{
            //    allvariables.strquary = "update case_details set alternate_title='" + alternatetitle + "'where case_id='" + case_id + "'";
            //    indexservices.insertbottrigger(case_id, "Change Alternate Title");
            //}
             if(alternatetitle == "UTL")
            {                
                
                allvariables.strquary1 = "update folder_ids set folder_id='" + 4 + "'where case_id='" + case_id + "'";
                //"insert into folder_ids (case_id,folder_id) values ('" + case_id + "','" + allvariables.folder_id + "')";
                allvariables.strquary = "update case_details set attempts_made='" + -51 + "'where case_id='" + case_id + "'";
                indexservices.insertbottrigger(case_id, "move_utl");

            } if (alternatetitle == "Supervisor")
            {
                allvariables.strquary1 = "update folder_ids set folder_id='" + 7 + "'where case_id='" + case_id + "'";
                //"insert into folder_ids (case_id,folder_id) values ('" + case_id + "','" + allvariables.folder_id + "')";
                allvariables.strquary = "update case_details set attemsfoldername='" + -52 + "'where case_id='" + case_id + "'";
                //indexservices.insertbottrigger(case_id, "Move to UTL");
            }
             if (alternatetitle == "24Hours")
            {
                allvariables.strquary1 = "update folder_ids set folder_id='" + 9 + "'where case_id='" + case_id + "'";
                //"insert into folder_ids (case_id,folder_id) values ('" + case_id + "','" + allvariables.folder_id + "')";
                allvariables.strquary = "update case_details set attemsfoldername='" + -53 + "'where case_id='" + case_id + "'";
                //indexservices.insertbottrigger(case_id, "Move to UTL");
            }
             if (alternatetitle == "Blockedaccounts")
            {
                allvariables.strquary1 = "update folder_ids set folder_id='" + 8 + "'where case_id='" + case_id + "'";
                //"insert into folder_ids (case_id,folder_id) values ('" + case_id + "','" + allvariables.folder_id + "')";
              allvariables.strquary = "update case_details set attemsfoldername='" + -54 + "'where case_id='" + case_id + "'";
               // indexservices.insertbottrigger(case_id, "Move to UTL");
            }
             if(alternatetitle== "clearselection")
            {
                allvariables.strquary1 = "update folder_ids set folder_id='" + 1 + "'where case_id='" + case_id + "'";
                allvariables.strquary = "update case_details set attemsfoldername='" + 0 + "'where case_id='" + case_id + "'";
            }

            if (allvariables.strquary1 !=null)
                db.Database.ExecuteSqlCommand(allvariables.strquary1);
            if (allvariables.strquary != null)
            db.Database.ExecuteSqlCommand(allvariables.strquary);

        }
       //insert a DATA agent_case_attempts_made AND add_case_notes ,update follow_up_date as case_details table
        public void insertsaveandupdate(string strqu,int i,string case_id,string follow_up_date)
        {
            allvariables.strquary = null;
            allvariables.strquary1 = null;
            if (i==1)
            {
                //if (follow_up_date != null) {
                //    strquary1 = "update case_details set follow_up_date='" + Convert.ToDateTime(follow_up_date) + "' where case_id='" + case_id + "'";

                //}

                allvariables.strquary = "insert into agent_case_attempts_made (case_id,agent_id,attempts_made,attempts_Date,case_priority) values (" + strqu + "";

            }else if(i==2)
            {
                if (follow_up_date != null)
                {
                    allvariables.strquary1 = "update case_details set follow_up_date='" + Convert.ToDateTime(follow_up_date) + "' where case_id='" + case_id + "'";

                }
                allvariables.strquary = "insert into add_case_notes (case_id,trigger_phone,spoke_to,left_vmail,info_req,info_rec,tracking,deadline,attempts_made,"
                   + "follow_up_date,Remarks,creation_date ,modification_date,agent_id) values (" + strqu +"";// + case_id + "' ,'" + addcase.trigger_phone + "' ,'" + txtspoke 
                //     + "' ,'" + txtvmail + "','" + txtinforeq + "','" + txtinforec + "','" + addcase.tracking + "','" + txtdeadline
                //     + "','" + txtattemptsmade +"','"+ txtfollowupdate +"','"+ txtremarks + "','" + DateTime.Now + "','" + DateTime.Now + "')";

            }

            if (allvariables.strquary1 != null)
            {
                db.Database.ExecuteSqlCommand(allvariables.strquary1);
            }
            if(allvariables.strquary != null)
            {
                db.Database.ExecuteSqlCommand(allvariables.strquary);
            }
                        
        }
        //select attempt
        public int attemptsmadecount(string case_id)
        {
            allvariables.attempt=0;
            var agentcase = (from ep in db.case_details 
                                 // join e in db.case_details on ep.case_id equals e.case_id
                                 // join t in db.recipient_details on ep.case_id equals t.case_id
                             where ep.case_id == case_id
                             select new
                             {
                                 attempts_made = ep.attempts_made 
                             }).ToList();
            foreach (var item in agentcase)
            {
                allvariables.attempt = Convert.ToInt32 (item.attempts_made);
            }

            return allvariables.attempt;
        }
        //update case details attempts mades and folder id's
        public void updatecasetable(int attempts, string case_id)
        {
            allvariables.strquary = null;

            allvariables.strquary = "update case_details set attempts_made='" + attempts + "' where case_id='" + case_id + "'";


            //if (alternatetitle != "UTL")
            //{
            //    strquary = "update case_details set alternate_title='" + alternatetitle + "'where case_id='" + case_id + "'";
            //}
            //else if (alternatetitle == "UTL")
            //{
            //    strquary = "update agent_case_attempts_made set attempts_made='" + alternatetitle + "'where case_id='" + case_id + "'";

            //}

            if (allvariables.strquary != null)
                db.Database.ExecuteSqlCommand(allvariables.strquary);
            allvariables.strquary = null;

            allvariables.strquary = "update folder_ids set folder_id='" + 1 + "'where case_id='" + case_id + "'";
            indexservices.insertbottrigger(case_id, "move_wip");
            if (allvariables.strquary != null)
                db.Database.ExecuteSqlCommand(allvariables.strquary);
        }
        //select not supervisor agetn id 
        public string loginiddetails(string username)
        {
            string agent_id = null;
            var agentcase = (from ep in db.logins 
                                  join e in db.agent_details  on ep.user_id  equals e.fedex_id
                             // join t in db.recipient_details on ep.case_id equals t.case_id
                             where ep.user_name  == username && (e.supervisor=="N" || e.supervisor == "n" )
                             select new
                             {
                                 agent_id = e.fedex_id 
                             }).ToList();
            foreach (var item in agentcase)
            {
                agent_id = item.agent_id;
            }

           
            return agent_id;
        }
        //select if supervisor agetn id 
        public string loginsupervisordetails(string username)
        {
            string agent_id = null;
            var agentcase = (from ep in db.logins
                             join e in db.agent_details on ep.user_id equals e.fedex_id
                             // join t in db.recipient_details on ep.case_id equals t.case_id
                             where ep.user_name == username && (e.supervisor == "Y" || e.supervisor == "y")
                             select new
                             {
                                 agent_id = e.fedex_id
                             }).ToList();
            foreach (var item in agentcase)
            {
                agent_id = item.agent_id;
            }
            return agent_id;
        }
        //save case notes table  email id 
        public void updatecasetable(string emailid,DateTime? deadline,string case_id)
        {

            if (deadline != null)
            {
                allvariables.strquary = "update case_notes set email_id='" + emailid + "'where case_id='" + case_id + "'";
            }
            //else if (alternatetitle == "UTL")
            //{
            //    var agentcase = (from ep in db.folder_details
            //                         // join e in db.case_details on ep.case_id equals e.case_id
            //                         // join t in db.folder_ids on ep.folder_id equals t.folder_id
            //                     where ep.folder_name == "UTL"
            //                     select new
            //                     {
            //                         folder_id = ep.folder_id
            //                     }).ToList();
            //    AddCasenotesModel addcase = new AddCasenotesModel();
            //    foreach (var item in agentcase)
            //    {
            //        allvariables.folder_id = item.folder_id;
            //        //addcase.tracking = item.tracking;
            //    }
            //    var agentcase1 = (from ep in db.folder_ids
            //                          // join e in db.case_details on ep.case_id equals e.case_id
            //                          // join t in db.folder_ids on ep.folder_id equals t.folder_id
            //                      where ep.folder_id == allvariables.folder_id
            //                      select new { }).ToList();
            //    if (agentcase1.Count != 0)
            //    {
            //        string strquery = "delete from folder_ids where case_id='" + case_id + "'";
            //        db.Database.ExecuteSqlCommand(strquery);
            //    }


            //    allvariables.strquary1 = "insert into folder_ids (case_id,folder_id) values ('" + case_id + "','" + allvariables.folder_id + "')";
            //    allvariables.strquary = "update case_details set attempts_made='" + 51 + "'where case_id='" + case_id + "'";

            //}

            //if (allvariables.strquary1 != null)
            //    db.Database.ExecuteSqlCommand(allvariables.strquary1);
            if (allvariables.strquary != null)
                db.Database.ExecuteSqlCommand(allvariables.strquary);

        }

        //update case_details table case_priority
         public void updatecasedetailspriority(string case_id ,int priority)
        {
            allvariables.strquary = null;
            allvariables.strquary = "update case_details set case_priority='" + priority + "' where case_id='" + case_id + "'";           
            if (allvariables.strquary != null)
                db.Database.ExecuteSqlCommand(allvariables.strquary);
        }
        //update folder_ids detales table folder_id
        public void updategedatable(string case_id,int folderid)
        {
            string strq = "update folder_ids set folder_id= "+ folderid + " where case_id='"+ case_id +"'";
            //   string strqeu = "insert into status_case_details(status_id,case_id,creation_date ,modification_date)values(" + strqe + "";
            if (strq != null)
            {
                db.Database.ExecuteSqlCommand(strq);
            }

        }
        //insert bot_trigger table  
        public void insertbottriggertable(string strqe)
        {
            string strq = "insert into bot_trigger (case_id, bot_trigger, creationdate, modificationdate,flag) values(" + strqe + "";

         //   string strqeu = "insert into status_case_details(status_id,case_id,creation_date ,modification_date)values(" + strqe + "";
            if (strq != null)
            {
                db.Database.ExecuteSqlCommand(strq);
            }
           
        }
        //insert send_template table
        public void insertsendtemptable(string strqe)
        {
            string strq = "insert into send_template (case_id, emp_name, company_name,email,select_form, creation_date, modification_date) values(" + strqe + "";
           
            if (strq != null)
            {
                db.Database.ExecuteSqlCommand(strq);
            }

        }
        //insert status_case_details table
        public void insertstatustable(string strqe)
        {
           // string strq = "insert into bot_trigger (case_id, bot_trigger, creationdate, modificationdate,flag) values(" + strqe + "";

            string strqeu = "insert into status_case_details(status_id,case_id,creation_date ,modification_date)values(" + strqe + "";
           
            if (strqeu != null)
            {
                db.Database.ExecuteSqlCommand(strqeu);
            }
        }
        //insert add_case_notes table
        public void insertremarks(string strqu)
        {
            allvariables.strquary = "insert into add_case_notes (case_id,Remarks,creation_date ,modification_date,agent_id) values (" + strqu + "";

            if (allvariables.strquary != null)
            {
                db.Database.ExecuteSqlCommand(allvariables.strquary);
            }

        }
        //update case_ details table dealine
        public void updatedeadline(string case_id,DateTime deadline)
        {
            string strqe = "Update case_details set deadline='" + deadline + "' where case_id ='" + case_id + "'";
            if (strqe != null)
            {
                db.Database.ExecuteSqlCommand(strqe);
            }
        }
    }
}