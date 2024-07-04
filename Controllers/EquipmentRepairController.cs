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
using PerfectWebERP.Filters;
using static PerfectWebERP.Models.CommonSearchPopupModel;

namespace PerfectWebERP.Controllers
{
    public class EquipmentRepairController : Controller
    {
        // GET: EquipmentRepair
        public ActionResult Index(string mgrp)
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

        public ActionResult EquipmentRepairView(string TransMode)
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
            EquipmentRepair.RepairListModel RepairList = new EquipmentRepair.RepairListModel();
            var mode = TransMode.Substring(0, 2);
            var modeData = "";
            string labelpopup = "";
            string lblpro = "";
            string loghead = "";
            string desc = " ";
            switch (mode)
            {
                case "IN":
                    modeData = "'P'";
                    break;
                case "VL":
                    modeData = "'V'";
                    labelpopup = "Driver";
                    lblpro = "Vehicle";
                    loghead = "Vehicle Rent And Return";
                    desc = "Route";

                    break;
                case "TO":
                    modeData = "'T'";
                    labelpopup = "Operator";
                    lblpro = "Tool";
                    loghead = "Tool Rent And Return";
                    desc = "Description";
                    break;
                case "AT":
                    modeData = "'A'";
                    break;
                default:
                    modeData = "'P'";
                    break;
            }
            ViewBag.lblemp = labelpopup;
            ViewBag.lblpro = lblpro;
            ViewBag.headlog = loghead;
            ViewBag.lbldesc = desc;
            ViewBag.tmode = mode;
            EquipmentRepair repair = new EquipmentRepair();
            var TypeList = repair.GeLeadStatusList(input: new EquipmentRepair.ModeLead { Mode = 68 },

            companyKey: _userLoginInfo.CompanyKey);

            RepairList.ActionStatusList = TypeList.Data;



            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name, PMDefault AS PMDefault",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
       );
            RepairList.PaymentView = PaymentView.Data;

            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType=4 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            RepairList.BillTypeListView = BillTypeListView.Data;

            return PartialView("_AddEquipmentRepair", RepairList);
        }

         

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewVehicleAndToolRepair(EquipmentRepair.RepairListModel data)
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            EquipmentRepair repair = new EquipmentRepair();

            var datresponse = repair.UpdateVehicleAndToolrepairData(input: new EquipmentRepair.UpdateVehicleAndToolRepair
            {
                UserAction = 1,
                Debug = 0,
                ID_EquipmentRentalDetails = 0,
                EquRentalDate = data.EquRentalDate,
                EquDescription = data.EquDescription,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                EquRentType = data.EquRentType,
                TransMode = data.TransMode,
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                TaxAmountDetails = data.TaxAmountDetails is null ? "" : Common.xmlTostring(data.TaxAmountDetails),
                EquEquipmentDistance = data.EquEquipmentDistance,
                EquEquipmentNo = data.EquEquipmentNo,
                EquSecurityAmount = data.EquSecurityAmount,
                FK_MasterID = data.FK_MasterID,
                FK_EquipmentRentalDetails = data.FK_EquipmentRentalDetails,
                EquTaxAmount = data.EquTaxAmount,
                EquNetAmount = data.EquNetAmount,
                EquRentAmount = data.EquRentAmount,
                EquPayAmount = data.EquPayAmount,
                IncludeTax=data.IncludeTax,
                FK_BillType = data.FK_BillType,
                EquOperator=data.EquOperator,
                EquOperatorExpDate=data.EquOperatorExpDate,
                EquOperatorMobile=data.EquOperatorMobile,
                EquOperatorNo=data.EquOperatorNo,
                EquReturndays = data.EquReturndays,
                OtherChDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateNewVehicleAndToolRepair(EquipmentRepair.RepairListModel data)
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
            var imagenull = false;
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
            if (imagenull)
            {
                data.ImageList = null;
            }
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            EquipmentRepair repair = new EquipmentRepair();

            var datresponse = repair.UpdateVehicleAndToolrepairData(input: new EquipmentRepair.UpdateVehicleAndToolRepair
            {
                UserAction =2,
                Debug = 0,
                ID_EquipmentRentalDetails = data.ID_EquipmentRentalDetails,
                EquRentalDate = data.EquRentalDate,
                EquDescription = data.EquDescription,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                EquRentType = data.EquRentType,
                TransMode = data.TransMode,
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                TaxAmountDetails = data.TaxAmountDetails is null ? "" : Common.xmlTostring(data.TaxAmountDetails),
                EquEquipmentDistance = data.EquEquipmentDistance,
                EquEquipmentNo = data.EquEquipmentNo,
                EquSecurityAmount = data.EquSecurityAmount,
                FK_MasterID = data.FK_MasterID,
                FK_EquipmentRentalDetails = data.FK_EquipmentRentalDetails,
                EquTaxAmount = data.EquTaxAmount,
                EquNetAmount = data.EquNetAmount,
                EquRentAmount = data.EquRentAmount,
                EquPayAmount = data.EquPayAmount,
                IncludeTax = data.IncludeTax,
                FK_BillType = data.FK_BillType,
                EquOperator = data.EquOperator,
                EquOperatorExpDate = data.EquOperatorExpDate,
                EquOperatorMobile = data.EquOperatorMobile,
                EquReturndays = data.EquReturndays,
                EquOperatorNo=data.EquOperatorNo,
                OtherChDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,


            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRentList(int pageSize, int pageIndex, string Name, string TransMode)
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

            EquipmentRepair vehic = new EquipmentRepair();
            var MakerInfo = vehic.GetRentSelect(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentRepair.GetRentDetails
            {
                FK_EquipmentRentalDetails = 0,
                TransMode = TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,

            });

            return Json(new { MakerInfo.Process, MakerInfo.Data, pageSize, pageIndex, totalrecord = (MakerInfo.Data is null) ? 0 : MakerInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRepairInfo(EquipmentRepair.RepairListModel RepairInfo, string TransModes)
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

            EquipmentRepair vehic = new EquipmentRepair();

            var modelInfo = vehic.GetRentSelectData(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentRepair.GetRentbyIdDetails
            {
                FK_EquipmentRentalDetails = RepairInfo.ID_EquipmentRentalDetails,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = TransModes
            });
            var Taxselect = vehic.GetTaxDetails(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentRepair.TaxAmount {
                ID_Transaction = RepairInfo.ID_EquipmentRentalDetails,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = TransModes
            });


            var paymentdetail = vehic.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentRepair.GetPaymentin
            {
                FK_Master = RepairInfo.ID_EquipmentRentalDetails,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = TransModes,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
            var OtherCharge = vehic.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new EquipmentRepair.GetSubTableSales
            {
                FK_Transaction = RepairInfo.ID_EquipmentRentalDetails,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = TransModes
            });



            CommonSearchPopupModel prodimg = new CommonSearchPopupModel();
            var Imageselect = prodimg.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new CommonSearchPopupModel.GetImagein
            {
                FK_Master = RepairInfo.ID_EquipmentRentalDetails,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = TransModes

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
            return Json(new { modelInfo, Imageselect, Taxselect, paymentdetail , OtherCharge }, JsonRequestBehavior.AllowGet);

        }  

           
                          
           


       
        public ActionResult GetRentDeleteReasonList()
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

        public ActionResult DeleteRent(EquipmentRepair.DeleteView data, string TransModes)
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

            EquipmentRepair vehi = new EquipmentRepair();



            Output datresponse = vehi.DeleteRentData(input: new EquipmentRepair.DeleteRent
            {
                FK_EquipmentRentalDetails = data.ID_EquipmentRentalDetails,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = TransModes,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetTaxAmount(EquipmentRepair.BindTaxAmount Data, string TransMode)
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

            EquipmentRepair RepairModel = new EquipmentRepair();

            var datresponse = RepairModel.FillTax(input: new EquipmentRepair.BindTaxAmount
            {
                FK_Company = _userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }



    }
}