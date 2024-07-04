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
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class MeasurementUnitController : Controller
    {


             // GET: MeasurementUnit
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

            public ActionResult LoadMeasurementUnitForm()
            {


                UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            MeasurementUnitModel.MeasurementUnitListModel MUList = new MeasurementUnitModel.MeasurementUnitListModel();



                var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                  companyKey: _userLoginInfo.CompanyKey,
                  procedureName: "ProGetNextNo",
                  parameter: new NextSortOrder
                  {
                      TableName = "MeasurementUnit",
                      FieldName = "SortOrder",
                      Debug = 0
                  });

                MUList.SortOrder = a.Data[0].NextNo;




                return PartialView("_AddMeasurementUnit", MUList);
            }


            [HttpGet]
            public ActionResult GetMeasurementUnitList()
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


                MeasurementUnitModel measurementUnit = new MeasurementUnitModel();
                var data = measurementUnit.GetMeasurementUnitData(input: new MeasurementUnitModel.InputMeasurementUnitID { FK_MeasurementUnit = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);

                return Json(data, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult GetMeasurementUnitInfoByID(MeasurementUnitModel.MeasurementUnitInfoView data)
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

                MeasurementUnitModel measurement = new MeasurementUnitModel();
                var measureInfo = measurement.GetMeasurementUnitData(companyKey: _userLoginInfo.CompanyKey, input: new MeasurementUnitModel.InputMeasurementUnitID { FK_MeasurementUnit = data.MeasurementUnitID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

                return Json(measureInfo, JsonRequestBehavior.AllowGet);
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult AddNewMeasurementUnitDetails(MeasurementUnitModel.MeasurementUnitsInputFromViewModel data)
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

                MeasurementUnitModel measurement = new MeasurementUnitModel();


             
                var dataresponse = measurement.UpdateMeasurementUnitData(input: new MeasurementUnitModel.UpdateMeasurementUnit
                {
                    UserAction = 1,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = data.TransMode,
                    FK_MeasurementUnit=0,
                    ID_MeasurementUnit = 0,
                    MUName = data.MeasurementUnit,
                    MUShortName = data.ShortName,
                    MUDescription=data.Description,
                    SortOrder = data.SortOrder,
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }



            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult UpdateMeasurementUnitDetails(MeasurementUnitModel.MeasurementUnitsInputFromViewModel data)
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

                MeasurementUnitModel measurement = new MeasurementUnitModel();


              
                var dataresponse = measurement.UpdateMeasurementUnitData(input: new MeasurementUnitModel.UpdateMeasurementUnit
                {
                    UserAction = 2,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = data.TransMode,
                    FK_MeasurementUnit = data.MeasurementUnitID,
                    ID_MeasurementUnit = data.MeasurementUnitID,
                    MUName = data.MeasurementUnit,
                    MUShortName = data.ShortName,
                    MUDescription = data.Description,
                    SortOrder = data.SortOrder,
                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
            }

            [HttpPost]
            [ValidateAntiForgeryToken()]
            public ActionResult DeleteMeasurementUnitInfo(MeasurementUnitModel.MeasurementUnitInfoView data)
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

                MeasurementUnitModel measurement = new MeasurementUnitModel();
                var datresponse = measurement.DeleteMeasurementUnitData(input: new MeasurementUnitModel.DeleteMeasurementUnit { FK_MeasurementUnit = data.MeasurementUnitID, EntrBy= _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,TransMode="" }, companyKey: _userLoginInfo.CompanyKey);
                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }


        public ActionResult GetMeasurementUnitDeleteReasonList()
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