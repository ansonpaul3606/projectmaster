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
using System.Configuration;
using System.IO;

namespace PerfectWebERP.Controllers
{
    public class BackupSettingController : Controller
    {
        // GET: BackupSetting
        public ActionResult Index(string mtd)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }
        public ActionResult LoadBackupsettingsForm(string mtd)
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            BackupSettingModel.BackupSettingModelList List = new BackupSettingModel.BackupSettingModelList();
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "BackupSettings",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            List.SortOrder = a.Data[0].NextNo;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddBackupSettingView", List);
        }
        #region
        //[HttpPost]
        //public ActionResult Uploaddirectoryfolder(BackupSettingModel.Viewdocpath folderPath)
        //{


        //    string UploadPath = ConfigurationManager.AppSettings["DataUpload"].ToString();
        //    string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        //    if (folderPath.Docs != null && folderPath.Docs.ContentLength > 0)
        //    {
        //        string FileName = Path.GetFileNameWithoutExtension(folderPath.Docs.FileName);
        //        string Filextension = Path.GetExtension(folderPath.Docs.FileName);
        //        FileName = Guid.NewGuid().ToString() + Filextension;// + "-" + FileName.Replace(" ", "").Trim()

        //        if (!Directory.Exists(CurrentDirectory + UploadPath))
        //        {
        //            Directory.CreateDirectory(CurrentDirectory + UploadPath);
        //        }
        //        folderPath.Path = FileName;// CurrentDirectory + UploadPath + FileName;
        //    }
        //    else
        //    {
        //        folderPath.Path = folderPath.EditDocPath;

        //    }

        //    return View(folderPath);


        #endregion

        //}


        [HttpPost]
        public ActionResult AddBackupSettings(BackupSettingModel.BackupSettingModelView inputdata)
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
            BackupSettingModel objfld = new BackupSettingModel();
          


            var data = objfld.AddBOMProject(input: new BackupSettingModel.UpdateBackupSetting
            {
                UserAction = 1,
                ID_BackupSettings = 0,//insert

                BSBackupType = inputdata.BSBackupType,
                BSBackupName = inputdata.BSBackupName,
                BSBackupPath = inputdata.BSBackupPath,
                BSKeepOldCopy = inputdata.BSKeepOldCopy,
                BSSortOrder = inputdata.BSSortOrder,
                BSOverwrite = inputdata.BSOverwrite,
                BSActive = inputdata.BSActive,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,



            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateBackupSettings(BackupSettingModel.BackupSettingModelView inputdata)
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
            BackupSettingModel objfld = new BackupSettingModel();



            var data = objfld.AddBOMProject(input: new BackupSettingModel.UpdateBackupSetting
            {
                UserAction = 2,
                ID_BackupSettings = inputdata.ID_BackupSettings,
                BSBackupType = inputdata.BSBackupType,
                BSBackupName = inputdata.BSBackupName,
                BSBackupPath = inputdata.BSBackupPath,
                BSKeepOldCopy = inputdata.BSKeepOldCopy,
                BSSortOrder = inputdata.BSSortOrder,
                BSOverwrite = inputdata.BSOverwrite,
                BSActive = inputdata.BSActive,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,




            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetBackupSettingList(int pageSize, int pageIndex, string Name, string TransModes)
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
            BackupSettingModel priority = new BackupSettingModel();

            var BackupSettingInfo = priority.GetBOMProjectlistviewData(companyKey: _userLoginInfo.CompanyKey, input: new BackupSettingModel.BackupSettingViewID

            {
                FK_BackupSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,

                TransMode = TransModes

            });

            return Json(new{ BackupSettingInfo.Process,  BackupSettingInfo.Data, pageSize, pageIndex, totalrecord = (BackupSettingInfo.Data is null) ? 0 : BackupSettingInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult GetBackupSettingInfoByID(BackupSettingModel.BackupSettingModelView data)
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


            BackupSettingModel Model = new BackupSettingModel();

            var BackupSettingInfo = Model.GetBOMProjectlistviewData(companyKey: _userLoginInfo.CompanyKey, input: new BackupSettingModel.BackupSettingViewID

            {
                FK_BackupSettings = data.ID_BackupSettings,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
               

            });


            return Json(BackupSettingInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBackupSettingsReasonList()
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
      
       
        public ActionResult DeleteBackupSettings(BackupSettingModel.BackupSettingModelView data)
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

            BackupSettingModel BOMPrt = new BackupSettingModel();

            var datresponse = BOMPrt.DeleteBackupSettingData(input: new BackupSettingModel.DeleteBackupSetting
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_BackupSettings = data.ID_BackupSettings,
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