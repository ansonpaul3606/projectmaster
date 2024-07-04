/*----------------------------------------------------------------------
Created By	: AmrithaAk
Created On	: 19/03/2022
Purpose		: EmployeeStockTransfer
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
    public class StockTransferRequestController : Controller
    {
       
        public ActionResult RequestIndex(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;            
            return View();
        }
        public ActionResult LoadFormEmployeeStockTransfer(string TransMode)
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

            EmployeeStockTransferModel.EmployeeStockTransferViewList list = new EmployeeStockTransferModel.EmployeeStockTransferViewList();

            var OpDepartmentList = Common.GetDataViaQuery<EmployeeStockTransferModel.Department>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS DepartmentName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                        companyKey: _userLoginInfo.CompanyKey

             );
            list.DepartmentList = OpDepartmentList.Data;

            var OpDepartmentListto = Common.GetDataViaQuery<EmployeeStockTransferModel.DepartmentTo>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentIDTo,DeptName AS DepartmentNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                       companyKey: _userLoginInfo.CompanyKey

            );
            list.DepartmentListTo = OpDepartmentListto.Data;


            var OpBranchList = Common.GetDataViaQuery<EmployeeStockTransferModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                       companyKey: _userLoginInfo.CompanyKey

            );
            list.BranchList = OpBranchList.Data;

            var OpBranchListto = Common.GetDataViaQuery<EmployeeStockTransferModel.BranchTo>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchIDTo,BrName AS BranchNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                     companyKey: _userLoginInfo.CompanyKey

          );
            list.BranchListTo = OpBranchListto.Data;

            ViewBag.TransMode = TransMode;
            //Show  Unit List
            var UnitList = Common.GetDataViaQuery<EmployeeStockTransferModel.Unit>(parameters: new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "[ID_Unit] AS ID_Unit,[UnitName] AS UnitName,NoOfUnits AS UnitCount",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

              );
            list.UnitList = UnitList.Data;
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
            return PartialView("_AddStockRequest", list);
        }
        [HttpPost]

        [ValidateAntiForgeryToken()]
        public ActionResult GetDepartmentList()
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

            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
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

            #endregion :: Model validation  ::

            var departmentInfo = Common.GetDataViaQuery<EmployeeStockTransferModel.DepartmentTo>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentIDTo,DeptName AS DepartmentNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
       );
            return Json(departmentInfo, JsonRequestBehavior.AllowGet);


        }

        public ActionResult GetProductList(EmployeeStockTransferModel.StockTransferID datas)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


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

            ModelState.Remove("ReasonID");

            if (!ModelState.IsValid)
            {
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
            EmployeeStockTransferModel employeeStock = new EmployeeStockTransferModel();

            var EmployeetransproductInfo = employeeStock.GetStockTransferData(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeStockTransferModel.StockTransferID
            {
                FK_Branch = datas.FK_Branch,
                FK_Department = datas.FK_Department,
                FK_Employee = datas.FK_Employee,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = datas.TransMode
            });

            return Json(EmployeetransproductInfo, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetProductBaseInfo(long productID)
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

            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
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

            #endregion :: Model validation  ::

            var productInfo = Common.GetDataViaQuery<EmployeeStockTransferModel.ProductBaseInfo>(parameters: new APIParameters
            {
                TableName = "Stock as S LEFT JOIN  Product AS P ON P.ID_Product = S.FK_Product ",
                SelectFields = "S.CurrentQuantity AS QuantityAvaliable,S.CurrentStbyQuantity AS StandByQuantity,S.ID_Stock AS StockID",
                Criteria = " S.FK_Company=" + _userLoginInfo.FK_Company + "AND S.FK_Product = " + productID,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
       );
            return Json(productInfo, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewEmployeeStocktransfer(EmployeeStockTransferModel.EmployeeStockTransferView data)
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

           
            EmployeeStockTransferModel employeeStocktrnf = new EmployeeStockTransferModel();
            var datresponse = employeeStocktrnf.UpdateEmployeeStockTransferData(input: new EmployeeStockTransferModel.UpdateEmployeeStockTransfer
            {
                UserAction = 1,
                FK_BranchFrom = data.BranchID,
                FK_DepartmentFrom = data.DepartmentID,
                FK_EmployeeFrom = (data.EmployeeID.HasValue) ? data.EmployeeID.Value : 0,
                STRequest = data.ModeTR,
                TransDate = Convert.ToDateTime(data.TransDate),
                FK_BranchTo = data.BranchIDTo,
                FK_DepartmentTo = data.DepartmentIDTo,
                FK_EmployeeTo = (data.EmployeeIDTo.HasValue) ? data.EmployeeIDTo.Value : 0,

                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_StockRequest = (data.FK_StockTransfer.HasValue) ? data.FK_StockTransfer.Value : 0,
                ID_StockTransfer = 0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                EmployeeStockTransferDetails = data.SubEmployeeStockTransfers is null ? "" : Common.xmlTostring(data.SubEmployeeStockTransfers),

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateStocktransfer(EmployeeStockTransferModel.EmployeeStockTransferView data)
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

            
            EmployeeStockTransferModel employeeStocktrnf = new EmployeeStockTransferModel();
            var datresponse = employeeStocktrnf.UpdateEmployeeStockTransferData(input: new EmployeeStockTransferModel.UpdateEmployeeStockTransfer
            {
                UserAction = 2,
                FK_BranchFrom = data.BranchID,
                FK_DepartmentFrom = data.DepartmentID,
                FK_EmployeeFrom = (data.EmployeeID.HasValue) ? data.EmployeeID.Value : 0,
         
                
                TransDate = Convert.ToDateTime(data.TransDate),
                FK_BranchTo = data.BranchIDTo,
                FK_DepartmentTo = data.DepartmentIDTo,
                FK_EmployeeTo =(data.EmployeeIDTo.HasValue) ? data.EmployeeIDTo.Value : 0,
                STRequest = data.ModeTR,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                ID_StockTransfer = data.StockTransferID,
                FK_StockRequest= (data.FK_StockTransfer.HasValue) ? data.FK_StockTransfer.Value : 0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                EmployeeStockTransferDetails = data.SubEmployeeStockTransfers is null ? "" : Common.xmlTostring(data.SubEmployeeStockTransfers),

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetEmployeeStockTransferList(int pageSize, int pageIndex, string Name, string TransModes)
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

            ModelState.Remove("ReasonID");

            if (!ModelState.IsValid)
            {
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
            EmployeeStockTransferModel employeeStock = new EmployeeStockTransferModel();

            var EmployeetransferInfo = employeeStock.GetEmployeeStockTransferData(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeStockTransferModel.EmployeeStockTransferID

            {
                FK_StockTransfer = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //Search= Name,
                TransMode = TransModes
            });

            //  return Json(EmployeetransferInfo, JsonRequestBehavior.AllowGet);
            return Json(new { EmployeetransferInfo.Process, EmployeetransferInfo.Data, pageSize, pageIndex, totalrecord = (EmployeetransferInfo.Data is null) ? 0 : EmployeetransferInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]

        public ActionResult GetEmployeeStockTransferInfo(EmployeeStockTransferModel.EmployeeStockTransferView data)
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
            //removing a node in model while validating
            //--- Model validation 
            if (!ModelState.IsValid)
            {

                // since no need to continue just return
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

            #endregion :: Model validation  ::


            EmployeeStockTransferModel objprd = new EmployeeStockTransferModel();

            var mptableInfo = objprd.GetEmployeeStockTransferData(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeStockTransferModel.EmployeeStockTransferID
            {
                FK_StockTransfer = data.StockTransferID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode
            });

            var subtable = objprd.GetSubTableEmployeeStockTransferData(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeStockTransferModel.EmployeeStockTransferSubSelect
            {
                FK_StockTransfer = data.StockTransferID,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].SubEmployeeStockTransfers = subtable.Data;
            }

            return Json(mptableInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteEmployeeStocktransfer(EmployeeStockTransferModel.EmployeeStockTransferInfoView data)
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

            if (!ModelState.IsValid)
            {
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

            EmployeeStockTransferModel employeeStock = new EmployeeStockTransferModel();


            Output datresponse = employeeStock.DeleteEmployeeStockTransferData(input: new EmployeeStockTransferModel.DeleteEmployeeStockTransfer
            {
                FK_StockTransfer = data.StockTransferID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = "",
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetEmployeeStockTransferReasonList()
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

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStockTransferRequestUnit(EmployeeStockTransferModel.ProductWiseUnitVue data)
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

            EmployeeStockTransferModel Empstock = new EmployeeStockTransferModel();


            var Data = Empstock.GetMultiunit(input: new EmployeeStockTransferModel.ProductWiseUnit
            {

                Mode = "P",
                TransMode = data.TransMode,
                ProductID = data.ProductID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Transaction = 0,
                SalesReturnMode = 0,

            }, companyKey: _userLoginInfo.CompanyKey);

            //return Json(new { MuultipleUnitList }, JsonRequestBehavior.AllowGet);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
    }
}

