using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class SLASettingsReportController : Controller
    {
        // GET: SLASettingsReport
        #region[Index]
        public ActionResult Index(string mtd, string mgrp)
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
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.TransMode = mGrp;
            return View();
        }
        #endregion

        #region[LoadSLAReport]

        public ActionResult LoadSLAReport(string mtd, string TransMode)
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

            SLASettingsModel model = new SLASettingsModel();

            SLASettingsModel.SLAReportView view = new SLASettingsModel.SLAReportView();

            CommonMethod commonMethod = new CommonMethod();
            ViewBag.PageTitle = commonMethod.DecryptString(mtd);

            var complaintListData = Common.GetDataViaQuery<SLASettingsModel.ComplaintList>(parameters: new APIParameters
            {
                TableName = "ComplaintList",
                SelectFields = "ID_ComplaintList AS ID_ComplaintList ,CompntName AS CompntName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );
            view.complaintLists = complaintListData.Data;


            var customerCatData = Common.GetDataViaQuery<SLASettingsModel.CustomerCategory>(parameters: new APIParameters
            {
                TableName = "CustomerCategory   ",
                SelectFields = "ID_CustomerCategory AS ID_CustomerCategory ,CuscattyName AS CuscattyName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
               companyKey: _userLoginInfo.CompanyKey
            );
            view.customerCategories = customerCatData.Data;

            var modeList = model.GetSlaModeReport(input: new SLASettingsModel.ModeInput { Mode = 101 },companyKey: _userLoginInfo.CompanyKey);

            view.ModeList = modeList.Data;

            CustomerserviceregisterModel cstrmdl = new CustomerserviceregisterModel();

            var Category = Common.GetDataViaQuery<SLASettingsModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS CategoryID ,CatName AS CategoryName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
                   companyKey: _userLoginInfo.CompanyKey

                      );
            view.CategoryList = Category.Data;

            return PartialView("_AddSLASettingsReport", view);
        }

        #endregion
    }
}