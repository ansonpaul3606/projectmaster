using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.ProductionReport;

namespace PerfectWebERP.Controllers
{
    //[CheckSessionTimeOut]
    public class ProductionReportController : Controller
    {
      
        // GET: StockReport 
        public ActionResult Index()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            return View();

        }

        public ActionResult LoadFormProductionReport()
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


            StockReportModel stock = new StockReportModel();
            StockReportModel.StockReportView stockview = new StockReportModel.StockReportView();


            var branch = Common.GetDataViaQuery<StockReportModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            stockview.BranchList = branch.Data;


            //////
          //   var branch = Common.GetDataViaQuery<StockReportModel.Branchs>(parameters: new APIParameters
          //  {
          //      TableName = "Branch ",
          //      SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
          //      Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
          //      SortFields = "",
          //      GroupByFileds = ""
          //  },
          //companyKey: _userLoginInfo.CompanyKey);

          //  stockview.BranchList = branch.Data;
            ///
            var branchTo = Common.GetDataViaQuery<StockReportModel.BranchTo>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchIDTo,BrName AS BranchNameTo",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            stockview.BranchListTo = branchTo.Data;
            var Companyname = Common.GetDataViaQuery<SalesReportModel.salelist>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName Companyname",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);

            stockview.Companyname = Companyname.Data[0].Companyname;
            var categorylist = Common.GetDataViaQuery<StockReportModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = " ID_Category AS CategoryID,CatName AS CategoryName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );


            stockview.CategoryList = categorylist.Data;

            var departmentlist = Common.GetDataViaQuery<StockReportModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentID,DeptName as DepartmentName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey
         );


            stockview.DepartmentList = departmentlist.Data;
            var departmentlistTo = Common.GetDataViaQuery<StockReportModel.DepartmentTo>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepartmentIDTo,DeptName as DepartmentNameTo",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
                companyKey: _userLoginInfo.CompanyKey
                );


            stockview.DepartmentListTo = departmentlistTo.Data;

            var ModeLists = Common.GetDataViaQuery<StockReportModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeNames,M.Mode Modes",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );
            stockview.ModeList = ModeLists.Data;

            ProductionReport PrReport = new ProductionReport();
            ProductionReportView PrReportView = new ProductionReportView();
            var AccountGroupType = PrReport.FillReportType(input: new ProductionReport.ReportTypeMode { Mode = 70 }, companyKey: _userLoginInfo.CompanyKey);
            PrReportView.ReportType = AccountGroupType.Data;

            //return PartialView("_AddProductionReport", stockview);
            return PartialView("_AddProductionReport", PrReportView);
        }

        public ActionResult getModeCriteriaList(Int32 mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            StockReportModel.StockReportView stockview = new StockReportModel.StockReportView();
            StockReportModel jpaymode = new StockReportModel();
            var criteriamodeList = jpaymode.GroupingCriteriaList(input: new StockReportModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
            // stockview.CriteriaList = criteriamodeList.Data;
            return Json(criteriamodeList.Data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetProductionReportList(JobcartReportView inputdata)
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


            ProductionReport jobcart = new ProductionReport();
            var data = jobcart.GetJobCardReport(companyKey: _userLoginInfo.CompanyKey, input: new ProductionReport.JobcartReportView
            {  


                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch= _userLoginInfo.FK_Branch,
                FK_product= inputdata.FK_product,
                FK_Stage=inputdata.FK_Stage,
                Status=inputdata.Status,
                FromDate=inputdata.FromDate,
                AsonDate=inputdata.AsonDate,
                ToDate=inputdata.ToDate,
                ReportMode= inputdata.ReportMode,
                GroupBy = inputdata.GroupBy,
                FK_Material=inputdata.FK_Material,
                ID_JobCard = inputdata.ID_JobCard
            });
            //List<string> headers = new List<string>();
            //foreach (DataColumn col in data.Data.)
            //{
            //    headers.Add(col.ColumnName);
            //}
            //ProjectStagesModel ProjectStages = new ProjectStagesModel();
            //var data = ProjectStages.GetProjectStagesData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectStagesModel.InputProjectStagesID
            //{
            //    FK_ProjectStages = 0,
            //    FK_Company = _userLoginInfo.FK_Company,
            //    FK_Machine = _userLoginInfo.FK_Machine,
            //    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            //    EntrBy = _userLoginInfo.EntrBy,
            //    PageIndex = pageIndex + 1,
            //    PageSize = pageSize,
            //    Name = Name,
            //    TransMode = TransMode
            //});

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data   }, JsonRequestBehavior.AllowGet);

            

        }

        [HttpPost]
        public ActionResult FillReportType(ProductionReport.ReportTypeMode data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AccountGroupModel.AccountGroupTypeView AccountGroupTypeList = new AccountGroupModel.AccountGroupTypeView();
            AccountGroupModel objAccountGrp = new AccountGroupModel();

            var AccountGroupType = objAccountGrp.FillAccountGroup(input: new AccountGroupModel.FinalAccountTypeMode { Mode = 70 }, companyKey: _userLoginInfo.CompanyKey);
            object result;
            //switch (data.FK_AccType)
            //{
            //    case 2:
            //        result = AccountGroupType.Data.Where(a => a.ModeParent == 2).ToList();
            //        break;
            //    case 3:
            //        result = AccountGroupType.Data.Where(a => a.ModeParent == 3).ToList();
            //        break;
            //    default:
            //        result = AccountGroupType;
            //        break;
            //}


            //AccountGroupTypeList.AccountGroupType = data.AccountGroupType;

             return Json(new { AccountGroupType }, JsonRequestBehavior.AllowGet);
           // return null;

        }
        public ActionResult GetEmployeeLeadDefault()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<LeadGenerateModel.EmployeeLeadInfo>(parameters: new APIParameters
            {
                TableName = "Employee E  join Branch B on E.FK_Branch=B.ID_Branch  join BranchType BT on B.FK_BranchType=BT.ID_BranchType  left join Department D on  E.FK_Department = D.ID_Department",
                SelectFields = "E.ID_Employee,E.EmpFName,CASE WHEN BT.FK_BranchMode IN (1,2) THEN 1 ELSE -1 END AS ID_BranchMode,B.ID_Branch,BT.ID_BranchType,E.FK_Department, BT.BTName,B.BrName,D.DeptName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );


            //OrganizationModel Organization = new OrganizationModel();

            //var data = Organization.GetOrganizationData(companyKey: _userLoginInfo.CompanyKey, input: new OrganizationModel.OrganizationID { ID_Organization = 0 });

            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}