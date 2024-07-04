using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.General;
using System.Dynamic;
using Newtonsoft.Json;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using System.Reflection;
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ReportSettingController : Controller
    {
        // GET: ReportSetting
        public ActionResult ReportSetting()
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
        public ActionResult ReportView()
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

        public ActionResult LoadFormReportSetting()
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

            ReportSettingModel.ReportSettingList rptListObj = new ReportSettingModel.ReportSettingList();

            ReportSettingModel objrpt = new ReportSettingModel();

            var frmtlist = objrpt.GetReportFormat(input: new ReportSettingModel.ModeLead
            {
                ReportFormat = 0

            },
                companyKey: _userLoginInfo.CompanyKey);

            rptListObj.FrmtNames = frmtlist.Data;

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "ReportSettings",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            rptListObj.SortOrd = a.Data[0].NextNo;

            return PartialView("_AddReportSetting", rptListObj);
        }


        public ActionResult LoadFormProductWise()
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
            ProductionWiseServiceModel.PrdwiseViewList prdListObj = new ProductionWiseServiceModel.PrdwiseViewList();

            var prdwiseServlist = Common.GetDataViaQuery<ProductionWiseServiceModel.Services>(parameters: new APIParameters
            {
                TableName = "Services",
                SelectFields = "ID_Services AS ServiceID,ServicesName AS ServiceList",
                Criteria = "cancelled=0 AND Passed=1 and FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            prdListObj.ServiceList = prdwiseServlist.Data;

            ProductionWiseServiceModel objPrdSer = new ProductionWiseServiceModel();

            var PerdtypList = objPrdSer.GetPerdTypList(input: new ProductionWiseServiceModel.ModeLead { Mode = 3 },
                companyKey: _userLoginInfo.CompanyKey);

            prdListObj.PerdList = PerdtypList.Data;

            return PartialView("_AddPrdWiseService", prdListObj);
        }

        [HttpPost]
        public ActionResult GetReportList(int pageSize, int pageIndex,string Name)
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

            ReportSettingModel objrep = new ReportSettingModel();

            var data = objrep.GetReportSettingData(companyKey: _userLoginInfo.CompanyKey, input: new ReportSettingModel.ReportSettingID
            {

                FK_ReportSettings = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name=Name,
                TransMode = transMode

            });


            if (data.Data != null)
            {
                return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { pageSize, pageIndex }, JsonRequestBehavior.AllowGet);
            }




        }


        public ActionResult getMstrList(Int32 frmtID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ReportSettingModel objrpt = new ReportSettingModel();

            var data = objrpt.GetReportMastr(input: new ReportSettingModel.ModeLead
            {
                ReportFormat = frmtID

            },

          companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult getFieldList(Int32 masterID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ReportSettingModel objfld = new ReportSettingModel();

            var data = objfld.GetReportField(input: new ReportSettingModel.FldLead
            {
                ReportMasterTable = masterID

            },

          companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddReportSetting(ReportSettingModel.ReportSettingModelView rptsett)
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

            ModelState.Clear();

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

            ReportSettingModel reptobje = new ReportSettingModel();

            var datresponse = reptobje.UpdateReportSettingData(input: new ReportSettingModel.UpdateReportSettingList
            {
                ID_ReportSettings = 0,
                UserAction = 1,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = "",
                FK_ReportFormat = rptsett.FrmtID,
                FK_ReportMasterTable = rptsett.MasterID,
                RpsName = rptsett.RpName,
                RpsSubName = rptsett.SbName,
                SortOrder = rptsett.SortOrd,
                SortFields = rptsett.SortArea,

                ReportSettingsDetailsType = rptsett.ReportSettingsDetailsType is null ? "" : Common.xmlTostring(rptsett.ReportSettingsDetailsType),


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetReportSettinginfo(ReportSettingModel.ReportSettingID Data)
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

            ReportSettingModel Objrptset = new ReportSettingModel();



            var ReportDetails = Objrptset.GetReportSettingData(companyKey: _userLoginInfo.CompanyKey, input: new ReportSettingModel.ReportSettingID
            {
                FK_ReportSettings = Data.FK_ReportSettings,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy
            });

            var ReportSettingsDetailsType = Objrptset.GetReportSettingdetailsData(companyKey: _userLoginInfo.CompanyKey, input: new ReportSettingModel.ReportSettingID
            {
                FK_ReportSettings = Data.FK_ReportSettings,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy
            });




            return Json(new { ReportDetails, ReportSettingsDetailsType }, JsonRequestBehavior.AllowGet);

        }



        public ActionResult GetReptSettingdelReasonList()
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


            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID
            {
                FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""
            });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        //delete productwise type
        public ActionResult DeleteReptSettInfo(ReportSettingModel.ReptSettRsnView data)
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


            ModelState.Remove("RpName");
            ModelState.Remove("FrmtID");
            ModelState.Remove("SbName");



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

            ReportSettingModel.ReptSettingDelete objrptdel = new ReportSettingModel.ReptSettingDelete
            {
                FK_ReportSettings = data.ID_ReportSettings,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<ReportSettingModel.ReptSettingDelete>(
                companyKey: _userLoginInfo.CompanyKey, procedureName: "ProReportSettingsDelete", parameter: objrptdel);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadReportViewForm()
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

            ReportSettingModel.ReportSettingList rptview = new ReportSettingModel.ReportSettingList();

          
            var FormatName = Common.GetDataViaQuery<ReportSettingModel.ReportNames>(parameters: new APIParameters
            {
                TableName = "ReportSettings",
                SelectFields = " ID_ReportSettings ReptId,RpsName RptName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = " ID_ReportSettings"
            },
            companyKey: _userLoginInfo.CompanyKey
           );

            rptview.reportNames = FormatName.Data;

            return PartialView("_AddReportView", rptview);
        }


        public ActionResult getReportViewList(DateTime FromDate, DateTime ToDate, Int32 Report)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ReportSettingModel objfld = new ReportSettingModel();

                var data = objfld.GetReportView(input: new ReportSettingModel.RptFld
                {
                    FromDate = FromDate,
                    ToDate = ToDate,
                    FK_ReportSettings = Report,
                    FK_Company= _userLoginInfo.FK_Company

                },

            companyKey: _userLoginInfo.CompanyKey);



                return Json(data, JsonRequestBehavior.AllowGet);


        }
        public class KeyValuePair
        {
            public string key { get; set; }
            public string value { get; set; }
        }

    }
}

