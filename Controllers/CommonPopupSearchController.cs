using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class CommonPopupSearchController : Controller
    {
        // GET: CommonPopupSearch
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadImageForm(int mode)
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

            var imgmodelst = objcmp.GetImgModelist(input: new CommonSearchPopupModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);

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




        public ActionResult LoadMultipleImageForm(int mode)
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

            var imgmodelst = objcmp.GetImgModelist(input: new CommonSearchPopupModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);

            imgfld.ImgModeList = imgmodelst.Data;
                  
            return PartialView("ImageMultiple", imgfld);
        }

            public ActionResult MultipleImageLoadForm(int mode)
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

            var imgmodelst = objcmp.GetImgModelist(input: new CommonSearchPopupModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);

            imgfld.ImgModeList = imgmodelst.Data;
                  
            return PartialView("_MultipleImageUpload", imgfld);
        }


        public ActionResult LoadMultipleProductionImage(int mode)
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

            var imgmodelst = objcmp.GetImgModelist(input: new CommonSearchPopupModel.ModeLead { Mode = mode }, companyKey: _userLoginInfo.CompanyKey);

            imgfld.ImgModeList = imgmodelst.Data;

            return PartialView("ImageMultipleProduction", imgfld);
        }
        [HttpPost]
        //public ActionResult GetImage(string byteData)
        //{
        //    byte[] bytes = Convert.FromBase64String(byteData);


        //    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

        //    return Json(base64String, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetImage(string byteData, bool isImage)
        //{
        //    byte[] bytes = Convert.FromBase64String(byteData);

        //    if (isImage)
        //    {
        //        // Convert bytes to base64 string for images
        //        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        //        return Json(base64String, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        // Return byte data directly for documents
        //        return File(bytes, "application/octet-stream", "document_name.ext");
        //    }
        //}
        public ActionResult GetImage(string byteData, bool isImage)
        {
            byte[] bytes = Convert.FromBase64String(byteData);

            if (isImage)
            {
                // Convert bytes to base64 string for images
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                return Json(base64String, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Return byte data directly for documents
                return File(bytes, "application/octet-stream", "document_name.ext");
            }
        }

        public ActionResult ImageClear()
        {
            Session["CompanyImage"] = "";
            Session["EmployeeImage"] = "";
            return Json(JsonRequestBehavior.AllowGet);
        }

        //public ActionResult ImageClear()
        //{
        //    Session["CompanyImage"] = "";
        //    Session["EmployeeImage"] = "";
        //    return Json(JsonRequestBehavior.AllowGet);
        //}
        
        [HttpPost]
        public ActionResult Imagesession(CommonSearchPopupModel.CommonImageList data)
        {
            if (data.CompanyImage != null)
            {
                foreach (var itm in data.CompanyImage)
                {
                    itm.ComImgValue = itm.ComImgValue.Split(';')[1].Replace("base64,", "");
                }
                string CompanyImageList = "";
                CompanyImageList = data.CompanyImage is null ? "" : Common.xmlTostring(data.CompanyImage);
                Session["CompanyImage"] = CompanyImageList;
            }
            else if (data.EmployeeImage != null)
            {
                foreach (var itm in data.EmployeeImage)
                {
                    itm.EmpImgValue = itm.EmpImgValue.Split(';')[1].Replace("base64,", "");
                }
                string EmployeeimageList = "";
                EmployeeimageList = data.EmployeeImage is null ? "" : Common.xmlTostring(data.EmployeeImage);
                Session["EmployeeImage"] = EmployeeimageList;
            }
            else if (data.WarProductImage != null)
            {
                foreach (var itm in data.WarProductImage)
                {if (itm != null)
                    {
                        if (itm.ProdImage.Contains("base64,"))
                        {
                            itm.ProdImage = itm.ProdImage.Split(';')[1].Replace("base64,", "");
                        }
                    }
                }
                string WarProductImageList = "";
                WarProductImageList = data.WarProductImage is null ? "" : Common.xmlTostring(data.WarProductImage);
                Session["WarProductImage"] = WarProductImageList;
            }

            else if (data.WarrantyProductImage != null)
            {
                foreach (var itm in data.WarrantyProductImage)
                {
                    if (itm.ProdImage.Contains("base64,"))
                    {
                        itm.ProdImage = itm.ProdImage.Split(';')[1].Replace("base64,", "");
                    }
                }
                string WarrantyProductImageList = "";
                WarrantyProductImageList = data.WarrantyProductImage is null ? "" : Common.xmlTostring(data.WarrantyProductImage);
                Session["WarrantyProductImage"] = WarrantyProductImageList;
            }
            else if (data.ProductionImage != null)
            {
                foreach (var itm in data.ProductionImage)
                {
                    if (itm.PSImage.Contains("base64,"))
                    {
                        itm.PSImage = itm.PSImage.Split(';')[1].Replace("base64,", "");
                    }
                }
                string ProductionImageList = "";
                ProductionImageList = data.ProductionImage is null ? "" : Common.xmlTostring(data.ProductionImage);
                Session["ProductionImage"] = ProductionImageList;
            }
            return Json( JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCmnPopList(CommonSearchPopupModel.GetPopSearch dt)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CommonSearchPopupModel objfld = new CommonSearchPopupModel();


            var data = objfld.GetCommonlist(input: new CommonSearchPopupModel.GetPopSearch
            {
                TransMode = dt.TransMode,
                Pagemode = dt.Pagemode,
                FK_Company = _userLoginInfo.FK_Company,
                Critrea1 = dt.Critrea1,
                Critrea2 = dt.Critrea2,
                Critrea3 = dt.Critrea3,
                Critrea4 = dt.Critrea4,
                Criteria5 = dt.Criteria5,
                PageIndex = dt.PageIndex + 1,
                PageSize = dt.PageSize,
                Name = dt.Name,
                NameCriteria=dt.NameCriteria

            },
             companyKey: _userLoginInfo.CompanyKey);
            return Json(new { data.Process, data.Data, dt.PageSize, dt.PageIndex, dt.Pagemode, dt.Critrea1, dt.Critrea2, dt.Critrea3, dt.Critrea4 }, JsonRequestBehavior.AllowGet);


        }



        public ActionResult GetCmnPopListGet(CommonSearchPopupModel.GetPopSearch dt)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            CommonSearchPopupModel objfld = new CommonSearchPopupModel();

            var data = objfld.GetCommonlist(input: new CommonSearchPopupModel.GetPopSearch
            {
                TransMode = "",
                Pagemode = dt.Pagemode,
                FK_Company = _userLoginInfo.FK_Company,
                Critrea1 = dt.Critrea1,
                Critrea2 = dt.Critrea2,
                Critrea3 = dt.Critrea3,
                Critrea4 = dt.Critrea4,
                PageIndex = dt.PageIndex + 1,
                PageSize = dt.PageSize,
                Name = dt.Name,
                NameCriteria = dt.NameCriteria,
                ID = dt.ID

            },
             companyKey: _userLoginInfo.CompanyKey);
            return Json(new { data.Process, data.Data, dt.PageSize, dt.PageIndex, dt.Pagemode, dt.Critrea1, dt.Critrea2, dt.Critrea3, dt.Critrea4 }, JsonRequestBehavior.AllowGet);


        }

       
        //[HttpPost]
        //public ActionResult InsertImage(string byteData)
        //{
        //    byte[] bytes = Convert.FromBase64String(byteData);
        //    var jsonResult = Json(bytes, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;
        //    return jsonResult;

        //}


        //public ActionResult GetCmnPopList1(Int32 Pagemode, Int32 Critrea1, Int32 Critrea2, int pageSize, int pageIndex,string Name)
        //{

        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    CommonSearchPopupModel objfld = new CommonSearchPopupModel();


        //    var data = objfld.GetCommonlist(input: new CommonSearchPopupModel.GetPopSearch
        //    {
        //        TransMode = "",
        //        Pagemode = Pagemode,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        Critrea1 = Critrea1,
        //        Critrea2 = Critrea2,
        //        Critrea3 = "",
        //        Critrea4 = "",
        //        PageIndex = pageIndex + 1,
        //        PageSize = pageSize,
        //        Name=Name

        //    },
        //     companyKey: _userLoginInfo.CompanyKey);
        //    return Json(new { data.Process, data.Data, pageSize, pageIndex, Pagemode , Critrea1 , Critrea2 }, JsonRequestBehavior.AllowGet);


        //}
        //public ActionResult GetCmnPopListGet1(Int32 Pagemode, Int32 Critrea1, Int32 Critrea2, int pageSize, int pageIndex, string Name, int ID)
        //{
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    CommonSearchPopupModel objfld = new CommonSearchPopupModel();


        //    var data = objfld.GetCommonlist(input: new CommonSearchPopupModel.GetPopSearch
        //    {
        //        TransMode = "",
        //        Pagemode = Pagemode,
        //        FK_Company = _userLoginInfo.FK_Company,
        //        Critrea1 = Critrea1,
        //        Critrea2 = Critrea2,
        //        Critrea3 = "",
        //        Critrea4 = "",
        //        PageIndex = pageIndex + 1,
        //        PageSize = pageSize,
        //        Name = Name,
        //        ID = ID

        //    },
        //     companyKey: _userLoginInfo.CompanyKey);
        //    return Json(new { data.Process, data.Data, pageSize, pageIndex, Pagemode, Critrea1, Critrea2 }, JsonRequestBehavior.AllowGet);


        //}


    }
    }

