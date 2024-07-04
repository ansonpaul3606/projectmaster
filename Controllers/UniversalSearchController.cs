using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class UniversalSearchController : Controller
    {
        // GET: UniversalSearch
        public ActionResult Index()
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

        public ActionResult LoadUniversalSearchlist()
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
            return PartialView("_AddUniversalSearchForm");
        }


        public ActionResult GetUniversalSearchList(UniversalSearchModel.UniversalsearchView data, int pageSize, int pageIndex)
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

            UniversalSearchModel datas = new UniversalSearchModel();

            var outputList = datas.GetUniversalSearchData(companyKey: _userLoginInfo.CompanyKey, input: new UniversalSearchModel.UniversalsearchView
            {
                Mode = data.Mode,
                Name = data.Name,
                MobileNo = data.MobileNo,
                Address = data.Address,
                ReferenceNo = data.ReferenceNo,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = 0,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,

            });
            return Json(new { outputList.Process, outputList.Data, pageIndex, pageSize, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);


        }

        public ActionResult LoadCustomerDetails(long ReferenceID, string TransMode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            ServiceListModel mdl = new ServiceListModel();
            ServiceListModel.ServiceListModelDetails dtls = new ServiceListModel.ServiceListModelDetails();
            dtls.TransMode = TransMode;
            var CustmInfo = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
            {
                ReferenceID = ReferenceID,
                TransMode = TransMode,
                Detailed = 0,
                CheckList = 1
            }, companyKey: _userLoginInfo.CompanyKey).Data;
            if (CustmInfo != null)
            {
                if (CustmInfo.Rows.Count > 0)
                {
                    dtls.Name = Convert.ToString(CustmInfo.Rows[0]["Name"]);
                    dtls.Address1 = Convert.ToString(CustmInfo.Rows[0]["Address1"]);
                    dtls.Address2 = Convert.ToString(CustmInfo.Rows[0]["Address2"]);
                    dtls.Area = Convert.ToString(CustmInfo.Rows[0]["Area"]);
                    dtls.District = Convert.ToString(CustmInfo.Rows[0]["District"]);
                    dtls.Email = Convert.ToString(CustmInfo.Rows[0]["Email"]);
                    dtls.GSTIN = Convert.ToString(CustmInfo.Rows[0]["GSTINNo"]);
                    dtls.Landmark = Convert.ToString(CustmInfo.Rows[0]["Landmark"]);
                    dtls.Mobile = Convert.ToString(CustmInfo.Rows[0]["Mobile"]);
                    dtls.PhoneNo = Convert.ToString(CustmInfo.Rows[0]["PhoneNo"]);
                    dtls.PinCode = Convert.ToString(CustmInfo.Rows[0]["PinCode"]);
                    dtls.Post = Convert.ToString(CustmInfo.Rows[0]["Post"]);
                    dtls.State = Convert.ToString(CustmInfo.Rows[0]["State"]);

                }
            }
            dtls.DtTransHistryList = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
            {
                ReferenceID = ReferenceID,
                TransMode = TransMode,
                Detailed = 0,
                CheckList = 2
            }, companyKey: _userLoginInfo.CompanyKey).Data;

            dtls.DtEMIList = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
            {
                ReferenceID = ReferenceID,
                TransMode = TransMode,
                Detailed = 0,
                CheckList = 3
            }, companyKey: _userLoginInfo.CompanyKey).Data;
           

            return View(dtls);
        }

        public ActionResult LoadCustomerDetailsDatas(long ReferenceID, string TransMode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ServiceListModel.ServiceListModelDetailsData dtls = new ServiceListModel.ServiceListModelDetailsData();
            dtls.TransMode = TransMode;
            ServiceListModel mdl = new ServiceListModel();
            int Detailed = 0, CheckList = 0;
            if (TransMode == "INSL")
            {
                Detailed = 1;
                CheckList = 1;
            }
            else if (TransMode == "LFLG")
            {
                Detailed = 1;
                CheckList = 2;
            }
            else if (TransMode == "INSO")
            {
                Detailed = 1;
                CheckList = 3;
            }
            else if (TransMode == "PRCR")
            {
                Detailed = 1;
                CheckList = 4;
            }
            dtls.DtList = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
            {
                ReferenceID = ReferenceID,
                TransMode = TransMode,
                Detailed = Detailed,
                CheckList = CheckList
            }, companyKey: _userLoginInfo.CompanyKey).Data;
            if (TransMode == "PRCR")
            {
                dtls.DtProjectList1 = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
                {
                    ReferenceID = ReferenceID,
                    TransMode = TransMode,
                    Detailed = 2,
                    CheckList = 4
                }, companyKey: _userLoginInfo.CompanyKey).Data;
                dtls.DtProjectList2 = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
                {
                    ReferenceID = ReferenceID,
                    TransMode = TransMode,
                    Detailed = 3,
                    CheckList = 4
                }, companyKey: _userLoginInfo.CompanyKey).Data;
                dtls.DtProjectList3 = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
                {
                    ReferenceID = ReferenceID,
                    TransMode = TransMode,
                    Detailed = 4,
                    CheckList = 4
                }, companyKey: _userLoginInfo.CompanyKey).Data;
            }
            if (TransMode == "CUSV")
            {


                DataTable dtbl = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
                {
                    ReferenceID = ReferenceID,
                    TransMode = TransMode,
                    Detailed = 1,
                    CheckList = 4
                }, companyKey: _userLoginInfo.CompanyKey).Data;
                dtls.TimeLineData = dtbl.AsEnumerable().Select(row => new ServiceListModel.TimeLineChartList
                {
                    Description = Convert.ToString(row["Description"]),
                    EntrBy = Convert.ToString(row["EntrBy"]),
                    EntrOn = Convert.ToString(row["EntrOn"]),
                    MoreData = Convert.ToString(row["MoreData"]),
                    SLNo = Convert.ToInt64(row["SLNo"]),
                    Title1 = Convert.ToString(row["Title1"]),
                    Title2 = Convert.ToString(row["Title2"]),

                });
            }
            return PartialView("_LoadCustomerDetailsDatas",dtls);
        }

        public ActionResult LoadEMIHstryDetailsDatas(long ReferenceID, string TransMode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ServiceListModel.ServiceListModelDetailsData dtls = new ServiceListModel.ServiceListModelDetailsData();
            dtls.TransMode = TransMode;
            ServiceListModel mdl = new ServiceListModel();
            int Detailed = 0, CheckList = 0;
            if (TransMode == "ACCT")
            {
                Detailed = 1;
                CheckList = 6;
            }
           
            dtls.DtList = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
            {
                ReferenceID = ReferenceID,
                TransMode = TransMode,
                Detailed = Detailed,
                CheckList = CheckList
            }, companyKey: _userLoginInfo.CompanyKey).Data;

            if (TransMode == "ACCT")
            {
                Detailed = 2;
                CheckList = 6;

            }
            dtls.DtDetailsList = mdl.GetUniversalSearchDetals(input: new ServiceListModel.ServiceListModelDetailsInput
            {
                ReferenceID = ReferenceID,
                TransMode = TransMode,
                Detailed = Detailed,
                CheckList = CheckList
            }, companyKey: _userLoginInfo.CompanyKey).Data;

            return PartialView("_LoadCustomerEMIDetailsDatas", dtls);
        }
    }
}