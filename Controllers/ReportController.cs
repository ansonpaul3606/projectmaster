using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.ViewModel;

using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;

using PerfectWebERP.ReportModel;
using System.Net;
using PerfectWebERP.Filters;
using Newtonsoft.Json;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ReportController : Controller
    {
        // GET: Report
        string Reportname = "";
        string TransMode = "RptTransMode";
        public ActionResult GetCustomerTicketsReport(ReportLoadModel.Rppsarams data)//,int Report_Id , string FK_Employee, int Rptype, string FromDate, string ToDate, int Status, string RpFieldptype)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            Rpparams reportparam = new Rpparams();

            {
                if (data.ReportMode == 1)//Report_Id == 1)
                {
                    reportparam.ReportSchema = "CustomerTicke";
                    reportparam.Reportname = "CustTicket.rpt";
                    reportparam.filename = "Customer Ticket Report";

                }
                if (data.ReportMode == 2)//Report_Id == 2)
                {
                    reportparam.ReportSchema = "OutstandingList";
                    reportparam.Reportname = "OutStandingList.rpt";
                    reportparam.filename = "Outstanding List Report";
                }
                if (data.ReportMode/*Report_Id*/ == 3)
                {
                    reportparam.ReportSchema = "CustomerTicketsReport";
                    reportparam.Reportname = "CustTicket1.rpt";
                    reportparam.filename = "Customer Ticket Report";
                }
                reportparam.FK_Employee = data.FK_Employee; //FK_Employee;
                reportparam.FromDate = Convert.ToDateTime(data.FromDate/*FromDate*/);
                reportparam.ToDate = Convert.ToDateTime(data.ToDate/*ToDate*/);
                reportparam.Rptype = data.Rptype/*Rptype*/;
                reportparam.RpFieldptype = data.RpFieldptype /*RpFieldptype*/;
                reportparam.Status = data.Status /*Status*/;
                //reportparam.ReportSchema = "CustomerTicketsReport";
                reportparam.TableCount = 0;





            };
            Session["Rpparams"] = reportparam;
            try
            {

                //CustomerTicketsModel country = new CustomerTicketsModel();

                //var outputData = country.GetCustomerReport(input: new CustomerTicketsModel.customerid
                //{

                //    FK_Company = _userLoginInfo.FK_Company,
                //    FK_Branch = _userLoginInfo.FK_Branch,
                //    FK_Employee = FK_Employee,
                //    FromDate = Convert.ToDateTime(FromDate),
                //    ToDate = Convert.ToDateTime(ToDate),
                //    Status = Status,


                //}, companyKey: _userLoginInfo.CompanyKey);

                ////Session["User"] = _userLoginInfo.UserName;
                ///////Session["Reportname"] = "CustTicket1.rpt";
                Session["RpFieldptype"] = data.RpFieldptype/*RpFieldptype*/;
                ////Session["Rptype"] = Rptype;
                ////Session["FK_Employee"] = FK_Employee;
                ////Session["Status"] = Status;
                //Session["Reportdata"] = outputData.Data;
                //Session["filename"] = "Customer Ticket Report";
                ////Session["Fromdate"] = Convert.ToDateTime(FromDate).ToString("dd/MM/yyyy");
                ////Session["ToDate"] = Convert.ToDateTime(ToDate).ToString("dd/MM/yyyy");

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();
                //if (outputData.Data == null)
                //{
                //    return Content("No Data Found");
                //}

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult GetReport(ReportLoadModel.Reportparameters data)//,int Report_Id , string FK_Employee, int Rptype, string FromDate, string ToDate, int Status, string RpFieldptype)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                Reportpassparams reportpassparam = new Reportpassparams();

                {
                    //if (data.ReportMode == 1)
                    //{
                    //    reportpassparam.ReportSchema = "CustomerTicketsReport";
                    //    reportpassparam.Reportname = "CustTicket1.rpt";
                    //    reportpassparam.filename = "Customer Ticket Report";

                    //}
                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "OutstandingpendingList";
                        reportpassparam.Reportname = "OutStandPendingList.rpt";
                        reportpassparam.filename = "Action List";
                    }
                    else if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "OutstandingcompletedList";
                        reportpassparam.Reportname = "OutstandCompletedList.rpt";
                        reportpassparam.filename = "Follow up List";
                    }
                    else if (data.ReportMode == 3)
                    {
                        reportpassparam.ReportSchema = "Ledger";
                        reportpassparam.Reportname = "Ledger.rpt";
                        reportpassparam.filename = "Ledger Report";
                    }
                    else if (data.ReportMode == 4)
                    {
                        reportpassparam.ReportSchema = "StatusList";
                        reportpassparam.Reportname = "StatusList.rpt";
                        reportpassparam.filename = "Status List";
                    }
                    else if (data.ReportMode == 5)
                    {
                        reportpassparam.ReportSchema = "NewList";
                        reportpassparam.Reportname = "NewList.rpt";
                        reportpassparam.filename = "New List";
                    }


                    string employee;
                    string product;
                    string action;
                    string actiontype;
                    string statuscd;
                    string crtname;
                    string leadnam;
                    if (data.FK_Employee != 0)
                    {
                        employee = "  Employee : " + data.Employee + ",";
                    }
                    else { employee = ""; }
                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }
                    if (data.FollowUpAction != 0)
                    {
                        action = "  Action : " + data.Actioname + ",";
                    }
                    else { action = ""; }
                    if (data.FollowUType != 0)
                    {
                        actiontype = "  Action Type : " + data.Actiontypname + ",";
                    }
                    else { actiontype = ""; }
                    if (data.Status != 0)
                    {
                        statuscd = "  Status : " + data.Statusname + ",";
                    }
                    else { statuscd = ""; }
                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }
                    if (data.FK_Priority != 0)
                    {
                        leadnam = "  Lead Type : " + data.Leadname + ",";
                    }
                    else { leadnam = ""; }
                    if (data.FK_Employee != 0 || data.FollowUpAction != 0 || data.FollowUType != 0 || data.Status != 0 || data.FK_Product != 0 || data.Criteria != 0 || data.FK_Priority != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + employee + product + action + actiontype + statuscd + leadnam + crtname;
                    }
                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Product = data.FK_Product;
                    reportpassparam.FromDate = data.FromDate;
                    reportpassparam.ToDate = data.ToDate;
                    reportpassparam.FollowUpAction = data.FollowUpAction;
                    reportpassparam.FollowUType = data.FollowUType;
                    reportpassparam.FK_Priority = data.FK_Priority;
                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.FK_Employee = data.FK_Employee;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = data.TableCount;
                    reportpassparam.LeadNo = data.LeadNo;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;
                    reportpassparam.Status = data.Status;
                    reportpassparam.Reportsection = "Lead";
                    reportpassparam.FK_CollectedBy = data.FK_CollectedBy;
                    reportpassparam.FK_Area = data.FK_Area;
                    reportpassparam.Category = data.Category;
                    reportpassparam.ReportFrom = data.ReportFrom;
                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }
                };
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #region old
        public ActionResult GetProjectReportsOld(ReportLoadModel.Reportparameters data)//,int Report_Id , string FK_Employee, int Rptype, string FromDate, string ToDate, int Status, string RpFieldptype)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                Reportpassparams reportpassparam = new Reportpassparams();
                {
                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "ProjectList";
                        reportpassparam.Reportname = "ProjectList.rpt";
                        reportpassparam.filename = "Project List";
                    }
                    else if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "ProjectTeamList";
                        reportpassparam.Reportname = "ProjectwiseTeams.rpt";
                        reportpassparam.filename = "Project wise Team Report";
                    }
                    else if (data.ReportMode == 3)
                    {
                        reportpassparam.ReportSchema = "CategorywiseProjects";
                        reportpassparam.Reportname = "CategorywiseProjects.rpt";
                        reportpassparam.filename = "Category wise Project Report";
                    }
                    else if (data.ReportMode == 4)
                    {
                        reportpassparam.ReportSchema = "SiteVisit";
                        reportpassparam.Reportname = "SiteVisit.rpt";
                        reportpassparam.filename = "Site Visit Report";
                    }
                    else if (data.ReportMode == 5)
                    {
                        reportpassparam.ReportSchema = "CostPreparation";
                        reportpassparam.Reportname = "CostPreparation.rpt";
                        reportpassparam.filename = "Cost Preparation Report";
                    }
                    else if (data.ReportMode == 6)
                    {
                        reportpassparam.ReportSchema = "MaterialDetails";
                        reportpassparam.Reportname = "MaterialAllocation.rpt";
                        reportpassparam.filename = "Project Wise Material Allocation Report";
                    }
                    else if (data.ReportMode == 7)
                    {
                        reportpassparam.ReportSchema = "MaterialDetailsUsage";
                        reportpassparam.Reportname = "MaterialUsage.rpt";
                        reportpassparam.filename = "Project Wise  Material Usage Report";
                    }
                    else if (data.ReportMode == 8)
                    {
                        reportpassparam.ReportSchema = "MaterialDetailsWastage";
                        reportpassparam.Reportname = "MaterialWastage.rpt";
                        reportpassparam.filename = "Project Wise  Material Wastage Report";
                    }
                    else if (data.ReportMode == 9)
                    {
                        reportpassparam.ReportSchema = "MaterialDetailsDamage";
                        reportpassparam.Reportname = "MaterialDamage.rpt";
                        reportpassparam.filename = "Project Wise  Material Damage Report";
                    }
                    else if (data.ReportMode == 10)
                    {
                        reportpassparam.ReportSchema = "MaterialDetailsBalance";
                        reportpassparam.Reportname = "MaterialBalance.rpt";
                        reportpassparam.filename = "Project Wise  Material Balance/Stock Report";
                    }
                    else if (data.ReportMode == 11)
                    {
                        reportpassparam.ReportSchema = "ExtraWorkDetails";
                        reportpassparam.Reportname = "ProjectExtraWork.rpt";
                        reportpassparam.filename = "Project Wise  Extra Work Report";
                    }
                    else if (data.ReportMode == 12)
                    {
                        reportpassparam.ReportSchema = "ProjectStatus";
                        reportpassparam.Reportname = "ProjectStatus.rpt";
                        reportpassparam.filename = "Project Status List Report";
                    }

                    string employee;
                    string product;
                    string action;
                    string actiontype;
                    string statuscd;
                    string crtname;
                    string leadnam;
                    if (data.FK_Employee != 0)
                    {
                        employee = "  Employee : " + data.Employee + ",";
                    }
                    else { employee = ""; }
                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }
                    if (data.FollowUpAction != 0)
                    {
                        action = "  Action : " + data.Actioname + ",";
                    }
                    else { action = ""; }
                    if (data.FollowUType != 0)
                    {
                        actiontype = "  Action Type : " + data.Actiontypname + ",";
                    }
                    else { actiontype = ""; }
                    if (data.Status != 0)
                    {
                        statuscd = "  Status : " + data.Statusname + ",";
                    }
                    else { statuscd = ""; }
                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }
                    if (data.FK_Priority != 0)
                    {
                        leadnam = "  Lead Type : " + data.Leadname + ",";
                    }
                    else { leadnam = ""; }
                    if (data.FK_Employee != 0 || data.FollowUpAction != 0 || data.FollowUType != 0 || data.Status != 0 || data.FK_Product != 0 || data.Criteria != 0 || data.FK_Priority != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + employee + product + action + actiontype + statuscd + leadnam + crtname;
                    }
                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.Branchname = "All Branch";
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = data.TableCount;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.LeadNo = data.LeadNo;
                    reportpassparam.Criteria = data.Criteria;
                    reportpassparam.Reportsection = "Project";
                    reportpassparam.FK_Area = data.FK_Area;
                    reportpassparam.Category = data.Category;

                };
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult GetServiceReportOld(ReportLoadModel.Reportparameters data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                Reportpassparams reportpassparam = new Reportpassparams();

                {

                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "CustomerService";
                        reportpassparam.Reportname = "CustomerService.rpt";
                        reportpassparam.filename = "New List Report";
                    }
                    if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "TicketLedger";
                        reportpassparam.Reportname = "TicketLedger.rpt";
                        reportpassparam.filename = "Ticket Ledger Report";
                    }
                    if (data.ReportMode == 3)
                    {
                        reportpassparam.ReportSchema = "ServiceOutstanding";
                        reportpassparam.Reportname = "ServiceOutstandingReport.rpt";
                        reportpassparam.filename = "Service Outstanding Report";
                    }
                    if (data.ReportMode == 4)
                    {
                        reportpassparam.ReportSchema = "ServiceActionList";
                        reportpassparam.Reportname = "ServiceActionList.rpt";
                        reportpassparam.filename = "Service Action List";
                    }
                    if (data.ReportMode == 5)
                    {
                        reportpassparam.ReportSchema = "EmployeeWiseOutstanding";
                        reportpassparam.Reportname = "EmployeeWiseOutstanding.rpt";
                        reportpassparam.filename = "Employee Wise Outstanding Report";
                    }
                    else if (data.ReportMode == 6)
                    {
                        reportpassparam.ReportSchema = "ServiceList";
                        reportpassparam.Reportname = "ServiceList.rpt";
                        reportpassparam.filename = "Service List";
                    }
                    else if (data.ReportMode == 7)
                    {
                        reportpassparam.ReportSchema = "ReplacedProductList";
                        reportpassparam.Reportname = "ReplacedProductList.rpt";
                        reportpassparam.filename = "Replaced Product List";
                    }
                    string employee;
                    string product;
                    string complainttype;
                    //string complaintservicename;
                    //string complaintproducttype;
                    string statuscd;
                    string crtname;
                    string priority;
                    string area = "";
                    string category = "";
                    string nextaction = "";
                    string rplcmnttype;
                    if (data.FK_Employee != 0)
                    {
                        employee = "  Employee : " + data.Employee + ",";
                    }
                    else { employee = ""; }
                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }
                    if (data.ComplaintType != 0 && data.ComplaintService == 1)
                    {
                        complainttype = "  Complaint : " + data.complaintservicename + ",";
                    }

                    else if (data.ComplaintType != 0 && data.ComplaintService == 2)
                    {
                        complainttype = "  Service : " + data.complaintservicename + ",";
                    }
                    else { complainttype = ""; }

                    if (data.Status != 6 && (data.ReportMode == 3 || data.ReportMode == 4 || data.ReportMode == 5))
                    {
                        statuscd = "  Status : " + data.Statusname + ",";
                    }
                    else { statuscd = ""; }
                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }
                    if (data.FK_Area != 0)
                    {
                        area = " Area : " + data.areas;
                    }
                    else { area = ""; }
                    if (data.FK_Category != 0)
                    {
                        category = "  Category : " + data.categorys;
                    }
                    else { category = ""; }
                    if (data.FK_Priority != 0)
                    {
                        priority = "  Priority : " + data.Priority + ",";
                    }
                    else { priority = ""; }
                    if (data.FK_NextAction != 0)
                    {
                        nextaction = "  Priority : " + data.Priority + ",";
                    }
                    else { nextaction = ""; }
                    if (data.ReplacementType != 0)
                    {
                        rplcmnttype = "  Replacement Type : " + data.ReplacementTypeName + ",";
                    }
                    else { rplcmnttype = ""; }
                    if (data.FK_Employee != 0 || data.ComplaintType != 0 || data.ComplaintProductType != 0 || data.Status != 0 || data.FK_Product != 0 || data.Criteria != 0 || data.FK_Priority != 0 || data.FK_NextAction != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + employee + product + complainttype + statuscd + priority + area + category + nextaction + rplcmnttype + crtname;
                    }
                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Product = data.FK_Product;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.ComplaintType = data.ComplaintType;
                    reportpassparam.ComplaintProductType = data.ComplaintProductType;
                    reportpassparam.FK_Priority = data.FK_Priority;
                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.FK_Employee = data.FK_Employee;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = data.TableCount;
                    reportpassparam.TicketNo = data.TicketNo;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;
                    reportpassparam.Status = data.Status;
                    reportpassparam.Reportsection = "Customer Service";
                    reportpassparam.FK_Category = data.FK_Category;
                    reportpassparam.FK_Area = data.FK_Area;
                    reportpassparam.FK_NextAction = data.FK_NextAction;
                    reportpassparam.DueDaysFrom = data.DueDaysFrom;
                    reportpassparam.DueDaysTo = data.DueDaysTo;
                    reportpassparam.DueCriteria = data.DueCriteria;
                    reportpassparam.ComplaintService = data.ComplaintService;
                    reportpassparam.ReplacementType = data.ReplacementType;
                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }
                };
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult GetSalesReportOld(saleReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                Reportpassparams reportpassparam = new Reportpassparams();


                {
                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "Saleslist";
                        reportpassparam.Reportname = "SalesList.rpt";
                        reportpassparam.filename = "Sales Register Report";
                    }
                    if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "Saleslistdet";
                        reportpassparam.Reportname = "SalesDetailedList.rpt";
                        reportpassparam.filename = "Sales Detailed Report";
                    }
                    if (data.ReportMode == 3)
                    {
                        reportpassparam.ReportSchema = "Saleslistnondet";
                        reportpassparam.Reportname = "SalesNondetailed.rpt";
                        reportpassparam.filename = "Sales Non Detailed Report";
                    }
                    if (data.ReportMode == 4)
                    {
                        reportpassparam.ReportSchema = "HoldedLists";
                        reportpassparam.Reportname = "HoldedList.rpt";
                        reportpassparam.filename = "Holded List Report";
                    }
                    if (data.ReportMode == 5)
                    {
                        reportpassparam.ReportSchema = "SalesLedgers";
                        reportpassparam.Reportname = "SalesLedger.rpt";
                        reportpassparam.filename = "Sales Ledger Report";
                    }
                    if (data.ReportMode == 14)
                    {
                        reportpassparam.ReportSchema = "PaymentMethodType";
                        reportpassparam.Reportname = "PaymentMethodType.rpt";
                        reportpassparam.filename = "Payment Method Type Report";
                    }
                    string product;
                    string crtname;
                    string catgrynm;
                    string departmnt;
                    string billtyp;
                    string paymentmethod;
                    if (data.FK_Department != 0)
                    {
                        departmnt = "  Department : " + data.depname + ",";
                    }
                    else { departmnt = ""; }
                    if (data.billtype != 0)
                    {
                        billtyp = "  Bill Type : " + data.billtypename + ",";
                    }
                    else { billtyp = ""; }

                    if (data.pmtype != 0)
                    {
                        paymentmethod = "  Payment Method Type  : " + data.pmtypename + ",";
                    }
                    else { paymentmethod = ""; }
                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }
                    if (data.FK_Category != 0)
                    {
                        catgrynm = "  Category : " + data.categorys + ",";
                    }
                    else { catgrynm = ""; }


                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }


                    if (data.FK_Product != 0 || data.Criteria != 0 || data.FK_Department != 0 || data.billtype != 0 || data.FK_Category != 0 || data.pmtype != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + billtyp + paymentmethod + departmnt + catgrynm + product + crtname;
                    }
                    var TranSMode = "INSL";
                    if (data.ReportMode == 1 || data.ReportMode == 3)
                    {
                        data.ReportMode = 2;
                        TranSMode = "INSL";
                    }
                    else if (data.ReportMode == 4)
                    {
                        data.ReportMode = 3;
                        TranSMode = "INSLHO";
                    }
                    else if (data.ReportMode == 5)
                    {
                        data.ReportMode = 4;
                        TranSMode = "INSL";

                    }
                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Department = data.FK_Department;
                    reportpassparam.FK_Product = data.FK_Product;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.BillNo = data.BillNo;
                    reportpassparam.ID_Master = data.ID_Master;

                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.categorys = data.categorys;
                    reportpassparam.FK_Category = data.FK_Category;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = 0;
                    reportpassparam.billtype = data.billtype;
                    reportpassparam.pmtype = data.pmtype;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;
                    reportpassparam.depname = data.depname;
                    reportpassparam.TranSMode = TranSMode;


                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }
                };
                reportpassparam.Reportsection = "SalesReport";
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion
        #region New
        public ActionResult GetProjectReports(ReportLoadModel.Reportparameters data)//,int Report_Id , string FK_Employee, int Rptype, string FromDate, string ToDate, int Status, string RpFieldptype)
        {
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            byte TableCount = 1; var dt = new DataTable(); var ds = new DataSet(); var TranSMode = "PRJC";
            #endregion ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                ReportLoadModel reportload = new ReportLoadModel();
                Reportname = (_userLoginInfo.UserName + data.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[TransMode] = TranSMode;
                //if (data.ReportMode == 14)
                //{
                //    var outputData = reportload.GetProjectReportSummary(input: new ReportLoadModel.ProjectReportprocedureparams
                //    {

                //        ReportMode = data.ReportMode,
                //        FromDate = data.FromDate == null ? null : data.FromDate,
                //        ToDate = data.ToDate == null ? null : data.ToDate,

                //        FK_Company = _userLoginInfo.FK_Company,
                //        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                //        EntrBy = _userLoginInfo.EntrBy,
                //        FK_Machine = _userLoginInfo.FK_Machine,
                //        LeadNo = data.LeadNo,
                //        Criteria = data.Criteria,
                //        FK_Project = data.FK_Project,

                //        FK_Area = data.FK_Area,
                //        Category = data.Category,
                //        TableCount = TableCount,
                //        Detailed = data.Detailed,

                //    }, companyKey: _userLoginInfo.CompanyKey);
                //    ds = outputData.Data;
                //    Session[Reportname] = ds;
                //}
                //else
                //{
                var outputData = reportload.ProjectReportGet(input: new ReportLoadModel.ProjectReportprocedureparams
                {

                    ReportMode = data.ReportMode,
                    FromDate = data.FromDate == null ? null : data.FromDate,
                    ToDate = data.ToDate == null ? null : data.ToDate,

                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Employee = data.FK_Employee,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    LeadNo = data.LeadNo,
                    Criteria = data.Criteria,
                    FK_Project = data.FK_Project,
                    //FK_Expense=data.FK_Expense,
                    //FK_Stage=data.FK_Stage,
                    FK_Area = data.FK_Area,
                    Category = data.Category,
                    TableCount = TableCount,
                    Detailed = data.Detailed,
                    FK_Branch = data.FK_Branch,
                    ReportSubMode = data.ReportSubMode,
                    FK_PettyCashier = data.FK_PettyCashier,
                }, companyKey: _userLoginInfo.CompanyKey);
                dt = outputData.Data;
                Session[Reportname] = dt;
                //}


                //if (data.ReportMode == 13)
                //{
                //    var dtnew = new DataTable();
                //    dtnew.Columns.Add("Income");
                //    dtnew.Columns.Add("Amount_R", typeof(decimal));
                //    dtnew.Columns.Add("Expense");
                //    dtnew.Columns.Add("Amount'_R", typeof(decimal));
                //    dtnew.Columns.Add("hdn_Type");
                //    dtnew.Columns.Add("hdn_Groupby");
                //    DataRow row; string mode = "";
                //    var dtIncome = dt.AsEnumerable().Where(dr => dr.Field<string>("Mode") == "Income");// dt.Select("Type =Income");
                //    var dtExpense = dt.AsEnumerable().Where(dr => dr.Field<string>("Mode") == "Expense");// dt.Select("Type=Expense");
                //    foreach (DataRow Rw in dtIncome)
                //    {
                //        mode = Rw["Mode"].ToString();
                //        row = dtnew.NewRow();
                //        row[mode] = Rw["Narration"].ToString();
                //        row["hdn_Type"] = Rw["Type"].ToString();
                //        if (mode == "Income")
                //        {
                //            row[1] = Rw["Amount_R"].ToString();
                //            row[3] = 0;
                //        }
                //        row["hdn_Groupby"] = "Type";
                //        dtnew.Rows.Add(row);
                //    }
                //    long i = 1;
                //    foreach (DataRow Rw in dtExpense)
                //    {
                //        mode = Rw["Mode"].ToString();
                //        if (i <= dtnew.Rows.Count)
                //        {
                //            foreach (DataRow Rows in dtnew.Rows)
                //            {
                //                if (mode == "Expense")
                //                {

                //                    Rows[mode] = Rw["Narration"].ToString();
                //                    Rows[3] = Rw["Amount_R"].ToString();
                //                }

                //            }

                //        }
                //        else
                //        {
                //            mode = Rw["Mode"].ToString();
                //            row = dtnew.NewRow();
                //            row[mode] = Rw["Narration"].ToString();
                //            row["hdn_Type"] = Rw["Type"].ToString();
                //            if (mode == "Expense")
                //            {
                //                row[1] = Rw["Amount_R"].ToString();
                //                row[3] = 0;
                //            }
                //            row["hdn_Groupby"] = "Type";
                //            dtnew.Rows.Add(row);
                //        }
                //        i++;
                //    }







                //    Session[Reportname] = dtnew;
                //}
                //else
                //{

                //}


                return Json(true, JsonRequestBehavior.AllowGet);




            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult GetServiceReport(ReportLoadModel.Reportparameters data)
        {
            byte TableCount = 1; var dt = new DataTable(); var TranSMode = "";
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            ReportLoadModel reportload = new ReportLoadModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                var outputData = reportload.ServiceReportGet(input: new ReportLoadModel.ServiceReportprocedureparams
                {

                    ReportMode = data.ReportMode,
                    FromDate = data.FromDate == null ? null : data.FromDate,
                    ToDate = data.ToDate == null ? null : data.ToDate,
                    FK_Product = data.FK_Product,
                    FK_Branch = data.FK_Branch,
                    FK_Employee = data.FK_Employee,
                    FK_Priority = data.FK_Priority,
                    //ComplaintProductType = data.ComplaintProductType,
                    ComplaintType = data.ComplaintType,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TicketNo = data.TicketNo,
                    Criteria = data.Criteria,
                    //Status = Convert.ToInt64(reportparam.Status),
                    Status = data.Status,
                    FK_Area = data.FK_Area,
                    FK_Category = data.FK_Category,
                    FK_NextAction = data.FK_NextAction,
                    DueDaysFrom = data.DueDaysFrom,
                    DueDaysTo = data.DueDaysTo,
                    DueCriteria = data.DueCriteria,
                    ComplaintService = data.ComplaintService,
                    //FK_Servicetype=data.FK_Servicetype,
                    ReplacementType = data.ReplacementType,
                    TableCount = TableCount,
                    BillingType = data.BillingType,

                }, companyKey: _userLoginInfo.CompanyKey);
                dt = outputData.Data;
                Reportname = (_userLoginInfo.UserName + data.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;
                return Json(true, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult GetSalesReport(saleReportpassparams data)
        {
            byte TableCount = 1; var TranSMode = ""; var dt = new DataTable();
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            ReportLoadModel reportload = new ReportLoadModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                TranSMode = "INSL";



                //if (data.ReportMode == 1)
                //{

                //    if (data.ReportMode == 4)
                //    {
                //        data.TableCount = 6;
                //    }
                //}
                if (data.ReportMode == 1 || data.ReportMode == 3)
                {
                    data.ReportMode = 2;
                    TranSMode = "INSL";
                }
                else if (data.ReportMode == 4)
                {
                    data.ReportMode = 3;
                    TranSMode = "INSLHO";
                }
                else if (data.ReportMode == 5)
                {
                    data.ReportMode = 4;
                    TranSMode = "INSL";

                }
                var outputData = reportload.GetSalesReport(input: new ReportLoadModel.SalesReports
                {

                    ReportMode = data.ReportMode,
                    FromDate = data.FromDate == null ? null : data.FromDate,
                    ToDate = data.ToDate == null ? null : data.ToDate,
                    BillNo = data.BillNo,
                    ID_Master = data.ID_Master,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,

                    Criteria = data.Criteria,
                    FK_Product = data.FK_Product,
                    FK_BillType = data.billtype,
                    FK_PaymentMethod = data.pmtype,
                    FK_Department = data.FK_Department,
                    FK_Branch = data.FK_Branch,
                    FK_Category = data.FK_Category,
                    TableCount = TableCount,
                    TranSMode = TranSMode
                }, companyKey: _userLoginInfo.CompanyKey);
                dt = outputData.Data;

                Reportname = (_userLoginInfo.UserName + data.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;
                return Json(true, JsonRequestBehavior.AllowGet);




                //datas reportpassparam = new Reportpassparams();


                //{
                //    if (data.ReportMode == 1)
                //    {
                //        //reportpassparam.ReportSchema = "Saleslist";
                //        //reportpassparam.Reportname = "SalesList.rpt";
                //        //reportpassparam.filename = "Sales Register Report";
                //    }
                //    if (data.ReportMode == 2)
                //    {
                //        reportpassparam.ReportSchema = "Saleslistdet";
                //        reportpassparam.Reportname = "SalesDetailedList.rpt";
                //        reportpassparam.filename = "Sales Detailed Report";
                //    }
                //    if (data.ReportMode == 3)
                //    {
                //        reportpassparam.ReportSchema = "Saleslistnondet";
                //        reportpassparam.Reportname = "SalesNondetailed.rpt";
                //        reportpassparam.filename = "Sales Non Detailed Report";
                //    }
                //    if (data.ReportMode == 4)
                //    {
                //        reportpassparam.ReportSchema = "HoldedLists";
                //        reportpassparam.Reportname = "HoldedList.rpt";
                //        reportpassparam.filename = "Holded List Report";
                //    }
                //    if (data.ReportMode == 5)
                //    {
                //        reportpassparam.ReportSchema = "SalesLedgers";
                //        reportpassparam.Reportname = "SalesLedger.rpt";
                //        reportpassparam.filename = "Sales Ledger Report";
                //    }
                //    if (data.ReportMode == 14)
                //    {
                //        reportpassparam.ReportSchema = "PaymentMethodType";
                //        reportpassparam.Reportname = "PaymentMethodType.rpt";
                //        reportpassparam.filename = "Payment Method Type Report";
                //    }
                //    string product;
                //    string crtname;
                //    string catgrynm;
                //    string departmnt;
                //    string billtyp;
                //    string paymentmethod;
                //    if (data.FK_Department != 0)
                //    {
                //        departmnt = "  Department : " + data.depname + ",";
                //    }
                //    else { departmnt = ""; }
                //    if (data.billtype != 0)
                //    {
                //        billtyp = "  Bill Type : " + data.billtypename + ",";
                //    }
                //    else { billtyp = ""; }

                //    if (data.pmtype != 0)
                //    {
                //        paymentmethod = "  Payment Method Type  : " + data.pmtypename + ",";
                //    }
                //    else { paymentmethod = ""; }
                //    if (data.FK_Product != 0)
                //    {
                //        product = "  Product : " + data.Prodname + ",";
                //    }
                //    else { product = ""; }
                //    if (data.FK_Category != 0)
                //    {
                //        catgrynm = "  Category : " + data.categorys + ",";
                //    }
                //    else { catgrynm = ""; }


                //    if (data.Criteria != 0)
                //    {
                //        crtname = "  Group By : " + data.Critername;
                //    }
                //    else {
                //        crtname = "";
                //    }


                //    if (data.FK_Product != 0 || data.Criteria != 0 || data.FK_Department != 0 || data.billtype != 0 || data.FK_Category != 0 || data.pmtype != 0)
                //    {
                //        reportpassparam.Filter = "Filter By:" + billtyp + paymentmethod + departmnt + catgrynm + product + crtname;
                //    }
                //    var TranSMode = "INSL";
                //    if (data.ReportMode == 1 || data.ReportMode == 3)
                //    {
                //        data.ReportMode = 2;
                //        TranSMode = "INSL";
                //    }
                //    else if (data.ReportMode == 4)
                //    {
                //        data.ReportMode = 3;
                //        TranSMode = "INSLHO";
                //    }
                //    else if (data.ReportMode == 5)
                //    {
                //        data.ReportMode = 4;
                //        TranSMode = "INSL";

                //    }
                //    reportpassparam.ReportMode = data.ReportMode;
                //    reportpassparam.FK_Department = data.FK_Department;
                //    reportpassparam.FK_Product = data.FK_Product;
                //    reportpassparam.FromDate = (data.FromDate);
                //    reportpassparam.ToDate = (data.ToDate);
                //    reportpassparam.BillNo = data.BillNo;
                //    reportpassparam.ID_Master = data.ID_Master;

                //    reportpassparam.FK_Branch = data.FK_Branch;
                //    reportpassparam.categorys = data.categorys;
                //    reportpassparam.FK_Category = data.FK_Category;
                //    reportpassparam.Rptype = data.Rptype;
                //    reportpassparam.TableCount = 0;
                //    reportpassparam.billtype = data.billtype;
                //    reportpassparam.pmtype = data.pmtype;
                //    reportpassparam.CompName = data.CompName;
                //    reportpassparam.Criteria = data.Criteria;
                //    reportpassparam.depname = data.depname;
                //    reportpassparam.TranSMode = TranSMode;


                //    if (data.Branchname == "Please Select")
                //    {
                //        reportpassparam.Branchname = "All Branch";
                //    }
                //    else
                //    {
                //        reportpassparam.Branchname = data.Branchname;
                //    }
                //};
                //reportpassparam.Reportsection = "SalesReport";
                //Session["Reportpassparams"] = reportpassparam;


                //Response.Buffer = false;
                //Response.ClearContent();
                //Response.ClearHeaders();

                //return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion
        public ActionResult GetSalesOrderReport(saleReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                Reportpassparams reportpassparam = new Reportpassparams();


                {
                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "SalesOrderlist";
                        reportpassparam.Reportname = "SalesOrderlist.rpt";
                        reportpassparam.filename = "Sales Order Wise Report";
                    }
                    if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "SalesODetlist";
                        reportpassparam.Reportname = "SalesOrderDetlist.rpt";
                        reportpassparam.filename = "Sales Order Detailed Report";
                    }
                    if (data.ReportMode == 3)
                    {
                        reportpassparam.ReportSchema = "SalesONonDetlist";
                        reportpassparam.Reportname = "SalesOrderNonDetlist.rpt";
                        reportpassparam.filename = "Sales Order Non Detailed Report";
                    }

                    string product;
                    string crtname;
                    string catgrynm;
                    string departmnt;

                    if (data.FK_Department != 0)
                    {
                        departmnt = "  Department : " + data.depname + ",";
                    }
                    else { departmnt = ""; }

                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }
                    if (data.FK_Category != 0)
                    {
                        catgrynm = "  Category : " + data.categorys + ",";
                    }
                    else { catgrynm = ""; }


                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }


                    if (data.FK_Product != 0 || data.Criteria != 0 || data.FK_Department != 0 || data.FK_Category != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + departmnt + catgrynm + product + crtname;
                    }
                    var TranSMode = "INSLHO";
                    if (data.ReportMode == 1 || data.ReportMode == 2 || data.ReportMode == 3)
                    {
                        data.ReportMode = 8;

                    }

                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Department = data.FK_Department;
                    reportpassparam.FK_Product = data.FK_Product;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);

                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.categorys = data.categorys;
                    reportpassparam.FK_Category = data.FK_Category;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = 0;
                    reportpassparam.billtype = data.billtype;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;
                    reportpassparam.depname = data.depname;
                    reportpassparam.TranSMode = TranSMode;


                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }
                };
                reportpassparam.Reportsection = "SalesOrderReport";
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult GetStockReport(stockReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                Reportpassparams reportpassparam = new Reportpassparams();

                {

                    if (data.ReportMode == 5)
                    {
                        reportpassparam.ReportSchema = "CurrentStocks";
                        reportpassparam.Reportname = "CurrentStock.rpt";
                        reportpassparam.filename = "Current Stock Report";
                    }
                    if (data.ReportMode == 6)
                    {
                        reportpassparam.ReportSchema = "CurrentEmployeeStock";
                        reportpassparam.Reportname = "CurrentEmployeeStock.rpt";
                        reportpassparam.filename = "Current Employee Stock Report";
                    }
                    if (data.ReportMode == 7)
                    {
                        reportpassparam.ReportSchema = "StockRegister";
                        reportpassparam.Reportname = "StockRegister.rpt";
                        reportpassparam.filename = "Stock Register Report";
                    }
                    if (data.ReportMode == 12)
                    {
                        reportpassparam.ReportSchema = "MovementListNonDetailed";
                        reportpassparam.Reportname = "ProductMovementListNonDetailed.rpt";
                        reportpassparam.filename = "MovementList Non Detailed Report";
                    }
                    if (data.ReportMode == 11)
                    {
                        reportpassparam.ReportSchema = "MovementListDetailed";
                        reportpassparam.Reportname = "ProductMovementListDetailed.rpt";
                        reportpassparam.filename = "MovementList Detailed Report";
                    }
                    if (data.ReportMode == 13)
                    {
                        reportpassparam.ReportSchema = "StockTransferList";
                        reportpassparam.Reportname = "StockTransferList.rpt";
                        reportpassparam.filename = "Stock Transfer List";
                    }
                    if (data.ReportMode == 15)
                    {
                        reportpassparam.ReportSchema = "StockConversionList";
                        reportpassparam.Reportname = "StockConversionList.rpt";
                        reportpassparam.filename = "Stock Conversion List";
                    }
                    string mode;
                    string product;
                    string employee;
                    string employeeto;

                    string branch = "";
                    string branchto = "";
                    string crtname;
                    string category = "";
                    string department = "";
                    string departmentto = "";
                    string oldproduct = "";
                    string newproduct = "";

                    if (data.Modes != "")
                    {
                        mode = "  Mode : " + data.Modes + ",";
                    }
                    else { mode = ""; }
                    if (data.FK_Employee != 0)
                    {
                        employee = "  Employee : " + data.Employeename + ",";
                    }
                    else { employee = ""; }
                    if (data.FK_EmployeeTo != 0)
                    {
                        employeeto = "  Employee To : " + data.EmployeenameTo + ",";
                    }
                    else { employeeto = ""; }

                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }

                    if (data.FK_Category != 0)
                    {
                        category = "  Category : " + data.Categoryname;
                    }
                    else { category = ""; }
                    if (data.FK_Branch != 0)
                    {
                        branch = "  Branch : " + data.Branchname + ",";
                    }
                    else { branch = ""; }
                    if (data.FK_BranchTo != 0)
                    {
                        branchto = "  Branch To: " + data.BranchnameTo + ",";
                    }
                    else { branchto = ""; }

                    if (data.FK_Department != 0)
                    {
                        department = "  Department : " + data.Departmentname + ",";
                    }
                    else { department = ""; }
                    if (data.FK_DepartmentTo != 0)
                    {
                        departmentto = "  Department To : " + data.DepartmentnameTo + ",";
                    }
                    else { departmentto = ""; }
                    if (data.FK_ProductNew != 0)
                    {
                        newproduct = "  New Product : " + data.ProductNew + ",";
                    }
                    else { newproduct = ""; }
                    if (data.FK_ProductOld != 0)
                    {
                        oldproduct = " Old Product : " + data.ProductOld + ",";
                    }
                    else { oldproduct = ""; }

                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }
                    if (data.FK_Product != 0 || data.FK_Department != 0 || data.FK_Branch != 0 || data.FK_Category != 0 || data.Criteria != 0 || data.FK_BranchTo != 0 || data.FK_DepartmentTo != 0 || data.FK_EmployeeTo != 0 || data.FK_ProductOld != 0 || data.FK_ProductNew != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + branch + department + employee + branchto + departmentto + employeeto + category + product + oldproduct + newproduct + crtname;
                    }
                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Product = data.FK_Product;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.FK_Employee = data.FK_Employee;
                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.FK_EmployeeTo = data.FK_EmployeeTo;
                    reportpassparam.FK_BranchTo = data.FK_BranchTo;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = data.TableCount;

                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;

                    reportpassparam.Reportsection = "CurrentStock";
                    reportpassparam.FK_Category = data.FK_Category;
                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.FK_Department = data.FK_Department;
                    reportpassparam.FK_DepartmentTo = data.FK_DepartmentTo;
                    reportpassparam.FK_ProductNew = data.FK_ProductNew;
                    reportpassparam.FK_ProductOld = data.FK_ProductOld;

                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }

                };
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public ActionResult GetStockReport2(stockReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            ReportLoadModel reportload = new ReportLoadModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                     

                var outputData = reportload.GetStockReport(input: new ReportLoadModel.StockReports
                {

                    ReportMode = data.ReportMode,
                    FromDate = data.FromDate == null ? null : data.FromDate,
                    ToDate = data.ToDate == null ? null : data.ToDate,

                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,

                    Criteria = data.Criteria,
                    FK_Product = data.FK_Product,
                    FK_Employee = data.FK_Employee,
                    FK_Department = data.FK_Department,
                    FK_Branch = data.FK_Branch,
                    FK_EmployeeTo = data.FK_EmployeeTo,
                    FK_DepartmentTo = data.FK_DepartmentTo,
                    FK_BranchTo = data.FK_BranchTo,
                    FK_Category = data.FK_Category,
                    FK_ProductOld = data.FK_ProductOld,
                    FK_ProductNew = data.FK_ProductNew,
                    TableCount = data.TableCount,
                    ActionStatus = Common.xmlTostring(data.ActionlistArray.Select(a => new StockReportList.Actionlist { FK_Mode = a }).ToList()),
                }, companyKey: _userLoginInfo.CompanyKey);
                var dt = outputData.Data;

                var TranSMode = "";

                Reportname = (_userLoginInfo.UserName + data.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        #region [GetPurchaseReport]
        public ActionResult GetPurchaseReport(PurchaseReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                Reportpassparams reportpassparam = new Reportpassparams();


                {
                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "PurchaseNonDetailed";
                        reportpassparam.Reportname = "PurchaseNonDetailed.rpt";
                        reportpassparam.filename = "Purchase Non Detailed Report";
                    }
                    else if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "PurchaseDetailed";
                        reportpassparam.Reportname = "PurchaseDetailedReport.rpt";
                        reportpassparam.filename = "Purchase Detailed Report";

                    }


                    string product;
                    string crtname;
                    string catgrynm;
                    string departmnt;
                    string billtyp;
                    string supplier;
                    if (data.depname != "")
                    {
                        departmnt = "  Department : " + data.depname + ",";
                    }
                    else { departmnt = ""; }
                    if (data.billtype != 0)
                    {
                        billtyp = "  Bill Type : " + data.billtypename + ",";
                    }
                    else { billtyp = ""; }
                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }
                    if (data.categorys != "" && data.ReportMode == 2)
                    {
                        catgrynm = "  Category : " + data.categorys + ",";
                    }
                    else { catgrynm = ""; }

                    if (data.FK_Supplier != 0)
                    {
                        supplier = " Supplier : " + data.SuppName + ",";
                    }
                    else { supplier = ""; }
                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }


                    if (data.FK_Product != 0 || data.Criteria != 0 || data.depname != "" || data.billtype != 0 || (data.categorys != "" && data.ReportMode == 2) || data.FK_Supplier != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + departmnt + product + supplier + catgrynm + crtname;
                    }
                    var TranSMode = "INPU";
                    if (data.ReportMode == 1 || data.ReportMode == 2)
                    {
                        data.ReportMode = 1;
                        TranSMode = "INPU";
                    }
                    else if (data.ReportMode == 4)
                    {
                        data.ReportMode = 3;
                        TranSMode = "INSLHO";
                    }
                    else if (data.ReportMode == 5)
                    {
                        data.ReportMode = 4;
                        TranSMode = "INSL";

                    }
                    if (data.ReportMode == 1)
                    {
                        data.ReportMode = 1;
                    }
                    else if (data.ReportMode == 2)
                    {
                        data.ReportMode = 1;
                    }
                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Department = data.FK_Department;
                    reportpassparam.FK_Product = data.FK_Product;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.BillNo = data.BillNo;
                    reportpassparam.FK_Supplier = data.FK_Supplier;

                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.categorys = data.categorys;
                    reportpassparam.FK_Category = data.FK_Category;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = 0;
                    reportpassparam.billtype = data.billtype;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;
                    reportpassparam.depname = data.depname;
                    reportpassparam.TranSMode = data.TransMode;
                    reportpassparam.SuppName = data.SuppName;


                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }
                };
                reportpassparam.Reportsection = "PurchaseReport";
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        public ActionResult GetPurchaseReportTablr(Reportpassparams input)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ReportLoadModel objfld = new ReportLoadModel();
            string TranSMode = input.TranSMode;
            #region Rpt1
            
            var data = objfld.PurchaseReportTablr(input: new ReportLoadModel.PurchaseReportProcedureparms
                {
                    ReportMode = input.ReportMode,
                    FromDate = input.FromDate == null ? null : input.FromDate,
                    ToDate = input.ToDate == null ? null : input.ToDate,
                    BillNo = input.BillNo,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_Supplier = input.FK_Supplier,

                    Criteria = input.Criteria,
                    FK_Product = input.FK_Product,
                    FK_BillType = input.billtype,
                    FK_Department = input.FK_Department,
                    FK_Branch = input.FK_Branch,
                    FK_Category = input.FK_Category,
                    TableCount = 2,
                    TranSMode = TranSMode

                }, companyKey: _userLoginInfo.CompanyKey);

                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
               
            return Json(true, JsonRequestBehavior.AllowGet);
        }

            public ActionResult GetPurchaseOrderReport(PurchaseReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                Reportpassparams reportpassparam = new Reportpassparams();


                {
                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "PurchaseOrderNonDetailed";
                        reportpassparam.Reportname = "PurchaseOrderNonDetailed.rpt";
                        reportpassparam.filename = "Purchase Order Non Detailed Report";
                    }
                    else if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "PurchaseOrderDetailed";
                        reportpassparam.Reportname = "PurchaseOrderDetailedReport.rpt";
                        reportpassparam.filename = "Purchase Order Detailed Report";

                    }


                    string product;
                    string crtname;
                    string catgrynm;
                    string departmnt;
                    string billtyp;
                    string supplier;
                    if (data.depname != "")
                    {
                        departmnt = "  Department : " + data.depname + ",";
                    }
                    else { departmnt = ""; }
                    if (data.billtype != 0)
                    {
                        billtyp = "  Bill Type : " + data.billtypename + ",";
                    }
                    else { billtyp = ""; }
                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }
                    if (data.categorys != "" && data.ReportMode == 2)
                    {
                        catgrynm = "  Category : " + data.categorys + ",";
                    }
                    else { catgrynm = ""; }


                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }

                    if (data.FK_Supplier != 0)
                    {
                        supplier = " Supplier : " + data.SuppName + ",";
                    }
                    else { supplier = ""; }

                    if (data.FK_Product != 0 || data.Criteria != 0 || data.depname != "" || data.billtype != 0 || data.categorys != "" || data.FK_Supplier != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + departmnt + billtyp + product + supplier + catgrynm + crtname;
                    }
                    var TranSMode = "INPU";
                    if (data.ReportMode == 1 || data.ReportMode == 2)
                    {
                        data.ReportMode = 1;
                        TranSMode = "INPU";
                    }
                    else if (data.ReportMode == 4)
                    {
                        data.ReportMode = 3;
                        TranSMode = "INSLHO";
                    }
                    else if (data.ReportMode == 5)
                    {
                        data.ReportMode = 4;
                        TranSMode = "INSL";

                    }
                    if (data.ReportMode == 1 || data.ReportMode == 2)
                    {
                        data.ReportMode = 9;
                    }

                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Department = data.FK_Department;
                    reportpassparam.FK_Product = data.FK_Product;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.BillNo = data.BillNo;
                    reportpassparam.FK_Supplier = data.FK_Supplier;

                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.categorys = data.categorys;
                    reportpassparam.FK_Category = data.FK_Category;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = 0;
                    reportpassparam.billtype = data.billtype;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;
                    reportpassparam.depname = data.depname;
                    reportpassparam.TranSMode = TranSMode;


                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }
                };
                reportpassparam.Reportsection = "PurchaseOrderReport";
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public ActionResult GetPurchaseReturnReport(PurchaseReturnReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                Reportpassparams reportpassparam = new Reportpassparams();


                {
                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "PurchaseReturnNonDetailed";
                        reportpassparam.Reportname = "PurchaseReturnNonDetailed.rpt";
                        reportpassparam.filename = "Purchase Return Non Detailed Report";
                    }
                    else if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "PurchaseReturnDetailed";
                        reportpassparam.Reportname = "PurchaseReturnDetailed.rpt";
                        reportpassparam.filename = "Purchase Return Detailed Report";

                    }


                    string product;
                    string crtname;
                    string catgrynm;
                    string departmnt;
                    string billtyp;
                    string supplier;
                    if (data.depname != "")
                    {
                        departmnt = "  Department : " + data.depname + ",";
                    }
                    else { departmnt = ""; }
                    if (data.billtype != 0)
                    {
                        billtyp = "  Bill Type : " + data.billtypename + ",";
                    }
                    else { billtyp = ""; }
                    if (data.FK_Product != 0)
                    {
                        product = "  Product : " + data.Prodname + ",";
                    }
                    else { product = ""; }
                    if (data.categorys != "" && data.ReportMode == 2)
                    {
                        catgrynm = "  Category : " + data.categorys + ",";
                    }
                    else { catgrynm = ""; }


                    if (data.Criteria != 0)
                    {
                        crtname = "  Group By : " + data.Critername;
                    }
                    else { crtname = ""; }

                    if (data.FK_Supplier != 0)
                    {
                        supplier = " Supplier : " + data.SuppName + ",";
                    }
                    else { supplier = ""; }

                    if (data.FK_Product != 0 || data.Criteria != 0 || data.depname != "" || data.billtype != 0 || data.categorys != "" || data.FK_Supplier != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + departmnt + billtyp + product + supplier + catgrynm + crtname;
                    }
                    var TranSMode = "INPU";
                    if (data.ReportMode == 1 || data.ReportMode == 2)
                    {
                        data.ReportMode = 10;
                        TranSMode = "INPR";
                    }
                    else if (data.ReportMode == 4)
                    {
                        data.ReportMode = 3;
                        TranSMode = "INSLHO";
                    }
                    else if (data.ReportMode == 5)
                    {
                        data.ReportMode = 4;
                        TranSMode = "INSL";

                    }


                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Department = data.FK_Department;
                    reportpassparam.FK_Product = data.FK_Product;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.BillNo = data.BillNo;
                    reportpassparam.FK_Supplier = data.FK_Supplier;

                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.categorys = data.categorys;
                    reportpassparam.FK_Category = data.FK_Category;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = 0;
                    reportpassparam.billtype = data.billtype;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;
                    reportpassparam.depname = data.depname;
                    reportpassparam.TranSMode = data.TransMode;


                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }
                };
                reportpassparam.Reportsection = "PurchaseReturnReport";
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public ActionResult GetBillNo(Invoiceparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                Reportpassparams reportpassparam = new Reportpassparams();
                {
                    reportpassparam.FK_SALES = data.FK_SALES;
                    reportpassparam.ReportSchema = "Invoices";
                    reportpassparam.Reportname = "Invoice.rpt";
                    reportpassparam.filename = "Invoice";
                    reportpassparam.Rptype = 1;
                    reportpassparam.TableCount = 0;

                };
                reportpassparam.Reportsection = "Invoice";
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public ActionResult GetGSTReport(GSTReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                Reportpassparams reportpassparam = new Reportpassparams();

                {

                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "PurchaseGST";
                        reportpassparam.Reportname = "PurchaseGST.rpt";
                        reportpassparam.filename = "Purchase Tax Report";
                    }
                    if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "SalesGST";
                        reportpassparam.Reportname = "SaleTax.rpt";
                        reportpassparam.filename = "Sales Tax Report";
                    }
                    if (data.ReportMode == 3)
                    {
                        reportpassparam.ReportSchema = "PurchaseReturnGST";
                        reportpassparam.Reportname = "PurchaseReturnGST.rpt";
                        reportpassparam.filename = "Purchase Return GST";
                    }




                    string branch = "";
                    string department = "";
                    string taxtype = "";
                    string CusName = "";
                    string SuppName = "";
                    string Mode = "";

                    if (data.FK_Branch != 0)
                    {
                        branch = "  Branch : " + data.Branchname + ",";
                    }
                    else { branch = ""; }
                    if (data.FK_Department != 0)
                    {
                        department = "  Department : " + data.Departmentname + ",";
                    }
                    else { department = ""; }
                    if (data.TaxTypeID != 0)
                    {
                        taxtype = "  Tax Type : " + data.TaxTypename + ",";
                    }
                    else { taxtype = ""; }
                    if (data.CustomerID != 0)
                    {
                        CusName = "  Customer : " + data.CusName + ",";
                    }
                    else { CusName = ""; }
                    if (data.SupplierID != 0)
                    {
                        SuppName = "  Supplier : " + data.SuppName + ",";
                    }
                    else { SuppName = ""; }
                    if (data.ModeID != null)
                    {
                        Mode = "  Mode : " + data.ModeName + ",";
                    }
                    else { Mode = ""; }

                    if (data.FK_Department != 0 || data.FK_Branch != 0 || data.TaxTypeID != 0 || data.CustomerID != 0 || data.SupplierID != 0 || data.ModeID != "")
                    {
                        reportpassparam.Filter = "Filter By:" + branch + department + taxtype + CusName + SuppName + Mode;
                    }
                    reportpassparam.ReportMode = data.ReportMode;

                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);


                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = data.TableCount;
                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Reportsection = "GSTReport";

                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.FK_Department = data.FK_Department;
                    reportpassparam.FK_Supplier = data.SupplierID;
                    reportpassparam.FK_Customer = data.CustomerID;
                    reportpassparam.Mode = data.ModeID;
                    reportpassparam.FK_TaxType = data.TaxTypeID;

                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }

                };
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public ActionResult GetAccountsReport(AccountsReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {
                Reportpassparams reportpassparam = new Reportpassparams();
                {
                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "AccountsLedger";
                        reportpassparam.Reportname = "AccountLedger.rpt";
                        reportpassparam.filename = "Accounts Ledger";
                    }
                    if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "AccountStatement";
                        reportpassparam.Reportname = "AccountStatement.rpt";
                        reportpassparam.filename = "Account Statement";
                    }
                    string accountHead;
                    string accountSubHead;
                    if (data.AccountHead != 0)
                    {
                        accountHead = "Account Head : " + data.AHeadName + ",";
                    }
                    else { accountHead = ""; }
                    if (data.AccountHeadSub != 0)
                    {
                        accountSubHead = "  Account Sub Head : " + data.ASHeadName + ",";
                    }
                    else { accountSubHead = ""; }
                    //filter
                    if (data.AccountHead != 0 || data.AccountHeadSub != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + accountHead + accountSubHead;
                    }
                    var TranSMode = "";
                    if (data.ReportMode == 1 || data.ReportMode == 3)
                    {
                        data.ReportMode = 2;
                        TranSMode = "INSL";
                    }
                    else if (data.ReportMode == 4)
                    {
                        data.ReportMode = 3;
                        TranSMode = "INSLHO";
                    }
                    else if (data.ReportMode == 5)
                    {
                        data.ReportMode = 4;
                        TranSMode = "INSL";

                    }
                    reportpassparam.ReportMode = data.ReportMode;
                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.FK_AccountHead = data.AccountHead;
                    reportpassparam.FK_AccountHeadSub = data.AccountHeadSub;
                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.Reportsection = "AccountsReport";
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = 0;


                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }
                };

                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public ActionResult GetAMCWarrantyMappinginvoice(AMCWarrantyparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                Reportpassparams reportpassparam = new Reportpassparams();
                {
                    reportpassparam.FK_Master = data.FK_Master;
                    reportpassparam.ReportSchema = "AMCWarrantyInvoice";
                    reportpassparam.Reportname = "AMCWarrantyInvoice.rpt";
                    reportpassparam.filename = "AMCWarrantyInvoice";
                    reportpassparam.Rptype = 1;
                    reportpassparam.Modes = data.Modes;
                    reportpassparam.TableCount = 0;
                };
                reportpassparam.Reportsection = "AMCWarrantyInvoice";
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #region [GetServiceBillNo]
        public ActionResult GetServiceBillNo(ServiceInvoice data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                Reportpassparams reportpassparam = new Reportpassparams();
                {
                    reportpassparam.FK_ServiceBill = data.FK_ServiceBill;
                    reportpassparam.ReportSchema = "ServiceInvoice";
                    reportpassparam.Reportname = "ServiceInvoice.rpt";
                    reportpassparam.filename = "ServiceInvoice";
                    reportpassparam.Rptype = 1;
                    reportpassparam.TableCount = 0;

                };
                reportpassparam.Reportsection = "ServiceInvoice";
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion

        public ActionResult GetPayRollReport(PayRollReportpassparams data)
        {

            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                Reportpassparams reportpassparam = new Reportpassparams();

                {

                    if (data.ReportMode == 1)
                    {
                        reportpassparam.ReportSchema = "Individualpayslip";
                        reportpassparam.Reportname = "Individualpayslip.rpt";
                        reportpassparam.filename = "Individual Pay Slip Report";

                    }
                    if (data.ReportMode == 2)
                    {
                        reportpassparam.ReportSchema = "Salarycertificate";
                        reportpassparam.Reportname = "Salarycertificate.rpt";
                        reportpassparam.filename = "Salary Certificate";

                    }

                    string employee;


                    string branch = "";

                    string department = "";



                    if (data.FK_Employee != 0)
                    {
                        employee = "  Employee : " + data.Employeename + ",";
                    }
                    else { employee = ""; }



                    if (data.FK_Branch != 0)
                    {
                        branch = "  Branch : " + data.Branchname + ",";
                    }
                    else { branch = ""; }

                    if (data.FK_Department != 0)
                    {
                        department = "  Department : " + data.Departmentname + ",";
                    }
                    else { department = ""; }

                    if (data.FK_Department != 0 || data.FK_Branch != 0 || data.Criteria != 0)
                    {
                        reportpassparam.Filter = "Filter By:" + branch + department + employee;
                    }
                    reportpassparam.ReportMode = data.ReportMode;

                    reportpassparam.FromDate = (data.FromDate);
                    reportpassparam.ToDate = (data.ToDate);
                    reportpassparam.FK_Employee = data.FK_Employee;
                    reportpassparam.FK_Branch = data.FK_Branch;

                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Rptype = data.Rptype;
                    reportpassparam.TableCount = data.TableCount;

                    reportpassparam.CompName = data.CompName;
                    reportpassparam.Criteria = data.Criteria;

                    reportpassparam.Reportsection = "PayRoll";

                    reportpassparam.FK_Branch = data.FK_Branch;
                    reportpassparam.FK_Department = data.FK_Department;


                    if (data.Branchname == "Please Select")
                    {
                        reportpassparam.Branchname = "All Branch";
                    }
                    else
                    {
                        reportpassparam.Branchname = data.Branchname;
                    }

                };
                Session["Reportpassparams"] = reportpassparam;


                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                return Redirect("~/Reports/LoadRpt.aspx");

            }
            catch (Exception ex)
            {
                throw;
            }

        }




        public ActionResult GetCustomerReport(CustomerReportsParams data)
        {
            byte TableCount = 1;
            var TranSMode = data.TransMode;
            var dt = new DataTable();
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            ReportLoadModel reportload = new ReportLoadModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            if (data.ReportMode != 8)
            {
                try
                {

                    var outputData = reportload.GetCustomerReport(input: new ReportLoadModel.CustomerReports
                    {

                        ReportMode = data.ReportMode,
                        FromDate = data.ReportMode == 5 ? data.AsonDate : data.FromDate,
                        ToDate = data.ToDate,
                        FK_Branch = data.FK_Branch,
                        FK_CustomerType = data.FK_CustomerType,
                        FK_CustomerSector = data.FK_CustomerSector,
                        FK_Customer = data.FK_Customer,
                        FK_Category = data.FK_Category,
                        FK_Product = data.FK_Product,
                        FK_State = data.FK_State,
                        FK_District = data.FK_District,
                        FK_Area = data.FK_Area,
                        FK_Company = _userLoginInfo.FK_Company,
                        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                        EntrBy = _userLoginInfo.EntrBy,
                        FK_Machine = _userLoginInfo.FK_Machine,
                        Criteria = data.Criteria,
                        TransMode = data.TransMode,
                        //AsonDate = data.AsonDate,
                    }, companyKey: _userLoginInfo.CompanyKey);
                    dt = outputData.Data;

                    Reportname = (_userLoginInfo.UserName + data.ReportMode + data.TransMode).Replace(" ", "");
                    Session[TransMode] = TranSMode;
                    Session[Reportname] = dt;


                    return Json(outputData.Process, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            else{//For customer profile

                try
                {

                  

                    var data1 = reportload.GetCustomerReport(input:  new ReportLoadModel.CustomerReports
                    {

                        ReportMode = data.ReportMode,
                        FromDate = data.ReportMode == 5 ? data.AsonDate : data.FromDate,
                        ToDate = data.ToDate,
                        FK_Branch = data.FK_Branch,
                        FK_CustomerType = data.FK_CustomerType,
                        FK_CustomerSector = data.FK_CustomerSector,
                        FK_Customer = data.FK_Customer,
                        FK_Category = data.FK_Category,
                        FK_Product = data.FK_Product,
                        FK_State = data.FK_State,
                        FK_District = data.FK_District,
                        FK_Area = data.FK_Area,
                        FK_Company = _userLoginInfo.FK_Company,
                        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                        EntrBy = _userLoginInfo.EntrBy,
                        FK_Machine = _userLoginInfo.FK_Machine,
                        Criteria = data.Criteria,
                        TransMode = data.TransMode,
                        //AsonDate = data.AsonDate,
                        TableCount = 1,
                    }, companyKey: _userLoginInfo.CompanyKey);
                    string Data = JsonConvert.SerializeObject(data1, Formatting.Indented);

                    var data2 = reportload.GetCustomerReport(input: new ReportLoadModel.CustomerReports
                    {

                        ReportMode = data.ReportMode,
                        FromDate = data.ReportMode == 5 ? data.AsonDate : data.FromDate,
                        ToDate = data.ToDate,
                        FK_Branch = data.FK_Branch,
                        FK_CustomerType = data.FK_CustomerType,
                        FK_CustomerSector = data.FK_CustomerSector,
                        FK_Customer = data.FK_Customer,
                        FK_Category = data.FK_Category,
                        FK_Product = data.FK_Product,
                        FK_State = data.FK_State,
                        FK_District = data.FK_District,
                        FK_Area = data.FK_Area,
                        FK_Company = _userLoginInfo.FK_Company,
                        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                        EntrBy = _userLoginInfo.EntrBy,
                        FK_Machine = _userLoginInfo.FK_Machine,
                        Criteria = data.Criteria,
                        TransMode = data.TransMode,
                        //AsonDate = data.AsonDate,
                        TableCount = 2,
                    }, companyKey: _userLoginInfo.CompanyKey);

                    dt = data2.Data;
                    Reportname = (_userLoginInfo.UserName + data.ReportMode + data.TransMode).Replace(" ", "");
                    Session[TransMode] = TranSMode;
                    Session[Reportname] = dt;
                    string detailsData = JsonConvert.SerializeObject(data2, Formatting.Indented);

                    //var Data = data2.Data;
                    var Process = data2.Process;

                   // var detailsData = data1.Data;
                    var detailsProcess = data1.Data;




                    return Json(new {  Data, detailsData }, JsonRequestBehavior.AllowGet);


                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }


        #region[GetSLAReport]
        public ActionResult GetSLAReport(GetSlaReports sla)
        {

            byte TableCount = 1;
            var TranSMode = sla.TransMode;
            var dt = new DataTable();
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            ReportLoadModel reportload = new ReportLoadModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            try
            {
                var outputData = reportload.GetSlaReport(input: new SlaReportsParams
                {
                    //ReportMode = sla.ReportMode,
                    FK_ComplaintList = sla.FK_ComplaintList,
                    AsOnDate = sla.AsOnDate,
                    FK_CustomerType = sla.FK_CustomerType,
                    FK_Product = sla.FK_Product,
                    FK_Category = sla.FK_Category,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = 0,
                    FK_Criteria = sla.FK_Criteria,


                }, companyKey: _userLoginInfo.CompanyKey);
                dt = outputData.Data;

                Reportname = (_userLoginInfo.UserName + sla.ReportMode + sla.TransMode).Replace(" ", "");
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;

                return Json(outputData.Process, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        public ActionResult GetShortageReport(ShortageReportsParams data)
        {
            byte TableCount = 1;
            var TranSMode = data.TransMode;
            var dt = new DataTable();
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            ReportLoadModel reportload = new ReportLoadModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                var outputData = reportload.GetShortageReport(input: new ReportLoadModel.ShortageReports
                {

                    ReportMode = data.ReportMode,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    FK_Branch = data.FK_Branch,
                    FK_Purchase = data.FK_Purchase,
                    FK_Supplier = data.FK_Supplier,
                    FK_Product = data.FK_Product,

                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    Criteria = data.Criteria,
                    TransMode = data.TransMode,
                }, companyKey: _userLoginInfo.CompanyKey);
                dt = outputData.Data;

                Reportname = (_userLoginInfo.UserName + data.ReportMode + data.TransMode).Replace(" ", "");
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;


                return Json(outputData.Process, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ActionResult GetMediaPromotionReport(MediaPromotionReportsParams data)
        {
            byte TableCount = 1;
            var TranSMode = data.TransMode;
            var dt = new DataTable();
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            ReportLoadModel reportload = new ReportLoadModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                var outputData = reportload.GetMediaPromotionReport(input: new ReportLoadModel.MediaPromoReports
                {

                    ReportMode = data.ReportMode,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    FK_Branch = data.FK_Branch,
                    FK_Media = data.FK_Media,
                    FK_SubMedia = data.FK_SubMedia,

                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    Criteria = data.Criteria,
                    TransMode = data.TransMode,
                }, companyKey: _userLoginInfo.CompanyKey);
                dt = outputData.Data;

                Reportname = (_userLoginInfo.UserName + data.ReportMode + data.TransMode).Replace(" ", "");
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;


                return Json(outputData.Process, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult ShowReport(String rptMode)
        {
          UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            string Reportname = _userLoginInfo.UserName;
            string RptTransMode = Session[TransMode].ToString();
            if (RptTransMode == "INSL")
            {
                if (rptMode == "1" || rptMode == "3")
                {
                    rptMode = "2";

                }
                else if (rptMode == "4")
                {
                    rptMode = "3";

                }
                else if (rptMode == "5")
                {
                    rptMode = "4";


                }

            }
            else if (RptTransMode == "INSLHO")
            {
                if (rptMode == "4")
                {
                    rptMode = "3";

                }
            }
            Reportname = (Reportname + rptMode + RptTransMode).Replace(" ", "");
            //if (RptTransMode == "PRJC" && rptMode=="14")
            //{
            //    DataSet ds = ((DataSet)Session[Reportname]) ?? new DataSet();

            //    if (!ds.Tables[0].Columns.Contains("hdn_cmmnRpt_Type"))
            //    {
            //        ds.Tables[0].Columns.Add("hdn_cmmnRpt_Type");
            //    }

            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        ds.Tables[0].Rows[0]["hdn_cmmnRpt_Type"] = rptMode + RptTransMode;
            //    }

            //    ViewBag.CompanyName = _userLoginInfo.Company;

            //    Session.Remove(TransMode);
            //    Session.Remove(Reportname);
            //    return PartialView("_AddReportdataSet", ds);
            //}
            //else
            //{
            
            var dt = ((DataTable)Session[Reportname]) ?? new DataTable();

            if (!dt.Columns.Contains("hdn_cmmnRpt_Type"))
                {
                dt.Columns.Add("hdn_cmmnRpt_Type");
            }
            if (!dt.Columns.Contains("hdn_ComCategory"))
            {
                dt.Columns.Add("hdn_ComCategory");
                if (dt.Rows.Count > 0)
                {

                    dt.Rows[0]["hdn_ComCategory"] = _userLoginInfo.CompCategory;

                }
            }

            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["hdn_cmmnRpt_Type"] = rptMode + RptTransMode;


            }

            ViewBag.CompanyName = _userLoginInfo.Company;

            Session.Remove(TransMode);
            Session.Remove(Reportname);
            return PartialView("_AddReports", dt);
            //}

        }

        public ActionResult GetSalesReturnGSTReport(SalesReturnGstReportpassparams data)
        {
            byte TableCount = 1;
            var TranSMode = data.TranSMode;
            var dt = new DataTable();
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::

            ReportLoadModel reportload = new ReportLoadModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            try
            {

                var outputData = reportload.GetSalesReturnGSTReport(input: new ReportLoadModel.SalesReturnGSTReports
                {

                    ReportMode = data.ReportMode,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    FK_Branch = data.FK_Branch,
                    FK_Department = data.FK_Department,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    Criteria = data.Criteria,
                    TranSMode = data.TranSMode,
                }, companyKey: _userLoginInfo.CompanyKey);
                dt = outputData.Data;

                Reportname = (_userLoginInfo.UserName + data.ReportMode + data.TranSMode).Replace(" ", "");
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;


                return Json(outputData.Process, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region[GetAuditReport]
        public ActionResult GetAuditReport(AuditReportParams data)
        {
            //byte TableCount = 1;
            //var TranSMode = data.TransMode;
            //var dt = new DataTable();
            #region ::  Check User Session to verifyLogin  ::

            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            #endregion ::  Check User Session to verifyLogin  ::
            var TranSMode = data.TransMode;
            var dt = new DataTable();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ReportLoadModel reportload = new ReportLoadModel();
            try
            {

                var outputData = reportload.GetAuditTrailReport(input: new AuditReportParams
                {

                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    FK_MenuGroup = data.FK_MenuGroup,
                    FK_MenuList = data.FK_MenuList,
                    FK_UserRole = data.FK_UserRole,
                    FK_Users = data.FK_Users,
                    ReferenceNo =data.ReferenceNo,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_Branch = data.FK_Branch,
                    FK_Department = data.FK_Company,
                    Action= data.Action,
                    TransMode = data.TransMode,
                }, companyKey: _userLoginInfo.CompanyKey);
                dt = outputData.Data;

                Reportname = (_userLoginInfo.UserName + data.ReportMode + data.TransMode).Replace(" ", "");
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;


                return Json(outputData.Process, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
            #endregion
        }


        #region[GetOtherChargeReportList]
        public ActionResult GetOtherChargeReportList(OtherChargeReportInput OtherChargeModel)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            var TranSMode = OtherChargeModel.TransMode;
            var dt = new DataTable();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ReportLoadModel reportload = new ReportLoadModel();
            try { 

            var datares = reportload.GetOtherChargeGeneralReport(input: new OtherChargeReportInput
            {
                ToDate = OtherChargeModel.ToDate,
                FromDate = OtherChargeModel.FromDate,
                FK_Branch = OtherChargeModel.FK_Branch,
                OtherChargeType = OtherChargeModel.OtherChargeType,
                Module = OtherChargeModel.Module,
                ImportID = OtherChargeModel.ImportID,
                SupplierID = OtherChargeModel.SupplierID,
                ID_Customer = OtherChargeModel.ID_Customer,
                ProdRptCriteria = OtherChargeModel.ProdRptCriteria,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);


                dt = datares.Data;

                Reportname = (_userLoginInfo.UserName + OtherChargeModel.ReportMode + OtherChargeModel.TransMode).Replace(" ", "");
                Session[TransMode] = TranSMode;
                Session[Reportname] = dt;


                return Json(datares.Process, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region[GetSupplierReportgridViewList]
        public ActionResult GetSupplierReportgridViewList(SupplierReportModel.Suppliergridinput Data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ReportLoadModel objfld = new ReportLoadModel();
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            var TranSMode = Data.TransMode;
            var dt = new DataTable();

            var data = objfld.GetSupplierOutStandingReportView(input: new SupplierReportModel.Suppliergridinput
            {
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                AsonDate = Data.AsonDate,
                BranchID = Data.BranchID,
                SupplierTypeID = Data.SupplierTypeID,
                IncludeAdvance = Data.IncludeAdvance,
                FK_Mode = Data.FK_Mode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = TransMode,
            },
            companyKey: _userLoginInfo.CompanyKey);

            //JsonResult json = Json(data, JsonRequestBehavior.AllowGet);
            //json.MaxJsonLength = int.MaxValue;
            //return json;

            dt = data.Data;
            Reportname = (_userLoginInfo.UserName + Data.FK_Mode + Data.TransMode).Replace(" ", "");
            Session[TransMode] = TranSMode;
            Session[Reportname] = dt;
            return Json(data.Process, JsonRequestBehavior.AllowGet);


        }
        #endregion
        #region[GetIncentiveProcessList]
        [HttpPost]
        public ActionResult GetIncentiveProcessReportList(RptIncentiveProcessListModel.RptIncentiveProcessinputview input)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            string TranSMode = input.TransMode;
            var dt = new DataTable();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            RptIncentiveProcessListModel objfld = new RptIncentiveProcessListModel();
            var datares = objfld.GetIncentiveProcessReportdataTab(new RptIncentiveProcessListModel.RptIncentiveProcessinputview
            {
                ToDate = input.ToDate,
                FromDate = input.FromDate,
                FK_Branch = input.FK_Branch,
                FK_Department = input.FK_Department,
                FK_Employee = input.FK_Employee,
                FK_IncentiveType = input.FK_IncentiveType,
                ReportType = input.ReportType,
                GroupBy = input.GroupBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);

            dt = datares.Data;
            Reportname = (_userLoginInfo.UserName + input.ReportType + input.TransMode).Replace(" ", "");
            Session[TransMode] = TranSMode;
            Session[Reportname] = dt;

            return Json(true, JsonRequestBehavior.AllowGet);

        }

        #endregion
        #region[GetIncentiveReports]
        [HttpPost]
        public ActionResult GetIncentiveProcessReportList1(RptIncentiveProcessListModel.RptIncentiveProcessinput input)
        {
            if (Session["UserLoginInfo"] is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Please login to continue" },
                        Status = "Session Timeout",
                    }
                }, JsonRequestBehavior.AllowGet);
            }
            string TranSMode = input.TransMode;
            var dt = new DataTable();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            //try
            //{

                RptIncentiveProcessListModel objfld = new RptIncentiveProcessListModel();
                if (input.ReportMode!=4)
                {
                    if (input.ReportMode == 1)
                    {
                        var datares = objfld.GetIncentiveProcessReportdataTab(new RptIncentiveProcessListModel.RptIncentiveProcessinputview
                        {
                            ToDate = input.ToDate,
                            FromDate = input.FromDate,
                            FK_Branch = input.FK_Branch,
                            FK_Department = input.FK_Department,
                            FK_Employee = input.FK_Employee,
                            FK_IncentiveType = input.FK_IncentiveType,
                            ReportType = input.ReportType,
                            GroupBy = input.GroupBy,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            FK_Company = _userLoginInfo.FK_Company,

                        }, companyKey: _userLoginInfo.CompanyKey);

                        dt = datares.Data;
                    }
                    else if (input.ReportMode == 2)
                    {
                        var datares = objfld.GetIncentivePaidList(new RptIncentiveProcessListModel.GetRptIncentivePaid
                        {
                            ToDate = input.ToDate,
                            FromDate = input.FromDate,
                            FK_Branch = input.FK_Branch,
                            FK_Department = input.FK_Department,
                            FK_Employee = input.FK_Employee,
                            FK_IncentiveType = input.FK_IncentiveType,
                            ReportType = input.ReportType,
                            GroupBy = input.GroupBy,
                            FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                            FK_Company = _userLoginInfo.FK_Company,
                            PaymentMode = input.PaymentMode,

                        }, companyKey: _userLoginInfo.CompanyKey);

                        dt = datares.Data;
                    }
                else if (input.ReportMode == 3)
                {
                    var datares = objfld.GetIncentiveOutstandList(new RptIncentiveProcessListModel.GetRptIncentiveOutstand
                    {
                        ToDate = input.ToDate == null ? null : input.ToDate,
                        FromDate = input.FromDate == null ? null : input.FromDate,
                        FK_Branch = input.FK_Branch,                        
                        FK_IncentiveType = input.FK_IncentiveType,
                        ReportType = input.ReportType,                        
                        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                        FK_Company = _userLoginInfo.FK_Company,
                        AsOndate=input.AsOndate == null ? null : input.AsOndate,

                    }, companyKey: _userLoginInfo.CompanyKey);

                    dt = datares.Data;
                }
                Reportname = (_userLoginInfo.UserName + input.ReportType + input.TransMode).Replace(" ", "");
                    Session[TransMode] = TranSMode;
                    Session[Reportname] = dt;

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data1 = objfld.GetIncentiveLedger(input: new RptIncentiveProcessListModel.GetLedgerIncentive
                    {
                        FK_Company = _userLoginInfo.FK_Company,
                        FK_Branch = input.FK_Branch,
                        FK_Employee = input.FK_Employee,                        
                        TableCount = 1,                       
                    }, companyKey: _userLoginInfo.CompanyKey);

                    var data2 = objfld.GetIncentiveLedgerDetails(input: new RptIncentiveProcessListModel.GetLedgerIncentive
                    {
                        FK_Company = _userLoginInfo.FK_Company,
                        FK_Branch = input.FK_Branch,
                        FK_Employee = input.FK_Employee,
                        TableCount = 2,
                    }, companyKey: _userLoginInfo.CompanyKey);

                    //var data3 = objfld.GetIncentiveLedgerFooter(input: new RptIncentiveProcessListModel.GetLedgerIncentive
                    //{
                    //    FK_Company = _userLoginInfo.FK_Company,
                    //    FK_Branch = input.FK_Branch,
                    //    FK_Employee = input.FK_Employee,
                    //    TableCount = 3,
                    //}, companyKey: _userLoginInfo.CompanyKey);

                    var detailsData = data1.Data;
                    var detailsProcess = data1.Data;

                    var Data = data2.Data;
                    var Process = data2.Process;

                    //var DataFooter = data3.Data;
                    //var ProcessFooter = data3.Process;

                    return Json(new { Process, Data, detailsProcess, detailsData/*, DataFooter, ProcessFooter*/ }, JsonRequestBehavior.AllowGet);
                }

            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        #endregion
    }
}
#endregion