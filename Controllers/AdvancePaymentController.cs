using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class AdvancePaymentController : Controller
    {
        // GET: AdvancePayment
        public ActionResult Index(string mtd)
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.mtd = mtd;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);
            return View();
        }
        public ActionResult LoadAdvancePayment(string mtd)
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

            AdvancePaymentModel.AdvancePaymentView list = new AdvancePaymentModel.AdvancePaymentView();

            var OpDepartmentListto = Common.GetDataViaQuery<AdvancePaymentModel.DepartmentTo>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS FK_Department,DeptName AS DepartmentNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                      companyKey: _userLoginInfo.CompanyKey

           );
            list.DepartmentListTo = OpDepartmentListto.Data;

            var OpBranchListto = Common.GetDataViaQuery<AdvancePaymentModel.BranchTo>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS FK_Branch,BrName AS BranchNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                    companyKey: _userLoginInfo.CompanyKey

         );
            list.BranchListTo = OpBranchListto.Data;

            var Allowancetype = Common.GetDataViaQuery<AdvancePaymentModel.AllowanceTypeList>(parameters: new APIParameters
            {
                TableName = "AllowanceType",
                SelectFields = "ID_AllowanceType AS FK_AllowanceType,ALWName AS Allowancetype",
                Criteria = "cancelled=0 AND Passed=1  AND ALWMode=2 AND ALWType=11 AND FK_Company="+ _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                  companyKey: _userLoginInfo.CompanyKey

       );
            list.AllowanceTypeList = Allowancetype.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddAdvancePayment", list);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewAdvancePayment(AdvancePaymentModel.AdvancePaymentView data)
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


            AdvancePaymentModel objprdwise = new AdvancePaymentModel();


            byte userAction = 1;//update : 2 | Add : 1 

            //int branchCode = _userLoginInfo.FK_Branch;
            //int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateAdvancePaymentData(input: new AdvancePaymentModel.AdvancePaymentUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_SalaryAdvancePayment = 0,
                FK_EMployee = data.FK_EMployee,
                TransDate = data.TransDate,
                EffectDate = data.EffectDate,
                FK_AllowanceType = data.FK_AllowanceType,
                SalMonthlyRecAmount = data.SalMonthlyRecAmount,
                SalAdvAmount = data.SalAdvAmount,
                FK_SalaryAdvancePayment = data.FK_SalaryAdvancePayment,
                //FK_Branch = data.FK_Branch,
                //FK_Department = data.FK_Department


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAdvancePaymentList(int pageSize, int pageIndex, string Name)
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



            //string transMode = "";
            AdvancePaymentModel AdvancePayment = new AdvancePaymentModel();

            var data = AdvancePayment.GetAdvancePaymentData(companyKey: _userLoginInfo.CompanyKey, input: new AdvancePaymentModel.AdvancePayment
            {
                FK_SalaryAdvancePayment = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //TransMode = transMode
            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAdvancePaymentInfo(AdvancePaymentModel.AdvancePayment data)
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

            AdvancePaymentModel AdvancePayment = new AdvancePaymentModel();
            var advancepaymentInfo = AdvancePayment.GetAdvancePaymentData(companyKey: _userLoginInfo.CompanyKey, input: new AdvancePaymentModel.AdvancePayment { FK_SalaryAdvancePayment = data.FK_SalaryAdvancePayment, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, EntrBy = _userLoginInfo.EntrBy });

            return Json(advancepaymentInfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAdvancePaymentReasonList()
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
        public ActionResult DeleteAdvancePaymentInfo(AdvancePaymentModel.AdvancePaymentView data)

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

            ModelState.Remove("FK_EMployee");
            //ModelState.Remove("PftTypeShortName");


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


            AdvancePaymentModel.DeleteAdvancePayment AdvancePayment = new AdvancePaymentModel.DeleteAdvancePayment
            {
                FK_SalaryAdvancePayment = data.ID_SalaryAdvancePayment,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""

            };

            Output dataresponse = Common.UpdateTableData<AdvancePaymentModel.DeleteAdvancePayment>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProSalaryAdvancePaymentDelete", parameter: AdvancePayment);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAdvancePayment(AdvancePaymentModel.AdvancePaymentView data)
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

            ModelState.Remove("ID_SalaryAdvancePayment");


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


            AdvancePaymentModel objprdwise = new AdvancePaymentModel();


            byte userAction = 2;//update : 2 | Add : 1 

            //int branchCode = _userLoginInfo.FK_Branch;
            //int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;

            var dataresponse = objprdwise.UpdateAdvancePaymentData(input: new AdvancePaymentModel.AdvancePaymentUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_SalaryAdvancePayment = data.ID_SalaryAdvancePayment,
                FK_EMployee = data.FK_EMployee,
                TransDate = data.TransDate,
                EffectDate = data.EffectDate,
                FK_AllowanceType = data.FK_AllowanceType,
                SalMonthlyRecAmount = data.SalMonthlyRecAmount,
                SalAdvAmount = data.SalAdvAmount,
                FK_SalaryAdvancePayment = data.FK_SalaryAdvancePayment,




            }, companyKey: _userLoginInfo.CompanyKey);

            

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


    }
}