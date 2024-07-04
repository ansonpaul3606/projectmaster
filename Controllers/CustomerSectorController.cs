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
    public class CustomerSectorController : Controller
    {
        // GET: CustomerSector
        public ActionResult CustomerSectorIndex(string mtd)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
          
        }




        public ActionResult LoadFormCustomerSector(string mtd)
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
            CustomerSectorModel.Sortorderlist custsect = new CustomerSectorModel.Sortorderlist();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "CustomerSector",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            custsect.SortOrder = a.Data[0].NextNo;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddCustomerSectorForm", custsect);

        }



        [HttpPost]
        //slide window show customersector details//

        public ActionResult GetCustomerSectorList(int pageSize, int pageIndex, string Name)
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

            //     var data = Common.GetDataViaQuery<CustomerSectorModel.CustomerSectorView>(parameters: new APIParameters
            //     {
            //         TableName = "CustomerSector C",
            //         SelectFields = "C.ID_CustomerSector AS CustomerSectorID,C.CussectyName AS Name,C.CussectyShortName AS ShortName,C.SortOrder AS SortOrder",
            //         Criteria = "C.Cancelled=0 AND C.Passed=1",
            //         SortFields = "",
            //         GroupByFileds = ""
            //     },


            //   companyKey: _userLoginInfo.CompanyKey
            //);
            //     return Json(data, JsonRequestBehavior.AllowGet);

            CustomerSectorModel customerSector = new CustomerSectorModel();



            var data = customerSector.GetCustomerSectorData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerSectorModel.InputCustomerSectorID { ID_CustomerSector = 0,FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy,PageIndex=pageIndex+1,PageSize=pageSize, Name = Name, TransMode = transMode });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
          //  return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount}, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        //Add New Customersector//
        public ActionResult AddNewCustomerSectorDetails(CustomerSectorModel.CustomerSectorInputView newCusSector)
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
            ModelState.Remove("CustomerSectorID");
            ModelState.Remove("ReasonID");
            ModelState.Remove("SortOrder");
            ModelState.Remove("TotalCount");
            #region :: Model validation  ::



            ModelState.Remove("CustomerSectorID");
            ModelState.Remove("SortOrder");

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

            //DistrictModel states = new DistrictModel();
            CustomerSectorModel Cussector = new CustomerSectorModel();

            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            
            //  DistrictModel.DistrictView district = new DistrictModel.DistrictView
            var dataresponse = Cussector.UpdateCustomerSectorData(input: new CustomerSectorModel.UpdateCustomerSector
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = newCusSector.TransMode,
                Debug = 0,

                ID_CustomerSector = 0,
                CussectyName = newCusSector.Name,
                CussectyShortName = newCusSector.ShortName,
                SortOrder = newCusSector.SortOrder

            }, companyKey: _userLoginInfo.CompanyKey);
      
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        //update CustomerSector
        public ActionResult UpdateCustomerSectorDetails(CustomerSectorModel.CustomerSectorInputView data)
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

            ModelState.Remove("SortOrder");
            ModelState.Remove("ReasonID");
            ModelState.Remove("TotalCount");
            #region :: Model validation  ::



            ModelState.Remove("CustomerSectorID");
            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("TotalCount");
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

            CustomerSectorModel customerSector = new CustomerSectorModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
          
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
      

            var dataresponse = customerSector.UpdateCustomerSectorData(input: new CustomerSectorModel.UpdateCustomerSector
            {
                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,

                ID_CustomerSector = data.CustomerSectorID,
                CussectyName = data.Name,
                CussectyShortName = data.ShortName,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        //delete customer sector
        public ActionResult DeleteCustomerSectorInfo(CustomerSectorModel.CustomerSectorInputView data)
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
            ModelState.Remove("Name");
            ModelState.Remove("ShortName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("TotalCount");


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

            CustomerSectorModel.DeleteCustomerSector Cussector = new CustomerSectorModel.DeleteCustomerSector
            {
                ID_CustomerSector = data.CustomerSectorID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<CustomerSectorModel.DeleteCustomerSector>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proCustomerSectorDelete", parameter: Cussector);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetCustomerSectorReasonList()
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
        public ActionResult GetCustomerSectorInfoByID(CustomerSectorModel.CustomerSectorInputView data)
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
            ModelState.Remove("SortOrder");
            ModelState.Remove("ShortName");
            ModelState.Remove("Name");
            ModelState.Remove("ReasonID");
            ModelState.Remove("TotalCount");

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

            CustomerSectorModel cussector = new CustomerSectorModel();
            var cussectorInfo = cussector.GetCustomerSectorData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerSectorModel.InputCustomerSectorID { ID_CustomerSector = data.CustomerSectorID, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy });
            return Json(cussectorInfo, JsonRequestBehavior.AllowGet);

        }

    }
}