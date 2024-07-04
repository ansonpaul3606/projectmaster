using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.General;
using PerfectWebERP.Models;

namespace PerfectWebERP.Controllers
{
    public class DeliverylistController : Controller
    {
        // GET: Deliverylist
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
            //string mGrp = objCmnMethod.DecryptString(mgrp);
            //ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;

            return View();
        }
            

     
            public ActionResult LoadDeliverylistRepo(string mtd)
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

            DeliverylistreportModel DeliveryReport = new DeliverylistreportModel();
            DeliverylistreportModel.Deliveryview Delicerylist = new DeliverylistreportModel.Deliveryview();
               



                CommonMethod objCmnMethod = new CommonMethod();
                ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);


                return PartialView("_AddDeliverylistReport", Delicerylist);
            }





        #region[GetDeliveryModelList]
        public ActionResult GetDeliveryModelList(DeliverylistreportModel.DeliverylistReportInput DeliveryModel)
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
            DeliverylistreportModel reportModel = new DeliverylistreportModel();

                var datares = reportModel.GetDeliveryGeneralReport(new DeliverylistreportModel.DeliverylistReportInput
                {
                    FK_Mode = DeliveryModel.FK_Mode,                    
                    FromDate = DeliveryModel.FromDate,
                    ToDate = DeliveryModel.ToDate,
                    FK_State=DeliveryModel.FK_State,
                    FK_District=DeliveryModel.FK_District,
                    FK_Product=DeliveryModel.FK_Product,
                    FK_Status=DeliveryModel.FK_Status,
                    FK_Area = DeliveryModel.FK_Area,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Machine = _userLoginInfo.FK_Machine

                }, companyKey: _userLoginInfo.CompanyKey);


                return Json(new { datares }, JsonRequestBehavior.AllowGet);
            }
            #endregion

        }
    }



