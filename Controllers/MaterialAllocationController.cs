/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 22/08/2022
Purpose		: MaterialAllocation
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
using static PerfectWebERP.Models.MaterialAllocationModel;
using System.IO;
using System.Web.Hosting;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class MaterialAllocationController : Controller
    {


        // GET: MaterialAllocation
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;

           // var StandbyChk = Common.GetDataViaQuery<SaleModel.GeneralSettingsModel>(parameters: new APIParameters
           // {
           //     TableName = "GeneralSettings",
           //     SelectFields = "GsModule as GsModule,GsValue AS GsValue,GsField AS GsField",
           //     Criteria = "GsField='ESBYSTInP'AND FK_Company=" + _userLoginInfo.FK_Company,
           //     SortFields = "",
           //     GroupByFileds = ""
           // },
           //companyKey: _userLoginInfo.CompanyKey);

            var sesionStockCheckMTP = "";
            //Session["StandbyChk.Data[0].GsValue"] = sesionStockCheckMTP;
            //ViewBag.ChekStandBy = StandbyChk.Data[0].GsValue;

            var perfomadata = Common.GetDataViaQuery<LeadGenerateModel.MRPEdit>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'PROJ' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='PROJ006'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            ViewBag.ChekStandBy = perfomadata.Data[0].GsValue;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            Common.ClearOtherCharges(ViewBag.TransMode);

            return View();
        }

        public ActionResult LoadMaterialAllocationForm(string mtd)
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            MaterialAllocationModel.MaterialAllocationListModel listModel = new MaterialAllocationModel.MaterialAllocationListModel();
            MaterialAllocationModel repair = new MaterialAllocationModel();
            var TypeList = repair.GeLeadStatusList(input: new MaterialAllocationModel.ModeLead { Mode = 76 },

            companyKey: _userLoginInfo.CompanyKey);

            listModel.ActionStatusList = TypeList.Data;

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[ProjectStages]",
                SelectFields = "[ID_ProjectStages] AS StageID,[PrStName] AS StageName",
                Criteria = "Passed=1 And Cancelled=0 AND TransMode='PRST' AND  FK_Company=" + _userLoginInfo.FK_Company ,
                GroupByFileds = "",
                SortFields = ""
            };
            var Stages = Common.GetDataViaQuery<MaterialAllocationModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            listModel.StageList = Stages.Data;
            listModel.FK_Employee = _userLoginInfo.FK_Employee;
            var OpSList = Common.GetDataViaQuery<MaterialAllocationModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeName,M.Mode",
                Criteria = "Mode<>'O'",
                GroupByFileds = "",
                SortFields = ""
            },
             companyKey: _userLoginInfo.CompanyKey);          
            listModel.ModeList = OpSList.Data;



            listModel.VisitDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            var OpBranchList = Common.GetDataViaQuery<MaterialAllocationModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                     companyKey: _userLoginInfo.CompanyKey

          );
            listModel.BranchList = OpBranchList.Data;
          
            return PartialView("_AddMaterialAllocation", listModel);
        }
        [HttpPost]
        public ActionResult GetProjectStages(long FK_Project)
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
                TableName = "ProjectWiseStages PWS JOIN ProjectStages PS ON PS.ID_ProjectStages = PWS.FK_ProjectStages LEFT JOIN ProjectFollowUp PF ON PF.FK_Stage=PWS.FK_ProjectStages AND PF.PrFuCurrentStatus = 3 AND PF.FK_Project = PWS.FK_Project AND PF.Cancelled = 0",
                SelectFields = "distinct [ID_ProjectStages] AS ProjectStagesID,[PrStName] AS StageName",                
                Criteria = "PWS.Passed=1 And PWS.Cancelled=0 AND TransMode='PRST' AND PWS.FK_Project ='" + FK_Project + "'" + "AND PWS.FK_Company='" + _userLoginInfo.FK_Company + "'"+"AND PF.ID_ProjectFollowUp IS NULL",
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialAllocationModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }

      
        [HttpPost]
        public ActionResult GetProjectTeam(MaterialModel.MaterialRequestView cr)
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

            #endregion :: Model validation  ::
            APIParameters apiSub = new APIParameters
            {
                TableName = " [ProjectTeam] join ProjectWiseStages  on ProjectWiseStages.FK_ProjectTeam=ProjectTeam.ID_ProjectTeam and ProjectWiseStages.Cancelled=0",
                SelectFields = "DISTINCT [ID_ProjectTeam] AS TeamID,[ProjTeamName] AS TeamName",
                Criteria = "ProjectTeam.Passed=1 And ProjectTeam.Cancelled=0 And ProjectTeam.FK_Project ='" + cr.FK_Project + "' AND FK_ProjectStages = '" + cr.FK_Stages + "' AND ProjectTeam.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
          
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialModel.TeamList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CheckAdditionalQuantity(MaterialModel.QuantityCheck cr)
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

            #endregion :: Model validation  ::
            var QuanityCheckResult = new MaterialModel().CheckQuantity(companyKey: _userLoginInfo.CompanyKey, input: new MaterialModel.QuantityCheck
            {
                FK_Product = cr.FK_Product,
                FK_Project = cr.FK_Project,
                FK_Stage = cr.FK_Stage,
                FK_Team = cr.FK_Team,
                Quantity = cr.Quantity,
                FK_ProjectMaterialAllocation=cr.FK_ProjectMaterialAllocation
            });
            var Warnmsg = "";
            if (QuanityCheckResult.Data != null && QuanityCheckResult.Data.Count > 0)
            {
                Warnmsg = QuanityCheckResult.Data[0].Warnmsg;

            }
            return Json(Warnmsg, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetProjectStages(MaterialAllocationModel.MaterialAllocationView cr)
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


            #endregion :: Model validation  ::
            APIParameters apiSub = new APIParameters
            {
                TableName = "ProjectWiseStages join ProjectStages on ProjectStages.ID_ProjectStages = ProjectWiseStages.FK_ProjectStages",
                SelectFields = "distinct [ID_ProjectStages] AS StageID,[PrStName] AS StageName",
                Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 And TransMode='PRST' And FK_Project ='" + cr.FK_Project + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
                               
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialAllocationModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetEmployees(MaterialAllocationModel.MaterialAllocationView cr)
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
                TableName = "ProjectAttendanceDetails PAD join ProjectTeam on ProjectTeam.TeamID = PAD.FK_Master join ProjectWiseStages on ProjectWiseStages.FK_ProjectTeam = PAD.FK_Master  join Employee on PAD.FK_Employee = Employee.ID_Employee",
                SelectFields = "distinct ID_Employee as FK_Employee,EmpFName as EmployeeName",
                Criteria = "PAD.Passed=1 And PAD.Cancelled=0 And PAD.TransMode='PRTC' AND ProjectWiseStages.FK_Project ='" + cr.FK_Project + "'" + "And FK_ProjectStages = '" + cr.FK_Stage + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialAllocationModel.EmployeeList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
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
            MaterialAllocationModel MaterialAllocation = new MaterialAllocationModel();
            var prInfo = Lead.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
            //var subproduct = MaterialAllocation.GetSubProduct(companyKey: _userLoginInfo.CompanyKey, input: new MaterialAllocationModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

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
            else {
                return Json(new {  }, JsonRequestBehavior.AllowGet);
            }

        }
       
        [HttpPost]
        public ActionResult GetMaterialAllocationList(int pageSize, int pageIndex, string Name)
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

            MaterialAllocationModel MaterialAllocation = new MaterialAllocationModel();
            var data = MaterialAllocation.GetMaterialAllocationData(input: new MaterialAllocationModel.InputProjectMaterialAllocationID
            {

                FK_ProjectMaterialAllocation = 0,
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
        public ActionResult GetMaterialAllocationInfoByID(MaterialAllocationModel.MaterialAllocationView data)
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

            MaterialAllocationModel MaterialAllocation = new MaterialAllocationModel();
            var MaterialAllocationDetails = MaterialAllocation.GetMaterialAllocationData(companyKey: _userLoginInfo.CompanyKey, input: new MaterialAllocationModel.InputProjectMaterialAllocationID {
                FK_ProjectMaterialAllocation = data.ProjectMaterialAllocationID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,              
                TransMode = data.TransMode,
                CheckList = 0
            });


            var MaterialDetails = MaterialAllocation.GetMaterialAllocationMaterialData(companyKey: _userLoginInfo.CompanyKey, input: new MaterialAllocationModel.InputProjectMaterialAllocationID {
                FK_ProjectMaterialAllocation = data.ProjectMaterialAllocationID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = data.TransMode,
                CheckList = 1
            });
            var SerialNumberInfo = MaterialAllocation.GetSerialNumber(companyKey: _userLoginInfo.CompanyKey, input: new MaterialAllocationModel.SelectSerialNo
            {
                ID_ProjectMaterialDetails = data.ProjectMaterialAllocationID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                Mode = 2,
            });


            return Json(new { MaterialAllocationDetails, MaterialDetails , SerialNumberInfo }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddNewMaterialAllocationDetails(MaterialAllocationModel.MaterialAllocationInputFromViewModel data)
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

            MaterialAllocationModel measurement = new MaterialAllocationModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = measurement.UpdateMaterialAllocationData(input: new MaterialAllocationModel.UpdateMaterialAllocation
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_ProjectMaterialAllocation = 0,
                ID_ProjectMaterialAllocation = 0,
                ProMatAllocationDate = data.Date,
                FK_Project = data.FK_Project,
                FK_Stages = data.FK_Stage,
                FK_Team = data.FK_Team,
                FK_Employee = data.FK_Employee,
                
                FK_ProjectMaterialRequest = (data.FK_ProjectMaterialRequest.HasValue) ? data.FK_ProjectMaterialRequest.Value : 0,
                FK_Department = _userLoginInfo.FK_Department,
                RequestMode = data.RequestMode,
                MaterialDetails = data.MaterialDetails is null ? "" : Common.xmlTostring(data.MaterialDetails),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ProductSerialNumbers = data.ProductSerialNumbers is null ? "" : Common.xmlTostring(data.ProductSerialNumbers),
                FK_Category=data.FK_Category,
                FK_BOMProject=data.FK_BOMProject,
                FK_Branch=data.FK_Branch,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMaterialAllocationDetails(MaterialAllocationModel.MaterialAllocationInputFromViewModel data)
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




            //#endregion :: Model validation  ::

            MaterialAllocationModel measurement = new MaterialAllocationModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = measurement.UpdateMaterialAllocationData(input: new MaterialAllocationModel.UpdateMaterialAllocation
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_ProjectMaterialAllocation = data.ProjectMaterialAllocationID,
                ID_ProjectMaterialAllocation = data.ProjectMaterialAllocationID,
                ProMatAllocationDate = data.Date,
                FK_Project = data.FK_Project,
                FK_Stages = data.FK_Stage,
                FK_Team = data.FK_Team,
                FK_Employee = data.FK_Employee,
                FK_ProjectMaterialRequest = (data.FK_ProjectMaterialRequest.HasValue) ? data.FK_ProjectMaterialRequest.Value : 0,
                FK_Department = _userLoginInfo.FK_Department,
                RequestMode = data.RequestMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                MaterialDetails = data.MaterialDetails is null ? "" : Common.xmlTostring(data.MaterialDetails),
                ProductSerialNumbers = data.ProductSerialNumbers is null ? "" : Common.xmlTostring(data.ProductSerialNumbers),
                FK_Category = data.FK_Category,
                FK_BOMProject = data.FK_BOMProject,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMaterialAllocationInfo(MaterialAllocationModel.MaterialAllocationInfoView data)
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

            MaterialAllocationModel measurement = new MaterialAllocationModel();
            var datresponse = measurement.DeleteMaterialAllocationData(input: new MaterialAllocationModel.DeleteMaterialAllocation { FK_ProjectMaterialAllocation = data.ProjectMaterialAllocationID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetMaterialAllocationDeleteReasonList()
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



        public ActionResult GetMaterialnewfill(MaterialAllocationModel.MaterialAllocationView data)
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
            MaterialAllocationModel objrenew = new MaterialAllocationModel();

            var RefillInfo = objrenew.GetMaterialfill(companyKey: _userLoginInfo.CompanyKey, input: new MaterialAllocationModel.Datamaterialfill
            {
                //FK_ProjectMaterialRequest = data.FK_ProjectMaterialRequest,
                FK_ProjectMaterialRequest = (data.FK_ProjectMaterialRequest.HasValue) ? data.FK_ProjectMaterialRequest.Value : 0,
                FK_Branch = data.FK_Branch,
                FK_Department=data.FK_Department

            });





            return Json(RefillInfo, JsonRequestBehavior.AllowGet);
        }


        #region[GetSerialNumberForProdunct]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetSubProductsDetailInfo(MaterialAllocationModel.InputProduct saleInfo)
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

            MaterialAllocationModel objrenew = new MaterialAllocationModel();

            var subproduct = objrenew.GetSubProducts(companyKey: _userLoginInfo.CompanyKey, input: new MaterialAllocationModel.GetSubProduct
            {
                ID_Sales = saleInfo.ID_ProjectMaterialDetails,
                TransMode = saleInfo.TransMode,
                FK_Product = saleInfo.FK_Product,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                StockID = saleInfo.StockID,
                Mode = saleInfo.Mode,
                Quantity = saleInfo.Quantity,
            });
            return Json(new { subproduct }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region GetDeliverySlip
        [HttpPost]
        public ActionResult GetDeliverySlip(GetDeliverySlipDataIp inputdata)
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


            MaterialAllocationModel MaterialAllocation = new MaterialAllocationModel();
            var Tabledata = MaterialAllocation.GetDeliverySlipTableData(input: new MaterialAllocationModel.GetDeliverySlipDataProIp
            {

                Fk_MaterilaAllocation = inputdata.Fk_MaterilaAllocation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Mode = 2,
                TransMode = "PRMA"

            }
            , companyKey: _userLoginInfo.CompanyKey);
            var Otherdata = MaterialAllocation.GetDeliverySlipData(input: new MaterialAllocationModel.GetDeliverySlipDataProIp
            {

                Fk_MaterilaAllocation = inputdata.Fk_MaterilaAllocation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Mode = 1,
                TransMode = "PRMA"

            }
            , companyKey: _userLoginInfo.CompanyKey);

            //---------------
            if(Otherdata.Data[0].Comlogo!="" && Otherdata.Data[0].Comlogo != null)
            {
                string imageURL = "";
                string folder = "CompanyLogoImg";
                folder = $"{folder}/company_{_userLoginInfo.FK_Company}";
                string comlogo = Otherdata.Data.FirstOrDefault().Comlogo;
                string fileName = Otherdata.Data.FirstOrDefault().ComlogoName;


                byte[] bytes = Convert.FromBase64String(comlogo);//image base64string
                if (bytes.Length > 0)
                {

                    String path = System.Web.HttpContext.Current.Server.MapPath("/" + folder); //Path

                    //Check if directory exist
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                    }

                    string filePath = "/" + folder + "/" + fileName;

                    using (var stream = new FileStream(HostingEnvironment.MapPath(filePath), FileMode.Create))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Flush();
                        //imageURL = baseUri + folder + "/" + fileName;
                        imageURL = folder + "/" + fileName;
                    }
                }
                Otherdata.Data.FirstOrDefault().Comlogo = imageURL;

                // var extension1 = imageURL.split('.').pop();
                string extension = Path.GetExtension(imageURL);
                extension = extension.TrimStart('.');
                Otherdata.Data.FirstOrDefault().Extension = extension;
            }
            else
            {
                Otherdata.Data.FirstOrDefault().Extension = "";
               if( Otherdata.Data[0].Comlogo == null)
                {
                    Otherdata.Data[0].Comlogo = "";
                }
            }

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { Tabledata, Otherdata }, JsonRequestBehavior.AllowGet);

        }


        //public static string ConvertBase64ToImageURL(DataRow dr)
        //{
        //    string folder = "CustomerPortalSlider";
        //    string imageURL = "";
        //    var baseUri = new Uri(System.Web.HttpContext.Current.Request.Url, "/");

        //    if (dr != null)
        //    {
        //        long companyID = 0;
        //        long sliderID = 0;
        //        string fileName = "";// Convert.ToString(dr["SliderTitle"]);

        //        if (dr["FK_Company"] != DBNull.Value)
        //        {
        //            companyID = Convert.ToInt64(dr["FK_Company"]);
        //        }


        //        folder = $"{folder}/company_{companyID}";

        //        if (dr["ImageFileName"] != DBNull.Value)
        //        {
        //            fileName = Convert.ToString(dr["ImageFileName"]);
        //        }


        //        if (dr["SilderImage"] != DBNull.Value)
        //        {

        //            String path = System.Web.HttpContext.Current.Server.MapPath("~/" + folder); //Path

        //            //Check if directory exist
        //            if (!System.IO.Directory.Exists(path))
        //            {
        //                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
        //            }


        //            //    byte[] bytes = Convert.FromBase64String(Convert.ToString(dr["SilderImage"]));

        //            string imgbase = Convert.ToString(dr["SilderImage"]);

        //            string result = System.Text.RegularExpressions.Regex.Replace(imgbase, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);

        //            //------------------------
        //            byte[] bytes = Convert.FromBase64String(result);//image base64string

        //            //byte[] bytes = (byte[])(dr["SilderImage2"]);
        //            if (bytes.Length > 0)
        //            {
        //                if (String.IsNullOrWhiteSpace(fileName))
        //                {
        //                    if (dr["ID_CusPortalSlider"] != DBNull.Value)
        //                    {
        //                        sliderID = Convert.ToInt64(dr["ID_CusPortalSlider"]);
        //                    }

        //                    string fileExtension = GetFileExtension(bytes);
        //                    fileName = $"sliderImage_{sliderID}.{fileExtension}";
        //                }

        //                string filePath = "~/" + folder + "/" + fileName;

        //                using (var stream = new FileStream(HostingEnvironment.MapPath(filePath), FileMode.Create))
        //                {
        //                    stream.Write(bytes, 0, bytes.Length);
        //                    stream.Flush();
        //                    //imageURL = baseUri + folder + "/" + fileName;
        //                    imageURL = folder + "/" + fileName;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            imageURL = "";
        //        }
        //    }



        //    return imageURL;

        //}

        #endregion

        #region CategorybasedonProject
        public ActionResult GetCategoryList(Int32 FK_Project)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<MaterialAllocationModel.Category>(parameters: new APIParameters
            {
                TableName = "Project P JOIN Category C ON C.ID_Category=P.FK_Category AND C.Cancelled=0",
                SelectFields = "DISTINCT(P.ID_Project) AS ID_Project,C.ID_Category AS CategoryID,C.CatName AS CategoryName",
                Criteria = "P.Cancelled=0 AND P.Passed=1 AND P.FK_Company=" + _userLoginInfo.FK_Company + "AND P.ID_Project=" + FK_Project,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CategorybasedonProject
        public ActionResult GetBOMList(Int32 FK_Category)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CostPreparationModel.BOMList>(parameters: new APIParameters
            {
                TableName = "BOMProject BP ",
                SelectFields = "BP.BOMName AS BOMName,BP.ID_BOMProject AS ID_BOMProject",
                Criteria = "BP.Cancelled =0 AND BP.Passed=1 AND BP.FK_Category='" + FK_Category + "'" + " AND BP.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProductdetailsBasedOnBOM
        public ActionResult GetProductDetails(MaterialAllocationModel.BOMProjectFill Data)
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

            MaterialAllocationModel matAllocModel = new MaterialAllocationModel();

            var datresponse = matAllocModel.FillBOMProject(input: new MaterialAllocationModel.BOMProjectFill
            {
                //FK_Master = Data.FK_Master,
                Mode = 1,
                FK_Category = Data.FK_Category,
                ID_BOMProject = Data.ID_BOMProject,
                FK_Company = _userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

}