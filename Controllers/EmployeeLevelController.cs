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
    public class EmployeeLevelController : Controller
    {
        // GET: EmployeeLevel
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


        public ActionResult LoadEmployeeLevelForm()
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

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "EmployeeLevel",
                FieldName = "SortOrder",
                Debug = 0
            });

            return PartialView("_AddEmployeeLevelForm", a.Data[0].NextNo);
        }

        [HttpPost]
        public ActionResult GetEmployeeLevelList(int pageSize, int pageIndex, string Name)
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
            EmployeeLevelModel employeeLevel = new EmployeeLevelModel();

            var data = employeeLevel.GetEmployeeLevelData(companyKey: _userLoginInfo.CompanyKey, 
                input: new EmployeeLevelModel.EmployeeLevelID {
                    EntrBy=_userLoginInfo.EntrBy,
                    FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                    FK_Company=_userLoginInfo.FK_Company,
                    FK_EmployeeLevel=0,
                    FK_Machine=_userLoginInfo.FK_Machine,
                    Name = Name,
                    PageIndex = pageIndex + 1,
                    PageSize = pageSize,
                    TransMode = transMode

                });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
           // return Json(new { outputList.Process, outputList.Data, pageSize, pageIndex, totalrecord = outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewEmployeeLevel(EmployeeLevelModel.EmployeeLevelView data)
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
            ModelState.Remove("SlNo");
            ModelState.Remove("EmployeeLevelID"); 
            ModelState.Remove("ReasonID");
            ModelState.Remove("EmployeeLevelPriority");
            ModelState.Remove("SortOrder");
            #region :: Model validation  ::
            //--- Model validation 
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                //errorList = ModelState.Values.SelectMany(m => m.Errors)
                //                        .Select(e => e.ErrorMessage)
                //                        .ToList();

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


            EmployeeLevelModel employeeLevel = new EmployeeLevelModel();

            var datresponse = employeeLevel.UpdateEmployeeLevelData(input: new EmployeeLevelModel.UpdateEmployeeLevel
            {

                UserAction = 1,
                BackupId=0,
                EmpLvlName=data.EmployeeLevelName,
                EmpLvlShortName=data.EmployeeLevelShortName,
                EmpLvlPriority=data.EmployeeLevelPriority,
                EntrBy=_userLoginInfo.EntrBy,
                FK_Branch=_userLoginInfo.FK_Branch,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_Company=_userLoginInfo.FK_Company,
                FK_EmployeeLevel=0,
                FK_Machine=_userLoginInfo.FK_Machine,
                TransMode=data.TransMode,
                ID_EmployeeLevel=0,
                SortOrder=data.SortOrder

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateEmployeeLevel(EmployeeLevelModel.EmployeeLevelView data)
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
            ModelState.Remove("SlNo");
            ModelState.Remove("ReasonID");
            ModelState.Remove("EmployeeLevelPriority");
            ModelState.Remove("SortOrder");
            #region :: Model validation  ::
            //--- Model validation 
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                //errorList = ModelState.Values.SelectMany(m => m.Errors)
                //                        .Select(e => e.ErrorMessage)
                //                        .ToList();

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


            EmployeeLevelModel employeeLevel = new EmployeeLevelModel();

            var datresponse = employeeLevel.UpdateEmployeeLevelData(input: new EmployeeLevelModel.UpdateEmployeeLevel
            {

                UserAction = 2,
                BackupId = 0,
                EmpLvlName = data.EmployeeLevelName,
                EmpLvlShortName = data.EmployeeLevelShortName,
                EmpLvlPriority = data.EmployeeLevelPriority,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_EmployeeLevel = data.EmployeeLevelID,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                ID_EmployeeLevel = data.EmployeeLevelID,
                SortOrder = data.SortOrder

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetEmployeeLevelInfo(EmployeeLevelModel.EmployeeLevelView emoloyeeLevelInfo)   //fn works when click view or edit button and id passes and take its needed properties.
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
            ModelState.Remove("SlNo");
            ModelState.Remove("EmployeeLevelName");
            ModelState.Remove("EmployeeLevelShortName");
            ModelState.Remove("EmployeeLevelPriority");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::

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


            EmployeeLevelModel employeeLevel = new EmployeeLevelModel();

            var Info = employeeLevel.GetEmployeeLevelData(companyKey: _userLoginInfo.CompanyKey,
                input: new EmployeeLevelModel.EmployeeLevelID
                {

                     FK_EmployeeLevel = emoloyeeLevelInfo.EmployeeLevelID,
                     TransMode ="",
                     FK_Machine=_userLoginInfo.FK_Machine,
                     EntrBy=_userLoginInfo.EntrBy,
                     FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                     FK_Company=_userLoginInfo.FK_Company,
                     
                     PageIndex=0,
                     PageSize=0
                });

            return Json(Info, JsonRequestBehavior.AllowGet);
        }




        //public ActionResult GetEmployeeLevelReasonList()
        //{
        //    #region ::  Check User Session to verifyLogin  ::

        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion ::  Check User Session to verifyLogin  ::


        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    ReasonModel reason = new ReasonModel();

        //    var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { ID_Reason = 0, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy });


        //    APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
        //    {
        //        Process = outputList.Process,
        //        Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
        //    };


        //    return Json(deleteReason, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult GetEmployeeLevelReasonList()
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
        public ActionResult DeleteEmployeeLevel(EmployeeLevelModel.EmployeeLevelView data)
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

            ModelState.Remove("SlNo");
            ModelState.Remove("EmployeeLevelName");
            ModelState.Remove("EmployeeLevelShortName");
            ModelState.Remove("EmployeeLevelPriority");
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

            EmployeeLevelModel modelObj = new EmployeeLevelModel();

            Output datresponse = modelObj.DeleteEmployeeLevelData(input: new EmployeeLevelModel.DeleteEmployeeLevel
            {
                 FK_EmployeeLevel=data.EmployeeLevelID,
                 TransMode="",

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