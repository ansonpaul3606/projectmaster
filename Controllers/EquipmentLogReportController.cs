using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class EquipmentLogReportController : Controller
    {
        // GET: EquipmentLogReport
        #region[Index]
        public ActionResult Index(string mtd,string mgrp)
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
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return View();
        }
        #endregion

        #region[LoadEqlogRepo]
        public ActionResult LoadEqlogRepo(string mtd,string TransMode)
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
            LogModel.LogListModel LogList = new LogModel.LogListModel();
            var mode = TransMode.Substring(0, 2);
            var modeData = "";
            string labelpopup = "";
            string lblpro = "";
            string loghead = "";
            string desc = " ";
            switch (mode)
            {
                case "IN":
                    modeData = "'P'";
                    break;
                case "VL":
                    modeData = "'V'";
                    labelpopup = "Driver";
                    lblpro = "Vehicle";
                    loghead = "Vehicle Log";
                    desc = "Route";

                    break;
                case "TO":
                    modeData = "'T'";
                    labelpopup = "Operator";
                    lblpro = "Tool";
                    loghead = "Tool Log";
                    desc = "Description";
                    break;
                case "AT":
                    modeData = "'A'";
                    break;
                default:
                    modeData = "'P'";
                    break;
            }
            ViewBag.lblemp = labelpopup;
            ViewBag.lblpro = lblpro;
            ViewBag.headlog = loghead;
            ViewBag.lbldesc = desc;
            ViewBag.tmode = mode;
            var MaintenanceView = Common.GetDataViaQuery<LogModel.TypeList>(parameters: new APIParameters
            {
                TableName = "Maintenance",
                SelectFields = "ID_Maintenance as TypeID,MaintenanceName as TypeName",
                Criteria = "cancelled=0 AND Passed =1 AND Mode=" + modeData + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            LogList.TypeListdata = MaintenanceView.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddEqLogReportform", LogList);
        }
        #endregion


        #region[GetEqlogReportgridViewList]
        public ActionResult GetEqlogReportgridViewList(LogModel.EqlogReportInput logView)
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

            LogModel logModel = new LogModel();

            var data = logModel.GetEqlogGeneralReport(input: new LogModel.EqlogReportInput
            {
                FromDate = logView.FromDate,
                ToDate = logView.ToDate,
                ID_Report = logView.ID_Report,
                ID_FIELD = logView.ID_FIELD,
                EmployeeID = logView.EmployeeID,
                FK_Maintenance = logView.FK_Maintenance,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = logView.TransMode
            }, companyKey: _userLoginInfo.CompanyKey);


            if (data is null)
            {
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = false,
                        Message = new List<string> { "Failed to retrieve data" },
                        Status = "Error",
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            //---  group by name and id ====>       //var groupedData = data.Data.GroupBy(x => new { x.Name, x.FK_VehicleLog }).Select(g => new
            else {

                var groupedData = data.Data.GroupBy(x => new { x.Name }).Select(g => new          //   <=== only name
                {
                    g.Key.Name,
                    //g.Key.FK_VehicleLog,
                    Logout = g.Select(x => new LogModel.LogSubViewOutputTable
                    {
                        FK_Maintenance = x.FK_Maintenance,
                        //MaintenanceDescription = x.MaintenanceDescription,
                        MaintenanceName = x.MaintenanceName,
                        EmpName = x.EmpName,
                        VtLogStartDate = x.VtLogStartDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        VtLogEndDate = x.VtLogEndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        FK_VehicleLog = x.FK_VehicleLog,
                        Name = x.Name,
                        VtLogNetAmount = x.VtLogNetAmount,
                        VtlogStartKm = x.VtlogStartKm,
                        VtlogEndKm = x.VtlogEndKm,
                        VtLogDescription = x.VtLogDescription,
                        VtlDetRemarks = x.VtlDetRemarks

                    }).ToList()


                });

                List<LogModel.LogViewTable> lgModel = new List<LogModel.LogViewTable>();
                foreach (var group in groupedData)
                {
                    lgModel.Add(new LogModel.LogViewTable
                    {
                        Name = group.Name,
                        //FK_VehicleLog = group.FK_VehicleLog,
                        Logout = group.Logout
                    });

                }


                APIGetRecordsDynamic<LogModel.LogViewTable> output = new APIGetRecordsDynamic<LogModel.LogViewTable>();
                output.Process = data.Process;
                output.Data = lgModel;
                return Json(output, JsonRequestBehavior.AllowGet);
            }
          

          
        }
        #endregion
    }
}