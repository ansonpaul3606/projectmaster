using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class MultipleImageController : Controller
    {
        // GET: MultipleImage
        public ActionResult Index()
        {
             return View();
           
        }


        public ActionResult LoadMultipleImageForm()
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

            MultipleImageModel.Imagelist imgfld = new MultipleImageModel.Imagelist();

            MultipleImageModel objcmp = new MultipleImageModel();

            var imgmodelst = objcmp.GetImgModelist(input: new MultipleImageModel.ImageMode { Mode = 24 }, companyKey: _userLoginInfo.CompanyKey);

            imgfld.ImgModeList = imgmodelst.Data;

            var identityproof = Common.GetDataViaQuery<MultipleImageModel.IdentityProof>(parameters: new APIParameters
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

            return PartialView("_MultipleImageUpload", imgfld);
        }
        
        //public ActionResult LoadImageForm(int mode)
        //{
        //    #region ::  Check User Session to verifyLogin  ::

        //    if (Session["UserLoginInfo"] is null)
        //    {
        //        return Json(new
        //        {
        //            Process = new Output
        //            {
        //                IsProcess = false,
        //                Message = new List<string> { "Please login to continue" },
        //                Status = "Session Timeout",
        //            }
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    #endregion ::  Check User Session to verifyLogin  ::
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //     return PartialView("_MultipleImage");
        //}


        [HttpPost]
        public ActionResult GetImage(string byteData)
        {
            byte[] bytes = Convert.FromBase64String(byteData);


            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

            return Json(base64String, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImageClear()
        {
            Session["CompanyImage"] = "";
            Session["EmployeeImage"] = "";
            Session["WarrantyTypeImage"] = "";
            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Imagesession(MultipleImageModel.CommonImageList data)
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
            else if (data.ProductImage != null)
            {
                foreach (var itm in data.ProductImage)
                {
                    itm.ProdImgValue = itm.ProdImgValue.Split(';')[1].Replace("base64,", "");
                }
                string ProductImageList = "";
                ProductImageList = data.ProductImage is null ? "" : Common.xmlTostring(data.ProductImage);
                Session["ProductImage"] = ProductImageList;
            }

            else if (data.WarrantyTypeImage != null)
            {
                foreach (var itm in data.WarrantyTypeImage)
                {
                    itm.WarrantyImgValue = itm.WarrantyImgValue.Split(';')[1].Replace("base64,", "");
                }
                string WarrantyTypeImageList = "";
                WarrantyTypeImageList = data.ProductImage is null ? "" : Common.xmlTostring(data.ProductImage);
                Session["WarrantyTypeImage"] = WarrantyTypeImageList;
            }
            return Json(JsonRequestBehavior.AllowGet);
        }       

    }
}