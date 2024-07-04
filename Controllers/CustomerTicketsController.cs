using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using static PerfectWebERP.Models.CustomerTicketsModel;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class CustomerTicketsController : Controller
    {
        // GET: CustomerTickets
        public ActionResult CustomerTickets()
        {
            //UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            //ViewBag.Username = _userLoginInfo.UserName;
            //ViewBag.UserRole = _userLoginInfo.UserRole;
            //ViewBag.UserAvatar = _userLoginInfo.UserAvatar;

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
                Criteria = "Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company  ,
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
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey);

            statusobj.CompName = compname.Data[0].CompName;

            CustomerTicketsModel objpaymode = new CustomerTicketsModel();
            //var statusmodeList = objpaymode.GeLeadStatusList(input: new CustomerTicketsModel.ModeLead { Mode = 6 }, companyKey: _userLoginInfo.CompanyKey);

            //statusobj.StatusList = statusmodeList.Data;

            var statusmodeList = objpaymode.GeLeadStatusList(input: new CustomerTicketsModel.ModeLead { Mode = 11 }, companyKey: _userLoginInfo.CompanyKey);

            statusobj.StatusList = statusmodeList.Data;



            var SummaryMode = objpaymode.GeSummaryList(input: new CustomerTicketsModel.ModeLead { Mode = 110 }, companyKey: _userLoginInfo.CompanyKey);

            statusobj.summaryLists = SummaryMode.Data;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;

            return PartialView("_AddCustomertickets", statusobj);
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

            if (input.ReportMode == 1)
            {
                var data = objfld.GetActionListReportdata(input: new Reportdto
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
                }, companyKey: _userLoginInfo.CompanyKey);


                // return Json(data, JsonRequestBehavior.AllowGet);
                //return Json(new { data.Process, data.Data, input.PageSize, input.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);
            }
            else if(input.ReportMode ==4)
            {
                var data = objfld.GetStatusListReportData(input: new Reportdto
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
                }, companyKey: _userLoginInfo.CompanyKey);


                // return Json(data, JsonRequestBehavior.AllowGet);
                //return Json(new { data.Process, data.Data, input.PageSize, input.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);
            }
            else if(input.ReportMode == 2)
            {
                var data = objfld.GetFollowUpListReportData(input: new Reportdto
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
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);

            }
            else if (input.ReportMode == 5) 
            {
                var data = objfld.GetNewListReportData(input: new Reportdto
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
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);

            }
            else if (input.ReportMode == 6|| input.ReportMode == 7)
            {
                var data = objfld.GetProductwiseLeadListReportData(input: new Reportdto
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
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);

            }
            #region Rpt 7
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
                }, companyKey: _userLoginInfo.CompanyKey);

                var data2 = objfld.GetLedgerListDetailsReportData(input: new Reportdto
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
                }, companyKey: _userLoginInfo.CompanyKey);

                var Data = data2.Data;
                var Process = data2.Process;

                var detailsData = data1.Data;
                var detailsProcess = data1.Data;
                return Json(new { Process, Data, detailsProcess, detailsData }, JsonRequestBehavior.AllowGet);

            }
            #region Rpt8
            else if (input.ReportMode == 8)
            {

                var data = objfld.GetLeadSummaryGrid(input: new ReportLeadSummaryInput
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

                //Reportname = (_userLoginInfo.UserName + input.ReportMode + TranSMode).Replace(" ", ""); ;
                //Session[TransMode] = TranSMode;
                //Session[Reportname] = data.Data;
                return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);

            }
            #endregion
            else
            {
                var data = objfld.GetStatusListReportData(input: new Reportdto
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
                }, companyKey: _userLoginInfo.CompanyKey);


                 return Json(new { data.Process, data.Data }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}