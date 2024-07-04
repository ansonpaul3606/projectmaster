using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class StockReceiveController : Controller
    {
        #region[Index]
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.TransMode = mGrp;
            return View();

        }
        #endregion

        #region[LoadStockReceive]
        public ActionResult LoadStockReceive(string mtd, string TransMode)
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            StockReceiveModel.StockReceiveView view = new StockReceiveModel.StockReceiveView();

            var OpBranchList = Common.GetDataViaQuery<StockReceiveModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey

          );
            view.BranchList = OpBranchList.Data;

            var OpDepartmentList = Common.GetDataViaQuery<StockReceiveModel.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );
            view.DepartmentList = OpDepartmentList.Data;

            var OpBranchListto = Common.GetDataViaQuery<StockReceiveModel.BranchTo>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchIDTo,BrName AS BranchNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
               companyKey: _userLoginInfo.CompanyKey

              );
            view.BranchListTo = OpBranchListto.Data;

            var OpDepartmentListto = Common.GetDataViaQuery<StockReceiveModel.DepartmentTo>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentIDTo,DeptName AS DepartmentNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                       companyKey: _userLoginInfo.CompanyKey

            );
            view.DepartmentListTo = OpDepartmentListto.Data;

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);
            PurchaseOrderModel models = new PurchaseOrderModel();
            var LastIDNo = models.GetPurchaseOrderlastID(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseOrderModel.LastIdIn { FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = TransMode });
            view.LastID = LastIDNo.Data[0].LastID;

            //Show  Unit List
            var UnitList = Common.GetDataViaQuery<StockReceiveModel.Unit>(parameters: new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "[ID_Unit] AS ID_Unit,[UnitName] AS UnitName,NoOfUnits AS UnitCount",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

              );
            view.UnitList = UnitList.Data;

            var unitset = Common.GetDataViaQuery<ProductModel.MultipleUnitSettings>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT003'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
   companyKey: _userLoginInfo.CompanyKey

);


            ViewBag.Multiunitsettings = unitset.Data[0].GsValue;
            return PartialView("_AddStockReceive", view);
        }
        #endregion

        #region[GetTransferReqData]
        public ActionResult GetTransferReqData(int BranchIDTo, int DepartmentIDTo, int? EmployeeIDTo)
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
            StockReceiveModel model = new StockReceiveModel();
            var searchInfo = model.GetSearchTransferdata(companyKey: _userLoginInfo.CompanyKey, input: new StockReceiveModel.SearchInput
            {
                BranchIDTo = BranchIDTo,
                DepartmentIDTo = DepartmentIDTo,
                EmployeeIDTo = (EmployeeIDTo.HasValue) ? EmployeeIDTo.Value : 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });

            return Json(new { searchInfo }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[GetStocktransferInfo]
        [HttpPost]
        public ActionResult GetStocktransferInfo(StockReceiveModel.StockReceiveView data)
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
            StockReceiveModel objprd = new StockReceiveModel();

            var subtable = objprd.Fillproduct(companyKey: _userLoginInfo.CompanyKey, input: new StockReceiveModel.EmployeeStockTransferSubView
            {
                FK_StockTransfer = (data.FK_StockTransfer.HasValue) ? data.FK_StockTransfer.Value : 0,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });


            return Json(subtable, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[AddStockReceiveData]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddStockReceiveData(StockReceiveModel.StockReceiveView view)
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

            StockReceiveModel model = new StockReceiveModel();

            var result = model.UpdateEmployeeStockReceiveData(new StockReceiveModel.UpdateStockReceive
            {
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_BranchFrom = view.BranchID,
                FK_BranchTo = view.BranchIDTo,
                FK_DepartmentFrom =view.DepartmentID,
                FK_DepartmentTo = view.DepartmentIDTo,
                FK_EmployeeFrom = view.EmployeeID,
                FK_EmployeeTo = view.EmployeeIDTo,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                UserAction =  1,
                Debug = 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransDate = view.TransDate,
                TransMode = view.TransMode,
                ID_StockTransfer = (view.FK_StockTransfer.HasValue) ? view.FK_StockTransfer.Value : 0,
                LastID = (view.LastID.HasValue) ? view.LastID.Value : 0,
                EmployeeStockReceiveDetails = view.SubEmployeeStockTransfers is null ? "" : Common.xmlTostring(view.SubEmployeeStockTransfers),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = result }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}