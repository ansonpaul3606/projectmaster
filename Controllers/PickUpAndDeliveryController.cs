using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class PickUpAndDeliveryController : Controller
    {
        // GET: PickUpAndDelivery
        public ActionResult Index(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;

            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;

            return View();
        }

        public ActionResult LoadDeliveryPickupForm()
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



            PickUpandDelivery.DeliveryPickupassignView Assign = new PickUpandDelivery.DeliveryPickupassignView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

  

 
            PickUpandDelivery objpaymode = new PickUpandDelivery();
            var rolemodeList = objpaymode.GeLeadStatusList(input: new PickUpandDelivery.ModeLead { Mode = 21 }, companyKey: _userLoginInfo.CompanyKey);

            var NetAction = Common.GetDataViaQuery<PickUpandDelivery.Status>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = " ID_NextAction,NxtActnName,FK_ActionStatus",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey
           );
            Assign.StatusList = NetAction.Data;
            Assign.ActionStatusList = rolemodeList.Data;

            var PaymentViews = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            Assign.PaymentView = PaymentViews.Data;

            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType=3 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            Assign.BillTypeListView = BillTypeListView.Data;

            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            
            var EmpName = Common.GetDataViaQuery<LeadGenerateModel.EmployeeInfo>(parameters: new APIParameters
            {


                TableName = "Employee",
                SelectFields = "ID_Employee,EmpFName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""


            },
      companyKey: _userLoginInfo.CompanyKey

         );
            Assign.EmployeeInfoList = EmpName.Data;
            Assign.UserCode = _userLoginInfo.EntrBy;
            return PartialView("_AddDeliveryandPickup", Assign);



        }

        [HttpPost]
        public ActionResult GetDeliiveryAssignResult(PickUpandDelivery.DeliveryInput Data)
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

            PickUpandDelivery Model = new PickUpandDelivery();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::


            long fk_Employee= _userLoginInfo.FK_Employee;

            if (_userLoginInfo.UsrrlManager || _userLoginInfo.UsrrlAdmin)
            {
                fk_Employee = Data.FK_Employee;
            }



            var data = Model.GetDeliveryresult(input: new PickUpandDelivery.DeliveryInput
            {
                FK_Branch = Data.FK_Branch == null ? 0 : Data.FK_Branch,
                FK_Product = Data.FK_Product == null ? 0 : Data.FK_Product,
                EntrBy = _userLoginInfo.EntrBy,
                Priority = Data.Priority == null ? "" :Data.Priority,
                Status = Data.Status,
                FromDate = Data.FromDate == null ? "" : Data.FromDate,
                ToDate = Data.ToDate == null ? "" : Data.ToDate,
                TicketNo = Data.TicketNo == null ? "" : Data.TicketNo,
                CusName = Data.CusName == null ? "" : Data.CusName,
                CusMobile = Data.CusMobile == null ? "" : Data.CusMobile,
                FK_Category = Data.FK_Category == null ? "" : Data.FK_Category,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = Data.PageIndex + 1,
                PageSize = Data.PageSize,
                Mode = Data.Mode,
                Module  = Data.Module == null ? "" :Data.Module,
                FK_Area = Data.FK_Area == null ? 0 : Data.FK_Area,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Employee = Data.FK_Employee == null ? 0 : fk_Employee,
                
            }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::

            return Json(new { data.Process, data.Data, Data.PageSize, Data.PageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


       [HttpPost]
        public ActionResult GetTicketDetailsDP(PickUpandDelivery.Deliverydetails Data)
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

            #endregion ::  Check User Session to verifyLogin  :

            PickUpandDelivery Model = new PickUpandDelivery();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var datas = Model.GetProductdetails(input: new PickUpandDelivery.Deliverydetails
            {

                TransMode = Data.TransMode,
                FK_ProductDelivery = Data.FK_ProductDelivery,

                Mode = Data.Mode,
                Details = 1,

            }, companyKey: _userLoginInfo.CompanyKey);

            var data = Model.GetDeliverydetails(input: new PickUpandDelivery.Deliverydetails
            {

                 TransMode = Data.TransMode,
                 FK_ProductDelivery = Data.FK_ProductDelivery,
        
                 Mode = Data.Mode,
                 Details = 0,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data ,datas}, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetTicketDetailsSTby(PickUpandDelivery.Deliverydetails Data)
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

            #endregion ::  Check User Session to verifyLogin  :

            PickUpandDelivery Model = new PickUpandDelivery();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var datas = Model.GetProductdetails_stby(input: new PickUpandDelivery.Deliverydetails
            {

                TransMode = Data.TransMode,
                FK_ProductDelivery = Data.FK_ProductDelivery,

                Mode = Data.Mode,
                Details = 1,

            }, companyKey: _userLoginInfo.CompanyKey);

            var data = Model.GetDeliverydetails_stby(input: new PickUpandDelivery.Deliverydetails
            {

                TransMode = Data.TransMode,
                FK_ProductDelivery = Data.FK_ProductDelivery,

                Mode = Data.Mode,
                Details = 0,

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, datas }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken()]

        public ActionResult Updatedeliveryandpick(PickUpandDelivery.ProductDeliveryFollowUp data)
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

            #endregion ::  Check User Session to verifyLogin  :

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            bool IsValid = true;
            List<string> _ErrorMessage = new List<string>();
            ModelState.Clear();

            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

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

            PickUpandDelivery Model = new PickUpandDelivery();

            if (IsValid == true)
            {
                var datresponse = Model.UpdateDeliverypickupData(input: new PickUpandDelivery.UpdateProductDeliveryFollowUp
                {
                    UserAction = 1,

                    FK_ProductDeliveryAssign = data.FK_ProductDeliveryAssign,

                    PdfEmployeeNotes = data.Remarks,
                    PdfDeliveryDate = data.PdfDeliveryDate,
                    PdfDeliveryTime = data.PdfDeliveryTime,

                    CSAstatus = data.CSAstatus,
                    ID_NextAction = data.ID_NextAction,

                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

                    EntrBy = _userLoginInfo.EntrBy,

                    FK_Machine = _userLoginInfo.FK_Machine,

                    FK_Employee = data.FK_Employee,
                    Debug = 0,

                    productReplace = data.Productdetails is null ? "" : Common.xmlTostring(data.Productdetails),
                    PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                    StandByAmount = data.StandByTotalAmount,
                    FK_BillType=data.FK_BillType,
                    TransMode=data.TransMode,
                    Pickupstatus=data.Pickupstatus
                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost] 

        public ActionResult GetDeliveryPickFollowupList(int pageSize, int pageIndex, string Name)
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

            PickUpandDelivery Model = new PickUpandDelivery();

            var data = Model.GetDelPickfollowupData(companyKey: _userLoginInfo.CompanyKey, input: new PickUpandDelivery.DelPickfollowupselectID
            {

                FK_ProductDeliveryFollowUp = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Detailed = 0,
                TransMode = "CUSA"

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex }, JsonRequestBehavior.AllowGet);



        }


        #region[GetTicketDetailsSales]
        [HttpPost]
        public ActionResult GetTicketDetailsSales(PickUpandDelivery.Deliverydetails Data)
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

            #endregion ::  Check User Session to verifyLogin  :

            PickUpandDelivery Model = new PickUpandDelivery();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var datas = Model.GetSaleProductdetails(input: new PickUpandDelivery.Deliverydetails
            {

                TransMode = Data.TransMode,
                FK_ProductDelivery = Data.FK_ProductDelivery,

                Mode = Data.Mode,
                Details = 1,

            }, companyKey: _userLoginInfo.CompanyKey);

            var data = Model.GetSaleDelivery(input: new PickUpandDelivery.Deliverydetails
            {
                TransMode = Data.TransMode,
                FK_ProductDelivery = Data.FK_ProductDelivery,
                Mode = Data.Mode,
                Details = 0,

            }, companyKey: _userLoginInfo.CompanyKey);
            var ComplaintList = Model.GetComplaintList(input: new PickUpandDelivery.GetProductComplaintList
            {
                FK_Company = _userLoginInfo.FK_Company,
               
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { data.Process, data.Data, datas, ComplaintList }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[UpdateSalesdelivery]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateSalesdelivery(PickUpandDelivery.SaleDeliveryInput view)
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

            PickUpandDelivery pickmodel = new PickUpandDelivery();
            var datares = pickmodel.UpdateSalesDelivery(new PickUpandDelivery.UpdateSalesdelivery
            {
                FK_ProductDeliveryAssign = view.FK_ProductDeliveryAssign,
                FK_Employee = view.FK_Employee,
                PdfDeliveryDate = view.PdfDeliveryDate,
                PdfDeliveryTime = view.PdfDeliveryTime,
                PdfEmployeeNotes = view.Remarks,
                CSAstatus = view.CSAstatus,
                TransMode = view.TransMode,
                productReplace = view.productOuts is null ? "" : Common.xmlTostring(view.productOuts),
                UserAction = 1,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,


                ID_NextAction = 0,
                PaymentDetail = "",
                StandByAmount = 0,
                FK_BillType = 0,

                PdfCustomerNotes = "",
                PdfDeliveryCharge = 0,
                PdfAction = 0,
                PdfNetAmount = 0,
                DeliveryComplaint = view.DeliveryComplaints is null ? "" : Common.xmlTostring(view.DeliveryComplaints)

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datares }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[UpdateStandbydelivery]
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateStandbydelivery(PickUpandDelivery.StandbyDeliveryInput data)
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

            #endregion ::  Check User Session to verifyLogin  :

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            bool IsValid = true;
            List<string> _ErrorMessage = new List<string>();
            ModelState.Clear();

            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

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

            PickUpandDelivery Model = new PickUpandDelivery();

            if (IsValid == true)
            {
                var datresponse = Model.UpdateDeliverypickupData(input: new PickUpandDelivery.UpdateProductDeliveryFollowUp
                {
                    UserAction = 1,

                    FK_ProductDeliveryAssign = data.FK_ProductDeliveryAssign,

                    PdfEmployeeNotes = data.Remarks,
                    PdfDeliveryDate = data.PdfDeliveryDate,
                    PdfDeliveryTime = data.PdfDeliveryTime,

                    CSAstatus = data.CSAstatus,
                    ID_NextAction = 0,

                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

                    EntrBy = _userLoginInfo.EntrBy,

                    FK_Machine = _userLoginInfo.FK_Machine,

                    FK_Employee = data.FK_Employee,
                    Debug = 0,

                    productReplace = data.Productdetails is null ? "" : Common.xmlTostring(data.Productdetails),
                    PaymentDetail = "",
                    StandByAmount =0,
                    FK_BillType=0,
                    TransMode=data.TransMode
                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}





 


