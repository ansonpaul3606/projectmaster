/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 19/07/2022
Purpose		: CostPreparation
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
using static PerfectWebERP.Models.CommonSearchPopupModel;




namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class CostPreparationController : Controller
    {


        // GET: CostPreparation
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadCostPreparationForm(string mtd)
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            CostPreparationModel.CostPreparationListModel SVList = new CostPreparationModel.CostPreparationListModel();

            //EmployeeTypeList
            CostPreparationModel objpaymode = new CostPreparationModel();
            var rolemodeList = objpaymode.GeLeadStatusList(input: new CostPreparationModel.ModeLead { Mode = 21 }, companyKey: _userLoginInfo.CompanyKey);



            SVList.EmployeeTypeList = rolemodeList.Data;

            var departmentList = Common.GetDataViaQuery<CostPreparationModel.Department>(parameters: new APIParameters
            {
                TableName = "Department Dp",
                SelectFields = "Dp.ID_Department AS DepartmentID,Dp.DeptName AS[DepartmentName]",
                Criteria = "Dp.Cancelled=0 AND Dp.Passed=1 AND Dp.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            SVList.DepartmentList = departmentList.Data;

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Project=1 AND Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Category = Common.GetDataViaQuery<CostPreparationModel.Category>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            SVList.CategoryList = Category.Data;


            var durationTypeList = Common.GetDataViaProcedure<CostPreparationModel.DurationTypeList, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 25 });


            SVList.DurationTypeList = durationTypeList.Data;

            var WorkTypeList = Common.GetDataViaQuery<CostPreparationModel.WorkTypeList>(parameters: new APIParameters
            {
                TableName = "ExtraWorkType EWT",
                SelectFields = "EWT.ID_ExtwrkType AS WorkTypeID,EWT.EWTName AS[WorkType]",
                Criteria = "EWT.Cancelled=0 AND EWT.Passed=1 AND EWT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            SVList.WorkTypeList = WorkTypeList.Data;
            SVList.VisitDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddCostPreparation", SVList);
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
            CostPreparationModel CostPreparation = new CostPreparationModel();
            var prInfo = Lead.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
            //var subproduct = CostPreparation.GetSubProduct(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

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
                var SiteVisitDetails = SiteVisit.GetSiteVisitData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = siteVisitId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = LeadGenerateInfo.TransMode });


                var EmployeeDetails = SiteVisit.GetSiteVisitEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = siteVisitId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = LeadGenerateInfo.TransMode, CheckList = 1 });


                var MeasurementDetails = SiteVisit.GetSiteVisitMeasurementData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = siteVisitId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = LeadGenerateInfo.TransMode, CheckList = 2 });

                return Json(new { SiteVisitDetails, EmployeeDetails, MeasurementDetails , siteVisitId }, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(new { siteVisitId }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult GetOtherCharges(CostPreparationModel.BindOtherCharge Data)
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

            CostPreparationModel CostPreparationModel = new CostPreparationModel();

            var OtherCharges = CostPreparationModel.FillOtherCharges(input: new CostPreparationModel.BindOtherCharge
            {
                TransMode = Data.TransMode,
                FK_Company = _userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCostPreparationList(int pageSize, int pageIndex, string Name,string TransMode)
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



            CostPreparationModel CostPreparation = new CostPreparationModel();
            var data = CostPreparation.GetCostPreparationData(input: new CostPreparationModel.InputCostPreparationID
            {

                FK_ProjectCostPreparation = 0,
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
        public ActionResult GetCostPreparationInfoByID(CostPreparationModel.CostPreparationView data)
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

            CostPreparationModel CostPreparation = new CostPreparationModel();
            var CostPreparationDetails = CostPreparation.GetCostPreparationData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = data.CostPreparationID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode });


            var EmployeeDetails = CostPreparation.GetCostPreparationEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = data.CostPreparationID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode, CheckList = 1 });


            var MaterialDetails = CostPreparation.GetCostPreparationMaterialData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = data.CostPreparationID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode, CheckList = 2 });

            var WorkDetails = CostPreparation.GetCostPreparationWorkData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = data.CostPreparationID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode, CheckList = 3 });


            var OtherChargeDetails = CostPreparation.GetCostPreparationOtherChargeData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = data.CostPreparationID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode, CheckList = 4 });

            var subimg = CostPreparation.GetCostPreparationImageData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = data.CostPreparationID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = data.TransMode, CheckList = 5});
            var imgLst = new List<CompanyImage>();
            if (subimg.Data != null)
            {
                foreach (var dt in subimg.Data)
                {
                    var img = new CompanyImage();
                    img.ComImgMode = dt.ImgMode;
                    img.ComImgName = dt.ImgName;
                    img.ComImgValue = dt.ImgValue;

                    imgLst.Add(img);
                }
                string CompanyImageList = "";
                CompanyImageList = subimg.Data.FirstOrDefault() != null ? Common.xmlTostring(imgLst) : "";
                Session["CompanyImage"] = CompanyImageList;
            }
            return Json(new { CostPreparationDetails, EmployeeDetails, MaterialDetails,WorkDetails, OtherChargeDetails, subimg }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddNewCostPreparationDetails(CostPreparationModel.CostPreparationInputFromViewModel data)
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

            CostPreparationModel measurement = new CostPreparationModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = measurement.UpdateCostPreparationData(input: new CostPreparationModel.UpdateCostPreparation
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Category = data.FK_Category,
                FK_BOMProject = data.FK_BOMProject,
                TransMode = data.TransMode,
                FK_ProjectCostPreparation = 0,
                ID_ProjectCostPreparation = 0,
                FK_LeadGeneration = data.LeadGenerationID,
                PCPVisitDate = data.VisitDate,
                PCPMaterialCost = data.MaterialCost,
                PCPWorkCost = data.WorkCost,
                PCPOtherCharges = data.OtherCharges,
                PCPAdditionalCost = data.AdditionalCost,
                PCPTotalAmount = data.TotalAmount,
                PCPRemarks = data.Remarks,
                EmployeeDetails = data.EmployeeDetails is null ? "" : Common.xmlTostring(data.EmployeeDetails),
                MaterialDetails = data.MaterialDetails is null ? "" : Common.xmlTostring(data.MaterialDetails),
                WorkDetails = data.WorkDetails is null ? "" : Common.xmlTostring(data.WorkDetails),
                OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ImportFrom = (data.ImportFrom.HasValue) ? data.ImportFrom.Value : 0,
                ImageList = image
            }, companyKey: _userLoginInfo.CompanyKey);

      
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCostPreparationDetails(CostPreparationModel.CostPreparationInputFromViewModel data)
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

            CostPreparationModel measurement = new CostPreparationModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = measurement.UpdateCostPreparationData(input: new CostPreparationModel.UpdateCostPreparation
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Category = data.FK_Category,
                FK_BOMProject = data.FK_BOMProject,
                TransMode = data.TransMode,
                FK_ProjectCostPreparation = data.CostPreparationID,
                ID_ProjectCostPreparation = data.CostPreparationID,
                FK_LeadGeneration = data.LeadGenerationID,
                PCPVisitDate = data.VisitDate,
                PCPMaterialCost = data.MaterialCost,
                PCPWorkCost = data.WorkCost,
                PCPOtherCharges = data.OtherCharges,
                PCPAdditionalCost = data.AdditionalCost,
                PCPTotalAmount = data.TotalAmount,
                PCPRemarks = data.Remarks,
                EmployeeDetails = data.EmployeeDetails is null ? "" : Common.xmlTostring(data.EmployeeDetails),
                MaterialDetails = data.MaterialDetails is null ? "" : Common.xmlTostring(data.MaterialDetails),
                WorkDetails = data.WorkDetails is null ? "" : Common.xmlTostring(data.WorkDetails),
                OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                ImageList = image,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ImportFrom = (data.ImportFrom.HasValue) ? data.ImportFrom.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);

      
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteCostPreparationInfo(CostPreparationModel.CostPreparationInfoView data)
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

            CostPreparationModel measurement = new CostPreparationModel();
            var datresponse = measurement.DeleteCostPreparationData(input: new CostPreparationModel.DeleteCostPreparation { FK_ProjectCostPreparation = data.CostPreparationID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCostPreparationDeleteReasonList()
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
        public ActionResult GetProductDetails(CostPreparationModel.BOMProjectFill Data)
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

            CostPreparationModel costPrepModel = new CostPreparationModel();

            var datresponse = costPrepModel.FillBOMProject(input: new CostPreparationModel.BOMProjectFill
            {
                FK_Master = Data.FK_Master,
                Mode = Data.Mode,
                FK_Category = Data.FK_Category,
                ID_BOMProject=Data.ID_BOMProject,
                FK_Company = _userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

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


        public ActionResult GetLeadProjectQuotationFill(Int64 FK_Masterval)
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

            CostPreparationModel purchaseModel = new CostPreparationModel();

            var datresponse = purchaseModel.FilleadQuotation(input: new CostPreparationModel.ProjectfillQuoation
            {
                FK_Master = FK_Masterval,



            }, companyKey: _userLoginInfo.CompanyKey);

            var leaddata = Common.GetDataViaQuery<CostPreparationModel.CostPreparationView>(parameters: new APIParameters
            {
                TableName = "Quotation Q Join LeadGenerate LG ON LG.ID_LeadGenerate=Q.FK_Master",
         
               SelectFields = "LG.LgLeadNo AS LeadNo,LG.ID_LeadGenerate AS LeadGenerationID ,LG.LgLeadDate LeadDates",
                Criteria = "    Q.Cancelled=0 AND Q.Passed=1 AND Q.FK_Company=" + _userLoginInfo.FK_Company + " AND Q.ID_Quotation=" + FK_Masterval,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

            );

           

            return Json(new { datresponse, leaddata }, JsonRequestBehavior.AllowGet);
        }
    }
}