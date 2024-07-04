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

namespace PerfectWebERP.Controllers
{
    public class VehiclesController : Controller
    {
        // GET: Vehicles
        public ActionResult Index(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod cmnmethd = new CommonMethod();
            string mGrp = cmnmethd.DecryptString(mgrp);
            ViewBag.mtd = mtd;
            ViewBag.TransMode = mGrp;
            return View();
        }

        public ActionResult LoadVehicle(string mtd,string TransMode)
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
            Vehicles.VehicleView vehiclelist= new Vehicles.VehicleView();

            var Makeli = Common.GetDataViaQuery<Vehicles.MakeList>(parameters: new APIParameters
            {
                TableName = "Maker",
                SelectFields = "ID_Maker as FK_Maker, MakName As MakeName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey

                );
            vehiclelist.MakeList = Makeli.Data;       

            var Category = Common.GetDataViaQuery<Vehicles.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS FK_Category ,CatName AS CategoryName",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode='V' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            vehiclelist.CategoryList = Category.Data;

            var Modeli = Common.GetDataViaQuery<Vehicles.ModelList>(parameters: new APIParameters
            {
                TableName = "Model",
                SelectFields = "ID_Model as FK_Model, ModelName As ModelName",
                Criteria = "cancelled=0 AND Passed=1 AND Mode='V' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey

                );
            vehiclelist.ModelList = Modeli.Data;

            var Brandli = Common.GetDataViaQuery<Vehicles.BrandList>(parameters: new APIParameters
            {
                TableName = "Brand",
                SelectFields = "ID_Brand AS FK_Brand ,BrName AS BrandName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            vehiclelist.BrandList = Brandli.Data;

            var Manufli = Common.GetDataViaQuery<Vehicles.ManufacturerList>(parameters: new APIParameters
            {
                TableName = "Manufacturer",
                SelectFields = "ID_Manufacturer as FK_Manufacturer, ManufName As ManufactureName",
                Criteria = "cancelled=0 AND Passed=1 AND Mode='V' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey

                );
            vehiclelist.ManufacturerList = Manufli.Data;
            Vehicles log = new Vehicles();
            var FuelTypeList = log.GeLeadStatusList(input: new Vehicles.ModeLead { Mode = 65 },

            companyKey: _userLoginInfo.CompanyKey);

            vehiclelist.ActionStatusList = FuelTypeList.Data;

            var PaymentViews = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            vehiclelist.PaymentView = PaymentViews.Data;

            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType = 1 AND Mode = 'V' AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company ,  
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            vehiclelist.BillTypeListView = BillTypeListView.Data;


            ViewBag.TransMode = TransMode;
            string Header = "";
            string Display= "";
            string ListName = "";
            var Status=1;
            var mode = TransMode.Substring((TransMode.Length)-2, 2);
            switch (mode)
            {
                case "PU":
                    Header = "Vehicles";
                    Display = "block";
                    Status = 1;
                    ListName = "Vehicle List";
                    break;
                case "OP":
                    Header = "Vehicle Opening";
                    Display = "none";
                    Status = 0;
                    ListName = "Vehicle Opening List";
                    break;                
                default:
                    Header = "Vehicles";
                    Display = "block";
                    Status = 1;
                    ListName = "Vehicle List";
                    break;
            }

            ViewBag.Header = Header;
            ViewBag.Display = Display;
            ViewBag.Status = Status;
            ViewBag.ListName = ListName;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddVehicles", vehiclelist);
        }


        public ActionResult GetOtherCharges(Vehicles.BindOtherCharge Data)
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



        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddVehicle(Vehicles.VehicleView data)
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

            var OtherChgDetails = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            Vehicles brandmodel = new Vehicles();
            var datresponse = brandmodel.UpdateVehicleData(input: new Vehicles.UpdateVehicle
            {
                UserAction = 1,
                Debug = 0,
                TransMode = data.TransMode,
                ID_Vehicle = data.ID_Vehicle,
                VehVehicleNo = data.VehVehicleNo,
                VehChasisNo = data.VehChasisNo,
                VehRegDate = data.VehRegDate,
                FK_Model = data.FK_Model,
                FK_Maker = data.FK_Maker,
                FK_Brand = data.FK_Brand,
                FK_Manufacturer = data.FK_Manufacturer,
                FK_Category = data.FK_Category,
                VehAmount=data.VehAmount,
                VehNetAmount = data.VehNetAmount,
                FK_Company = _userLoginInfo.FK_Company,      
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                OtherChgDetails = OtherChgDetails is null ? "" : Common.xmlTostring(OtherChgDetails),
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Fuel = data.FK_Fuel,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Vehicle = 0,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                FK_BillType=data.FK_BillType,
                IncludeTax=data.IncludeTax,
                FK_Supplier=data.FK_Supplier,
                TransDate=data.TransDate
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateVehicle(Vehicles.VehicleView data)
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
            if (data.ImageList != null) {
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var OtherChgDetails = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);
            Vehicles vehiclemodel = new Vehicles();
            var datresponse = vehiclemodel.UpdateVehicleData(input: new Vehicles.UpdateVehicle
            {
                UserAction = 2,
                Debug = 0,
                TransMode = data.TransMode,
                ID_Vehicle = data.ID_Vehicle,
                VehVehicleNo = data.VehVehicleNo,
                VehChasisNo = data.VehChasisNo,
                VehRegDate = data.VehRegDate,
                FK_Model = data.FK_Model,
                FK_Maker =data.FK_Maker ,
                FK_Brand =data.FK_Brand,
                VehAmount = data.VehAmount,
                VehNetAmount = data.VehNetAmount,
                FK_Manufacturer = data.FK_Manufacturer, 
                FK_Category = data.FK_Category,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,              
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                OtherChgDetails = OtherChgDetails is null ? "" : Common.xmlTostring(OtherChgDetails),
                EntrBy = _userLoginInfo.EntrBy,
                FK_Fuel = data.FK_Fuel,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Vehicle=data.ID_Vehicle,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                FK_BillType = data.FK_BillType,
                IncludeTax = data.IncludeTax,
                FK_Supplier = data.FK_Supplier,
                TransDate = data.TransDate
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetVehicleList(int pageSize, int pageIndex, string Name, string TransModes)
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

            Vehicles vehic = new Vehicles();
            var MakerInfo = vehic.GetVehicleSelect(companyKey: _userLoginInfo.CompanyKey, input: new Vehicles.GetVehicleDetails
            {
                FK_Vehicle = 0,
                TransMode = TransModes,
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

        public ActionResult GetVehicleInfo(Vehicles.VehicleView data)
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

            Vehicles vehic = new Vehicles();
            CommonSearchPopupModel prodimg = new CommonSearchPopupModel();
            Common.fillOtherCharges(data.TransMode, data.ID_Vehicle);
            var modelInfo = vehic.GetVehicleSelectData(companyKey: _userLoginInfo.CompanyKey, input: new Vehicles.GetVehiclebyIdDetails
            {
                FK_Vehicle =data.ID_Vehicle, 
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });
            var OtherCharge = vehic.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new Vehicles.GetSubTableSales
            {
                FK_Transaction = data.ID_Vehicle,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });
            var TaxDetails = vehic.GetTaxDetailsNew(companyKey: _userLoginInfo.CompanyKey, input: new Vehicles.PurchaseNewTax
            {
                ID_Transaction = data.ID_Vehicle,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var paymentdetail = vehic.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new Vehicles.GetPaymentin
            {
                FK_Master = data.ID_Vehicle,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });

            
            var Imageselect = prodimg.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new CommonSearchPopupModel.GetImagein
            {
                FK_Master = data.ID_Vehicle,
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
            return Json(new { modelInfo, Imageselect, OtherCharge,TaxDetails, paymentdetail }, JsonRequestBehavior.AllowGet);
           
        }

        public ActionResult GetVehicleDeleteReasonList()
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

        public ActionResult DeleteVehicle(Vehicles.DeleteView data)
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

            Vehicles vehi = new Vehicles();



            Output datresponse = vehi.DeleteVehicleData(input: new Vehicles.DeleteVehicle
            {
                FK_Vehicle = data.ID_Vehicle,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = data.TransMode,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTaxAmount(Vehicles.BindTaxAmount Data)
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

            Vehicles EquimentModel = new Vehicles();

            var datresponse = EquimentModel.FillTax(input: new Vehicles.BindTaxAmount
            {
                FK_Company = _userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }


    }
}