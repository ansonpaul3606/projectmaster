/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 17/09/2022
Purpose		: ProjectFollowUp
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
    public class ProjectFollowUpController : Controller
    {


        // GET: ProjectFollowUp
        public ActionResult Index(string mtd, string mgrp)
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

        public ActionResult LoadProjectFollowUpForm(string mtd)
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ProjectFollowUpModel.ProjectFollowUpListModel listModel = new ProjectFollowUpModel.ProjectFollowUpListModel();
            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[ProjectStages]",
                SelectFields = "[ID_ProjectStages] AS ProjectStagesID,[PrStName] AS StageName",
                Criteria = "Passed=1 And Cancelled=0 AND TransMode='PRST' AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Stages = Common.GetDataViaQuery<ProjectFollowUpModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            listModel.StageList = Stages.Data;
            listModel.FK_Employee = _userLoginInfo.FK_Employee;
            var OpSList = Common.GetDataViaQuery<ProjectFollowUpModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeName,M.Mode",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey

             );


            listModel.ModeList = OpSList.Data;
            listModel.VisitDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            var StatusList = Common.GetDataViaProcedure<ProjectFollowUpModel.StatusList, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 34 });

            listModel.StatusList = StatusList.Data;
            listModel.StatusList.RemoveAll(v => v.ID_Mode != 3);
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddProjectFollowUp", listModel);
        }

        public ActionResult GetStatusList(ProjectFollowUpModel.ProjectFollowUpView cr)
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
                TableName = " ProjectFollowUp",
                SelectFields = "Top 1 PrFuCurrentStatus as CurrentStatusID",
                Criteria = "Passed=1 And Cancelled=0 And FK_Project ='" + cr.FK_Project + "' AND FK_Stage = '" + cr.FK_Stage + "'AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = "entron desc"
            };
            var data = Common.GetDataViaQuery<ProjectFollowUpModel.ProjectFollowUpView>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);

            long? CurrentStatusID = 0;
            if (data.Data != null)
                CurrentStatusID = data.Data[0].CurrentStatusID;

            var StatusList = Common.GetDataViaProcedure<ProjectFollowUpModel.StatusList, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 34 });

            if (cr.CurrentStatusID == 0)
            {

                if (CurrentStatusID == 1)
                    StatusList.Data.RemoveAll(v => v.ID_Mode == 1);
                else if (CurrentStatusID == 2)
                    StatusList.Data.RemoveAll(v => v.ID_Mode != 1);
                else
                    StatusList.Data.RemoveAll(v => v.ID_Mode != 1);

            }
            else  //update case.Should load the previous status list
            {
                if (cr.CurrentStatusID == 1)
                    StatusList.Data.RemoveAll(v => v.ID_Mode != 1);
                else if (cr.CurrentStatusID == 2)
                    StatusList.Data.RemoveAll(v => v.ID_Mode == 1);
                else
                    StatusList.Data.RemoveAll(v => v.ID_Mode == 1);
            }
            var DueDate = "";
            if (cr.FK_Stage != 0)
            {
                var StageDueDate = Common.GetDataViaQuery<ProjectFollowUpModel.StageDueDate>(parameters: new APIParameters
                {
                    TableName = "ProjectWiseStages ",
                    SelectFields = "CONVERT(DATETIME,(MAX(PWSFinishDate)),103)  DueDate ",
                    Criteria = "Cancelled = 0 AND Passed = 1 AND  FK_Project = '" + cr.FK_Project + "' AND FK_ProjectStages = '" + cr.FK_Stage + "' AND  FK_Company=" + _userLoginInfo.FK_Company,
                    GroupByFileds = "",
                    SortFields = ""
                },
              companyKey: _userLoginInfo.CompanyKey

              ).Data;
                
                if (StageDueDate.Count > 0)
                {
                    if (StageDueDate[0].DueDate != null)
                        DueDate = Convert.ToDateTime(StageDueDate[0].DueDate).ToString("yyyy/MM/dd");

                }
            }
            else
            {
                var ProjectDueDate = Common.GetDataViaQuery<ProjectFollowUpModel.StageDueDate>(parameters: new APIParameters
                {
                    TableName = "Project ",
                    SelectFields = "CONVERT(DATETIME,ProjFinishDate,103)  DueDate ",
                    Criteria = "Cancelled = 0 AND Passed = 1 AND  ID_Project = '" + cr.FK_Project + "' AND  FK_Company=" + _userLoginInfo.FK_Company,
                    GroupByFileds = "",
                    SortFields = ""
                },
                 companyKey: _userLoginInfo.CompanyKey

                 ).Data;
                if (ProjectDueDate != null)
                {
                    if (ProjectDueDate.Count > 0)
                    {
                        if (ProjectDueDate[0].DueDate != null)
                            DueDate = Convert.ToDateTime(ProjectDueDate[0].DueDate).ToString("yyyy/MM/dd");

                    }
                }

            }

            return Json(new { StatusList, DueDate }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProjectStages(ProjectFollowUpModel.ProjectFollowUpView cr)
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
                TableName = "ProjectWiseStages join ProjectStages on ProjectStages.ID_ProjectStages = ProjectWiseStages.FK_ProjectStages",
                SelectFields = "distinct [ID_ProjectStages] AS ProjectStagesID,[PrStName] AS StageName",
                //Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 AND ProjectStages.ID_ProjectStages not in (select FK_Stage from ProjectFollowUp WHere FK_Project ='" + cr.FK_Project + "'" + " and isnull(ProjectFollowUp.PrFuCurrentStatus, 0) = 3  AND ProjectFollowUp.Cancelled=0) And ProjectWiseStages.FK_Project ='" + cr.FK_Project + "' AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 AND TransMode='PRST' AND FK_Project ='" + cr.FK_Project + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<ProjectFollowUpModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetEmployees(ProjectFollowUpModel.ProjectFollowUpView cr)
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


            APIParameters apiSub = new APIParameters
            {
                TableName = "ProjectAttendanceDetails PAD join ProjectTeam on ProjectTeam.ID_ProjectTeam = PAD.FK_Master join ProjectWiseStages on ProjectWiseStages.FK_ProjectTeam = PAD.FK_Master  join Employee on PAD.FK_Employee = Employee.ID_Employee",
                SelectFields = "distinct ID_Employee as EmployeeID,EmpFName as EmployeeName",
                Criteria = "PAD.Passed=1 And PAD.Cancelled=0 And PAD.TransMode='PRTC' AND ProjectWiseStages.FK_Project ='" + cr.FK_Project + "' And FK_ProjectStages = '" + cr.FK_Stage + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<ProjectFollowUpModel.EmployeeList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetAllocationInfo(MaterialAllocationModel.MaterialAllocationView AllocationView)
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


            long ProjectID = AllocationView.FK_Project;
            MaterialAllocationModel MaterialAllocation = new MaterialAllocationModel();
            var AllocationDetails = MaterialAllocation.GetMaterialAllocationData(companyKey: _userLoginInfo.CompanyKey, input: new MaterialAllocationModel.InputProjectMaterialAllocationID { FK_ProjectMaterialAllocation = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 2, FK_Project = ProjectID });

            var distAllocations = AllocationDetails.Data != null ? AllocationDetails.Data.GroupBy(v => new { v.FK_Stage, v.FK_Team, v.FK_Employee }).ToList() : null;
            return Json(new { AllocationDetails, distAllocations }, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetProjectFollowUpList(int pageSize, int pageIndex, string Name, string TransMode)
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



            ProjectFollowUpModel ProjectFollowUp = new ProjectFollowUpModel();
            var data = ProjectFollowUp.GetProjectFollowUpData(input: new ProjectFollowUpModel.InputProjectFollowUpID
            {

                FK_ProjectFollowUp = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode

            }
            , companyKey: _userLoginInfo.CompanyKey);

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProjectFollowUpInfoByID(ProjectFollowUpModel.ProjectFollowUpView data)
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

            ProjectFollowUpModel ProjectFollowUp = new ProjectFollowUpModel();
            var ProjectFollowUpDetails = ProjectFollowUp.GetProjectFollowUpData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectFollowUpModel.InputProjectFollowUpID { FK_ProjectFollowUp = data.ProjectFollowUpID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode });





            return Json(new { ProjectFollowUpDetails }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddNewProjectFollowUpDetails(ProjectFollowUpModel.ProjectFollowUpInputFromViewModel data)
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

            ProjectFollowUpModel measurement = new ProjectFollowUpModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = measurement.UpdateProjectFollowUpData(input: new ProjectFollowUpModel.UpdateProjectFollowUp
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                FK_ProjectFollowUp = 0,
                ID_ProjectFollowUp = 0,
                EffectDate = data.Date,
                PrFuStatusDate = data.StatusDate,
                FK_Project = data.FK_Project,
                FK_Stage = data.FK_Stage,
                PrFuCurrentStatus = data.CurrentStatusID,
                PrPuRemarks = data.Remarks,
                Reason = data.Reason
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProjectFollowUpDetails(ProjectFollowUpModel.ProjectFollowUpInputFromViewModel data)
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

            ProjectFollowUpModel measurement = new ProjectFollowUpModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = measurement.UpdateProjectFollowUpData(input: new ProjectFollowUpModel.UpdateProjectFollowUp
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_ProjectFollowUp = data.ProjectFollowUpID,
                ID_ProjectFollowUp = data.ProjectFollowUpID,
                TransMode = data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                EffectDate = data.Date,
                PrFuStatusDate = data.StatusDate,
                FK_Project = data.FK_Project,
                FK_Stage = data.FK_Stage,
                PrFuCurrentStatus = data.CurrentStatusID,
                PrPuRemarks = data.Remarks,

                Reason = data.Reason
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteProjectFollowUpInfo(ProjectFollowUpModel.ProjectFollowUpInfoView data)
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

            ProjectFollowUpModel measurement = new ProjectFollowUpModel();
            var datresponse = measurement.DeleteProjectFollowUpData(input: new ProjectFollowUpModel.DeleteProjectFollowUp { FK_ProjectFollowUp = data.ProjectFollowUpID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProjectFollowUpDeleteReasonList()
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