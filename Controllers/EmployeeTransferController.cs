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
    public class EmployeeTransferController : Controller
    {
        // GET: EmployeeTransfer
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            return View();
        }
        public ActionResult LoadEmployeeTransferForm(string mtd)
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

            EmployeeTransferModel.EmployeeListModel EmployeeList = new EmployeeTransferModel.EmployeeListModel();

            //BranchList

            //BranchTypeList

            var branchTypeList = Common.GetDataViaQuery<EmployeeTransferModel.BranchTypelist>(parameters: new APIParameters
            {
                TableName = "BranchType B",
                SelectFields = "B.ID_BranchType as BranchTypeID,B.BTName AS BranchType",
                Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_BranchMode<3 AND B.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );

            EmployeeList.BranchTypeList = branchTypeList.Data;           
        

            var designationList = Common.GetDataViaQuery<EmployeeTransferModel.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            EmployeeList.DesignationList = designationList.Data;

            var departmentList = Common.GetDataViaQuery<EmployeeTransferModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Department Dp",
                SelectFields = "Dp.ID_Department AS DepartmentID,Dp.DeptName AS[Department]",
                Criteria = "Dp.Cancelled=0 AND Dp.Passed=1 AND Dp.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            EmployeeList.DepartmentList = departmentList.Data;            

            return PartialView("_AddEmployeeTransfer", EmployeeList);

        }
        [HttpGet]
        public ActionResult GetDesignationList()
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
            var data = Common.GetDataViaQuery<EmployeeTransferModel.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey

          );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetDepartmentList()
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
            var data = Common.GetDataViaQuery<EmployeeTransferModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Department Dp",
                SelectFields = "Dp.ID_Department AS DepartmentID,Dp.DeptName AS[Department]",
                Criteria = "Dp.Cancelled=0 AND Dp.Passed=1 AND Dp.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [ValidateAntiForgeryToken]
        public ActionResult GetBranchDetails(long BranchTypeID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<EmployeeTransferModel.Branchlist>(parameters: new APIParameters
            {

                TableName = "Branch B",
                SelectFields = "B.ID_Branch as BranchID,B.BrName AS Branch",
                Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company + " AND B.FK_BranchType = " + BranchTypeID,
                GroupByFileds = "",
                SortFields = ""


            },
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //[ValidateAntiForgeryToken]
        public ActionResult GetNewBranchDetails(long NewBranchtypeID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<EmployeeTransferModel.NewBranchlist>(parameters: new APIParameters
            {

                TableName = "Branch B",
                SelectFields = "B.ID_Branch as NewBranchID,B.BrName AS NewBranch",
                Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company + " AND B.FK_BranchType = " + NewBranchtypeID,
                GroupByFileds = "",
                SortFields = ""


            },
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetEmployeeTransferList(int pageSize, int pageIndex, string Name)
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

            EmployeeTransferModel employee = new EmployeeTransferModel();

            var data = employee.GetEmployeeTransferData(input: new EmployeeTransferModel.EmployeeTransferID
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_EmployeeTransfer = 0,
                Name = Name,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = ""
            }, companyKey: _userLoginInfo.CompanyKey);

            
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken()]
        public ActionResult GetEmployeeTransferInfoByID(EmployeeTransferModel.EmployeeIDView data)
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

            #region :: Model validation  ::
                       
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

            #endregion :: Model validation  ::


            EmployeeTransferModel employee = new EmployeeTransferModel();

            var employeeInfo = employee.GetEmployeeTransferData(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeTransferModel.EmployeeTransferID
            {
                TransMode = "",
                PageSize = 0,
                PageIndex = 0,
                FK_EmployeeTransfer = data.FK_EmployeeTransfer,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            });        


            return Json(new {  employeeInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddEmployeeTransfer(EmployeeTransferModel.EmployeeTransferView data)
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

         
            ModelState.Remove("EmployeeID");           
            ModelState.Remove("DesignationID");
            ModelState.Remove("DepartmentID");           
            ModelState.Remove("BranchTypeID");
            ModelState.Remove("BranchID");
            ModelState.Remove("SlNo");


            #region :: Model validation  ::
            //--- Model validation 
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

        
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


            EmployeeTransferModel employee = new EmployeeTransferModel();          

            var datresponse = employee.UpdateEmployeeTransferData(input: new EmployeeTransferModel.UpdateEmployeeTransfer
            {
                UserAction = 1,
                ID_EmployeeTransfer = data.ID_EmployeeTransfer,
                FK_EmployeeTransfer = data.FK_EmployeeTransfer,
                EmployeeID = data.EmployeeID,
                NewDepartmentID = data.NewDepartmentID,              
                NewDesignationID = data.NewDesignationID,
                NewBranchID = data.NewBranchID,
                NewBranchtypeID = data.NewBranchtypeID,
                DepartmentID=data.DepartmentID,
                DesignationID=data.DesignationID,
                BranchID=data.BranchID,
                BranchTypeID=data.BranchTypeID,
                Debug = 0,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,                           
            }, companyKey: _userLoginInfo.CompanyKey);
           

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateEmployeeTransfer(EmployeeTransferModel.EmployeeTransferView data)
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
        
            ModelState.Remove("EmployeeID");             
            ModelState.Remove("DesignationID");
            ModelState.Remove("DepartmentID");
            ModelState.Remove("EmployeeTypeID");
            ModelState.Remove("BranchTypeID");
            ModelState.Remove("BranchID");
            
                      
            #region :: Model validation  ::
                      
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

            #endregion :: Model validation  ::

            EmployeeTransferModel employee = new EmployeeTransferModel();
           
            var datresponse = employee.UpdateEmployeeTransferData(input: new EmployeeTransferModel.UpdateEmployeeTransfer
            {
                UserAction = 2,
                ID_EmployeeTransfer=data.ID_EmployeeTransfer,
                FK_EmployeeTransfer=data.FK_EmployeeTransfer,
                EmployeeID = data.EmployeeID,
                NewDepartmentID = data.NewDepartmentID,
                NewDesignationID = data.NewDesignationID,
                NewBranchID = data.NewBranchID,
                NewBranchtypeID = data.NewBranchtypeID,
                DepartmentID = data.DepartmentID,
                DesignationID = data.DesignationID,
                BranchID = data.BranchID,
                BranchTypeID = data.BranchTypeID,
                Debug = 0,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteEmployeeTransfer(EmployeeTransferModel.EmployeeTransferView data)
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

                     

            EmployeeTransferModel employee = new EmployeeTransferModel();

            var datresponse = employee.DeleteEmployeeTransferData(input: new EmployeeTransferModel.DeleteEmployeeTransfer
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_EmployeeTransfer = data.ID_EmployeeTransfer,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID


            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetEmployeeReasonList()
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