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
    public class PaperRenewalReportController : Controller
    {
        // GET: PaperRenewalReport
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


            var headName = ViewBag.TransMode.Substring(0, 2);

            string loghead = "";
           

            if (headName == "VL")
            {
                loghead = "Vehicle";
            }
            else if (headName == "TO")
            {
                loghead = "Tool";
            }
            ViewBag.headlog = loghead;
            

            return View();
        }

        public ActionResult BindProvider(PaperModel.PaperMaster data)
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


            APIParameters apiSub = new APIParameters
            {
                TableName = "[Provider]",
                SelectFields = "[ID_Provider] AS ID_Provider,[ProvName] AS ProvName",
                Criteria = "Passed=1 And Cancelled=0 And FK_Paper='" + data.FK_Paper + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var ProviderInfo = Common.GetDataViaQuery<PaperModel.Provider>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(ProviderInfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LoadFormpaperrenewalReport(string mtd,string TransMode)
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
            Renewalreportmodel.RenewalReportView ReportList = new Renewalreportmodel.RenewalReportView();
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
                    loghead = " Vehicle Paper Renewal Monitoring";

                    desc = "Route";

                    break;
                case "TO":
                    modeData = "'T'";
                    labelpopup = "Operator";
                    lblpro = "Tool";
                    loghead = "Tool Paper Renewal Monitoring";
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
            var vehi = Common.GetDataViaQuery<Renewalreportmodel.ProviderList>(parameters: new APIParameters
            {
                TableName = "Provider",
                SelectFields = "ID_Provider AS FK_Provider ,ProvName AS PaperProviderName",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode=" + modeData + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Provider",
                GroupByFileds = ""
            },
                      companyKey: _userLoginInfo.CompanyKey

                         );
            ReportList.ProviderList = vehi.Data;
            var paper = Common.GetDataViaQuery<Renewalreportmodel.PaperList>(parameters: new APIParameters
            {
                TableName = "Paper",
                SelectFields = "ID_Paper AS FK_Paper ,PaperName AS PaperName",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode=" + modeData +" AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Paper",
                GroupByFileds = ""
            },
               companyKey: _userLoginInfo.CompanyKey

      );
            ReportList.PaperList = paper.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("RenewalReport", ReportList);
        }
        public ActionResult GetRenewListOnLoad(Renewalreportmodel.RenewalReportView data, string TransMode)
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

            Renewalreportmodel amc = new Renewalreportmodel();
           

       
      
       
       
       
     
        var outputList = amc.GetRenewData(companyKey: _userLoginInfo.CompanyKey, input: new Renewalreportmodel.renewalreport
            {
                Mode= data.Mode,
                FK_Provider = data.FK_Provider,
                FK_Papper = data.FK_Papper,            
                DemandDays = data.DemandDays,
                AsonDate = data.AsonDate,
                PmdPaperNo=data.PmdPaperNo,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_ModuleType = data.FK_ModuleType,
                 Pageindex = 0,
                 PageSize = 0,



        });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetrenewList(Renewalreportmodel.renewalreport data)
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

            Renewalreportmodel amc = new Renewalreportmodel();


            var outputList = amc.GetRenewData(companyKey: _userLoginInfo.CompanyKey, input: new Renewalreportmodel.renewalreport
            {
                Mode = data.Mode,
                FK_Provider = data.FK_Provider,
                FK_Papper = data.FK_Papper,
                DemandDays = data.DemandDays,
                AsonDate = data.AsonDate,
                PmdPaperNo = data.PmdPaperNo,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_ModuleType = data.FK_ModuleType,
                Pageindex = data.Pageindex + 1,
                PageSize = data.PageSize,
            });
            //return Json(new { outputList }, JsonRequestBehavior.AllowGet);
            return Json(new { outputList.Process, outputList.Data, data.PageSize, data.Pageindex, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

    }
}
            
        