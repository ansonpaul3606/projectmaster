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
    public class BranchtypeController : Controller
    {
        // GET: Branchtype
       

        public ActionResult BranchtypeIndex(string mtd)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 


            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.mtd = mtd;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }


        public ActionResult LoadFormBranchtype(string mtd)
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
          
            branchtypeModel.BranchViewList branchList = new branchtypeModel.BranchViewList();

            var BranchModeist = Common.GetDataViaQuery<branchtypeModel.BranchMode>(parameters: new APIParameters
            {
                TableName = "BranchMode",
                SelectFields = "ID_BranchMode  AS BranchModeID,BranchMode AS BranchModeName",
                Criteria = "",
                SortFields = "ID_BranchMode",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey

               );


            branchList.BranchModeList = BranchModeist.Data;

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "BranchType",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            branchList.SortOrder = a.Data[0].NextNo;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddBranchtype", branchList);
        }



        [HttpPost]
        public ActionResult GetBranchTypeList(int pageSize, int pageIndex, string Name)
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

            branchtypeModel branchtype = new branchtypeModel();
            var data = branchtype.GetbranchtypeData(companyKey: _userLoginInfo.CompanyKey, input: new branchtypeModel.InputbranchtypeID { ID_BranchType = 0, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy , PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });

           // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewBranchTypeDetails(branchtypeModel.branchtypeView data)
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

            ModelState.Remove("BranchtypeID");
            ModelState.Remove("BranchModeID");
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

            
            branchtypeModel branchtype = new branchtypeModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
           /// string userCode = _userLoginInfo.Us;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
          

            var dataresponse = branchtype.UpdatebranchtypeData(input: new branchtypeModel.Updatebranchtype
            {
               
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,

                ID_BranchType = 0,
                BTName = data.Name,
                BTShortName = data.ShortName,
                FK_BranchMode = data.BranchModeID,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBranchTypeDetails(branchtypeModel.branchtypeView data)
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

          
            ModelState.Remove("BranchModeID");
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

            branchtypeModel branchtype = new branchtypeModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
           // string userCode = _userLoginInfo.UserCode;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
          

            var dataresponse = branchtype.UpdatebranchtypeData(input: new branchtypeModel.Updatebranchtype
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,

                ID_BranchType = data.BranchtypeID,
                BTName = data.Name,
                BTShortName = data.ShortName,
                FK_BranchMode = data.BranchModeID,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetBranchtypeInfoByID(branchtypeModel.branchtypeView data)
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
            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("BranchModeID");
            ModelState.Remove("BranchMode");
            ModelState.Remove("ReasonID");


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

            branchtypeModel branchtype = new branchtypeModel();
            var reasonInfo = branchtype.GetbranchtypeData(companyKey: _userLoginInfo.CompanyKey, input: new branchtypeModel.InputbranchtypeID { ID_BranchType = data.BranchtypeID, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy  });

            return Json(reasonInfo, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken()]
        //delete branch type
        public ActionResult DeletebranchtypeInfo(branchtypeModel.BranchtypeInfoView data)
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

            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("BranchModeID");
            ModelState.Remove("BranchMode");

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

            branchtypeModel.Deletebranchtype branchtype = new branchtypeModel.Deletebranchtype
            {
                ID_BranchType = data.ID_BranchType,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<branchtypeModel.Deletebranchtype>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proBranchTypeDelete", parameter: branchtype);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetBranchtypeDeleteReasonList()
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