/*----------------------------------------------------------------------
Created By	: Kavya K
Created On	: 25/11/2022
Purpose		: ProductionStage
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
    public class ProductionStageSettingController : Controller
    {
        public ActionResult Index(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            //ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            return View();
        }

        public ActionResult LoadFormProductionStage()
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

            ProductionStageSettingModel.ProductionStageView ObjPrdctnList = new ProductionStageSettingModel.ProductionStageView();
            ProductionStageSettingModel DurationList = new ProductionStageSettingModel();

            var a = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
            companyKey: _userLoginInfo.CompanyKey,
            procedureName: "ProGetNextNo",
            parameter: new NextSortOrder
            {
                TableName = "ProductionStage",
                FieldName = "SortOrder",
                Debug = 0
            });

            ObjPrdctnList.SortOrder = a.Data[0].NextNo;

  
            var Duration = DurationList.GetDurationList(input: new ProductionStageSettingModel.Duration { Mode = 43 }, companyKey: _userLoginInfo.CompanyKey);

            ObjPrdctnList.DurationType = Duration.Data;

            return PartialView("_AddProductionStageSetting", ObjPrdctnList);
        }

        [HttpPost]
        public ActionResult GetProductionStageList(int pageSize, int pageIndex, string Name,string TransMode)
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
            ProductionStageSettingModel ProductionStage = new ProductionStageSettingModel();

            var ProdInfo = ProductionStage.GetProductionStageData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStageSettingModel.ProductionStageID {
                FK_ProductionStage = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = TransMode,
                CheckList = 0

            });
            return Json(new { ProdInfo.Process, ProdInfo.Data, pageSize, pageIndex, totalrecord = (ProdInfo.Data is null) ? 0 : ProdInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewProductionStage(ProductionStageSettingModel.ProductionStageView data)
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

            ModelState.Remove("ProductionStageID");
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
            if (data.ImageList != null)
            {
                foreach (CommonSearchPopupModel.ImageListView itm in data.ImageList)
                {
                    if (itm.ProdImage != null && itm.ProdImage != "")
                    {
                        var img = itm.ProdImage.Split(';')[1].Replace("base64,", "");

                        itm.ProdImage = img;
                    }
                    else
                    {
                        itm.ProdImage = "";

                    }
                }
            }
            ProductionStageSettingModel ProductionStage = new ProductionStageSettingModel();
            
            var datresponse = ProductionStage.UpdateProductionStageData(input: new ProductionStageSettingModel.UpdateProductionStage
            {
                UserAction = 1,
                PSDate = data.PSDate,
                FK_Product = data.FK_Product,
                FK_Stage = data.FK_Stage,
                FK_Team = data.FK_Team,
                PSDurationType = data.PSDurationType,
                PSDurationPrd = data.PSDurationPrd,
                PSPrllCount = data.PSPrllCount,
                PSEmpWise = data.PSEmpWise,
                PSWorkPer = data.PSWorkPer,
                TransMode = data.TransMode,
                PSRemarks = data.PSRemarks,
                InputMaterialDetails = data.InputMaterialDetails is null ? "" : Common.xmlTostring(data.InputMaterialDetails),
                OutputProductsDetails = data.OutputProductsDetails is null ? "" : Common.xmlTostring(data.OutputProductsDetails),
                ResourceDetails = data.ResourceDetails is null ? "" : Common.xmlTostring(data.ResourceDetails),
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_ProductionStage = 0,
                Debug=0,
                FK_ProductionStage=0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateProductionStage(ProductionStageSettingModel.ProductionStageView data)
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
            var imagenull = false;
            if (data.ImageList != null)
            {
                foreach (CommonSearchPopupModel.ImageListView itm in data.ImageList)
                {
                    if (itm != null)
                    {
                        if (itm.ProdImage != null && itm.ProdImage != "")
                        {
                            var img = itm.ProdImage.Split(';')[1].Replace("base64,", "");

                            itm.ProdImage = img;
                        }
                        else
                        {
                            itm.ProdImage = "";

                        }
                    }
                    else
                    {
                        imagenull = true;
                    }
                }
            }
            if(imagenull)
            {
                data.ImageList = null;
            }
            ProductionStageSettingModel ProductionStage = new ProductionStageSettingModel();
            var datresponse = ProductionStage.UpdateProductionStageData(input: new ProductionStageSettingModel.UpdateProductionStage
            {
                UserAction = 2,
                PSDate = data.PSDate,
                FK_Product = data.FK_Product,
                FK_Stage = data.FK_Stage,
                FK_Team = data.FK_Team,
                PSDurationType = data.PSDurationType,
                PSDurationPrd = data.PSDurationPrd,
                PSPrllCount = data.PSPrllCount,
                PSEmpWise = data.PSEmpWise,
                PSWorkPer = data.PSWorkPer,
                TransMode = data.TransMode,
                PSRemarks = data.PSRemarks,
                InputMaterialDetails = data.InputMaterialDetails is null ? "" : Common.xmlTostring(data.InputMaterialDetails),
                OutputProductsDetails = data.OutputProductsDetails is null ? "" : Common.xmlTostring(data.OutputProductsDetails),
                ResourceDetails = data.ResourceDetails is null ? "" : Common.xmlTostring(data.ResourceDetails),
                ImageList =data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                ID_ProductionStage = data.ID_ProductionStage,
                Debug = 0,
                FK_ProductionStage = data.ID_ProductionStage,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetProductionStageInfo(ProductionStageSettingModel.ProductionStageView data)
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

          


            ProductionStageSettingModel ProductionStage = new ProductionStageSettingModel();

           
                var ProductionStageInfo = ProductionStage.GetProductionStageData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStageSettingModel.ProductionStageID
                {
                    FK_ProductionStage = data.FK_ProductionStage,
                    FK_Company = _userLoginInfo.FK_Company,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    TransMode = data.TransMode,
                    CheckList = 0
                });



            var ProductionStageInputItem = ProductionStage.GetProductionStageInputDetailData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStageSettingModel.ProductionStageID
            {
                FK_ProductionStage = data.FK_ProductionStage,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                CheckList = 1
            });

            var ProductionStageOutputItem = ProductionStage.GetProductionStageInputDetailData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStageSettingModel.ProductionStageID
            {
                FK_ProductionStage = data.FK_ProductionStage,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                CheckList = 2
            });
            var ProductionStageResourceItem = ProductionStage.GetProductionStageInputDetailData(companyKey: _userLoginInfo.CompanyKey, input: new ProductionStageSettingModel.ProductionStageID
            {
                FK_ProductionStage = data.FK_ProductionStage,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                TransMode = data.TransMode,
                CheckList = 3
            });

            CommonSearchPopupModel prodimg = new CommonSearchPopupModel();
            var Imageselect = prodimg.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new CommonSearchPopupModel.GetImagein
            {
                FK_Master = data.FK_ProductionStage,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
               
            });
            if (Imageselect.Data != null)
            {
                foreach (CommonSearchPopupModel.ImageListView itm in Imageselect.Data)
                {
                    if (itm.ProdImage != "" && itm.ProdImage != null)
                    {
                        itm.ProdImage = "data:image/;base64," + itm.ProdImage;
                    }
                }
            }
            return Json(new { ProductionStageInfo, ProductionStageInputItem , ProductionStageOutputItem, ProductionStageResourceItem, Imageselect }, JsonRequestBehavior.AllowGet);
          
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteProductionStage(ProductionStageSettingModel.ProductionStageInfoView data)
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

            ProductionStageSettingModel ProductionStage = new ProductionStageSettingModel();

            var datresponse = ProductionStage.DeleteProductionStageData(input: new ProductionStageSettingModel.DeleteProductionStage {
                TransMode=data.TransMode,
                FK_ProductionStage = data.FK_ProductionStage,
                Debug = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Reason=data.ReasonID,
                FK_Company=_userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
               FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
               }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductionStageReasonList()
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

            ReasonModel reason = new ReasonModel();

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID {
                FK_Reason = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy });

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
            return PartialView("ImageMultipleProduction", imgfld);
        }
    }
}


