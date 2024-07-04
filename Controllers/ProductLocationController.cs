using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class ProductLocationController : Controller
    {  
        public ActionResult Index(string mtd)
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;                  
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.mtd = mtd;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            return View();
        }

        public ActionResult loadProductLocationForm(string mtd)
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

            ProductLocationModel.ProductLocationList output = new ProductLocationModel.ProductLocationList();
            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
             companyKey: _userLoginInfo.CompanyKey,
             procedureName: "ProGetNextNo",
             parameter: new NextSortOrder
             {
                 TableName = "ProductLocation",
                 FieldName = "SortOrder",
                 Debug = 0
             });

            output.SortOrder = a.Data[0].NextNo;
            var OpBranchList = Common.GetDataViaQuery<ProductLocationModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch AS BranchID,BrName AS BranchName",
                Criteria = "cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
                    companyKey: _userLoginInfo.CompanyKey

         );
            output.BranchList = OpBranchList.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddProductLocation", output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProductLocation(ProductLocationModel.ProductLocationView data)
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


            ProductLocationModel objprdwise = new ProductLocationModel();


            byte userAction = 1;//update : 2 | Add : 1 

            //int branchCode = _userLoginInfo.FK_Branch;
            //int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;


            var dataresponse = objprdwise.UpdateProductionLoactionData(input: new ProductLocationModel.ProductionLocationUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_ProductLocation = data.FK_ProductLocation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_ProductLocation = 0,
                LocationName = data.LocationName,
                LocationShortName = data.LocationShortName,
                SortOrder = (data.SortOrder.HasValue) ? data.SortOrder.Value : 0,
               
                FK_Branch =data.BranchID




            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateProductLocation(ProductLocationModel.ProductLocationView data)
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

            ModelState.Remove("ID_ProductLocation");


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


            ProductLocationModel objprdwise = new ProductLocationModel();


            byte userAction = 2;//update : 2 | Add : 1 

            //int branchCode = _userLoginInfo.FK_Branch;
            //int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;

            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;

            var dataresponse = objprdwise.UpdateProductionLoactionData(input: new ProductLocationModel.ProductionLocationUpdate
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_ProductLocation = data.FK_ProductLocation,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = "",
                Debug = 0,
                ID_ProductLocation = data.ID_ProductLocation,
                LocationName = data.LocationName,
                LocationShortName = data.LocationShortName,
                SortOrder = (data.SortOrder.HasValue) ? data.SortOrder.Value : 0,

                FK_Branch = data.BranchID



            }, companyKey: _userLoginInfo.CompanyKey);



            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductLocationList(int pageSize, int pageIndex, string Name)
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
            ProductLocationModel objProductLocation = new ProductLocationModel();

            var outputList = objProductLocation.GetProductLocationData(companyKey: _userLoginInfo.CompanyKey, input: new ProductLocationModel.GetProductLocation
            {
                FK_ProductLocation = 0,
                FK_Company = _userLoginInfo.FK_Company,               
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,                
                Name = Name
            });

            return Json(new { outputList.Process, outputList.Data, pageSize, pageIndex, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetProductLocationInfo(ProductLocationModel.GetProductLocation data)
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

            ProductLocationModel ProductLocation = new ProductLocationModel();
            var productionlocationinfo = ProductLocation.GetProductLocationData(companyKey: _userLoginInfo.CompanyKey, input: new ProductLocationModel.GetProductLocation { FK_ProductLocation = data.FK_ProductLocation, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine,  EntrBy = _userLoginInfo.EntrBy });

            return Json(productionlocationinfo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteProductionLocationInfo(ProductLocationModel.ProductLocationView data)

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

            ModelState.Remove("FK_EMployee");
            //ModelState.Remove("PftTypeShortName");


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


            ProductLocationModel.DeleteProductionLocation ProductLocation = new ProductLocationModel.DeleteProductionLocation
            {
                FK_ProductLocation = data.ID_ProductLocation,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,

            };

            Output dataresponse = Common.UpdateTableData<ProductLocationModel.DeleteProductionLocation>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProProductLocationDelete", parameter: ProductLocation);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductionLocationReasonList()
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
    }
}