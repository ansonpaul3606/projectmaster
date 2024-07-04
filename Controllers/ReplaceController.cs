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
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    public class ReplaceController : Controller
    {
        // GET: Replace
        public ActionResult Replace(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return View();
        }

        [HttpGet]
        public ActionResult LoadReplaceForm(string mtd)
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

            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;

            ShortageMarkingModel.ShortageMarkingView list = new ShortageMarkingModel.ShortageMarkingView();
            ShortageMarkingModel Shortage = new ShortageMarkingModel();

            var catglist = Common.GetDataViaQuery<ShortageMarkingModel.Categorydata>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = " ID_Category AS ID_Category,CatName AS CategoryName",
                Criteria = "Cancelled=0  AND Passed=1 AND Project=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            list.CategoryList = catglist.Data;

            APIParameters apiParaUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "ID_Unit AS ID_Unit,UnitName AS UnitName,NoOfUnits AS UnitCount",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var unit = Common.GetDataViaQuery<ShortageMarkingModel.Unit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaUnit);

            list.UnitList = unit.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.mtd = mtd;
            return PartialView("_AddReplaceForm", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPurchaseReturnInfo(ReplaceModel.PurchaseReturnView Data)
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

            ReplaceModel purchasereturn = new ReplaceModel();

            var PrRetProducts = purchasereturn.GetPurchaseReturnProductData(companyKey: _userLoginInfo.CompanyKey, input: new ReplaceModel.GetPurchaseReturn
            {
                FK_PurchaseReturn = Data.ID_PurchaseReturn,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Details = 1,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            List<ReplaceModel.PurchaseReturnDetails> objList = new List<ReplaceModel.PurchaseReturnDetails>();
            if(PrRetProducts.Data!=null)
            {
                foreach(var row in PrRetProducts.Data)
                {
                    ReplaceModel.PurchaseReturnDetails obj =row;
                    obj.NetPurchase = Convert.ToString(Math.Round(Convert.ToDecimal(obj.NetPurchase), 2));
                    obj.TaxAmount = Convert.ToString(Math.Round(Convert.ToDecimal(obj.TaxAmount), 2));
                    obj.PpdRate = Convert.ToString(Math.Round(Convert.ToDecimal(obj.PpdRate), 2));
                    objList.Add(obj);
                }
            }
            return Json(new { objList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaxAmountNew(ReplaceModel.BindTaxAmountNew Data)
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

            ReplaceModel replaceModel = new ReplaceModel();

            var datresponse = replaceModel.FillTaxNew(input: new ReplaceModel.BindTaxAmountNew
            {
                Amount = Data.Amount,
                Includetax = Data.Includetax,
                FK_Product = Data.FK_Product,

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetProductUnit(int ProductID)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<ReplaceModel.Unit>(parameters: new APIParameters
            {
                TableName = "Product P LEFT JOIN Unit U ON U.ID_Unit = P.FK_Unit",
                SelectFields = "U.ID_Unit AS ID_Unit,U.UnitName AS UnitName,U.NoOfUnits AS UnitCount",
                Criteria = "P.Cancelled =0 AND P.Passed=1 AND P.ID_Product=" + ProductID + " AND P.FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddReplace(ReplaceModel.ReplaceInput data)
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

            
            ReplaceModel replace = new ReplaceModel();
            
            var datresponse = replace.UpdateReplaceData(input: new ReplaceModel.UpdateReplace
            {
                UserAction = 1,                
                TransMode = data.TransMode,
                FK_Supplier = data.FK_Supplier,
                RepDate = data.TransDate,
                FK_PurchaseReturn = data.FK_PurchaseReturn,                
                RepIsPurchaseReturn = data.PurchaseReturn,
                //To Section
                RepFromTotal = data.RepFromTotal,
                RepFromRoundOff = data.RepFromRoundOff,
                RepFromNetAmount = data.RepFromNetAmount,
                ReplaceFromDetails = data.ReplaceFromDetails is null ? "" : Common.xmlTostring(data.ReplaceFromDetails),
                RepFTaxDetails = data.RepFTaxDetails is null ? "" : Common.xmlTostring(data.RepFTaxDetails),
                //From Section
                RepToTotal = data.RepToTotal,
                RepToDiscountAmount = data.RepToDiscountAmount,
                RepToDiscountPer = data.RepToDiscountPer,
                RepToRoundOff = data.RepToRoundOff,
                RepToNetAmount = data.RepToNetAmount,
                ReplaceToDetails = data.ReplaceToDetails is null ? "" : Common.xmlTostring(data.ReplaceToDetails),
                RepTTaxDetails = data.RepTTaxDetails is null ? "" : Common.xmlTostring(data.RepTTaxDetails),                
                //Keys
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = _userLoginInfo.FK_Department,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                //ReplaceID
                ID_Replace = 0,                
                //LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetReplaceInfo(ReplaceModel.GetReplaceByID data)
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

            ReplaceModel replace = new ReplaceModel();

            var Info = replace.GetReplaceSelect(companyKey: _userLoginInfo.CompanyKey, input: new ReplaceModel.GetReplace
            {
                FK_ReplaceMaster = data.ID_Replace,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                Details = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            var RepFromInfo = replace.GetReplaceFromSelect(companyKey: _userLoginInfo.CompanyKey, input: new ReplaceModel.GetReplace
            {
                FK_ReplaceMaster = data.ID_Replace,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                Details = 1,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });

            var RepToInfo = replace.GetReplaceToSelect(companyKey: _userLoginInfo.CompanyKey, input: new ReplaceModel.GetReplace
            {
                FK_ReplaceMaster = data.ID_Replace,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                Details = 2,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });

            var PTaxDetails = replace.GetTaxDetails(companyKey: _userLoginInfo.CompanyKey, input: new ReplaceModel.GetReplaceTax
            {
                ID_Transaction = data.ID_Replace,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            List<ReplaceModel.TaxListData> ReplaceToTax = new List<ReplaceModel.TaxListData>();
            List<ReplaceModel.TaxListData> ReplaceFromTax = new List<ReplaceModel.TaxListData>();
            if (PTaxDetails.Data != null)
            {
                ReplaceToTax = PTaxDetails.Data.Where(item => item.TransType == 1).ToList();
                ReplaceFromTax = PTaxDetails.Data.Where(item => item.TransType == 2).ToList();
            }
            

            return Json(new { Info, RepFromInfo, RepToInfo, ReplaceToTax, ReplaceFromTax }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetReplaceList(int pageSize, int pageIndex, string Name, string TransModes)
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

            string transMode = "";

            ReplaceModel replace = new ReplaceModel();

            var Info = replace.GetReplaceSelect(companyKey: _userLoginInfo.CompanyKey, input: new ReplaceModel.GetReplace
            {
                FK_ReplaceMaster = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransModes,
                Details = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });
            // var PurDetailsInfo=
            return Json(new { Info.Process, Info.Data, pageSize, pageIndex, totalrecord = (Info.Data is null) ? 0 : Info.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReplaceReasonList()
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

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""
            });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteReplace(ReplaceModel.ReplaceDeleteInput data)
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


            
            ReplaceModel replace = new ReplaceModel();

            var datresponse = replace.DeleteReplaceData(input: new ReplaceModel.DeleteReplace
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_Replacemaster = data.ID_Replace,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}