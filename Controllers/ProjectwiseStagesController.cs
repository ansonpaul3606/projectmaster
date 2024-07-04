/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 02/08/2022
Purpose		: ProjectwiseStages
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
    public class ProjectwiseStagesController : Controller
    {


        // GET: ProjectwiseStages
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
            //ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadProjectwiseStagesForm()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ProjectwiseStagesModel.ProjectwiseStagesListModel SVList = new ProjectwiseStagesModel.ProjectwiseStagesListModel();

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[ProjectStages]",
                SelectFields = "[ID_ProjectStages] AS ProjectStagesID,[PrStName] AS StageName",
                Criteria = "Passed=1 And Cancelled=0 AND TransMode='PRST' AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Stages = Common.GetDataViaQuery<ProjectwiseStagesModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            SVList.StageList = Stages.Data;




            var durationTypeList = Common.GetDataViaProcedure<ProjectwiseStagesModel.DurationTypeList, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 25 });


            SVList.DurationTypeList = durationTypeList.Data;

            var WorkTypeList = Common.GetDataViaQuery<ProjectwiseStagesModel.WorkTypeList>(parameters: new APIParameters
            {
                TableName = "ExtraWorkType EWT",
                SelectFields = "EWT.ID_ExtwrkType AS WorkTypeID,EWT.EWTName AS[WorkType]",
                Criteria = "EWT.Cancelled=0 AND EWT.Passed=1 AND EWT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            SVList.WorkTypeList = WorkTypeList.Data;
            SVList.VisitDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            return PartialView("_AddProjectwiseStages", SVList);
        }
        public ActionResult GetProjectTeam(ProjectwiseStagesModel.ProjectwiseStagesView cr)
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
                TableName = "[ProjectTeam]",
                SelectFields = "[ID_ProjectTeam] AS ID_ProjectTeam,[ProjTeamName] AS TeamName",
                Criteria = "Passed=1 And Cancelled=0 And TransMode='PRTC' And FK_Project ='" + cr.FK_Project + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<ProjectwiseStagesModel.TeamList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetLeadGenInfo(LeadGenerateModel.LeadGenerateView LeadGenerateInfo)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::


            #endregion :: Model validation  ::


            LeadGenerateModel Lead = new LeadGenerateModel();
            ProjectwiseStagesModel ProjectwiseStages = new ProjectwiseStagesModel();
            var prInfo = Lead.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
            //var subproduct = ProjectwiseStages.GetSubProduct(companyKey: _userLoginInfo.CompanyKey, input: new ProjectwiseStagesModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            var subproduct = Lead.GetSubProductSite(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });


            return Json(new { subproduct, prInfo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetSiteVisitInfo(LeadGenerateModel.LeadGenerateView LeadGenerateInfo)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::


            #endregion :: Model validation  ::
            var ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID;

            var data = Common.GetDataViaQuery<SiteVisitModel.SiteVisitView>(parameters: new APIParameters
            {
                TableName = "SiteVisit SV",
                SelectFields = "SV.ID_SiteVisit as SiteVisitID",
                Criteria = "    SV.Cancelled=0 AND SV.Passed=1 AND SV.FK_Company=" + _userLoginInfo.FK_Company + " AND SV.FK_LeadGeneration=" + ID_LeadGenerate,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );
            long siteVisitId = 0;
            if (data.Data != null)
                siteVisitId = data.Data[0].SiteVisitID;

            SiteVisitModel SiteVisit = new SiteVisitModel();
            ;
            if (siteVisitId > 0)
            {
                var SiteVisitDetails = SiteVisit.GetSiteVisitData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = siteVisitId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


                var EmployeeDetails = SiteVisit.GetSiteVisitEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = siteVisitId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 1 });


                var MeasurementDetails = SiteVisit.GetSiteVisitMeasurementData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = siteVisitId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 2 });

                return Json(new { SiteVisitDetails, EmployeeDetails, MeasurementDetails }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetProjectStagePercentageSum(ProjectwiseStagesModel.ProjectwiseStagesView PrWsDt)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::


            #endregion :: Model validation  ::


            var data = Common.GetDataViaQuery<ProjectwiseStagesModel.ProjectwiseStagesView>(parameters: new APIParameters
            {
                TableName = "ProjectWiseStages",
                SelectFields = " sum(PWSProjPercent) as Percentage",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + " AND ProjectWiseStages.FK_Project=" + PrWsDt.FK_Project + "and ID_ProjectWiseStages!= " + PrWsDt.ProjectwiseStagesID,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );
            decimal TotPercentage = 0;
            if (data.Data != null)
                TotPercentage = data.Data[0].Percentage;

            return Json(TotPercentage, JsonRequestBehavior.AllowGet);



        }

        [HttpPost]
        public ActionResult GetProjectwiseStagesList(int pageSize, int pageIndex, string Name,string TransMode)
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

            ProjectwiseStagesModel ProjectwiseStages = new ProjectwiseStagesModel();
            var data = ProjectwiseStages.GetProjectwiseStagesData(input: new ProjectwiseStagesModel.InputProjectwiseStagesID
            {

                FK_ProjectwiseStages = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode

            }
            , companyKey: _userLoginInfo.CompanyKey);

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProjectwiseStagesInfoByID(ProjectwiseStagesModel.ProjectwiseStagesView data)
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

            ProjectwiseStagesModel ProjectwiseStages = new ProjectwiseStagesModel();
            var ProjectwiseStagesDetails = ProjectwiseStages.GetProjectwiseStagesData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectwiseStagesModel.InputProjectwiseStagesID { FK_ProjectwiseStages = data.ProjectwiseStagesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode});


            var MaterialDetails = ProjectwiseStages.GetProjectwiseStagesMaterialData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectwiseStagesModel.InputProjectwiseStagesID { FK_ProjectwiseStages = data.ProjectwiseStagesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode, CheckList = 2 });

            var WorkDetails = ProjectwiseStages.GetProjectwiseStagesWorkData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectwiseStagesModel.InputProjectwiseStagesID { FK_ProjectwiseStages = data.ProjectwiseStagesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode, CheckList = 3 });


            return Json(new { ProjectwiseStagesDetails, MaterialDetails, WorkDetails, }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadImageForm()
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
            CommonSearchPopupModel.Imagelist imgfld = new CommonSearchPopupModel.Imagelist();

            CommonSearchPopupModel objcmp = new CommonSearchPopupModel();

            //var imgmodelst = objcmp.GetImgModelist(input: new CommonSearchPopupModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
            var imgmodelst = Common.GetDataViaQuery<CommonSearchPopupModel.ImgMode>(parameters: new APIParameters
            {
                TableName = "DocumentType DT",
                SelectFields = "DT.ID_DocumentType as ID_Mode,DT.DocTName as ModeName",
                Criteria = "    DT.Cancelled=0 AND DT.Passed=1 AND DT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );

            imgfld.ImgModeList = imgmodelst.Data;


            var identityproof = Common.GetDataViaQuery<CommonSearchPopupModel.IdentityProof>(parameters: new APIParameters
            {
                TableName = "IdentityProof I",
                SelectFields = "I.ID_IdentityProof,I.IdName ",
                Criteria = "    I.Cancelled=0 AND I.Passed=1 AND I.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey

          );

            imgfld.IdentityProofs = identityproof.Data;
            return PartialView("Image", imgfld);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProjectwiseStagesDetails(ProjectwiseStagesModel.ProjectwiseStagesInputFromViewModel data)
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

            ProjectwiseStagesModel measurement = new ProjectwiseStagesModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = measurement.UpdateProjectwiseStagesData(input: new ProjectwiseStagesModel.UpdateProjectwiseStages
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_ProjectwiseStages = 0,
                ID_ProjectWiseStages = 0,
                PWSDate = data.Date,
                PWSStartDate = data.StartDate,
                PWSFinishDate = data.EndDate,
                FK_Project = data.FK_Project,
                FK_ProjectStages = data.FK_Stage,
                FK_ProjectTeam = data.FK_Team,
                PWSDuration = data.Duration,
                PWSDurationType = data.DurationType,
                PWSProjPercent = data.Percentage,
                CollectionAmount = data.CollectionAmount,
                MaterialDetails = data.MaterialDetails is null ? "" : Common.xmlTostring(data.MaterialDetails),
                WorkDetails = data.WorkDetails is null ? "" : Common.xmlTostring(data.WorkDetails),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProjectwiseStagesDetails(ProjectwiseStagesModel.ProjectwiseStagesInputFromViewModel data)
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
            //#region :: Model validation  ::



            ////removing a node in model while validating
            ////--- Model validation 
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

            //#endregion :: Model validation  ::

            ProjectwiseStagesModel measurement = new ProjectwiseStagesModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = measurement.UpdateProjectwiseStagesData(input: new ProjectwiseStagesModel.UpdateProjectwiseStages
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_ProjectwiseStages = data.ProjectwiseStagesID,
                ID_ProjectWiseStages = data.ProjectwiseStagesID,
                PWSDate = data.Date,
                PWSStartDate = data.StartDate,
                PWSFinishDate = data.EndDate,
                FK_Project = data.FK_Project,
                FK_ProjectStages = data.FK_Stage,
                FK_ProjectTeam = data.FK_Team,
                PWSDuration = data.Duration,
                PWSDurationType = data.DurationType,
                PWSProjPercent = data.Percentage,
                CollectionAmount = data.CollectionAmount,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                MaterialDetails = data.MaterialDetails is null ? "" : Common.xmlTostring(data.MaterialDetails),
                WorkDetails = data.WorkDetails is null ? "" : Common.xmlTostring(data.WorkDetails)
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteProjectwiseStagesInfo(ProjectwiseStagesModel.ProjectwiseStagesInfoView data)
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

            ProjectwiseStagesModel measurement = new ProjectwiseStagesModel();
            var datresponse = measurement.DeleteProjectwiseStagesData(input: new ProjectwiseStagesModel.DeleteProjectwiseStages { FK_ProjectwiseStages = data.ProjectwiseStagesID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProjectwiseStagesDeleteReasonList()
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
        public ActionResult EnableEdit(ProjectwiseStagesModel.UpdateProjectwiseStages project)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProjectwiseStagesModel EnableEditmode = new ProjectwiseStagesModel();
            var data = Common.GetDataViaQuery<ProjectwiseStagesModel.EnableEdit>(parameters: new APIParameters
            {
                TableName = "ProjectFollowUp",
                SelectFields = "FK_Project,PrFuCurrentStatus",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "  AND PrFuCurrentStatus=3 AND FK_Project= " + project.FK_Project,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }



    }
}