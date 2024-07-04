using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
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
    public class MediaPromotionController : Controller
    {
        // GET: MediaPromotion
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.mtd = mtd;
            return View();
        }

        public ActionResult MediaPromotionView(string mtd)
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
            Mediapromotion.MediaPromotionList Promlist = new Mediapromotion.MediaPromotionList();
            var SubMe = Common.GetDataViaQuery<Mediapromotion.SubMediaList>(parameters: new APIParameters
            {
                TableName = "MediaSubMaster",
                SelectFields = "ID_MediaSubMaster as FK_MediaSubMaster,SubMdaName as SubMediaName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey
           );
            Promlist.SubMediaList = SubMe.Data;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
          );
            Promlist.PaymentView = PaymentView.Data;

            var Media = Common.GetDataViaQuery<Mediapromotion.MediaList>(parameters: new APIParameters
            {
                TableName = "MediaMaster",
                SelectFields = "ID_MediaMaster AS FK_MediaMaster ,MdaName AS MediaName",
                Criteria = "Cancelled=0 AND Passed=1 AND  FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_MediaMaster",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey

     );
            Promlist.MediaList = Media.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddMediaPromotion", Promlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddMedia(Mediapromotion.MediaView data)
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


            Mediapromotion brandmodel = new Mediapromotion();
            var datresponse = brandmodel.UpdateMediaData(input: new Mediapromotion.UpdateMedia
            {
                UserAction = 1,
                Debug = 0,
                TransMode = "",
                ID_Media = data.ID_Media,
                PrmName = data.PrmName,
                PrmStartDate = data.PrmStartDate,
                FK_MediaSubMaster = data.FK_MediaSubMaster,
                PrmEndDate = data.PrmEndDate,
                FK_MediaMaster = data.FK_MediaMaster,
                PrmAmount = data.PrmAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PrmDescription=data.PrmDescription,
                FK_Media =data.FK_Media,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail)
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateMedia(Mediapromotion.MediaView data)
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


            Mediapromotion brandmodel = new Mediapromotion();
            var datresponse = brandmodel.UpdateMediaData(input: new Mediapromotion.UpdateMedia
            {
                UserAction = 2,
                Debug = 0,
                TransMode = "",
                ID_Media = data.ID_Media,
                PrmName = data.PrmName,
                PrmStartDate = data.PrmStartDate,
                FK_MediaSubMaster = data.FK_MediaSubMaster,
                PrmEndDate = data.PrmEndDate,
                FK_MediaMaster = data.FK_MediaMaster,
                PrmAmount = data.PrmAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PrmDescription = data.PrmDescription,
                FK_Media = data.FK_Media,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail)
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetMediaList(int pageSize, int pageIndex, string Name)
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

            Mediapromotion medi = new Mediapromotion();
            var MakInfo = medi.GetMediaSelect(companyKey: _userLoginInfo.CompanyKey, input: new Mediapromotion.GetMediaDetails
            {
                FK_Media = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            });

            return Json(new { MakInfo.Process, MakInfo.Data, pageSize, pageIndex, totalrecord = (MakInfo.Data is null) ? 0 : MakInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMediaInfo(Mediapromotion.MediaView data)
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

            Mediapromotion brand = new Mediapromotion();

            var modelInfo = brand.GetMediaSelectData(companyKey: _userLoginInfo.CompanyKey, input: new Mediapromotion.GetMediabyIdDetails
            {
                FK_Media = data.ID_Media,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,

            });
            var paymentdetail = brand.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new Mediapromotion.GetPaymentin
            {
                FK_Master = data.ID_Media,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
           
            return Json(new { modelInfo, paymentdetail }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMediaDeleteReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID
            {
                FK_Reason = 0,
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

        public ActionResult DeleteMedia(Mediapromotion.DeleteView data)
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

            Mediapromotion brands = new Mediapromotion();



            Output datresponse = brands.DeleteMediaData(input: new Mediapromotion.DeleteMedia
            {
                FK_Media = data.ID_Media,
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
        public ActionResult GetSubmediamaster(Int64 FK_MediaMaster)
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

            var data = Common.GetDataViaQuery<Mediapromotion.SubMediaList>(parameters: new APIParameters
            {
                TableName = "[MediaSubMaster]",
                SelectFields = "[ID_MediaSubMaster] AS FK_MediaSubMaster,[SubMdaName] AS SubMediaName",
                Criteria = "Passed=1 And Cancelled=0 And FK_Media ='" + FK_MediaMaster + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
      

    }
}




   
      
    

        

        

        
