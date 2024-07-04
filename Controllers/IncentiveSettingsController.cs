using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.IncentiveSettingsModal;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class IncentiveSettingsController : Controller
    {
        // GET: IncentiveSettings
        public ActionResult Index()
        {
 
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;

            return View();
        }
      


        public ActionResult LoadIncetiveSettings()
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

            IncentiveSettingsModal.IncentiveSettingsList IncentiveSettingList = new IncentiveSettingsModal.IncentiveSettingsList();

            //DesignationList

            var designationList = Common.GetDataViaQuery<IncentiveSettingsModal.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            IncentiveSettingList.DesignationList = designationList.Data;
            //ServiceList
            var prdwiseServlist = Common.GetDataViaQuery<IncentiveSettingsModal.Services>(parameters: new APIParameters
            {
                TableName = "Services",
                SelectFields = "ID_Services AS ServiceID,ServicesName AS ServiceList",
                Criteria = "cancelled=0 AND Passed=1 and FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            IncentiveSettingList.ServiceList = prdwiseServlist.Data;

            //category
            var Category = Common.GetDataViaQuery<IncentiveSettingsModal.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS CategoryID ,CatName AS Category",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode='p' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey

              );
            IncentiveSettingList.CategoryList = Category.Data;

            return PartialView("_AddIncentiveSettings", IncentiveSettingList);
           
        }
        [HttpGet]
        public ActionResult GetDesignationList()
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
            var data = Common.GetDataViaQuery<IncentiveSettingsModal.DesignationList>(parameters: new APIParameters
            {
                TableName = "Designation D",
                SelectFields = "D.ID_Designation AS DesignationID,D.DesName AS[Designation]",
                Criteria = "D.Cancelled=0 AND D.Passed=1 AND D.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey

          );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetServiceList(int prdid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var data = Common.GetDataViaQuery<IncentiveSettingsModal.Service>(parameters: new APIParameters
            {
                TableName = "ProductWiseServicedetails PWS  join Services  S on S.ID_Services = PWS.FK_Services",
                SelectFields = "DISTINCT S.ID_Services AS ID_Services,S.ServicesName AS ServicesName",
                Criteria = "PWS.FK_ProductWiseServices in (SELECT ID_ProductWiseServices from ProductWiseServices where FK_Category =  " + prdid + " AND Cancelled =0 AND Passed=1)",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveEmployeedata(IncentiveSettingsModal.inputdata inputdata)
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
            IncentiveSettingsModal objfld = new IncentiveSettingsModal();

          

            var data = objfld.Saveincetivesetting(input: new IncentiveSettingsModal.inputdatalist
            {
                UserAction = 1,  //insert
                FK_Company = _userLoginInfo.FK_Company,
                FK_Designation= inputdata.Designation ,
                FK_Employee=inputdata.Employee,
                EffectDate=inputdata.EffectDate,
                SubIncentiveData = inputdata.SubIncentiveData is null ? "" : Common.xmlTostring(inputdata.SubIncentiveData),
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_Machine=_userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_ServiceIncentiveSettings=0,
                ID_ServiceIncentiveSettings=0,
                FK_AccountHead=inputdata.AccountHead,
                FK_AccountSubHead=inputdata.AccountHeadSub,
                IsActive=inputdata.IsActive

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

       // [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GetIncentiveSettingsData(int pageSize, int pageIndex, string Name)
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
            IncentiveSettingsModal objfld = new IncentiveSettingsModal();

                var data = objfld.IncentiveSettingsListData(input: new IncentiveSettingsModal.IncentiveSettingsListDatainputModal
                {

                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_ServiceIncentiveSettings = 0,
                    PageIndex = pageIndex + 1,
                    Name = Name,
                    PageSize = pageSize,
                    Detailed = false

                }, companyKey: _userLoginInfo.CompanyKey);

            //return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleateIncentiveSettingsData(IncentiveSettingsModal.IncentiveSettingsDeleateinputModal inputdata)
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
            IncentiveSettingsModal objfld = new IncentiveSettingsModal();

            var data = objfld.IncentiveSettingsDealte(input: new IncentiveSettingsModal.IncentiveSettingsDeleateinputModal
            {

                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason=inputdata.FK_Reason,
                ID_IncentiveTransaction = inputdata.ID_IncentiveTransaction,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult GetIncentivetableData(IncentiveSettingsModal.IncentiveSettingsListDatainputModal inputdata)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            IncentiveSettingsModal objfld = new IncentiveSettingsModal();

            //test
            var maindata = objfld.IncentiveSettingsListData(input: new IncentiveSettingsModal.IncentiveSettingsListDatainputModal
            {

                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_ServiceIncentiveSettings = inputdata.FK_ServiceIncentiveSettings,
                Detailed =false

            }, companyKey: _userLoginInfo.CompanyKey);

            var subdata = objfld.IncentiveSettingstableData(input: new IncentiveSettingsModal.IncentiveSettingsListDatainputModal
            {

                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_ServiceIncentiveSettings = inputdata.FK_ServiceIncentiveSettings,
                Detailed = true

            }, companyKey: _userLoginInfo.CompanyKey);


           


                return Json(new{ maindata, subdata}, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetReasonList()
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
            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateIncetiveSettings(IncentiveSettingsModal.inputdata inputdata)
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
            IncentiveSettingsModal objfld = new IncentiveSettingsModal();




            var data = objfld.Saveincetivesetting(input: new IncentiveSettingsModal.inputdatalist
            {
                UserAction = 2,  //update
                FK_Company = _userLoginInfo.FK_Company,
                FK_Designation = inputdata.Designation,
                FK_Employee = inputdata.Employee,
                EffectDate = inputdata.EffectDate,
                SubIncentiveData = inputdata.SubIncentiveData is null ? "" : Common.xmlTostring(inputdata.SubIncentiveData),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_ServiceIncentiveSettings = 0,
                FK_AccountHead = inputdata.AccountHead,
                FK_AccountSubHead = inputdata.AccountHeadSub,
                IsActive = inputdata.IsActive,
                ID_ServiceIncentiveSettings =inputdata.ID_ServiceIncentiveSettings

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
   


}