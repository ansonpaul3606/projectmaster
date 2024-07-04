/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 25/11/2022
Purpose		: ProjectStages for Production
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
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ProductionStageController : Controller
    {


        // GET: ProjectStages
        public ActionResult Index(string mgrp)
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
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadProjectStagesForm()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ProjectStagesModel.ProjectStagesListModel MUList = new ProjectStagesModel.ProjectStagesListModel();



            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "ProjectStages",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            MUList.SortOrder = a.Data[0].NextNo;
            ViewBag.NextSortOrder = a.Data[0].NextNo;
            ProjectStagesModel.ProjectStagesListModel Prd = new ProjectStagesModel.ProjectStagesListModel();

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Project=0 AND Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Category = Common.GetDataViaQuery<ProjectStagesModel.Category>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            MUList.CategoryList = Category.Data;


            return PartialView("_AddProductionStages", MUList);
        }
        public ActionResult GetSubcategory(ProductModel.Category cr)
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
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::
            APIParameters apiSub = new APIParameters
            {
                TableName = "[SubCategory]",
                SelectFields = "[ID_SubCategory] AS ID_SubCategory,[SubCatName] AS SubCatName",
                Criteria = "Passed=1 And Cancelled=0 And FK_Category ='" + cr.CategoryID + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<ProductModel.Subcategory>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }


        //public ActionResult GetProjectStagesList()
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


        //    ProjectStagesModel ProjectStages = new ProjectStagesModel();
        //    var data = ProjectStages.GetProjectStagesData(input: new ProjectStagesModel.InputProjectStagesID { FK_ProjectStages = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0 }, companyKey: _userLoginInfo.CompanyKey);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public ActionResult GetProjectStagesList(int pageSize, int pageIndex, string Name, String TransMode)
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


            ProjectStagesModel ProjectStages = new ProjectStagesModel();
            var data = ProjectStages.GetProjectStagesData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectStagesModel.InputProjectStagesID
            {
                FK_ProjectStages = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode
            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProjectStagesInfoByID(ProjectStagesModel.ProjectStagesInfoView data)
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

            ProjectStagesModel measurement = new ProjectStagesModel();
            var measureInfo = measurement.GetProjectStagesData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectStagesModel.InputProjectStagesID
            {
                FK_ProjectStages = data.ProjectStagesID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode=data.TransMode
            });

            return Json(measureInfo, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProductionStageDetails(ProjectStagesModel.ProjectStagesInputFromViewModel data)
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
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::

            ProjectStagesModel measurement = new ProjectStagesModel();



            var dataresponse = measurement.UpdateProjectStagesData(input: new ProjectStagesModel.UpdateProjectStages
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_ProjectStages = 0,
                ID_ProjectStages = 0,
                PrStName = data.Name,
                PrStShortName = data.ShortName,
                FK_Category = data.Category,
                FK_SubCategory = data.SubCategory,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProductionStageDetails(ProjectStagesModel.ProjectStagesInputFromViewModel data)
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
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::

            ProjectStagesModel measurement = new ProjectStagesModel();



            var dataresponse = measurement.UpdateProjectStagesData(input: new ProjectStagesModel.UpdateProjectStages
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_ProjectStages = data.ProjectStagesID,
                ID_ProjectStages = data.ProjectStagesID,
                PrStName = data.Name,
                PrStShortName = data.ShortName,
                FK_Category = data.Category,
                FK_SubCategory = data.SubCategory,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteProjectStagesInfo(ProjectStagesModel.ProjectStagesInfoView data)
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

            ProjectStagesModel measurement = new ProjectStagesModel();
            var datresponse = measurement.DeleteProjectStagesData(input: new ProjectStagesModel.DeleteProjectStages { FK_ProjectStages = data.ProjectStagesID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProjectStagesDeleteReasonList()
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