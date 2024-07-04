using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class PayrollController : Controller
    {
        // GET: Payroll
        public ActionResult AcquaintanceProcess()
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

        public ActionResult LoadAcquaintanceProcess()
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

            PayrollModel.SalaryProcessNew objSalaryProcessNew = new PayrollModel.SalaryProcessNew();

            var branch = Common.GetDataViaQuery<PayrollModel.Branchs>(companyKey: _userLoginInfo.CompanyKey,parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            });
            objSalaryProcessNew.Branch = branch.Data.AsEnumerable();
            var departmentList = Common.GetDataViaQuery<PayrollModel.DepartmentList>(companyKey: _userLoginInfo.CompanyKey,parameters: new APIParameters
            {
                TableName = "Department Dp",
                SelectFields = "Dp.ID_Department AS DepartmentID,Dp.DeptName AS[Department]",
                Criteria = "Dp.Cancelled=0 AND Dp.Passed=1 AND Dp.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            });

            objSalaryProcessNew.Department = departmentList.Data.AsEnumerable();

            return PartialView("_LoadAcquaintanceProcess", objSalaryProcessNew);

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GetSalaryInfo(PayrollModel.GetSalaryData obj)
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

            PayrollModel num = new PayrollModel();
            var SalaryData = num.GetSalaryDetailsData(companyKey: _userLoginInfo.CompanyKey,input: new PayrollModel.GetSalaryData
            {   ProcessDate=obj.ProcessDate,
                FK_Branch=obj.FK_Branch,
                FK_Department=obj.FK_Department,
                FK_Employee = obj.FK_Employee,
                FromDate =obj.FromDate,
                ToDate=obj.ToDate,
                FK_Company = _userLoginInfo.FK_Company,
                Mode=0
            });
            var SalaryDataDetails = num.GetSalaryDetails(companyKey: _userLoginInfo.CompanyKey, input: new PayrollModel.GetSalaryData
            {
                ProcessDate = obj.ProcessDate,
                FK_Branch = obj.FK_Branch,
                FK_Department = obj.FK_Department,
                FK_Employee = obj.FK_Employee,
                FromDate = obj.FromDate,
                ToDate = obj.ToDate,
                FK_Company = _userLoginInfo.FK_Company,
                Mode = 1
            });

            return Json(new { SalaryData, SalaryDataDetails }, JsonRequestBehavior.AllowGet);
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateSalaryInfo(PayrollModel.InputSalaryData data)
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

            PayrollModel salary = new PayrollModel();

            var datresponse = salary.UpdateSalaryInfo(input: new PayrollModel.UpdateSalaryData
            {
                UserAction = data.UserAction,
                GroupID = data.GroupID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = data.FK_Department,
                FK_Employee = data.FK_Employee,
                FK_Branch = data.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Machine = _userLoginInfo.FK_Machine,
                SalaryProcessDetails = data.SalaryProcessDetails is null ? "" : Common.xmlTostring(data.SalaryProcessDetails),
                SalarySubdetails = data.SalarySubdetails is null ? "" : Common.xmlTostring(data.SalarySubdetails),
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason = data.FK_Reason,
                ProcessDate = data.ProcessDate,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSalaryProcessList(PayrollModel.SalaryProcessDataInfo data)
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

            PayrollModel salary = new PayrollModel();
            var SalaryProcessInfo = salary.GetSalaryProcessSelect(companyKey: _userLoginInfo.CompanyKey, input: new PayrollModel.GetSalaryProcessData
            {
                FromDate = null,
                ToDate = null,
                FK_Branch = 0,
                FK_Department = 0,
                FK_Employee = 0,
                PageIndex = data.pageIndex + 1,
                PageSize = data.pageSize,
                Name = data.Name,
                Mode=0,
            });

            return Json(new { SalaryProcessInfo.Process, SalaryProcessInfo.Data, data.pageSize, data.pageIndex, totalrecord = (SalaryProcessInfo.Data is null) ? 0 : SalaryProcessInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSalaryProcessListByID(PayrollModel.SalaryProcessDataInfo data)
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

            PayrollModel salary = new PayrollModel();
            var SalaryData = salary.GetSalaryProcessSelectById(companyKey: _userLoginInfo.CompanyKey, input: new PayrollModel.GetSalaryProcessData
            {
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,
                FK_Employee = data.FK_Employee,
                PageIndex = 0,
                PageSize = 0,
                Name = data.Name,
                Mode = 1,
            });
            var SalaryDataDetails = salary.GetSalaryProcessSelectById(companyKey: _userLoginInfo.CompanyKey, input: new PayrollModel.GetSalaryProcessData
            {
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,
                FK_Employee = 0,
                PageIndex = 0,
                PageSize = 0,
                Name = data.Name,
                Mode = 1,
            });
            return Json(new { SalaryData}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSalaryProcessEmpData(PayrollModel.SalaryProcessDataInfo data)
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

            PayrollModel salary = new PayrollModel();

            var SalaryDataDetails = salary.GetSalaryProcessSelectByMode2(companyKey: _userLoginInfo.CompanyKey, input: new PayrollModel.GetSalaryProcessData
            {
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,
                FK_Employee=data.FK_Employee,
                PageIndex = 0,
                PageSize = 0,
                Name = data.Name,
                Mode = 2,
                FK_SalaryProcess=data.FK_SalaryProcess,
            });
            return Json(new { SalaryDataDetails }, JsonRequestBehavior.AllowGet);
        }
    }
}