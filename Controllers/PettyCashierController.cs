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
    public class PettyCashierController : Controller
    {
        // GET: PettyCashier

               public ActionResult Index(string mtd)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }
        public ActionResult LoadPettyCashierForm(string mtd)
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            PettyCashierModel.PettyCashierListModel taxtypeList = new PettyCashierModel.PettyCashierListModel();          
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "PettyCashier",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            taxtypeList.SortOrder = a.Data[0].NextNo;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddPettyCashierView", taxtypeList);
        }



        #region  PettyCashierList
        [HttpPost]
        public ActionResult GetPettyCashierList(int pageSize, int pageIndex, string Name, string TransMode)
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
            PettyCashierModel pcash = new PettyCashierModel();
            var data = pcash.GetPettyCashierData(input: new PettyCashierModel.GetPettyCashierinfo
            {

                FK_PettyCashier = 0,

                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,

            }
            , companyKey: _userLoginInfo.CompanyKey);


            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region GetPettyCashierList by id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetPettyCashierInfoByID(PettyCashierModel.GetPettyCashierinfo data)
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

            PettyCashierModel cashp = new PettyCashierModel();
            var cashpInfo = cashp.GetPettyCashierData(companyKey: _userLoginInfo.CompanyKey, input: new PettyCashierModel.GetPettyCashierinfo { FK_PettyCashier = data.FK_PettyCashier, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

            return Json(cashpInfo, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Add PettyCashier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPettyCashier(PettyCashierModel.PettyCashierModelViewModel data)
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
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
            ModelState.Remove("Active");
            ModelState.Remove("SortOrder");
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

            PettyCashierModel petty = new PettyCashierModel();



            var dataresponse = petty.UpdatePettyCashierData(input: new PettyCashierModel.UpdatePettyCashier
            {
                UserAction = 1,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,


                FK_PettyCashier = 0,
                ID_PettyCashier = 0,
                PtyCshrName = data.Names,
                PtyCshrShortName = data.ShortName,
                PtyCshrActive = data.Active,
             
                FK_AccountHead = data.AccountHead,
                FK_AccountHeadSub = data.AccountHeadSub,
                SortOrder = data.SortOrder,
            }, companyKey: _userLoginInfo.CompanyKey);
            Output output = new Output();

            if (dataresponse.Data.FirstOrDefault().Column1 > 0)
            {

                output.IsProcess = true;
                output.Message = new List<string> { "Saved Successfully" };

            }
            else
            {
                output.Message = new List<string> { dataresponse.Data.FirstOrDefault().ErrMsg };
                output.code = dataresponse.Data.FirstOrDefault().ErrCode;
                output.IsProcess = false;
            }

            return Json(new { Process = output }, JsonRequestBehavior.AllowGet);
        }


        #endregion
        #region update PettyCashier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAddPettyCashier(PettyCashierModel.PettyCashierModelViewModel data)
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
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
            ModelState.Remove("Active");
            ModelState.Remove("SortOrder");
            

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

            PettyCashierModel petty = new PettyCashierModel();



            var dataresponse = petty.UpdatePettyCashierData(input: new PettyCashierModel.UpdatePettyCashier
            {
                UserAction = 2,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

              
                ID_PettyCashier = data.PettyCashierID,
                PtyCshrName = data.Names,
                PtyCshrShortName = data.ShortName,
                PtyCshrActive = data.Active,

                FK_AccountHead = data.AccountHead,
                FK_AccountHeadSub = data.AccountHeadSub,
                SortOrder = data.SortOrder,
             

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region delete PettyCashierList
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeletePettyCashierInfo(PettyCashierModel.PettyCashierModelViewModel data)
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

            PettyCashierModel taxtype = new PettyCashierModel();
            var datresponse = taxtype.DeletePettyCashierData(input: new PettyCashierModel.DeletePettyCashier { FK_PettyCashier = data.PettyCashierID, EntrBy = _userLoginInfo.EntrBy, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_Reason = data.ReasonID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, TransMode = "" }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult GetPettyCashierDeleteReasonList()
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

        public ActionResult GetAccountHeadDetails()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<TaxTypeModel.AccountHeadView>(parameters: new APIParameters
            {
                TableName = "AccountHead AH",
                SelectFields = "AH.ID_AccountHead AS AccountHead,AH.AHCode AS AccountCode,AH.AHName AS AHeadName",
                Criteria = "AH.Cancelled =0 AND AH.Passed=1",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAccountSubHeadDetails(int AccountCode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<TaxTypeModel.AccountSubHeadView>(parameters: new APIParameters
            {
                TableName = "AccountSubHead ASH",
                SelectFields = "ASH.ID_AccountSubHead AS AccountHeadSub,ASH.AHCode AS AccountSHCode,ASH.ASHName AS ASHeadName",
                Criteria = "ASH.Cancelled =0 AND ASH.Passed=1 AND ASH.AHCode=" + AccountCode,
                SortFields = "",
                GroupByFileds = ""
            },

           
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }
    }
}