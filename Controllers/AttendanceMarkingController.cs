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

namespace PerfectWebERP.Controllers
{
    public class AttendanceMarkingController : Controller
    {
        // GET: AttendanceMarking
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
            CommonMethod objCmnMethod = new CommonMethod();
            string mTd = objCmnMethod.DecryptString(mtd);
            TempData["mTd"] = mTd.ToString();
            ViewBag.PageTitles = mTd.ToString();
            return View();
        }
        public ActionResult LoadFormAttendanceMarking(string mtd)
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
            AttendanceMarkingModel.AttendanceMarkingModelView list = new AttendanceMarkingModel.AttendanceMarkingModelView();

            var OpDepartmentListto = Common.GetDataViaQuery<AttendanceMarkingModel.DepartmentTo>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS FK_Department,DeptName AS DepartmentNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                       companyKey: _userLoginInfo.CompanyKey

            );
            list.DepartmentListTo = OpDepartmentListto.Data;

            var OpBranchListto = Common.GetDataViaQuery<AttendanceMarkingModel.BranchTo>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS FK_Branch,BrName AS BranchNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                      companyKey: _userLoginInfo.CompanyKey

           );
            list.BranchListTo = OpBranchListto.Data;

            ViewBag.PageTitle = TempData["mTd"] as string;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddAttendanceMarking", list);
        }

        public ActionResult EmployeeAttendanceDetails(AttendanceMarkingModel.AttendanceMarkingModelView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel.ServiceListView AssignModel = new ServiceListModel.ServiceListView();
            AttendanceMarkingModel data = new AttendanceMarkingModel();


            var Lits = data.GetEmployeeattendanceDetails(input: new AttendanceMarkingModel.GetEmployeedetails { FK_Branch = Data.FK_Branch, FK_Department = Data.FK_Department, ProcessDate = Data.AMKDate, FK_Company = _userLoginInfo.FK_Company}, companyKey: _userLoginInfo.CompanyKey);

          
            return Json(new { Lits }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewAttendanceMarking(AttendanceMarkingModel.AttendanceMarkingModelView data)
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

           // ModelState.Remove("AttendanceMarkingDetailsView");


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


            AttendanceMarkingModel objprdwise = new AttendanceMarkingModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateAttendanceMarkingData(input: new AttendanceMarkingModel.AttendanceMarkingUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,
                ID_EmployeeAttendanceDetails = 0,
                AMKDate = data.AMKDate,
                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,
                
                AttendanceMarkingDetailsView = data.AttendanceMarkingDetailsView is null ? "" : Common.xmlTostring(data.AttendanceMarkingDetailsView),


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAttendanceMarking(AttendanceMarkingModel.AttendanceMarkingModelView data)
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

            //ModelState.Remove("EquipmentServiceDetails");


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


            AttendanceMarkingModel objprdwise = new AttendanceMarkingModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateAttendanceMarkingData(input: new AttendanceMarkingModel.AttendanceMarkingUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,
                ID_EmployeeAttendanceDetails = (data.ID_EmployeeAttendanceDetails.HasValue) ? data.ID_EmployeeAttendanceDetails.Value : 0,
              
                AMKDate = data.AMKDate,
                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,

                AttendanceMarkingDetailsView = data.AttendanceMarkingDetailsView is null ? "" : Common.xmlTostring(data.AttendanceMarkingDetailsView),


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public ActionResult GetAttendanceMarkingList(int pageSize, int pageIndex, string Name)
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

            AttendanceMarkingModel objPrd = new AttendanceMarkingModel();
            var data = objPrd.GetAttendanceMarkingData(companyKey: _userLoginInfo.CompanyKey, input: new AttendanceMarkingModel.AttendanceMarkingID
            {

               // FK_EmployeeAttendanceDetails = 0,
                FK_Company = _userLoginInfo.FK_Company,

                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Details = 0,
                TransMode = transMode

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetAttendanceMarkingInfo(AttendanceMarkingModel.AttendanceMarkingID data)
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
            AttendanceMarkingModel objprd = new AttendanceMarkingModel();

            var mptableInfo = objprd.GetAttendanceMarkingDatainfoid(companyKey: _userLoginInfo.CompanyKey, input: new AttendanceMarkingModel.AttendanceMarkingID
            {
               
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch=data.FK_Branch,
                FK_Department=data.FK_Department,
                AMKDate=data.AMKDate,
                Details = 0,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = 0,
                PageSize = 0,
            });

            var subtable = objprd.GetAttendanceMarkingDatainfodetails(companyKey: _userLoginInfo.CompanyKey, input: new AttendanceMarkingModel.AttendanceMarkingID
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,
                AMKDate = data.AMKDate,
                Details = 1,
                EntrBy = _userLoginInfo.EntrBy
            });

           

            return Json(new { mptableInfo, subtable }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAttendanceMarking(AttendanceMarkingModel.AttendanceMarkingModelView data)

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

            ModelState.Remove("FK_Department");
            //ModelState.Remove("PftTypeShortName");


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


            AttendanceMarkingModel.AttendnaceMarkingDelete AttendanceMarking = new AttendanceMarkingModel.AttendnaceMarkingDelete
            {
                //FK_SalaryAdvancePayment = data.ID_SalaryAdvancePayment,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Department = data.FK_Department,
                FK_Branch = data.FK_Branch,
                AMKDate = data.AttDate,
                FK_Reason = data.ReasonID,
                TransMode = ""

            };

           Output dataresponse = Common.UpdateTableData<AttendanceMarkingModel.AttendnaceMarkingDelete>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProAttendanceMarkingDelete", parameter: AttendanceMarking);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetAttendanceDeleteReasonList()
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