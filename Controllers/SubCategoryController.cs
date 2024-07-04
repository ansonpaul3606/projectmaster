using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
      

        public ActionResult SubCategory()
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

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            return View();
        }

        public ActionResult LoadSubCategoryViewForm()
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            SubCategoryModel.CategoryTypeList module = new SubCategoryModel.CategoryTypeList();
            var LeadType = System.Configuration.ConfigurationManager.AppSettings["Lead"];
            var leadmode = "";
            if (LeadType == "1")
            {
                leadmode = "  ID_ModuleType=4";
            }
            var modeList = Common.GetDataViaQuery<SubCategoryModel.ModuleType>(parameters: new APIParameters
            {
                TableName = "ModuleType",
                SelectFields = "Mode as Mode,ModuleName as ModuleName",
                Criteria = ""+ leadmode,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            module.Modulelist = modeList.Data;

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "SubCategory",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            module.SortOrder = a.Data[0].NextNo;





            return PartialView("_AddSubCategoryform", module);
        }

        [HttpGet]
        public ActionResult GetCategoryList()
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
            var data = Common.GetDataViaQuery<SubCategoryModel.Category>(parameters: new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
                 companyKey: _userLoginInfo.CompanyKey

                    );
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetCategoryList(SubCategoryModel.Module plc)
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
            var CategoryInfo = Common.GetDataViaProcedure<SubCategoryModel.Category, SubCategoryModel.Module>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProModuleTypeCategorySelect", parameter: new SubCategoryModel.Module
            {
                Mode = plc.Mode,
                FK_Company = _userLoginInfo.FK_Company

            });

            return Json(CategoryInfo, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddSubCategory(SubCategoryModel.SubCategoryInputView newcate)
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

            Output _output = new Output();
            //ModelState.Remove("Place");
            ModelState.Remove("SubCategoryID");
            ModelState.Remove("SortOrder");
            ModelState.Remove("SubCatName");
            ModelState.Remove("SubCatShortName");
            ModelState.Remove("CategoryID");
            ModelState.Remove("Mode");
            ModelState.Remove("SortOrder");
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
            if (false)
            {

            }
            SubCategoryModel subCategory = new SubCategoryModel();
            var datresponse = subCategory.UpdateSubCategoryData(input: new SubCategoryModel.UpdateSubCategory
            {

                UserAction = 1,
                //PostID = 0,
                SubCatName = newcate.SubCatName,
                SubCatShortName = newcate.SubCatShortName,
              
                SortOrder = newcate.SortOrder,
                Mode = newcate.Mode,
                FK_Category = newcate.CategoryID,
                EntrBy = _userLoginInfo.EntrBy,
                // EntrOn = DateTime.Now
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                BackupId=0,
                FK_Branch=_userLoginInfo.FK_Branch,
                FK_SubCategory=0,
                ID_SubCategory=0,
                TransMode=""
                
                //BackupId = newpost.PostID,
                //CancelBy = _userLoginInfo.CancelBy,
                //Cancelled = _userLoginInfo.Cancelled,
                // CancelOn = newpost.CancelOn,


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetSubCategoryInfo(SubCategoryModel.SubCategoryInputView data)
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

            ModelState.Remove("SubCatName");
            ModelState.Remove("SubCatShortName");
            ModelState.Remove("CategoryID");
            ModelState.Remove("Mode");
            ModelState.Remove("SortOrder");
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


            SubCategoryModel subcategory = new SubCategoryModel();
            var subCategoryInfo = subcategory.GetSubCategoryData( input: new SubCategoryModel.SubCategoryID {
                EntrBy=_userLoginInfo.EntrBy,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_Company=_userLoginInfo.FK_Company,
                FK_Machine=_userLoginInfo.FK_Machine,
                FK_SubCategory=data.SubCategoryID
            },companyKey:_userLoginInfo.CompanyKey);

            return Json(subCategoryInfo, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult GetSubCategoryList(int pageSize, int pageIndex, string Name)
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

            SubCategoryModel subCategory = new SubCategoryModel();

            var data = subCategory.GetSubCategoryData(companyKey: _userLoginInfo.CompanyKey, input: new SubCategoryModel.SubCategoryID
            {
                FK_SubCategory = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });


            //  return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetSubCategoryList()
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

            //   var data = Common.GetDataViaQuery<ReasonModel.ReasonsView>(parameters: new APIParameters
            //   {
            //       TableName = "Reason AS R JOIN ReasonMode AS RM ON RM.ID_ReasonMode=R.FK_ReasonMode",
            //       SelectFields = "R.[ID_Reason] AS ReasonID,R.[ResnName] AS Reason,R.[ResnShortName] AS ShortName,RM.[ReasonModeName] AS ModeName,R.[SortOrder] AS Sort",
            //       Criteria = "R.Cancelled=0 AND R.Passed=1",
            //       SortFields = "R.SortOrder",
            //       GroupByFileds = ""
            //   },
            // companyKey: _userLoginInfo.CompanyKey
            //);

            SubCategoryModel subCategory = new SubCategoryModel();
            var data = subCategory.GetSubCategoryData(input: new SubCategoryModel.SubCategoryID { FK_SubCategory= 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy,FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,FK_Machine=_userLoginInfo.FK_Machine }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSubCategoryDetails(SubCategoryModel.SubCategoryInputView data)
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

            ModelState.Remove("Mode");
            ModelState.Remove("SubCatName");
            ModelState.Remove("SubCatShortName");
            ModelState.Remove("CategoryID");
            ModelState.Remove("Mode");
            ModelState.Remove("SortOrder");
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

            SubCategoryModel subCategory = new SubCategoryModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;
            

            var dataresponse = subCategory.UpdateSubCategoryData(input: new SubCategoryModel.UpdateSubCategory
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                
                FK_BranchCodeUser = branchUserCode,
                
                BackupId = backupId,
                FK_Company = OrgnCode,

                //UserCode = userCode,
                //AuditData = "",
                //SqlUpdateQuery = "",
                //FK_Reason = 0,
                //BranchCode = branchCode,
                //Cancelled = false,

                EntrBy = entrBy,
                ID_SubCategory = data.SubCategoryID,
                SubCatName = data.SubCatName,
                SubCatShortName = data.SubCatShortName,
                Mode = data.Mode,
                SortOrder = data.SortOrder,
                FK_Branch=_userLoginInfo.FK_Branch,
                FK_Category=data.CategoryID,
                FK_SubCategory=0,
                TransMode=""
                

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubCategoryReasonList()
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
        public ActionResult DeleteSubCategory(SubCategoryModel.SubCategoryInfoView data)
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

            SubCategoryModel subCategory = new SubCategoryModel();

            Output datresponse = subCategory.DeleteSubCategoryData(input: new SubCategoryModel.DeleteSubCategory
            {
                FK_SubCategory = data.SubCategoryID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

                FK_Branch = _userLoginInfo.FK_Branch,
                
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

    }
}