using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class EquipmentServiceReportController : Controller
    {
        // GET: EquipmentServiceReport
        public ActionResult Index(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;

            return View();
        }


        public ActionResult LoadFormServiceEquipmentReport()
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
            EquipmentServiceReportModel.ServiceReportView ReportList = new EquipmentServiceReportModel.ServiceReportView();
            var Maintenancetype = Common.GetDataViaQuery<EquipmentServiceReportModel.MaintenancetypeList>(parameters: new APIParameters

            {
                TableName = "Maintenance",
                SelectFields = "ID_Maintenance AS FK_Master,MaintenanceName as Maintenancetype",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

         companyKey: _userLoginInfo.CompanyKey
             );
            ReportList.MaintenancetypeList = Maintenancetype.Data;

            var Incidencetype = Common.GetDataViaQuery<EquipmentServiceReportModel.IncidencetypeList>(parameters: new APIParameters
            {
                TableName = "Incidence",
                SelectFields = "ID_Incidence AS FK_Master,IncidenceName AS Incidencetype",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                  companyKey: _userLoginInfo.CompanyKey

       );
            ReportList.IncidencetypeList = Incidencetype.Data;

            return PartialView("_AddEquipmentServiceReport", ReportList);
        }
        public ActionResult GetServiceEquipmentOnLoad(EquipmentServiceReportModel.ServiceReportView data)
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

            EquipmentServiceReportModel amc = new EquipmentServiceReportModel();








            var outputList = amc.GetServiceData(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentServiceReportModel.servicereport
            {
                FK_Mode = data.FK_Mode,
                FK_Transaction = data.FK_Transaction,
                FK_Type = data.FK_Type,                
                FromDate = data.FromDate,
                ToDate=data.ToDate,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,              
               // Pageindex = 0,
              //  PageSize = 0,



            });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEquipmentServiceList(EquipmentServiceReportModel.servicereport data)
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

            EquipmentServiceReportModel amc = new EquipmentServiceReportModel();


            var outputList = amc.GetServiceData(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentServiceReportModel.servicereport
            {
                FK_Mode = data.FK_Mode,
                FK_Transaction = data.FK_Transaction,
                FK_Type = data.FK_Type,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,           
                //Pageindex = data.Pageindex + 1,
                //PageSize = data.PageSize,
            });
            //return Json(new { outputList }, JsonRequestBehavior.AllowGet);
            return Json(new { outputList.Process, outputList.Data, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetALType(int FK_Mode)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            EquipmentServiceReportModel.ServiceReportView list = new EquipmentServiceReportModel.ServiceReportView();
            if (FK_Mode == 1)
            {
                var Maintenancetype = Common.GetDataViaQuery<EquipmentServiceReportModel.ModeList>(parameters: new APIParameters

                {
                    TableName = "Maintenance",
                    SelectFields = "ID_Maintenance AS FK_Transaction,MaintenanceName as ModeName",
                    Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "",
                    GroupByFileds = ""
                },

             companyKey: _userLoginInfo.CompanyKey
                 );
                list.ModeList = Maintenancetype.Data;
            }
            if (FK_Mode == 2)
            {
                var Incidencetype = Common.GetDataViaQuery<EquipmentServiceReportModel.ModeList>(parameters: new APIParameters
                {
                    TableName = "Incidence",
                    SelectFields = "ID_Incidence AS FK_Transaction,IncidenceName AS ModeName",
                    Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "",
                    GroupByFileds = ""
                },
               companyKey: _userLoginInfo.CompanyKey

    );
                list.ModeList = Incidencetype.Data;
            }

            return Json(list.ModeList, JsonRequestBehavior.AllowGet);
        }

        #region[GetEqServiceReportList]
        public ActionResult GetEqServiceReportList(EquipmentServiceReportModel.servicereport servicereport)
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

            EquipmentServiceReportModel equipment = new EquipmentServiceReportModel();

            var data = equipment.GetEqServiceGeneralReport(input: new EquipmentServiceReportModel.servicereport
            {
                ToDate = servicereport.ToDate,
                FromDate = servicereport.FromDate,
                FK_Type = servicereport.FK_Type,
                FK_Mode = servicereport.FK_Mode,
                FK_Transaction = servicereport.FK_Transaction,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}


