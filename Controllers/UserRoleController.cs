//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System.Data;
using PerfectWebERP.DataAccess;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class UserRoleController : Controller
    {
        // GET: UserRole
        public ActionResult Index()
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
            var ChangeMOPinSales = Common.GetDataViaQuery<UserRoleModel.Settings>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT008'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
                    companyKey: _userLoginInfo.CompanyKey

         );



            ViewBag.Setting = ChangeMOPinSales.Data[0].GsValue;

            return View();
        }


        public ActionResult LoadUserRoleForm()
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            UserRoleModel.UserRoleViewList sortno = new UserRoleModel.UserRoleViewList();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "UserRole",
                FieldName = "SortOrder",
                Debug = 0
            });
            sortno.SortOrder = a.Data[0].NextNo;
            return PartialView("_AddUserRoleForm", sortno);
        }

        [HttpPost]
        public ActionResult GetUserRoleList(int pageSize, int pageIndex, string Name)
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
            ModelState.Remove("ReasonID");
            ModelState.Remove("TotalCount");

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

            UserRoleModel userRole = new UserRoleModel();

            var data = userRole.GetUserRoleData(input: new UserRoleModel.GetUserRole
            {
                FK_UserRole = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
           // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddUserRole(UserRoleModel.UserRoleView data)
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
            ModelState.Remove("UserRoleID");
            ModelState.Remove("ReasonID");
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
            UserRoleModel userRole = new UserRoleModel();
            var datresponse = userRole.UpdateUserRoleData(input: new UserRoleModel.UpdateUserRole
            {

                UserAction = 1,
                //PostID = 0,
              //  UsrrlAccRpt = data.UsrrlAccRpt,
                UsrrlAdmin = data.UsrrlAdmin,
                UsrrlManager=data.UsrrlManager,
              //  UsrrlMsth = data.UsrrlMsth,
                UsrrlName = data.UserRoleName,
                UsrrlShortName = data.UserRoleShortName,
                SortOrder = data.SortOrder,
               // UsrrlAuth = data.UsrrlAuth,
                UsrrlDashBoard = data.UsrrlDashBoard,
                EntrBy = _userLoginInfo.EntrBy,
                // EntrOn = DateTime.Now
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                BackupId = 0,
                UsrrlBrCls = data.UsrrlBrCls,
                UsrrlMISRpt = data.UsrrlMISRpt,
                UsrrlMsAdd= data.UsrrlMsAdd,
                UsrrlMsDel=data.UsrrlMsDelete,
                UsrrlMsEdt=data.UsrrlMsEdit,
               // UsrrlMsRpt=data.UsrrlMsRpt,
                UsrrlOpr=data.UsrrlOpr,
                UsrrlStAdd=data.UsrrlSettingsAdd,
                UsrrlStDel= data.UsrrlSettingsDelete,
                UsrrlStEdt=data.UsrrlSettingsEdit,
                UsrrlTrAdd=data.UsrrlTrAdd,
                UsrrlTrDel=data.UsrrlTrAdd,
                UsrrlTrEdt=data.UsrrlTrEdit,
               // UsrrlTrRpt=data.UsrrlTrRpt,
                UsrrlTyAdd=data.UsrrlTypeAdd,
                UsrrlTyDel=data.UsrrlTypeDelete,
                UsrrlTyEdt=data.UsrrlTypeEdit,
                //UsrrlTyStRpt=data.UsrrlTypeSettingsRpt,

                UsrrlAppUser =data.UsrrlApplicationUser,
                UsrrlTyView =data.UsrrlTypeView,
                UsrrlStView= data.UsrrlSettingView,
                UsrrlMsView= data.UsrrlMasterView,
                UsrrlTrView =data.UsrrlTransactionView,
                UsrrlViewRpt = data.UsrrlViewReport,
                UsrrlSecurUser =data.UsrrlSecurtyUser,
                UsrrlSecurBackUp =data.UsrrlSecurtyBackUp,
                UsrrlSecurAuth =data.UsrrlSecurtyAuth,
                UsrrlDayBeg =data.UsrrlDayBegin,
                UsrrlPtRpt =data.UsrrlPrintReport,
                UsrrlPtVoucher =data.UsrrlPrintVoucher,
              
                FK_UserRole = 0,
                ID_UserRole = 0,
                UsrrlEditSalesPrice = data.UsrrlEditSalesPrice

                //TransMode = ""

                //BackupId = newpost.PostID,
                //CancelBy = _userLoginInfo.CancelBy,
                //Cancelled = _userLoginInfo.Cancelled,
                // CancelOn = newpost.CancelOn,


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetUserRoleInfo(UserRoleModel.UserRoleView data)
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

            ModelState.Remove("UserRoleName");
            ModelState.Remove("UserRoleShortName");
            ModelState.Remove("UsrrlAdmin");
            ModelState.Remove("UsrrlOpr");
           
            ModelState.Remove("UsrrlTypeAdd");
            ModelState.Remove("UsrrlTypeEdit");
            ModelState.Remove("UsrrlTypeDelete");
            ModelState.Remove("UsrrlSettingsAdd");
            ModelState.Remove("UsrrlSettingsEdit");
            ModelState.Remove("UsrrlSettingsDelete");
            ModelState.Remove("UsrrlMsAdd");
            ModelState.Remove("UsrrlMsEdit");
            ModelState.Remove("UsrrlMsDelete");
           // ModelState.Remove("UsrrlMsth");
            ModelState.Remove("UsrrlTrAdd");
            ModelState.Remove("UsrrlTrEdit");
            ModelState.Remove("UsrrlTrDelete");
            //ModelState.Remove("UsrrlAuth");
            ModelState.Remove("UsrrlBrCls");
           // ModelState.Remove("UsrrlTypeSettingsRpt");
           // ModelState.Remove("UsrrlMsRpt");
            //ModelState.Remove("UsrrlTrRpt");
            ModelState.Remove("UsrrlDashBoard");
           // ModelState.Remove("UsrrlAccRpt");
            ModelState.Remove("UsrrlMISRpt");

            ModelState.Remove("UsrrlTypeView");
            ModelState.Remove("UsrrlApplicationUser");
            ModelState.Remove("UsrrlSettingView");
            ModelState.Remove("UsrrlMasterView");
            ModelState.Remove("UsrrlTransactionView");
            ModelState.Remove("UsrrlViewReport");
            ModelState.Remove("UsrrlSecurtyUser");
            ModelState.Remove("UsrrlSecurtyBackUp");
            ModelState.Remove("UsrrlSecurtyAuth");

            ModelState.Remove("UsrrlDayBegin");
            ModelState.Remove("UsrrlPrintReport");
            ModelState.Remove("UsrrlPrintVoucher");

            ModelState.Remove("SortOrder");
            ModelState.Remove("UsrrlEditSalesPrice");
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


            UserRoleModel userRole = new UserRoleModel();
            var subCategoryInfo = userRole.GetUserRoleData(input: new UserRoleModel.GetUserRole
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_UserRole = data.UserRoleID
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(subCategoryInfo, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUserRole(UserRoleModel.UserRoleView data)
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
            ModelState.Remove("ReasonID");
            ModelState.Remove("SortOrder");
            //ModelState.Remove("Mode");
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

            UserRoleModel userRole = new UserRoleModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;


            var dataresponse = userRole.UpdateUserRoleData(input: new UserRoleModel.UpdateUserRole
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,

                FK_BranchCodeUser = branchUserCode,

                BackupId = backupId,
                FK_Company = OrgnCode,



                EntrBy = entrBy,
                ID_UserRole= data.UserRoleID,
               // UsrrlAccRpt = data.UsrrlAccRpt,
                UsrrlName=data.UserRoleName,
                UsrrlShortName = data.UserRoleShortName,
                UsrrlAdmin = data.UsrrlAdmin,
                UsrrlManager=data.UsrrlManager,
                SortOrder = data.SortOrder,
               // UsrrlAuth = data.UsrrlAuth,
                UsrrlBrCls=data.UsrrlBrCls,
                UsrrlDashBoard=data.UsrrlDashBoard,
                UsrrlMISRpt=data.UsrrlMISRpt,
                UsrrlMsAdd=data.UsrrlMsAdd,
                UsrrlMsDel=data.UsrrlMsDelete,
                UsrrlMsEdt=data.UsrrlMsEdit,
               // UsrrlMsRpt=data.UsrrlMsRpt,
               // UsrrlMsth=data.UsrrlMsth,
                UsrrlOpr=data.UsrrlOpr,
                UsrrlStAdd=data.UsrrlSettingsAdd,
                UsrrlStDel=data.UsrrlSettingsDelete,
                UsrrlStEdt=data.UsrrlSettingsEdit,
                UsrrlTrAdd=data.UsrrlTrAdd,
                UsrrlTrDel=data.UsrrlTrDelete,
                UsrrlTrEdt=data.UsrrlTrEdit,
              //  UsrrlTrRpt=data.UsrrlTrRpt,
                UsrrlTyAdd=data.UsrrlTypeAdd,
                UsrrlTyDel=data.UsrrlTypeDelete,
                UsrrlTyEdt=data.UsrrlTypeEdit,
                // UsrrlTyStRpt=data.UsrrlTypeSettingsRpt,
                UsrrlAppUser = data.UsrrlApplicationUser,
                UsrrlTyView = data.UsrrlTypeView,
                UsrrlStView = data.UsrrlSettingView,
                UsrrlMsView = data.UsrrlMasterView,
                UsrrlTrView = data.UsrrlTransactionView,
                UsrrlViewRpt = data.UsrrlViewReport,
                UsrrlSecurUser = data.UsrrlSecurtyUser,
                UsrrlSecurBackUp = data.UsrrlSecurtyBackUp,
                UsrrlSecurAuth = data.UsrrlSecurtyAuth,
                UsrrlDayBeg = data.UsrrlDayBegin,
                UsrrlPtRpt = data.UsrrlPrintReport,
                UsrrlPtVoucher = data.UsrrlPrintVoucher,
                FK_UserRole = 0,
                UsrrlEditSalesPrice = data.UsrrlEditSalesPrice
                //TransMode = ""


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserRoleReasonList()
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
        public ActionResult DeletUserRole(UserRoleModel.UserRoleView data)
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

            ModelState.Remove("UserRoleName");
            ModelState.Remove("UserRoleShortName");
            ModelState.Remove("UsrrlAdmin");
            ModelState.Remove("UsrrlOpr");

            ModelState.Remove("UsrrlTypeAdd");
            ModelState.Remove("UsrrlTypeEdit");
            ModelState.Remove("UsrrlTypeDelete");
            ModelState.Remove("UsrrlSettingsAdd");
            ModelState.Remove("UsrrlSettingsEdit");
            ModelState.Remove("UsrrlSettingsDelete");
            ModelState.Remove("UsrrlMsAdd");
            ModelState.Remove("UsrrlMsEdit");
            ModelState.Remove("UsrrlMsDelete");
           // ModelState.Remove("UsrrlMsth");
            ModelState.Remove("UsrrlTrAdd");
            ModelState.Remove("UsrrlTrEdit");
            ModelState.Remove("UsrrlTrDelete");
            //ModelState.Remove("UsrrlAuth");
            ModelState.Remove("UsrrlBrCls");
            ModelState.Remove("UsrrlTypeSettingsRpt");
           // ModelState.Remove("UsrrlMsRpt");
           // ModelState.Remove("UsrrlTrRpt");
            ModelState.Remove("UsrrlDashBoard");
           // ModelState.Remove("UsrrlAccRpt");

            ModelState.Remove("UsrrlMISRpt");
            ModelState.Remove("UsrrlTypeView");
            ModelState.Remove("UsrrlApplicationUser");
            ModelState.Remove("UsrrlSettingView");
            ModelState.Remove("UsrrlMasterView");
            ModelState.Remove("UsrrlTransactionView");
            ModelState.Remove("UsrrlViewReport");
            ModelState.Remove("UsrrlSecurtyUser");
            ModelState.Remove("UsrrlSecurtyBackUp");
            ModelState.Remove("UsrrlSecurtyAuth");

            ModelState.Remove("UsrrlDayBegin");
            ModelState.Remove("UsrrlPrintReport");
            ModelState.Remove("UsrrlPrintVoucher");
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

            UserRoleModel userRole = new UserRoleModel();

            Output datresponse = userRole.DeleteUserRoleData(input: new UserRoleModel.DeleteUserRole
            {
                FK_UserRole = data.UserRoleID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

                
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


    }
}