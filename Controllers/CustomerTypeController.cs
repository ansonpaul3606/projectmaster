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
    public class CustomerTypeController : Controller
    {
        public ActionResult CustomerType()
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
        public ActionResult GetCustomerTypeList(int pageSize, int pageIndex, string Name)
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

            CustomerTypeModel customerType = new CustomerTypeModel();

            var data = customerType.GetCustomerTypetData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerTypeModel.GetCustomerType
            {
                ID_CustomerType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });


            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
          //  return Json(new { outputList.Process, outputList.Data, pageSize, pageIndex, totalrecord = outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadCustomerTypeForm()
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

            // if any data need to be displayed with the form, then get the data and pass it a model with the partial view and handle that data in view

            //CustomerModel.CustomerFormViewModel formViewModel = new CustomerModel.CustomerFormViewModel();
            //return PartialView("_CustomerForm",formViewModel);

            //country-amith
            //state-jisi
            //place -diljith
            //district amritha
            //AreaReference aiswarya
            //HttpPostAttribute athul

            CustomerTypeModel.CustomerTypeView formListData = new CustomerTypeModel.CustomerTypeView();

            StateModel state = new StateModel();




            CustomerTypeModel.CustomerTypeFormData dis = new CustomerTypeModel.CustomerTypeFormData();
            var dislidt = Common.GetDataViaQuery<CustomerTypeModel.Sector>(parameters: new APIParameters
            {
                TableName = "CustomerSector",
                SelectFields = "ID_CustomerSector as SectorID, CussectyName As SectorName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey

                );
            dis.SectorList = dislidt.Data;

            
            var categorylst = Common.GetDataViaQuery<CustomerTypeModel.Categorys>(parameters: new APIParameters
            {
                TableName = "CustomerCategory",
                SelectFields = "ID_CustomerCategory as CategoryID, CuscattyName As CategoryName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey

                );

            dis.CategoryList = categorylst.Data;
         

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProGetNextNo",
                parameter: new NextSortOrder
                {
                    TableName = "CustomerType",
                    FieldName = "SortOrder",
                    Debug = 0
                });
            dis.SortOrder = a.Data[0].NextNo;


            return PartialView("_AddCustomerType", dis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]   //antiforgery token

        public ActionResult AddNewCustomerType(CustomerTypeModel.CustomerTypeView data)
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
            ModelState.Remove("Priority");
            ModelState.Remove("ReasonID");
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


            CustomerTypeModel customerType = new CustomerTypeModel();



            var datresponse = customerType.UpdateCustomerTypeData(input: new CustomerTypeModel.UpdateCustomerType
            {
                UserAction = 1,
                ID_CustomerType=0,
                CustyName = data.CustomerTypeName,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                CustyShortName = data.ShortName,
                CusPriority = data.Priority,
                SortOrder = data.SortOrder,
                TransMode=data.TransMode,
                FK_CustomerSector = data.SectorID,
                FK_CustomerCategory =data.CategoryID,
                CustyDefault = data.CustyDefault,

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]   //antiforgery token

        public ActionResult GetCustomerTypeInfo(CustomerTypeModel.CustomerTypeView customerTypeInfo)
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
            ModelState.Remove("CategoryID");
            ModelState.Remove("CategoryName");
            ModelState.Remove("SectorID");
            ModelState.Remove("SectorName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ShortName");
            ModelState.Remove("Priority");
            
            ModelState.Remove("CustomerTypeName");
            

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


            CustomerTypeModel customerType = new CustomerTypeModel();

            var cusInfo = customerType.GetCustomerTypetData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerTypeModel.GetCustomerType
            {
                ID_CustomerType = customerTypeInfo.CustomerTypeID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            return Json(cusInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]   //antiforgery token

        public ActionResult UpdateCustomerType(CustomerTypeModel.CustomerTypeView data)
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
            ModelState.Remove("CategoryID");
            ModelState.Remove("CategoryName");
            ModelState.Remove("SectorID");
            ModelState.Remove("SectorName");
            ModelState.Remove("SortOrder");   
            ModelState.Remove("Priority");
            


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

            CustomerTypeModel customerType = new CustomerTypeModel();



            var datresponse = customerType.UpdateCustomerTypeData(input: new CustomerTypeModel.UpdateCustomerType
            {
                UserAction = 2,
               
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,                
                FK_Machine = _userLoginInfo.FK_Machine,
                CusPriority=data.Priority,
                CustyShortName = data.ShortName,
                FK_CustomerSector = data.SectorID,
                FK_CustomerCategory = data.CategoryID,
                ID_CustomerType =data.CustomerTypeID,
                CustyName = data.CustomerTypeName,
                TransMode = data.TransMode,
                SortOrder =data.SortOrder,
                CustyDefault = data.CustyDefault,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]   //antiforgery token

        public ActionResult DeleteCustomerType(CustomerTypeModel.CustomerTypeView data)
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

            
            ModelState.Remove("CategoryID");
            ModelState.Remove("CategoryName");
            ModelState.Remove("SectorID");
            ModelState.Remove("SectorName");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ShortName");
            ModelState.Remove("Priority");

            ModelState.Remove("CustomerTypeName");
           

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

            CustomerTypeModel customerType = new CustomerTypeModel();

            var datresponse = customerType.DeleteCustomerTypeData(input: new CustomerTypeModel.DeleteCustomerType {

                ID_CustomerType = data.CustomerTypeID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerTypeDeleteReasonList()
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


