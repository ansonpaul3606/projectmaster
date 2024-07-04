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
    [CheckSessionTimeOut]
    public class MediaMasterController : Controller
    {
        // GET: MediaMaster
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

        public ActionResult LoadMediaMasterForm()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            MediaMasterModel.MediaMasterListModel mediaMasterList = new MediaMasterModel.MediaMasterListModel();  //<-- Data to sened to front end form(front end variable)
            
            //<get model master list
            var mediaMasterModelist = Common.GetDataViaQuery<MediaMasterModel.MediaMaster>(parameters: new APIParameters
            {
                TableName = "MediaType",
                SelectFields = "ID_MediaType  AS MediaTypeID,MediaTypeName AS MediaTypeName",
                Criteria = "",
                SortFields = "ID_MediaType",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey

              );



            mediaMasterList.MediaTypeList = mediaMasterModelist.Data;//saving model master list to front end variable

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "MediaMaster",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            mediaMasterList.SortOrder = a.Data[0].NextNo;




            return PartialView("_AddMediaMasterForm", mediaMasterList);
        }



        //[HttpGet]
        //public ActionResult GetMediaMasterLists(int pageSize, int pageIndex, string Name)
        //{
        //    #region ::  Check User Session to verifyLogin  ::

        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion ::  Check User Session to verifyLogin  ::

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    //   var data = Common.GetDataViaQuery<ReasonModel.ReasonsView>(parameters: new APIParameters
        //    //   {
        //    //       TableName = "Reason AS R JOIN ReasonMode AS RM ON RM.ID_ReasonMode=R.FK_ReasonMode",
        //    //       SelectFields = "R.[ID_Reason] AS ReasonID,R.[ResnName] AS Reason,R.[ResnShortName] AS ShortName,RM.[ReasonModeName] AS ModeName,R.[SortOrder] AS Sort",
        //    //       Criteria = "R.Cancelled=0 AND R.Passed=1",
        //    //       SortFields = "R.SortOrder",
        //    //       GroupByFileds = ""
        //    //   },
        //    // companyKey: _userLoginInfo.CompanyKey
        //    //);

        //    MediaMasterModel mediaMaster = new MediaMasterModel();
        //    var data = mediaMaster.GetMediaMasterData(input: new MediaMasterModel.InputMediaMasterID {
        //        ID_MediaMaster = 0,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        EntrBy = _userLoginInfo.EntrBy }, 
        //        companyKey: _userLoginInfo.CompanyKey);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult GetMediaMasterList(int pageSize, int pageIndex, string Name)
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


            MediaMasterModel mediaMaster = new MediaMasterModel();
            var data = mediaMaster.GetMediaMasterData(input: new MediaMasterModel.InputMediaMasterID
            {
                ID_MediaMaster = 0,
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
        public ActionResult GetMediaMasterInfoByID(MediaMasterModel.MediaMasterInfoView data)
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

            MediaMasterModel mediaMaster = new MediaMasterModel();
            var mediaMasterInfo = mediaMaster.GetMediaMasterData(companyKey: _userLoginInfo.CompanyKey, input: new MediaMasterModel.InputMediaMasterID { ID_MediaMaster = data.MediaMasterID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            return Json(mediaMasterInfo, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewMediaMasterDetails(MediaMasterModel.MediaMasterInputFromViewModel data)
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

            MediaMasterModel mediaMaster = new MediaMasterModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = mediaMaster.UpdateMediaMasterData(input: new MediaMasterModel.UpdateMediaMaster
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,

                FK_Company = _userLoginInfo.FK_Company,

                EntrBy = entrBy,

                TransMode = data.TransMode,
                Debug = 0,


                ID_MediaMaster = 0,
                MdaName = data.MediaName,
                MdaShortName = data.MediaShortName,
                FK_MediaType = data.MediaTypeID,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMediaMasterDetails(MediaMasterModel.MediaMasterInputFromViewModel data)
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

            MediaMasterModel mediaMaster = new MediaMasterModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = mediaMaster.UpdateMediaMasterData(input: new MediaMasterModel.UpdateMediaMaster
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,

                FK_Company = _userLoginInfo.FK_Company,

                EntrBy = entrBy,

                TransMode = data.TransMode,
                Debug = 0,


                ID_MediaMaster = data.MediaMasterID,
                MdaName = data.MediaName,
                MdaShortName = data.MediaShortName,
                FK_MediaType = data.MediaTypeID,
                SortOrder = data.SortOrder,
                
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMediaMasterInfo(MediaMasterModel.MediaMasterInputFromViewModel data)
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

            ModelState.Remove("MediaName");
            ModelState.Remove("MediaShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("MediaTypeID");
            ModelState.Remove("MediaTypeName");

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

            MediaMasterModel.DeleteMediaMaster mediaMaster = new MediaMasterModel.DeleteMediaMaster
            {
                ID_MediaMaster = data.MediaMasterID,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID,
            };

            Output datresponse = Common.UpdateTableData<MediaMasterModel.DeleteMediaMaster>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proMediaMasterDelete", parameter: mediaMaster);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMediaMasterReasonList()
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


    }
}