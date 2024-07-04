
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
    public class SiteInspectionAssignController : Controller
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

        public ActionResult LoadSiteInspectionAssignForm(string mtd)
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            SiteInspectionAssignModel.SiteInspectionModel SVList = new SiteInspectionAssignModel.SiteInspectionModel();

            //EmployeeTypeList
            SiteInspectionAssignModel objpaymode = new SiteInspectionAssignModel();
            var rolemodeList = objpaymode.GeLeadStatusList(input: new SiteInspectionAssignModel.ModeLead { Mode = 21 }, companyKey: _userLoginInfo.CompanyKey);



            SVList.EmployeeTypeList = rolemodeList.Data;

            var InspectionList = objpaymode.GetInspectionListData(input: new SiteInspectionAssignModel.ModeValue { Mode = 106 },
            companyKey: _userLoginInfo.CompanyKey);
            SVList.InspectionListData = InspectionList.Data;

            var departmentList = Common.GetDataViaQuery<SiteInspectionAssignModel.Department>(parameters: new APIParameters
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


       
        
            var CheckListType = Common.GetDataViaQuery<SiteInspectionAssignModel.CheckListType>(parameters: new APIParameters
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

            var CheckList = Common.GetDataViaQuery<SiteInspectionAssignModel.CheckList>(parameters: new APIParameters
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
            return PartialView("AddSiteInspectionAssign", SVList);
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
        public ActionResult GetSiteInspectionAssignList(int pageSize, int pageIndex, string Name, string TransMode)
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



            SiteInspectionAssignModel SiteVisit = new SiteInspectionAssignModel();
            var data = SiteVisit.GetSiteInspectionAssignData(input: new SiteInspectionAssignModel.InputSiteInspectionID
            {

                FK_SiteVisitAssignment = 0,
                Detailed = 0,
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


            // var EmployeeDetails = SiteVisit.GetSiteInspectionAssignEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new SiteInspectionAssignModel.InputSiteInspectionID { FK_SiteInspectionAssign = data.SiteInspectionAssignID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "", CheckList = 1 });



           // var CheckList = Common.GetDataViaQuery<SiteVisitModel.CheckList>(parameters: new APIParameters
           // {
           //     TableName = "SiteVisitCheckList S JOIN CheckList CL ON CL.ID_CheckList =  S.FK_CheckList",
           //     SelectFields = " S.FK_CheckListType, CL.ID_CheckList,CL.CkLstName",
           //     Criteria = "S.Cancelled = 0  AND S.Cancelled=0 AND  CL.FK_Company=" + _userLoginInfo.FK_Company + " AND FK_SiteVisit=" + data.SiteInspectionAssignID,
           //     GroupByFileds = "",
           //     SortFields = "FK_CheckListType"
           // },
           //companyKey: _userLoginInfo.CompanyKey

           //);
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewSiteInspectionAssignDetails(SiteInspectionAssignModel.SiteInspectionInputFromViewModel data)
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

            SiteInspectionAssignModel sitevisit = new SiteInspectionAssignModel();

           

          
            var dataresponse = sitevisit.UpdateSiteInspectionAssignData(input: new SiteInspectionAssignModel.UpdateSiteInspection
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_SiteVisitAssignment = 0,
                ID_SiteVisitAssignment = 0,
                FK_LeadGeneration = data.LeadGenerationID,
                SVSiteVisitDate = data.VisitDate,
                SVSitevisitTime = data.VisitTime,
                SVInspectionNote1 = data.Note1,
                SVInspectionNote2 = data.Note2,
                Clear = data.Clear,            
                SVExpenseAmount = (data.SVExpenseAmount.HasValue) ? data.SVExpenseAmount.Value : 0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                EmployeeDetails = data.EmployeeDetails is null ? "" : Common.xmlTostring(data.EmployeeDetails),
             
                CheckListSub = data.CheckListSub is null ? "" : Common.xmlTostring(data.CheckListSub),
                InspectionType = data.InspectionType,
            }, companyKey: _userLoginInfo.CompanyKey);
            if (dataresponse.IsProcess)
            {
                Common.ClearOtherCharges(data.TransMode);
            }
            try
            {
                Common.SendMobileNotification(companyKey: _userLoginInfo.CompanyKey);
            }
            catch (Exception ex)
            {
            }
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSiteInspectionAssignDetails(SiteInspectionAssignModel.SiteInspectionInputFromViewModel data)
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

            SiteInspectionAssignModel sitevisit = new SiteInspectionAssignModel();
            var otherCharge = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            string image = (string)Session["CompanyImage"];
            var dataresponse = sitevisit.UpdateSiteInspectionAssignData(input: new SiteInspectionAssignModel.UpdateSiteInspection
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_SiteVisitAssignment = data.SiteInspectionAssignID,
                ID_SiteVisitAssignment = data.SiteInspectionAssignID,
                FK_LeadGeneration = data.LeadGenerationID,
                SVSiteVisitDate = data.VisitDate,
                SVSitevisitTime = data.VisitTime,
                SVInspectionNote1 = data.Note1,
                SVInspectionNote2 = data.Note2,
              
                SVExpenseAmount = (data.SVExpenseAmount.HasValue) ? data.SVExpenseAmount.Value : 0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                EmployeeDetails = data.EmployeeDetails is null ? "" : Common.xmlTostring(data.EmployeeDetails),
              

              
                CheckListSub = data.CheckListSub is null ? "" : Common.xmlTostring(data.CheckListSub),
                InspectionType = data.InspectionType,

            }, companyKey: _userLoginInfo.CompanyKey);
            if (dataresponse.IsProcess)
            {
                Common.ClearOtherCharges(data.TransMode);
            }
            try
            {
                Common.SendMobileNotification(companyKey: _userLoginInfo.CompanyKey);
            }
            catch (Exception ex)
            {
            }
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteSiteInspectionAssignInfo(SiteInspectionAssignModel.SiteInspectionInfoView data)
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

            SiteInspectionAssignModel sitevisit = new SiteInspectionAssignModel();
            var datresponse = sitevisit.DeleteSiteInspectionAssignData(input: new SiteInspectionAssignModel.DeleteSiteInspection { FK_SiteVisitAssignment = data.SiteInspectionAssignID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = data.TransMode }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSiteInspectionAssignDeleteReasonList()
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
        public ActionResult GetChecklistPrint(SiteInspectionAssignModel.Inputprintchecklist data)
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

            SiteInspectionAssignModel SiteVisit = new SiteInspectionAssignModel();
            var checklistcustomerinfo = SiteVisit.GetChecklistcusPrint(companyKey: _userLoginInfo.CompanyKey, input: new SiteInspectionAssignModel.Inputprintchecklist { FK_SiteVisitAssignment = data.FK_SiteVisitAssignment, FK_Company = _userLoginInfo.FK_Company, Mode = 1 });

            var checklistprintinfo = SiteVisit.GetChecklistPrint(companyKey: _userLoginInfo.CompanyKey, input: new SiteInspectionAssignModel.Inputprintchecklist { FK_SiteVisitAssignment = data.FK_SiteVisitAssignment, FK_Company = _userLoginInfo.FK_Company, Mode = 3 });
            return Json(new { checklistcustomerinfo, checklistprintinfo }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetDepartment(int EmployeeID)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<SiteInspectionAssignModel.Department>(parameters: new APIParameters
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
            SiteInspectionAssignModel.SiteInspectionModel SVList = new SiteInspectionAssignModel.SiteInspectionModel();

            var CheckListType = Common.GetDataViaQuery<SiteInspectionAssignModel.CheckListType>(parameters: new APIParameters
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

            var CheckList = Common.GetDataViaQuery<SiteInspectionAssignModel.CheckList>(parameters: new APIParameters
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


        public ActionResult GetChecklistPrints(SiteInspectionAssignModel.Inputprintchecklist data)
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

            SiteInspectionAssignModel SiteVisit = new SiteInspectionAssignModel();
            var checklistcustomerinfo = SiteVisit.GetChecklistcusPrint(companyKey: _userLoginInfo.CompanyKey, input: new SiteInspectionAssignModel.Inputprintchecklist { @FK_SiteVisitAssignment = data.FK_SiteVisitAssignment ,FK_Company = _userLoginInfo.FK_Company, Mode = 1 });

            var checklistprintinfo = SiteVisit.GetChecklistPrint(companyKey: _userLoginInfo.CompanyKey, input: new SiteInspectionAssignModel.Inputprintchecklist { @FK_SiteVisitAssignment = data.FK_SiteVisitAssignment, FK_Company = _userLoginInfo.FK_Company, Mode = 3 });
            return Json(new { checklistcustomerinfo, checklistprintinfo }, JsonRequestBehavior.AllowGet);
        }
    }
}