using PerfectWebERP.Filters;
//using PerfectWebERP.General;
//using PerfectWebERP.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.IO;

using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class CustomerProductMappingController : Controller
    {
        // GET: CustomerProductMapping
        public ActionResult Index(string mtd,string mgrp)        {
          
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

            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
           
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            // ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadCustomerProductForm(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            CustomerProductMappingModel.CustomerProductMappingViewList CustomerPrdctmListObj = new CustomerProductMappingModel.CustomerProductMappingViewList();
            CustomerPrdctmListObj.FK_Employee = _userLoginInfo.FK_Employee;
            var EmpName = Common.GetDataViaQuery<CustomerProductMappingModel.EmployeeInfo>(parameters: new APIParameters
            {
                TableName = "Employee",
                SelectFields = "ID_Employee,EmpFName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
      companyKey: _userLoginInfo.CompanyKey

         );
            CustomerPrdctmListObj.EmployeeInfoList = EmpName.Data;

            var LeadFrmModelist = Common.GetDataViaQuery<CustomerProductMappingModel.LeadFrom>(parameters: new APIParameters
            {
                TableName = "LeadFrom",
                SelectFields = "ID_LeadFrom  ,LeadFromName",
                Criteria = "LeadFromName in('Branch','Dealer','Franchise','Extension Counter','Freelancer')",
                SortFields = "ID_LeadFrom",
                GroupByFileds = ""
            },

companyKey: _userLoginInfo.CompanyKey

   );
            CustomerPrdctmListObj.BranchNameList = LeadFrmModelist.Data;


            var branchtypelist = Common.GetDataViaQuery<CustomerProductMappingModel.BranchTypes>(parameters: new APIParameters
            {
                TableName = "BranchType",
                SelectFields = "ID_BranchType AS BranchTypeID,BTName AS BranchType,FK_BranchMode AS BranchModeID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
             );
            CustomerPrdctmListObj.BranchTypelists = branchtypelist.Data;


            var warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
             );
            CustomerPrdctmListObj.Warrantytype = warrantytype.Data;

            var amctype = Common.GetDataViaQuery<AMCTypeModel.AMCTypeView>(parameters: new APIParameters
            {
                TableName = "AMCType",
                SelectFields = "ID_AMCType as AMCTypeID,AMCName as AMCName",
                Criteria = "Cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            CustomerPrdctmListObj.AMCtype = amctype.Data;

            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddCustomerProductMapping", CustomerPrdctmListObj);
        }

        public ActionResult GetCusDtlByMob(string CusPhnNo)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
          
            CustomerProductMappingModel custinfo = new CustomerProductMappingModel();

            //var data = Common.GetDataViaQuery<CustomerProductMappingModel.CustInfo>(parameters: new APIParameters
            //{
            //    TableName = "Customer  AS C LEFT JOIN Post As Pt ON Pt.ID_Post=C.FK_Post LEFT JOIN District As D ON D.ID_District=C.FK_District   LEFT JOIN States AS ST ON ST.ID_States=D.FK_States LEFT JOIN Country AS Ct ON Ct.ID_Country=ST.FK_Country LEFT JOIN Company On C.FK_Company=Company.ID_Company",
            //    SelectFields = "CusName as CustomerName,CusAddress1,CusMobile CusPhnNo,CompName as Company,CntryCode CountryID,CntryName as Country,ID_States StatesID,StName as States,ID_District DistrictID,DtName as District,ID_Post PostID, PostName as Post,PinCode",
            //    Criteria = "C.Cancelled=0  AND C.Passed=1 AND C.FK_Company=" + _userLoginInfo.FK_Company + "  AND CusMobile='" + CusPhnNo + "'",
            //    SortFields = "",
            //    GroupByFileds = ""
            //},
            //companyKey: _userLoginInfo.CompanyKey);
            var data = Common.GetDataViaQuery<CustomerProductMappingModel.CustInfo>(parameters: new APIParameters
            {
                TableName = "Customer  AS C LEFT JOIN Area As Ar ON Ar.ID_Area=C.FK_Area LEFT JOIN Post As Pt ON Pt.ID_Post=C.FK_Post LEFT JOIN District As D ON D.ID_District=C.FK_District   LEFT JOIN States AS ST ON ST.ID_States=D.FK_States LEFT JOIN Country AS Ct ON Ct.ID_Country=ST.FK_Country LEFT JOIN Company On C.FK_Company=Company.ID_Company",
                SelectFields = "ID_Customer AS ID_Customer,CusName CustomerName,CusAddress1,CusMobile as CusPhnNo,C.FK_Country as CountryID,C.FK_State as StatesID,C.FK_District as DistrictID,C.FK_Post as PostID,C.FK_Area as AreaID,CntryName,StName,DtName,PostName,AreaName,C.CusLandmark as Landmark,PinCode,Company.CompName Company,C.CusReferenceNo CusReferenceNo",
                Criteria = "C.Cancelled=0  AND C.Passed=1 AND C.FK_Company=" + _userLoginInfo.FK_Company + "  AND CusMobile='" + CusPhnNo + "'",
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);


            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProductSearch(int FK_Category, string ProductName)
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

            var data = Common.GetDataViaQuery<CustomerProductMappingModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P",
                SelectFields = "ID_Product,ProdName as ProductName,ProdShortName , ProdHSNCode ",
                Criteria = "Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "  AND FK_Category=" + FK_Category + "  AND ProductName LIKE +" + "'%" + ProductName + "%'",

                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

       
        public ActionResult GetPinCodedetails(string Pincode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
          
            var data = Common.GetDataViaQuery<CustomerProductMappingModel.searchPinModel>(parameters: new APIParameters
            {
                TableName = "Post As Pt  JOIN Area A ON A.ID_Area=Pt.FK_Area  JOIN District As D ON D.ID_District=A.FK_District  JOIN States AS ST ON ST.ID_States=D.FK_States  JOIN Country AS Ct ON Ct.ID_Country=ST.FK_Country",
                SelectFields = "ID_Country CountryID,CntryName as Country,ID_States StatesID,StName as States,ID_District DistrictID,DtName as District,A.AreaName AS AreaName,A.ID_Area AS AreaID,ID_Post PostID, PostName as Post,PinCode",
                Criteria = "Pt.Cancelled =0 AND Pt.Passed=1  AND Pt.FK_Company=" + _userLoginInfo.FK_Company + "  AND Pt.Pincode='" + Pincode + "'",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetImage(string byteData)
        {
            byte[] bytes = Convert.FromBase64String(byteData);


            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Json(base64String, JsonRequestBehavior.AllowGet);
        }

       
    //[ValidateInput(false)]
    //    public JsonResult SaveToTemp(HttpPostedFileBase file)
    //    {
    //        try
    //        {
    //            string filename = "";
    //            string imgepath = "Null";
    //            if (file != null)
    //            {
    //                filename = file.FileName;
    //                imgepath = filename;
    //                string extension = Path.GetExtension(file.FileName);
    //                filename = DateTime.Now.Ticks + filename;
    //                var path = Path.Combine(Server.MapPath("~/Temp/"), filename);
    //                file.SaveAs(path);
    //            }
    //            return Json(filename, JsonRequestBehavior.AllowGet);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

        public ActionResult GetBranchNameList(Int32 ID_LeadFrom)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CustomerProductMappingModel objfld = new CustomerProductMappingModel();


            var BranchNameData = objfld.GetBranchNames(input: new CustomerProductMappingModel.GetBranchName
            {
                TransMode = "",
                ID_LeadFrom = ID_LeadFrom,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = 0,
                PageSize = 10,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine
            },
             companyKey: _userLoginInfo.CompanyKey);

            return Json(BranchNameData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetBranchList(string branchtypeid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerProductMappingModel.Branchs>(parameters: new APIParameters
            {

                TableName = "Branch",
                SelectFields = " BrName AS Branch,ID_Branch AS BranchID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND Cancelled =0 AND Passed=1 AND FK_BranchType='" + branchtypeid + "'",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult GetDefaultBranchName()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CustomerProductMappingModel.EmployeeInfo>(parameters: new APIParameters
            {
                TableName = "Employee E  join Branch B on E.FK_Branch=B.ID_Branch  join BranchType BT on B.FK_BranchType=BT.ID_BranchType  left join Department D on  E.FK_Department = D.ID_Department",
                SelectFields = "E.ID_Employee,E.EmpFName,CASE WHEN BT.FK_BranchMode IN (1,2) THEN 1 ELSE -1 END AS ID_BranchMode,B.ID_Branch,BT.ID_BranchType,E.FK_Department, BT.BTName,B.BrName,D.DeptName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
companyKey: _userLoginInfo.CompanyKey

);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCustomerWiseProductList(int pageSize, int pageIndex, string Name,string Transmode)
        {
            #region ::  Check User Session to verifyLogin  ::
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            //UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            //pageAccess = _userLoginInfo.PageAccessRights;
            //ViewBag.PagedAccessRights = pageAccess;
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



            CustomerProductMappingModel CustomerProductload = new CustomerProductMappingModel();

            var data = CustomerProductload.GetCustomerWiseProductData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerProductMappingModel.CustomerWiseProductID
            {
               //ID_CustomerWiseProductDetails = 0,
                GroupID=0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = Transmode,
                Detailed = 0,
                Name= Name
            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetCustomerProductInfo(CustomerProductMappingModel.CustomerWiseProductList data)
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


            #endregion :: Model validation  ::


            CustomerProductMappingModel Productdata = new CustomerProductMappingModel();

            var ProductdataInfo = Productdata.GetCustomerWiseProductData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerProductMappingModel.CustomerWiseProductID
            {
                // ID_CustomerWiseProductDetails = data.ID_CustomerWiseProductDetails,
                GroupID = data.GroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 0,
                TransMode = data.TransMode
            });
            var ProductInfo = Productdata.GetCustomerWiseProductDetails(companyKey: _userLoginInfo.CompanyKey, input: new CustomerProductMappingModel.CustomerWiseProductDataView
            {

                GroupID = data.GroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Detailed = 1,
                TransMode = data.TransMode
            });
            var Imageselect = Productdata.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new CustomerProductMappingModel.GetProductImagein
            {
                FK_Master = data.GroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Product = data.ProductID,
                TransMode = data.TransMode
            });


            var warrantyselect = Productdata.GetWarrantySelect(companyKey: _userLoginInfo.CompanyKey, input: new CustomerProductMappingModel.GetWarrantyImagein
            {
                FK_Master = data.GroupID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_Product = data.ProductID
            });

            return Json(new {  ProductdataInfo, ProductInfo, Imageselect, warrantyselect }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerwiseProductInfo(CustomerProductMappingModel.CustomerWiseProductList data)
        {
            CustomerProductMappingModel Productdata = new CustomerProductMappingModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var Imageselect = Productdata.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new CustomerProductMappingModel.GetProductImagein
            {
                FK_Master = data.ID_CustomerWiseProductDetails,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Product = data.ProductID,
                TransMode = data.TransMode
            });


            var warrantyselect = Productdata.GetWarrantySelect(companyKey: _userLoginInfo.CompanyKey, input: new CustomerProductMappingModel.GetWarrantyImagein
            {
                FK_Master = data.ID_CustomerWiseProductDetails,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_Product=data.ProductID
            });

          

            

            return Json(new { Imageselect,warrantyselect }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetWarrantyInfo(CustomerProductMappingModel.GetWarrantyDtls data)
        {
            CustomerProductMappingModel warrData = new CustomerProductMappingModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
           
            var warrantyselect = warrData.GetWarrantyDtlsData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerProductMappingModel.GetWarrantyDtls
            {
                FK_Product = data.FK_Product,
                TransDate = data.TransDate
            });

            return Json(new { warrantyselect }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveCustomerProductMapping(CustomerProductMappingModel.CustomerWiseProductDetailsView data)
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

            CustomerProductMappingModel ServiceMapping = new CustomerProductMappingModel();
            //string ProductImage = (string)Session["ProductImage"];

            //string warimage = (string)Session["WarrantyProductImage"];
            string warimage = (string)Session["WarProductImage"];

            var datresponse = ServiceMapping.UpdateCustomerProductMappingData(input: new CustomerProductMappingModel.UpdateCustomerWiseProductDetails
            {
                UserAction=1,
                TransMode=data.TransMode,
                ID_CustomerWiseProductDetails = data.ID_CustomerWiseProductDetails,
                FK_CustomerWiseProductDetails= data.ID_CustomerWiseProductDetails,
                FK_Customer = data.FK_Customer,
                FK_Sales = data.FK_Sales,
                FK_Product = data.ProductID,
                FK_EMIPlan = data.FK_EMIPlan,
                CWPDSalesDate = data.CWPDSalesDate,
                CWPDTotalAmount = data.CWPDTotalAmount,
                CWPDInstalmentAmount = data.CWPDInstalmentAmount,
                CWPDSalFreeQuantity = data.CWPDSalFreeQuantity,
                CWPDSalActualQuantity = data.CWPDSalQuantity,
                CWPDProductStatus = data.CWPDProductStatus,
                CWPDModelNo = data.CWPDModelNo,
                BillNo = data.BillNo,
                CustomerName=data.CustomerName,
                Address1=data.Address1,
                CusPhnNo = data.CusPhnNo,
                PinCode=data.PinCode,
                Company = data.Company,
                FK_Branch = data.BranchID,
                FK_Country = data.CountryID,
                FK_District = data.DistrictID,
                FK_State = data.StatesID,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                Remarks = data.Remarks,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Landmark=data.Landmark,
                CustomerWiseProductDetails =data.CustomerWiseProductDetails is null ? "" : Common.xmlTostring(data.CustomerWiseProductDetails),
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                WarrantyImgDetails = warimage,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                CusReferenceNo =data.CusReferenceNo
            }, companyKey: _userLoginInfo.CompanyKey);

            Session["WarProductImage"] = "";

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateCustomerProductMapping(CustomerProductMappingModel.CustomerWiseProductList data)
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

            CustomerProductMappingModel ServiceMapping = new CustomerProductMappingModel();
          
            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            string warimage = (string)Session["WarrantyProductImage"];
            var datresponse = ServiceMapping.UpdateCustomerProductMappingData(input: new CustomerProductMappingModel.UpdateCustomerWiseProductDetails
            {
                   UserAction = 2,
                ID_CustomerWiseProductDetails = data.ID_CustomerWiseProductDetails,
                FK_Customer = data.FK_Customer,
                FK_Sales = data.FK_Sales,
                FK_Product = data.FK_Product,
                FK_EMIPlan = data.FK_EMIPlan,
                CWPDSalesDate = data.CWPDSalesDate,
                CWPDTotalAmount = data.CWPDTotalAmount,
                CWPDInstalmentAmount = data.CWPDInstalmentAmount,
                CWPDSalQuantity = data.CWPDSalQuantity,
                CWPDSalFreeQuantity = data.CWPDSalFreeQuantity,
                CWPDSalActualQuantity = data.CWPDSalActualQuantity,
                CWPDProductStatus = data.CWPDProductStatus,
                CWPDModelNo = data.CWPDModelNo,
                BillNo = data.BillNo,
                Landmark = data.Landmark,
                Remarks = data.Remarks,
                FK_Branch = data.BranchID,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                CustomerWiseProductDetails = data.CustomerWiseProductDetails is null ? "" : Common.xmlTostring(data.CustomerWiseProductDetails),
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                WarrantyImgDetails = warimage,
                TransMode = data.TransMode,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                CusReferenceNo = data.CusReferenceNo
            }, companyKey: _userLoginInfo.CompanyKey);
            Session["WarrantyProductImage"] = "";
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerProductReasonList()
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
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteCustomerProductInfo(CustomerProductMappingModel.DeleteCustomerProduct data)

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


            CustomerProductMappingModel.DeleteCustomerProduct CustomerWiseProductdelte = new CustomerProductMappingModel.DeleteCustomerProduct
            {
                TransMode = "",
                Debug = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason = data.FK_Reason,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                GroupID = data.GroupID
            };

            Output dataresponse = Common.UpdateTableData<CustomerProductMappingModel.DeleteCustomerProduct>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCustomerWiseProductDetailsDelete", parameter: CustomerWiseProductdelte);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAMCWarrantydetails(CustomerProductMappingModel.AmcWarrantyDetailsInput Data)
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

            CustomerProductMappingModel ServiceMapping = new CustomerProductMappingModel();

            var datresponse = ServiceMapping.GetAmcWarrantyfill(input: new CustomerProductMappingModel.GetAmcWarrantyDetails
            {
                Mode = Data.Mode,
                FK_Type = Data.FK_Type,
                Date = Data.Date,
                Quantity = Data.Quantity,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }
    }
}
