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
    public class GoodsTrasferMethodController : Controller
    {
        // GET: GoodsTrasferMethod
        public ActionResult GoodsTrasferMethod()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadGoodsTrasferMethodForm()
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            GoodsTrasferMethodModel.Locationlist dis = new GoodsTrasferMethodModel.Locationlist();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProGetNextNo",
                parameter: new NextSortOrder
                {
                    TableName = "goodstransfermethod",
                    FieldName = "SortOrder",
                    Debug = 0
                });

            dis.Sort = a.Data[0].NextNo;

            return PartialView("_AddGoodsTransferMethod", dis);
        }

        [HttpPost]
        public ActionResult GetGoodsTransferList(int pageSize, int pageIndex, string Name)
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

            string transMode = "";
            GoodsTrasferMethodModel country = new GoodsTrasferMethodModel();

            var data = country.GetGoodsTransferData(companyKey: _userLoginInfo.CompanyKey, input: new GoodsTrasferMethodModel.SelectGoodstransfer
            {
                FK_GoodsTransferMethod = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });            
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewGoodsTransfer(GoodsTrasferMethodModel.GoodsTrasferInputView data)
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

            GoodsTrasferMethodModel country = new GoodsTrasferMethodModel();
            var datresponse = country.UpdateGoodsTransferData(input: new GoodsTrasferMethodModel.GoodsTransferUpdate
            {
                UserAction = 1,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                GoodsTransName = data.GoodsTransName,
                GoodsTransShortName = data.GoodsTransShortName,
                ID_GoodsTransferMethod = 0,
                SortOrder = data.Sort,
            }, companyKey: _userLoginInfo.CompanyKey);



            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateGoodsTransfer(GoodsTrasferMethodModel.GoodsTrasferInputView data)
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

            GoodsTrasferMethodModel country = new GoodsTrasferMethodModel();
            var datresponse = country.UpdateGoodsTransferData(input: new GoodsTrasferMethodModel.GoodsTransferUpdate
            {
                UserAction = 2,
                ID_GoodsTransferMethod = data.ID_GoodsTransferMethod,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                GoodsTransName = data.GoodsTransName,
                GoodsTransShortName = data.GoodsTransShortName,
                SortOrder = data.Sort,
            }, companyKey: _userLoginInfo.CompanyKey);



            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetGoodsTransferInfo(GoodsTrasferMethodModel.SelectGoodstransfer data)
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

            GoodsTrasferMethodModel goodstransfer = new GoodsTrasferMethodModel();
            var countryInfo = goodstransfer.GetGoodsTransferData(companyKey: _userLoginInfo.CompanyKey, input: new GoodsTrasferMethodModel.SelectGoodstransfer
            { FK_GoodsTransferMethod = data.FK_GoodsTransferMethod,
              FK_Company = _userLoginInfo.FK_Company,
              FK_Machine = _userLoginInfo.FK_Machine,
              FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
              TransMode = data.TransMode,
              EntrBy = _userLoginInfo.EntrBy });

            return Json(countryInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGoodsTransferReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID {
                FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = "" });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult DeleteGoodsTrasferMethodInfo(GoodsTrasferMethodModel.GoodsTrasferInputView data)
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


            GoodsTrasferMethodModel.DeleteGoodstransfer country = new GoodsTrasferMethodModel.DeleteGoodstransfer
            {
                FK_GoodsTransferMethod = data.ID_GoodsTransferMethod,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""

            };
            Output dataresponse = Common.UpdateTableData<GoodsTrasferMethodModel.DeleteGoodstransfer>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProGoodsTransferMethodDelete", parameter: country);
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}