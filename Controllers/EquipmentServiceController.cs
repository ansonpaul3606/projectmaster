using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class EquipmentServiceController : Controller
    {
        // GET: EquipmentService
        public ActionResult Index(string mtd,string mgrp)
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
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            var headName = ViewBag.TransMode.Substring(0, 2);

            string loghead = "";

            if (headName == "VL")
            {
                loghead = "Vehicle";
            }
            else if (headName == "TO")
            {
                loghead = "Tool";
            }
            ViewBag.headlog = loghead;
            return View();
        }

        public ActionResult LoadEquipmentService(string mtd,string TransMode)
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
          
            EquipmentServiceModel.EquipmentServiceView type = new EquipmentServiceModel.EquipmentServiceView();
            var mode = TransMode.Substring(0, 2);
            var modeData = "";
            string lblpro = "";
            string loghead = "";
            string lbllist = "";

            switch (mode)
            {
                case "IN":
                    modeData = "'P'";
                    break;
                case "VL":
                    modeData = "'V'";
                    lblpro = "Vehicle";
                    loghead = "Vehicle Service";
                    lbllist = "Vehicle Service List";


                    break;
                case "TO":
                    modeData = "'T'";
                    lblpro = "Tool";
                    loghead = "Tool Service";
                    lbllist = "Tool Service List";

                    break;
                case "AT":
                    modeData = "'A'";
                    break;
                default:
                    modeData = "'P'";
                    break;
            }
            ViewBag.lblpro = lblpro;
            ViewBag.headlog = loghead;
            ViewBag.tmode = mode;
            ViewBag.lbllist = lbllist;

            var Maintenancetype = Common.GetDataViaQuery<EquipmentServiceModel.MaintenancetypeList>(parameters: new APIParameters

            {
                TableName = "Maintenance",
                SelectFields = "ID_Maintenance AS FK_Master,MaintenanceName as Maintenancetype",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode=" + modeData + " AND FK_Company =" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

               companyKey: _userLoginInfo.CompanyKey
                   );
            type.MaintenancetypeList = Maintenancetype.Data;

            var Incidencetype = Common.GetDataViaQuery<EquipmentServiceModel.IncidencetypeList>(parameters: new APIParameters
            {
                TableName = "Incidence",
                SelectFields = "ID_Incidence AS FK_Master,IncidenceName AS Incidencetype",
                Criteria = "cancelled=0 AND Passed=1 AND Mode=" + modeData + " And FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                  companyKey: _userLoginInfo.CompanyKey

       );
            type.IncidencetypeList = Incidencetype.Data;
            ViewBag.TransMode = TransMode;
            var PaymentViews = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name, PMDefault AS PMDefault",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);
            type.PaymentView = PaymentViews.Data;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddEquipmentService", type);
        }
        [HttpPost]

        public ActionResult GetALType(int FK_Mode, string TransMode)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            EquipmentServiceModel.EquipmentServiceView list = new EquipmentServiceModel.EquipmentServiceView();
            var mode = TransMode.Substring(0, 2);
            var modeData = "";            

            switch (mode)
            {
                case "IN":
                    modeData = "'P'";
                    break;
                case "VL":
                    modeData = "'V'";

                    break;
                case "TO":
                    modeData = "'T'";
                   
                    break;
                case "AT":
                    modeData = "'A'";
                    break;
                default:
                    modeData = "'P'";
                    break;
            }
            if (FK_Mode == 1)
            {
                var Maintenancetype = Common.GetDataViaQuery<EquipmentServiceModel.ModeList>(parameters: new APIParameters

                {
                    TableName = "Maintenance",
                    SelectFields = "ID_Maintenance AS FK_Transaction,MaintenanceName as ModeName",
                    Criteria = "Cancelled=0 AND Passed=1 AND Mode=" + modeData +" AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "",
                    GroupByFileds = ""
                },

             companyKey: _userLoginInfo.CompanyKey
                 );
                list.ModeList = Maintenancetype.Data;
            }
          if(FK_Mode == 2)
            {
                var Incidencetype = Common.GetDataViaQuery<EquipmentServiceModel.ModeList>(parameters: new APIParameters
                {
                    TableName = "Incidence",
                    SelectFields = "ID_Incidence AS FK_Transaction,IncidenceName AS ModeName",
                    Criteria = "cancelled=0 AND Passed=1 AND Mode=" + modeData + " AND  FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "",
                    GroupByFileds = ""
                },
               companyKey: _userLoginInfo.CompanyKey

    );
                list.ModeList = Incidencetype.Data;
            }

            return Json(list.ModeList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewEquipmentService(EquipmentServiceModel.EquipmentServiceView data)
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

            ModelState.Remove("EquipmentServiceDetails");


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

            if (data.ImageList != null)
            {
                foreach (CommonSearchPopupModel.ImageListView itm in data.ImageList)
                {
                    if (itm.ProdImage != null && itm.ProdImage != "")
                    {
                        var img = itm.ProdImage.Split(';')[1].Replace("base64,", "");

                        itm.ProdImage = img;
                    }
                    else
                    {
                        itm.ProdImage = "";

                    }
                }
            }
            EquipmentServiceModel objprdwise = new EquipmentServiceModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;

            

            var dataresponse = objprdwise.UpdateEquipmentServiceData(input: new EquipmentServiceModel.EquipmentServiceUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,
                ID_EquipmentService = 0,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                TransDate = data.TransDate,
                FromTime = data.FromTime,
                ToTime = data.ToTime,
                FK_Mode = data.FK_Mode,
                ServiceCentre = data.ServiceCentre,

                FK_Transaction = data.FK_Transaction,
                FK_Master = (data.FK_Master.HasValue) ? data.FK_Master.Value : 0,
                FK_EquipmentService = data.FK_EquipmentService,
                SubTotalAmount=data.SubTotalAmount,
                NetAmount=data.NetAmount,
                FK_ServiceBooking = (data.FK_ServiceBooking.HasValue) ? data.FK_ServiceBooking.Value : 0,
                FK_EquipmentServiceType = (data.FK_EquipmentServiceType.HasValue) ? data.FK_EquipmentServiceType.Value : 0,

                OtherCharge = (data.OtherCharge.HasValue) ? data.OtherCharge.Value : 0,
                SerPickDel = data.SerPickDel,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                //ID_Names = data.ID_Names,

                //WarrentyService = data.WarrentyService==true?1: data.AMCService==true?2:0,
                //BookingNo = data.BookingNo,
                //VehicleNo = data.VehicleNo,

                AMCService = (data.AMCService.HasValue) ? data.AMCService.Value : 0,
                WarrentyService = (data.WarrentyService.HasValue) ? data.WarrentyService.Value : 0,
                //AMCService = (data.AMCService.HasValue) ? data.AMCService:0,
                //WarrentyService =0,
                DiscountAmount = (data.DiscountAmount.HasValue) ? data.DiscountAmount.Value : 0,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                OtherChgDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),

                TaxAmountDetails = data.TaxAmountDetails is null ? "" : Common.xmlTostring(data.TaxAmountDetails),
                TaxAmount = (data.TaxAmount.HasValue) ? data.TaxAmount.Value : 0,
                IncludeTax = data.IncludeTax,
                EquipmentServiceDetails = data.EquipmentServiceDetails is null ? "" : Common.xmlTostring(data.EquipmentServiceDetails),


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEquipmentService(EquipmentServiceModel.EquipmentServiceView data)
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

            ModelState.Remove("EquipmentServiceDetails");


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

            var imagenull = false;
            if (data.ImageList != null)
            {
                foreach (CommonSearchPopupModel.ImageListView itm in data.ImageList)
                {
                    if (itm != null)
                    {
                        if (itm.ProdImage != null && itm.ProdImage != "")
                        {
                            var img = itm.ProdImage.Split(';')[1].Replace("base64,", "");

                            itm.ProdImage = img;
                        }
                        else
                        {
                            itm.ProdImage = "";

                        }
                    }
                    else
                    {
                        imagenull = true;
                    }
                }
            }

            if (imagenull)
            {
                data.ImageList = null;
            }
            EquipmentServiceModel objprdwise = new EquipmentServiceModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateEquipmentServiceData(input: new EquipmentServiceModel.EquipmentServiceUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,
                ID_EquipmentService = data.ID_EquipmentService,
                FromDate = data.FromDate,
                TransDate = data.TransDate,
                ToDate = data.ToDate,
                FromTime = data.FromTime,
                ToTime = data.ToTime,
                FK_Mode = data.FK_Mode,
                FK_Transaction = data.FK_Transaction,
                FK_Master = (data.FK_Master.HasValue) ? data.FK_Master.Value : 0,
                FK_EquipmentService = data.FK_EquipmentService,
                SubTotalAmount = data.SubTotalAmount,
                NetAmount = data.NetAmount,
                SerPickDel = data.SerPickDel,

                FK_ServiceBooking = (data.FK_ServiceBooking.HasValue) ? data.FK_ServiceBooking.Value : 0,
                AMCService = (data.AMCService.HasValue) ? data.AMCService.Value : 0,
                WarrentyService = (data.WarrentyService.HasValue) ? data.WarrentyService.Value : 0,
                OtherCharge = (data.OtherCharge.HasValue) ? data.OtherCharge.Value : 0,
                FK_EquipmentServiceType = (data.FK_EquipmentServiceType.HasValue) ? data.FK_EquipmentServiceType.Value : 0,


                //ID_Names = data.ID_Names,

                //WarrentyService = data.WarrentyService,
                //BookingNo = data.BookingNo,
                //VehicleNo = data.VehicleNo,

                //WarrentyService = data.WarrentyService == true ? 1 : data.AMCService == true ? 2 : 0,

                //AMCService = (data.AMCService.HasValue) ? (data.AMCService.Value == 1) ? 1 : 0 : 0,
                //WarrentyService = (data.AMCService.HasValue) ? (data.AMCService.Value == 2) ? 2 : 0 : 0,
                //AMCService = (data.AMCService.HasValue) ? data.AMCService : 0,
                //WarrentyService = 0,
                DiscountAmount = (data.DiscountAmount.HasValue) ? data.DiscountAmount.Value : 0,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                OtherChgDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),

                TaxAmountDetails = data.TaxAmountDetails is null ? "" : Common.xmlTostring(data.TaxAmountDetails),
                TaxAmount = (data.TaxAmount.HasValue) ? data.TaxAmount.Value : 0,
                IncludeTax = data.IncludeTax,
                EquipmentServiceDetails = data.EquipmentServiceDetails is null ? "" : Common.xmlTostring(data.EquipmentServiceDetails),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEquipmentServiceList(int pageSize, int pageIndex, string Name, string TransMode)
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


            EquipmentServiceModel objPrd = new EquipmentServiceModel();
            var data = objPrd.GetEquipmentServiceData(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentServiceModel.EquipmentServiceID
            {

                FK_EquipmentService = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Detailed = 0,
                TransMode = TransMode

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetEquipmentServiceInfo(EquipmentServiceModel.EquipmentServiceID data)
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
            EquipmentServiceModel objprd = new EquipmentServiceModel();
            CommonSearchPopupModel prodimg = new CommonSearchPopupModel();
            var mptableInfo = objprd.GetEquipmentServiceDatainfoid(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentServiceModel.EquipmentServiceID
            {
                FK_EquipmentService = data.FK_EquipmentService,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                TransMode = data.TransMode,
                EntrBy = _userLoginInfo.EntrBy
            });

            var subtable = objprd.GetEquipmentServiceSubtabledata(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentServiceModel.EquipmentServiceDetailsSubSelect
            {
                FK_EquipmentService = data.FK_EquipmentService,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].EquipmentServiceDetails = subtable.Data;
            }

            var OtherCharge = objprd.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentServiceModel.GetSubTableSales
            {
                FK_Transaction = data.FK_EquipmentService,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });
            var paymentdetail = objprd.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentServiceModel.GetPaymentin
            {
                FK_Master = data.FK_EquipmentService,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
            var Taxselect = objprd.GetTaxDetails(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentServiceModel.TaxAmount
            { ID_Transaction = data.FK_EquipmentService,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });
            var Imageselect = prodimg.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new CommonSearchPopupModel.GetImagein
            {
                FK_Master = data.FK_EquipmentService,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode

            });
            if (Imageselect.Data != null)
            {
                foreach (CommonSearchPopupModel.ImageListView itm in Imageselect.Data)
                {
                    if (itm.ProdImage != "" && itm.ProdImage != null)
                    {
                        itm.ProdImage = "data:image/;base64," + itm.ProdImage;
                    }
                }
            }
            //return Json(mptableInfo,paymentdetail JsonRequestBehavior.AllowGet);
            return Json(new { mptableInfo, paymentdetail, OtherCharge, Taxselect, Imageselect }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEquipmentServiceDeleteReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID
            {
                FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""
            });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

       


        [HttpPost]
        [ValidateAntiForgeryToken()]

        public ActionResult DeleteEquipmentService(EquipmentServiceModel.EquipmentServiceRsnView data)
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

            EquipmentServiceModel.EquipmentServiceDelete objprddel = new EquipmentServiceModel.EquipmentServiceDelete
            {
                FK_EquipmentService = data.ID_EquipmentService,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<EquipmentServiceModel.EquipmentServiceDelete>(
                companyKey: _userLoginInfo.CompanyKey, procedureName: "ProEquipmentServiceDelete", parameter: objprddel);
            //test data
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTaxAmount(EquipmentServiceModel.BindTaxAmount Data)
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

            EquipmentServiceModel EquimentModel = new EquipmentServiceModel();

            var datresponse = EquimentModel.FillTax(input: new EquipmentServiceModel.BindTaxAmount
            {
               FK_Company=_userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOtherCharges(EquipmentServiceModel.BindOtherCharge Data)
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

            PurchaseModel purchaseModel = new PurchaseModel();

            var OtherCharges = purchaseModel.FillOtherCharges(input: new PurchaseModel.BindOtherCharge
            {
                TransMode = Data.TransMode,

            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType=1",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }


    }
}