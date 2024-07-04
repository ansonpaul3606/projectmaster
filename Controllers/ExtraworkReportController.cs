using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.ExtraworkReportModel;

namespace PerfectWebERP.Controllers
{
    public class ExtraworkReportController : Controller
    {
        // GET: ExtraworkReport
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
        public ActionResult LoadExtraworkReport()
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

            ExtraworkReportModel.ExtraworkReportView ExtraworkReportList = new ExtraworkReportModel.ExtraworkReportView();
            ExtraworkReportModel model = new ExtraworkReportModel();



            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[ProjectStages]",
                SelectFields = "[ID_ProjectStages] AS ProjectStagesID,[PrStName] AS StageName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Stages = Common.GetDataViaQuery<ExtraworkReportModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            ExtraworkReportList.StageList = Stages.Data;


            var WorkTypeList = Common.GetDataViaQuery<ExtraworkReportModel.WorkTypeList>(parameters: new APIParameters
            {
                TableName = "ExtraWorkType EWT",
                SelectFields = "EWT.ID_ExtwrkType AS WorkTypeID,EWT.EWTName AS[WorkType]",
                Criteria = "EWT.Cancelled=0 AND EWT.Passed=1 AND EWTExtraWorkType=1 AND EWT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );
            ExtraworkReportList.WorkTypeList = WorkTypeList.Data;


            var projectList = model.GetProjectData(companyKey: _userLoginInfo.CompanyKey, input: new ExtraworkReportModel.Popupinput
            {
                Pagemode = 41,
                Critrea1 = 0,
                Critrea2 = 0,
                Critrea3 = "",
                Critrea4 = "",
                FK_Company=_userLoginInfo.FK_Company

            });
            ExtraworkReportList.ProjectList = projectList.Data;


            var SuppInfo = model.GetSupplierModeData(companyKey: _userLoginInfo.CompanyKey, input: new ExtraworkReportModel.Modetype { Mode = 85 });

            ExtraworkReportList.listMode = SuppInfo.Data;

            var Subcontarctorlist = model.GetSubcontractorpopup(companyKey: _userLoginInfo.CompanyKey, input: new ExtraworkReportModel.Popupinput
            {
                Pagemode = 123, //subcontractor
                Critrea1 = 0,
                Critrea2 = 0,
                Critrea3 = "",
                Critrea4 = "",
                FK_Company = _userLoginInfo.FK_Company
            });
            ExtraworkReportList.SubcontractorList = Subcontarctorlist.Data;

            return PartialView("_AddExtraworkReport", ExtraworkReportList);

        }

        [HttpPost]
        public ActionResult GetReportData(Reportdto input)
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
            ExtraworkReportModel objfld = new ExtraworkReportModel();


            var data = objfld.GetReportData(input: new Reportdto
            {
                FK_WorkType =input.FK_WorkType,
                FromDate=input.FromDate,
                ToDate=input.ToDate,
                FK_Project=input.FK_Project,
                FK_Stage = input.FK_Stage,
                FK_Supplier = input.FK_Supplier,  
               // PageIndex = input.PageIndex + 1,
                PageIndex = 0 ,
                PageSize = input.PageSize,
                FK_Company=_userLoginInfo.FK_Company
            
            }, companyKey: _userLoginInfo.CompanyKey);


            //return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, input.PageSize, input.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
    }
}