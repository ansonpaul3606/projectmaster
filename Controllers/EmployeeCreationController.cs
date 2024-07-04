/*----------------------------------------------------------------------
Created By	: Jisi Rajan
Created On	: 16/02/2022
Purpose		: EmployeeCreation
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
    public class EmployeeCreationController : Controller
    {
        public ActionResult Index(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            string pageName = "";
            switch (_userLoginInfo.CompCategory)
            {

                case "9":
                    pageName = "IndexGcc";
                    break;
                default:
                    pageName = "Index";
                    break;
            }
            return View(pageName);
           
        }

       


        public ActionResult LoadEmployeeForm(string mtd)
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

            EmployeeCreationModel.EmployeeListModel EmployeeList = new EmployeeCreationModel.EmployeeListModel();

            //BranchList

            //BranchTypeList

            //var branchTypeList = Common.GetDataViaQuery<EmployeeCreationModel.BranchTypelist>(parameters: new APIParameters
            //{
            //    TableName = "BranchType B",
            //    SelectFields = "B.ID_BranchType as BranchTypeID,B.BTName AS BranchType",
            //    Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_BranchMode<3 AND B.FK_Company=" + _userLoginInfo.FK_Company,
            //    GroupByFileds = "",
            //    SortFields = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey

            //);

            //EmployeeList.BranchTypeList = branchTypeList.Data;
            var branchTypeList = Common.GetDataViaQuery<EmployeeCreationModel.BranchTypelist>(parameters: new APIParameters
            {
                TableName = "BranchType B",
                SelectFields = "B.ID_BranchType as BranchTypeID,B.BTName AS BranchType",
                Criteria = "B.Cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey

          );

            EmployeeList.BranchTypeList = branchTypeList.Data;

            //EmployeeTypeList

            var employeeList = Common.GetDataViaQuery<EmployeeCreationModel.EmployeeTypeList>(parameters: new APIParameters
            {
                TableName = "EmployeeType E",
                SelectFields = "E.ID_EmployeeType AS  EmployeeTypeID,E.EmptyName AS EmployeeType",
                Criteria = "E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey

          );

            EmployeeList.EmployeeTypeList = employeeList.Data;

            //DesignationList

            var designationList = Common.GetDataViaQuery<EmployeeCreationModel.DesignationList>(parameters: new APIParameters
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

            var departmentList = Common.GetDataViaQuery<EmployeeCreationModel.DepartmentList>(parameters: new APIParameters
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


            //EmployeeCreationModel model = new EmployeeCreationModel();
            //var EmpNo = model.GetEmployeeNo(input: new EmployeeCreationModel.inputGetEmployeeNo { FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            //EmployeeList.EmployeeNumber = EmpNo.Data[0].EmployeeNumber;
            string pageName = "";

            switch (_userLoginInfo.CompCategory)
            {
               
                case "9":
                    pageName = "_AddEmployeeCreation GCC";
                    break;
                default:
                    pageName = "_AddEmployeeCreation";
                    break;
            }
            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
         
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            return PartialView(pageName, EmployeeList);

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
            var data = Common.GetDataViaQuery<EmployeeCreationModel.DesignationList>(parameters: new APIParameters
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
            var data = Common.GetDataViaQuery<EmployeeCreationModel.DepartmentList>(parameters: new APIParameters
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

            var data = Common.GetDataViaQuery<EmployeeCreationModel.Branchlist>(parameters: new APIParameters
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

        [HttpPost]
        public ActionResult GetEmployeeList(int pageSize, int pageIndex, string Name)
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

            EmployeeCreationModel employee = new EmployeeCreationModel();

            var data = employee.GetEmployeeCreationData(input: new EmployeeCreationModel.EmployeeCreationID
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Employee = 0,
                Name = Name,
                PageIndex = pageIndex +1,
                PageSize = pageSize,
                TransMode = ""
            }, companyKey: _userLoginInfo.CompanyKey);

           // return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetEmployeeInfoByID(EmployeeCreationModel.EmployeeIDView data)
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


            EmployeeCreationModel employee = new EmployeeCreationModel();

            var employeeInfo = employee.GetEmployeeCreationData(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeCreationModel.EmployeeCreationID
            {
                TransMode = "",
                PageSize = 0,
                PageIndex = 0,
                FK_Employee = data.EmployeeID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            });
            var subeimg = employee.GetEmployeeImage(companyKey: _userLoginInfo.CompanyKey, input: new EmployeeCreationModel.EmployeeImgId { ID_Employee = data.EmployeeID, EntrBy = _userLoginInfo.EntrBy });

            
            return Json(new { subeimg, employeeInfo },  JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewEmployee(EmployeeCreationModel.EmployeeCreationView data)
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
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            ModelState.Remove("Area");
            ModelState.Remove("Post");
            ModelState.Remove("Place");
            ModelState.Remove("Occupation");
            ModelState.Remove("EmployeeType");
            ModelState.Remove("DesignationID");
            ModelState.Remove("DepartmentID");


            ModelState.Remove("EmployeeTypeID");
            ModelState.Remove("BranchTypeID");
            ModelState.Remove("BranchID");
            ModelState.Remove("SlNo");
          
      
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


            EmployeeCreationModel employee = new EmployeeCreationModel();

            string empimage = (string)Session["EmployeeImage"];

            var datresponse = employee.UpdateEmployeeCreationData(input: new EmployeeCreationModel.UpdateEmployeeCreation
            {
                UserAction = 1,
                EmployeeNo = "",
                EmployeeName = data.EmployeeName,
                EmployeeMobile = data.EmployeeMobile,
                EmployeePhone = data.EmployeePhone,
                EmployeeEmail = data.EmployeeEmail,
                EmployeeAddress = data.EmployeeAddress,
                FK_Country = data.CountryID,
                FK_State = data.StatesID,
                FK_District = data.DistrictID,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                //FK_Post = data.PostID,
                FK_Branch = data.BranchID,
                FK_EmployeeType = data.EmployeeTypeID,
                FK_Designation = data.DesignationID,
                FK_Department =data.DepartmentID,
                ID_Employee = 0,
                FK_Employee = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                LEmployeeName = data.LEmployeeName,
                EmployeeImageList = empimage
            }, companyKey: _userLoginInfo.CompanyKey);
            Session["EmployeeImage"] = "";

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateEmployee(EmployeeCreationModel.EmployeeCreationView data)
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
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            ModelState.Remove("Area");
            ModelState.Remove("Post");
            ModelState.Remove("Place");
            ModelState.Remove("Occupation");
            ModelState.Remove("EmployeeType");
            ModelState.Remove("DesignationID");
            ModelState.Remove("DepartmentID");
            ModelState.Remove("EmployeeTypeID");
            ModelState.Remove("BranchTypeID");
            ModelState.Remove("BranchID");
            ModelState.Remove("SlNo");
          
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

            EmployeeCreationModel employee = new EmployeeCreationModel();
            string empimage = (string)Session["EmployeeImage"];
            var datresponse = employee.UpdateEmployeeCreationData(input: new EmployeeCreationModel.UpdateEmployeeCreation
            {
                UserAction = 2,
                EmployeeNo = "",
                EmployeeName = data.EmployeeName,
                EmployeeMobile = data.EmployeeMobile,
                EmployeePhone = data.EmployeePhone,
                EmployeeEmail = data.EmployeeEmail,
                EmployeeAddress = data.EmployeeAddress,
                FK_Country = data.CountryID,
                FK_State = data.StatesID,
                FK_District = data.DistrictID,
               
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                //FK_Post = data.PostID,
                FK_Branch = data.BranchID,
                FK_EmployeeType = data.EmployeeTypeID,
                FK_Designation = data.DesignationID,
                FK_Department=data.DepartmentID,

                ID_Employee = data.EmployeeID,
                FK_Employee = data.EmployeeID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                LEmployeeName = data.LEmployeeName,
                EmployeeImageList = empimage
                //EntrOn = DateTime.UtcNow

            }, companyKey: _userLoginInfo.CompanyKey);

            Session["EmployeeImage"] = "";
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteEmployee(EmployeeCreationModel.EmployeeIDView data)
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

            EmployeeCreationModel employee = new EmployeeCreationModel();

            var datresponse = employee.DeleteEmployeeCreationData(input: new EmployeeCreationModel.DeleteEmployeeCreation
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_Employee = data.EmployeeID,
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






        public ActionResult GetPinCodedetails(string Pincode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            EmployeeCreationModel dataModel = new EmployeeCreationModel();
            var outputData = dataModel.GetDetailsByPincodeData(input: new EmployeeCreationModel.InputPincode { FK_Company = _userLoginInfo.FK_Company, Pincode = Pincode }, companyKey: _userLoginInfo.CompanyKey);
            return Json(outputData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCountryList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<EmployeeCreationModel.Countrylist>(parameters: new APIParameters
            {
                TableName = "Country",
                SelectFields = " CntryName AS Country,ID_Country AS CountryID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateList(Int64 countryID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            StateModel dataModel = new StateModel();
            var outputData = dataModel.GetStateData(input: new StateModel.GetState
            {
                FK_States = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Country = countryID,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(outputData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictList(Int64 statesid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<EmployeeCreationModel.Districtlist>(parameters: new APIParameters
            {
                TableName = "District",
                SelectFields = " DtName AS District,ID_District AS DistrictID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_States='" + statesid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAreaList(Int64 districtid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<EmployeeCreationModel.Arealist>(parameters: new APIParameters
            {
                TableName = "Area",
                SelectFields = " AreaName AS Area,ID_Area AS AreaID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District='" + districtid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetPlaceList(Int64 districtid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<EmployeeCreationModel.Placelist>(parameters: new APIParameters
            {

                TableName = "Place",
                SelectFields = " PlcName AS Place,ID_Place AS PlaceID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District='" + districtid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPostList(EmployeeCreationModel.InputPlace datas)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<EmployeeCreationModel.Postlist>(parameters: new APIParameters
            {
                TableName = "Post",
                SelectFields = "PostName AS Post,ID_Post AS PostID,PinCode AS PinCode",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District=" + datas.DistrictID + " AND FK_Place=" + datas.PlaceID + " AND FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }



    }
}


