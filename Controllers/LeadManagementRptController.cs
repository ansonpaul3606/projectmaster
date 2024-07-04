using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.CustomerTicketsModel;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class LeadManagementRptController : Controller
    {
        string Reportname = "";
        string   TransMode = "RptTransMode";
        // GET: LeadManagementRpt
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.CompCategory = _userLoginInfo.CompCategory;
      
            return View();
        }
        public ActionResult LoadCustomerTicketsForm()
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerTicketsModel.Customerlist statusobj = new CustomerTicketsModel.Customerlist();
            var branch = Common.GetDataViaQuery<CustomerTicketsModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.BranchList = branch.Data;

            var product = Common.GetDataViaQuery<CustomerTicketsModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P",
                SelectFields = "ID_Product,ProdName",
                Criteria = "Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.ProductList = product.Data;

            var NextAcList = Common.GetDataViaQuery<CustomerTicketsModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction ,NxtActnName",
                Criteria = "FK_ActionModule=1 AND Cancelled=0  AND Passed=1 AND FK_ActionModule=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_NextAction",
                GroupByFileds = ""
            },

            companyKey: _userLoginInfo.CompanyKey);
            statusobj.NextActionList = NextAcList.Data;

            var CatLeadgen = "";
            ViewBag.ProductMode = 29;
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    ViewBag.ProductMode = 101;
                    CatLeadgen = "AND CatLeadGenerate = 1";
                    break;
                case "2":
                    ViewBag.ProductMode = 29;
                    CatLeadgen = "";
                    break;
                default:
                    ViewBag.ProductMode = 101;
                    CatLeadgen = "AND CatLeadGenerate = 1";
                    break;
            }

            var Category = Common.GetDataViaQuery<CustomerTicketsModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1 " + CatLeadgen + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey);
            statusobj.CategoryList = Category.Data;

            var actiontypelist = Common.GetDataViaQuery<CustomerTicketsModel.ActionTypes>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType,ActnTypeName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 AND FK_ActionModule=1",

                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);
            statusobj.Actntyplists = actiontypelist.Data;
            var compname = Common.GetDataViaQuery<CustomerTicketsModel.Customerlist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);

            statusobj.CompName = compname.Data[0].CompName;

            var deplst = Common.GetDataViaQuery<CustomerTicketsModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepId,DeptName as Depname",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            statusobj.deprtmnt = deplst.Data;

            CustomerTicketsModel objpaymode = new CustomerTicketsModel();
            //var statusmodeList = objpaymode.GeLeadStatusList(input: new CustomerTicketsModel.ModeLead { Mode = 6 }, companyKey: _userLoginInfo.CompanyKey);

            //statusobj.StatusList = statusmodeList.Data;

            var statusmodeList = objpaymode.GeLeadStatusList(input: new CustomerTicketsModel.ModeLead { Mode = 11 }, companyKey: _userLoginInfo.CompanyKey);
        
            statusobj.StatusList = statusmodeList.Data;

            var SummaryMode = objpaymode.GeSummaryList(input: new CustomerTicketsModel.ModeLead { Mode = 110 }, companyKey: _userLoginInfo.CompanyKey);

            statusobj.summaryLists = SummaryMode.Data;

            var targetCriteriaList = objpaymode.GetTargetCriteria(input: new CustomerTicketsModel.ModeLead { Mode = 120 }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.TargetCriteriaList = targetCriteriaList.Data;

            var Newlistsumdata = objpaymode.GeSummaryList(input: new CustomerTicketsModel.ModeLead { Mode = 124 }, companyKey: _userLoginInfo.CompanyKey);

            statusobj.NewListSummary = Newlistsumdata.Data;

            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            ViewBag.CompCategory = _userLoginInfo.CompCategory;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;

            return PartialView("_AddLeadManagementRpt", statusobj);
        }


        public ActionResult getModeList(Int32 mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerTicketsModel.Customerlist statusobj = new CustomerTicketsModel.Customerlist();
            CustomerTicketsModel objpaymode = new CustomerTicketsModel();
            var statusmodeList = objpaymode.GeLeadStatusList(input: new CustomerTicketsModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.StatusList = statusmodeList.Data;
            return Json(statusmodeList, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetCustomerList()
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

            CustomerTicketsModel.Customerlist LeadFromListObj = new CustomerTicketsModel.Customerlist();

            var data = Common.GetDataViaQuery<CustomerTicketsModel.Customerlist>(parameters: new APIParameters
            {
                TableName = "Employee ",
                SelectFields = "ID_Employee AS EmployeeID,EmpFName AS Employeename ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );
            var branch = Common.GetDataViaQuery<CustomerTicketsModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );
            LeadFromListObj.BranchList = branch.Data;


            var empleadfrm = Common.GetDataViaQuery<CustomerTicketsModel.EmployeeLeadInfo>(parameters: new APIParameters
            {
                TableName = "Employee E  join Branch B on E.FK_Branch=B.ID_Branch  join BranchType BT on B.FK_BranchType=BT.ID_BranchType  left join Department D on  E.FK_Department = D.ID_Department",
                SelectFields = "E.ID_Employee,E.EmpFName,CASE WHEN BT.FK_BranchMode IN (1,2) THEN 1 ELSE -1 END AS ID_BranchMode,B.ID_Branch,BT.ID_BranchType,E.FK_Department",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey

          );
            LeadFromListObj.EmployeeLeadInfoList = empleadfrm.Data;

            return Json(data, JsonRequestBehavior.AllowGet);



        }


        [HttpPost]
        public ActionResult GetReportData(Reportdto input)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerTicketsModel objfld = new CustomerTicketsModel();
            string TranSMode = "LFLMRPT";
            #region Rpt1
            if (input.ReportMode == 1)
            {
                var data = objfld.GetActionListReportdataTab(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    //tra = input.TransMode,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    //PageIndex = input.PageIndex + 1,
                    //ID_Report=input.ID_Report,
                    FK_Product = input.FK_Product,
                    FK_Priority = input.FK_Priority,
                    FollowUpAction = input.FollowUpAction,
                    //Companyname = input.Companyname,
                    FK_CollectedBy = input.FK_CollectedBy,
                    Category = input.Category,
                    FK_Area = input.FK_Area,
                    LeadNo = input.LeadNo,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    //PageSize = input.PageSize,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FollowUpType = input.FollowUpType,
                    FK_District=input.FK_District,
                }, companyKey: _userLoginInfo.CompanyKey);

                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                // return Json(data, JsonRequestBehavior.AllowGet);
                //return Json(new { data.Process, data.Data, input.PageSize, input.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            #endregion
            #region Rpt4
            else if (input.ReportMode == 4)
            {
                var data = objfld.GetStatusListReportdataTab(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    //tra = input.TransMode,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    //PageIndex = input.PageIndex + 1,
                    //ID_Report=input.ID_Report,
                    FK_Product = input.FK_Product,
                    FK_Priority = input.FK_Priority,
                    FollowUpAction = input.FollowUpAction,
                    //Companyname = input.Companyname,
                    FK_CollectedBy = input.FK_CollectedBy,
                    Category = input.Category,
                    FK_Area = input.FK_Area,
                    LeadNo = input.LeadNo,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    //PageSize = input.PageSize,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_District = input.FK_District,
                }, companyKey: _userLoginInfo.CompanyKey);

                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                // return Json(data, JsonRequestBehavior.AllowGet);
                //return Json(new { data.Process, data.Data, input.PageSize, input.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            #endregion
            #region Rpt2
            else if (input.ReportMode == 2)
            {
                var data = objfld.GetFollowUpListReportDataTab(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate, 
                    ReportMode = input.ReportMode,
                    FK_Product = input.FK_Product,
                    FK_Priority = input.FK_Priority,
                    FollowUpAction = input.FollowUpAction,
                    FollowUpType=input.FollowUpType,
                    FK_CollectedBy = input.FK_CollectedBy,
                    Category = input.Category,
                    FK_Area = input.FK_Area,
                    LeadNo = input.LeadNo,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_District = input.FK_District,
                }, companyKey: _userLoginInfo.CompanyKey);

                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            #endregion
            #region Rpt5
            else if (input.ReportMode == 5)
            {
                var data = objfld.GetNewListReportDataTab(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                   // FK_Employee = input.FK_Employee,
                    FK_AssignTo= input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    FK_Product = input.FK_Product,
                    FK_Priority = input.FK_Priority,
                    FollowUpAction = input.FollowUpAction,
                    FK_CollectedBy = input.FK_CollectedBy,
                    Category = input.Category,
                    FK_Area = input.FK_Area,
                    LeadNo = input.LeadNo,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TargetCriteria =input.TargetCriteria,
                    FK_District = input.FK_District,
                    ProductType=input.ProductType,
                }, companyKey: _userLoginInfo.CompanyKey);

                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            #endregion
            #region Rpt6 and & 7
            else if (input.ReportMode == 6 || input.ReportMode == 7)
            {
                
                var data = objfld.GetProductwiseLeadListRptData(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    FK_Product = input.FK_Product,
                    FK_Priority = input.FK_Priority,
                    FollowUpAction = input.FollowUpAction,
                    FK_CollectedBy = input.FK_CollectedBy,
                    Category = input.Category,
                    FK_Area = input.FK_Area,
                    LeadNo = input.LeadNo,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    ProductType = input.ProductType,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_District = input.FK_District,
                }, companyKey: _userLoginInfo.CompanyKey);

                Reportname =( _userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[TransMode] = TranSMode;
                Session[Reportname] = data.Data;
                return Json(new { data.Process}, JsonRequestBehavior.AllowGet);

            }
            #endregion
            #region Rp7
            //else if (input.ReportMode ==7)
            //{
            //    var data = objfld.GetProductwiseLeadListReportData(input: new Reportdto
            //    {
            //        FK_Company = _userLoginInfo.FK_Company,
            //        FK_Branch = input.FK_Branch,
            //        FK_Employee = input.FK_Employee,
            //        FromDate = input.FromDate,
            //        ToDate = input.ToDate,
            //        ReportMode = input.ReportMode,
            //        FK_Product = input.FK_Product,
            //        FK_Priority = input.FK_Priority,
            //        FollowUpAction = input.FollowUpAction,
            //        FK_CollectedBy = input.FK_CollectedBy,
            //        Category = input.Category,
            //        FK_Area = input.FK_Area,
            //        LeadNo = input.LeadNo,
            //        Criteria = input.Criteria,
            //        Status = input.Status,
            //        EntrBy = _userLoginInfo.EntrBy,
            //        ProductType = _userLoginInfo.ProductType,
            //        FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            //        FK_Machine = _userLoginInfo.FK_Machine,
            //    }, companyKey: _userLoginInfo.CompanyKey);


            //    return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);

            //}
            #endregion      
            #region Rpt3

            else if (input.ReportMode == 3)
            {
                var data1 = objfld.GetLedgerListReportData(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    FK_Product = input.FK_Product,
                    FK_Priority = input.FK_Priority,
                    FollowUpAction = input.FollowUpAction,
                    FK_CollectedBy = input.FK_CollectedBy,
                    Category = input.Category,
                    FK_Area = input.FK_Area,
                    LeadNo = input.LeadNo,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    ProductType = _userLoginInfo.ProductType,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TableCount = 1,
                    FK_District = input.FK_District,
                }, companyKey: _userLoginInfo.CompanyKey);

                var data2 = objfld.GetLedgerListDetailsReportData(input: new CustomerTicketsModel.Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    FK_Product = input.FK_Product,
                    FK_Priority = input.FK_Priority,
                    FollowUpAction = input.FollowUpAction,
                    FK_CollectedBy = input.FK_CollectedBy,
                    Category = input.Category,
                    FK_Area = input.FK_Area,
                    LeadNo = input.LeadNo,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    ProductType = _userLoginInfo.ProductType,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TableCount = 2,
                    FK_District = input.FK_District,
                }, companyKey: _userLoginInfo.CompanyKey);

                var Data = data2.Data;
                var Process = data2.Process;

                var detailsData = data1.Data;
                var detailsProcess = data1.Data;




                return Json(new { Process, Data, detailsProcess, detailsData }, JsonRequestBehavior.AllowGet);

            }
            #endregion
            #region Rpt8
            else if (input.ReportMode == 8)
            {

                var data = objfld.GetLeadSummary(input: new ReportLeadSummaryInput
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.FK_Area,// Actaual pass Summary Mode,
                    FK_Product = input.FK_Product,
                    FK_Category = input.Category,
                }, companyKey: _userLoginInfo.CompanyKey);
                
                Reportname = (_userLoginInfo.UserName + input.ReportMode+ input.FK_Area + TranSMode).Replace(" ", ""); ;
                Session[TransMode] = TranSMode;
                Session[Reportname] = data.Data;
                
                return Json(new { data.Process }, JsonRequestBehavior.AllowGet);

            }
            #endregion

            #region Rpt9
            else if (input.ReportMode == 9)
            {
                var data = objfld.GetActionListReportdataTab(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
            Destination= input.Destination,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,                     
                    Category = input.Category,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_Area= input.FK_Area,
                    FK_District = input.FK_District,
                }, companyKey: _userLoginInfo.CompanyKey);

                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                // return Json(data, JsonRequestBehavior.AllowGet);
                //return Json(new { data.Process, data.Data, input.PageSize, input.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
                return Json(true, JsonRequestBehavior.AllowGet);


            }
            #endregion
            #region Rpt Employee Wise Target
            else if (input.ReportMode == 10)
            {
                var data = objfld.GetEmployeeWiseTargetListRptData(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,                    
                    TargetCriteria = input.TargetCriteria,                    
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    DepartmentID = input.DepartmentID,
                    Criteria = input.Criteria,
                }, companyKey: _userLoginInfo.CompanyKey);
                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else if (input.ReportMode == 11)
            {
                var data = objfld.GetLeadAssignListRptData(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_AssignBy = input.FK_AssignBy,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    FK_AssignTo = input.FK_AssignTo,
                    FK_CollectBy = input.FK_CollectBy,
                    FollowUpAction = input.FollowUpAction,
                    //TargetCriteria = input.TargetCriteria,
                    //EntrBy = _userLoginInfo.EntrBy,
                    //FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    //FK_Machine = _userLoginInfo.FK_Machine,
                    //DepartmentID = input.DepartmentID,
                    //Criteria = input.Criteria,
                }, companyKey: _userLoginInfo.CompanyKey);
                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else if (input.ReportMode == 12)
            {
                var data = objfld.GetLeadAssignListRptData(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                  
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    FK_AssignTo = input.FK_AssignTo,
                    FK_CollectBy = input.FK_CollectBy,
                    FK_Product = input.FK_Product,
                    Category = input.Category,

                }, companyKey: _userLoginInfo.CompanyKey);
                Session[TransMode] = TranSMode;
                Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                Session[Reportname] = data.Data;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            #endregion
            #region Rpt else


            else
            {
                var data = objfld.GetStatusListReportDataTab(input: new Reportdto
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = input.FK_Branch,
                    FK_Employee = input.FK_Employee,
                    FromDate = input.FromDate,
                    ToDate = input.ToDate,
                    ReportMode = input.ReportMode,
                    FK_Product = input.FK_Product,
                    FK_Priority = input.FK_Priority,
                    FollowUpAction = input.FollowUpAction,
                    FK_CollectedBy = input.FK_CollectedBy,
                    Category = input.Category,
                    FK_Area = input.FK_Area,
                    LeadNo = input.LeadNo,
                    Criteria = input.Criteria,
                    Status = input.Status,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_District = input.FK_District,
                }, companyKey: _userLoginInfo.CompanyKey);
                ViewBag.Table = data.Data;
                Session[TransMode] = TranSMode;
                Session[Reportname] = data.Data;
                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);
            }
            #endregion
        }

        public ActionResult getGroupModeList(CustomerTicketsModel.GroupModeinput data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerTicketsModel.Customerlist statusobj = new CustomerTicketsModel.Customerlist();
            CustomerTicketsModel objpaymode = new CustomerTicketsModel();
            var groupList = objpaymode.GetGroupList(input: new CustomerTicketsModel.GroupModeinput { Mode = data.Mode, Group= data.Group }, companyKey: _userLoginInfo.CompanyKey);
            statusobj.GroupList = groupList.Data;
            return Json(groupList, JsonRequestBehavior.AllowGet);

        }


    }
}