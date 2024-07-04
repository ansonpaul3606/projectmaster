
/*----------------------------------------------------------------------
Created By	: Amritha A K
Created On	: 29/01/2022
Purpose		: Branch
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
using static PerfectWebERP.Models.CommonSearchPopupModel;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class BranchController : Controller
    {
        public ActionResult Index(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return View();
        }


        public ActionResult LoadFormBranch(string mtd)
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
            var branchlist =Common.GetDataViaQuery<BranchModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch B JOIN BranchType BT ON BT.ID_BranchType = B.FK_BranchType",
                SelectFields = "ID_Branch AS BranchParentID,BrName AS BranchParent",
                Criteria = "B.cancelled=0 AND B.Passed=1 AND B.FK_Company=" + _userLoginInfo.FK_Company+ "AND BT.FK_BranchMode <> 6",               
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );
            var branchtypelist = Common.GetDataViaQuery<BranchModel.BranchTypes>(parameters: new APIParameters
            {
                TableName = "BranchType",
                SelectFields = "ID_BranchType AS BranchTypeIDs,BTName AS BranchTypess,FK_BranchMode AS BranchMode",
                Criteria = "cancelled=0 AND Passed=1 and FK_Company="+_userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            BranchModel.BranchViewList branchViewList = new BranchModel.BranchViewList
            {
                BranchList = branchlist.Data,
                BranchTypelists = branchtypelist.Data
            };


            //BranchModel.BranchView sortno = new BranchModel.BranchView();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "Branch",
                FieldName = "SortOrder",
                Debug = 0
            });

            branchViewList.SortOrder = a.Data[0].NextNo;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddBranchForm", branchViewList);
        }

        public ActionResult GetEmployeeList()
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

            var data = Common.GetDataViaQuery<BranchModel.Employees>(parameters: new APIParameters
            {
                TableName = "Employee",
                SelectFields = "ID_Employee AS EmployeeID,Concat(EmpFName,EmpLName) AS Employee",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
             companyKey: _userLoginInfo.CompanyKey
            );

            return Json(data, JsonRequestBehavior.AllowGet);



        }


        [HttpPost]
        public ActionResult GetBranchList(int pageSize, int pageIndex, string Name)
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

            string transMode = "";
            ModelState.Remove("ReasonID");

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
            BranchModel Branch = new BranchModel();

            var data = Branch.GetBranchData(companyKey: _userLoginInfo.CompanyKey, input: new BranchModel.BranchID { ID_Branch = 0, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy ,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });
           

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
          //  return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewBranch(BranchModel.BranchView data)
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

            ModelState.Remove("BranchID");
            ModelState.Remove("Country");
           // ModelState.Remove("CountryID");
            ModelState.Remove("States");
           // ModelState.Remove("StatesID");
            ModelState.Remove("District");
          //  ModelState.Remove("DistrictID");
            // ModelState.Remove("AreaID");
            // ModelState.Remove("Area");
            //ModelState.Remove("PlaceID");
            ModelState.Remove("Place");
           // ModelState.Remove("PostID");
            ModelState.Remove("Post");

            ModelState.Remove("EmployeeID");
            ModelState.Remove("BranchParentID");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
            ModelState.Remove("Address1");
            ModelState.Remove("Address2");
            ModelState.Remove("CashPositionLimit");
            ModelState.Remove("CashReOrderLevel");
            ModelState.Remove("Latitude");
            ModelState.Remove("Longitude");
            ModelState.Remove("Phone");
            ModelState.Remove("GSTNo");

            ModelState.Remove("StartTime");
            ModelState.Remove("EndTime");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ContactPerson");
            ModelState.Remove("ImageList");
            ModelState.Remove("FK_IntroduceBranch");
            
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

            BranchModel Branch = new BranchModel();


            string image = (string)Session["CompanyImage"];
            var datresponse = Branch.UpdateBranchData(input: new BranchModel.UpdateBranch
            {
                UserAction = 1,
                ID_Branch = 0,
                BrCode = data.Code,
                BrName = data.Name,
                BrShortName = data.ShortName,
                BrAddress1 = data.Address1,
                BrAddress2 = data.Address2,
                BrPhone = data.Phone,
                GSTNo = data.GSTNo,
                BrMobile = data.Mobile,
                BrEmail = data.Email,
                BrCashPositionLimit = data.CashPositionLimit,
                BrCashReOrderLevel = data.CashReOrderLevel,
                BrLatitude = data.Latitude,
                BrLongitude = data.Longitude,
                BrStartTime = data.StartTime,
                BrEndTime = data.EndTime,
                SortOrder = data.SortOrder,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_District = data.DistrictID,
                FK_States = data.StatesID,
                FK_Country = data.CountryID,
                TransMode=data.TransMode,
                FK_Employee = (data.EmployeeID.HasValue)?data.EmployeeID.Value:0,
                FK_BranchParent =(data.BranchParentID.HasValue)?data.BranchParentID.Value:0,
                FK_BranchType = data.BranchTypeIDs,
                FK_AccountHead = (data.AccountHead.HasValue)?data.AccountHead.Value:0,
                FK_AccountSubHead = (data.AccountHeadSub.HasValue)?data.AccountHeadSub.Value:0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                BrContactPerson=data.ContactPerson,
                ImageList = image,
                BranchBankDetails = data.BranchBankDetails is null ? "" : Common.xmlTostring(data.BranchBankDetails),
                FK_IntroduceBranch = data.FK_IntroduceBranch,
                BranchMode = data.BranchMode,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateBranch(BranchModel.BranchView data)
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

            
            ModelState.Remove("EmployeeID");
            ModelState.Remove("Country");
            ModelState.Remove("CountryID");
            ModelState.Remove("States");
            ModelState.Remove("StatesID");
            ModelState.Remove("District");
            ModelState.Remove("DistrictID");
            ModelState.Remove("AreaID");
            ModelState.Remove("Area");
            ModelState.Remove("PlaceID");
            ModelState.Remove("Place");
            ModelState.Remove("PostID");
            ModelState.Remove("Post");
            ModelState.Remove("Address1");
            ModelState.Remove("Address2");
            ModelState.Remove("BranchParentID");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
            ModelState.Remove("StartTime");
            ModelState.Remove("EndTime");
            ModelState.Remove("CashPositionLimit");
            ModelState.Remove("CashReOrderLevel");
            ModelState.Remove("Latitude");
            ModelState.Remove("Longitude");
            ModelState.Remove("Phone");
            ModelState.Remove("GSTNo");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ContactPerson");
            ModelState.Remove("ImageList");
            ModelState.Remove("FK_IntroduceBranch");
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
            string image = (string)Session["CompanyImage"];
            BranchModel Branch = new BranchModel();
            var datresponse = Branch.UpdateBranchData(input: new BranchModel.UpdateBranch
            {
                UserAction = 2,
                ID_Branch =data.BranchID,
                BrCode = data.Code,
                BrName = data.Name,
                BrShortName = data.ShortName,
                BrAddress1 = data.Address1,
                BrAddress2 = data.Address2,
                BrPhone = data.Phone,
                GSTNo= data.GSTNo,
                BrMobile = data.Mobile,
                BrEmail = data.Email,
                BrCashPositionLimit = data.CashPositionLimit,
                BrCashReOrderLevel = data.CashReOrderLevel,
                BrLatitude = data.Latitude,
                BrLongitude = data.Longitude,
                BrStartTime = data.StartTime,
                BrEndTime = data.EndTime,
                SortOrder = data.SortOrder,
                TransMode = data.TransMode,
                FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                FK_District = data.DistrictID,
                FK_States = data.StatesID,
                FK_Country = data.CountryID,
                FK_Employee = (data.EmployeeID.HasValue) ? data.EmployeeID.Value : 0,
                FK_BranchParent = (data.BranchParentID.HasValue) ? data.BranchParentID.Value : 0,
                FK_BranchType = data.BranchTypeIDs,
                FK_AccountHead = (data.AccountHead.HasValue) ? data.AccountHead.Value : 0,
                FK_AccountSubHead = (data.AccountHeadSub.HasValue) ? data.AccountHeadSub.Value : 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                BrContactPerson = data.ContactPerson,
                ImageList = image,
                 BranchBankDetails = data.BranchBankDetails is null ? "" : Common.xmlTostring(data.BranchBankDetails),
                FK_IntroduceBranch=data.FK_IntroduceBranch,
                BranchMode = data.BranchMode,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBranchInfoByID(BranchModel.BranchView data)
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

           
            ModelState.Remove("Country");
            ModelState.Remove("CountryID");
            ModelState.Remove("States");
            ModelState.Remove("StatesID");
            ModelState.Remove("District");
            ModelState.Remove("DistrictID");
            ModelState.Remove("AreaID");
            ModelState.Remove("Area");
            ModelState.Remove("PlaceID");
            ModelState.Remove("Place");
            ModelState.Remove("PostID");
            ModelState.Remove("Post");
            ModelState.Remove("Code");
            ModelState.Remove("Name");
            ModelState.Remove("ShortName"); 
            ModelState.Remove("Address1");
            ModelState.Remove("Address2");
            ModelState.Remove("Mobile");
            ModelState.Remove("Phone");
            ModelState.Remove("GSTNo");
            ModelState.Remove("BranchTypeIDs");
            ModelState.Remove("CashPositionLimit");
            ModelState.Remove("CashReOrderLevel");
            ModelState.Remove("Latitude");
            ModelState.Remove("Longitude");
            ModelState.Remove("EmployeeID");
            ModelState.Remove("BranchParentID");
            ModelState.Remove("AHeadName");
            ModelState.Remove("ASHeadName");
            ModelState.Remove("AccountHead");
            ModelState.Remove("AccountHeadSub");
            ModelState.Remove("AccountCode");
            ModelState.Remove("AccountSHCode");
            ModelState.Remove("StartTime");
            ModelState.Remove("EndTime");
            ModelState.Remove("SortOrder");
            ModelState.Remove("ContactPerson");
            ModelState.Remove("FK_IntroduceBranch");
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

            BranchModel branch = new BranchModel();

            var branchInfo = branch.GetBranchData(companyKey: _userLoginInfo.CompanyKey, input: new BranchModel.BranchID {
                ID_Branch = data.BranchID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                Mode = 0
            });
            var subtable = branch.GetBranchbankdetailsData(companyKey: _userLoginInfo.CompanyKey, input: new BranchModel.BranchBankDetailsSubSelect
            {
                FK_Branch = data.BranchID,
               
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            if (subtable.Process.IsProcess)
            {
                branchInfo.Data[0].BranchBankDetails = subtable.Data;
            }



            var subimg = branch.GetBranchImageData(companyKey: _userLoginInfo.CompanyKey, input: new BranchModel.BranchID
            {
                ID_Branch = data.BranchID,
                Mode = 1
            });

            var imgLst = new List<CompanyImage>();
            if (subimg.Data != null)
            {
                foreach (var dt in subimg.Data)
                {
                    var img = new CompanyImage();
                    img.ComImgMode = dt.SVImgMode;
                    img.ComImgName = dt.SVImgName;
                    img.ComImgValue = dt.SVImgValue;

                    imgLst.Add(img);
                }
                string CompanyImageList = "";
                CompanyImageList = subimg.Data.FirstOrDefault() != null ? Common.xmlTostring(imgLst) : "";
                Session["CompanyImage"] = CompanyImageList;
            }

            //return Json(branchInfo, JsonRequestBehavior.AllowGet);
            return Json(new { branchInfo, subimg }, JsonRequestBehavior.AllowGet);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken()]

        public ActionResult DeleteBranchInfo(BranchModel.BranchInfoView data)
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

            BranchModel.DeleteBranch deleteBranch = new BranchModel.DeleteBranch
            {
                ID_Branch = data.ID_Branch,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = "",
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID
            };

            Output dataresponse = Common.UpdateTableData<BranchModel.DeleteBranch>(companyKey: _userLoginInfo.CompanyKey, procedureName: "proBranchDelete", parameter: deleteBranch);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetBranchDeleteReasonList()
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


        public ActionResult LoadImageForm()
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
            CommonSearchPopupModel.Imagelist imgfld = new CommonSearchPopupModel.Imagelist();

            CommonSearchPopupModel objcmp = new CommonSearchPopupModel();

            //var imgmodelst = objcmp.GetImgModelist(input: new CommonSearchPopupModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);
            var imgmodelst = Common.GetDataViaQuery<CommonSearchPopupModel.ImgMode>(parameters: new APIParameters
            {
                TableName = "DocumentType DT",
                SelectFields = "DT.ID_DocumentType as ID_Mode,DT.DocTName as ModeName",
                Criteria = "    DT.Cancelled=0 AND DT.Passed=1 AND DT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
        companyKey: _userLoginInfo.CompanyKey

         );

            imgfld.ImgModeList = imgmodelst.Data;


            var identityproof = Common.GetDataViaQuery<CommonSearchPopupModel.IdentityProof>(parameters: new APIParameters
            {
                TableName = "IdentityProof I",
                SelectFields = "I.ID_IdentityProof,I.IdName ",
                Criteria = "    I.Cancelled=0 AND I.Passed=1 AND I.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
         companyKey: _userLoginInfo.CompanyKey

          );

            imgfld.IdentityProofs = identityproof.Data;
            return PartialView("Image", imgfld);
        }

    }
}