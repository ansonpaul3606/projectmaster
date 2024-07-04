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
    public class WarrantyTypeController : Controller
    {
        // GET: WarrantyType
        public ActionResult WarrantyTypeIndex()
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

            return View();
        }

        public ActionResult LoadWarrantyForm()
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

            WarrantyTypeModel.WarrantyList output = new WarrantyTypeModel.WarrantyList();
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "WarrantyType",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            output.SortOrder = a.Data[0].NextNo;
            return PartialView("_AddWarrantyType",output);
        }
        [HttpPost]
        public ActionResult AddWarrantyType(WarrantyTypeModel.WarantyTypeInputView data)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
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

            WarrantyTypeModel warrtyp = new WarrantyTypeModel();

            var datresponse = warrtyp.UpdateWarrantyData(input: new WarrantyTypeModel.UpdateWarranty
            {
                

                WartyName=data.WarrantyTypeName,
                WartyShortName=data.WarrantyTypeShortName,
                BackupId=data.WarrantyTypeID,
                FK_WarrantyType=data.WarrantyTypeID,
                ID_WarrantyType=data.WarrantyTypeID,


                UserAction=1,
                //WarrantyName=data.WarrantyTypeName,
                //WarrantyShortName=data.WarrantyTypeShortName,
                SortOrder=data.SortOrder,
                IsExtended=data.IsExtended,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode,
                Debug = 0

            },

            companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetWarrantyList(int pageSize, int pageIndex, string Name, string TransMode)
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

            WarrantyTypeModel Warrantytype = new WarrantyTypeModel();

            var data = Warrantytype.GetWarrantyTypeData(companyKey: _userLoginInfo.CompanyKey, input: new WarrantyTypeModel.GetWarrantyType
            {
                FK_WarrantyType = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine=_userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = TransMode,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
            });

          
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        

            [HttpPost]
        public ActionResult GetWarrantyTypeInfo(WarrantyTypeModel.WarrantyTypeView warrantyInfo)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visibleGetWarrantyInfo
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


            WarrantyTypeModel warrantytype = new WarrantyTypeModel();

            var warInfo = warrantytype.GetWarrantyTypeData(companyKey: _userLoginInfo.CompanyKey, input: new WarrantyTypeModel.GetWarrantyType
            {
                FK_WarrantyType = warrantyInfo.WarrantyTypeID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_Machine=_userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy
            });

            return Json(warInfo, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DeleteWarrantytype(WarrantyTypeModel.WarrantyTypeInfoView data)
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

            ModelState.Remove("WarrantyTypeID");

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

            WarrantyTypeModel warrantytype = new WarrantyTypeModel();




            var datresponse = warrantytype.DeleteWarrantyData(input: new WarrantyTypeModel.DeleteWarrantyType
            {
                FK_WarrantyType = data.WarrantyTypeID,
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

        public ActionResult UpdateWarrantyType(WarrantyTypeModel.WarantyTypeInputView data)
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

            #region :: Model validation  ::
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

            WarrantyTypeModel unit = new WarrantyTypeModel();

            var datresponse = unit.UpdateWarrantyData(input: new WarrantyTypeModel.UpdateWarranty
            {
                WartyName = data.WarrantyTypeName,
                WartyShortName = data.WarrantyTypeShortName,
                BackupId = data.WarrantyTypeID,
                FK_WarrantyType = data.WarrantyTypeID,
                ID_WarrantyType = data.WarrantyTypeID,

                UserAction = 2,
                //WarrantyName = data.WarrantyTypeName,
                //WarrantyShortName = data.WarrantyTypeShortName,
                //IDWarrantyType = data.WarrantyTypeID,
                EntrBy = _userLoginInfo.EntrBy,

                // EntrOn = data.EntrOn,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                SortOrder = data.SortOrder,
                IsExtended=data.IsExtended,
                FK_Machine = _userLoginInfo.FK_Machine,

                TransMode = data.TransMode,
                Debug=0
           
                // FK_Reason = _userLoginInfo.FK_Reason,
               
                
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWarrantyDeleteReasonList()
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