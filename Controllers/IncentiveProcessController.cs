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
    public class IncentiveProcessController : Controller
    {
        // GET: IncentiveProcess
        public ActionResult Index(string mtd, string mgrp)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

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

        public ActionResult LoadIncentiveProcessForm(string mtd)
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            IncentiveProcessModel.IncentiveProcessViewlist list = new IncentiveProcessModel.IncentiveProcessViewlist();
            var branch = Common.GetDataViaQuery<IncentiveProcessModel.Branchs>(parameters: new APIParameters
            {
                TableName = "Branch ",
                SelectFields = "ID_Branch AS BranchID,BrName AS Branch",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            list.BranchList = branch.Data;
            var deplst = Common.GetDataViaQuery<IncentiveProcessModel.Department>(parameters: new APIParameters
            {

                TableName = "Department",
                SelectFields = "ID_Department as DepId,DeptName as Depname",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
       companyKey: _userLoginInfo.CompanyKey
       );


            list.deprtmnt = deplst.Data;
            var Incentivetypelist = Common.GetDataViaQuery<IncentiveProcessModel.Incentivetype>(parameters: new APIParameters
            {

                TableName = "IncentiveType",
                SelectFields = "ID_IncentiveType as IncentiveTypeID,IncTName as IncentiveType",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
   companyKey: _userLoginInfo.CompanyKey
   );


            list.IncentivetypeList = Incentivetypelist.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;

            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;

            return PartialView("_AddIncentiveProcess",list);
        }

        public ActionResult GetIncentiveprocessDetails(IncentiveProcessModel.Inputvalueforprocessview Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::

            IncentiveProcessModel data = new IncentiveProcessModel();


            var Lits = data.GetInputvalueforIncentiveprocessdetails(input: new IncentiveProcessModel.GetInputvalueforprocess
            {
                FK_Branch = Data.FK_Branch,
                FK_Department = Data.FK_Department,
                IPWCGroupID=0,
                Mode=0,
                Clear=Data.Clear,
                ProcessDate = Data.ProcessDate,
                FromDate=Data.FromDate,
                ToDate = Data.ToDate,
                FK_Employee=Data.FK_Employee,
                FK_IncentiveType=Data.FK_IncentiveType,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine=_userLoginInfo.FK_Machine,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                EditPopupUpdatelist = Data.EditPopupUpdatelist is null ? "" : Common.xmlTostring(Data.EditPopupUpdatelist),
                EntrBy=_userLoginInfo.EntrBy,
                
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Lits }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEditIncentiveprocessDetails(IncentiveProcessModel.Inputvalueforprocessview Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::

            IncentiveProcessModel data = new IncentiveProcessModel();


            var Lits = data.GetInputvalueforIncentiveprocessedit(input: new IncentiveProcessModel.GetInputvalueforprocess
            {
                FK_Branch = Data.FK_Branch,
                FK_Department = Data.FK_Department,
                IPWCGroupID = Data.GroupID,
                Mode = 1,
                Clear = 1,
                ProcessDate = Data.ProcessDate,
                FromDate = Data.FromDate,
                ToDate = Data.ToDate,
                FK_Employee = Data.FK_Employee,
                FK_IncentiveType = Data.FK_IncentiveType,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EditPopupUpdatelist = Data.EditPopupUpdatelist is null ? "" : Common.xmlTostring(Data.EditPopupUpdatelist),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Lits }, JsonRequestBehavior.AllowGet);
        }
        
       

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateEditIncentivePopupDetails(IncentiveProcessModel.Inputvalueforprocess data)
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
            IncentiveProcessModel datalist = new IncentiveProcessModel();
            var datresponse = datalist.UpdateEditpopupData(input: new IncentiveProcessModel.GetInputvalueforprocess
            {

                FK_Branch = data.FK_Branch,
                FK_Department = data.FK_Department,
                IPWCGroupID = data.GroupID,
                Mode = 2,
                Clear = 1,
                ProcessDate = data.ProcessDate,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Employee = data.FK_Employee,
                FK_IncentiveType = data.FK_IncentiveType,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EditPopupUpdatelist = data.EditPopupUpdatelist is null ? "" : Common.xmlTostring(data.EditPopupUpdatelist),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,



            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveIncentiveProcess(IncentiveProcessModel.Inputvalueforprocess data)
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

            IncentiveProcessModel process = new IncentiveProcessModel();

            var datresponse = process.UpdateIncentiveProcessInfo(input: new IncentiveProcessModel.UpdateIncentiveProcessData
            {
                UserAction = 1,
               
                FK_Company = _userLoginInfo.FK_Company,
                FK_Department = (data.FK_Department.HasValue) ? data.FK_Department.Value : 0,
              
                FK_Employee = (data.FK_Employee.HasValue) ? data.FK_Employee.Value : 0,
              
                FK_Branch = (data.FK_Branch.HasValue) ? data.FK_Branch.Value : 0,
               
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Machine = _userLoginInfo.FK_Machine,
                IncentiveProcessDetailsList = data.IncentiveProcessDetailsList is null ? "" : Common.xmlTostring(data.IncentiveProcessDetailsList),
                FK_IncentiveType = (data.FK_IncentiveType.HasValue) ? data.FK_IncentiveType.Value : 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode=data.TransMode,
                ProcessDate = data.ProcessDate,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetIncentiveProcessList(int pageSize, int pageIndex, string Name, string TransMode)
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
            IncentiveProcessModel type = new IncentiveProcessModel();
            var data = type.GetIncentiveProcessListData(input: new IncentiveProcessModel.GetIncentiveProcess
            {

                FK_IncentiveProcess = 0,

                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,
                Mode=0,

            }
            , companyKey: _userLoginInfo.CompanyKey);


            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]

        public ActionResult GetIncentiveProcessInfoID(IncentiveProcessModel.IncentiveProcessViewlist data)

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


            IncentiveProcessModel IntprModel = new IncentiveProcessModel();

            var mptableInfo = IntprModel.GetIncentiveProcessListData(companyKey: _userLoginInfo.CompanyKey, input: new IncentiveProcessModel.GetIncentiveProcess
            {
                FK_IncentiveProcess = data.ID_IncentiveProcess,

                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
               
                Mode = 0,
            });

            var subtable = IntprModel.GetIncentiveProcessListDatas(companyKey: _userLoginInfo.CompanyKey, input: new IncentiveProcessModel.GetIncentiveProcess
            {
                FK_IncentiveProcess = data.ID_IncentiveProcess,

                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

                Mode = 1,
            });

         

            return Json(new { mptableInfo, subtable },JsonRequestBehavior.AllowGet);
        }


  
        public ActionResult GetEditIncentiveprocessviewDetails(IncentiveProcessModel.IncentiveProcessViewlist data)

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


            IncentiveProcessModel IntprModel = new IncentiveProcessModel();

         

            var popviewtable = IntprModel.GetIncentiveProcesspopListDatas(companyKey: _userLoginInfo.CompanyKey, input: new IncentiveProcessModel.GetIncentiveProcess
            {
                FK_IncentiveProcess = data.ID_IncentiveProcess,

                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

                Mode = 2,
            });



            return Json(popviewtable, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteIncentiveProcessInfo(IncentiveProcessModel.IncentiveProcessInfoView data)
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

            IncentiveProcessModel type = new IncentiveProcessModel();
            var datresponse = type.DeleteIncentiveProcessData(input: new IncentiveProcessModel.DeleteIncentiveProcess {
                FK_IncentiveProcess = data.IncentiveProcessID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetIncentiveprocessDeleteReasonList()
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