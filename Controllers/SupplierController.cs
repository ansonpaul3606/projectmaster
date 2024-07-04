/*----------------------------------------------------------------------
Created By	: Jisi Rajan
Created On	: 29/01/2022
Purpose		: Supplier
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

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class SupplierController : Controller
    {
        public ActionResult Index()
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

        public ActionResult LoadSupplierForm()
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
            ViewBag.PagedAccessRights = pageAccess;

            SupplierModel.SupplierListModel SupplierList = new SupplierModel.SupplierListModel();

            var SuppList = Common.GetDataViaQuery<SupplierModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeName,M.Mode",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );

            SupplierList.ModeList = SuppList.Data;
            var Transtypelist = Common.GetDataViaQuery<SupplierModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType<>0",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            },
        companyKey: _userLoginInfo.CompanyKey
       );
            SupplierList.TransTypeList = Transtypelist.Data;
            var GoodsTranslist = Common.GetDataViaQuery<SupplierModel.GoodstransList>(parameters: new APIParameters
            {
                TableName = "GoodsTransferMethod G",
                SelectFields = "G.ID_GoodsTransferMethod as GoodsTransID,G.GoodsTransName as GoodsTransName",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey

           );

            SupplierList.GoodstransList = GoodsTranslist.Data;

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "Supplier",
                FieldName = "SortOrder",
                Debug = 0
            });


         
            var SupplierTypeName = Common.GetDataViaQuery<SupplierModel.SupplierTypeNameList>(parameters: new APIParameters
            {
                TableName = "SupplierType ST",
                SelectFields = "ST.ID_SupplierType AS ID_SupplierType,ST.STName AS[STName]",
                Criteria = "ST.Cancelled=0 AND ST.Passed=1 AND ST.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey
          );

            SupplierList.SupplierType = SupplierTypeName.Data;

            SupplierList.SortOrder = a.Data[0].NextNo;

            SupplierModel model = new SupplierModel();
            var SupNo = model.GetSupplierNo(input: new SupplierModel.inputGetSupplierNo { FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);

            SupplierList.SupNumber = SupNo.Data[0].SupplierNumber;

            var branchdata = model.GetBranchHead(input: new SupplierModel.InputBranch
            {
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);

            SupplierList.branchHeads = branchdata.Data;


            return PartialView("_AddSupplierForm", SupplierList);
            //return PartialView("_AddSupplierForm", a.Data[0].NextNo);
        }

        [HttpPost]
        public ActionResult GetSupplierList(int pageSize, int pageIndex, string Name,string Transmode)
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

            SupplierModel supplier = new SupplierModel();
            string transMode = "";
            var data = supplier.GetSupplierData(input: new SupplierModel.SupplierID
            {
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Supplier = 0,
                PageIndex = pageIndex+1,
                PageSize = pageSize,
                Name = Name,
                TransMode = Transmode,
            }, companyKey: _userLoginInfo.CompanyKey);

           // return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSupplierInfoByID(SupplierModel.SupplierIDView data)
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

            SupplierModel supplier = new SupplierModel();
            var supplierInfo = supplier.GetSupplierData(companyKey: _userLoginInfo.CompanyKey, input: new SupplierModel.SupplierID
            {
                TransMode = "",
                PageSize = 0,
                PageIndex = 0,
                FK_Supplier = data.SupplierID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine

            });


            return Json(supplierInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewSupplier(SupplierModel.SupplierView data)
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
            ModelState.Remove("SupplierID");
            ModelState.Remove("SlNo");
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            ModelState.Remove("Area");
            ModelState.Remove("Post");
            ModelState.Remove("Place");
            ModelState.Remove("SortOrder");
            if (data.Active)
            {
                ModelState.Remove("Descriptions");
            }
          

           

            SupplierModel Supplier = new SupplierModel();

            var datresponse = Supplier.UpdateSupplierData(input: new SupplierModel.UpdateSupplier
            {
                UserAction = 1,
                SuppName = data.Name,
                // FK_SupplierCategory = data.SupplierCategory,
                SuppContactPerson = data.ContactPerson,
                SuppAddress = data.Address,
                SuppPhone = data.Phone,
                SuppMobile = data.Mobile,
                SuppEmail = data.Email,
                SuppFax = data.Fax,
                SuppGSTINNo = data.GSTINNo,
                //SuppKGSTNo = data.KGSTNo,
                //SuppCSTNo = data.CSTNo,
                Active = data.Active,
                Descriptions = (data.Active) ? null : data.Descriptions,
                Mode = data.Mode,
                SortOrder = data.SortOrder,
                FK_Country = data.CountryID,
                FK_States = data.StatesID,
                FK_District = data.DistrictID,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
              //  FK_Post = data.PostID,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                // FK_Place = data.PlaceID,
                BankName = data.BankName,
                BranchName = data.BranchName,
                AccountHolderName = data.AccountHolderName,
                BankAccount = data.BankAccount,
                IFSCCode = data.IFSCCode,
                AadharCard = data.AadharCard,
                //ConfirmBankAccount = data.ConfirmBankAccount,
                PanNo = data.PanNo,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_Supplier = 0,
                SupNumber = data.Number.ToString(),
                FK_Supplier =0,
                GoodsTransID= (data.GoodsTransID.HasValue) ? data.GoodsTransID.Value : 0,
                TransMode ="",
                FK_SupplierType = data.ID_SupplierType,
                FK_Branch = data.FK_Branch,
                SuppShortname=data.ShortName,
                OpeningBalance= data.OpeningBalance,
                FK_TransType= data.FK_TransType,
                OpeningBalDate = data.OpeningBalDate == null ? "" : data.OpeningBalDate.ToString(),
                 FK_AccountHead = (data.AccountHeadID.HasValue) ? data.AccountHeadID.Value : 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateSupplierDetails(SupplierModel.SupplierView data)
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
            ModelState.Remove("SlNo");
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            
            ModelState.Remove("Area");
            ModelState.Remove("Post");
            ModelState.Remove("Place");
            ModelState.Remove("SortOrder");
            if (data.Active)
            {
                ModelState.Remove("Descriptions");
            }
            
            #region :: Model validation  ::
            //removing a node in model while validating
            //--- Model validation 
          //  if (!ModelState.IsValid)
           // {

                // since no need to continue just return
               // return Json(new
               // {
                 //   Process = new Output
                  //  {
                   //     IsProcess = false,
                   //     Message = ModelState.Values.SelectMany(m => m.Errors)
                                       // .Select(e => e.ErrorMessage)
                                      //  .ToList(),
                       // Status = "Validation failed",
                   // }
               // }, JsonRequestBehavior.AllowGet);
           // }

            #endregion :: Model validation  ::


            SupplierModel Supplier = new SupplierModel();
            var datresponse = Supplier.UpdateSupplierData(input: new SupplierModel.UpdateSupplier
            {
                UserAction = 2,
                TransMode="",
                FK_Supplier=data.SupplierID,
                ID_Supplier=data.SupplierID,
                SuppName = data.Name,
                //FK_SupplierCategory = data.SupplierCategory,
                SuppContactPerson = data.ContactPerson,
                SuppAddress = data.Address,
                SuppPhone = data.Phone,
                SuppMobile = data.Mobile,
                SuppEmail = data.Email,
                SuppFax = data.Fax,
                SuppGSTINNo = data.GSTINNo,
                //SuppKGSTNo = data.KGSTNo,
                //SuppCSTNo = data.CSTNo,
                Active = data.Active,
                Descriptions = (data.Active)?null:data.Descriptions,
                Mode = data.Mode,
                SortOrder = data.SortOrder,
                FK_Country = data.CountryID,
                FK_States = data.StatesID,
                FK_District = data.DistrictID,
               // FK_Area = data.AreaID,
              //  FK_Post = data.PostID,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                BankName = data.BankName,
                BranchName = data.BranchName,
                AccountHolderName = data.AccountHolderName,
                BankAccount = data.BankAccount,
                IFSCCode = data.IFSCCode,
                AadharCard = data.AadharCard,
                //ConfirmBankAccount = data.ConfirmBankAccount,
                PanNo = data.PanNo,
                SupNumber = data.Number.ToString(),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                GoodsTransID = (data.GoodsTransID.HasValue) ? data.GoodsTransID.Value : 0,
                FK_SupplierType = data.ID_SupplierType,
                FK_Branch = data.FK_Branch,
                SuppShortname = data.ShortName,
                OpeningBalance = data.OpeningBalance,
                FK_TransType = data.FK_TransType,
                OpeningBalDate = data.OpeningBalDate == null ? "" : data.OpeningBalDate.ToString(),
                FK_AccountHead = (data.AccountHeadID.HasValue) ? data.AccountHeadID.Value : 0
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteSupplierInfo(SupplierModel.SupplierIDView data)
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

            SupplierModel Supplier = new SupplierModel();

            var datresponse = Supplier.DeleteSupplierData(input: new SupplierModel.DeleteSupplier {
                EntrBy = _userLoginInfo.EntrBy,
                FK_Supplier = data.SupplierID,
                TransMode = "",
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason=data.ReasonID


            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSupplierdDeleteReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy,FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,FK_Machine=_userLoginInfo.FK_Machine,PageIndex=0,PageSize=0,TransMode="" });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken()]
        public ActionResult GetPinCodedetails(string Pincode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            SupplierModel dataModel= new SupplierModel();
            var outputData = dataModel.GetDetailsByPincodeData(input:new SupplierModel.InputPincode {FK_Company=_userLoginInfo.FK_Company,Pincode= Pincode },companyKey:_userLoginInfo.CompanyKey);
            return Json(outputData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCountryList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<SupplierModel.Countrylist>(parameters: new APIParameters
            {
                TableName = "Country",
                SelectFields = " CntryName AS Country,ID_Country AS CountryID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }
            public ActionResult GetStateList(Int64 countryID)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            StateModel dataModel = new StateModel();
            var outputData = dataModel.GetStateData(input: new StateModel.GetState
            {
                FK_States =0,
                EntrBy =_userLoginInfo.EntrBy,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_Company=_userLoginInfo.FK_Company,
                FK_Country= countryID,
                FK_Machine=_userLoginInfo.FK_Machine,
                PageIndex=0,
                PageSize=0,
                TransMode=""
                
             },companyKey:_userLoginInfo.CompanyKey);
            return Json(outputData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictList(Int64 statesid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<SupplierModel.Districtlist>(parameters: new APIParameters
            {
                TableName = "District",
                SelectFields = " DtName AS District,ID_District AS DistrictID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_States='" + statesid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAreaList(Int64 districtid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<SupplierModel.Arealist>(parameters: new APIParameters
            {
                TableName = "Area",
                SelectFields = " AreaName AS Area,ID_Area AS AreaID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District='" + districtid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },

          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPlaceList(Int64 districtid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<SupplierModel.Placelist>(parameters: new APIParameters
            {

                TableName = "Place",
                SelectFields = " PlcName AS Place,ID_Place AS PlaceID",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District='" + districtid + "' AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetPostList(SupplierModel.InputPlace datas)
        {
           
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<SupplierModel.Postlist>(parameters: new APIParameters
            {
                TableName = "Post",
                SelectFields = "PostName AS Post,ID_Post AS PostID,PinCode AS PinCode",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District=" + datas.DistrictID + " AND FK_Place=" + datas.PlaceID + " AND FK_Company= " + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult FillDetailsByGSTNo(string GSTINNo)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            SupplierModel model = new SupplierModel();


            var data = model.GetSupplierByGST(input: 
             new SupplierModel.SupplierGSTINView
            {
                 FK_Company=_userLoginInfo.FK_Company,
                 GSTINNo= GSTINNo

             },

              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);


        }


        public ActionResult GetSupplierDeleteReasonList()
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


