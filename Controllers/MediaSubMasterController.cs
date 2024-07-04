using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System.Data;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Filters;


namespace PerfectWebERP.Controllers
{
    public class MediaSubMasterController : Controller
    {
        // GET: MediaSubMaster
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }
        public ActionResult LoadMediaSubMaster()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            MediaSubMasterModel.MediaSubMasterListModel mediaSubMasterList = new MediaSubMasterModel.MediaSubMasterListModel();  //<-- Data to sened to front end form(front end variable)

            //<get model master list
            var MediaMasterList = Common.GetDataViaQuery<MediaSubMasterModel.MediaMaster>(parameters: new APIParameters
            {
                TableName = "MediaMaster",
                SelectFields = "ID_MediaMaster  AS MediaMasterID,MdaName AS MediaMasterName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "MediaMasterID",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey

              );

            mediaSubMasterList.MediaMasterList = MediaMasterList.Data; //saving model master list to front end variable

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "MediaSubMaster",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            mediaSubMasterList.SortOrder = a.Data[0].NextNo;


            return PartialView("_AddMediaSubMaster", mediaSubMasterList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubMediaMaster(MediaSubMasterModel.MediaSubMasterInputFromViewModel data)
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
            ModelState.Remove("MediaMasterID");
            ModelState.Remove("MediaTypeID");

            ModelState.Remove("ReasonID");
            ModelState.Remove("SortOrder");

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

            MediaSubMasterModel mediaSubMaster = new MediaSubMasterModel();


            byte userAction = 1;//update : 2 | Add : 1 
            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
          

            var dataresponse = mediaSubMaster.UpdateMediaSubMasterData(input: new MediaSubMasterModel.UpdateMediaSubMaster
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,
                ID_MediaSubMaster = 0,
                SubMdaName = data.SubMediaName,
                SubMdaShortName = data.SubMediaShortName,
                FK_Media = data.FK_Media,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updateSubMediaMaster(MediaSubMasterModel.MediaSubMasterInputFromViewModel data)
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

            ModelState.Remove("MediaTypeID");

            ModelState.Remove("ReasonID");
            ModelState.Remove("SortOrder");
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

            MediaSubMasterModel mediaSubMaster = new MediaSubMasterModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = mediaSubMaster.UpdateMediaSubMasterData(input: new MediaSubMasterModel.UpdateMediaSubMaster
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,

                FK_Company = _userLoginInfo.FK_Company,

                EntrBy = entrBy,

                TransMode = data.TransMode,
                Debug = 0,


                ID_MediaSubMaster = data.ID_MediaSubMaster,
                SubMdaName = data.SubMediaName,
                SubMdaShortName = data.SubMediaShortName,
                FK_Media = data.FK_Media,
                SortOrder = data.SortOrder,

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetMediaSubMasterList(int pageSize, int pageIndex, string Name)
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


            MediaSubMasterModel mediaSubMaster = new MediaSubMasterModel();
            var data = mediaSubMaster.GetMediaSubMasterData(input: new MediaSubMasterModel.InputMediaSubMasterID
            {
                ID_MediaSubMaster = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name
            },
                companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetMediaSubMasterInfoByID(MediaSubMasterModel.MediaSubMasterInfoView data)
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

            MediaSubMasterModel mediaSubMaster = new MediaSubMasterModel();
            var mediaMasterInfo = mediaSubMaster.GetMediaSubMasterData(companyKey: _userLoginInfo.CompanyKey, input: new MediaSubMasterModel.InputMediaSubMasterID { ID_MediaSubMaster = data.MediaSubMasterID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            return Json(mediaMasterInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMediaSubMasterInfo(MediaSubMasterModel.MediaSubMasterInputFromViewModel data)
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

            MediaSubMasterModel.DeleteMediaSubMaster mediaSubMaster = new MediaSubMasterModel.DeleteMediaSubMaster
            {
                ID_MediaSubMaster = data.ID_MediaSubMaster,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID,
            };

            Output datresponse = Common.UpdateTableData<MediaSubMasterModel.DeleteMediaSubMaster>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proMediaSubMasterDelete", parameter: mediaSubMaster);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMediaSubMasterReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMediaList()
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
            MediaSubMasterModel.MediaSubMasterListModel mediaSubMasterList = new MediaSubMasterModel.MediaSubMasterListModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<MediaSubMasterModel.MediaMaster>(parameters: new APIParameters
            {
                TableName = "MediaMaster",
                SelectFields = "ID_MediaMaster  AS MediaMasterID,MdaName AS MediaMasterName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "MediaMasterID",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}