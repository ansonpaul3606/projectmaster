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
using System.IO;

namespace PerfectWebERP.Controllers
{
    public class DiscountAuthorizationController : Controller
    {
       
        public ActionResult DiscountAuthorization(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            CommonMethod cmnmethd = new CommonMethod();
            string mGrp = cmnmethd.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            return View();
        }

        public ActionResult LoadDiscountAuthorization(string mtd)
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

            DiscountAuthorizationModel.DiscountAuthorizationViewList DisAuth = new DiscountAuthorizationModel.DiscountAuthorizationViewList();

            DiscountAuthorizationModel dis = new DiscountAuthorizationModel();
            MenuGroupModel MenuGroup = new MenuGroupModel();
            var MenuGroupInfo = MenuGroup.GetMenuGroupData(companyKey: _userLoginInfo.CompanyKey, input: new MenuGroupModel.GetMenuGroup
            {
                FK_MenuGroup = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Name = ""
            });
            DisAuth.ModuleList = MenuGroupInfo.Data.AsEnumerable();
            var Category = Common.GetDataViaQuery<DiscountAuthorizationModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS CategoryID ,CatName AS CategoryName, Project",
                Criteria = "Cancelled=0 AND Passed=1  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            DisAuth.CategoryList = Category.Data;
            //var ComplainttList = Common.GetDataViaQuery<DiscountAuthorizationModel.ComplaintList>(parameters: new APIParameters
            //{
            //    TableName = "ComplaintList C",
            //    SelectFields = "C.ID_ComplaintList as ComplaintListID,C.CompntName as Complaint",
            //    Criteria = "C.Cancelled=0",
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            // companyKey: _userLoginInfo.CompanyKey
            // );

            //DisAuth.ComplaintLists = ComplainttList.Data;
            var userrolelist = Common.GetDataViaQuery<DiscountAuthorizationModel.Userrole>(companyKey: _userLoginInfo.CompanyKey, parameters: new APIParameters
            {
                TableName = "UserRole",
                SelectFields = "ID_UserRole AS UserRoleID,UsrrlName AS UserRole",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",
                SortFields = "",
                GroupByFileds = ""
            });
            DisAuth.UserRolelists = userrolelist.Data.AsEnumerable();

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_LoadDiscountAuthorization", DisAuth);
        }

        public ActionResult GetSubcategory(DiscountAuthorizationModel.Category cr)
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
            var SubcategoryInfo = Common.GetDataViaQuery<DiscountAuthorizationModel.Subcategory>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetMenuGroupList()
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

            var data = Common.GetDataViaQuery<DiscountAuthorizationModel.MenuGroups>(parameters: new APIParameters
            {
                TableName = "MenuGroup",
                SelectFields = "ID_MenuGroup AS MenuGroupID,MnuGrpName AS MenuGroup",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult getMenuList(Int32 menugroupid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<DiscountAuthorizationModel.MenuLists>(parameters: new APIParameters
            {

                TableName = "MenuList",
                SelectFields = "ID_MenuList AS MenuListID, MnuLstName AS MenuList",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_MenuGroup='" + menugroupid + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult getUserList(DiscountAuthorizationModel.Usersrelated data)
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
            DiscountAuthorizationModel objAuth = new DiscountAuthorizationModel();
            var dataresponse = objAuth.GetAuthorizationlevelUsersnamelist(companyKey: _userLoginInfo.CompanyKey, input: new DiscountAuthorizationModel.Usersrelated
            {
                FK_BranchMode = 0,
                FK_UserRole = data.FK_UserRole,
                FK_Company = _userLoginInfo.FK_Company
            });
            return Json(dataresponse, JsonRequestBehavior.AllowGet);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddDiscountAuthorization(DiscountAuthorizationModel.DiscountAuthorizationView data)
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


            DiscountAuthorizationModel model = new DiscountAuthorizationModel();
            var datresponse = model.UpdateDiscountData(input: new DiscountAuthorizationModel.UpdateDiscountAuthorization
            {
                UserAction = 1,
                TransMode = data.TransMode,
                ID_DiscountAuthorizationSettings = data.ID_DiscountAuthorizationSettings,
                EffectDate = data.EffectDate,
                FK_MenuGroup = data.FK_MenuGroup,
                FK_MenuList=data.FK_MenuList,
                FK_Category = data.FK_Category,
                FK_SubCategory = data.FK_SubCategory,
                FK_Product = data.FK_Product,             
               // FK_ComplaintList = data.FK_ComplaintList,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_DiscountAuthorizationSettings = data.FK_DiscountAuthorizationSettings,
                SkipPreviousLevel=data.SkipPreviousLevel,
                ActiveCorrectionOption=data.ActiveCorrectionOption,                
        DetailsView = data.DetailsView is null ? "" : Common.xmlTostring(data.DetailsView),
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateDiscountAuthorization(DiscountAuthorizationModel.DiscountAuthorizationView data)
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


            DiscountAuthorizationModel model = new DiscountAuthorizationModel();
            var datresponse = model.UpdateDiscountData(input: new DiscountAuthorizationModel.UpdateDiscountAuthorization
            {
                UserAction = 2,
                TransMode = data.TransMode,
                ID_DiscountAuthorizationSettings = data.ID_DiscountAuthorizationSettings,
                EffectDate = data.EffectDate,
                FK_MenuGroup = data.FK_MenuGroup,
                FK_MenuList = data.FK_MenuList,
                FK_Category = data.FK_Category,
                FK_SubCategory = data.FK_SubCategory,
                FK_Product = data.FK_Product,
               // FK_ComplaintList = data.FK_ComplaintList,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_DiscountAuthorizationSettings = data.FK_DiscountAuthorizationSettings,
                SkipPreviousLevel = data.SkipPreviousLevel,
                ActiveCorrectionOption = data.ActiveCorrectionOption,
                DetailsView = data.DetailsView is null ? "" : Common.xmlTostring(data.DetailsView),
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DiscountAuthorizationList(int pageSize, int pageIndex, string Name, string TransModes)
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

            ModelState.Remove("ReasonID");

            if (!ModelState.IsValid)
            {
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
            DiscountAuthorizationModel Discounttype = new DiscountAuthorizationModel();

            var DiscounttypeInfo = Discounttype.GetDiscountAuthorizationData(companyKey: _userLoginInfo.CompanyKey, input: new DiscountAuthorizationModel.DiscountAuthorizationID

            {
                FK_DiscountAuthorizationSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //Search= Name,
                TransMode = TransModes
            });

            //  return Json(EmployeetransferInfo, JsonRequestBehavior.AllowGet);
            return Json(new { DiscounttypeInfo.Process, DiscounttypeInfo.Data, pageSize, pageIndex, totalrecord = (DiscounttypeInfo.Data is null) ? 0 : DiscounttypeInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult GetDiscountAuthorizationInfo(DiscountAuthorizationModel.DiscountAuthorizationView data)
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


            DiscountAuthorizationModel objprd = new DiscountAuthorizationModel();

            var mptableInfo = objprd.GetDiscountData(companyKey: _userLoginInfo.CompanyKey, input: new DiscountAuthorizationModel.DiscountAuthorizationID
            {
                FK_DiscountAuthorizationSettings = data.ID_DiscountAuthorizationSettings,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode
            });
            var subtable = objprd.GetDiscountDatanew(companyKey: _userLoginInfo.CompanyKey, input: new DiscountAuthorizationModel.DiscountAuthorizationID
            {
                FK_DiscountAuthorizationSettings = data.ID_DiscountAuthorizationSettings,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode

            });



            return Json(new { mptableInfo, subtable }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteDiscountAuthorization(DiscountAuthorizationModel.DiscountAuthorizationView data)
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

            if (!ModelState.IsValid)
            {
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

            DiscountAuthorizationModel Material = new DiscountAuthorizationModel();


            Output datresponse = Material.DeleteDiscountAuthorizationData(input: new DiscountAuthorizationModel.DeleteDiscountType
            {
                FK_DiscountAuthorizationSettings = data.ID_DiscountAuthorizationSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = data.TransMode,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetResourceReasonList()
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