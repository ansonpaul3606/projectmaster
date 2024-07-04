using System;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace PerfectWebERP.Controllers
{
    public class BOMProjectController : Controller
    {
        // GET: BOMProject
        public ActionResult Index(string mtd, string mgrp)
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
            ViewBag.Fk_BranchCode = _userLoginInfo.FK_BranchCodeUser;

            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }

        public ActionResult LoadBOMProject(string mtd)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            BOMProjectModel obj = new BOMProjectModel();
            BOMProjectModel.BOMProjectModelview objview = new BOMProjectModel.BOMProjectModelview();
           

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Project=1 AND Passed=1 And CatProject=1 AND Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Category = Common.GetDataViaQuery<BOMProjectModel.Category>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            objview.CategoryList = Category.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddBOMProject", objview);
        }

        [HttpPost]
        public ActionResult AddBOMProject(BOMProjectModel.BOMProjectModelview inputdata)
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
            BOMProjectModel objfld = new BOMProjectModel();



            var data = objfld.AddBOMProject(input: new BOMProjectModel.UpdateBOMProject
            {
                UserAction = 1,
                ID_BOMProject = 0,//insert
                BOMName=inputdata.BOMName,
                FK_Category = inputdata.CategoryID,
               
                BOMDate = inputdata.EffectDate,
                BOMProjectProductDetails = inputdata.BOMProjectProductDetails is null ? "" : Common.xmlTostring(inputdata.BOMProjectProductDetails),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,



            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateBOMProject(BOMProjectModel.BOMProjectModelview inputdata)
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
            BOMProjectModel objfld = new BOMProjectModel();



            var data = objfld.AddBOMProject(input: new BOMProjectModel.UpdateBOMProject
            {
                UserAction = 2,
                ID_BOMProject = inputdata.ID_BOMProject,//insert
                BOMName = inputdata.BOMName,
                FK_Category = inputdata.CategoryID,

                BOMDate = inputdata.EffectDate,
                BOMProjectProductDetails = inputdata.BOMProjectProductDetails is null ? "" : Common.xmlTostring(inputdata.BOMProjectProductDetails),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,



            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetBOMProjectList(int pageSize, int pageIndex, string Name, string TransModes)
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
            BOMProjectModel priority = new BOMProjectModel();
         
            var BOMProjectInfo = priority.GetBOMProjectlistviewData(companyKey: _userLoginInfo.CompanyKey, input: new BOMProjectModel.BOMProjectViewID

            {
                FK_BOMProject = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,

                TransMode = TransModes

            });

          
            return Json(new { BOMProjectInfo.Process, BOMProjectInfo.Data, pageSize, pageIndex, totalrecord = (BOMProjectInfo.Data is null) ? 0 : BOMProjectInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public ActionResult GetBOMProjectInfoByID(BOMProjectModel.BOMProjectViewID data)
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


            BOMProjectModel BOMModel = new BOMProjectModel();

            var mptableInfo = BOMModel.GetBOMProjectlistviewData(companyKey: _userLoginInfo.CompanyKey, input: new BOMProjectModel.BOMProjectViewID
            {
                FK_BOMProject = data.FK_BOMProject,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
            });

            var subtable = BOMModel.GetProductDetailsSubtableData(companyKey: _userLoginInfo.CompanyKey, input: new BOMProjectModel.BOMProjectViewID {

                FK_BOMProject = data.FK_BOMProject,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].BOMProjectProductDetails = subtable.Data;
            }

            return Json(mptableInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBOMProjectReasonList()
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
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteBOMProject(BOMProjectModel.BOMProjectModelview data)
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

            BOMProjectModel BOMPrt = new BOMProjectModel();

            var datresponse = BOMPrt.DeleteBOMProjectData(input: new BOMProjectModel.DeleteBOMProject
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_BOMProject = data.ID_BOMProject,
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