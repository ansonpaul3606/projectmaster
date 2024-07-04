/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 27/07/2022
Purpose		: ProjectCreation
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
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ProjectCreationController : Controller
    {


             // GET: ProjectCreation
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

            return View();
            }

            public ActionResult LoadProjectCreationForm(string mtd)
            {


                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;            
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ProjectCreationModel.ProjectCreationListModel MUList = new ProjectCreationModel.ProjectCreationListModel();



                var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                  companyKey: _userLoginInfo.CompanyKey,
                  procedureName: "ProGetNextNo",
                  parameter: new NextSortOrder
                  {
                      TableName = "ProjectCreation",
                      FieldName = "SortOrder",
                      Debug = 0
                  });

                MUList.SortOrder = a.Data[0].NextNo;

            var durationTypeList = Common.GetDataViaProcedure<ProjectCreationModel.DurationTypeList, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 25 });


            MUList.DurationTypeList = durationTypeList.Data;

            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Passed=1  and Cancelled=0  AND FK_Company=" + _userLoginInfo.FK_Company, //Project chechbox checking is not required here. As per the client requirement All Category should load in Project category Category section
                GroupByFileds = "",
                SortFields = ""
            };
            var Category = Common.GetDataViaQuery<ProjectCreationModel.Category>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            MUList.CategoryList = Category.Data;

            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<ProjectCreationModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);

            MUList.TaxgroupList = taxgroup.Data;

            APIParameters apiParamebranch = new APIParameters
            {

                TableName = "Branch B",
                SelectFields = "B.ID_Branch as BranchID,B.BrName AS Branch",
                Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""


            };
            var branch = Common.GetDataViaQuery<ProjectCreationModel.BranchList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParamebranch);

            MUList.BranchList = branch.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            var custransmode = Common.GetDataViaQuery<ProjectCreationModel>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode as CusTransMode",
                Criteria = "ControllerName='Customer'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
       companyKey: _userLoginInfo.CompanyKey);
            string cus = custransmode.ToString();
            ViewBag.CusTransmode = objCmnMethod.EncryptString(cus);

            return PartialView("_AddProjectCreation", MUList);
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
        public ActionResult GetFinalAmount(LeadGenerateModel.LeadGenerateView LeadGenerateInfo)
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
            var ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID;

            var data = Common.GetDataViaQuery<CostPreparationModel.CostPreparationView>(parameters: new APIParameters
            {
                TableName = "ProjectCostPreparation SV",
                SelectFields = "SV.ID_ProjectCostPreparation as CostPreparationID,SV.TotalAmount as FinalAmount",
                Criteria = "    SV.Cancelled=0 AND SV.Passed=1 AND SV.FK_Company=" + _userLoginInfo.FK_Company + " AND SV.FK_LeadGeneration=" + ID_LeadGenerate,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );
            return Json(ID_LeadGenerate, JsonRequestBehavior.AllowGet);

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
      
       
        public ActionResult GetTotalAmount(LeadGenerateModel.LeadGenerateView LeadGenerateInfo)
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

            var data = Common.GetDataViaQuery<CostPreparationModel.CostPreparationView>(parameters: new APIParameters
            {
                TableName = "ProjectCostPreparation PCP",
                SelectFields = "PCP.PCPTotalAmount as TotalAmount",
                Criteria = " PCP.Cancelled=0 AND PCP.Passed=1 AND PCP.FK_Company=" + _userLoginInfo.FK_Company + " AND PCP.FK_LeadGeneration=" + ID_LeadGenerate,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );
            decimal TotAmount = 0;
            if (data.Data != null)
                TotAmount = data.Data[0].TotalAmount;

            
          
                return Json(new { TotAmount }, JsonRequestBehavior.AllowGet);
            

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetCostPreparationInfo(LeadGenerateModel.LeadGenerateView LeadGenerateInfo)
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

            var data = Common.GetDataViaQuery<CostPreparationModel.CostPreparationView>(parameters: new APIParameters
            {
                TableName = "ProjectCostPreparation PCP",
                SelectFields = "PCP.ID_ProjectCostPreparation as CostPreparationID",
                Criteria = " PCP.Cancelled=0 AND PCP.Passed=1 AND PCP.FK_Company=" + _userLoginInfo.FK_Company + " AND PCP.FK_LeadGeneration=" + ID_LeadGenerate,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );
            long CostPreparationId = 0;
            if (data.Data != null)
                CostPreparationId = data.Data[0].CostPreparationID;

            CostPreparationModel CostPreparation = new CostPreparationModel();
            ;
            if (CostPreparationId > 0)
            {
                var CostPreparationDetails = CostPreparation.GetCostPreparationData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = CostPreparationId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


                var EmployeeDetails = CostPreparation.GetCostPreparationEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = CostPreparationId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 1 });


                var MaterialDetails = CostPreparation.GetCostPreparationMaterialData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = CostPreparationId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 2 });

                var WorkDetails = CostPreparation.GetCostPreparationWorkData(companyKey: _userLoginInfo.CompanyKey, input: new CostPreparationModel.InputCostPreparationID { FK_ProjectCostPreparation = CostPreparationId, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 3 });


                return Json(new { CostPreparationDetails, EmployeeDetails, MaterialDetails , WorkDetails, CostPreparationId }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { CostPreparationId }, JsonRequestBehavior.AllowGet);
            }

        }

      

        [HttpPost]
        public ActionResult GetProjectCreationList(int pageSize, int pageIndex, string Name)
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

            ProjectCreationModel ProjectCreation = new ProjectCreationModel();
            var data = ProjectCreation.GetProjectCreationData(input: new ProjectCreationModel.InputProjectID
            {

                FK_Project = 0,
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
            public ActionResult GetProjectCreationInfoByID(ProjectCreationModel.ProjectCreationInfoView data)
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

                ProjectCreationModel measurement = new ProjectCreationModel();
                var projectInfo = measurement.GetProjectCreationData(companyKey: _userLoginInfo.CompanyKey, input: new ProjectCreationModel.InputProjectID {
                    FK_Project = data.ProjectID,
                    FK_Company = _userLoginInfo.FK_Company,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    PageIndex = 0,
                    PageSize = 0,
                    TransMode = data.TransMode });
            var buybackdetail = measurement.GetBuyBackSelect(companyKey: _userLoginInfo.CompanyKey, input: new ProjectCreationModel.GetBuyBack
            {
                FK_Master = data.ProjectID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });

            var Imageselect = measurement.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new ProjectCreationModel.GetImagein
            {                
               
                 FK_Project = data.ProjectID,
                 Image = 1,
                 FK_Company = _userLoginInfo.FK_Company,
                 EntrBy = _userLoginInfo.EntrBy,
                 FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                 FK_Machine = _userLoginInfo.FK_Machine,
                 PageIndex = 0,
                 PageSize = 0,                
                 TransMode = ""


             });
            if (Imageselect.Data != null)
            {
                foreach (ProjectCreationModel.ImageListView itm in Imageselect.Data)
                {
                    if (itm.ProdImage != "" && itm.ProdImage != null)
                    {
                        itm.ProdImage = "data:image/;base64," + itm.ProdImage;
                    }
                }
            }
            return Json(new { projectInfo, Imageselect,buybackdetail }, JsonRequestBehavior.AllowGet);
        }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult AddNewProjectCreationDetails(ProjectCreationModel.ProjectCreationInputFromViewModel data)
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
            if (data.ImageList != null)
            {
                foreach (ProjectCreationModel.ImageListView itm in data.ImageList)
                {
                    if (itm.ProdImage != null && itm.ProdImage != "")
                    {
                        var img = itm.ProdImage.Split(';')[1].Replace("base64,", "");

                        itm.ProdImage = img;
                    }
                    else
                    {
                        itm.ProdImage = "";

                    }
                }
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
                #region :: Model validation  ::



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

                #endregion :: Model validation  ::

                ProjectCreationModel measurement = new ProjectCreationModel();


             
                var dataresponse = measurement.UpdateProjectCreationDatas(input: new ProjectCreationModel.UpdateProjectCreation
                {
                    UserAction = 1,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,                    
                    FK_Project=0,
                    ID_Project = 0,
                    FK_LeadGeneration = data.LeadGenerationID,
                    ProjName = data.Name,
                    ProjShortName = data.ShortName,
                    FK_Category=data.Category,
                    FK_Branch = data.BranchID,
                    FK_SubCategory = data.SubCategory,
                    ProjCreateDate = data.CreateDate,
                    ProjStartDate = data.StartDate,
                    ProjFinishDate = data.EndDate,
                    ProjFinalizationAmount = data.FinalAmount,
                    ProjectAmount = data.ProjectAmount,
                    DiscountAmount = data.DiscountAmount,
                    BuyBackAmount=data.BuyBackAmount,
                    ProjDuration = data.Duration,
                    ProjDurationType = data.DurationType,
                    ProjIncludeTax = data.ProjIncludeTax,
                    FK_TaxGroup = data.FK_TaxGroup,
                    ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                    buyback = data.buyback is null ? "" : Common.xmlTostring(data.buyback),
                    TransMode =data.TransMode,
                    LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,


                }, companyKey: _userLoginInfo.CompanyKey);


            ProjectCreationModel.UpdateProjectCreationResult obData = new ProjectCreationModel.UpdateProjectCreationResult();

            if(dataresponse.Data[0].ErrCode != 0)
            {
                string errMsg = dataresponse.Data[0].ErrMsg;
                return Json(new { Process = new Output { IsProcess = false, Message = new List<string> { errMsg } } });
            }
            else
            {
                List<string> objMsg = new List<string>();
                objMsg.Add(dataresponse.Data[0].CusNumber);
                objMsg.Add(dataresponse.Data[0].ProjectNo);
           
                dataresponse.Process.Message = objMsg;        
                return Json(new { Process = new Output { IsProcess = true, Message = objMsg}, CusNumber= dataresponse.Data[0].CusNumber, ProjectNo= dataresponse.Data[0].ProjectNo });
            }
            //return Json(new { dataresponse, data = dataresponse.Data, dataresponse.Process }, JsonRequestBehavior.AllowGet);
                       
            //obData = dataresponse.Data[0];
            //if (dataresponse.Data[0].ErrCode != -2)
            //{               
            //    List<string> objMsg = new List<string>();
            //    objMsg.Add(obData.Success);
            //    dataresponse.Process.Message = objMsg;
            //    return Json(new { Process = dataresponse.Process }, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return Json(dataresponse, JsonRequestBehavior.AllowGet);
            //}
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProjectCreationDetails(ProjectCreationModel.ProjectCreationInputFromViewModel data)
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
            var imagenull = false;

            if (data.ImageList != null)
            {
                foreach (ProjectCreationModel.ImageListView itm in data.ImageList)
                {
                    if (itm != null)
                    {
                        if (itm.ProdImage != null && itm.ProdImage != "")
                        {
                            var img = itm.ProdImage.Split(';')[1].Replace("base64,", "");

                            itm.ProdImage = img;
                        }
                        else
                        {
                            itm.ProdImage = "";

                        }
                    }
                    else
                    {
                        imagenull = true;
                    }
                }
            }

            
           
            if (imagenull)
            {
                data.ImageList = null;
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

                ProjectCreationModel measurement = new ProjectCreationModel();


              
                var dataresponse = measurement.UpdateProjectCreationData(input: new ProjectCreationModel.UpdateProjectCreation
                {
                    UserAction = 2,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_Project = data.ProjectID,
                    ID_Project = data.ProjectID,
                    FK_LeadGeneration = data.LeadGenerationID,
                    ProjName = data.Name,
                    ProjShortName = data.ShortName,
                    FK_Category = data.Category,
                    FK_Branch = data.BranchID,
                    FK_SubCategory = data.SubCategory,
                    ProjCreateDate = data.CreateDate,
                    ProjStartDate = data.StartDate,
                    ProjFinishDate = data.EndDate,
                    ProjFinalizationAmount = data.FinalAmount,
                    ProjectAmount = data.ProjectAmount,
                    DiscountAmount = data.DiscountAmount,
                    BuyBackAmount = data.BuyBackAmount,
                    ProjDuration = data.Duration,
                    ProjDurationType = data.DurationType,
                    ProjIncludeTax=data.ProjIncludeTax,
                    FK_TaxGroup=data.FK_TaxGroup,
                    ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                    buyback = data.buyback is null ? "" : Common.xmlTostring(data.buyback),
                    TransMode = data.TransMode,
                    LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,


                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }

            [HttpPost]
            [ValidateAntiForgeryToken()]
            public ActionResult DeleteProjectCreationInfo(ProjectCreationModel.ProjectCreationInfoView data)
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

            ProjectCreationModel measurement = new ProjectCreationModel();
                var datresponse = measurement.DeleteProjectCreationData(input: new ProjectCreationModel.DeleteProjectCreation { FK_Project = data.ProjectID, EntrBy= _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,TransMode="" }, companyKey: _userLoginInfo.CompanyKey);
                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }


        public ActionResult GetProjectCreationDeleteReasonList()
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
        public ActionResult ShowBuyBack()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProjectCreationModel BuyBack = new ProjectCreationModel();
            //var data = Common.GetDataViaQuery<ProjectCreationModel.BuyBack>(parameters: new APIParameters
            //{
            //    TableName = "GeneralSettings",
            //    SelectFields = "GsValue,GsField",
            //    Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "  AND GsField='BuyBck'",
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey);
            var data = Common.GetDataViaQuery<SaleModel.BuyBack>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'PROJ' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='PROJ007'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EnableEdit(ProjectCreationModel.UpdateProjectCreation project)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ProjectCreationModel EnableEditmode = new ProjectCreationModel();
            var data = Common.GetDataViaQuery<ProjectCreationModel.EnableEdit>(parameters: new APIParameters
            {
                TableName = "ProjectBilling",
                SelectFields = "FK_Project,PrBillMode",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "  AND PrBillMode=2 AND FK_Project= "+ project.FK_Project,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindCustomerdetails(ProjectCreationModel.CustomerID Data)
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

            ProjectCreationModel ProjectCreationModel = new ProjectCreationModel();

            var datresponse = ProjectCreationModel.GetCustomerDetailsFill(input: new ProjectCreationModel.CustomerID
            {
                FK_Customer = Data.FK_Customer,
                CusOth = Data.CusOth,
                TransMode = Data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,

            },
          companyKey: _userLoginInfo.CompanyKey);

            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }

    }
}