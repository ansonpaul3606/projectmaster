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
using static PerfectWebERP.Models.WalkingCustomerModel;

namespace PerfectWebERP.Controllers
{
    public class WalkingCustomerController : Controller
    {
        // GET: WalkingCustomer
        public ActionResult WalkingCustomer(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            ViewBag.FK_AssignedToBranch = _userLoginInfo.FK_BranchCodeUser;
            //   ViewBag.ID_Users = _userLoginInfo.ID_Users;
            ViewBag.mtd = mtd;
            return View();
        }

        public ActionResult LoadWalkingCustomer(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;

            WalkingCustomerModel walkingmodel = new WalkingCustomerModel();

            WalkingCustomerModel.WalkingCustomerPartialModel model = new WalkingCustomerModel.WalkingCustomerPartialModel();
            model.ID_Users = _userLoginInfo.ID_Users;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddWalkingCustomer", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWalkingCustomer(WalkingCustomerModel.WalkingCustomerView data)
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
            WalkingCustomerModel walkingmodel = new WalkingCustomerModel();

            var dataresponse = walkingmodel.UpdateWalkingCustomerData(input: new WalkingCustomerModel.UpdateWalkingCustomer
            {
                UserAction = 1,
                Debug = 0,
                ID_CustomerAssignment = 0,
                CusName =data.CustomerName,
                CusMobile =data.CustomerPhone,
                CaAssignedDate = data.AssignedDate,
                FK_Employee =data.FK_AssignedTo,
                CaDescription =data.Description,
                TransMode = "",
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                leadByMobileNo = data.leadByMobileNoItems is null ? "" : Common.xmlTostring(data.leadByMobileNoItems),
            
            }, companyKey: _userLoginInfo.CompanyKey);
            try
            {
                Common.SendMobileNotification(companyKey: _userLoginInfo.CompanyKey);
            }
            catch (Exception ex)
            {
            }

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateWalkingCustomer(WalkingCustomerModel.WalkingCustomerView data)
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
            WalkingCustomerModel walkingmodel = new WalkingCustomerModel();

            var dataresponse = walkingmodel.UpdateWalkingCustomerData(input: new WalkingCustomerModel.UpdateWalkingCustomer
            {
                UserAction = 2,
                Debug = 0,
                ID_CustomerAssignment = data.ID_WalkingCustomer,
                CusName = data.CustomerName,
                CusMobile = data.CustomerPhone,
                CaAssignedDate = data.AssignedDate,
                FK_Employee = data.FK_AssignedTo,
                CaDescription = data.Description,
                TransMode = "",
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                //leadByMobileNo = data.leadByMobileNoItems is null ? "" : Common.xmlTostring(data.leadByMobileNoItems),
            }, companyKey: _userLoginInfo.CompanyKey);
            try
            {
                Common.SendMobileNotification(companyKey: _userLoginInfo.CompanyKey);
            }
            catch (Exception ex)
            {
            }

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetWalkingCustomerList(int pageSize, int pageIndex, string Name)
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

            WalkingCustomerModel walkingmodel = new WalkingCustomerModel();

            var data = walkingmodel.GetWalkingCustomerData(companyKey: _userLoginInfo.CompanyKey, input: new WalkingCustomerModel.GetWalkingCustomer
            {
                FK_CustomerAssignment = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode
            });
            
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetWalkingCustomerInfo(WalkingCustomerModel.WalkingCustomerView data)
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

            WalkingCustomerModel walkingmodel = new WalkingCustomerModel();

            var modelInfo = walkingmodel.GetWalkingCustomerDataByID(companyKey: _userLoginInfo.CompanyKey, input: new WalkingCustomerModel.GetWalkingCustomerById
            {
                FK_CustomerAssignment = data.ID_WalkingCustomer,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,                
                TransMode = ""
            });
            ViewBag.FK_AssignedToBranch = _userLoginInfo.FK_BranchCodeUser;
            return Json(modelInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteWalkingCustomer(WalkingCustomerModel.DeleteView data)
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
            
            WalkingCustomerModel walkingmodel = new WalkingCustomerModel();


            Output datresponse = walkingmodel.DeleteWalkingCustomerData(input: new WalkingCustomerModel.DeleteWalkingCustomer
            {
                FK_CustomerAssignment = data.ID_WalkingCustomer,
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

        public ActionResult GetWalkingCustomerDeleteReasonList()
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
        public ActionResult GetMobileData(MobileInput inputdata)
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

            WalkingCustomerModel walkingmodel = new WalkingCustomerModel();

            
               var data = walkingmodel.GetmobileData(companyKey: _userLoginInfo.CompanyKey, input: new WalkingCustomerModel.MobileInput
                {

                    FK_Company = _userLoginInfo.FK_Company,
                    MobileNo = inputdata.MobileNo

                });

           
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}