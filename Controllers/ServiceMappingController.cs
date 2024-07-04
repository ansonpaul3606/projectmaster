/*----------------------------------------------------------------------
Created By	: Amritha AK
Created On	: 09/02/2022
Purpose		: ServiceMapping
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

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
    [CheckSessionTimeOut]
    public class ServiceMappingController : Controller
    {
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadFormServiceMapping()
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

            return PartialView("_AddServiceMappingForm");
        }

      

        [HttpPost]
        public ActionResult GetServiceMappingList(int pageSize, int pageIndex, string Name)
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


            ServiceMappingModel ServiceMapping = new ServiceMappingModel();

            var data = ServiceMapping.GetServiceMappingData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceMappingModel.ServiceMappingID
            {
                ID_ServiceMapping = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name
              
            });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetServicemappingInfo(ServiceMappingModel.ServiceMappingView servicemappingInfo)
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
            ModelState.Remove("ReasonID");
          
            #region :: Model validation  ::

            //--- Model validation 
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::


            ServiceMappingModel serviceMapping = new ServiceMappingModel();

            var mptableInfo = serviceMapping.GetServiceMappingData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceMappingModel.ServiceMappingID { ID_ServiceMapping = servicemappingInfo.ServiceMappingID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
            var subtable = serviceMapping.GetSubServiceMappingData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceMappingModel.ServiceMappigSubSelect { ID_ServiceMapping = servicemappingInfo.ServiceMappingID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            if (subtable.Process.IsProcess)
            {
                mptableInfo.Data[0].Subservicemapping = subtable.Data;
            }

            return Json( mptableInfo , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewServiceMapping(ServiceMappingModel.ServiceMappingView data)
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

            ModelState.Remove("ServiceMappingID");
        
            ModelState.Remove("EmployeeID");

            ModelState.Remove("Employee");
            ModelState.Remove("SMActive");

            ModelState.Remove("ProductID");
            ModelState.Remove("ProductName");
            ModelState.Remove("Priority");
            
            ModelState.Remove("Subservicemapping");
            ModelState.Remove("Subservicemapping.ProductID");
            ModelState.Remove("Subservicemapping.ProductName");
            ModelState.Remove("Subservicemapping.Priority");
            ModelState.Clear();

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

            ServiceMappingModel ServiceMapping = new ServiceMappingModel();

            var datresponse = ServiceMapping.UpdateServiceMappingData(input: new ServiceMappingModel.UpdateServiceMapping
            {
                UserAction = 1
,
                FK_Employee = data.EmployeeID,
                Active =data.SMActive,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

                ID_ServiceMapping = 0,

                SubServiceMappingDetails = data.Subservicemapping is null ? "" : Common.xmlTostring(data.Subservicemapping),

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateServiceMapping(ServiceMappingModel.ServiceMappingView data)
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
            ModelState.Remove("EmployeeID");

            ModelState.Remove("Employee");
            ModelState.Remove("SMActive");

            ModelState.Remove("ProductID");
            ModelState.Remove("ProductName");
            ModelState.Remove("Subservicemapping");
            ModelState.Remove("Subservicemapping");
            ModelState.Remove("Subservicemapping.ProductID");
            ModelState.Remove("Subservicemapping.ProductName");
            ModelState.Remove("Subservicemapping.Priority");

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

            ServiceMappingModel ServiceMapping = new ServiceMappingModel();
            var datresponse = ServiceMapping.UpdateServiceMappingData(input: new ServiceMappingModel.UpdateServiceMapping
            {
                UserAction = 2,         
                ID_ServiceMapping =data.ServiceMappingID,
                FK_Employee = data.EmployeeID,
                Active =data.SMActive,
                SubServiceMappingDetails = data.Subservicemapping is null ? "" : Common.xmlTostring(data.Subservicemapping),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteServiceMapping(ServiceMappingModel.ServiceMappingInfoView data)
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

            ServiceMappingModel ServiceMapping = new ServiceMappingModel();
           // ComplaintCheckListModel mdl = new ComplaintCheckListModel();



            Output datresponse = ServiceMapping.DeleteServiceMappingData(input: new ServiceMappingModel.DeleteServiceMapping
            {
                ID_ServiceMapping = data.ID_ServiceMapping,
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


        public ActionResult GetServiceMappingReasonList()
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

        //get employee baseinf
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetEmployeeBaseInfo(long employeeID)
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

            var employeeInfo = Common.GetDataViaQuery<ServiceMappingModel.EmployeeBaseInfo>(parameters: new APIParameters
            {
                TableName = "Employee  AS E LEFT JOIN  Department AS D ON D.ID_Department =E.FK_Department LEFT JOIN Designation AS DS ON DS.ID_Designation =E.FK_Designation",
                SelectFields = "E.ID_Employee AS EmployeeID,Concat(E.EmpFName,E.EmpLName) AS EmployeeName,E.EmpEmrgContactNum AS Number,D.ID_Department AS DepartmentID ,D.DeptName AS EmployeeDepartment,DS.ID_Designation As DesignationID,DS.DesName AS EmployeeDesignation",
                Criteria = "E.cancelled=0 AND E.Passed=1 and E.FK_Company=" + _userLoginInfo.FK_Company+ "AND E.ID_Employee = "+ employeeID,
                SortFields = "",
                GroupByFileds = ""
            },
     companyKey: _userLoginInfo.CompanyKey
    );
            return Json(employeeInfo, JsonRequestBehavior.AllowGet);
        }



    }
}


