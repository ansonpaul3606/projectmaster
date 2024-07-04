using Newtonsoft.Json;
using PerfectWebERP.Business;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using PerfectWebERP.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]

    public class ProductController : Controller
    {
        public static string xmlstr;
        // GET: Product
        public ActionResult Index()
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.Companycategory = _userLoginInfo.CompCategory;

            return View();

          
        }
        public ActionResult LoadProductForm()
        {
            #region::Check User Session to verifyLogin::

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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            //  Prd.SubCategoryList=
            var LeadType = System.Configuration.ConfigurationManager.AppSettings["Lead"];
            var leadmode = "";
            if (LeadType=="1")
            {
                leadmode = "  AND ID_ModuleType=4";
            }           

            APIParameters apiParameters = new APIParameters
            {
                TableName = "[ModuleType]",
                SelectFields = "[ID_ModuleType] AS ID_ModuleType,[ModuleName] AS ModuleName,[Mode] as Mode",
                Criteria = "Mode!='V'"+ leadmode ,
                GroupByFileds = "",
                SortFields = ""
            };
            var Module = Common.GetDataViaQuery<ProductModel.ModuleType>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameters);



            ProductModel.DropDownListModel Prd = new ProductModel.DropDownListModel();            
            StateModel obj = new StateModel();
            Prd.ModuleTypeList = Module.Data;

            APIParameters apiParam = new APIParameters
            {
                TableName = "[Manufacturer]",
                SelectFields = "[ID_Manufacturer] AS ID_Manufacturer,[ManufName] AS ManufName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var manufacture = Common.GetDataViaQuery<ProductModel.Manufacture>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParam);

            Prd.ManufactureList = manufacture.Data;
            APIParameters apiParaUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "[ID_Unit] AS ID_Unit,[UnitName] AS UnitName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var unit = Common.GetDataViaQuery<ProductModel.Unit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaUnit);

            Prd.UnitList = unit.Data;
            APIParameters apiParaMulUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "[ID_Unit] AS ID_Unit,[UnitName] AS UnitName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var MUnit = Common.GetDataViaQuery<ProductModel.MultiUnit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaMulUnit);

            Prd.MultipleUnitList = MUnit.Data;
            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Passed=1 And Cancelled=0 AND Project=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Category = Common.GetDataViaQuery<ProductModel.Category>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            Prd.CategoryList = Category.Data;

          
            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<ProductModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);

            Prd.TaxgroupList = taxgroup.Data;
            var warrantytype = Common.GetDataViaQuery<WarrantyTypeModel.WarrantyTypeView>(parameters: new APIParameters
            {
                TableName = "WarrantyType",
                SelectFields = "ID_WarrantyType as WarrantyTypeID,WartyName as WarrantyName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
           Prd.Warrantytype = warrantytype.Data;
            var warrtype = Common.GetDataViaQuery<ProductModel.WarrantyList>(parameters: new APIParameters
            {
                TableName = "WarrantyTypeSetting WS",
                SelectFields = " WS.ID_WarrantyTypeSetting,WS.WarrantyTypeSetting ",
                Criteria = "",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            Prd.warrlist = warrtype.Data;
            ViewBag.Companycategory = _userLoginInfo.CompCategory;

            var OpBranchListto = Common.GetDataViaQuery<ProductModel.BranchTo>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchIDTo,BrName AS BranchNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                  companyKey: _userLoginInfo.CompanyKey

           );
            Prd.BranchListTo = OpBranchListto.Data;

            var OpDepartmentListto = Common.GetDataViaQuery<ProductModel.DepartmentTo>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department AS DepartmentIDTo,DeptName AS DepartmentNameTo",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                     companyKey: _userLoginInfo.CompanyKey

          );
            Prd.DepartmentListTo = OpDepartmentListto.Data;

            var unitset = Common.GetDataViaQuery<ProductModel.MultipleUnitSettings>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'INVT' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='INVT003'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
                    companyKey: _userLoginInfo.CompanyKey

         );
            
           
           
            ViewBag.Multiunitsettings = unitset.Data[0].GsValue;

            


            return PartialView("_ProductForm", Prd);

        }
        public ActionResult GetTaxType()
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

            var data = Common.GetDataViaQuery<ProductModel.TaxType>(parameters: new APIParameters
            {
                TableName = "TaxType T",
                SelectFields = "T.TaxtyName,T.ID_TaxType",
                Criteria = "T.Cancelled=0 AND T.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProduct()
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

            var data = Common.GetDataViaQuery<ProductModel.InclusiveProduct>(parameters: new APIParameters
            {
                TableName = "Product P",
                SelectFields = "P.ProdName,P.ID_Product",
                Criteria = "P.Cancelled=0 AND P.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetProductInfo(ProductModel.ProductView productInfo)
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


            ProductModel  product = new ProductModel();

            var prInfo = product.GetProductData(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProduct { ID_Product = productInfo.ProductID, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy });
            var subproduct = product.GetSubProduct(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProduct { ID_Product = productInfo.ProductID, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy, FK_Machine= _userLoginInfo.FK_Machine });

            var tax = product.GettaxData(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProduct { ID_Product = productInfo.ProductID, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy });
            var warrDtls=product.GetProductWarranty(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProduct { ID_Product = productInfo.ProductID, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });
            var VMultiUnit = product.GetMultipleunits(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProduct { ID_Product = productInfo.ProductID, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });
            return Json(new { tax,subproduct , prInfo, warrDtls, VMultiUnit }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Deleteproduct(ProductModel.ProductView data)
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

            ProductModel product = new ProductModel();

            var datresponse = product.DeleteProductData(input: new ProductModel.DeleteProduct { ID_Product = data.ProductID,EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_Company = _userLoginInfo.FK_Company, FK_Reason = data.ReasonID, }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteProductInfo(int ProductID)
        {
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
            // HttpResponseMessage datresponse = APIServices.CallAPiService(bldash, "api/DashBoardApi/UserAccountSelect/");

            int OrgnCode = 1;
            int FK_Machine = 100;
            string userCode = "code";

            string Bankkey = "";
            BlServices blServices = new BlServices();
            blServices.DbName = "PERFECTERP" + Bankkey;

            blServices.ObjectName = "[ProProductDelete]";
            //blServices.ObjectParameters = "@OrgnCode|@FK_Machine|@ID_Customer|@UserCode";
            //blServices.ObjectArguments = $"{OrgnCode}|{FK_Machine}|{customerID}|{userCode}";
            blServices.ObjectParameters = "@FK_Product|@UserCode|@FK_Machine";
            blServices.ObjectArguments = $"{ProductID}|{userCode}|{FK_Machine}";

            blServices.ObjectDataTypes = "int16|string|int16";
            blServices.ObjectSplitChar = "|";
            HttpResponseMessage datresponse = APIServices.CallAPiService(blServices, "api/Masters/GetMastersProcedureOutputAsNonQuery");


            rootProcedureOutputAsNonQuery root = new rootProcedureOutputAsNonQuery();
            if (datresponse.IsSuccessStatusCode)
            {
                root = JsonConvert.DeserializeObject<rootProcedureOutputAsNonQuery>(datresponse.Content.ReadAsStringAsync().Result);

                // Need to check the value of *root* and then determine whether its success or failure then build curresponding response to front end
                return Json(new
                {
                    Process = new Output
                    {
                        IsProcess = true,
                        Message = new List<string> { "Deleted Successfully" },
                        Status = "OK"
                    }
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                Process = new Output
                {
                    IsProcess = false,
                    Message = new List<string> { "Something Went Wrong" },
                    Status = "API Error"
                }
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBindata(ProductModel.ModuleType cr)
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
            ProductModel.DropDownListModel Prd = new ProductModel.DropDownListModel();
            APIParameters apiParam = new APIParameters
            {
                
                TableName = "[Manufacturer]",
                SelectFields = "[ID_Manufacturer] AS ID_Manufacturer,[ManufName] AS ManufName",
                Criteria = "Passed=1 And Cancelled=0 and  Mode='" + cr.Mode + "'" +"AND FK_Company=" + _userLoginInfo.FK_Company ,
                GroupByFileds = "",
                SortFields = ""
            };
            var manufacture = Common.GetDataViaQuery<ProductModel.Manufacture>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParam);

            Prd.ManufactureList = manufacture.Data;
            APIParameters apiParaUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "[ID_Unit] AS ID_Unit,[UnitName] AS UnitName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var unit = Common.GetDataViaQuery<ProductModel.Unit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaUnit);

            Prd.UnitList = unit.Data;

            APIParameters apiParaMulUnit = new APIParameters
            {
                TableName = "[Unit]",
                SelectFields = "[ID_Unit] AS FK_Unit,[UnitName] AS UnitName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var MUnit = Common.GetDataViaQuery<ProductModel.MultiUnit>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParaMulUnit);

            Prd.MultipleUnitList = MUnit.Data;
            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[Category]",
                SelectFields = "[ID_Category] AS CategoryID,[CatName] AS CategoryName",
                Criteria = "Passed=1 And Cancelled=0 And Mode='" + cr.Mode + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Category = Common.GetDataViaQuery<ProductModel.Category>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            Prd.CategoryList = Category.Data;
            APIParameters apiParameSubCate = new APIParameters
            {
                TableName = "[SubCategory]",
                SelectFields = "[ID_SubCategory],FK_Category as CategoryID,[SubCatName] AS SubCatName",
                Criteria = "Passed=1 And Cancelled=0 And Mode='" + cr.Mode + "'" +  "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubCategory = Common.GetDataViaQuery<ProductModel.Subcategory>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameSubCate);
            Prd.SubCategoryList = SubCategory.Data;

            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<ProductModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);

            Prd.TaxgroupList = taxgroup.Data;
            //   var SubcategoryInfo = Common.GetDataViaProcedure<ProductModel.Subcategory, ProductModel.Category>(companyKey: _userLoginInfo.CompanyKey, procedureName: "DistrictSelect", parameter: new ProductModel.Category { CategoryID = cr.CategoryID });

            return Json(Prd, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetSubcategory(ProductModel.Category cr)
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
            APIParameters apiSub = new APIParameters
            {
                TableName = "[SubCategory]",
                SelectFields = "[ID_SubCategory] AS ID_SubCategory,[SubCatName] AS SubCatName",
                Criteria = "Passed=1 And Cancelled=0 And Mode='" + cr.Mode + "'" + "AND+ FK_Category ='" + cr.CategoryID + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<ProductModel.Subcategory>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetProductList(ProductModel.GetProduct data)
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

            ProductModel product = new ProductModel();

            var outputList = product.GetProductData(companyKey: _userLoginInfo.CompanyKey,  input: new ProductModel.GetProduct { ID_Product = 0 ,TransMode= data.TransMode, Name =data.Name,FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetProductListView(int pageSize, int pageIndex, string Name)
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



            string transMode = "";
            ProductModel product = new ProductModel();

            var data = product.GetProductDataSearch(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProductID
            {
                ID_Product = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                //FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                UserCode = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //Search= Name,
                TransMode = transMode
            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewProduct(ProductModel.ProductView data)
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
            ModelState.Remove("ProductID");
            ModelState.Remove("ProdQRCode");
            ModelState.Remove("ProdCode");
            ModelState.Remove("ProductID");
            ModelState.Remove("ProdReorderLevel");
            ModelState.Remove("ProdMaxLevel");
            ModelState.Remove("ProdMinLevel");
            ModelState.Remove("ProdReorderQuantity");
            ModelState.Remove("ProdSales");
            ModelState.Remove("ProdSalesReturn");
            ModelState.Remove("ProdStockTransfer");
            ModelState.Remove("ProdProductionIn");
            ModelState.Remove("ProdProductionOut");
            ModelState.Remove("ProdPurchaseReturn");
            ModelState.Remove("AmountWise");
            ModelState.Remove("SubCategotyID");
            ModelState.Remove("ManufacturerID");
            ModelState.Remove("AmountWise");
            ModelState.Remove("CategoryID");
            ModelState.Remove("TaxGroupID");
            ModelState.Remove("UnitID");

            ModelState.Remove("MRP");
            ModelState.Remove("SalPrice");
            ModelState.Remove("ProductionCost");
            ModelState.Remove("OpeningQuantity");
            ModelState.Remove("OpeningStbyQuantity");
            ModelState.Remove("BranchID");
            ModelState.Remove("DepartmentID");
            //ModelState.Remove("QRCode");
            //ModelState.Remove("BarCode");
            ModelState.Remove("BatchNo");
            ModelState.Remove("ExpiryDate");
            ModelState.Remove("PurRate");

           

            #region :: Model validation  ::
            //--- Model validation 
            if (!ModelState.IsValid)
            {
                List<string> errorList = new List<string>();

                //errorList = ModelState.Values.SelectMany(m => m.Errors)
                //                        .Select(e => e.ErrorMessage)
                //                        .ToList();

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


            ProductModel customer = new ProductModel();

            //convert a model to XML 
            //    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(data.TaxTypeDetails.GetType());
            //    System.IO.TextWriter writer = new System.IO.StringWriter();
            //    x.Serialize(writer, data.TaxTypeDetails);

            //ArrayList ar = new ArrayList();
            //    ar.Add(data.TaxTypeDetails);
            //    ProductModel.ProductView root5 = new ProductModel.ProductView();
            //    root5.TaxTypeDetails = data.TaxTypeDetails;
            //    System.Xml.Serialization.XmlSerializer y = new System.Xml.Serialization.XmlSerializer(root5.GetType());
            //    System.IO.TextWriter writer2 = new System.IO.StringWriter();
            //    y.Serialize(writer2, root5);

            //  blAccountHeadSubTransaction.XmlAccountTransMultiple = xmlstr;
            var datresponse = customer.UpdateProductData(input: new ProductModel.UpdateProduct
            {

                UserAction = 1,
                ProdNonStockItem = data.ProdNonStockItem,
                Mode = data.Mode,
                ProdName = data.ProdName,
                ProdShortName = data.ProdShortName,
                ProdHSNCode = data.ProdHSNCode,
                ProdCode = data.ProdCode,
                ProdMinLevel = data.ProdMinLevel,
                ProdMaxLevel = data.ProdMaxLevel,
                ProdReorderLevel = data.ProdReorderLevel,
                ProdReorderQuantity = data.ProdReorderQuantity,
                ProdMaterialDetails = data.ProdMaterialDetails,
                ProdSales = data.ProdSales,
                ProdSalesReturn = data.ProdSalesReturn,
                ProdPurchase = data.ProdPurchase,
                ProdPurchaseReturn = data.ProdPurchaseReturn,
                ProdStockTransfer = data.ProdStockTransfer,
                ProdProductionIn = data.ProdProductionIn,
                ProdProductionOut = data.ProdProductionOut,
                ProdQRCode = data.ProdQRCode,
                ProdBarcode = data.ProdBarcode,
                FK_Category = data.CategoryID,
                FK_SubCategoty = data.SubCategotyID,
                FK_Manufacturer = data.ManufacturerID,
                FK_Unit = data.UnitID,
                FK_Branch = _userLoginInfo.FK_Branch,
                TaxGroupID = data.TaxGroupID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode=data.TransMode,
                //TaxTypeDetails = data.TaxTypeDetails is null?"": Common.xmlTostring(data.TaxTypeDetails),
                SubProductDetails = data.SubProductDetails is null ? "" : Common.xmlTostring(data.SubProductDetails),
                WarrantyDetails = data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                ProdLead = data.ProdLead,
                ProdProject = data.ProdProject,
                MinRate=(data.MinAmount.HasValue) ? data.MinAmount.Value : 0,
                MaxRate = (data.MaxAmount.HasValue) ? data.MaxAmount.Value : 0,

                BranchID = data.BranchID,
                DepartmentID = data.DepartmentID,
                SalPrice = data.SalPrice,
                MRP = data.MRP,
                PurRate = data.PurRate,
                ProductionCost = data.ProductionCost,
                OpeningQuantity = data.OpeningQuantity,
                OpeningStbyQuantity = data.OpeningStbyQuantity,
                //BarCode = data.BarCode,
                //QRCode = data.QRCode,
                BatchNo = data.BatchNo,
                ExpiryDate = data.ExpiryDate == null ? "" : data.ExpiryDate.ToString(),
                MultipleUnits = data.MultipleUnits != null ? Common.xmlTostring(data.MultipleUnits.Select(a => new ProductModel.MultipleUnits { FK_Unit = a }).ToList()) :null
                //writer2.ToString(),
                // TaxXml = Common.WriteToXml(ar)
                //ID_Product = 0,

                // EntrOn=DateTime.UtcNow
            }, companyKey: _userLoginInfo.CompanyKey);
             // Show Error Message
             Output output = new Output();

            if (datresponse.Data.FirstOrDefault().Column1 > 0)
            {
         
                output.IsProcess = true;
                output.Message = new List<string> { "Saved Successfully" };

            }
            else
            {
                output.Message = new List<string> { datresponse.Data.FirstOrDefault().ErrMsg };
                output.code = datresponse.Data.FirstOrDefault().ErrCode;
                output.IsProcess = false;
            }

            return Json(new { Process = output }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateProduct(ProductModel.ProductView data)
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

            ModelState.Remove("ProdQRCode");
            ModelState.Remove("ProdCode");
      
            ModelState.Remove("ProdReorderLevel");
            ModelState.Remove("ProdMaxLevel");
            ModelState.Remove("ProdMinLevel");
            ModelState.Remove("ProdReorderQuantity");
            ModelState.Remove("ProdSales");
            ModelState.Remove("ProdSalesReturn");
            ModelState.Remove("ProdStockTransfer");
            ModelState.Remove("ProdProductionIn");
            ModelState.Remove("ProdProductionOut");
            ModelState.Remove("ProdPurchaseReturn");
            ModelState.Remove("AmountWise");
            ModelState.Remove("SubCategotyID");
            ModelState.Remove("ManufacturerID");
            ModelState.Remove("AmountWise");
            ModelState.Remove("CategoryID");
            ModelState.Remove("TaxGroupID");
            ModelState.Remove("UnitID");                                        //<remove node in model validation here> 
            ModelState.Remove("MRP");
            ModelState.Remove("SalPrice");
            ModelState.Remove("ProductionCost");
            ModelState.Remove("OpeningQuantity");
            ModelState.Remove("OpeningStbyQuantity");
            ModelState.Remove("BranchID");
            ModelState.Remove("DepartmentID");
            //ModelState.Remove("QRCode");
            //ModelState.Remove("BarCode");
            ModelState.Remove("BatchNo");
            ModelState.Remove("ExpiryDate");
            ModelState.Remove("PurRate");

            #region :: Model validation  ::

            //--- Model validation 
            //if (!ModelState.IsValid)
            //{

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

            ProductModel product = new ProductModel();

            ArrayList ar = new ArrayList();
            ar.Add(data.TaxTypeDetails);

            var datresponse = product.UpdateProductData(input: new ProductModel.UpdateProduct
            {
               UserAction = 2,
               ProdNonStockItem=data.ProdNonStockItem,
               Mode = data.Mode is null ? "": data.Mode,
               ID_Product= data.ProductID,
                ProdName = data.ProdName is null ? "" : data.ProdName,
               ProdShortName = data.ProdShortName is null ? "" : data.ProdShortName,
               ProdHSNCode = data.ProdHSNCode is null ? "" : data.ProdHSNCode,
                ProdCode = data.ProdCode is null ? "" : data.ProdCode,
                ProdMinLevel = data.ProdMinLevel,
                ProdMaxLevel = data.ProdMaxLevel,
                ProdReorderLevel = data.ProdReorderLevel,
                ProdReorderQuantity = data.ProdReorderQuantity,
               ProdMaterialDetails = data.ProdMaterialDetails is null ? "" : data.ProdMaterialDetails,
                ProdPurchase = data.ProdPurchase,
                ProdSales = data.ProdSales,
               ProdSalesReturn = data.ProdSalesReturn,
                ProdPurchaseReturn = data.ProdPurchaseReturn,
                ProdStockTransfer = data.ProdStockTransfer,
                ProdProductionIn = data.ProdProductionIn,
                ProdProductionOut = data.ProdProductionOut,
                ProdQRCode = data.ProdQRCode is null ? "" : data.ProdQRCode,
                ProdBarcode = data.ProdBarcode is null ? "" : data.ProdBarcode,
                FK_Category = data.CategoryID,
                FK_SubCategoty = data.SubCategotyID,
                FK_Manufacturer = data.ManufacturerID,
                FK_Unit = data.UnitID,
                TaxGroupID=data.TaxGroupID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Branch = _userLoginInfo.FK_Branch,
                TransMode=data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,

               // TaxTypeDetails = data.TaxTypeDetails is null ? "" : Common.xmlTostring(data.TaxTypeDetails),
                SubProductDetails = data.SubProductDetails is null ? "" : Common.xmlTostring(data.SubProductDetails),
                WarrantyDetails=data.WarrantyDetails is null ? "" : Common.xmlTostring(data.WarrantyDetails),
                ProdLead = data.ProdLead,
                MinRate = (data.MinAmount.HasValue) ? data.MinAmount.Value : 0,
                MaxRate = (data.MaxAmount.HasValue) ? data.MaxAmount.Value : 0,
                ProdProject = data.ProdProject,
                BranchID = data.BranchID,
                DepartmentID = data.DepartmentID,
                SalPrice = data.SalPrice,
                MRP = data.MRP,
                PurRate = data.PurRate,
                ProductionCost = data.ProductionCost,
                OpeningQuantity = data.OpeningQuantity,
                OpeningStbyQuantity = data.OpeningStbyQuantity,
                //BarCode = data.BarCode,
                //QRCode = data.QRCode,
                BatchNo = data.BatchNo,
                ExpiryDate = data.ExpiryDate == null ? "" : data.ExpiryDate.ToString(),
                MultipleUnits = data.MultipleUnits != null ? Common.xmlTostring(data.MultipleUnits.Select(a => new ProductModel.MultipleUnits { FK_Unit = a }).ToList()) : null

            }, companyKey: _userLoginInfo.CompanyKey);

            // Show OutPut Messages
            Output output = new Output();

            if (datresponse.Data.FirstOrDefault().Column1 > 0)
            {
                output.IsProcess = true;
                output.Message = new List<string> { "Updated Successfully" };

            }
            else
            {
                output.Message = new List<string> { datresponse.Data.FirstOrDefault().ErrMsg };
                output.code = datresponse.Data.FirstOrDefault().ErrCode;
                output.IsProcess = false;
            }

            return Json(new { Process = output }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Enquiry()
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.Companycategory = _userLoginInfo.CompCategory;

            return View();


        }
        public ActionResult LoadFormProductEnquiry()
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
            ProductModel.ProductEnquiry prdListObj = new ProductModel.ProductEnquiry();
            var Category = Common.GetDataViaQuery<ProductModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS FK_Category ,CatName AS Category",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode='P' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            prdListObj.CategoryList = Category.Data;

            return PartialView("_LoadProductEnquiry", prdListObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetProductList(ProductModel.GetProductEnquiry obj)
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
           
            ProductModel product = new ProductModel();
             var proDtls = product.GetProductEnquiryData(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProductEnquiry
             {
                 FK_Category = obj.FK_Category,
                 FK_Company = _userLoginInfo.FK_Company,
                 FK_Branch = _userLoginInfo.FK_Branch,
                 Offer=obj.Offer,
                 Name=obj.Name,
                 FK_Product=obj.FK_Product
             });
           
            return Json(new {proDtls}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetProductOffersList(ProductModel.GetProductOffer obj)
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

            ProductModel product = new ProductModel();
           
            var offerDtls = product.GetProductOfferData(companyKey: _userLoginInfo.CompanyKey, input: new ProductModel.GetProductOffer
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Category = obj.FK_Category,
                FK_Product = obj.FK_Product              
            });
            return Json(new { offerDtls }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadImages()
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
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;           
            return View();
        }
        public ActionResult LoadFormUploadImages()
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
            ProductModel.ProductImages prdListObj = new ProductModel.ProductImages();
            var Category = Common.GetDataViaQuery<ProductModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS FK_Category ,CatName AS Category",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode='P' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            prdListObj.CategoryList = Category.Data;

            return PartialView("_LoadProductImages", prdListObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult SaveImages(ProductModel.SaveImages objImage)
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
          
            List<ProductModel.ProductImageDetails> objImg = new List<ProductModel.ProductImageDetails>();
            foreach(var item in objImage.ImageList)
            {
                ProductModel.ProductImageDetails obj = new ProductModel.ProductImageDetails();
                obj.ImageData = item.ImageData.Split(';')[1].Replace("base64,", "");
                obj.FileName = item.FileName;
                obj.DefaultImage = item.DefaultImage;
                objImg.Add(obj);
            }
            ProductModel objProduct = new ProductModel();
            var dataresponse = objProduct.UpdateProductImagesData(input: new ProductModel.UpdateProductImages
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_Product = objImage.FK_Product,
                EntrBy = _userLoginInfo.EntrBy,
                ImageList = objImg is null ? "" : Common.xmlTostring(objImg)

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductDataWithImage(ProductModel.GetProductInfoImages data)
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
            ProductModel product = new ProductModel();

            var outputList = product.GetProductDataWithImage(companyKey: _userLoginInfo.CompanyKey, 
                input: new ProductModel.GetProductInfoImages {
                    FK_Product = data.FK_Product,
                    FK_Company = _userLoginInfo.FK_Company
                });
            if(outputList.Data!=null)
            {
                List<ProductModel.ProductInfoImages> objData = new List<ProductModel.ProductInfoImages>();
                foreach (var row in outputList.Data)
                {
                    ProductModel.ProductInfoImages lst = new ProductModel.ProductInfoImages();
                    lst.DefaultImage = row.DefaultImage;
                    lst.FileName = row.FileName;
                    lst.ImageData = "";
                    if (row.ImageData != "" && row.ImageData != null)
                    {
                        lst.ImageData = "data:image/;base64," + row.ImageData;
                    }
                    objData.Add(lst);
                }
                outputList.Data = objData;
            }            
            return Json(outputList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMultipleUnit(Int64 ID_Unit)
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

            var data = Common.GetDataViaQuery<ProductModel.MultiUnit>(parameters: new APIParameters
            {  
                TableName = "[Unit]",
                SelectFields = "[ID_Unit] AS FK_Unit,[UnitName] AS UnitName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company + "  AND ID_Unit <>" + ID_Unit,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}