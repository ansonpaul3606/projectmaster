using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
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
    public class BuyerController : Controller
    {
        // GET: Buyer
        public ActionResult Index(string mtd)
        {   
           
         UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

                UserAcssRightInfo pageAccess = new UserAcssRightInfo();
                pageAccess = _userLoginInfo.PageAccessRights;
                ViewBag.Username = _userLoginInfo.UserName;
                ViewBag.UserRole = _userLoginInfo.UserRole;
                ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
                ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mTd = objCmnMethod.DecryptString(mtd);
            TempData["mTd"] = mTd.ToString();
            return View();
        }
            public ActionResult BuyerView()
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
                Buyermodel.BuyerView makelist = new Buyermodel.BuyerView();
            ViewBag.PageTitle = TempData["mTd"] as string;
            return PartialView("_AddBuyerview", makelist);
            }

            [HttpPost]
            [ValidateAntiForgeryToken()]
            public ActionResult AddBuyer(Buyermodel.BuyerView data)
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


            Buyermodel buyermodel = new Buyermodel();
                var datresponse = buyermodel.UpdateBuyerData(input: new Buyermodel.UpdateBuyer
                {
                    UserAction = 1,
                    BuyerName = data.Name,
                    BuyerAddress = data.Address,
                    BuyerPhone = data.Phone,  
                    BuyerGSTINNo = data.GSTINNo,                  
                    FK_Country = data.CountryID,
                    FK_States = data.StatesID,
                    FK_District = data.DistrictID,
                    FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                    FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,          
                    FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,          
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,                 
                    TransMode = "",
                    

                }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

            [HttpPost]
            [ValidateAntiForgeryToken()]
            public ActionResult UpdateBuyer(Buyermodel.BuyerView data)
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


            Buyermodel buyermodel = new Buyermodel();
                var datresponse = buyermodel.UpdateBuyerData(input: new Buyermodel.UpdateBuyer
                {
                    UserAction = 2,
                    Debug = 0,
                    TransMode = "",
                    ID_Buyer = data.ID_Buyer,                  
                    BuyerName = data.Name,
                    BuyerAddress = data.Address,
                    BuyerPhone = data.Phone,
                    BuyerGSTINNo = data.GSTINNo,
                    FK_Country = data.CountryID,
                    FK_States = data.StatesID,
                    FK_District = data.DistrictID,
                    FK_Place = (data.PlaceID.HasValue) ? data.PlaceID.Value : 0,
                    FK_Area = (data.AreaID.HasValue) ? data.AreaID.Value : 0,
                    FK_Post = (data.PostID.HasValue) ? data.PostID.Value : 0,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    

                }, companyKey: _userLoginInfo.CompanyKey);
                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

            }

            public ActionResult GetBuyerList(int pageSize, int pageIndex, string Name)
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

                Buyermodel brand = new Buyermodel();
                var MakerInfo = brand.GetBuyerSelect(companyKey: _userLoginInfo.CompanyKey, input: new Buyermodel.GetBuyerDetails
                {
                    FK_Buyer = 0,
                    FK_Company = _userLoginInfo.FK_Company,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    PageIndex = pageIndex + 1,
                    PageSize = pageSize,
                    Name = Name,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                });

                return Json(new { MakerInfo.Process, MakerInfo.Data, pageSize, pageIndex, totalrecord = (MakerInfo.Data is null) ? 0 : MakerInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            }

            public ActionResult GetBuyerInfo(Buyermodel.BuyerView data)
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

            Buyermodel brand = new Buyermodel();

                var modelInfo = brand.GetBuyerSelectData(companyKey: _userLoginInfo.CompanyKey, input: new Buyermodel.GetBuyerbyIdDetails
                {
                    FK_Buyer= data.ID_Buyer,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    FK_Machine = _userLoginInfo.FK_Machine
                });

                return Json(modelInfo, JsonRequestBehavior.AllowGet);
            }

            public ActionResult GetBuyerDeleteReasonList()
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

            public ActionResult DeleteBuyer(Buyermodel.DeleteView data)
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

                Buyermodel brands = new Buyermodel();



                Output datresponse = brands.DeleteBuyerData(input: new Buyermodel.DeleteBuyer
                {
                    FK_Buyer = data.ID_Buyer,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Reason = data.ReasonID,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    Debug = 0,
                    TransMode = "",
                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }
        public ActionResult GetPinCodedetails(string Pincode)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            Buyermodel dataModel = new Buyermodel();
            var outputData = dataModel.GetDetailsByPincodeData(input: new Buyermodel.InputPincode { FK_Company = _userLoginInfo.FK_Company,
                Pincode = Pincode }, companyKey: _userLoginInfo.CompanyKey);
            return Json(outputData, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCountryList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<Buyermodel.Countrylist>(parameters: new APIParameters
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
                FK_States = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Country = countryID,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                TransMode = ""

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(outputData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictList(Int64 statesid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<Buyermodel.Districtlist>(parameters: new APIParameters
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

            var data = Common.GetDataViaQuery<Buyermodel.Arealist>(parameters: new APIParameters
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

            var data = Common.GetDataViaQuery<Buyermodel.Placelist>(parameters: new APIParameters
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
        public ActionResult GetPostList(Buyermodel.InputPlace datas)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<Buyermodel.Postlist>(parameters: new APIParameters
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
            Buyermodel model = new Buyermodel();


            var data = model.GetSupplierByGST(input:
             new Buyermodel.GSTINView
             {
                 FK_Company = _userLoginInfo.FK_Company,
                 GSTINNo = GSTINNo

             },

              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);


        }


    }
}



