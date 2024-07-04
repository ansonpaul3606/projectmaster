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
using PerfectWebERP.Filters;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class LocalSupplierController : Controller
    {
        // GET: LocalSupplier
        public ActionResult Index()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            return View();
        }

        public ActionResult LoadLocalSupplierForm()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            
            return PartialView("_AddLocalSupplier");
        }

        public ActionResult FillDetailsByGSTNo(string GSTINNo)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LocalSupplierModel model = new LocalSupplierModel();


            var data = model.GetSupplierByGST(input:
             new LocalSupplierModel.SupplierGSTINView
             {
                 FK_Company = _userLoginInfo.FK_Company,
                 GSTINNo = GSTINNo

             },

              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaveLocalSupplier(LocalSupplierModel.SupplierView data)
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

            LocalSupplierModel Supplier = new LocalSupplierModel();

            var datresponse = Supplier.UpdateLocalSupplierData(input: new LocalSupplierModel.UpdateSupplier
            {
                UserAction = 1,
                SuppName = data.Name,
               SuppContactPerson = data.ContactPerson,
                SuppMobile = data.Mobile,
               SuppGSTINNo = data.GSTINNo,
                IsSupplier=0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_Supplier = 0,
                FK_Supplier = 0,
                TransMode=data.TransMode,
                ProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateLocalSupplier(LocalSupplierModel.SupplierView data)
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

            LocalSupplierModel Supplier = new LocalSupplierModel();

            var datresponse = Supplier.UpdateLocalSupplierData(input: new LocalSupplierModel.UpdateSupplier
            {
                UserAction = 2,
                SuppName = data.Name,
                SuppContactPerson = data.ContactPerson,
                SuppMobile = data.Mobile,
                SuppGSTINNo = data.GSTINNo,
                IsSupplier = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_Supplier = data.SupplierID,
                FK_Supplier = data.SupplierID,
                TransMode = data.TransMode,
                ProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetLocalSupplierList(int pageSize, int pageIndex, string Name,string TransMode)
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

            LocalSupplierModel supplier = new LocalSupplierModel();

           
            var data = supplier.GetSupplierData(input: new LocalSupplierModel.SupplierID
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Supplier = 0,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                TransMode = TransMode,
                Name=Name
            }, companyKey: _userLoginInfo.CompanyKey);

            // return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LocalSupplierToSupplierUpdate(LocalSupplierModel.LocalSupplierView data)
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

            LocalSupplierModel Supplierupdate = new LocalSupplierModel();

            var datresponse = Supplierupdate.UpdateLocalSupplierToSupplier(input: new LocalSupplierModel.UpdateLocalSupplier
            {
                //UserAction = 1,
                //Name = data.Name,
                //ContactPerson = data.ContactPerson,
                //Mobile = data.Mobile,
                //GSTINNo = data.GSTINNo,
                IsSupplier = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Supplier = data.FK_Supplier,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        
       public ActionResult GetLocalSupplierInfo(LocalSupplierModel.SupplierView data)
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

            LocalSupplierModel supplierdata = new LocalSupplierModel();
            var supplierInfo = supplierdata.GetSupplierData(companyKey: _userLoginInfo.CompanyKey, input: new LocalSupplierModel.SupplierID
            {
                FK_Supplier = data.SupplierID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var Productinfo = supplierdata.GetLocalSupplierProductDetailsSelect(companyKey: _userLoginInfo.CompanyKey, input: new LocalSupplierModel.LocalSupplierItemDetails
            {
                FK_Supplier = data.SupplierID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(new { supplierInfo, Productinfo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocalSupplierReasonList()
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


        public ActionResult DeleteLocalSupplierInfo(LocalSupplierModel.DeleteLocalSupplier data)

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


            LocalSupplierModel.DeleteLocalSupplier localsupplierdelete = new LocalSupplierModel.DeleteLocalSupplier
            {

                TransMode = "",
                FK_Supplier = data.FK_Supplier,
                Debug = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason = data.FK_Reason,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser

            };

            Output dataresponse = Common.UpdateTableData<LocalSupplierModel.DeleteLocalSupplier>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProLocalSupplierDelete", parameter: localsupplierdelete);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
    }
}