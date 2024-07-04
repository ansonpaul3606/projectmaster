/*----------------------------------------------------------------------
Created By	: Santhisree
Created On	: 12/07/2022
Purpose		: SiteVisit
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
    public class SiteVisitController : Controller
    {


        // GET: SiteVisit
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
            Common.ClearOtherCharges(ViewBag.TransMode);
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadSiteVisitForm(string mtd)
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            SiteVisitModel.SiteVisitListModel SVList = new SiteVisitModel.SiteVisitListModel();

            //EmployeeTypeList
            SiteVisitModel objpaymode = new SiteVisitModel();
            var rolemodeList = objpaymode.GeLeadStatusList(input: new SiteVisitModel.ModeLead { Mode = 21 }, companyKey: _userLoginInfo.CompanyKey);



            SVList.EmployeeTypeList = rolemodeList.Data;

            var InspectionList = objpaymode.GetInspectionListData(input: new SiteVisitModel.ModeValue { Mode = 106 },
            companyKey: _userLoginInfo.CompanyKey);
            SVList.InspectionListData = InspectionList.Data;

            var departmentList = Common.GetDataViaQuery<SiteVisitModel.Department>(parameters: new APIParameters
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


            var sitevisitTypeList = Common.GetDataViaQuery<SiteVisitModel.MeasurementTypeList>(parameters: new APIParameters
            {
                TableName = "MeasurementType MT",
                SelectFields = "MT.ID_MeasurementType AS MeasurementTypeID,MT.MTName AS[MeasurementType]",
                Criteria = "MT.Cancelled=0 AND MT.Passed=1 AND MT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            SVList.MeasurementTypeList = sitevisitTypeList.Data;

            var sitevisitUnitList = Common.GetDataViaQuery<SiteVisitModel.MeasurementUnitList>(parameters: new APIParameters
            {
                TableName = "MeasurementUnit MU",
                SelectFields = "MU.ID_MeasurementUnit AS MeasurementUnitID,MU.MUName AS[MeasurementUnit]",
                Criteria = "MU.Cancelled=0 AND MU.Passed=1 AND MU.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            SVList.MeasurementUnitList = sitevisitUnitList.Data;

            var WorkTypeList = Common.GetDataViaQuery<SiteVisitModel.WorkTypeList>(parameters: new APIParameters
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
            var CheckListType = Common.GetDataViaQuery<SiteVisitModel.CheckListType>(parameters: new APIParameters
            {
                TableName = "CheckListType CLT",
                SelectFields = " ID_CheckListType, CLTyName",
                Criteria = "CLT.Cancelled = 0  AND CLT.Cancelled=0 AND CLT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = "ID_CheckListType"
            },
          companyKey: _userLoginInfo.CompanyKey

          );
            SVList.CheckListType = CheckListType.Data;

            var CheckList = Common.GetDataViaQuery<SiteVisitModel.CheckList>(parameters: new APIParameters
            {
                TableName = "CheckListSub CLB JOIN CheckList CL ON CL.ID_CheckList =  CLB.FK_CheckList",
                SelectFields = " CLB.FK_CheckListType, CL.ID_CheckList, CL.CkLstName",
                Criteria = "CLB.Cancelled = 0  AND CL.Cancelled=0 AND CL.Passed=1 AND   CL.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = "FK_CheckListType"
            },
           companyKey: _userLoginInfo.CompanyKey

           );
            SVList.CheckList = CheckList.Data;


            SVList.VisitDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            SVList.VisitTime = DateTime.Now.TimeOfDay.ToString();
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            var Sitevisitassigntransmode = Common.GetDataViaQuery<SiteVisitModel.SiteVisitView>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode as SitevisitassignTransMode",
                Criteria = "ControllerName='SiteInspectionAssign'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
    companyKey: _userLoginInfo.CompanyKey);
            string Sitevisit = Sitevisitassigntransmode.Data[0].SitevisitassignTransmode;
            ViewBag.SitevisitassignTransmode = Sitevisit;
            return PartialView("_AddSiteVisit", SVList);
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
            SiteVisitModel siteVisit = new SiteVisitModel();
            var OtherCharges = siteVisit.FillOtherCharges(input: new SiteVisitModel.BindOtherCharge
            {
                TransMode = Data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
               
                EntrBy = _userLoginInfo.EntrBy,


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
            SiteVisitModel SiteVisit = new SiteVisitModel();
            // var prInfo = Lead.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
            var prInfo = Lead1.GetLeadGenerateActionData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateActionModel.LeadFollowup { ID_LeadGenerateAction = LeadGenerateProductID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser });

            //var subproduct = Sit//eVisit.GetSubProduct(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

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
        public ActionResult GetSiteVisitList(int pageSize, int pageIndex, string Name, string TransMode)
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



            SiteVisitModel SiteVisit = new SiteVisitModel();
            var data = SiteVisit.GetSiteVisitData(input: new SiteVisitModel.InputSiteVisitID
            {

                FK_SiteVisit = 0,
                CheckList = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,

            }
            , companyKey: _userLoginInfo.CompanyKey);

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSiteVisitInfoByID(SiteVisitModel.SiteVisitView data)
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

            SiteVisitModel SiteVisit = new SiteVisitModel();
            var SiteVisitDetails = SiteVisit.GetSiteVisitData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = data.SiteVisitID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


            var EmployeeDetails = SiteVisit.GetSiteVisitEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = data.SiteVisitID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 1 });


            var MeasurementDetails = SiteVisit.GetSiteVisitMeasurementData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = data.SiteVisitID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 2 });

            var OtherChargeDetails = SiteVisit.GetSiteVisitOtherChargeData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = data.SiteVisitID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 3 });
            Common.fillOtherCharges(data.TransMode, data.SiteVisitID);

            var Imageselect = SiteVisit.GetSiteVisitImageData(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.InputSiteVisitID { FK_SiteVisit = data.SiteVisitID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 4 });
            if (Imageselect.Data != null)
            {
                foreach (SiteVisitModel.ImageListView itm in Imageselect.Data)
                {
                    if (itm.ProdImage != "" && itm.ProdImage != null)
                    {
                        itm.ProdImage = "data:image/;base64," + itm.ProdImage;
                    }
                }
            }
        
            var CheckList = Common.GetDataViaQuery<SiteVisitModel.CheckList>(parameters: new APIParameters
            {
                TableName = "SiteVisitCheckList S JOIN CheckList CL ON CL.ID_CheckList =  S.FK_CheckList",
                SelectFields = " S.FK_CheckListType, CL.ID_CheckList,CL.CkLstName",
                Criteria = "S.Cancelled = 0  AND S.Cancelled=0 AND  CL.FK_Company=" + _userLoginInfo.FK_Company + " AND FK_SiteVisit=" + data.SiteVisitID,
                GroupByFileds = "",
                SortFields = "FK_CheckListType"
            },
           companyKey: _userLoginInfo.CompanyKey

           );
            //nj
            return Json(new { SiteVisitDetails, EmployeeDetails, MeasurementDetails, OtherChargeDetails, Imageselect, CheckList }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddNewSiteVisitDetails(SiteVisitModel.SiteVisitInputFromViewModel data)
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

            if (data.ImageList != null)
            {
                foreach (CommonSearchPopupModel.ImageListView itm in data.ImageList)
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

            SiteVisitModel sitevisit = new SiteVisitModel();

            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            string image = (string)Session["CompanyImage"];
            var dataresponse = sitevisit.UpdateSiteVisitData(input: new SiteVisitModel.UpdateSiteVisit
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_SiteVisit = 0,
                ID_SiteVisit = 0,
                FK_LeadGeneration = data.LeadGenerationID,
                SVSiteVisitDate = data.VisitDate,
                SVSitevisitTime = data.VisitTime,
                SVInspectionNote1 = data.Note1,
                SVInspectionNote2 = data.Note2,
                SVCustomerNotes = data.CusNote,
                SVExpenseAmount = data.ExpenseAmount,
                SVRemarks = data.Remarks,
                EmployeeDetails = data.EmployeeDetails is null ? "" : Common.xmlTostring(data.EmployeeDetails),
                MeasurementDetails = data.MeasurementDetails is null ? "" : Common.xmlTostring(data.MeasurementDetails),
                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                CheckListSub = data.CheckListSub is null ? "" : Common.xmlTostring(data.CheckListSub),
               SVInspectioncharge = (data.SVExpenseAmount.HasValue) ? data.SVExpenseAmount.Value : 0,
                FK_SiteVisitAssignment = (data.FK_SiteVisitAssignment.HasValue) ? data.FK_SiteVisitAssignment.Value : 0,
                LastID= (data.LastID.HasValue) ? data.LastID.Value : 0,
                InspectionType = data.InspectionType,
            }, companyKey: _userLoginInfo.CompanyKey);
            if (dataresponse.IsProcess){
                Common.ClearOtherCharges(data.TransMode);
            }

                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSiteVisitDetails(SiteVisitModel.SiteVisitInputFromViewModel data)
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
            var imagenull = false;
            if (data.ImageList != null)
            {
                foreach (CommonSearchPopupModel.ImageListView itm in data.ImageList)
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

            SiteVisitModel sitevisit = new SiteVisitModel();
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            string image = (string)Session["CompanyImage"];
            var dataresponse = sitevisit.UpdateSiteVisitData(input: new SiteVisitModel.UpdateSiteVisit
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_SiteVisit = data.SiteVisitID,
                ID_SiteVisit = data.SiteVisitID,
                FK_LeadGeneration = data.LeadGenerationID,
                SVSiteVisitDate = data.VisitDate,
                SVSitevisitTime = data.VisitTime,
                SVInspectionNote1 = data.Note1,
                SVInspectionNote2 = data.Note2,
                SVCustomerNotes = data.CusNote,
                SVExpenseAmount = data.ExpenseAmount,
                SVRemarks = data.Remarks,
                EmployeeDetails = data.EmployeeDetails is null ? "" : Common.xmlTostring(data.EmployeeDetails),
                MeasurementDetails = data.MeasurementDetails is null ? "" : Common.xmlTostring(data.MeasurementDetails),
                OtherChargeDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                CheckListSub = data.CheckListSub is null ? "" : Common.xmlTostring(data.CheckListSub),
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                SVInspectioncharge = (data.SVExpenseAmount.HasValue) ? data.SVExpenseAmount.Value : 0,

                FK_SiteVisitAssignment = (data.FK_SiteVisitAssignment.HasValue) ? data.FK_SiteVisitAssignment.Value : 0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                InspectionType=data.InspectionType,
            }, companyKey: _userLoginInfo.CompanyKey);
            if (dataresponse.IsProcess)
            {
                Common.ClearOtherCharges(data.TransMode);
            }

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteSiteVisitInfo(SiteVisitModel.SiteVisitInfoView data)
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

            SiteVisitModel sitevisit = new SiteVisitModel();
            var datresponse = sitevisit.DeleteSiteVisitData(input: new SiteVisitModel.DeleteSiteVisit { FK_SiteVisit = data.SiteVisitID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSiteVisitDeleteReasonList()
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
        public ActionResult GetChecklistPrint(SiteVisitModel.Inputprintchecklist data)
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

            SiteVisitModel SiteVisit = new SiteVisitModel();
            var checklistcustomerinfo = SiteVisit.GetChecklistcusPrint(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.Inputprintchecklist { FK_SiteVisit = data.FK_SiteVisit, FK_Company = _userLoginInfo.FK_Company, Mode = 1 });
           
            var checklistprintinfo = SiteVisit.GetChecklistPrint(companyKey: _userLoginInfo.CompanyKey, input: new SiteVisitModel.Inputprintchecklist { FK_SiteVisit = data.FK_SiteVisit, FK_Company = _userLoginInfo.FK_Company, Mode = 3 });
            return Json(new { checklistcustomerinfo, checklistprintinfo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetDepartment(int EmployeeID)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<SiteVisitModel.Department>(parameters: new APIParameters
            {
                TableName = " Employee E LEFT JOIN Department Dp on FK_Department=ID_Department",
                SelectFields = " DeptName DepartmentName,ID_Department DepartmentID",
                Criteria = "E.Cancelled =0 AND E.Passed=1 AND E.ID_Employee=" + EmployeeID + " AND E.FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetCheckListData(int InspectionType)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SiteVisitModel.SiteVisitListModel SVList = new SiteVisitModel.SiteVisitListModel();

            var CheckListType = Common.GetDataViaQuery<SiteVisitModel.CheckListType>(parameters: new APIParameters
            {
                TableName = "CheckListType CLT",
                SelectFields = " ID_CheckListType, CLTyName",
                Criteria = "CLT.Cancelled = 0  AND CLT.Cancelled=0 AND CLT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = "ID_CheckListType"
            },
          companyKey: _userLoginInfo.CompanyKey

          );
            SVList.CheckListType = CheckListType.Data;

            var CheckList = Common.GetDataViaQuery<SiteVisitModel.CheckList>(parameters: new APIParameters
            {
                TableName = "CheckListSub CLB JOIN CheckList CL ON CL.ID_CheckList =  CLB.FK_CheckList",
                SelectFields = " CLB.FK_CheckListType, CL.ID_CheckList, CL.CkLstName",
                Criteria = "CLB.Cancelled = 0  AND CL.Cancelled=0 AND CL.Passed=1 AND CL.CkLstInspectionType=" + InspectionType + " AND   CL.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = "FK_CheckListType"
            },
           companyKey: _userLoginInfo.CompanyKey

           );
            SVList.CheckList = CheckList.Data;

            return Json(SVList, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSiteInspectionAssignInfoByID(SiteInspectionAssignModel.SiteInspectionView data)
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

            SiteInspectionAssignModel SiteVisit = new SiteInspectionAssignModel();


           
            var SiteVisitDetails = SiteVisit.GetSiteInspectionAssignData(input: new SiteInspectionAssignModel.InputSiteInspectionID
            {

                FK_SiteVisitAssignment = data.ID_SiteVisitAssignment,
                Detailed = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,

            }
          , companyKey: _userLoginInfo.CompanyKey);
            var EmployeeDetails = SiteVisit.GetSiteInspectionAssignEmployeeData(input: new SiteInspectionAssignModel.InputSiteInspectionID
            {

                FK_SiteVisitAssignment = data.ID_SiteVisitAssignment,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,

            }
            , companyKey: _userLoginInfo.CompanyKey);

            var CheckList = SiteVisit.GetSiteInspectionAssignChecklistData(input: new SiteInspectionAssignModel.InputSiteInspectionID
            {

                FK_SiteVisitAssignment = data.ID_SiteVisitAssignment,
                Detailed = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,

            }
            , companyKey: _userLoginInfo.CompanyKey);
            //nj
            return Json(new { SiteVisitDetails, EmployeeDetails, CheckList }, JsonRequestBehavior.AllowGet);
        }

    }
}