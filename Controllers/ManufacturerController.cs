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
using System.Web.UI.WebControls;
using PerfectWebERP.Filters;


namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ManufacturerController : Controller 
    {
        public ActionResult Manufacturer()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            return View();
        }
        public ActionResult LoadManufacturerForm()
        {


            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            ManufacturerModel.ManufacturerListModel ManufacturerList = new ManufacturerModel.ManufacturerListModel();
            var LeadType = System.Configuration.ConfigurationManager.AppSettings["Lead"];
            var leadmode = "";
            if (LeadType == "1")
            {
                leadmode = "  ID_ModuleType=4";
            }

            var Manufacturer = Common.GetDataViaQuery<ManufacturerModel.ModeList>(parameters: new APIParameters // Add Cash mode list Also..........
            {
                
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID, M.ModuleName as ModeName, M.Mode",                
                Criteria = ""+leadmode,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
             );


            var CountryList = Common.GetDataViaQuery<ManufacturerModel.Country>(parameters: new APIParameters
            {
                TableName = "[Country]",
                SelectFields = "[ID_Country] AS CountryID,[CntryName] AS CountryName",
                Criteria = "" ,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            var StateList = Common.GetDataViaQuery<ManufacturerModel.State>(parameters: new APIParameters
            {
                TableName = "[States]",
                SelectFields = "[ID_States] AS StateID,[StName] AS StateName",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey

            );
            var DistrictList = Common.GetDataViaQuery<ManufacturerModel.District>(parameters: new APIParameters
            {
                TableName = "[District]",
                SelectFields = "[ID_District] AS DistrictID,[DtName] AS DistrictName",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

            );

            var PostList = Common.GetDataViaQuery<ManufacturerModel.Post>(parameters: new APIParameters
            {
                TableName = "[Post]",
                SelectFields = "[ID_Post] AS PostID,[PostName] AS PostName",
                Criteria = "",
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey

            );

            ManufacturerList.ModeList = Manufacturer.Data;
            ManufacturerList.CountryList= CountryList.Data;
            ManufacturerList.StateList= StateList.Data;
            ManufacturerList.DistrictList = DistrictList.Data;
            ManufacturerList.PostList = PostList.Data;
           

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
              companyKey: _userLoginInfo.CompanyKey,
              procedureName: "ProGetNextNo",
              parameter: new NextSortOrder
              {
                  TableName = "Manufacturer",
                  FieldName = "SortOrder",
                  Debug = 0
              });

            ManufacturerList.SortOrder = a.Data[0].NextNo;

            return PartialView("_AddManufacturer", ManufacturerList);
        }

        [HttpPost]
      
        public ActionResult GetManufacturerList(int pageSize, int pageIndex, string Name)
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

            ManufacturerModel Manufacturer = new ManufacturerModel();
            var data = Manufacturer.GetManufacturerData(input: new ManufacturerModel.GetManufacturer
            {
                FK_Manufacturer = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
                

            },
                companyKey: _userLoginInfo.CompanyKey);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
           // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetManufacturerInfoByID(ManufacturerModel.ManufacturerInputFromViewModel data)
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

            ModelState.Remove("SortOrder");
            //ModelState.Remove("ManufacturerID");
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            ModelState.Remove("Post");
            ModelState.Remove("Address");
            ModelState.Remove("Active");
            ModelState.Remove("TotalCount");
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

            ManufacturerModel Manufacturer = new ManufacturerModel();
            var ManufacturerInfo = Manufacturer.GetManufacturerData(companyKey: _userLoginInfo.CompanyKey, input: new ManufacturerModel.GetManufacturer
            {
                FK_Manufacturer = data.ManufacturerID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 1,
                PageSize = 10,
                TransMode = ""
            });

            return Json(ManufacturerInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewManufacturerDetails(ManufacturerModel.ManufacturerInputFromViewModel data)
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


            ModelState.Remove("SortOrder");
            ModelState.Remove("ManufacturerID");
            ModelState.Remove("Country");
            ModelState.Remove("CountryID");
            ModelState.Remove("States");
            ModelState.Remove("StatesID");
            ModelState.Remove("District");
            ModelState.Remove("DistrictID");
            ModelState.Remove("Post");
            ModelState.Remove("PostID");
            ModelState.Remove("PlaceID");
            ModelState.Remove("Address");
            ModelState.Remove("Active");
            ModelState.Remove("TotalCount");

            #region :: Model validation  ::


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

            ManufacturerModel Manufacturer = new ManufacturerModel();


            byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string EntrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = Manufacturer.UpdateManufacturerData(input: new ManufacturerModel.UpdateManufacturer
            {

                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                BackupId = backupId,
                FK_Company = OrgnCode,
                EntrBy = EntrBy,
                ID_Manufacturer = 0,
                FK_Manufacturer = 0,
                FK_Branch = 0,
                ManufName = data.Manufacturer,
                ManufShortName = data.ShortName,
                SortOrder = data.SortOrder,
                Mode = data.Mode,                
                ManufGSTIN =data.GSTIN,                              
                ManufEmail = data.Email,
                ManufMobile = data.Mobile,
                ManufPContact = data.PMobile,
                ManufActive =data.Active,
                ManufPhone = data.Phone,
                FK_Country = data.CountryID,
                FK_District = data.DistrictID,
              //  FK_Post = data.PostID,
                FK_State = data.StatesID,
                //FK_ManufPin = data.Pin,
                ManufAddress = data.Address,
                ManufDescription = data.Description,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                TransMode=data.TransMode,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,




            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateManufacturerDetails(ManufacturerModel.ManufacturerInputFromViewModel data)
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
            ModelState.Remove("SortOrder");
            ModelState.Remove("ManufacturerID");
            ModelState.Remove("Country");
            ModelState.Remove("States");
            ModelState.Remove("District");
            ModelState.Remove("Post");
            ModelState.Remove("Address");
            ModelState.Remove("Active");
            ModelState.Remove("TotalCount");
            #region :: Model validation  ::



            //removing a node in model while validating
            ModelState.Remove("Interstate");
            ModelState.Remove("Intrastate");
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

            ManufacturerModel Manufacturer = new ManufacturerModel();


            byte userAction = 2;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string userCode = _userLoginInfo.EntrBy;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            int backupId = 0;

            var dataresponse = Manufacturer.UpdateManufacturerData(input: new ManufacturerModel.UpdateManufacturer
            {


                UserAction = userAction,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                BackupId = backupId,
                FK_Company = OrgnCode,
                EntrBy = entrBy,
                ID_Manufacturer = data.ManufacturerID,
                ManufName = data.Manufacturer,
                ManufShortName = data.ShortName,
                SortOrder = data.SortOrder,
                Mode = data.Mode,
                FK_Manufacturer = data.ManufacturerID,
                FK_Branch = 0,
                ManufGSTIN = data.GSTIN,
                FK_Country = data.CountryID,
                FK_District = data.DistrictID,
//FK_Post = data.PostID,
                FK_State = data.StatesID,
                ManufAddress = data.Address,
                ManufDescription = data.Description,
                ManufEmail = data.Email,
                ManufMobile = data.Mobile,
                ManufPContact = data.PMobile,
                ManufActive = data.Active,
                //FK_ManufPin = data.Pin,
                ManufPhone = data.Phone,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                TransMode = data.TransMode,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteManufacturerInfo(ManufacturerModel.ManufacturerInfoView data)
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

            ManufacturerModel Manufacturer = new ManufacturerModel();
            var datresponse = Manufacturer.DeleteManufacturerData(input: new ManufacturerModel.DeleteManufacturer
            {
                FK_Manufacturer = data.ManufacturerID,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = data.ReasonID,
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            },
            companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetManufacturerReasonList()
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

        public ActionResult FillDetailsByGSTNo(string GSTIN)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ManufacturerModel model = new ManufacturerModel();


            var data = model.GetManufacturerByGST(input:
             new ManufacturerModel.ManufacturerGSTINView
             {
                 FK_Company = _userLoginInfo.FK_Company,
                 GSTIN = GSTIN

             },

              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);


        }
    }
}