using c3i_fedex.Models;
using c3i_fedex.Persistence;
using c3i_fedex.services;
using c3i_fedex.Services;
using Novell.Directory.Ldap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace c3i_fedex.Controllers
{
    public class C3_FedexController : Controller
    {
        // GET: login
        //Testing comment by jainnedra
        ControllerVariables allvariables = new ControllerVariables();
        CaseviewPersistence caseviewPersistence = new CaseviewPersistence();
        //public string str = "Tracking #";
        string  filename = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + "log\\" + "logErrors.txt";
        
        
        ViewModel objvm = new ViewModel();
        [HttpGet]
        public ActionResult login()
        {
            #region
            var url = Request.RawUrl;
            if (url == @"/")
            {
                Response.Redirect("/c3_fedex/login");
            }
            if(Session["NotValidUser"]!=null)
            {
                ViewBag.NotValidUser = Session["NotValidUser"].ToString();
            }

            return View();
        }
        
        [HttpPost]
        public ActionResult login(string submit, string username,string password)
        {
           // try {
                //CurrencyConvertservices currencyConvertservices = new CurrencyConvertservices();
                //decimal rate = currencyConvertservices.ConvertGOOG(20, "USD", "INR");
                //SupervisorPersistence supervisorPersistence = new SupervisorPersistence();
                //  supervisorPersistence.insertagentcasedetails(allvariables.agent_id, allvariables.agent_id);
                //allvariables.strview = "login";
               // allvariables.userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                Session["userName"] = username;
            Session["supervisor"] = null;

                          ViewData["userName"] = username;
            Session["agent_id"] = username;// "c3700509"; //caseviewPersistence.loginiddetails(username.ToUpper());
                //    allvariables.adPath = System.Configuration.ConfigurationManager.AppSettings["DirectoryDomain"];
                //    try
                //    {
                //        DirectoryEntry entry = new DirectoryEntry(allvariables.adPath, username, password);
                //        Object obj = entry.NativeObject;
                //        DirectorySearcher search = new DirectorySearcher(entry);
                //        search.Filter = "(SAMAccountName=" + username + ")";
                //        search.PropertiesToLoad.Add("cn");
                //        //int SapId;
                //        SearchResult result = search.FindOne();
                //        if (result != null)
                //        {
                //            DirectoryEntry obj1 = result.GetDirectoryEntry();
                //            var obj2 = obj1.Properties;
                //            int sapId = Convert.ToInt32((obj2["physicalDeliveryOfficeName"].Value));
                //            string str = obj2["displayName"].Value.ToString();
                //            Session["userName"] = username;
                //            ViewData["userName"] = username;
                //            Session["agent_id"] = caseviewPersistence.loginiddetails(username.ToUpper());
                //            Session["supervisor"] = caseviewPersistence.loginsupervisordetails(username.ToUpper());
                //             if (Session["supervisor"] != null)
                //            {
                //                allvariables.strview = "supervisor";

            //            }
            //            else if (Session["agent_id"] != null) {
            allvariables.strview = "index";
                //            } 
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        var sw = new System.IO.StreamWriter(filename, true);
                //        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                //        sw.Close();
                //        //ViewBag.NotValidUser
                //        Session["NotValidUser"] = e.Message;

                //        //ViewBag["NotValidUser"] = e.Message;
                //        //TempData["alertMessage"] = "UserName or Password is not correct ";
                //        allvariables.strview = "login";

                //    }
                //}catch (Exception e)
                //{
                //    Session["NotValidUser"] = e.Message;
                //    //ViewBag["NotValidUser"] = e.Message;
                //    allvariables.strview = "login";
                //}

                return Redirect (allvariables.strview);
        }
        [HttpGet]
        public ActionResult logout()
        {
            try {
                /// Session["case_id"] = null;
                /// 
                Session.Clear();
            }
            catch
            {
                return Redirect("login");
            }
            return Redirect("login");
        }
        [HttpGet]
        public ActionResult supervisor(Viewindexresult vindx, Supervisorview supervisorview)
       {
            Indexservices indexservices = new Indexservices();
            try {

                try
                {
                    if (supervisorview.agentfedexid != null && supervisorview.agentname != null && supervisorview.agentmailid != null)
                        indexservices.insertdetailsofagetns(supervisorview.agentfedexid, supervisorview.agentname, supervisorview.agentmailid);
                }
                catch (Exception e)
                {
                    var sw = new StreamWriter(filename, true);
                    sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                    sw.Close();
                }
                //try
                //{
                //    if (supervisorview.newpassword != null && supervisorview.oldpssword  != null && supervisorview.agentfedexid != null)
                //        indexservices.insertdetailsofagetns(supervisorview.agentfedexid, supervisorview.agentname, supervisorview.agentmailid);
                //}
                //catch (Exception e)
                //{
                //    var sw = new StreamWriter(filename, true);
                //    sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                //    sw.Close();
                //}
                SupervisorPersistence supervisorPersistence = new SupervisorPersistence();
                #region commetend data
                //supervisorPersistence.insertagentcasedetails(allvariables.agent_id, allvariables.agent_id);
                ////checking the login details
                //if (Session["userName"] != null)
                //{
                //    allvariables.agent_id = Session["agent_id"].ToString();
                //    //send the user Name Controller to view 
                //    ViewData["userName"] = Session["userName"].ToString();
                //    //index services calss object
                //    Indexservices indexview = new Indexservices();
                //    //index persistence calss object
                //    IndexPersistence indexser = new IndexPersistence();
                //    //SupervisorPersistence supervisorPersistence = new SupervisorPersistence();
                //    //join the case details and agent case details 

                //    try
                //    {
                //        objvm.indexmodel = indexser.casedet(vindx.folder, allvariables.agent_id);
                //        objvm.agentnameandid  = supervisorPersistence.agentnameandid();
                //    }
                //    catch (Exception e)
                //    {
                //        var sw = new System.IO.StreamWriter(filename, true);
                //        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                //        sw.Close();
                //    }

                //    if (vindx.SelectData != null)
                //    {
                //        objvm.agentiddetails = indexser.agentcaedetails(vindx.SelectData, allvariables.agent_id);

                //    }
                //    #region old
                //    int selectatt = indexview.selectattempts(vindx.FilterItems); //agentcaedetails

                //    //Attempt wise case details
                //    if (selectatt != 0)
                //    {
                //        try
                //        {
                //            objvm.indexmodel = supervisorPersistence.casedetails(selectatt, vindx.folder);
                //        }
                //        catch (Exception e)
                //        {
                //            var sw = new System.IO.StreamWriter(filename, true);
                //            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                //            sw.Close();
                //        }
                //        // objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                //        //return View(objvm);
                //    }
                //    if (vindx.folder != null && selectatt != 0)
                //    {
                //        try
                //        {
                //            objvm.indexmodel = supervisorPersistence.casedetails(selectatt, vindx.folder);
                //        }
                //        catch (Exception e)
                //        {
                //            var sw = new System.IO.StreamWriter(filename, true);
                //            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                //            sw.Close();
                //        }
                //        // objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                //        return View(objvm);
                //    }

                //    if (vindx.folder != null && vindx.txtSearch != null && vindx.SelectData != null)
                //    {
                //        try
                //        {
                //            objvm.indexmodel = supervisorPersistence.searchcasedetails(vindx.txtSearch, vindx.SelectData, vindx.folder,allvariables.agent_id  );
                //        }
                //        catch (Exception e)
                //        {
                //            var sw = new System.IO.StreamWriter(filename, true);
                //            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                //            sw.Close();
                //        }
                //      //  objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                //        return View(objvm);
                //    }


                //    if (vindx.txtSearch != null && vindx.SelectData != null)
                //    {
                //        try
                //        {
                //            objvm.indexmodel = supervisorPersistence.searchcasedetails(vindx.txtSearch, vindx.SelectData, vindx.folder,allvariables.agent_id );
                //        }
                //        catch (Exception e)
                //        {
                //            var sw = new System.IO.StreamWriter(filename, true);
                //            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                //            sw.Close();
                //        }
                //        //objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                //        return View(objvm);
                //    }

                //   // checking number of cases available in the folders
                //    #endregion
                //    return View(objvm);
                //}
                //else
                //{
                //    return Redirect("login");
                //}
                //checking the login details
                #endregion
                #region tblcode
                if (Session["userName"] != null)
                {
                    //string timezones = null;
                    allvariables.agent_id = Session["agent_id"].ToString();
                    //StreamReader file =new StreamReader(timezonepath);
                    //while ((timezone = file.ReadLine()) != null)
                    //{

                    //    timezones = timezone.ToString();
                    //}


                    //var inTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezones);
                    //DateTime inTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, inTimeZone);
                    //file.Close();

                    //send the user Name Controller to view 
                    ViewData["userName"] = Session["userName"].ToString();
                    //index services calss object
                    Indexservices indexview = new Indexservices();
                    //index persistence calss object
                    IndexPersistence indexser = new IndexPersistence();

                    //join the case details and agent case details 
                    try
                    {
                        //objvm.indexmodel.Clear();
                        objvm.indexmodel = supervisorPersistence.casedet(vindx.folder, allvariables.agent_id);
                        objvm.agentDetails=supervisorPersistence.agentdetailsofcases(allvariables.agent_id);
                    }
                    catch (Exception e)
                    {

                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                    }
                    int selectatt = indexview.selectattempts(vindx.FilterItems);

                    //Attempt wise case details
                    if (selectatt != 0)
                    {
                        try
                        {

                            objvm.indexmodel.Clear();
                            objvm.indexmodel = supervisorPersistence.casedetails(selectatt, vindx.folder);
                            objvm.agentDetails = supervisorPersistence.agentdetailsofcases(allvariables.agent_id);
                        }
                        catch (Exception e)
                        {
                            var sw = new System.IO.StreamWriter(filename, true);
                            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                            sw.Close();
                        }
                        // objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                        //return View(objvm);
                    }
                    if (vindx.folder != null && selectatt != 0)
                    {
                        try
                        {
                            objvm.indexmodel.Clear();
                            objvm.indexmodel = supervisorPersistence.casedetails(selectatt, vindx.folder);
                            objvm.agentDetails = supervisorPersistence.agentdetailsofcases(allvariables.agent_id);
                        }
                        catch (Exception e)
                        {
                            var sw = new System.IO.StreamWriter(filename, true);
                            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                            sw.Close();
                        }
                        // objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                        //return View(objvm);
                    }

                    if (vindx.folder != null && vindx.txtSearch != null && vindx.SelectData != null)
                    {
                        try
                        {
                            objvm.indexmodel.Clear();
                            objvm.indexmodel = supervisorPersistence.searchcasedetails(vindx.txtSearch, vindx.SelectData, vindx.folder, allvariables.agent_id);
                            objvm.agentDetails = supervisorPersistence.agentdetailsofcases(allvariables.agent_id);
                        }
                        catch (Exception e)
                        {
                            var sw = new System.IO.StreamWriter(filename, true);
                            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                            sw.Close();
                        }
                        // objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                        // return View(objvm);
                    }


                    if (vindx.txtSearch != null && vindx.SelectData != null)
                    {
                        try
                        {
                            objvm.indexmodel.Clear();
                            objvm.indexmodel = supervisorPersistence.searchcasedetails(vindx.txtSearch, vindx.SelectData, vindx.folder, allvariables.agent_id);
                            objvm.agentDetails = supervisorPersistence.agentdetailsofcases(allvariables.agent_id);
                        }
                        catch (Exception e)
                        {
                            var sw = new System.IO.StreamWriter(filename, true);
                            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                            sw.Close();
                        }
                        //objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                        // return View(objvm);
                    }
                    //if (selectatt != 0 && vindx.txtSearch != null && vindx.SelectData != null)
                    //{
                    //    try
                    //    {
                    //        objvm.indexmodel.Clear();
                    //        objvm.indexmodel = supervisorPersistence.searchcasedetailsusingattemts(selectatt, vindx.txtSearch, vindx.SelectData, allvariables.agent_id);
                    //        objvm.agentDetails = supervisorPersistence.agentdetailsofcases(allvariables.agent_id);
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        var sw = new System.IO.StreamWriter(filename, true);
                    //        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                    //        sw.Close();
                    //    }
                    //    //objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                    //    // return View(objvm);
                    //}
                    // checking number of cases available in the folders 
                    try
                    {
                        objvm.All = indexser.countoftabledata("All", allvariables.agent_id);
                        objvm.Newly_Assigned = indexser.countoftabledata("New", allvariables.agent_id);
                        objvm.WIP = indexser.countoftabledata("WIP", allvariables.agent_id);
                        objvm.UTL = indexser.countoftabledata("UTL", allvariables.agent_id);
                        objvm.GEDA = indexser.countoftabledata("GEDA", allvariables.agent_id);
                        objvm.supervisor  = indexser.countoftabledata("Undersupervisor", allvariables.agent_id);
                        objvm.accounts  = indexser.countoftabledata("Blockedaccounts", allvariables.agent_id);
                        objvm.hours  = indexser.countoftabledata("24hours", allvariables.agent_id);
                        //checking which button is active
                        objvm.allactive = indexview.folderactive(vindx.folder);
                        objvm.newactive = indexview.folderactive(vindx.folder);
                        objvm.wipactive = indexview.folderactive(vindx.folder);
                        objvm.utlactive = indexview.folderactive(vindx.folder);
                        objvm.gedaactive = indexview.folderactive(vindx.folder);
                        objvm.undersupervisor  = indexview.folderactive(vindx.folder);
                        objvm.blockedaccounts  = indexview.folderactive(vindx.folder);
                        objvm.hours24  = indexview.folderactive(vindx.folder);
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                    }
                    #endregion
               
                return View(objvm);
            }
            else
            {
                return Redirect("login");
            }
            }
            catch
            {
                return Redirect("login");
            }
        }

        [HttpPost]
        public ActionResult supervisoragent(Viewindexresult vindx, Supervisorview supervisorview)
        {
            SupervisorPersistence supervisorpersistence = new SupervisorPersistence();
            string agetnid="";
            supervisorpersistence.agentdetailsofcases(agetnid);

            return View(objvm);
        }

        [HttpPost]
        public ActionResult supervisor(Supervisorview supervisorview)
        {
            try {
                //s string case_id=null;
                SupervisorPersistence supervisorPersistence = new SupervisorPersistence();
                Indexservices indexservices = new Indexservices();
                //string[] ids = formCollection["tblcaseid"].Split(new char[] { ',' });
                allvariables.agent_id = Session["agent_id"].ToString();

                try
                {
                    if (supervisorview.newpassword != null && supervisorview.oldpassword  != null && supervisorview.txtuserid  != null)
                        indexservices.insertbotpassword(supervisorview.txtuserid, supervisorview.oldpassword, supervisorview.newpassword);
                }
                catch (Exception e)
                {
                    var sw = new StreamWriter(filename, true);
                    sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                    sw.Close();
                }
                if (supervisorview.tblcaseid == null)
                { }
                else
                {
                    foreach (var caseid in supervisorview.tblcaseid)
                    {
                        try
                        {
                            //  foreach (string case_id in supervisorview.tblcaseid)
                            {
                                supervisorPersistence.insertagentcasedetails(caseid, allvariables.agent_id);
                            }
                        }


                        catch (Exception e)
                        {
                            var sw = new StreamWriter(filename, true);
                            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                            sw.Close();
                        }
                    }
                }
            }
            catch
            {
                return Redirect("login");
            }
            return Redirect("supervisor");
        }
        //index page 
        [HttpGet]
        public ActionResult index(Viewindexresult vindx )

        {
            try { 
            //checking the login details
            if (Session["userName"] != null)
            {
                //string timezones = null;
                allvariables.agent_id = Session["agent_id"].ToString();
                //StreamReader file =new StreamReader(timezonepath);
                //while ((timezone = file.ReadLine()) != null)
                //{

                //    timezones = timezone.ToString();
                //}

                
                //var inTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezones);
                //DateTime inTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, inTimeZone);
                //file.Close();

                //send the user Name Controller to view 
                ViewData["userName"] = Session["userName"].ToString();
                //index services calss object
                Indexservices indexview = new Indexservices();
                //index persistence calss object
                IndexPersistence indexser = new IndexPersistence();
               
                //join the case details and agent case details 
                try {
                    //objvm.indexmodel.Clear();
                    objvm.indexmodel = indexser.casedet(vindx.folder, allvariables.agent_id);
                        //objvm.shippersname = indexser.shippername(vindx.folder, allvariables.agent_id);
                        // objvm.shippersname = indexser.casedet(vindx.folder, allvariables.agent_id);
                        // objvm.ShipperDetails =indexser.
                    }
                    catch (Exception e)
                {

                    var sw = new System.IO.StreamWriter(filename, true);
                    sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                    sw.Close();
                        return View(objvm);
                }
                int selectatt = indexview.selectattempts(vindx.FilterItems);
               
                //Attempt wise case details
                if (selectatt != 0)
                {
                    try {
                       
                        objvm.indexmodel.Clear();
                        objvm.indexmodel = indexser.casedetails(selectatt, vindx.folder, allvariables.agent_id);
                    }catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                            return View(objvm);
                        }
                   // objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                    //return View(objvm);
                }
                if(vindx.folder != null && selectatt != 0)
                {
                    try {
                         objvm.indexmodel.Clear();
                        objvm.indexmodel = indexser.casedetails(selectatt, vindx.folder, allvariables.agent_id);
                    }catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                            return View(objvm);
                        }
                   // objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                    //return View(objvm);
                }
               
                if(vindx.folder !=null && vindx.txtSearch != null && vindx.SelectData != null)
                {
                    try {
                        objvm.indexmodel.Clear();
                        objvm.indexmodel = indexser.searchcasedetails(vindx.txtSearch, vindx.SelectData, vindx.folder, allvariables.agent_id);
                    }catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                            return View(objvm);
                        }
                   // objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                   // return View(objvm);
                }

                
                if (vindx.txtSearch != null && vindx.SelectData != null)
                {
                    try {
                            vindx.txtSearch.Trim();
                         objvm.indexmodel.Clear();
                        objvm.indexmodel = indexser.searchcasedetails(vindx.txtSearch, vindx.SelectData, vindx.folder, allvariables.agent_id);
                    }catch  (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                            return View(objvm);
                        }
                    //objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                   // return View(objvm);
                }
                if(selectatt !=0 && vindx.txtSearch != null && vindx.SelectData != null)
                {
                    try
                    {
                        objvm.indexmodel.Clear();
                        objvm.indexmodel = indexser.searchcasedetailsusingattemts(selectatt,vindx.txtSearch, vindx.SelectData, allvariables.agent_id);
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                            return View(objvm);
                        }
                        //objvm.indexmodel = Indexservices.ConvertToList<Indexmodel>(dttable1);
                        // return View(objvm);
                    }
                    if (vindx.shippername  != null)
                    {
                        try
                        {
                            objvm.indexmodel.Clear();
                            objvm.indexmodel = indexser.shiipernamesearch(vindx.shippername,allvariables.agent_id);
                        }
                        catch (Exception e)
                        {
                            var sw = new System.IO.StreamWriter(filename, true);
                            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                            sw.Close();
                            return View(objvm);
                        }
                    }
                    if (vindx.account_number  != null)
                    {
                        try
                        {
                            objvm.indexmodel.Clear();
                            objvm.indexmodel = indexser.accountnumbersearch(vindx.account_number, allvariables.agent_id);
                        }
                        catch (Exception e)
                        {
                            var sw = new System.IO.StreamWriter(filename, true);
                            sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                            sw.Close();
                            return View(objvm);
                        }
                    }
                    try
                    {
                        objvm.gedanotificationcount = indexser.getnotification(vindx.folder);
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                        return View(objvm);

                    }

                    // checking number of cases available in the folders 
                    try {
                        objvm.All = indexser.countoftabledata("All", allvariables.agent_id);
                        objvm.Newly_Assigned = indexser.countoftabledata("New", allvariables.agent_id);
                        objvm.WIP = indexser.countoftabledata("WIP", allvariables.agent_id);
                        objvm.UTL = indexser.countoftabledata("UTL", allvariables.agent_id);
                        objvm.GEDA = indexser.countoftabledata("GEDA", allvariables.agent_id);
                        objvm.supervisor = indexser.countoftabledata("Undersupervisor", allvariables.agent_id);
                        objvm.accounts = indexser.countoftabledata("Blockedaccounts", allvariables.agent_id);
                        objvm.hours = indexser.countoftabledata("24hours", allvariables.agent_id);
                        //checking which button is active
                        objvm.allactive = indexview.folderactive(vindx.folder);
                        objvm.newactive = indexview.folderactive(vindx.folder);
                        objvm.wipactive = indexview.folderactive(vindx.folder);
                        objvm.utlactive = indexview.folderactive(vindx.folder);
                        objvm.gedaactive = indexview.folderactive(vindx.folder);
                        objvm.undersupervisor = indexview.folderactive(vindx.folder);
                        objvm.blockedaccounts = indexview.folderactive(vindx.folder);
                        objvm.hours24 = indexview.folderactive(vindx.folder);
                    }
                catch(Exception e)
                {
                    var sw = new System.IO.StreamWriter(filename, true);
                    sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                    sw.Close();
                        return View(objvm);
                    }
                return View(objvm);
            }
            else
            {
                return Redirect("login");
            }
            }
            catch
            {
                return Redirect("login");
            }

        }
        //caseview page
        [HttpGet ]
        public ActionResult caseview(string case_id )
        {
            try { 
            if(Session["Success"]!= null)
            ViewBag.msg = Session["Success"].ToString();

            if (Session["userName"] != null)
            {
                ViewData["userName"] = Session["userName"].ToString();
                if (case_id==null)
                {
                    case_id = Session["caseid"].ToString();
                }
                Session["caseid"] = case_id;       
                CaseviewPersistence indexser = new CaseviewPersistence();               
                ViewModel objvm = new ViewModel();               
                //send the case_id and get the total inforamtion of case             
                try
                {                    
                     objvm = indexser.casedet(case_id);
                    Session["agentid"] = indexser.objagentcase.ToString();
                }
                catch (Exception e)
                {
                    var sw = new System.IO.StreamWriter(filename, true);
                    sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                    sw.Close();
                        return Redirect("login");
                    }
                return View(objvm);
            }
            else
            {
                return Redirect("login");
            }
            }
            catch
            {
                return Redirect("login");
            }
            // obj}vm.PrimaryRepDetails  = dbcase.primary_rep_details.Where((x) => x.primary_rep_id == objprimarycase).ToList().ToString();

        }     
        [HttpPost]
        public ActionResult caseview(CaseViewModel caseviewmodel)
        {
            try { 
           // CurrencyConvertservices
                //CurrencyConvertservices currencyConvertservices = new CurrencyConvertservices();
                Indexservices indexservices = new Indexservices(); 
                CaseviewPersistence caseviewpersistence = new CaseviewPersistence();
                string case_id = Session["caseid"].ToString();
                string agent_id = Session["agentid"].ToString();  
                  
               if(caseviewmodel.btnreverserate== "Reverse Rate")
                {
                    try
                    {
                        indexservices.insertbottrigger(case_id, "reverse_rate");
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                    }
                }
                else if (caseviewmodel.PackageScandetailsrefresh == "Refresh")
                {
                    try
                    {
                        indexservices.insertbottrigger(case_id, "refresh");
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                    }
                }
                else if (caseviewmodel.btnactivatetracking  == "Activate Tracking")
                {
                    try
                    {
                        indexservices.insertbottrigger(case_id, "activate_tracking");
                        caseviewpersistence.updategedatable(case_id, 5);
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                    }
                }
                else if (caseviewmodel.btnretrieve  == "Retrieve")
                {
                    try
                    {
                        indexservices.insertbottrigger(case_id, "retrieve");
                        caseviewpersistence.updategedatable(case_id, 1);
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                    }
                }
                else if (caseviewmodel.btnreactivatetracking  == "Reactivate Tracking")
                {
                    try
                    {
                        indexservices.insertbottrigger(case_id, "reactivate_tracking");
                        caseviewpersistence.updategedatable(case_id, 5);
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                    }
                }

                else if (caseviewmodel.btnsave == "Save")
            {
                try
                {
                    bool insert = false;
                    insert = caseviewpersistence.inserttable(case_id, insert);
                    indexservices.caseviewrequirements(case_id, caseviewmodel.input1, caseviewmodel.box1);
                    caseviewpersistence.updatecasetable(caseviewmodel.emailid, caseviewmodel.Deadline1,case_id);

                        caseviewpersistence.updatedeadline(case_id, caseviewmodel.Deadline1);

                        //    if(caseviewmodel.btnsaveandreturn == "Save & Return")
                        // indexservices.insertbottrigger(case_id, "Send Template");

                    }
                    catch (Exception e)
                {
                    var sw = new System.IO.StreamWriter(filename, true);
                    sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                    sw.Close();
                }
            }
                //btnsaveandreturn

          else if (caseviewmodel.btnsave == "Save & Update" || caseviewmodel.btnsaveandreturn== "Save & Return")
            {
                try {

                    indexservices.insertcasenotes(case_id, agent_id, caseviewmodel);
                      
                    }
                    catch (Exception e)
                {
                    var sw = new System.IO.StreamWriter(filename, true);
                    sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                    sw.Close();
                }
            }
            else if(caseviewmodel.btnsend == "Send")
                {
                    try
                    {
                        indexservices.insertsendtemp(case_id, agent_id, caseviewmodel);
                    }
                    catch (Exception e)
                    {
                        var sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(DateTime.Now.ToString() + " " + e.Message + " " + e.InnerException);
                        sw.Close();
                    }
                }
            
           

            TempData["Success"] = true;// "Save Successful  ";
            ViewBag.msg = "View Bag Value";
                 }catch
            {
                return Redirect("login");
            }
           // Session["Success"] = "Save Successful  ";
            return Redirect("caseview"); 
        }
        #endregion


       
    }
}