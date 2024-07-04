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
using PerfectWebERPAPI.Business;
using System.Text;

namespace PerfectWebERP.Controllers
{
    public class CustomerPortalImageController : Controller
    {
        // GET: CustomerPortalImage
        public ActionResult CusPortalImageUpload(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.mtd = mtd;
            ViewBag.PageTitles = objCmnMethod.DecryptString(mtd);

            return View();
        }
        public ActionResult LoadCusPortalImageUplaod(string mtd)
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

            CustomerPortalImageModel.CustomerPortalImage obj = new CustomerPortalImageModel.CustomerPortalImage();
            CustomerPortalImageModel objget = new CustomerPortalImageModel();
            var modeList = objget.GetImageModeList(input: new CustomerPortalImageModel.ModeLead { Mode = 127 }, companyKey: _userLoginInfo.CompanyKey);
            obj.ModeList = modeList.Data;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddCustPortalImageUpload", obj);
        }

        public ActionResult SaveImages(CustomerPortalImageModel.SaveImages objImage)
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

            string img = objImage.ImageList.Split(';')[1].Replace("base64,", "");
            //byte[] base64String = Convert.ToBase64String(objImage.ImageList);

           // byte[] imageListBytes = Encoding.UTF8.GetBytes(objImage.ImageList);

           // byte[] imageBytes = Convert.FromBase64String(img); // For VARBINARY(MAX)


            CustomerPortalImageModel objimg = new CustomerPortalImageModel();
            var dataresponse = objimg.UpdateImagesData(input: new CustomerPortalImageModel.UpdateImageData
            {
                UserAction=1,
                CusportalTitle=objImage.CusportalTitle,
                CusportalSubTitle=objImage.CusportalSubTitle,
                CusportalActive=objImage.CusportalActive,
                CusportalEffectFrom=objImage.CusportalEffectFrom,
                CusportalEffectTo=objImage.CusportalEffectTo,
                CusportalRedirectTo=objImage.CusportalRedirectTo,
                CusportalMode=objImage.CusportalMode,
                ImageList = objImage.ImageList,
                FK_Company = _userLoginInfo.FK_Company,
                Entrby = _userLoginInfo.EntrBy,
                ID_CusPortalSlider=0,

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateImages(CustomerPortalImageModel.SaveImages objImage)
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

            //string img = objImage.ImageList.Split(';')[1].Replace("base64,", "");
            //byte[] base64String = Convert.ToBase64String(objImage.ImageList);

            // byte[] imageListBytes = Encoding.UTF8.GetBytes(objImage.ImageList);

            // byte[] imageBytes = Convert.FromBase64String(img); // For VARBINARY(MAX)


            CustomerPortalImageModel objimg = new CustomerPortalImageModel();
            var dataresponse = objimg.UpdateImagesData(input: new CustomerPortalImageModel.UpdateImageData
            {
                UserAction = 2,
                CusportalTitle = objImage.CusportalTitle,
                CusportalSubTitle = objImage.CusportalSubTitle,
                CusportalActive = objImage.CusportalActive,
                CusportalEffectFrom = objImage.CusportalEffectFrom,
                CusportalEffectTo = objImage.CusportalEffectTo,
                CusportalRedirectTo = objImage.CusportalRedirectTo,
                CusportalMode = objImage.CusportalMode,
                ImageList = objImage.ImageList,
                FK_Company = _userLoginInfo.FK_Company,
                Entrby = _userLoginInfo.EntrBy,
                ID_CusPortalSlider = objImage.ID_CusPortalSlider,

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetImagesList(int pageSize, int pageIndex, string Name)
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

           
            CustomerPortalImageModel img = new CustomerPortalImageModel();

            var data = img.GetImageData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerPortalImageModel.ImageGetInput
            {
                FK_CusPortalSlider = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name
            });
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetImageInfoByID(CustomerPortalImageModel.ImageSelectView data)
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
            
            CustomerPortalImageModel img = new CustomerPortalImageModel();

            var ImageInfo = img.GetImageData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerPortalImageModel.ImageGetInput
            {
                FK_CusPortalSlider = data.FK_CusPortalSlider,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
            });
            //var Imagedata = img.GetImageDataDetails(companyKey: _userLoginInfo.CompanyKey, input: new CustomerPortalImageModel.ImageGetInput
            //{
            //    FK_CusPortalSlider = data.FK_CusPortalSlider,
            //    FK_Company = _userLoginInfo.FK_Company,
            //    FK_Machine = _userLoginInfo.FK_Machine,
            //    EntrBy = _userLoginInfo.EntrBy,
            //    Imagedetails=1,
            //});

            return Json(new{ ImageInfo/*, Imagedata*/ }, JsonRequestBehavior.AllowGet);


        }
    }
}