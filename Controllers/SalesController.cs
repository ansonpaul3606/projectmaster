/*----------------------------------------------------------------------
Created By	: Aiswarya
Created On	: 15/02/2022
Purpose		: Sales
-------------------------------------------------------------------------
Modification
On			By					OMID/Remarks
-------------------------------------------------------------------------
-------------------------------------------------------------------------*/

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
using System.IO;
using RestSharp;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class SalesController : Controller
    {
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.state = _userLoginInfo.FK_States;

            //var StandbyChk = Common.GetDataViaQuery<SaleModel.GeneralSettingsModel>(parameters: new APIParameters
            //{
            //    TableName = "GeneralSettings",
            //    SelectFields = "GsModule as GsModule,GsValue AS GsValue,GsField AS GsField",
            //    Criteria = "GsField='ESBYSTKM'AND FK_Company=" + _userLoginInfo.FK_Company,
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey);

            //var sesionStockCheck = "";
            //Session["StandbyChk.Data[0].GsValue"] = sesionStockCheck;
            //ViewBag.ChekStandBy = StandbyChk.Data[0].GsValue;

            var StandbyChk = Common.GetDataViaQuery<ProductModel.MultipleUnitSettings>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT005'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );
            ViewBag.ChekStandBy = StandbyChk.Data[0].GsValue;

            var MOPSales = Common.GetDataViaQuery<SaleModel.BuyBack>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT008'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            ViewBag.MOPSales = MOPSales.Data[0].GsValue;          

            Common.ClearOtherCharges(ViewBag.TransMode);
            return View();
        }
        [HttpPost]
        public ActionResult AddFlie()
        {


            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {

                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);
                    var _imgname = Guid.NewGuid().ToString();//will create new file name
                    //var _comPath = Server.MapPath("/blogImage/") + _imgname + _ext;
                    var _comPath = Server.MapPath("/MyImages/") + fileName;
                    _imgname = _imgname + _ext;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                }
            }
            return Json("Uploaded Successfully");
        }

        //[HttpPost]
        //public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        //{

        //    foreach (var file in files)
        //    {
        //        string filePath = Path.GetFileNameWithoutExtension(file.FileName) + Path.GetExtension(file.FileName);
        //        file.SaveAs(Path.Combine(Server.MapPath("~/Image"), filePath));

        //        //Area for TABLE data insert
        //        DataTable tbl = new DataTable();
        //        tbl.Name = filePath;
        //        tbl.Extension = Path.GetExtension(file.FileName);
        //        context.PhotoUploads.Add(tbl);
        //        context.SaveChanges();
        //    }
        //    return Json("Uploaded Successfully");
        //}
        public ActionResult Test()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult UploadFiles()
        //{
        //    // Checking no of files injected in Request object  
        //    if (Request.Files.Count > 0)
        //    {
        //        try
        //        {
        //            //  Get all files from Request object  
        //            HttpFileCollectionBase files = Request.Files;
        //            for (int i = 0; i < files.Count; i++)
        //            {
        //               // string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
        //               // string filename = path.GetFileName(Request.Files[i].FileName);  

        //                HttpPostedFileBase file = files[i];
        //                string fname;

        //                // Checking for Internet Explorer  
        //                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
        //                {
        //                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
        //                    fname = testfiles[testfiles.Length - 1];
        //                }
        //                else
        //                {
        //                    fname = file.FileName;
        //                }

        //                // Get the complete folder path and store the file inside it.  
        //              // fname = path.Combine(Server.MapPath("~/Uploads/"), fname);
        //               // file.SaveAs(fname);
        //            }
        //            // Returns message that successfully uploaded  
        //            return Json("File Uploaded Successfully!");
        //        }
        //        catch (Exception ex)
        //        {
        //            return Json("Error occurred. Error details: " + ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return Json("No files selected.");
        //    }
        //}
        [HttpPost]
        public ActionResult UploadFiles()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/MyImages"));
                        string pathString = System.IO.Path.Combine(path.ToString());


                        var fileName1 = Path.GetFileName(file.FileName);



                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                        var uploadpath = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(uploadpath);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new
                {
                    Message = fName
                });
            }
            else
            {
                return Json(new
                {
                    Message = "Error in saving file"
                });
            }
        }
        [HttpPost]
        public ActionResult Upload(List<HttpPostedFileBase> fileData)
        {
            string path = Server.MapPath("~/Uploads/");
            foreach (HttpPostedFileBase postedFile in fileData)
            {
                if (postedFile != null)
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(path + fileName);
                }
            }

            return Content("Success");
        }
        public ActionResult LoadFormSales(string mtd, string TransMode)
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
            SaleModel.DropDownListModel Sal = new SaleModel.DropDownListModel();
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType=2 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + " ORDER BY SortOrder asc",
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey
        );
            Sal.BillTypeListView = BillTypeListView.Data;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name,PMDefault AS PMDefault,PMMode",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" +  _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
       );
            Sal.PaymentView = PaymentView.Data;

            var warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            Sal.Warrantytype = warrantytype.Data;


            var amctype = Common.GetDataViaQuery<AMCTypeModel.AMCTypeView>(parameters: new APIParameters
            {
                TableName = "AMCType",
                SelectFields = "ID_AMCType as AMCTypeID,AMCName as AMCName",
                Criteria = "Cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            Sal.AMCtype = amctype.Data;

            var DepartmentList = Common.GetDataViaQuery<SaleModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentID,DeptName AS Department",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            Sal.DepartmentList = DepartmentList.Data;
            // var UnitList = Common.GetDataViaQuery<SaleModel.ProductUnitDetails>(parameters: new APIParameters
            // {
            //    TableName = "MultipleUnits Mu JOIN Unit U On Mu.FK_Unit=U.ID_Unit",
            //    SelectFields = "FK_Unit,U.UnitName",
            //     Criteria = "Mu.Passed=1 And Mu.Cancelled=0   AND Mu.FK_Company=" + _userLoginInfo.FK_Company,
            //    SortFields = "",
            //     GroupByFileds = ""
            // },
            //companyKey: _userLoginInfo.CompanyKey);
            //Sal.ProductUnitList = UnitList.Data;
            SaleModel ProductUnitData = new SaleModel();
            var UnitList = ProductUnitData.GetProductUnitData(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetProductUnitDtls
            {
                FK_Product = 0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

            });
            
            Sal.ProductUnitList = UnitList.Data;
            
            var BankList = Common.GetDataViaQuery<SaleModel.BankList>(parameters: new APIParameters
            {
                TableName = "Bank",
                SelectFields = "ID_Bank AS BankID,CONCAT(BankName,'/',BranchName) AS BankName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Branch=" + _userLoginInfo.FK_Branch,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            Sal.DepartmentList = DepartmentList.Data;
            Sal.BankList = BankList.Data;
            var ChangeMod = Common.GetDataViaProcedure<SaleModel.TransporttypeMode, SaleModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new SaleModel.ChangeModeInput { Mode = 53 });

            Sal.TransporttypeModeList = ChangeMod.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
           
            Common.ClearOtherCharges(TransMode);
            var custransmode = Common.GetDataViaQuery<SaleModel>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "TransMode as CusTransMode",
                Criteria = "ControllerName='Customer'AND cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
       companyKey: _userLoginInfo.CompanyKey);

            //var StandbyChk = Common.GetDataViaQuery<SaleModel.GeneralSettingsModel>(parameters: new APIParameters
            //{
            //    TableName = "GeneralSettings",
            //    SelectFields = "GsModule as GsModule,GsValue AS GsValue,GsField AS GsField",
            //    Criteria = "GsField='ESBYSTKM'AND FK_Company=" + _userLoginInfo.FK_Company,
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey);

            //var sesionStockCheck = "";
            //Session["StandbyChk.Data[0].GsValue"] = sesionStockCheck;
            //ViewBag.ChekStandBy = StandbyChk.Data[0].GsValue;

            string cus = custransmode.ToString();
            ViewBag.CusTransmode = objCmnMethod.EncryptString(cus);
            ViewBag.UserMrpEdit = _userLoginInfo.UserMrpEdit;
            ViewBag.UserPriceEdit = _userLoginInfo.UserPriceEdit;
            //var stockStby = 0;
            //ViewBag.stockStby
            var BToB = Common.GetDataViaQuery<SaleModel.BusinessToBusinessCustomer>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "COUNT(*) AS BtoB",
                Criteria = "BrGSTINNo !='' AND Cancelled = 0 AND Passed = 1 AND FK_Company = " + _userLoginInfo.FK_Company + "AND ID_Branch = " + _userLoginInfo.FK_Branch,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            // var tax = purchase.GettaxData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchase { ID_Product = purInfo.ProductID, FK_Company = _userLogin"nfo.FK_Company, UserCode = _userLoginInfo.EntrBy });
            Sal.BtoB = BToB.Data[0].BtoB;
            ViewBag.BtoB = BToB.Data[0].BtoB;
            ViewBag.FK_State = _userLoginInfo.FK_States;

            var MultiUnit = Common.GetDataViaQuery<SaleModel.BuyBack>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT003'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            ViewBag.MultiUnit = MultiUnit.Data[0].GsValue;

            return PartialView("_AddSales", Sal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetSalesInfo(SaleModel.SalesViewels saleInfo)
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
            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("ReasonID");
            #region :: Model validation  ::

            //--- Model validation 
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::
            SaleModel Sales = new SaleModel();
            Common.fillOtherCharges(saleInfo.TransMode, saleInfo.SalesID);
            //var SaleInfo = Sales.GetSalesData(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSales { FK_Sales = saleInfo.SalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });
            var SaleDetails = Sales.GetSalesDetails(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSalesdetail { ID_Sales = saleInfo.SalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode });
            var Imageselect = Sales.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetImagein { FK_Master = saleInfo.SalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode });
            var OtherCharge = Sales.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSubTableSales { FK_Transaction = saleInfo.SalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode });
            var paymentdetail = Sales.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetPaymentin { FK_Master = saleInfo.SalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser });
            var buybackdetail = Sales.GetBuyBackSelect(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetBuyBack { FK_Master = saleInfo.SalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser });
            var warrantyselect = Sales.GetWarrantySelect(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetImagein { FK_Master = saleInfo.SalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode });
            var SubProductInfo = Sales.GetSubProductSelect(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.SelectSubProduct
            {
                ID_Sales = saleInfo.SalesID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = saleInfo.TransMode,
                Mode = 0,
            });
            var SerialNumberInfo = Sales.GetSerialNumberSelect(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.SelectSubProduct
            {
                ID_Sales = saleInfo.SalesID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = saleInfo.TransMode,
                Mode = 1,
            });
            if (Imageselect.Data != null)
            {
                foreach (SaleModel.ImageListView itm in Imageselect.Data)
                {
                    if (itm.ProdImage != "" && itm.ProdImage != null)
                    {
                        itm.ProdImage = "data:image/;base64," + itm.ProdImage;
                    }
                }
            }
            var BToB = Common.GetDataViaQuery<SaleModel.BusinessToBusinessCustomer>(parameters: new APIParameters
            {
                TableName = "Customer",
                SelectFields = "COUNT(*) AS BtoB",
                Criteria = "CusGSTINNo !='' AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND ID_Customer="+saleInfo.FK_Customer,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            // var tax = purchase.GettaxData(companyKey: _userLoginInfo.CompanyKey, input: new PurchaseModel.GetPurchase { ID_Product = purInfo.ProductID, FK_Company = _userLogin"nfo.FK_Company, UserCode = _userLoginInfo.EntrBy });
            bool Btob= BToB.Data[0].BtoB;
            return Json(new { SaleDetails, Imageselect, OtherCharge, paymentdetail, warrantyselect, SubProductInfo, SerialNumberInfo, Btob , buybackdetail }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductwisePrice(SaleModel.GetProductWisePrice Data)
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

            SaleModel salesModel = new SaleModel();

            var datresponse = salesModel.GetProductwisePrice(input: new SaleModel.GetProductWisePrice
            {
                FK_Customer = Data.FK_Customer,
                FK_Product = Data.FK_Product,
                FK_Stock = Data.FK_Stock,
                Branch =1,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTaxAmountNew(PurchaseModel.BindTaxAmountNew Data)
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

            var datresponse = purchaseModel.FillTaxNew(input: new PurchaseModel.BindTaxAmountNew
            {
                Amount = Data.Amount,
                Includetax = 0/*Data.Includetax*/,
                FK_Product = Data.FK_Product,
                Sales = 1,
                Quantity = Data.Quantity,
                TaxtyInterstate = Data.TaxtyInterstate

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLeadFill(SaleModel.Leadfilldetails Data)
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

            SaleModel purchaseModel = new SaleModel();

            var datresponse = purchaseModel.Fillead(input: new SaleModel.Leadfill
            {
                FK_Master = Data.FK_Master,
                IsLead = Data.IsLead,


            }, companyKey: _userLoginInfo.CompanyKey);

            if (Data.Transmode != null)
            {
                Common.fillOtherCharges(Data.Transmode, Data.FK_Master);
            }
            
            var OtherCharge = purchaseModel.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSubTableSales
            {
                FK_Transaction = Data.FK_Master,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "INSO"
            });

            return Json(new { datresponse, OtherCharge }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetSalesList(int pageSize, int pageIndex, string Name, int AllSales, string TransModes)
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
            SaleModel Sales = new SaleModel();
            var data = Sales.GetSalesData(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSales
            {
                TransMode = TransModes,
                FK_Sales = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                AllSales = AllSales,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                EntrBy = _userLoginInfo.EntrBy
            });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewSales(SaleModel.SalesView data)
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

            if (data.ImageList != null)
            {
                foreach (SaleModel.ImageListView itm in data.ImageList)
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
            SaleModel Sales = new SaleModel();
            string warimage = (string)Session["WarProductImage"];
            string TransModeVal="INSL";
            if(data.ImportID == 2)      
            {
                TransModeVal = "INSO";
            }
            else if(data.ImportID == 6)
            {
                TransModeVal = "INQU";
            }
            else
            {
                TransModeVal = "INSL";
            }

            var otherCharge = Common.GetOtherCharges(TransModeVal);
            var otherChargeTax = Common.GetOtherChargeTax(TransModeVal);

            var datresponse = Sales.UpdateSalesData(input: new SaleModel.UpdateSales
            {

                UserAction = 1,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                ID_Sales = 0,
                ID_Hold = data.ID_Hold,
                TransMode = data.TransMode,
                FK_BillType = data.BillType,
                SalBillDate = data.SalBillDate,
                SalEnterDate = data.SalEnterDate,
                FK_Customer = data.ID_Customer,
                FK_LeadGenerate = data.FK_Lead,
                FK_CustomerOthersL = data.FK_CustomerOthers,
                FK_CustomerL = data.FK_Customer,
                MobileNo = data.MobileNo,
                SalBillTotal = data.SalBillTotal,
                SalOthercharges = data.OtherCharge,
                SalDiscount = data.SalDiscount,
                SalRoundoff = data.SalRoundoff,
                SalNetAmount = data.SalNetAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BrachCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Department = _userLoginInfo.FK_Department,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                CustomeName = data.CustomeName,
                Hold = data.Hold,
                AdvAmount = data.AdvAmount,
                FK_SalesOrder = data.FK_SalesOrder,
                StockadjonHold = data.StockadjonHold,
                OtherChgDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                ProductDetail = data.ProductDetail is null ? "" : Common.xmlTostring(data.ProductDetail),
                buyback=data.buyback is null? "": Common.xmlTostring(data.buyback),

                DownPayment = data.DownPayment,
                AdditionalAmount = data.AdditionalAmount,
                StartDate = data.StartDate,
                FK_FinancePlanType = data.FK_FinancePlanType,

                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_Country = (data.CountryID.HasValue) ? data.CountryID.Value : 0,
                FK_State = (data.StatesID.HasValue) ? data.StatesID.Value : 0,
                FK_District = (data.DistrictID.HasValue) ? data.DistrictID.Value : 0,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                DelAddress1 = data.Address1,
                DelAddress2 = data.Address2,
                DelName = data.ShpContactName,
                DelMobileNo = data.ShpMobile,

                DelTransportType = (data.Transporttype.HasValue) ? data.Transporttype.Value : 0,
                DelVehicleNo = data.Vehicleno,
                DelDriverName = data.DrvName,
                DelDriverMobileNo = data.DrvPhoneno,
                FK_Employee = data.FK_Employee,
                ProductSerialNumbers = data.ProductSerialNumbers is null ? "" : Common.xmlTostring(data.ProductSerialNumbers),
                SubproductDetails = data.SubproductDetails is null ? "" : Common.xmlTostring(data.SubproductDetails),
                WarrantyImgDetails = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                FK_Bank = (data.BankID.HasValue) ? data.BankID.Value : 0,
                ChekStandBy = data.ChekStandBy,
                ImportID=data.ImportID,
                FK_Quotation = data.FK_Quotation,
                FK_Vehicle=data.FK_Vehicle

            }, companyKey: _userLoginInfo.CompanyKey);

            if (datresponse.IsProcess)
            {
                Session["WarProductImage"] = "";
                sendMail objMail = new sendMail();
                objMail.sendMailData(Convert.ToInt32(datresponse.code), "SALES", _userLoginInfo.FK_Company, 1, 3, _userLoginInfo.CompanyKey, "");
                Common.ClearOtherCharges(data.TransMode);
            }

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateSales(SaleModel.SalesView data)
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

            SaleModel Sales = new SaleModel();
            var datresponse = Sales.UpdateSalesData(input: new SaleModel.UpdateSales
            {
                UserAction = 2
    ,
                //Mode = data.Mode,
                //SalBillNo = data.SalBillNo,
                //SalReferenceNo = data.SalReferenceNo,
                //SalBillDate = data.SalBillDate,
                //SalEffectDate = data.SalEffectDate,
                //SalBillTotal = data.SalBillTotal,
                //SalDiscount = data.SalDiscount,
                //SalOthercharges = data.SalOthercharges,
                //SalRoundoff = data.SalRoundoff,
                //SalNetAmount = data.SalNetAmount,
                //TaxtyIntrastate = data.TaxtyIntrastate,
                //FK_BillType = data.BillType,
                //FK_Customer = data.Customer,
                //FK_LeadGenerate = data.LeadGenerate,
                //FK_SalesOrder = data.SalesOrder,
                //FK_Branch = data.Branch,
                //FK_Company = _userLoginInfo.FK_Company,
                //FK_Department = data.Department,
                //FK_BrachCodeUser = data.BrachCodeUser,
                //EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteSales(SaleModel.SalesView data)
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

            SaleModel Sales = new SaleModel();


            var datresponse = Sales.DeleteSalesData(input: new SaleModel.DeleteSales { FK_SalesHold = data.ID_Hold, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Reason = data.ReasonID, FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSalesReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetSalesOrderInfo(SaleModel.SalesOrderID objSoID)
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
            SaleModel objSalesOrder = new SaleModel();
            var EmiData = objSalesOrder.GetEMIInstallmentDetailsSelects(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetEMIDetailsSelect
            {
                TransMode = "INSO",
                FK_Master = objSoID.FK_Master,
                Mode = 1,
            });
            return Json(EmiData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCustomerdetails(SaleModel.GetCustomerDetails Data)
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

            SaleModel salModel = new SaleModel();

            var datresponse = salModel.GetCustomerdetailsfill(input: new SaleModel.GetCustomerDetails
            {
                FK_Master = Data.FK_Master,
                ImportId = Data.ImportId,


            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindCustomerdetails(SaleModel.Bindshippingdetails Data)
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

            SaleModel salModel = new SaleModel();

            var datresponse = salModel.Getshippingdetailsfill(input: new SaleModel.Bindshippingdetails
            {
                FK_Master = Data.FK_Master,
                TransMode = Data.TransMode,
                //Mode = Data.Mode

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOtherCharges(SaleModel.BindOtherCharge Data)
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
                FK_Company=_userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType=2",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAMCWarrantydetails(SaleModel.AmcWarrantyDetailsInput Data)
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

            SaleModel salModel = new SaleModel();

            var datresponse = salModel.GetAmcWarrantyfill(input: new SaleModel.GetAmcWarrantyDetails
            {
                Mode = Data.Mode,
                FK_Type = Data.FK_Type,
                Date = Data.Date,
                Quantity = Data.Quantity,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getGetCustomerAccountBalance(int FK_Customer, DateTime TransDate)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SaleModel saleModel = new SaleModel();

            var data = saleModel.GetCustomerAccountBalance(input: new SaleModel.AccountbalFill
            {
                FK_Customer = FK_Customer,
                TransDate = TransDate
            },

            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateSalEwaybill(SaleModel.SalesView data)
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

            ModelState.Remove("SalesID");


            SaleModel Sales = new SaleModel();
            string warimage = (string)Session["WarProductImage"];

            string TransModeVal = "INSL";
           // Common.fillOtherCharges(TransModeVal, data.SalesID);
            var otherCharge = Common.GetOtherCharges(TransModeVal);
            var otherChargeTax = Common.GetOtherChargeTax(TransModeVal);
            var datresponse = Sales.UpdateSalEwaybill(input: new SaleModel.SalesEwaybillupdate
            {

                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Sales = data.SalesID,
                SalEWayBillNo = data.EWaybillNo,
                Discount = data.Discount,
                ItemWise = data.ItemWise,
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                OtherChgDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                 OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                SalesBillDetails = data.SalebillDetails is null ? "" : Common.xmlTostring(data.SalebillDetails),
                EntrBy = _userLoginInfo.EntrBy
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexScrap(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            ViewBag.FK_Department = _userLoginInfo.FK_Department;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;

            return View();
        }
        public ActionResult LoadSalesScrap()
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
            SaleModel.DropDownListModel Sal = new SaleModel.DropDownListModel();
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;

            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType=2 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey
        );
            Sal.BillTypeListView = BillTypeListView.Data;

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
       );
            Sal.PaymentView = PaymentView.Data;

            var warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            Sal.Warrantytype = warrantytype.Data;


            var amctype = Common.GetDataViaQuery<AMCTypeModel.AMCTypeView>(parameters: new APIParameters
            {
                TableName = "AMCType",
                SelectFields = "ID_AMCType as AMCTypeID,AMCName as AMCName",
                Criteria = "Cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            Sal.AMCtype = amctype.Data;

            var departmentList = Common.GetDataViaQuery<SaleModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Department Dp",
                SelectFields = "Dp.ID_Department AS DepartmentID,Dp.DeptName AS[Department]",
                Criteria = "Dp.Cancelled=0 AND Dp.Passed=1 AND Dp.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey

         );
            Sal.DepartmentList = departmentList.Data;
            var ChangeMod = Common.GetDataViaProcedure<SaleModel.TransporttypeMode, SaleModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new SaleModel.ChangeModeInput { Mode = 53 });

            Sal.TransporttypeModeList = ChangeMod.Data;
            return PartialView("_AddSalesScrap", Sal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewScrapSales(SaleModel.ScrapSaleView data)
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

            ModelState.Remove("SalesID");


            SaleModel Sales = new SaleModel();
            string warimage = (string)Session["WarProductImage"];


            var datresponse = Sales.UpdateScrapSalesData(input: new SaleModel.UpdateScrapSales
            {

                UserAction = 1,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                ID_ScrapSales = 0,

                TransMode = data.TransMode,

                FK_BillType = data.BillType,
                SalScarpBillDate = data.SalScrapBillDate,
                SalScarpEnterDate = data.SalScrapEnterDate,
                FK_Buyer = data.FK_Buyer,
                SalScarpBillNo = data.SalScrapBillNo,
                SalScarpBillTotal = data.SalScrapBillTotal,
                SalScarpOthercharges = data.OtherCharge,
                SalScarpTaxamount = data.TaxAmount,
                IncludeTax = data.IncludeTax,
                Auction = data.Auction,
                Auctiondetails = data.AuctionRemarks,
                SalScarpNetAmount = data.SalScrapNetAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BrachCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = data.DepartmentID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

                OtherChgDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                TaxAmountDetails = data.TaxAmountDetails is null ? "" : Common.xmlTostring(data.TaxAmountDetails),

                ScrapProductDetails = data.ScrapProductDetails is null ? "" : Common.xmlTostring(data.ScrapProductDetails)





            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateScrapSales(SaleModel.ScrapSaleView data)
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

            ModelState.Remove("SalesID");


            SaleModel Sales = new SaleModel();
            string warimage = (string)Session["WarProductImage"];


            var datresponse = Sales.UpdateScrapSalesData(input: new SaleModel.UpdateScrapSales
            {

                UserAction = 2,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                ID_ScrapSales = 0,

                TransMode = data.TransMode,
                FK_BillType = data.BillType,
                SalScarpBillDate = data.SalScrapBillDate,
                SalScarpEnterDate = data.SalScrapEnterDate,
                FK_Buyer = data.FK_Buyer,
                SalScarpBillNo = data.SalScrapBillNo,
                SalScarpBillTotal = data.SalScrapBillTotal,
                SalScarpOthercharges = data.OtherCharge,
                SalScarpTaxamount = data.TaxAmount,
                IncludeTax = data.IncludeTax,
                Auction = data.Auction,
                Auctiondetails = data.AuctionRemarks,
                SalScarpNetAmount = data.SalScrapNetAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BrachCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = data.DepartmentID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

                OtherChgDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                TaxAmountDetails = data.TaxAmountDetails is null ? "" : Common.xmlTostring(data.TaxAmountDetails),

                ScrapProductDetails = data.ScrapProductDetails is null ? "" : Common.xmlTostring(data.ScrapProductDetails)





            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTaxAmount(SaleModel.BindTaxAmount Data)
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

            SaleModel SalesModel = new SaleModel();

            var datresponse = SalesModel.FillTax(input: new SaleModel.BindTaxAmount
            {
                FK_Company = _userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteSalesScrap(SaleModel.ScrapSaleView data)
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

            SaleModel Sales = new SaleModel();


            var datresponse = Sales.DeleteSalesScrapData(input: new SaleModel.DeleteSalesscrap
            {
                FK_ScrapSales = data.ScrapSalesID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = data.TransMode,
                FK_Company = _userLoginInfo.FK_Company
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSalesscrapReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetSalesScarpList(int pageSize, int pageIndex, string Name, string TransModes)
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

            SaleModel Sales = new SaleModel();
            var SaleInfo = Sales.GetScrapSalesData(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetScrapSales
            {
                FK_ScrapSales = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = TransModes,
            });

            return Json(new { SaleInfo.Process, SaleInfo.Data, pageSize, pageIndex, totalrecord = (SaleInfo.Data is null) ? 0 : SaleInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetScrapSalesInfo(SaleModel.ScrapSaleView saleInfo)
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


            ModelState.Remove("ReasonID");

            #region :: Model validation  ::



            #endregion :: Model validation  ::


            SaleModel Sales = new SaleModel();


            var SaleDetails = Sales.GetScrapSalesData(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetScrapSales { FK_ScrapSales = saleInfo.ScrapSalesID, Detailed = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode });
            var productDetails = Sales.GetScarpSalesDetails(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetScrapSales { FK_ScrapSales = saleInfo.ScrapSalesID, Detailed = 1, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode });
            var OtherCharge = Sales.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSubTableSales { FK_Transaction = saleInfo.ScrapSalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode });
            var paymentdetail = Sales.GetPaymentScrapselect(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetPaymentin { FK_Master = saleInfo.ScrapSalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser });
            var Taxselect = Sales.GetTaxDetails(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.TaxAmount { ID_Transaction = saleInfo.ScrapSalesID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, TransMode = saleInfo.TransMode });
            return Json(new { SaleDetails, OtherCharge, paymentdetail, productDetails, Taxselect }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetSubProductsDetailInfo(SaleModel.InputProduct saleInfo)
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

            SaleModel Sales = new SaleModel();

            var subproduct = Sales.GetSubProducts(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSubProduct
            {
                ID_Sales = saleInfo.ID_Sales,
                TransMode = saleInfo.TransMode,
                FK_Product = saleInfo.FK_Product,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Department = _userLoginInfo.FK_Department,
                StockID = saleInfo.StockID,
                Mode = saleInfo.Mode,
                Quantity = saleInfo.Quantity,
            });
            return Json(new { subproduct }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClearOtherCharges(string Transmode)
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

            SaleModel purchaseModel = new SaleModel();

            Common.ClearOtherCharges(Transmode);
            

            return Json(new {  },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetWarrantyInfo(SaleModel.GetWarrantyDtls data)
        {
            SaleModel warrData = new SaleModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var warrantyselect = warrData.GetWarrantyDtlsData(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetWarrantyDtls
            {
                FK_Product = data.FK_Product,
                TransDate = data.TransDate,
                stockId = data.stockId,
            });

            return Json(new { warrantyselect }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GenerateEinvoice(SaleModel.SalesViewels saleInfo)
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

            SaleModel Sales = new SaleModel();

            var Einvoice = Sales.GenerateEinvoice(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetEinvoiceData
            {
                FK_Sales = saleInfo.SalesID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
            });
            string einvoiceJson = Einvoice.Data[0].JsonData; // Serialize Einvoice to JSON string            
            
            EinvoiceGenerateModel.GenerateEinvoiceData einvoiceObject = JsonConvert.DeserializeObject<EinvoiceGenerateModel.GenerateEinvoiceData>(einvoiceJson);

            // get user name and password for FK_company 

            var Einvoicesetting = Common.GetDataViaQuery<SaleModel.EInvoiceSettingsModel>(parameters: new APIParameters
            {
                TableName = "EInvoiceGeneralSettings",
                SelectFields = "Username as username,Password AS password,URL AS Url",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);

            //http client
            string EinvoiceGenerateBaseURL = $"{Einvoicesetting.Data.FirstOrDefault().Url}api/Invoice/";
            RestClient client_Invoice = new RestClient(EinvoiceGenerateBaseURL);
            RestRequest request_Invoice = new RestRequest("GenerateToken", Method.POST);

            request_Invoice.AddJsonBody(new { username = Einvoicesetting.Data.FirstOrDefault().Username, password = Einvoicesetting.Data.FirstOrDefault().Password });
            IRestResponse response_Invoice = client_Invoice.Execute(request_Invoice);
            //generate token
            //call gen irn

            if (response_Invoice.IsSuccessful)
            {
                var data = JsonConvert.DeserializeObject<SaleModel.APIResponse_TockenInfo>(response_Invoice.Content);

                if (data.Status)
                {
                    //success
                    //data eInvoice_Encryt   
                    RestClient gen_invoice_client = new RestClient(EinvoiceGenerateBaseURL);
                    RestRequest gen_invoice_request = new RestRequest("GenerateIRN", Method.POST);
                    gen_invoice_request.AddHeader("Authorization", $"Bearer {data.Token}");
                    gen_invoice_request.AddJsonBody(einvoiceJson);

                    IRestResponse gen_invoice_response = gen_invoice_client.Execute(gen_invoice_request);


                    if (gen_invoice_response.IsSuccessful)
                    {
                        //fail
                        var output = JsonConvert.DeserializeObject<SaleModel.IrnResponse>(gen_invoice_response.Content);

                        List<SaleModel.IrnResponse> outputsd = new List<SaleModel.IrnResponse>();
                        outputsd.Add(output);
                        string JsonString = JsonConvert.SerializeObject(output.data);
                        //List<SaleModel.IrnResponse> outputsd = new List<SaleModel.IrnResponse>();
                        
                        if (output.status)
                        {                     

                            var successData = output.data;
                            var Einvoicejsondata = Sales.AddEinviceData(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.EinvoiceData
                            {
                                FK_Sales = saleInfo.SalesID,
                                jsonData = JsonString
                            });

                            return Json(new APIGetRecordsDynamic<SaleModel.IrnResponse>
                            {
                                Process = new Output
                                {
                                    IsProcess = true,
                                    Message =new List<string> { "Success" },
                                  
                                },
                                Data= outputsd

                            }, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {
                            return Json(new
                            {
                                Process = new Output
                                {
                                    IsProcess = false,
                                    Message = output.error.Select(x => x.ErrorMessage).ToList(),
                                    Status = "Session Timeout",
                                }
                            }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        //fail
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
                    //SUCCESS 


                    //FAIL



                }
                else
                {

                }

            }
            //return Json(new { Einvoice= einvoiceObject }, JsonRequestBehavior.AllowGet);
            return Json(new { Einvoice = einvoiceObject }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ShowBuyBack()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SaleModel BuyBack = new SaleModel();
            //var data = Common.GetDataViaQuery<SaleModel.BuyBack>(parameters: new APIParameters
            //{
            //    TableName = "GeneralSettings",
            //    SelectFields = "GsValue,GsField",
            //    Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "  AND GsField='Buy'",
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey);

            var data = Common.GetDataViaQuery<SaleModel.BuyBack>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT007'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);          

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DisplayPaymentMethod()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SaleModel BuyBack = new SaleModel();
            //var data = Common.GetDataViaQuery<SaleModel.BuyBack>(parameters: new APIParameters
            //{
            //    TableName = "GeneralSettings",
            //    SelectFields = "GsValue,GsField",
            //    Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "  AND GsField='Buy'",
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey);

            var MOPSales = Common.GetDataViaQuery<SaleModel.BuyBack>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT008'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },

            companyKey: _userLoginInfo.CompanyKey);
            var Discount = Common.GetDataViaQuery<SaleModel.BuyBack>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT009'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },

            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { MOPSales, Discount }, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Generate_Sales_invoice(SaleModel.Invoice_ip saleInfo)
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

            SaleModel Sales = new SaleModel();

            var table1 = Sales.GetSalesInvoiceTable1(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.Invoice_ip
            {
                FK_Company=_userLoginInfo.FK_Company,
                FK_Master=saleInfo.FK_Master,
                Mode=1
            });
            var table2 = Sales.GetSalesInvoiceTable2(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.Invoice_ip
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Master = saleInfo.FK_Master,
                Mode = 2
            });
            var table3 = Sales.GetSalesInvoiceTable3(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.Invoice_ip
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Master = saleInfo.FK_Master,
                Mode = 3
            });
            //string einvoiceJson = Einvoice.Data[0].JsonData; // Serialize Einvoice to JSON string

            return Json(new { table1, table2, table3 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetLeadQuotationFill(Int64 FK_Masterval,string Transmode)
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

            SaleModel purchaseModel = new SaleModel();

            var datresponse = purchaseModel.FilleadQuotation(input: new SaleModel.LeadfillQuoation
            {
                FK_Master = FK_Masterval,
               


            }, companyKey: _userLoginInfo.CompanyKey);

            if (Transmode != null)
            {
                Common.fillOtherCharges(Transmode, FK_Masterval);
            }

            var OtherCharge = purchaseModel.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetSubTableSales
            {
                FK_Transaction = FK_Masterval,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "INQU"
            });

            return Json(new { datresponse, OtherCharge }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDeparmentbystockID(Int64 FK_Stock)
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
            SaleModel.DropDownListModel Sal = new SaleModel.DropDownListModel();
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            var departmentList = Common.GetDataViaQuery<SaleModel.DepartmentList>(parameters: new APIParameters
            {
                TableName = "Stock S JOIN Department Dp On Dp.ID_Department=S.FK_Department",
                SelectFields = "Dp.ID_Department AS DepartmentID,Dp.DeptName AS[Department]",
                Criteria =  "S.FK_Company=" + _userLoginInfo.FK_Company+ "AND S.ID_Stock="+ FK_Stock+"AND S.FK_Branch="+_userLoginInfo.FK_Branch,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

        );
           

            return Json(new { departmentList }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetProductUnit(long FK_Product)
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
            SaleModel ProductUnitData = new SaleModel();
            var UnitSelect = ProductUnitData.GetProductUnitData(companyKey: _userLoginInfo.CompanyKey, input: new SaleModel.GetProductUnitDtls
            {
                FK_Product = FK_Product,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,

            });
            SaleModel.DropDownListModel Sal = new SaleModel.DropDownListModel();
            Sal.ProductUnitList = UnitSelect.Data;
             return Json(UnitSelect, JsonRequestBehavior.AllowGet);
            //APIParameters apiSub = new APIParameters
            //{
            //    TableName = " Unit U  LEFT JOIN  MultipleUnits M On Mu.FK_Unit=U.ID_Unit",
            //    SelectFields = "FK_Unit,U.UnitName",
            //    Criteria = "Mu.Passed=1 And Mu.Cancelled=0 And FK_Product ='" + FK_Product + "'" + "AND Mu.FK_Company=" + _userLoginInfo.FK_Company,
            //    GroupByFileds = "",
            //    SortFields = ""
            //};
            //var ProductUnitInfo = Common.GetDataViaQuery<SaleModel.ProductUnitDetails>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            //return Json(ProductUnitInfo, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetSalesOtherInfo(SaleModel.SalsRecalculateInfoInput Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SaleModel objSales = new SaleModel();
            string TransModeVal = Data.TransMode;
            Common.fillOtherCharges(TransModeVal, Data.FK_Sales);
            var otherCharge = Common.GetOtherCharges(TransModeVal);
            var otherChargeTax = Common.GetOtherChargeTax(TransModeVal);

            var SalesInfo = objSales.GetSalesRecalculateInfo(input: new SaleModel.SalsRecalculateInfoInput
            {
                FK_Sales = Data.FK_Sales,
                FK_Company = _userLoginInfo.FK_Company,

                Discount = Data.Discount,
                Itemwise = Data.Itemwise,
                OtherChgDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new
            {
                SalesInfo

            }, JsonRequestBehavior.AllowGet);

            // return Json(data, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetSalesRecalculateInfo(SaleModel.SalsRecalculateInfoInput Data)

        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SaleModel objSales = new SaleModel();
            string TransModeVal = Data.TransMode;
            //Common.fillOtherCharges(TransModeVal, Data.FK_Sales);
            var otherCharge = Common.GetOtherCharges(TransModeVal);
            var otherChargeTax = Common.GetOtherChargeTax(TransModeVal);

            var SalesInfo = objSales.GetSalesRecalculateInfo(input: new SaleModel.SalsRecalculateInfoInput { FK_Sales = Data.FK_Sales,
                FK_Company = _userLoginInfo.FK_Company,
                
                Discount=Data.Discount,Itemwise=Data.Itemwise,
                OtherChgDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
            }, companyKey: _userLoginInfo.CompanyKey);
            
            return Json(new
            {
                SalesInfo
                
            }, JsonRequestBehavior.AllowGet);

            // return Json(data, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public ActionResult GetpaymentMethod()
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
            SaleModel.DropDownListModel Sal = new SaleModel.DropDownListModel();

            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name,PMDefault AS PMDefault,PMMode",
                Criteria = "cancelled=0 AND Passed =1 AND PMMode<>8 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
       companyKey: _userLoginInfo.CompanyKey
      );
            Sal.PaymentView = PaymentView.Data;
           
           
            return Json(PaymentView, JsonRequestBehavior.AllowGet);
   


        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateNewSales(SaleModel.SalesView data)
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

            if (data.ImageList != null)
            {
                foreach (SaleModel.ImageListView itm in data.ImageList)
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
            SaleModel Sales = new SaleModel();
            string warimage = (string)Session["WarProductImage"];
            string TransModeVal = "INSL";
            if (data.ImportID == 2)
            {
                TransModeVal = "INSO";
            }
            else if (data.ImportID == 6)
            {
                TransModeVal = "INQU";
            }
            else
            {
                TransModeVal = "INSL";
            }

            var otherCharge = Common.GetOtherCharges(TransModeVal);
            var otherChargeTax = Common.GetOtherChargeTax(TransModeVal);

            var datresponse = Sales.UpdateSalesData(input: new SaleModel.UpdateSales
            {

                UserAction = 2,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                ID_Sales = data.SalesID,
                ID_Hold = data.ID_Hold,
                TransMode = data.TransMode,
                FK_BillType = data.BillType,
                SalBillDate = data.SalBillDate,
                SalEnterDate = data.SalEnterDate,
                FK_Customer = data.ID_Customer,
                FK_LeadGenerate = data.FK_Lead,
                FK_CustomerOthersL = data.FK_CustomerOthers,
                FK_CustomerL = data.FK_Customer,
                MobileNo = data.MobileNo,
                SalBillTotal = data.SalBillTotal,
                SalOthercharges = data.OtherCharge,
                SalDiscount = data.SalDiscount,
                SalRoundoff = data.SalRoundoff,
                SalNetAmount = data.SalNetAmount,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BrachCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Department = _userLoginInfo.FK_Department,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                CustomeName = data.CustomeName,
                Hold = data.Hold,
                AdvAmount = data.AdvAmount,
                FK_SalesOrder = data.FK_SalesOrder,
                StockadjonHold = data.StockadjonHold,
                OtherChgDetails = otherCharge is null ? "" : Common.xmlTostring(otherCharge),
                OtherChgTaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                ProductDetail = data.ProductDetail is null ? "" : Common.xmlTostring(data.ProductDetail),
                buyback = data.buyback is null ? "" : Common.xmlTostring(data.buyback),

                DownPayment = data.DownPayment,
                AdditionalAmount = data.AdditionalAmount,
                StartDate = data.StartDate,
                FK_FinancePlanType = data.FK_FinancePlanType,

                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_Country = (data.CountryID.HasValue) ? data.CountryID.Value : 0,
                FK_State = (data.StatesID.HasValue) ? data.StatesID.Value : 0,
                FK_District = (data.DistrictID.HasValue) ? data.DistrictID.Value : 0,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                DelAddress1 = data.Address1,
                DelAddress2 = data.Address2,
                DelName = data.ShpContactName,
                DelMobileNo = data.ShpMobile,

                DelTransportType = (data.Transporttype.HasValue) ? data.Transporttype.Value : 0,
                DelVehicleNo = data.Vehicleno,
                DelDriverName = data.DrvName,
                DelDriverMobileNo = data.DrvPhoneno,
                FK_Employee = data.FK_Employee,
                ProductSerialNumbers = data.ProductSerialNumbers is null ? "" : Common.xmlTostring(data.ProductSerialNumbers),
                SubproductDetails = data.SubproductDetails is null ? "" : Common.xmlTostring(data.SubproductDetails),
                WarrantyImgDetails = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                FK_Bank = (data.BankID.HasValue) ? data.BankID.Value : 0,
                ChekStandBy = data.ChekStandBy,
                ImportID = data.ImportID,
                FK_Quotation = data.FK_Quotation,
                FK_Vehicle = data.FK_Vehicle

            }, companyKey: _userLoginInfo.CompanyKey);

            if (datresponse.IsProcess)
            {
                Session["WarProductImage"] = "";
                sendMail objMail = new sendMail();
                objMail.sendMailData(Convert.ToInt32(datresponse.code), "SALES", _userLoginInfo.FK_Company, 1, 3, _userLoginInfo.CompanyKey, "");
                Common.ClearOtherCharges(data.TransMode);
            }

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}

