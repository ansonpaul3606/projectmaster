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
    public class DepreciationProcessController : Controller
    {
        // GET: DepreciationProcess
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;

            ViewBag.Username = _userLoginInfo.UserName;

            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            return View();
        }
        public ActionResult LoadFormDepreciationProcess()
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



            DepreciationProcessModel.DepreciationProcessView view = new DepreciationProcessModel.DepreciationProcessView();

            DepreciationProcessModel prdListObj = new DepreciationProcessModel();

            var ModeTypeList = prdListObj.GetModeTypList(input: new DepreciationProcessModel.Modes { Mode = 67 },
           companyKey: _userLoginInfo.CompanyKey);

            view.ModeList = ModeTypeList.Data;
            return PartialView("_AddDepreciationProcess", view);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDepreciationProcess(DepreciationProcessModel.DepreciationProcessNew data)
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

            //ModelState.Remove("AttendanceMarkingDetailsView");


            #region :: Model validation  ::

            //removing a node in model while validating
            //--- Model validation 
         

            #endregion :: Model validation  ::


            DepreciationProcessModel objprdwise = new DepreciationProcessModel();


          
            var dataresponse = objprdwise.UpdateDepreciationProcessData(input: new DepreciationProcessModel.DepreciationProcessUpdateData
            {

                UserAction = 1,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
               
                Debug = 0,
                ID_DepreciationProcess = 0,
                DepreProcessDate = data.DepreProcessDate,
                DepreFromDate = data.DepreFromDate,
                DepreToDate = data.DepreToDate,
                Mode = data.Mode,

                FK_ItemCategory = data.FK_Category,
                FK_ItemMaster =  data.FK_Product,
                FK_ItemStock = data.FK_ItemStock,
                FK_DepreciationSettings = data.FK_DepreciationSettings,
                ItemQty = data.ItemQty,
                DeprePeriod = data.DeprePeriod,
                DepreAmt = data.DepreAmt,
                DepreMaxAmt = data.DepreMaxAmt,
                FK_Branch = data.FK_Branch,
                FK_Reason = data.FK_Reason,
                XMLDepreciationProcessDetail = data.DepreciationProcessDetails is null ? "" : Common.xmlTostring(data.DepreciationProcessDetails)
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetCategorylist(string Id_Mode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<DepreciationProcessModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category ",
                SelectFields = "ID_Category FK_Category, CatName Category",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_Company='" + _userLoginInfo.FK_Company + "'" + " AND Mode='" + Id_Mode + "'",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDepreciationProcessedInfo(DepreciationProcessModel.DepreciationProcessNew data)
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
            DepreciationProcessModel objprd = new DepreciationProcessModel();

            var mptableInfo = objprd.GetDepreciationDatainfoid(companyKey: _userLoginInfo.CompanyKey, input: new DepreciationProcessModel.DepreciationProcessID
            {

                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_DepreciationProcess=data.ID_DepreciationProcess,
                Detail = 0,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = 0,
                PageSize = 0,
            });

            var subtable = objprd.GetDepreciationDatainfodetails(companyKey: _userLoginInfo.CompanyKey, input: new DepreciationProcessModel.DepreciationProcessID
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_DepreciationProcess = data.ID_DepreciationProcess,
                Detail = 1,
                EntrBy = _userLoginInfo.EntrBy
            });



            return Json(new { mptableInfo, subtable }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult DepreciationProcessDetails(DepreciationProcessModel.XMLDepreciationProcessDetail Data)
        //{
        //    #region ::  Check User Session to verifyLogin  ::

        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion ::  Check User Session to verifyLogin  ::



        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    #region :: Fill List ::


        //    #endregion :: Fill List ::
        //    ServiceListModel.ServiceListView AssignModel = new ServiceListModel.ServiceListView();
        //    DepreciationProcessModel data = new DepreciationProcessModel();


        //    var Lits = data.GetDepreciationProcessDetails(input: new DepreciationProcessModel.GetDepreciationdetails { FK_Branch = Data.FK_Branch, ProcessDate = Data.ProcessDate, FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);


        //    return Json(new { Lits }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetDepreciationInfo(DepreciationProcessModel.GetDepreciationData Data)
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

            #endregion


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            DepreciationProcessModel data = new DepreciationProcessModel();


            var Lits = data.GetDepreciationDetailsData(companyKey: _userLoginInfo.CompanyKey, input: new DepreciationProcessModel.GetDepreciationData
            {
                ProcessDate = Data.ProcessDate,
                FK_Product = Data.FK_Product,
                Fromdate = Data.Fromdate,
                Todate = Data.Todate,
                Mode = Data.Mode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Category = Data.FK_Category,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Branch = _userLoginInfo.FK_Branch,
                EntrBy = _userLoginInfo.EntrBy,
                Detail = 1


            });


            //});

            return Json(new { Lits }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public ActionResult GetDepreciationProcessList(int pageSize, int pageIndex, string Name)
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

            DepreciationProcessModel objPrd = new DepreciationProcessModel();
            var data = objPrd.GetDepreciationProcessSelect(companyKey: _userLoginInfo.CompanyKey, input: new DepreciationProcessModel.DepreciationProcessID
            {

                FK_DepreciationProcess = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Detail = 0,
                TransMode = transMode





            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


       
        public ActionResult DeleteDepreciationProcess(DepreciationProcessModel.DepreciationProcessNew data)
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
            if (!ModelState.IsValid)
            {
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

            DepreciationProcessModel DelAccTrans = new DepreciationProcessModel();

            var datresponse = DelAccTrans.DeleteDepreciationProcess(input: new DepreciationProcessModel.DepreciationProcessDeleteData
            {
                UserAction = 2,
                ID_DepreciationProcess = data.ID_DepreciationProcess,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_DepreciationProcess = data.ID_DepreciationProcess,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.FK_Reason,
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetDepreciationProcessDeleteReasonList()
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
