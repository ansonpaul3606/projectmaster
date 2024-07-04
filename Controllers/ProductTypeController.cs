using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ProductTypeController : Controller
    {
        // GET: ProductPrice
        public ActionResult Index()
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
            return View();
        }
        public ActionResult LoadProductType()
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
            ProductTypeModel.ProductTypeView prdListObj = new ProductTypeModel.ProductTypeView();
            var Customertype = Common.GetDataViaQuery<ProductTypeModel.CustomertypeList>(parameters: new APIParameters

            {
                TableName = "CustomerType",
                SelectFields = "ID_CustomerType AS FK_Master,CustyName as Customertype",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                   companyKey: _userLoginInfo.CompanyKey
                   );
              prdListObj.CustomertypeList = Customertype.Data;

            return PartialView("_AddProductType", prdListObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProductType(ProductTypeModel.ProductTypeView data)
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

            ModelState.Remove("ID_PriceFixingType");


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


            ProductTypeModel objprdwise = new ProductTypeModel();


            byte userAction = 1;//update : 2 | Add : 1 

            //int branchCode = _userLoginInfo.FK_Branch;
            //int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateProducttypeData(input: new ProductTypeModel.ProducttypeUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_PriceFixingType = 0,
                PftTypeName = data.PftTypeName,
                PftTypeShortName = data.PftTypeShortName,
                PftMasterType = data.PftMasterType,
                FK_SubMaster = data.FK_SubMaster,
                

                FK_Master = (data.FK_Master.HasValue) ? data.FK_Master.Value : 0,
                FK_PriceFixingType = data.FK_PriceFixingType,
              

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetCustomerList(long FK_Master)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<ProductTypeModel.CustomerList>(parameters: new APIParameters

            {
                TableName = "Customer",
                SelectFields = "ID_Customer AS FK_SubMaster,CusName as Customerlist",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_CustomerType='" + FK_Master + "'" + "AND FK_Company =" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult UpdateProductType(ProductTypeModel.ProductTypeView data)
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

            ModelState.Remove("ID_PriceFixingType");


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


            ProductTypeModel objprdwise = new ProductTypeModel();


            byte userAction = 2;//update : 2 | Add : 1 

            //int branchCode = _userLoginInfo.FK_Branch;
            //int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;

            var dataresponse = objprdwise.UpdateProducttypeData(input: new ProductTypeModel.ProducttypeUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_PriceFixingType =data.ID_PriceFixingType,
                PftTypeName = data.PftTypeName,
                PftTypeShortName = data.PftTypeShortName,
                PftMasterType = data.PftMasterType,
                FK_PriceFixingType = data.FK_PriceFixingType,
                FK_Master = (data.FK_Master.HasValue) ? data.FK_Master.Value : 0,
                FK_SubMaster = data.FK_SubMaster



            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPriceTypeList(int pageSize, int pageIndex, string Name)
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
            ProductTypeModel PriceType = new ProductTypeModel();

            var data = PriceType.GetPriceTypeData(companyKey: _userLoginInfo.CompanyKey, input: new ProductTypeModel.PriceType
            {
                FK_PriceFixingType = 0,
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


        public ActionResult GetPriceTypeReasonList()
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
        public ActionResult DeletePriceTypeInfo(ProductTypeModel.PriceTypeInputView data)

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

            ModelState.Remove("PftTypeName");
            ModelState.Remove("PftTypeShortName");
          

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


            ProductTypeModel.DeletePriceType PriceType = new ProductTypeModel.DeletePriceType
            {
                FK_PriceFixingType = data.ID_PriceFixingType,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""

            };

            Output dataresponse = Common.UpdateTableData<ProductTypeModel.DeletePriceType>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProPriceFixingTypeDelete", parameter: PriceType);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPriceTypeInfo(ProductTypeModel.PriceType data)
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

            ProductTypeModel PriceType = new ProductTypeModel();
            var pricetypeInfo = PriceType.GetPriceTypeData(companyKey: _userLoginInfo.CompanyKey, input: new ProductTypeModel.PriceType { FK_PriceFixingType = data.FK_PriceFixingType, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, EntrBy = _userLoginInfo.EntrBy });

            return Json(pricetypeInfo, JsonRequestBehavior.AllowGet);
        }


    }
}