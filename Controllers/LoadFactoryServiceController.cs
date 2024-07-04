using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class LoadFactoryServiceController : Controller
    {
        // GET: LoadFactoryService
        public ActionResult Index()
        {
            @ViewBag.PageTitle = "Factor Service";
            return View();
           
        }


        [HttpPost]
        public ActionResult GetSearchResults(LoadFactoryService.TicketInputNew Data)
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

            LoadFactoryService Model = new LoadFactoryService();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Model.GetTicketsnew(input: new LoadFactoryService.TicketInputNew
            {
                FK_Branch = Data.FK_Branch == null ? 0 : Data.FK_Branch,
                FK_Product = Data.FK_Product == null ? 0 : Data.FK_Product,
                EntrBy = _userLoginInfo.EntrBy,
                FK_ComplaintType = Data.FK_ComplaintType == null ? 0 : Data.FK_ComplaintType,
                Status = Data.Status,
                FromDate = Data.FromDate == null ? "" : Data.FromDate,
                ToDate = Data.ToDate == null ? "" : Data.ToDate,
                TicketNumber = Data.TicketNumber == null ? "" : Data.TicketNumber,
                Customer = Data.Customer == null ? "" : Data.Customer,
                Mobile = Data.Mobile == null ? "" : Data.Mobile,
                SortOrder = Data.SortOrder == null ? 0 : Data.SortOrder,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = Data.PageIndex + 1,
                PageSize = Data.PageSize,
                Mode = Data.Mode,
                FK_Area = Data.FK_Area == null ? 0 : Data.FK_Area,
                FK_Post = Data.FK_Post == null ? 0 : Data.FK_Post,
                FK_Employee = Data.FK_Employee == null ? 0 : Data.FK_Employee,
                DueDays = Data.DueDays == null ? 0 : Data.DueDays,
            }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::


            //var iop;

            //for(var i = 0; i < 5; i++)
            //{
            //    data.Data.ForEach((j) =>
            //    {

            //    });
            //};
           



            LoadFactoryService.ServiceAssignDataCount obj = new LoadFactoryService.ServiceAssignDataCount();

            if (data.Data != null)
            {
                foreach (var row in data.Data)
                {
                    switch (Convert.ToInt32(row.MasterID))
                    {
                        case 2:
                            obj.New = Convert.ToInt32(row.Value);
                            obj.NewStatus = "display:block";
                            break;
                        case 3:
                            obj.Completed = Convert.ToInt32(row.Value);
                            obj.CompletedStatus = "display:block";
                            break;
                        case 4:
                            obj.Pending = Convert.ToInt32(row.Value);
                            obj.PendingStatus = "display:block";
                            break;
                        case 5:
                            obj.PickupRequest = Convert.ToInt32(row.Value);
                            obj.PickupRequestStatus = "display:block";
                            break;
                        case 6:
                            obj.ReplacementRequest = Convert.ToInt32(row.Value);
                            obj.ReplacementRequestStatus = "display:block";
                            break;
                        case 7:
                            obj.DeliveryRequest = Convert.ToInt32(row.Value);
                            obj.DeliveryRequestStatus = "display:block";
                            break;
                        case 8:
                            obj.FactoryService = Convert.ToInt32(row.Value);
                            obj.FactoryServiceStatus = "display:block";
                            break;
                    }
                }
            }

            return Json(new { data.Process, data.Data, Data.PageSize, Data.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount, obj }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadFactoryServicepage()
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


            LoadFactoryService.CustomerserviceassignView Assign = new LoadFactoryService.CustomerserviceassignView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var BranchList = Common.GetDataViaQuery<LoadFactoryService.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = " ID_Branch,BrName BranchName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );
            var ComplaintList = Common.GetDataViaQuery<LoadFactoryService.Complaint>(parameters: new APIParameters
            {
                TableName = "ComplaintList",
                SelectFields = "ID_ComplaintList,CompntName ComplaintName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );
            var OpDepartmentList = Common.GetDataViaQuery<LoadFactoryService.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey
            );
            var NetAction = Common.GetDataViaQuery<LoadFactoryService.Status>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = " ID_NextAction,NxtActnName,FK_ActionStatus",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );

            Assign.StatusList = NetAction.Data;
            LoadFactoryService objpaymode = new LoadFactoryService();
            var rolemodeList = objpaymode.GeLeadStatusList(input: new LoadFactoryService.ModeLead { Mode = 21 }, companyKey: _userLoginInfo.CompanyKey);

            Assign.ActionStatusList = rolemodeList.Data;
            Assign.DepartmentList = OpDepartmentList.Data;
            Assign.BranchList = BranchList.Data;
            Assign.ComplaintList = ComplaintList.Data;
            //5-Service
            //DepartmentModel objDept = new DepartmentModel();
            //var defaultDep = objDept.GetDefault(input: new DepartmentModel.GetDefaultDepartment { Mode = 5, FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            Assign.ID_DefaultDept = _userLoginInfo.FK_Department;


            CommonMethod objCmnMethod = new CommonMethod();
           // ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            //return PartialView("_AddServiceassignNew", Assign);
           // return PartialView("_LoadServiceAssign", Assign);
            return PartialView("_LoadFactory_Service_view", Assign);
        }
    }
}