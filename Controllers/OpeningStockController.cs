/*----------------------------------------------------------------------
Created By	: Jisi Rajan
Created On	: 07/02/2022
Purpose		: OpeningStock
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Business;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Interface;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System.Data;
using Newtonsoft.Json;
using PerfectWebERP.General;
using System.ComponentModel.DataAnnotations;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class OpeningStockController : Controller
    {
        public ActionResult Index(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            //  ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }



        public ActionResult LoadFormOpeningStock(string OpeningStockTransMode)
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

            OpeningStockModel.OpeningStockListModel listModel = new OpeningStockModel.OpeningStockListModel();
            listModel.FK_Employee = _userLoginInfo.FK_Employee;
            var OpSList = Common.GetDataViaQuery<OpeningStockModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeName,M.Mode",
                Criteria = "Mode!='V'",
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey

             );

            var OpBranchList = Common.GetDataViaQuery<OpeningStockModel.BranchList>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );
            var OpDepartmentList = Common.GetDataViaQuery<OpeningStockModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                       companyKey: _userLoginInfo.CompanyKey

            );


            listModel.ModeList = OpSList.Data;
            listModel.BranchList = OpBranchList.Data;
            listModel.DepartmentList = OpDepartmentList.Data;

            OpeningStockModel model = new OpeningStockModel();
            var SerialNo = model.GetCustomerNo(input: new OpeningStockModel.inputGetSerialNo { FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            listModel.SerialNumber = SerialNo.Data[0].SerialNumber;

            PurchaseOrderModel models = new PurchaseOrderModel();
            var LastIDNo = models.GetPurchaseOrderlastID(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseOrderModel.LastIdIn { FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = OpeningStockTransMode });
            listModel.LastID = LastIDNo.Data[0].LastID;

            return PartialView("_AddOpeningStockForm", listModel);
        }


        public ActionResult GetDepartmentList(Int32 departmentid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<OpeningStockModel.DepartmentList>(parameters: new APIParameters
            {

                TableName = "Department  D left join Employee E on  D.ID_Department = E.FK_Department",
                SelectFields = "DeptName AS DepartmentName,ID_Department AS DepartmentID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND Cancelled =0 AND Passed=1 AND FK_Department='" + departmentid + "'",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);


        }


        public ActionResult GetBindproductdata(string Mode)
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
            #region :: Model validation  ::

            #endregion :: Model validation  ::


            var data = Common.GetDataViaQuery<OpeningStockModel.ProductListView>(parameters: new APIParameters
            {

                TableName = "[Product]",
                SelectFields = "[ID_Product] AS ProductID,[ProdName] AS Product,ProdHSNCode AS ProdHSNCode",
                Criteria = "Passed=1 And Cancelled=0 and  Mode='" + Mode + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewOpeningStock(OpeningStockModel.OpeningStockView data)
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

            ModelState.Remove("MRP");
            ModelState.Remove("SalPrice");
            ModelState.Remove("ProductionCost");
            ModelState.Remove("OpeningQuantity");
            ModelState.Remove("OpeningStbyQuantity");
            ModelState.Remove("ProductID");
            ModelState.Remove("Product");
            ModelState.Remove("ProdHSNCode");
            ModelState.Remove("BranchID");
            ModelState.Remove("BranchName");
            ModelState.Remove("DepartmentID");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("ModeID");
            ModelState.Remove("Mode");
            ModelState.Remove("ModeName");
            ModelState.Remove("QRCode");
            ModelState.Remove("SerialNumber");
            ModelState.Remove("BarCode");
            ModelState.Remove("OpeningStockListxmls.MRP");
            ModelState.Remove("OpeningStockListxmls.SalPrice");
            ModelState.Remove("OpeningStockListxmls.ProductionCost");
            ModelState.Remove("OpeningStockListxmls.OpeningQuantity");
            ModelState.Remove("OpeningStockListxmls.OpeningStbyQuantity");
            ModelState.Remove("OpeningStockListxmls.ProductID");
            ModelState.Remove("OpeningStockListxmls.Product");
            ModelState.Remove("OpeningStockListxmls.ProdHSNCode");
            ModelState.Remove("OpeningStockListxmls.BranchID");
            ModelState.Remove("OpeningStockListxmls.BranchName");
            ModelState.Remove("OpeningStockListxmls.DepartmentID");
            ModelState.Remove("OpeningStockListxmls.DepartmentName");
            ModelState.Remove("OpeningStockListxmls.ModeID");
            ModelState.Remove("OpeningStockListxmls.Mode");
            ModelState.Remove("OpeningStockListxmls.ModeName");
            ModelState.Remove("OpeningStockListxmls.QRCode");
            ModelState.Remove("OpeningStockListxmls.SerialNumber");
            ModelState.Remove("OpeningStockListxmls.BarCode");
            ModelState.Remove("OpeningStockListxmls");
            ModelState.Clear();
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = ModelState.Values.SelectMany(m => m.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList(),
                        Status = "Validation failed",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            OpeningStockModel OpeningStock = new OpeningStockModel();

            var datresponse = OpeningStock.UpdateOpeningStockData(input: new OpeningStockModel.UpdateOpeningStock
            {
                UserAction = 1,
                OpeningStockXmlDetails = data.OpeningStockListxmls is null ? "" : Common.xmlTostring(data.OpeningStockListxmls),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }




     


    }
}


