using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.EmployeeWiseCommitionPaymentModal;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class EmployeeWiseCommissionController : Controller
    {
        
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

        #region[LoadEmployeeWiseCommision]
        public ActionResult LoadEmployeeWiseCommission()
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


            EmployeeWiseCommitionPaymentModal model = new EmployeeWiseCommitionPaymentModal();
            EmployeeWiseCommitionPaymentModal.EmployeeLoadwiseLoad view = new EmployeeWiseCommitionPaymentModal.EmployeeLoadwiseLoad();

            // var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            // {
            //     TableName = "PaymentMethod",
            //     SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
            //     Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
            //     SortFields = "",
            //     GroupByFileds = ""
            // },
            //   companyKey: _userLoginInfo.CompanyKey
            //);
            // inc.PaymentView = PaymentView.Data;


            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
               companyKey: _userLoginInfo.CompanyKey
              );

            view.PaymentView = PaymentView.Data;




            return PartialView("_AddEmployeewiseCommission", view);
        }
        #endregion
        [HttpPost]
        public ActionResult GetEmployeeWiseDataList(int pageSize, int pageIndex, string Name)
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

            EmployeeWiseCommitionPaymentModal model = new EmployeeWiseCommitionPaymentModal();



            // var data = model.CommissionListData(input, companyKey: _userLoginInfo.CompanyKey);
            var data = model.CommissionListData(input: new EmployeeWiseCommitionPaymentModal.CommisionwiseListDatainput
            {

                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                ID_IncentiveTransaction = 0,
                PageIndex = pageIndex + 1,
                Name = Name,
                PageSize = pageSize,
                Detailed = false

            }, companyKey: _userLoginInfo.CompanyKey);

            //IncentiveSettingsModal objfld = new IncentiveSettingsModal();
            //var data = objfld.IncentiveSettingsListData(input: new IncentiveSettingsModal.IncentiveSettingsListDatainputModal
            //{

            //    FK_Company = _userLoginInfo.FK_Company,
            //    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
            //    FK_Machine = _userLoginInfo.FK_Machine,
            //    EntrBy = _userLoginInfo.EntrBy,
            //    FK_ServiceIncentiveSettings = 0,
            //    PageIndex = pageIndex + 1,
            //    Name = Name,
            //    PageSize = pageSize,
            //    Detailed = false

            //}, companyKey: _userLoginInfo.CompanyKey);

           // return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            // return null;
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveEmployeeCommisiondata(SaveEmployeeCommisiondataIP inputdata)
        {
            try
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

            EmployeeWiseCommitionPaymentModal model = new EmployeeWiseCommitionPaymentModal();

          

                var data = model.InsertData(input: new EmployeeWiseCommitionPaymentModal.SaveEmployeeCommisiondataIP2
                {
                    Mode = 1,
                    FK_Employee = inputdata.FK_Employee,
                    paymentdetails = inputdata.paymentdetails is null ? "" : Common.xmlTostring(inputdata.paymentdetails),
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    EntrBy = _userLoginInfo.EntrBy,
                    UptoDate = inputdata.UptoDate,
                    TransDate = inputdata.TransDate,
                    incTrType = 1,
                    //TransType="P",
                    FK_Master = 1,
                    NetAmount = inputdata.NetAmount,
                    Transmode = "ACCP"




                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(data, JsonRequestBehavior.AllowGet);




            } catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
         
        }

        [HttpPost]
        public ActionResult GetPaymentDetails(employeeinput input)
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

            EmployeeWiseCommitionPaymentModal model = new EmployeeWiseCommitionPaymentModal();

           
            var data = model.GetEmployeeWiseData(input, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
            //return null;
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
        public ActionResult DeleateEmployeeWiseData(IncentiveSettingsModal.IncentiveSettingsDeleateinputModal inputdata)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            EmployeeWiseCommitionPaymentModal model = new EmployeeWiseCommitionPaymentModal();

            var data = model.CommissionwisetDatadelete(input: new EmployeeWiseCommitionPaymentModal.EmployeeWisesDeleateinputModal
            {
                TransMode="ACCP",
                FK_Company = _userLoginInfo.FK_Company, 
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason = inputdata.FK_Reason,
                ID_IncentiveTransaction = inputdata.ID_IncentiveTransaction,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
       

    }

}