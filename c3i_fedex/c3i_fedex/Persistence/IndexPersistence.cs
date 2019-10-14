using c3i_fedex.Models;
using c3i_fedex.services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace c3i_fedex.Persistence
{
    public class IndexPersistence
    {

        
      //  string strconn ="Data Source=ServerName;Initial Catalog=DataBaseName;User id=UserName;Password=Secret;";
        //SqlConnection conn = new SqlConnection("data source=10.3.2.31;initial catalog=Incedo_DEV;integrated security=True;connect timeout=0;");
        //  conn.Open(); data source=10.3.2.31;initial catalog=Incedo_DEV;integrated security=True;connect timeout=0;
        //SQLConnection conn = new SQLConnection();
        // Incedo_DEVEntities db = new Incedo_DEVEntities();
        C3_FedexCasedetailsEntities db = new C3_FedexCasedetailsEntities();
        IndexPersistenceVariables allvariables = new IndexPersistenceVariables();
        ViewModel objvm = new ViewModel();
        Indexmodel indexmodel = new Indexmodel();
        Indexservices indexservices = new Indexservices();
        //string agent_id = Session["agent_id"].toString();
        // int count = 0;
 public DataTable dt = new DataTable("case_details")
        {
            Columns = { "case_id", "attempts_made", "tracking", "title", "alternate_title", "commitment_date", "follow_up_date", "timezone", "name", "account_number", "assignment", "foldername" }
        };
        public DataTable dt2 = new DataTable
        {
            Columns = { "case_id", "attempts_made", "tracking", "title", "alternate_title", "commitment_date", "follow_up_date", "timezone", "name", "account_number", "assignment", "foldername" }
        };
        DataTable dt1 = new DataTable
        {
            Columns = {"agent_id"}
        };

        //public string indexservices.attemptsmodecahnge(string attemps)
        //{
        //    if (attemps == "51")
        //    {
        //        attemps = "UTL";

        //    }

        //string strobj = "(from ca in db.case_details join ag in db.agent_case_details on ca.case_id equals ag.case_id join lk in db.location_details_origion on  ca.case_id equals lk.case_id  join sn in db.shipper_details on ca.case_id equals sn.case_id ";
        //string strorderby = "orderby ca.case_priority descending,  ca.attempts_made descending, ca.follow_up_date ascending , ca.commitment_date ascending ,  ca.shipper_local_time descending, ca.assignment ascending ";
        //string strcondition = " where ag.agent_id== agent_id && (db.status_case_details.Any(x => x.case_id  ==ca.case_id )== false) ";
        //string strselct = "select new";
        //string strdata = " {case_id = ca.case_id, attempts_made = ca.attempts_made , tracking = ca.tracking_number, title = ca.title, alternate_title = ca.alternate_title, commitment_date = ca.commitment_date, follow_up_date=ca.follow_up_date , shipper_local_time = ca.shipper_local_time, assignment= ca.assignment, timezone=lk.TimeZone,   shippername=sn.name,  accountnumber=sn.account_number });";
       
        //    return attemps;
        //}
        //case details
        public List<Indexmodel > casedet(string folder,string agent_id)
        {
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
            db.GetType().GetHashCode();
            if (folder == "All" || folder == null || folder == "")
            {

                //string strqery = "where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)";

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
                                 //join af in db.status_case_details on ca.case_id  equals af.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                 //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 // from y1 on yg.DefaultIfEmpty()
                                 //strqery
                                 where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
                                 //&& lk.TimeZone.Contains(listoftime)
                                 // && activeProducts.Select(x => x).Contains(listoftime)
                                 //&& lk.TimeZone.Contains(listoftime)
                                 select new
                                 {
                                     case_id = ca.case_id,
                                     attempts_made = ca.attempts_made ,
                                     tracking = ca.tracking_number,
                                     title = ca.title,
                                     alternate_title = ca.alternate_title,
                                     commitment_date = ca.commitment_date,
                                     follow_up_date=ca.follow_up_date ,
                                     shipper_local_time = ca.shipper_local_time,
                                     assignment= ca.assignment,
                                     timezone=lk.TimeZone, 
                                     shippername=sn.name,
                                     accountnumber=sn.account_number,
                                     foldername = ep.folder_name 
                                 });


                //not in bussens hours 
                var objcased2 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                 where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking.Contains (lk.TimeZone )//listoftime.Contains (lk.TimeZone)
                                 
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
                                     accountnumber = sn.account_number,
                                     foldername = ep.folder_name
                                 });
                //DataTable dt = objcased1.CopyToDataTable();
                // DateTime timezones = indexview.mtdtimezone();
                dt.Clear();
                dt2.Clear();
                // objvm.casedatils = new List<case_details>();


                foreach (var item in objcased1)
                {
                    dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezone,item.shippername,item.accountnumber,item.assignment,item.foldername);
                }
                foreach (var item in objcased2)
                {
                    dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezone,item.shippername,item.accountnumber,item.assignment, item.foldername);
                }
                string TRUCK = "delivery - customs - customs Paperwork - None - None - 774151621010 - 09-JAN-19";
                dt = dt.DefaultView.ToTable(true);

                dt.Select("title = '" + TRUCK + "'");


                dt2 = dt2.DefaultView.ToTable(true);
                 dt.Merge(dt2);
                //dtAll.Merge(dtTwo);
                objvm.indexmodel = indexservices.casedetailstable(dt);
                //objvm.indexmodel = (from DataRow dr in dt.Rows
                //                    select new Indexmodel()
                //                    {
                //                        case_id = dr["case_id"].ToString(),
                //                        // attempts_made = ,
                //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
                //                        tracking = dr["tracking"].ToString(),
                //                        title = dr["title"].ToString(),
                //                        alternate_title = dr["alternate_title"].ToString(),
                //                      commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
                //                       shippers_local_time = indexservices.mtdtimezone((dr["timezone"].ToString())),
                //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
                //                        //Convert.ToDateTime(dr["follow_up_date"].ToString()) 
                //                        //indexservices.mtdcunvertdatetime (dr["follow_up_date"].ToString())
                //                    }).ToList();
                //  indexmodel = objvm.indexmodel();
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
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                 where ep.folder_name == folder &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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

                var objcased2 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending, ca.deadline ascending, ca.assignment ascending
                                 where ep.folder_name == folder && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking.Contains(lk.TimeZone)
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
                dt2.Clear();
                // objvm.casedatils = new List<case_details>();

                foreach (var item in objcased1)
                {

                    dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                }
                foreach (var item in objcased2)
                {

                    dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                }
                dt = dt.DefaultView.ToTable(true);
                dt2 = dt2.DefaultView.ToTable(true);
                dt.Merge(dt2);

                objvm.indexmodel= indexservices.casedetailstable(dt);
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

       
        public List<string> shippername(string folder, string agent_id)
        {
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
            db.GetType().GetHashCode();
            if (folder == "All" || folder == null || folder == "")
            {
                //conn.Open();
                //conn.Close();
                // string agetnid = Session["agent_id"].toString();
                #region Query
                // SELECT [Project1].[C1] AS[C1], [Project1].[case_id] AS[case_id], [Project1].[attempts_made] AS[attempts_made], [Project1].[tracking] AS[tracking], [Project1].[title] AS[title], [Project1].[alternate_title]   AS[alternate_title], [Project1].[commitment_date] AS[commitment_date], 
                //[Project1].[follow_up_date] AS[follow_up_date], Project1].[shipper_local_time]  AS[shipper_local_time], [Project1].[assignment] AS[assignment] FROM(SELECT [Extent1].[case_id] AS[case_id], [Extent1].[alternate_title] AS[alternate_title], [Extent1].[tracking] AS[tracking], [Extent1].[title] AS[title],
                // [Extent1].[commitment_date] AS[commitment_date], [Extent1].[attempts_made] AS[attempts_made], [Extent1].[follow_up_date] AS[follow_up_date], [Extent1].[shipper_local_time] AS[shipper_local_time], [Extent1].[assignment] AS[assignment],	1 AS[C1] FROM  [dbo].[case_details] AS [Extent1] INNER JOIN[dbo].[agent_case_details] AS[Extent2] ON [Extent1].[case_id] = [Extent2].[case_id]
                //)  AS[Project1] ORDER BY[Project1].[follow_up_date]ASC, [Project1].[commitment_date]  ASC, [Project1].[attempts_made]   ASC, [Project1].[shipper_local_time] ASC, [Project1].[assignment]   ASC
                #endregion
                #region case details
                var objcased1 = (from ca in db.case_details
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 //join af in db.status_case_details on ca.case_id  equals af.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                 //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 // from y1 on yg.DefaultIfEmpty()
                                 where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                var objcased2 = (from ca in db.case_details
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 //join af in db.status_case_details on ca.case_id  equals af.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                 //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 // from y1 on yg.DefaultIfEmpty()
                                 where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking.Contains(lk.TimeZone)
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
                // DateTime timezones = indexview.mtdtimezone();
                dt.Clear();
                dt2.Clear();
                // objvm.casedatils = new List<case_details>();


                foreach (var item in objcased1)
                {
                    dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezone,item.shippername,item.accountnumber,item.assignment); dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);
                }
                foreach (var item in objcased2)
                {
                    dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezone,item.shippername,item.accountnumber,item.assignment);
                }
                dt = dt.DefaultView.ToTable(true);
                dt2 = dt2.DefaultView.ToTable(true);
                dt.Merge(dt2);
                objvm.shippersname  = indexservices.shipperdetailsname(dt);
                
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
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                 where ep.folder_name == folder && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking .Contains(lk.TimeZone)
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

                var objcased2 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id
                                 join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                 join sn in db.shipper_details on ca.case_id equals sn.case_id
                                 //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                 //from ep1 in yg.DefaultIfEmpty()
                                 orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                 where ep.folder_name == folder && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking  .Contains(lk.TimeZone)
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
                dt2.Clear();
                // objvm.casedatils = new List<case_details>();

                foreach (var item in objcased1)
                {

                    dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                }
                foreach (var item in objcased2)
                {

                    dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                }
                dt = dt.DefaultView.ToTable(true);
                dt2 = dt2.DefaultView.ToTable(true);
                dt.Merge(dt2);

                objvm.indexmodel = indexservices.casedetailstable(dt);
               
               // #endregion

            }
            #endregion
            return objvm.shippersname;
        }
        public List<Indexmodel> casedetails(int  selectatt, string folder,string agent_id)

        {
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
            
            if (folder == "All" || folder == null || folder == "")
            {
                #region folder null
                if (selectatt == 0)
                {
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on  ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     // from y1 on yg.DefaultIfEmpty()
                                     orderby ca.attempts_made descending, ca.follow_up_date ascending , ca.commitment_date ascending ,  ca.shipper_local_time descending, ca.assignment ascending 
                                     where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)&& listoftimeworking.Contains(lk.TimeZone )
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
                    var objcased2 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     // from y1 on yg.DefaultIfEmpty()
                                     orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                     where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                    dt2.Clear();
                    // objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);


                    }
                    foreach (var item in objcased2)
                    {

                        dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    dt = dt.DefaultView.ToTable(true);
                    dt2 = dt2.DefaultView.ToTable(true);
                    dt.Merge(dt2);

                    objvm.indexmodel = indexservices.casedetailstable(dt);
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
                else if (selectatt ==4)
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     orderby ca.attempts_made descending, ca.follow_up_date ascending , ca.commitment_date ascending ,  ca.shipper_local_time descending, ca.assignment ascending 
                                     where ca.attempts_made >= selectatt &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone )
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
                    var objcased2 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                     where ca.attempts_made >= selectatt && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking .Contains(lk.TimeZone)
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
                    // objvm.indexmodel = null;
                    dt.Clear();
                    dt2.Clear();
                    // objvm.casedatils = new List<case_details>();
                    if (objcased1.Count() != 0)
                    {
                        foreach (var item in objcased1)
                        {

                            dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

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

                            dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }
                        
                       
                    }
                    else { }
                    dt = dt.DefaultView.ToTable(true);
                    dt2 = dt2.DefaultView.ToTable(true);
                    dt.Merge(dt2);
                    objvm.indexmodel = indexservices.casedetailstable(dt);
                }
                else
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                     where ca.attempts_made == selectatt && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking.Contains(lk.TimeZone)
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
                    var objcased2 = (from ca in db.case_details
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     //join e in db.folder_ids on ca.case_id equals e.case_id
                                     //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     //join pe in db.agent_case_attempts_made on ca.case_id equals pe.case_id into yg
                                     //from ep1 in yg.DefaultIfEmpty()
                                     orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                     where ca.attempts_made == selectatt && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking .Contains(lk.TimeZone)
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
                    // objvm.indexmodel = null;
                    dt.Clear();
                    dt2.Clear();
                    // objvm.casedatils = new List<case_details>();
                    if (objcased1.Count() != 0)
                    {
                        foreach (var item in objcased1)
                        {

                            dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }
                        //dt = dt.DefaultView.ToTable(true);

                        //objvm.indexmodel = indexservices.casedetailstable(dt);
                       
                    }
                    else { }
                    if (objcased2.Count() != 0)
                    {
                        foreach (var item in objcased2)
                        {

                            dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }
                       

                    }
                    else { }
                    dt = dt.DefaultView.ToTable(true);
                    dt2 = dt2.DefaultView.ToTable(true);
                    dt.Merge(dt2);
                    objvm.indexmodel = indexservices.casedetailstable(dt);
                }
            }
            #endregion
            else
            {
                #region folder not null
                
                {
                    if (selectatt == 0)
                    {
                        var objcased1 = (from ca in db.case_details
                                         join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                         join e in db.folder_ids on ca.case_id equals e.case_id
                                         join ep in db.folder_details on e.folder_id equals ep.folder_id
                                         join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                         join sn in db.shipper_details on ca.case_id equals sn.case_id
                                         orderby ca.attempts_made descending, ca.follow_up_date ascending , ca.commitment_date ascending ,  ca.shipper_local_time descending, ca.assignment ascending 
                                         where ep.folder_name==folder &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)&& listoftimeworking.Contains(lk.TimeZone )
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
                        var objcased2 = (from ca in db.case_details
                                         join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                         join e in db.folder_ids on ca.case_id equals e.case_id
                                         join ep in db.folder_details on e.folder_id equals ep.folder_id
                                         join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                         join sn in db.shipper_details on ca.case_id equals sn.case_id
                                         orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                         where ep.folder_name == folder && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking .Contains(lk.TimeZone)
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
                        dt2.Clear();
                        // objvm.casedatils = new List<case_details>();

                        foreach (var item in objcased1)
                        {

                            dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }
                        foreach (var item in objcased2)
                        {

                            dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                        }
                        // objvm.indexmodel = null;
                        dt = dt.DefaultView.ToTable(true);
                        dt2 = dt2.DefaultView.ToTable(true);
                        dt.Merge(dt2);

                        objvm.indexmodel= indexservices.casedetailstable(dt);
                        //objvm.indexmodel = (from DataRow dr in dt.Rows
                        //                    select new Indexmodel()
                        //                    {
                        //                        //indexservices.
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
                    else
                    {
                       // objvm.indexmodel = null;
                        // objvm.casedatilsall = db.case_details.ToList();
                        var objcased1 = (from ca in db.case_details
                                         join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                         join e in db.folder_ids on ca.case_id equals e.case_id
                                         join ep in db.folder_details on e.folder_id equals ep.folder_id
                                         join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                         join sn in db.shipper_details on ca.case_id equals sn.case_id
                                         orderby ca.attempts_made descending, ca.follow_up_date ascending , ca.commitment_date ascending ,  ca.shipper_local_time descending, ca.assignment ascending 
                                         where ca .attempts_made == selectatt && ep.folder_name == folder &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimeworking .Contains(lk.TimeZone )
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
                        var objcased2 = (from ca in db.case_details
                                         join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                         join e in db.folder_ids on ca.case_id equals e.case_id
                                         join ep in db.folder_details on e.folder_id equals ep.folder_id
                                         join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                         join sn in db.shipper_details on ca.case_id equals sn.case_id
                                         orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                                         where ca.attempts_made == selectatt && ep.folder_name == folder && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && listoftimenotworking .Contains(lk.TimeZone)
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
                        dt2.Clear();
                        // objvm.casedatils = new List<case_details>();
                        //if (objcased1.Count() != 0)
                        {
                            foreach (var item in objcased1)
                            {

                                dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                            }
                            foreach (var item in objcased2)
                            {

                                dt2.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                            }
                            dt = dt.DefaultView.ToTable(true);
                            dt2 = dt2.DefaultView.ToTable(true);
                            dt.Merge(dt2);

                            objvm.indexmodel = indexservices.casedetailstable(dt);
                            
                        }
                        
                    }
                }
                #endregion
            }
           return  objvm.indexmodel;
        }
        //count the datatable folder wise
        public int countoftabledata(string folder,string agent_id)
        {
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
            allvariables.count = 0;
            if (folder == "All" || folder == "")
            {
               // objvm.indexmodel = null;
                var objcased1 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
                                 select new
                                 {
                                     case_id = ca.case_id,
                                     attempts_made = ca.attempts_made,
                                     tracking = ca.tracking_number,
                                     title = ca.title,
                                     alternate_title = ca.alternate_title,
                                     commitment_date = ca.commitment_date,
                                     follow_up_date = ca.follow_up_date
                                 });

                allvariables.count = objcased1.Count();
            }
            else if (folder != "All")
            {
                var objcased1 = (from ca in db.case_details
                                 join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                 join e in db.folder_ids on ca.case_id equals e.case_id
                                 join ep in db.folder_details on e.folder_id equals ep.folder_id                                 
                                 where ep.folder_name == folder && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
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
                                     where ca.tracking_number  == txtSearch &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                    //   objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    dt = dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(dt);
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
                                     where ag.agent_id  == txtSearch &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    dt = dt.DefaultView.ToTable(true);

                objvm.indexmodel= indexservices.casedetailstable(dt);
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
                                     where ca.case_id == txtSearch &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                     dt = dt.DefaultView.ToTable(true);

                objvm.indexmodel= indexservices.casedetailstable(dt);
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
                                     where ca.attempts_made  == 51 &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                     dt = dt.DefaultView.ToTable(true);

                        objvm.indexmodel = indexservices.casedetailstable(dt);
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
                                     where ca.tracking_number  == txtSearch && ep.folder_name == folder &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                    //objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                     dt = dt.DefaultView.ToTable(true);

                objvm.indexmodel= indexservices.casedetailstable(dt);
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
                                     where ca.case_id == txtSearch && ep.folder_name == folder && ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                    //objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    dt = dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(dt);
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
                                     where ag.agent_id  == txtSearch && ep.folder_name == folder &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                    //objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    dt = dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(dt);
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
                                     where ca.attempts_made  == 51 && ep.folder_name == folder &&  ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
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
                    //objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date, item.timezone, item.shippername, item.accountnumber, item.assignment);

                    }
                    dt = dt.DefaultView.ToTable(true);

                    objvm.indexmodel = indexservices.casedetailstable(dt);
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
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
            // objvm.indexmodel = null;
            if (selectatt!=0)
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
                                     where ca.tracking_number == txtSearch && ag.agent_id == agent_id && ca.attempts_made == selectatt && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
                                     select new
                                     {
                                         case_id = ca.case_id,
                                         attempts_made = ca.attempts_made,
                                         tracking = ca.tracking_number,
                                         title = ca.title,
                                         alternate_title = ca.alternate_title,
                                         commitment_date = ca.commitment_date,
                                         follow_up_date = ca.follow_up_date,
                                         timezones =lk.TimeZone,
                                         shippersname=sn.name 

                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    dt.Clear();
                    //   objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezones,item.shippersname  );

                    }
                    dt = dt.DefaultView.ToTable(true);

                objvm.indexmodel= indexservices.casedetailstable(dt);
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
                else if (selectData == "Agent_Id")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     where ag.agent_id == txtSearch && ag.agent_id == agent_id && ca.attempts_made == selectatt && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
                                     select new
                                     {
                                         case_id = ca.case_id,
                                         attempts_made = ca.attempts_made,
                                         tracking = ca.tracking_number,
                                         title = ca.title,
                                         alternate_title = ca.alternate_title,
                                         commitment_date = ca.commitment_date,
                                         follow_up_date = ca.follow_up_date,
                                         timezones=lk.TimeZone,
                                         shippersname =sn.name 
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    dt.Clear();
                    // objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezones,item.shippersname  );

                    }
                    dt = dt.DefaultView.ToTable(true);

                objvm.indexmodel= indexservices.casedetailstable(dt);
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
                                     where ca.case_id == txtSearch && ag.agent_id == agent_id && ca.attempts_made == selectatt && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
                                     select new
                                     {
                                         case_id = ca.case_id,
                                         attempts_made = ca.attempts_made,
                                         tracking = ca.tracking_number,
                                         title = ca.title,
                                         alternate_title = ca.alternate_title,
                                         commitment_date = ca.commitment_date,
                                         follow_up_date = ca.follow_up_date,
                                         timezones =lk.TimeZone ,
                                         shippersname =sn.name
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    dt.Clear();
                    // objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezones,item.shippersname );

                    }
                    dt = dt.DefaultView.ToTable(true);

                objvm.indexmodel= indexservices.casedetailstable(dt);
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
                else if (selectData == "UTL")
                {
                    // objvm.casedatilsall = db.case_details.ToList();
                    var objcased1 = (from ca in db.case_details
                                         //join e in db.folder_ids on ca.case_id equals e.case_id
                                         //join ep in db.folder_details on e.folder_id equals ep.folder_id
                                     join ag in db.agent_case_details on ca.case_id equals ag.case_id
                                     join lk in db.location_details_origion on ca.case_id equals lk.case_id
                                     join sn in db.shipper_details on ca.case_id equals sn.case_id
                                     where ca.attempts_made == 51 && ag.agent_id == agent_id && ca.attempts_made == selectatt && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)
                                     select new
                                     {
                                         case_id = ca.case_id,
                                         attempts_made = ca.attempts_made,
                                         tracking = ca.tracking_number,
                                         title = ca.title,
                                         alternate_title = ca.alternate_title,
                                         commitment_date = ca.commitment_date,
                                         follow_up_date = ca.follow_up_date,
                                         timezones =lk.TimeZone ,
                                         shippersname =sn.name 
                                     });
                    //DataTable dt = objcased1.CopyToDataTable();

                    dt.Clear();
                    // objvm.casedatils = new List<case_details>();

                    foreach (var item in objcased1)
                    {

                        dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezones,item.shippersname  );

                    }
                    dt = dt.DefaultView.ToTable(true);

                objvm.indexmodel= indexservices.casedetailstable(dt);
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
        public List<agentiddetails> agentcaedetails(string SelectData, string agent_id)
        {
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
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
                                     agent_id=ca.agent_id 

                                 }).Distinct(); 
                //DataTable dt = objcased1.CopyToDataTable();

                dt1.Clear();
                //   objvm.casedatils = new List<case_details>();

                foreach (var item in objcased1)
                {

                    dt1.Rows.Add(item.agent_id );

                }
                objvm.agentiddetails = (from DataRow dr in dt1.Rows
                                    select new agentiddetails() 
                                    {

                                        agent_id  = dr["agent_id"].ToString(),
                                       
                                        //    follow_up_date = Convert.ToDateTime(dr["follow_up_date"].ToString())
                                    }).ToList();


            }
            return objvm.agentiddetails;
        }

        public string timezones(string strtimezone)
        {
            string strtime = null;
            var timezone = db.time_zone_codes.Where((x) => x.fedex_codes == strtimezone).ToList();

            foreach (var strt in timezone )
            {
                strtime = strt.timezone;
            }

            return strtime;
        }
        public List<string> timeinworkinghours()
        {
            var timezons = db.location_details_origion.GroupBy(x => x.TimeZone).Select(x => x.FirstOrDefault()); ;
            string workinghours = null;
            List<string> strworkinghours = new List<string>();
           // List<string> strworkinghours = new List<string>();
            foreach (var time in timezons)
            {
                if (indexservices.timeinworkighours(time.TimeZone) != null)
                {
                    if (workinghours == null) {
                        strworkinghours.Add(time.TimeZone);
                        //workinghours =  workinghours + indexservices.mtdtimezo(time.TimeZone);

                    }
                    else
                    {
                        //workinghours =  workinghours + "," + indexservices.mtdtimezo(time.TimeZone);
                    }


                }
               
            }
            return strworkinghours;
        }
        public List<string> timenotworkinghours()
        {
            var timezons = db.location_details_origion.GroupBy(x => x.TimeZone).Select(x => x.FirstOrDefault()); ;
            string workinghours = null;
            List<string> strworkinghours = new List<string>();
            // List<string> strworkinghours = new List<string>();
            foreach (var time in timezons)
            {
                if (indexservices.timenotworkighours(time.TimeZone) != null)
                {
                    if (workinghours == null)
                    {
                        strworkinghours.Add(time.TimeZone);
                        //workinghours =  workinghours + indexservices.mtdtimezo(time.TimeZone);

                    }
                    else
                    {
                        //workinghours =  workinghours + "," + indexservices.mtdtimezo(time.TimeZone);
                    }


                }

            }
            return strworkinghours;
        }
        public List<Indexmodel> shiipernamesearch(string shiipername,string agent_id)
        {
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
            #region case details
            var objcased1 = (from ca in db.case_details
                                 //join e in db.folder_ids on ca.case_id equals e.case_id
                                 //join ep in db.folder_details on e.folder_id equals ep.folder_id
                             join ag in db.agent_case_details on ca.case_id equals ag.case_id
                             join lk in db.location_details_origion on ca.case_id equals lk.case_id
                             join sn in db.shipper_details on ca.case_id equals sn.case_id
                             //join af in db.status_case_details on ca.case_id  equals af.case_id into yg
                             //from ep1 in yg.DefaultIfEmpty()
                            orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                             //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                             //from ep1 in yg.DefaultIfEmpty()
                             // from y1 on yg.DefaultIfEmpty()
                             where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false)&& sn.name== shiipername
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
            // DateTime timezones = indexview.mtdtimezone();
            dt.Clear();
            // objvm.casedatils = new List<case_details>();


            foreach (var item in objcased1)
            {
                dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezone,item.shippername,item.accountnumber,item.assignment);
            }
            dt = dt.DefaultView.ToTable(true);
            objvm.indexmodel = indexservices.casedetailstable(dt);
            //objvm.indexmodel = (from DataRow dr in dt.Rows
            //                    select new Indexmodel()
            //                    {
            //                        case_id = dr["case_id"].ToString(),
            //                        // attempts_made = ,
            //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
            //                        tracking = dr["tracking"].ToString(),
            //                        title = dr["title"].ToString(),
            //                        alternate_title = dr["alternate_title"].ToString(),
            //                      commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
            //                       shippers_local_time = indexservices.mtdtimezone((dr["timezone"].ToString())),
            //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
            //                        //Convert.ToDateTime(dr["follow_up_date"].ToString()) 
            //                        //indexservices.mtdcunvertdatetime (dr["follow_up_date"].ToString())
            //                    }).ToList();
            //  indexmodel = objvm.indexmodel();
            #endregion
            return objvm.indexmodel;
        }
        public List<Indexmodel> accountnumbersearch(string account_number, string agent_id)
        {
            List<string> listoftimeworking = timeinworkinghours();
            List<string> listoftimenotworking = timenotworkinghours();
            #region case details
            var objcased1 = (from ca in db.case_details
                                 //join e in db.folder_ids on ca.case_id equals e.case_id
                                 //join ep in db.folder_details on e.folder_id equals ep.folder_id
                             join ag in db.agent_case_details on ca.case_id equals ag.case_id
                             join lk in db.location_details_origion on ca.case_id equals lk.case_id
                             join sn in db.shipper_details on ca.case_id equals sn.case_id
                             //join af in db.status_case_details on ca.case_id  equals af.case_id into yg
                             //from ep1 in yg.DefaultIfEmpty()
                            orderby ca.case_priority descending, ca.attempts_made descending, ca.follow_up_date ascending, ca.commitment_date ascending,ca.deadline ascending , ca.assignment ascending
                             //join ep in db.agent_case_attempts_made on ca.case_id equals ep.case_id into yg
                             //from ep1 in yg.DefaultIfEmpty()
                             // from y1 on yg.DefaultIfEmpty()
                             where ag.agent_id == agent_id && (db.status_case_details.Any(x => x.case_id == ca.case_id) == false) && sn.account_number  == account_number
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
            // DateTime timezones = indexview.mtdtimezone();
            dt.Clear();
            // objvm.casedatils = new List<case_details>();


            foreach (var item in objcased1)
            {
                dt.Rows.Add(item.case_id, item.attempts_made, item.tracking, item.title, item.alternate_title, item.commitment_date, item.follow_up_date,item.timezone,item.shippername,item.accountnumber,item.assignment);
            }
            dt = dt.DefaultView.ToTable(true);
            objvm.indexmodel = indexservices.casedetailstable(dt);
            //objvm.indexmodel = (from DataRow dr in dt.Rows
            //                    select new Indexmodel()
            //                    {
            //                        case_id = dr["case_id"].ToString(),
            //                        // attempts_made = ,
            //                        attempts_made = indexservices.attemptsmodecahnge(dr["attempts_made"].ToString()),
            //                        tracking = dr["tracking"].ToString(),
            //                        title = dr["title"].ToString(),
            //                        alternate_title = dr["alternate_title"].ToString(),
            //                      commitment_date = Convert.ToDateTime(dr["commitment_date"].ToString()),
            //                       shippers_local_time = indexservices.mtdtimezone((dr["timezone"].ToString())),
            //                        follow_up_date = string.IsNullOrEmpty(dr["follow_up_date"].ToString()) ? (DateTime?)null : Convert.ToDateTime(dr["follow_up_date"].ToString())
            //                        //Convert.ToDateTime(dr["follow_up_date"].ToString()) 
            //                        //indexservices.mtdcunvertdatetime (dr["follow_up_date"].ToString())
            //                    }).ToList();
            //  indexmodel = objvm.indexmodel();
            #endregion
            return objvm.indexmodel;
        }

        public int getnotification(string case_id)
        {
            var today = DateTime.Today ;
          //  var q = db.Games.Where(t => DbFunctions.TruncateTime(t.StartDate) >= today);
            //var gedanotification = (from gd in db.geda_notifications
            //                       where gd.creation_date == today
            //                       //DbFunctions.TruncateTime(t.StartDate) >= today)
            //                        select new
            //                        {
            //                            notification=gd.notifications 
            //                        }

            //                       );

            var gedanotification = db.geda_notifications.ToList()
                            .Where(x => DateTime.Compare(x.creation_date.Value.Date, today.Date ) == 0)
                            .ToList();
            // objvm.gedanotifications=db.geda_notifications.Where((x) => x.case_id == case_id).ToList();
            return gedanotification.Count();
        }
    }
}