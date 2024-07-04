using PerfectWebERP.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Models;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class DesignationController : Controller
    {
        public ActionResult Designation()
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

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;


            return View();
        }

      


        [HttpPost]
        public ActionResult GetDesignationList(int pageSize, int pageIndex, string Name)
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
            DesignationModel Designation = new DesignationModel();



            var outputList = Designation.GetDesignationtData(companyKey: _userLoginInfo.CompanyKey, input: new DesignationModel.GetDesignation
            {
                ID_Designation = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });

          
            return Json(new { outputList.Process, outputList.Data, pageSize, pageIndex, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        [ValidateAntiForgeryToken()]   //antiforgery token
        public ActionResult GetDesignationInfo(DesignationModel.DesignationView data)
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

            DesignationModel Designation = new DesignationModel();

            var outputList = Designation.GetDesignationtData(companyKey: _userLoginInfo.CompanyKey, input: new DesignationModel.GetDesignation
            {
                ID_Designation = data.DesignationID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadDesignationForm()
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


            StateModel state = new StateModel();

            DesignationModel.DesignationFormData dis = new DesignationModel.DesignationFormData();
            var dislidt = Common.GetDataViaQuery<DesignationModel.EmployeeLevel>(parameters: new APIParameters
            {
                TableName = "EmployeeLevel",
                SelectFields = "ID_EmployeeLevel as EmployeeLevelID, EmpLvlName As EmployeeLevelName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey

                );
            dis.EmployeeLevelList = dislidt.Data;


            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProGetNextNo",
                parameter: new NextSortOrder
                {
                    TableName = "Designation",
                    FieldName = "SortOrder",
                    Debug = 0
                });
            dis.SortOrder = a.Data[0].NextNo;


            return PartialView("_AddDesignation", dis);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken()]   //antiforgery token
        public ActionResult AddNewDesignation(DesignationModel.DesignationView data)
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


            DesignationModel Designation = new DesignationModel();



            var datresponse = Designation.UpdateDesignationData(input: new DesignationModel.UpdateDesignation
            {
                UserAction = 1,
                ID_Designation = 0,
                DesName = data.Designation,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                DesShortName = data.ShortName,
                FK_EmployeeLevel = data.EmployeeLevelID,
                SortOrder = data.SortOrder,
                TransMode=data.TransMode,
                BackupID = 0,
                FK_Designation = data.DesignationID,
                FK_Branch = _userLoginInfo.FK_Branch
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]   //antiforgery token
        public ActionResult UpdateDesignation(DesignationModel.DesignationView data)
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

            ModelState.Remove("SortOrder");

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

            DesignationModel Designation = new DesignationModel();



            var datresponse = Designation.UpdateDesignationData(input: new DesignationModel.UpdateDesignation
            {
                UserAction = 2,

                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_EmployeeLevel= data.EmployeeLevelID,
                DesShortName = data.ShortName,
                DesName = data.Designation,
                ID_Designation = data.DesignationID,
                SortOrder = data.SortOrder,
                TransMode = data.TransMode,
                BackupID = 0,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Designation = data.DesignationID

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]   //antiforgery token
        public ActionResult DeleteDesignation(DesignationModel.DesignationView data)
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

            DesignationModel Designation = new DesignationModel();

            var datresponse = Designation.DeleteDesignationData(input: new DesignationModel.DeleteDesignation
            {
                //ID_Designation = data.DesignationID,
                //CancelBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Branch = 0,
                FK_Designation = data.DesignationID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,     
                FK_Reason = data.ReasonID
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetDesignationDeleteReasonList()
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
