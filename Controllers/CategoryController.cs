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
    public class CategoryController : Controller
    {
        public ActionResult Category()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.Companycategory = _userLoginInfo.CompCategory;
            //CommonMethod objCmnMethod = new CommonMethod();
            //string mTd = objCmnMethod.Decrypt(mtd);
            //TempData["mTd"] = mTd.ToString();
            return View();
        }
        public ActionResult LoadCategoryForm()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            CategoryModel.CategoryListModel CategoryList = new CategoryModel.CategoryListModel();
            var LeadType = System.Configuration.ConfigurationManager.AppSettings["Lead"];
            var leadmode = "";
            if (LeadType == "1")
            {
                leadmode = " ID_ModuleType=4";
            }

            var CatList = Common.GetDataViaQuery<CategoryModel.ModeList>(companyKey: _userLoginInfo.CompanyKey,parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeName,M.Mode",
                Criteria = "" + leadmode,
                GroupByFileds = "",
                SortFields = ""
            });

            CategoryList.ModeList = CatList.Data;
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "Category",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            CategoryList.SortOrder = a.Data[0].NextNo;

            ViewBag.Companycategory = _userLoginInfo.CompCategory;
           // ViewBag.PageTitle = TempData["mTd"] as string;

            return PartialView("_AddCategory", CategoryList);
        }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult GetCategoryList(int pageSize, int pageIndex, string Name,string Transmode)
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

            CategoryModel Category = new CategoryModel();

            var data = Category.GetCategoryData(companyKey: _userLoginInfo.CompanyKey, input: new CategoryModel.GetCategory
            {
                FK_Category = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = Transmode
            });
           

          //  return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCategoryInfoByID(CategoryModel.CategoryInfoView data)
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

            CategoryModel CategoryType = new CategoryModel();
            var CategoryInfo = CategoryType.GetCategoryData(companyKey: _userLoginInfo.CompanyKey, input: new CategoryModel.GetCategory
            {
                FK_Category = data.CategoryID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 1,
                PageSize = 10,
                TransMode = ""
            });

            return Json(CategoryInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewCategoryDetails(CategoryModel.CategoryInputFromViewModel data)
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
            ModelState.Remove("AccountHeadID");
            ModelState.Remove("AccountHeadSubID");
            ModelState.Remove("Project");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ProdSales");
            ModelState.Remove("ProdSalesReturn");
            ModelState.Remove("ProdStockTransfer");
            ModelState.Remove("ProdProductionIn");
            ModelState.Remove("ProdProductionOut");
            ModelState.Remove("ProdPurchaseReturn");

            #region :: Model validation  ::



            //removing a node in model while validating
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
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

            CategoryModel Category = new CategoryModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string EntrBy = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = Category.UpdateCategoryData(input: new CategoryModel.UpdateCategory
            {

                //AuditData = "",
                //SqlUpdateQuery = "",
                //FK_Reason = 0,
                //Cancelled = false,
                //BranchCode = branchCode,
                //UserCode = userCode,
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                BackupId = backupId,
                FK_Company = OrgnCode,
                EntrBy = entrBy,
                ID_Category = 0,
                CatName = data.Category,
                Project = data.Project,
                CatShortName = data.ShortName,
                FK_AccountHead = (data.AccountHeadID.HasValue) ? data.AccountHeadID.Value : 0,
                FK_AccountHeadSub = (data.AccountHeadSubID.HasValue) ? data.AccountHeadSubID.Value : 0,
                TransMode=data.TransMode,
                //FK_AccountHead = data.AccountHeadID,
                //FK_AccountHeadSub = data.AccountHeadSubID,
                SortOrder = data.SortOrder,
                Mode = data.Mode,
                FK_Category = 0,
                FK_Branch = 0,
                ProdSales = data.ProdSales,
                ProdSalesReturn = data.ProdSalesReturn,
                ProdPurchase = data.ProdPurchase,
                ProdPurchaseReturn = data.ProdPurchaseReturn,
                ProdStockTransfer = data.ProdStockTransfer,
                ProdProductionIn = data.ProdProductionIn,
                ProdProductionOut = data.ProdProductionOut,
                ProdLead = data.ProdLead,
                ProdProject = data.ProdProject


            }, companyKey: _userLoginInfo.CompanyKey);
            Output output = new Output();

            if (dataresponse.Data.FirstOrDefault().Column1 > 0)
            {

                output.IsProcess = true;
                output.Message = new List<string> { "Saved Successfully" };

            }
            else
            {
                output.Message = new List<string> { dataresponse.Data.FirstOrDefault().ErrMsg };
                output.code = dataresponse.Data.FirstOrDefault().ErrCode;
                output.IsProcess = false;
            }

            return Json(new { Process = output }, JsonRequestBehavior.AllowGet);

          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCategoryDetails(CategoryModel.CategoryInputFromViewModel data)
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

            ModelState.Remove("SortOrder");
            ModelState.Remove("Project");

            ModelState.Remove("ProdSales");
            ModelState.Remove("ProdSalesReturn");
            ModelState.Remove("ProdStockTransfer");
            ModelState.Remove("ProdProductionIn");
            ModelState.Remove("ProdProductionOut");
            ModelState.Remove("ProdPurchaseReturn");

            #region :: Model validation  ::



            //removing a node in model while validating
            ModelState.Remove("Interstate");
            ModelState.Remove("Intrastate");
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

            CategoryModel Category = new CategoryModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string EntrBy = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = Category.UpdateCategoryData(input: new CategoryModel.UpdateCategory
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Branch = branchCode,
                BackupId = backupId,
                FK_Company = OrgnCode,
                FK_Reason = 0,
                EntrBy = entrBy,
                // Cancelled = false,
                ID_Category = data.CategoryID,
                CatName = data.Category,
                CatShortName = data.ShortName,
                Mode = data.Mode,
                //FK_AccountHead = data.AccountHeadID,
                //FK_AccountHeadSub = data.AccountHeadSubID,
                FK_AccountHead = (data.AccountHeadID.HasValue) ? data.AccountHeadID.Value : 0,
                FK_AccountHeadSub = (data.AccountHeadSubID.HasValue) ? data.AccountHeadSubID.Value : 0,
                TransMode = data.TransMode,
                SortOrder = data.SortOrder,
                FK_Category= data.CategoryID,
                Project = data.Project,
                Passed =true,

                ProdPurchase = data.ProdPurchase,
                ProdSales = data.ProdSales,
                ProdSalesReturn = data.ProdSalesReturn,
                ProdPurchaseReturn = data.ProdPurchaseReturn,
                ProdStockTransfer = data.ProdStockTransfer,
                ProdProductionIn = data.ProdProductionIn,
                ProdProductionOut = data.ProdProductionOut,
                ProdLead = data.ProdLead,
                ProdProject = data.ProdProject

            }, companyKey: _userLoginInfo.CompanyKey);


            Output output = new Output();

            if (dataresponse.Data.FirstOrDefault().Column1 > 0)
            {

                output.IsProcess = true;
                output.Message = new List<string> { "Updated Successfully" };

            }
            else
            {
                output.Message = new List<string> { dataresponse.Data.FirstOrDefault().ErrMsg };
                output.code = dataresponse.Data.FirstOrDefault().ErrCode;
                output.IsProcess = false;
            }

            return Json(new { Process = output }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteCategoryInfo(CategoryModel.CategoryInfoView data)
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
            ModelState.Remove("CategoryName");
            ModelState.Remove("ShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("Project");
            // ModelState.Remove("States");
            ModelState.Remove("ModeID");
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

            CategoryModel Category = new CategoryModel();
            var datresponse = Category.DeleteCategoryData(input: new CategoryModel.DeleteCategory
            {
                FK_Category = data.CategoryID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID,
                FK_Branch= _userLoginInfo.FK_Branch,
                FK_BranchCodeUser= _userLoginInfo.FK_BranchCodeUser
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccountHeadDetails()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CategoryModel.AccountHeadView>(parameters: new APIParameters
            {
                TableName = "AccountHead AH",
                SelectFields = "AH.ID_AccountHead AS AccountHeadID,AH.AHName AS AHeadName",
                Criteria = "AH.Cancelled =0 AND AH.Passed=1",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAccountSubHeadDetails(int AccountHeadID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CategoryModel.AccountSubHeadView>(parameters: new APIParameters
            {
                TableName = "AccountSubHead ASH",
                SelectFields = "ASH.ID_AccountSubHead AS AccountHeadSubID,ASH.ASHName AS ASHeadName",
                Criteria = "ASH.Cancelled =0 AND ASH.Passed=1 AND ASH.AHCode=" + AccountHeadID,
                SortFields = "",
                GroupByFileds = ""
            },

            //blpopulate.TableName = "AccountHeadSub A JOIN Customer C ON A.FK_Customer=C.ID_Customer";
            //blopulate.ListFields = "A.ID_AccountHeadSub,A.AhsNumber,C.Cusname";
            //blpopulate.SortFields = "ID_AccountHeadSub,C.Cusname";
            //blpopulate.Criteria = "Cancelled=0 AND A.AhCode=" + AhCode;
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetCategoryReasonList()
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