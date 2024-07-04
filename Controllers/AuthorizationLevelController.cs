/*----------------------------------------------------------------------
Created By	: Amritha AK
Created On	: 03/03/2022
Purpose		: AuthorizationLevel
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------

-------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PerfectWebERP.Models;
using System.Data;
using PerfectWebERP.General;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]

    public class AuthorizationLevelController : Controller
    {        
        public ActionResult Index(string mtd)
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.mtd = mtd;
            return View();
        }
        public ActionResult LoadFormAuthorizationLevel(string mtd)
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

            AuthorizationLevelModel objAl = new AuthorizationLevelModel();
            AuthorizationLevelModel.AuthorizationlevelInit authorizationLevelInit = new AuthorizationLevelModel.AuthorizationlevelInit();
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
            authorizationLevelInit.ModuleList = MenuGroupInfo.Data.AsEnumerable();

            var branchtypelist = Common.GetDataViaQuery<AuthorizationLevelModel.BranchTypes>(companyKey: _userLoginInfo.CompanyKey,parameters: new APIParameters
            {
                TableName = "BranchType",
                SelectFields = "ID_BranchType AS BranchTypeID,BTName AS BranchType,FK_BranchMode AS BranchModeID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            });
            authorizationLevelInit.BranchTypelists = branchtypelist.Data.AsEnumerable();

            var userrolelist = Common.GetDataViaQuery<AuthorizationLevelModel.Userrole>(companyKey: _userLoginInfo.CompanyKey,parameters: new APIParameters
            {
                TableName = "UserRole",
                SelectFields = "ID_UserRole AS UserRoleID,UsrrlName AS UserRole",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",
                SortFields = "",
                GroupByFileds = ""
            });
            authorizationLevelInit.UserRolelists = userrolelist.Data.AsEnumerable();

            var amtCriteria = objAl.GetModeList(input: new AuthorizationLevelModel.GetModeData { Mode = 39 }, companyKey: _userLoginInfo.CompanyKey);
            authorizationLevelInit.AmountCriteria = amtCriteria.Data.AsEnumerable();
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddAuthorizationLevel", authorizationLevelInit);
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

            var data = Common.GetDataViaQuery<AuthorizationLevelModel.MenuGroups>(parameters: new APIParameters
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

            var data = Common.GetDataViaQuery<AuthorizationLevelModel.MenuLists>(parameters: new APIParameters
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
        public ActionResult getUserList(AuthorizationLevelModel.Usersrelated data)
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
            AuthorizationLevelModel objAuth = new AuthorizationLevelModel();
            var dataresponse=objAuth.GetAuthorizationlevelUsersnamelist(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.Usersrelated
            {
                FK_BranchMode = 0,
                FK_UserRole = data.FK_UserRole,
                FK_Company = _userLoginInfo.FK_Company
            });    
            return Json(dataresponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAuthorizationLevelList(int pageSize, int pageIndex, string Name)
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

            AuthorizationLevelModel objAulevel = new AuthorizationLevelModel();

            var data = objAulevel.GetAuthorizationLevelData(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationLevelID
            {
                FK_AuthorizationLevel = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = "",
                Name = Name
            });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

      

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetAuthorizationLeveInfo(AuthorizationLevelModel.AuthorizationLevelView authorizationlevelInfo)
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
            AuthorizationLevelModel authorizationlevel = new AuthorizationLevelModel();
            var alInfo = authorizationlevel.GetAuthorizationLevelData(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationLevelID
            {
                FK_AuthorizationLevel = authorizationlevelInfo.AuthorizationLevelID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy
            });
            var subtable = authorizationlevel.GetSubAuthorizationLevelData(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationLevelSubSelect
            {
                FK_AuthorizationLevel = authorizationlevelInfo.AuthorizationLevelID,
                FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(new { alInfo, subtable }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewAuthorizationLevel(AuthorizationLevelModel.AuthorizationLevelNew data)
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
            AuthorizationLevelModel AuthorizationLevel = new AuthorizationLevelModel();         

            var datresponse = AuthorizationLevel.UpdateAuthorizationLevelData(input: new AuthorizationLevelModel.UpdateAuthorizationLevel
            {
                UserAction = 1,
                AuthEffectDate = data.AuthEffectDate,
                FK_MenuGroup = data.FK_MenuGroup,
                FK_MenuList = data.FK_MenuList,               
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_AuthorizationLevel = 0,
                SkipPreviousLevel = data.SkipPreviousLevel,
                ActiveCorrectionOption = data.ActiveCorrectionOption,
                SubAuthorizationLevelDetails = data.SubAuthorizationLevel is null ? "" : Common.xmlTostring(data.SubAuthorizationLevel),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateAuthorizationLevel(AuthorizationLevelModel.AuthorizationLevelNew data)
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
            AuthorizationLevelModel AuthorizationLevel = new AuthorizationLevelModel();

            var datresponse = AuthorizationLevel.UpdateAuthorizationLevelData(input: new AuthorizationLevelModel.UpdateAuthorizationLevel
            {
                UserAction = 2,
                AuthEffectDate = data.AuthEffectDate,
                FK_MenuGroup = data.FK_MenuGroup,
                FK_MenuList = data.FK_MenuList,               
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_AuthorizationLevel = data.ID_AuthorizationLevel,
                SkipPreviousLevel = data.SkipPreviousLevel,
                ActiveCorrectionOption = data.ActiveCorrectionOption,
                SubAuthorizationLevelDetails = data.SubAuthorizationLevel is null ? "" : Common.xmlTostring(data.SubAuthorizationLevel),
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteAuthorizationLevel(AuthorizationLevelModel.AuthorizationLevelInfoView data)
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
            AuthorizationLevelModel AuthorizationLevel = new AuthorizationLevelModel();
            Output datresponse = AuthorizationLevel.DeleteAuthorizationLevelData(input: new AuthorizationLevelModel.DeleteAuthorizationLevel
            {
                FK_AuthorizationLevel = data.ID_AuthorizationLevel,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = "",
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAuthorizationLevelReasonList()
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
        public ActionResult List()
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }
        public ActionResult LoadAuthorizationCard()
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
            AuthorizationLevelModel objAuth = new AuthorizationLevelModel();

            var AuthModule = objAuth.GetAuthorizationModuleData(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationModuleSelect
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_User = _userLoginInfo.ID_Users,
                FK_UserGroup = _userLoginInfo.FK_UserRole
            });
            AuthorizationLevelModel.AuthorizationModuleVM objVMData = new AuthorizationLevelModel.AuthorizationModuleVM();
            objVMData.AMData = AuthModule.Data.Where(a=>a.NoofRecords!=0).AsEnumerable();
         
            return PartialView("_LoadAuthorizationCard", objVMData);
        }
        public ActionResult LoadAuthorizationList(string module)
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
            ViewBag.Module = module;
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            AuthorizationLevelModel objAuth = new AuthorizationLevelModel();
            var AuthInfo = objAuth.GetAuthorizationListData(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationListSelect
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_User = _userLoginInfo.ID_Users,
                FK_UserGroup = _userLoginInfo.FK_UserRole,
                Module = module
            });
            ViewBag.ListData = AuthInfo.Data;
            if (module== "AWAIT")
            {
                return PartialView("_LoadAuthorizationAwaitList");
            }
            return PartialView("_LoadAuthorizationList");

        }
       
        public ActionResult LoadAuthorizationAction(string authID,string module,int skip=0)
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
            AuthorizationLevelModel objAuth = new AuthorizationLevelModel();

            var AuthAction = objAuth.GetAuthorizationActionData(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationActionSelect
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_User = _userLoginInfo.ID_Users,
                FK_UserGroup = _userLoginInfo.FK_UserRole,
                AuthID= Convert.ToInt32(authID),
                Module = module,
            });
           
            ViewBag.authID = authID;
            ViewBag.module = module;
            ViewBag.TransactionDetails = AuthAction.Data[0].TransactionDetails;
            ViewBag.PartyDetails = AuthAction.Data[0].PartyDetails;
            ViewBag.SubTitle = AuthAction.Data[0].SubTitle;
            ViewBag.SubDetails = Convert.ToBoolean(AuthAction.Data[0].SubDetails);
            ViewBag.FooterLeft = AuthAction.Data[0].FooterLeft;
            ViewBag.FooterRight = AuthAction.Data[0].FooterRight;
            ViewBag.ActiveCorrectionOption = AuthAction.Data[0].ActiveCorrectionOption;
            ViewBag.Skip = skip;
            ReasonModel reason = new ReasonModel();
            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });
            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            AuthorizationLevelModel.AuthorizationActionVM obj = new AuthorizationLevelModel.AuthorizationActionVM();
            obj.Reason = deleteReason.Data.AsEnumerable();

            if(Convert.ToBoolean(AuthAction.Data[0].SubDetails))
            {
                var AuthInfo = objAuth.GetAuthorizationListDetails(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationListDetails
                {
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Master = AuthAction.Data[0].FK_Master,
                    TransMode = AuthAction.Data[0].TransMode,
                    Mode = AuthAction.Data[0].Mode
                });
                ViewBag.ListData = AuthInfo.Data;
            }
            
           
            return PartialView("_LoadAuthorizationAction", obj);
        }
        [HttpPost]
        public ActionResult GetAuthorizationList(string module)
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
            AuthorizationLevelModel objAuth = new AuthorizationLevelModel();

            var AuthInfo = objAuth.GetAuthorizationListData(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationListSelect
            {               
                FK_Company = _userLoginInfo.FK_Company,
                FK_User = _userLoginInfo.ID_Users,    
                FK_UserGroup=_userLoginInfo.FK_UserRole,
                Module= module
            });

          

            return Json(AuthInfo, JsonRequestBehavior.AllowGet);           
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Approved(int AuthID,int SkipPrev)
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
            AuthorizationLevelModel AuthorizationLevel = new AuthorizationLevelModel();

            var datresponse = AuthorizationLevel.UpdateAuthorizationApproveData(input: new AuthorizationLevelModel.UpdateAuthorizationApprove
            {              
                FK_Company = _userLoginInfo.FK_Company,               
                EntrBy = _userLoginInfo.EntrBy,
                FK_AuthorizationData = AuthID,
                SkipPrev= SkipPrev,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult RejectAction(Int64 AuthID,string RejectReson,long FK_Reason, int SkipPrev)
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
            AuthorizationLevelModel AuthorizationLevel = new AuthorizationLevelModel();

            var datresponse = AuthorizationLevel.UpdateAuthorizationRejectData(input: new AuthorizationLevelModel.UpdateAuthorizationReject
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_AuthorizationData = AuthID,
                Reason= RejectReson,
                FK_Reason=FK_Reason,
                SkipPrev = SkipPrev
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAuthorizationAwaitHistory(long FK_TransMaster,string TransMode)
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
            AuthorizationLevelModel objAuth = new AuthorizationLevelModel();

            var AuthInfo = objAuth.GetAuthorizationAwaitHistoryData(companyKey: _userLoginInfo.CompanyKey, input: new AuthorizationLevelModel.AuthorizationAwaitHistorySelect
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_TransMaster = FK_TransMaster,
                TransMode = TransMode
            });



            return Json(AuthInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Correction(Int64 AuthID, string CorrectionReson, long FK_Reason, int SkipPrev)
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
            AuthorizationLevelModel AuthorizationLevel = new AuthorizationLevelModel();

            var datresponse = AuthorizationLevel.UpdateAuthorizationCorrectionData(input: new AuthorizationLevelModel.UpdateAuthorizationCorrection
            {
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_AuthorizationData = AuthID,
                Reason = CorrectionReson,
                FK_Reason = FK_Reason,
                SkipPrev = SkipPrev
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}




