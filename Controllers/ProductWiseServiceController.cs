using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ProductWiseServiceController : Controller
    {
        //GET: ProductWiseService
        public ActionResult ProductWiseService()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadFormProductWise()
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
            ProductionWiseServiceModel.PrdwiseViewList prdListObj = new ProductionWiseServiceModel.PrdwiseViewList();

            var prdwiseServlist = Common.GetDataViaQuery<ProductionWiseServiceModel.Services>(parameters: new APIParameters
            {
                TableName = "Services",
                SelectFields = "ID_Services AS ServiceID,ServicesName AS ServiceList",
                Criteria = "cancelled=0 AND Passed=1 and FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey );

            prdListObj.ServiceList = prdwiseServlist.Data;

            ProductionWiseServiceModel objPrdSer = new ProductionWiseServiceModel();
          
            var PerdtypList = objPrdSer.GetPerdTypList(input: new ProductionWiseServiceModel.ModeLead { Mode = 3 }, 
                companyKey: _userLoginInfo.CompanyKey);

            prdListObj.PerdList = PerdtypList.Data;

            var Category = Common.GetDataViaQuery<ProductionWiseServiceModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS CategoryID ,CatName AS Category",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey

              );
            prdListObj.CategoryList = Category.Data;
            APIParameters apiParametaxgroup = new APIParameters
            {
                TableName = "[TaxGroup]",
                SelectFields = "[ID_TaxGroup] AS TaxGroupID,[TGName] AS TaxGroupName",
                Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var taxgroup = Common.GetDataViaQuery<ProductionWiseServiceModel.TaxGroup>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParametaxgroup);

            prdListObj.TaxgroupList = taxgroup.Data;

            return PartialView("_AddPrdWiseService", prdListObj);
        }



        [HttpPost]
        public ActionResult GetProductWiseList(int pageSize, int pageIndex,string Name)
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

            ProductionWiseServiceModel objPrdServ = new ProductionWiseServiceModel();
            var data = objPrdServ.GetPrdwiseData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionWiseServiceModel.ProductwiseID
            {
                
                FK_ProductWiseServices = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name=Name,
                Details=0,
                TransMode = transMode
               
            });

           // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

     
        //public ActionResult GetPrdList()
        //{
        //    #region ::  Check User Session to verifyLogin  ::

        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion ::  Check User Session to verifyLogin  ::

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    var data = Common.GetDataViaQuery<ProductionWiseServiceModel.Products>(parameters: new APIParameters
        //    {
        //        TableName = "Product",
        //        SelectFields = " ID_Product as PrdID,ProdName  as Product",
        //        Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
        //        SortFields = "",
        //        GroupByFileds = ""
        //    },
        //     companyKey: _userLoginInfo.CompanyKey
        //    );

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProductWiseService(ProductionWiseServiceModel.ProductionwiseView data)
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

            ModelState.Remove("PrdID");               
            ModelState.Remove("ServiceID");
            ModelState.Remove("ID_Mode");
            ModelState.Remove("EffectDate");
            ModelState.Remove("PeriofFrom");
            ModelState.Remove("PeriodTo");
            ModelState.Remove("ServiceCost");
            ModelState.Remove("SubProductID");
            ModelState.Remove("SubproductServiceDetails");
           

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


            ProductionWiseServiceModel objprdwise = new ProductionWiseServiceModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            ///string userCode = _userLoginInfo.Us;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdatePrdwiseData(input: new ProductionWiseServiceModel.ProductionwiseUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,
                EffectDate=data.EffectDate,
                ID_ProductWiseServices = 0,              
                 FK_Product = data.PrdID,
                 FK_Category=data.CategoryID,
                FK_TaxGroup = (data.TaxGroupID.HasValue) ? data.TaxGroupID.Value : 0,
                IncludeTaxOnServiceCharge = data.IncludeTax,
                SubproductServiceDetails = data.SubproductServiceDetails is null ? "" : Common.xmlTostring(data.SubproductServiceDetails),
                
               
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
      
        public ActionResult GetProductWiseServiceInfo(ProductionWiseServiceModel.ProductwiseID data)
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
            ProductionWiseServiceModel objprd = new ProductionWiseServiceModel();

            var mptableInfo = objprd.GetPrdwiseData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionWiseServiceModel.ProductwiseID
            {
                FK_ProductWiseServices = data.FK_ProductWiseServices,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Details = 0,
                EntrBy = _userLoginInfo.EntrBy
            });
        
            var subtable = objprd.GetSubProductwiseServiceData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionWiseServiceModel.SubproductwiseSubSelect { FK_ProductWiseServices = data.FK_ProductWiseServices, Details=1, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].SubproductServiceDetails = subtable.Data;
            }

            return Json(mptableInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPrdWiseDeleteReasonList()
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
        //delete productwise type
        public ActionResult DeletePrdWiselist(ProductionWiseServiceModel.PrdWiseRsnView data)
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


           
            ModelState.Remove("PrdID");
            ModelState.Remove("ServiceID");
            ModelState.Remove("ID_Mode");
            ModelState.Remove("EffectDate");
            ModelState.Remove("PeriofFrom");
            ModelState.Remove("PeriodTo");
            ModelState.Remove("ServiceCost");
            ModelState.Remove("SubProductID");
            ModelState.Remove("SubproductServiceDetails");
            #region :: Model validation  ::

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

            ProductionWiseServiceModel.ProductionwiseDelete objprddel = new ProductionWiseServiceModel.ProductionwiseDelete
            {
                FK_ProductWiseServices = data.ID_ProductWiseServices,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<ProductionWiseServiceModel.ProductionwiseDelete>(
                companyKey: _userLoginInfo.CompanyKey, procedureName: "ProProductWiseServicesDelete", parameter: objprddel);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

    }
}