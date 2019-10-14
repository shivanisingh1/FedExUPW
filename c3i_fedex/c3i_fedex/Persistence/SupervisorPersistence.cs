using c3i_fedex.Models;
using c3i_fedex.services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace c3i_fedex.Persistence
{
    public class SupervisorPersistence
    {
        C3_FedexCasedetailsEntities db = new C3_FedexCasedetailsEntities();
        IndexPersistenceVariables allvariables = new IndexPersistenceVariables();
        ViewModel objvm = new ViewModel();
        Indexservices indexservices = new Indexservices();
        IndexPersistence indexPersistence = new IndexPersistence();
       
        DataTable dt1 = new DataTable
        {
            Columns = { "agent_id" }
        };
        public void insertagentcasedetails(string case_id,string agent_id)
        {
             string Reassigned = "Reassigned";
            //foreach (string str in case_id  )
            //  {
            string strqu = "update agent_case_details set agent_id='" + agent_id + "',Reassigned='"+ Reassigned + "',modification_date= '"+ DateTime.Now + "' where case_id='" + case_id + "'";// (case_id,agent_id,Reassigned) values('" + str + "','"+ agent_id + "', Reassigned ')";

           // string strqu1 = "update add_case_notes set Reassigned='" + Reassigned + "' where case_id='" + case_id + "'";// (case_id,agent_id,Reassigned) values('" + str + "','"+ agent_id + "', Reassigned ')";
                                                                                                                                                       //  }
                                                                                                                                                       // string strqu = "insert into agent_case_details (case_id,agent_id,Reassigned) values('" + case_id + "','"+ agent_id + "', Reassigned ')";
            db.Database.ExecuteSqlCommand(strqu);
        }

        public List<Indexmodel> casedet(string folder, string agent_id)
        {
            List<string> listoftimeworking = indexPersistence.timeinworkinghours();
            List<string> listoftimenotworking = indexPersistence.timenotworkinghours();
            db.GetType().GetHashCode();
            if (folder == "All" || folder == null || folder == "")
            {




                #region Query
                // SELECT [Project1].[C1] AS[C1], [Project1].[case_id] AS[case_id], [Project1].[attempts_made] AS[attempts_made], [Project1].[tracking] AS[tracking], [Project1].[title] AS[title], [Project1].[alternate_title]   AS[alternate_title], [Project1].[commitment_date] AS[commitment_date], 
                //[Project1].[follow_up_date] AS[follow_up_date], Project1].[shipper_local_time]  AS[shipper_local_time], [Project1].[assignment] AS[assignment] FROM(SELECT [Extent1].[case_id] AS[case_id], [Extent1].[alternate_title] AS[alternate_title], [Extent1].[tracking] AS[tracking], [Extent1].[title] AS[title],
                // [Extent1].[commitment_date] AS[commitment_date], [Extent1].[attempts_made] AS[attempts_made], [Extent1].[follow_up_date] AS[follow_up_date], [Extent1].[shipper_local_time] AS[shipper_local_time], [Extent1].[assignment] AS[assignment],	1 AS[C1] FROM  [dbo].[case_details] AS [Extent1] INNER JOIN[dbo].[agent_case_details] AS[Extent2] ON [Extent1].[case_id] = [Extent2].[case_id]
                //)  AS[Project1] ORDER BY[Project1].[follow_up_date]ASC, [Project1].[commitment_date]  ASC, [Project1].[attempts_made]   ASC, [Project1].[shipper_local_time] ASC, [Project1].[assignment]   ASC
                #endregion
                //in bussines hours
                #region case details
                var objcased1 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                 //join af in db.status_case_details on ca.case_id  equals af.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                 //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 // from y1 on yg.DefaultIfEmpty()
                                 where  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
                                 //&& lk.TimeZone.Contains(listoftime)
                                 // && activeProducts.Select(x => x).Contains(listoftime)
                                 //&& lk.TimeZone.Contains(listoftime)
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
                                     shippername = ad.name,
                                     accountnumber = sn.account_number,
                                     foldername = ep.folder_name
                                 });


                //not in bussens hours 
                var objcased2 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                 where  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking.Contains(lk.TimeZone)//listoftime.Contains (lk.TimeZone)

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
                                     shippername = ad.name,
                                     accountnumber = sn.account_number,
                                     foldername = ep.folder_name
                                 });
                //DataTable dt = objcased1.CopyToDataTable();
                // DateTime timezones = indexview.mtdtimezone();
                indexPersistence.dt.Clear();
                indexPersistence.dt2.Clear();
                // objvm.casedatils = new List<case_details>();


                foreach (var item in objcased1)
                {
                    indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment, item.foldername);
                }
                foreach (var item in objcased2)
                {
                    indexPersistence.dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment, item.foldername);
                }

                indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);
                indexPersistence.dt2 = indexPersistence.dt2.DefaultView.ToTable(true);
                indexPersistence.dt.Merge(indexPersistence.dt2);
                //dtAll.Merge(dtTwo);
                objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
               
                #endregion

            }
            else
            {
                #region  details of case
                // objvm.casedatilsall = db.case_details.ToList();
                var objcased1 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                 where ep.folder_name == folder && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                                     shippername = ad.name,
                                     accountnumber = sn.account_number
                                 });

                var objcased2 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                 //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                 where ep.folder_name == folder &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking.Contains(lk.TimeZone)
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
                                     shippername = ad.name,
                                     accountnumber = sn.account_number
                                 });
                //DataTable dt = objcased1.CopyToDataTable();

                indexPersistence.dt.Clear();
                indexPersistence.dt2.Clear();
                // objvm.casedatils = new List<case_details>();

                foreach (var item in objcased1)
                {

                    indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                }
                foreach (var item in objcased2)
                {

                    indexPersistence.dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                }
                indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);
                indexPersistence.dt2 = indexPersistence.dt2.DefaultView.ToTable(true);
                indexPersistence.dt.Merge(indexPersistence.dt2);

                objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                //= (from DataRow dr in dt.Rows
                //                   select new Indexmodel()
                //                   {
                //                       case_id = dr["case_id"].ToString(),
                //                       // attempts_made = ,
                //                       attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                //                       tracking = dr["tracking"].ToString(),
                //                       title = dr["title"].ToString(),
                //                       alternate_title = dr["alternate_title"].ToString(),
                //                       commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                //                       shippers_local_time = indexservices.mtdtimezone((dr["timezone"].ToString())),
                //                       follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                //                       //Convert.ToDateTime(dr["follow_up_date"].ToString()) 
                //                       //indexservices.mtdcunvertdatetime (dr["follow_up_date"].ToString())
                //                   }).ToList();
                //  indexmodel = objvm.indexmodel();
                // #endregion

            }
            #endregion
            return objvm.indexmodel;
        }
        public List<agentnameandid> agentnameandid()
        {
            var agentdeatais=(from ca in db.agent_details 
                               //join e in db.folder_ids on ca.case_id equals e.case_id
                               //join ep in db.folder_details on e.folder_id equals ep.folder_id
                               // join ag in db.agent_case_details on ca.case_id equals ag.case_id
                               //orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                               //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                               //from ep1 in yg.DefaultIfEmpty()
                               // from y1 on yg.DefaultIfEmpty()
                               // where ag.agent_id == agent_id
                               select new
                               {
                                   agent_id=ca.fedex_id,
                                   agent_name=ca.name

                               });

            indexPersistence.dt.Clear();

            foreach (var item in agentdeatais)
            {
                indexPersistence.dt.Rows.Add(item.agent_id ,item.agent_name );
            }
            objvm.agentnameandid = (from DataRow dr in indexPersistence.dt.Rows
                                select new agentnameandid()
                                {
                                    agent_id = dr["agent_id"].ToString(),
                                    // attempts_made = ,
                                   /// attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                                    agent_name = dr["agent_name"].ToString()
                                    
                                    // follow_up_date=(DateTime)dr["follow_up_date"].ToString()
                                }).ToList();


            return objvm.agentnameandid;

        }
        public List<Indexmodel> casedetails(int selectatt, string folder)

        {
            List<string> listoftimeworking = indexPersistence.timeinworkinghours();
            List<string> listoftimenotworking = indexPersistence.timenotworkinghours();

              //if (folder == "All" || folder == null || folder == "")
            {
                #region folder null
                if (selectatt == 0)
                {
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     // from y1 on yg.DefaultIfEmpty()
                                     orderby ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.shipper_local_time descending, ca.assignment ascending
                                     where  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    var objcased2 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     // from y1 on yg.DefaultIfEmpty()
                                     orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                     where  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    indexPersistence.dt2.Clear();
                    // objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);


                    }
                    foreach (var item in objcased2)
                    {

                        indexPersistence.dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);
                    indexPersistence.dt2 = indexPersistence.dt2.DefaultView.ToTable(true);
                    indexPersistence.dt.Merge(indexPersistence.dt2);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                select new Indexmodel()
                    //                {

                    //                    case_id = dr["case_id"].ToString(),
                    //                    attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                    tracking = dr["tracking"].ToString(),
                    //                    title = dr["title"].ToString(),
                    //                    alternate_title = dr["alternate_title"].ToString(),
                    //                    commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                    follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                }).ToList();

                }
                else if (selectatt == 4)
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     orderby ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.shipper_local_time descending, ca.assignment ascending
                                     where ca.attempts_made >= selectatt &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    var objcased2 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                     where ca.attempts_made >= selectatt &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking.Contains(lk.TimeZone)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();
                    // objvm.indexmodel = null;
                    indexPersistence.dt.Clear();
                    indexPersistence.dt2.Clear();
                    // objvm.casedatils = new List<case_details>();
                    if (objcased1.Count() != 0)
                    {
                        foreach (var item in objcased1)
                        {

                            indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }

                        //objvm.indexmodel = (from DataRow dr in dt.Rows
                        //                    select new Indexmodel()
                        //                    {

                        //                        case_id = dr["case_id"].ToString(),
                        //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                        //                        tracking = dr["tracking"].ToString(),
                        //                        title = dr["title"].ToString(),
                        //                        alternate_title = dr["alternate_title"].ToString(),
                        //                       // commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                        //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                        //                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                        //                    }).ToList();
                    }
                    else { }
                    if (objcased2.Count() != 0)
                    {
                        foreach (var item in objcased2)
                        {

                            indexPersistence.dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }


                    }
                    else { }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);
                    indexPersistence.dt2 = indexPersistence.dt2.DefaultView.ToTable(true);
                    indexPersistence.dt.Merge(indexPersistence.dt2);
                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                }
                else
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                     where ca.attempts_made == selectatt &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number


                                     });
                    var objcased2 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                     where ca.attempts_made == selectatt &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking.Contains(lk.TimeZone)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number


                                     });
                    //DataTable dt = objcased1.CopyToDataTable();
                    // objvm.indexmodel = null;
                    indexPersistence.dt.Clear();
                    indexPersistence.dt2.Clear();
                    // objvm.casedatils = new List<case_details>();
                    if (objcased1.Count() != 0)
                    {
                        foreach (var item in objcased1)
                        {

                            indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }
                        //indexservices.dt = indexservices.dt.DefaultView.ToTable(true);

                        //objvm.indexmodel = indexservices.casedetailstable(dt);

                    }
                    else { }
                    if (objcased2.Count() != 0)
                    {
                        foreach (var item in objcased2)
                        {

                            indexPersistence.dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }


                    }
                    // else { }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);
                    indexPersistence.dt2 = indexPersistence.dt2.DefaultView.ToTable(true);
                    indexPersistence.dt.Merge(indexPersistence.dt2);
                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                }
            }
        
        
        
            #endregion
           // else
            {
                    #region folder not null

                    {
                        
                        //else
                        {
                            // objvm.indexmodel = null;
                            // objvm.casedatilsall = db.case_details.ToList();
                            var objcased1 = (from ca in db.case_details
                                             join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                             join e in db.folder_ids on ca.case_id equals e.case_id
                                             join ep in db.folder_details on e.folder_id equals ep.folder_id
                                             join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                             join sn in db.shipper_details on ca.case_id equals sn.case_id
                                             join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                             orderby ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.shipper_local_time descending, ca.assignment ascending
                                             where ca.attempts_made == selectatt &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                                                 shippername = ad.name,
                                                 accountnumber = sn.account_number
                                             });
                            var objcased2 = (from ca in db.case_details
                                             join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                             join e in db.folder_ids on ca.case_id equals e.case_id
                                             join ep in db.folder_details on e.folder_id equals ep.folder_id
                                             join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                             join sn in db.shipper_details on ca.case_id equals sn.case_id
                                             join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                             orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                             where ca.attempts_made == selectatt &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking.Contains(lk.TimeZone)
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
                                                 shippername = ad.name,
                                                 accountnumber = sn.account_number
                                             });
                            //DataTable dt = objcased1.CopyToDataTable();

                            indexPersistence.dt.Clear();
                            indexPersistence.dt2.Clear();
                            // objvm.casedatils = new List<case_details>();
                            //if (objcased1.Count() != 0)
                            {
                                foreach (var item in objcased1)
                                {

                                    indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                                }
                                foreach (var item in objcased2)
                                {

                                    indexPersistence.dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                                }
                                indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);
                                indexPersistence.dt2 = indexPersistence.dt2.DefaultView.ToTable(true);
                                indexPersistence.dt.Merge(indexPersistence.dt2);

                                objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);

                            }

                        }
                    }
                    #endregion
                }
                return objvm.indexmodel;
            }
        
        //count the datatable folder wise
        public int countoftabledata(string folder, string agent_id)
        {
            allvariables.count = 0;
            if (folder == "All" || folder == "")
            {
                // objvm.indexmodel = null;
                var objcased1 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 where  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) //ag.agent_id == agent_id
                                 select new
                                 {
                                     case_id = ca.case_id,
                                     attempts_made = ca.attempts_made,
                                     tracking = ca.tracking_number,
                                     title = ca.title,
                                     alternate_title = ca.alternate_title,
                                     commitment_date = ca.commitment_date,
                                     follow_up_date = ca.follow_up_date,
                                     name=ad.name 
                                 });

                allvariables.count = objcased1.Count();
            }
            else if (folder != "All")
            {
                var objcased1 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 where ep.folder_name == folder && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) //&& ag.agent_id == agent_id
                                 select new
                                 {
                                     case_id = ca.case_id,
                                     attempts_made = ca.attempts_made,
                                  tracking = ca.tracking_number,
                                     title = ca.title,
                                     alternate_title = ca.alternate_title,
                                     commitment_date = ca.commitment_date
                                 });
                allvariables.count = objcased1.Count();
            }

            return allvariables.count;
        }
        //search case id or traking id  if (txtSearch != null || SelectData != null)
        public List<Indexmodel> searchcasedetails(string txtSearch, string selectData, string folder, string agent_id)
        {
           // List<string> listoftimeworking = indexPersistence.timeinworkinghours();
           // List<string> listoftimenotworking = indexPersistence.timenotworkinghours();
            // objvm.indexmodel = null;
            if (folder == "All" || folder == "" || folder == null)
            {
                #region serach case details with out folders
                if (selectData == "Tracking")
                {
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id

                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     where ca.tracking_number == txtSearch &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number

                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    //   objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                select new Indexmodel()
                    //                {

                    //                    case_id = dr["case_id"].ToString(),
                    //                    attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                    tracking = dr["tracking"].ToString(),
                    //                    title = dr["title"].ToString(),
                    //                    alternate_title = dr["alternate_title"].ToString(),
                    //                     commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                    follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                }).ToList();

                }
                else if (selectData == "Agent_Id")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     where ag.agent_id == txtSearch &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    // objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                    select new Indexmodel()
                    //                    {

                    //                        case_id = dr["case_id"].ToString(),
                    //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                        tracking = dr["tracking"].ToString(),
                    //                        title = dr["title"].ToString(),
                    //                        alternate_title = dr["alternate_title"].ToString(),
                    //                        commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    }).ToList();
                }
                else if (selectData == "Case_ID")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     where ca.case_id == txtSearch &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    // objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                    select new Indexmodel()
                    //                    {

                    //                        case_id = dr["case_id"].ToString(),
                    //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                        tracking = dr["tracking"].ToString(),
                    //                        title = dr["title"].ToString(),
                    //                        alternate_title = dr["alternate_title"].ToString(),
                    //                         commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    }).ToList();
                }
                else if (selectData == "UTL")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     where ca.attempts_made == 51 &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number

                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    // objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                    select new Indexmodel()
                    //                    {

                    //                        case_id = dr["case_id"].ToString(),
                    //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                        tracking = dr["tracking"].ToString(),
                    //                        title = dr["title"].ToString(),
                    //                        alternate_title = dr["alternate_title"].ToString(),
                    //                          commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    }).ToList();
                }
                #endregion
            }
            else
            {
                #region serach case details with folders
                if (selectData == "Tracking")
                {
                    var objcased1 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join e in db.folder_ids on ca.case_id equals e.case_id
                                     join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     where ca.tracking_number == txtSearch && ep.folder_name == folder &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    //objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                    select new Indexmodel()
                    //                    {

                    //                        case_id = dr["case_id"].ToString(),
                    //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                        tracking = dr["tracking"].ToString(),
                    //                        title = dr["title"].ToString(),
                    //                        alternate_title = dr["alternate_title"].ToString(),
                    //                          commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    }).ToList();

                }
                else if (selectData == "Case_ID")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join e in db.folder_ids on ca.case_id equals e.case_id
                                     join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     where ca.case_id == txtSearch && ep.folder_name == folder &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number

                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    //objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                    select new Indexmodel()
                    //                    {

                    //                        case_id = dr["case_id"].ToString(),
                    //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                        tracking = dr["tracking"].ToString(),
                    //                        title = dr["title"].ToString(),
                    //                        alternate_title = dr["alternate_title"].ToString(),
                    //                         commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    }).ToList();
                }
                else if (selectData == "Agent_Id")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join e in db.folder_ids on ca.case_id equals e.case_id
                                     join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     where ag.agent_id == txtSearch && ep.folder_name == folder &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    //objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                    select new Indexmodel()
                    //                    {

                    //                        case_id = dr["case_id"].ToString(),
                    //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                        tracking = dr["tracking"].ToString(),
                    //                        title = dr["title"].ToString(),
                    //                        alternate_title = dr["alternate_title"].ToString(),
                    //                          commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    }).ToList();
                }
                else if (selectData == "UTL")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join e in db.folder_ids on ca.case_id equals e.case_id
                                     join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     where ca.attempts_made == 51 && ep.folder_name == folder &&  (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         shippername = ad.name,
                                         accountnumber = sn.account_number
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    //objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(indexPersistence.dt);
                    //objvm.indexmodel = (from DataRow dr in dt.Rows
                    //                    select new Indexmodel()
                    //                    {

                    //                        case_id = dr["case_id"].ToString(),
                    //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                    //                        tracking = dr["tracking"].ToString(),
                    //                        title = dr["title"].ToString(),
                    //                        alternate_title = dr["alternate_title"].ToString(),
                    //                        commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                    //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                    //                    }).ToList();
                }
                #endregion
            }

            return objvm.indexmodel;
        }
        public List<Indexmodel> searchcasedetailsusingattemts(int selectatt, string txtSearch, string selectData, string agent_id)
        {
            // objvm.indexmodel = null;
            if (selectatt != 0)
            {
                #region serach case details with out folders
                if (selectData == "Tracking")
                {
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     where ca.tracking_number == txtSearch  && ca.attempts_made == selectatt && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         name = ad.name,
                                         agentid = ad.fedex_id,
                                         timezones = lk.TimeZone


                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    // objvm.casedatils = new List<case_details>();


                    foreach (var item in objcased1)
                    {
                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.name, item.timezones);
                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailssupervisor(indexPersistence.dt);


                }
                else if (selectData == "Agent_Id")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     where ag.agent_id == txtSearch &&  ca.attempts_made == selectatt && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         name = ad.name,
                                         agentid = ad.fedex_id,
                                         timezones = lk.TimeZone


                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    // objvm.casedatils = new List<case_details>();


                    foreach (var item in objcased1)
                    {
                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.name, item.timezones);
                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailssupervisor(indexPersistence.dt);

                }
                else if (selectData == "Case_ID")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     where ca.case_id == txtSearch  && ca.attempts_made == selectatt && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         name = ad.name,
                                         agentid = ad.fedex_id,
                                         timezones = lk.TimeZone


                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    // objvm.casedatils = new List<case_details>();


                    foreach (var item in objcased1)
                    {
                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.name, item.timezones);
                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailssupervisor(indexPersistence.dt);

                }
                else if (selectData == "UTL")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join ad in db.agent_details on ag.agent_id equals ad.fedex_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     where ca.attempts_made == 51  && ca.attempts_made == selectatt && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                                         name = ad.name,
                                         agentid = ad.fedex_id,
                                         timezones = lk.TimeZone


                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    indexPersistence.dt.Clear();
                    // objvm.casedatils = new List<case_details>();


                    foreach (var item in objcased1)
                    {
                        indexPersistence.dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.name, item.timezones);
                    }
                    indexPersistence.dt = indexPersistence.dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailssupervisor(indexPersistence.dt);

                }
                #endregion
            }


            return objvm.indexmodel;
        }
        public List<agentiddetails> agentcaedetails(string SelectData, string agent_id)
        {
            // objvm.indexmodel = null;
            if (SelectData == "Agent_Id")
            {
                var objcased1 = (from ca in db.agent_case_details
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     //  where ca.tracking == txtSearch
                                 select new
                                 {
                                     agent_id = ca.agent_id

                                 }).Distinct();
                //DataTable dt = objcased1.CopyToDataTable();

                dt1.Clear();
                //   objvm.casedatils = new List<case_details>();

                foreach (var item in objcased1)
                {

                    dt1.Rows.Add(item.agent_id);

                }
                objvm.agentiddetails = (from DataRow dr in dt1.Rows
                                        select new agentiddetails()
                                        {

                                            agent_id = dr["agent_id"].ToString(),

                                            //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                                        }).ToList();


            }
            return objvm.agentiddetails;
        }

        public List<agent_details> agentdetailsofcases(string agentid)
        {
         //   if()
            objvm.agentDetails = db.agent_details.ToList();

            return objvm.agentDetails;
        }

        public void insertagetdetails(string strqe)
        {
            string strqei= "insert into agetn_details (fedex_id,name,agent_mail,creation_date ,modification_date) values (" + strqe + "";

            if (strqei != null)
            {
                db.Database.ExecuteSqlCommand(strqei);
            }
        }
        public void insertbotpassword(string strqe)
        {

      //      [bot_user_id]
      //,[bot_password]
      //,[creation_date]
      //,[modification_date]
      //,[flag]
        string strqei = "insert into update_bot_password (bot_user_id,bot_password,creation_date,modification_date ,flag) values (" + strqe + "";

            if (strqei != null)
            {
                db.Database.ExecuteSqlCommand(strqei);
            }
        }

    }
}