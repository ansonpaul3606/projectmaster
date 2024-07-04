/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 07/11/2022
Purpose		: ProductStageSettings
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
    public class ProductStageSettingsController : Controller
    {


        // GET: ProductStageSettings
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

        public ActionResult LoadProductStageSettingsForm()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ProductStageSettingsModel.ProductStageSettingsListModel SVList = new ProductStageSettingsModel.ProductStageSettingsListModel();

            //EmployeeTypeList
            ProductStageSettingsModel objpaymode = new ProductStageSettingsModel();
            var rolemodeList = objpaymode.GeLeadStatusList(input: new ProductStageSettingsModel.ModeLead { Mode = 21 }, companyKey: _userLoginInfo.CompanyKey);



            SVList.EmployeeTypeList = rolemodeList.Data;

            var departmentList = Common.GetDataViaQuery<ProductStageSettingsModel.Department>(parameters: new APIParameters
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


            var ProductStageSettingsTypeList = Common.GetDataViaQuery<ProductStageSettingsModel.MeasurementTypeList>(parameters: new APIParameters
            {
                TableName = "MeasurementType MT",
                SelectFields = "MT.ID_MeasurementType AS MeasurementTypeID,MT.MTName AS[MeasurementType]",
                Criteria = "MT.Cancelled=0 AND MT.Passed=1 AND MT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            SVList.MeasurementTypeList = ProductStageSettingsTypeList.Data;

            var ProductStageSettingsUnitList = Common.GetDataViaQuery<ProductStageSettingsModel.MeasurementUnitList>(parameters: new APIParameters
            {
                TableName = "MeasurementUnit MU",
                SelectFields = "MU.ID_MeasurementUnit AS MeasurementUnitID,MU.MUName AS[MeasurementUnit]",
                Criteria = "MU.Cancelled=0 AND MU.Passed=1 AND MU.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            SVList.MeasurementUnitList = ProductStageSettingsUnitList.Data;

            var WorkTypeList = Common.GetDataViaQuery<ProductStageSettingsModel.WorkTypeList>(parameters: new APIParameters
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
            SVList.VisitTime = DateTime.Now.TimeOfDay.ToString();
            return PartialView("_AddProductStageSettings", SVList);
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
        public ActionResult GetOtherCharges(SalesOrderModel.BindOtherCharge Data)
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
            ProductStageSettingsModel ProductStageSettings = new ProductStageSettingsModel();
            var OtherCharges = ProductStageSettings.FillOtherCharges(input: new ProductStageSettingsModel.BindOtherCharge
            {
                TransMode = Data.TransMode,

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

            var ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID;

            var data = Common.GetDataViaQuery<LeadGenerateModel.LeadGenerateView>(parameters: new APIParameters
            {
                TableName = "LeadGenerateProduct LGP",
                SelectFields = "LGP.ID_LeadGenerateProduct as LeadGenerateID",
                Criteria = "    LGP.Cancelled=0  AND LGP.FK_LeadGenerate=" + ID_LeadGenerate,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );
            long LeadGenerateProductID = 0;
            if (data.Data != null)
                LeadGenerateProductID = data.Data[0].LeadGenerateID;


            LeadGenerateModel Lead = new LeadGenerateModel();
            LeadGenerateActionModel Lead1 = new LeadGenerateActionModel();
            ProductStageSettingsModel ProductStageSettings = new ProductStageSettingsModel();
            // var prInfo = Lead.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
            var prInfo = Lead1.GetLeadGenerateActionData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateActionModel.LeadFollowup { ID_LeadGenerateAction = LeadGenerateProductID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser });

            //var subproduct = Sit//eVisit.GetSubProduct(companyKey: _userLoginInfo.CompanyKey, input: new ProductStageSettingsModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            var subproduct = Lead.GetSubProductSite(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });


            return Json(new { subproduct, prInfo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetLeadActionHistoryList(LeadGenerateActionModel.LeadGenerateHistory data)
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

            var ID_LeadGenerate = data.LeadGenerateProduct;

            var data1 = Common.GetDataViaQuery<LeadGenerateModel.LeadGenerateView>(parameters: new APIParameters
            {
                TableName = "LeadGenerateProduct LGP",
                SelectFields = "LGP.ID_LeadGenerateProduct as LeadGenerateID",
                Criteria = "    LGP.Cancelled=0  AND LGP.FK_LeadGenerate=" + ID_LeadGenerate,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );
            long LeadGenerateProductID = 0;
            if (data1.Data != null)
                LeadGenerateProductID = data1.Data[0].LeadGenerateID;

            LeadGenerateActionModel leadGenerate = new LeadGenerateActionModel();

            var outputList = leadGenerate.GetLeadGenerateActionHistory(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateActionModel.LeadGenerateHistory { LeadGenerateProduct = LeadGenerateProductID, PrductOnly = data.PrductOnly });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProductStageSettingsList(int pageSize, int pageIndex, string Name)
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

            ProductStageSettingsModel ProductStageSettings = new ProductStageSettingsModel();
            var data = ProductStageSettings.GetProductStageSettingsData(input: new ProductStageSettingsModel.InputProductStageSettingsID
            {

                FK_ProductStageSettings = 0,
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
        public ActionResult GetProductStageSettingsInfoByID(ProductStageSettingsModel.ProductStageSettingsView data)
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

            ProductStageSettingsModel ProductStageSettings = new ProductStageSettingsModel();
            var ProductStageSettingsDetails = ProductStageSettings.GetProductStageSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new ProductStageSettingsModel.InputProductStageSettingsID { FK_ProductStageSettings = data.ProductStageSettingsID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


            var EmployeeDetails = ProductStageSettings.GetProductStageSettingsEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new ProductStageSettingsModel.InputProductStageSettingsID { FK_ProductStageSettings = data.ProductStageSettingsID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 1 });


            var MeasurementDetails = ProductStageSettings.GetProductStageSettingsMeasurementData(companyKey: _userLoginInfo.CompanyKey, input: new ProductStageSettingsModel.InputProductStageSettingsID { FK_ProductStageSettings = data.ProductStageSettingsID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 2 });

            var OtherChargeDetails = ProductStageSettings.GetProductStageSettingsOtherChargeData(companyKey: _userLoginInfo.CompanyKey, input: new ProductStageSettingsModel.InputProductStageSettingsID { FK_ProductStageSettings = data.ProductStageSettingsID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 3 });

            var subimg = ProductStageSettings.GetProductStageSettingsImageData(companyKey: _userLoginInfo.CompanyKey, input: new ProductStageSettingsModel.InputProductStageSettingsID { FK_ProductStageSettings = data.ProductStageSettingsID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 4 });
            var imgLst = new List<CompanyImage>();
            if (subimg.Data != null)
            {
                foreach (var dt in subimg.Data)
                {
                    var img = new CompanyImage();
                    img.ComImgMode = dt.SVImgMode;
                    img.ComImgName = dt.SVImgName;
                    img.ComImgValue = dt.SVImgValue;

                    imgLst.Add(img);
                }
                string CompanyImageList = "";
                CompanyImageList = subimg.Data.FirstOrDefault() != null ? Common.xmlTostring(imgLst) : "";
                Session["CompanyImage"] = CompanyImageList;
            }
            return Json(new { ProductStageSettingsDetails, EmployeeDetails, MeasurementDetails, OtherChargeDetails, subimg }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddNewProductStageSettingsDetails(ProductStageSettingsModel.ProductStageSettingsInputFromViewModel data)
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

            ProductStageSettingsModel ProductStageSettings = new ProductStageSettingsModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = ProductStageSettings.UpdateProductStageSettingsData(input: new ProductStageSettingsModel.UpdateProductStageSettings
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "PRSV",
                FK_ProductStageSettings = 0,
                ID_ProductStageSettings = 0,
                FK_LeadGeneration = data.LeadGenerationID,
                SVProductStageSettingsDate = data.VisitDate,
                SVProductStageSettingsTime = data.VisitTime,
                SVInspectionNote1 = data.Note1,
                SVInspectionNote2 = data.Note2,
                SVCustomerNotes = data.CusNote,
                SVExpenseAmount = data.ExpenseAmount,
                SVRemarks = data.Remarks,
                EmployeeDetails = data.EmployeeDetails is null ? "" : Common.xmlTostring(data.EmployeeDetails),
                MeasurementDetails = data.MeasurementDetails is null ? "" : Common.xmlTostring(data.MeasurementDetails),
                OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                ImageList = image
            }, companyKey: _userLoginInfo.CompanyKey);

            Session["CompanyImage"] = string.Empty;
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProductStageSettingsDetails(ProductStageSettingsModel.ProductStageSettingsInputFromViewModel data)
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

            ProductStageSettingsModel ProductStageSettings = new ProductStageSettingsModel();


            string image = (string)Session["CompanyImage"];
            var dataresponse = ProductStageSettings.UpdateProductStageSettingsData(input: new ProductStageSettingsModel.UpdateProductStageSettings
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "PRSV",
                FK_ProductStageSettings = data.ProductStageSettingsID,
                ID_ProductStageSettings = data.ProductStageSettingsID,
                FK_LeadGeneration = data.LeadGenerationID,
                SVProductStageSettingsDate = data.VisitDate,
                SVProductStageSettingsTime = data.VisitTime,
                SVInspectionNote1 = data.Note1,
                SVInspectionNote2 = data.Note2,
                SVCustomerNotes = data.CusNote,
                SVExpenseAmount = data.ExpenseAmount,
                SVRemarks = data.Remarks,
                EmployeeDetails = data.EmployeeDetails is null ? "" : Common.xmlTostring(data.EmployeeDetails),
                MeasurementDetails = data.MeasurementDetails is null ? "" : Common.xmlTostring(data.MeasurementDetails),
                OtherChargeDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                ImageList = image
            }, companyKey: _userLoginInfo.CompanyKey);

            Session["CompanyImage"] = string.Empty;
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteProductStageSettingsInfo(ProductStageSettingsModel.ProductStageSettingsInfoView data)
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

            ProductStageSettingsModel ProductStageSettings = new ProductStageSettingsModel();
            var datresponse = ProductStageSettings.DeleteProductStageSettingsData(input: new ProductStageSettingsModel.DeleteProductStageSettings { FK_ProductStageSettings = data.ProductStageSettingsID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductStageSettingsDeleteReasonList()
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