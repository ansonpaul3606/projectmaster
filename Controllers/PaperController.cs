using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PerfectWebERP.Controllers
{
    public class PaperController : Controller
    {
        // GET: Paper
        public ActionResult Index(string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            return View();           
      }    
       public ActionResult LoadPaperMaster(string TransMode)
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

            PaperModel objP = new PaperModel();
            PaperModel.PaperNew objPapnew = new PaperModel.PaperNew();
            var sort = Common.GetDataViaProcedure<NextSortOrderOutput, NextSortOrder>(
                companyKey: _userLoginInfo.CompanyKey,
                procedureName: "ProGetNextNo",
                parameter: new NextSortOrder
                {
                    TableName = "Paper",
                    FieldName = "SortOrder",
                    Debug = 0
                });

            objPapnew.Sort = sort.Data[0].NextNo;
            var modeType = objP.GetModeList(input: new PaperModel.GetModeData { Mode = 64 }, companyKey: _userLoginInfo.CompanyKey);
            objPapnew.TypeList = modeType.Data.AsEnumerable();

            var Module = Common.GetDataViaQuery<PaperModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType",
                SelectFields = "ID_ModuleType as IDMode,ModuleName as ModeName,Mode",
                Criteria = "",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            objPapnew.modeList = Module.Data;
            ViewBag.TransMode = TransMode;
            return PartialView("_LoadPaperMaster", objPapnew);
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddPaper(PaperModel.PaperMaster data)
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


            PaperModel papermodel = new PaperModel();
            var datresponse = papermodel.UpdatePaperMaster(input: new PaperModel.UpdatePaper
            {
                UserAction = 1,
                Debug = 0,
                TransMode = "",
                ID_Paper = data.ID_Paper,
                PaperName = data.PaperName,
                PaperShortName = data.PaperShortName,
                SortOrder = data.SortOrder,
                FK_Type =data.FK_Type,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Mode = data.Mode,
                FK_Paper =data.FK_Paper,
                FK_AccountHead = data.FK_AccountHead,
                FK_AccountSubHead = data.FK_AccountSubHead,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }
 
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdatePaperMaster(PaperModel.PaperMaster data)
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


            PaperModel papermodel = new PaperModel();
            var datresponse = papermodel.UpdatePaperMaster(input: new PaperModel.UpdatePaper
            {
                UserAction = 2,
                Debug = 0,
                TransMode = "",
                ID_Paper = data.ID_Paper,
                PaperName = data.PaperName,
                PaperShortName = data.PaperShortName,
                SortOrder = data.SortOrder,
                FK_Type = data.FK_Type,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Paper = data.FK_Paper,
                Mode = data.Mode,
                FK_AccountHead = data.FK_AccountHead,
                FK_AccountSubHead = data.FK_AccountSubHead,
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPaperList(int pageSize, int pageIndex, string Name)
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

            PaperModel paper = new PaperModel();
            var MakerInfo = paper.GetPaperSelect(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.GetPaperDetails
            {
                FK_Paper = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Name = Name,
            });

            return Json(new { MakerInfo.Process, MakerInfo.Data, pageSize, pageIndex, totalrecord = (MakerInfo.Data is null) ? 0 : MakerInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindProvider(PaperModel.PaperMaster data )
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
      
          
            APIParameters apiSub = new APIParameters
            {
                TableName = "[Provider]",
                SelectFields = "[ID_Provider] AS ID_Provider,[ProvName] AS ProvName",
                Criteria = "Passed=1 And Cancelled=0 And FK_Paper='" + data.FK_Paper + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var ProviderInfo = Common.GetDataViaQuery<PaperModel.Provider>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(ProviderInfo, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetPaperInfo(PaperModel.PaperMaster data)
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

            PaperModel paper = new PaperModel();

            var modelInfo = paper.GetPaperSelectData(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.GetPaperbyIdDetails
            {
                FK_Paper = data.ID_Paper,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            return Json(modelInfo, JsonRequestBehavior.AllowGet);
        }
                      

        public ActionResult GetPaperDeleteReasonList()
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

        public ActionResult DeletePaper(PaperModel.DeleteView data)
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

            PaperModel paper = new PaperModel();



            Output datresponse = paper.DeletePaperData(input: new PaperModel.DeletePaper
            {
                FK_Paper = data.ID_Paper,
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



        public ActionResult Renew(string mtd,string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;
            CommonMethod cmnmethd = new CommonMethod();
            string mGrp = cmnmethd.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            var headName = ViewBag.TransMode.Substring(0, 2);

            string loghead = "";


            if (headName == "VL")
            {
                loghead = "Vehicle No";
            }
            else if (headName == "TO")
            {
                loghead = "Tool";
            }
            ViewBag.headlog = loghead;
            if(TempData["vData"]!=null)
            {
                var data = TempData["vData"] as RenewallModel;
                ViewBag.vName = data.NAME;               
                ViewBag.vPaper = data.ID_Papper;
                ViewBag.vPano = data.PapperNo;
                ViewBag.vPro = data.ID_Provider;
                ViewBag.vIssue = data.IssueDate;
               ViewBag.vIexpir = data.ExpireDATE;
                ViewBag.FK_Master = data.FK_Master;

            }
           
            return View();
           
        }


        public ActionResult LoadRenew(string mtd,string TransMode)
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
            PaperModel.PaperRenewalView paperlist = new PaperModel.PaperRenewalView();
            var mode = TransMode.Substring(0, 2);
            var modeData = "";
            string labelpopup = "";
            string lblpro = "";
            string loghead = "";
            string desc = " ";
            switch (mode)
            {
                case "IN":
                    modeData = "'P'";
                    break;
                case "VL":
                    modeData = "'V'";
                    labelpopup = "Driver";
                    lblpro = "Vehicle";
                    loghead = "Vehicle Paper Renewal";
                    desc = "Route";

                    break;
                case "TO":
                    modeData = "'T'";
                    labelpopup = "Operator";
                    lblpro = "Tool";
                    loghead = "Tool Paper Renewal";
                    desc = "Description";
                    break;
                case "AT":
                    modeData = "'A'";
                    break;
                default:
                    modeData = "'P'";
                    break;
            }
            ViewBag.lblemp = labelpopup;
            ViewBag.lblpro = lblpro;
            ViewBag.headlog = loghead;
            ViewBag.lbldesc = desc;
            ViewBag.tmode = mode;
            PaperModel repair = new PaperModel();
            var TypeList = repair.GeLeadStatusList(input: new PaperModel.ModeLead { Mode = 68 },

            companyKey: _userLoginInfo.CompanyKey);

            paperlist.ActionStatusList = TypeList.Data;



            var vehi = Common.GetDataViaQuery<PaperModel.ProviderList>(parameters: new APIParameters
            {
                TableName = "Provider",
                SelectFields = "ID_Provider AS FK_Provider ,ProvName AS PaperProviderName",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode=" + modeData + "  AND FK_Company=" + _userLoginInfo.FK_Company,

                SortFields = "ID_Provider",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey

             );
            paperlist.ProviderList = vehi.Data;
            var PaymentView = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name, PMDefault AS PMDefault",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company + "AND FK_Branch IN" + (0, _userLoginInfo.FK_Branch),
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey
            );
            paperlist.PaymentView = PaymentView.Data;
            var paper = Common.GetDataViaQuery<PaperModel.PaperList>(parameters: new APIParameters
            {
                TableName = "Paper",
                SelectFields = "ID_Paper AS FK_Paper ,PaperName AS PaperName",
                Criteria = "Cancelled=0 AND Passed=1 AND Mode=" + modeData + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Paper",
                GroupByFileds = ""
            },
               companyKey: _userLoginInfo.CompanyKey

      );
            paperlist.PaperList = paper.Data;

            ViewBag.TransMode = TransMode;

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView("_AddRenew", paperlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewPaperRenewal(PaperModel.PaperRenewalView data)
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

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            ModelState.Remove("ID_PaperMaintenanceDetails");
            ModelState.Remove("FK_PaperMaintenance");
            ModelState.Remove("FK_Paper");
            ModelState.Remove("PmTotalAmount");
            ModelState.Remove("PaperMaintenanceDetails");


            #region :: Model validation  ::

            //removing a node in model while validating
            //-- - Model validation
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


            PaperModel objprdwise = new PaperModel();


            //byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;         
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            var OtherChgDetails = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            var dataresponse = objprdwise.UpdatePaperrenewal(input: new PaperModel.PaperRenewalUpdate
            {

                UserAction = 1,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode =data.TransMode,
                Debug = 0,
                EffectDate = data.EffectDate,
                ID_PaperMaintenance = 0,
                FK_Master = data.FK_Master,
                TransDate = data.TransDate,
                PmTotalAmount = data.PmTotalAmount,
                FK_PaperMaintenance =data.FK_PaperMaintenance,
                PaperMaintenanceDetails = data.PaperMaintenanceDetails is null ? "" : Common.xmlTostring(data.PaperMaintenanceDetails),
                OtherChgDetails = OtherChgDetails is null ? "" : Common.xmlTostring(OtherChgDetails),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                TaxAmountDetails = data.TaxAmountDetails is null ? "" : Common.xmlTostring(data.TaxAmountDetails),

                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,



            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateNewPaperRenewal(PaperModel.PaperRenewalView data)
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
            var imagenull = false;
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
            if (imagenull)
            {
                data.ImageList = null;
            }

            #endregion ::  Check User Session to verifyLogin  ::

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            ModelState.Remove("ID_PaperMaintenanceDetails");
            ModelState.Remove("FK_PaperMaintenance");
            ModelState.Remove("FK_Paper");
            ModelState.Remove("PmTotalAmount");
            ModelState.Remove("PaperMaintenanceDetails");


            #region :: Model validation  ::

            //removing a node in model while validating
            //-- - Model validation
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


            PaperModel objprdwise = new PaperModel();


            //byte userAction = 1;//update : 2 | Add : 1 

            int branchCode = _userLoginInfo.FK_Branch;
            int OrgnCode = _userLoginInfo.FK_Company;
            short FK_Machine = _userLoginInfo.FK_Machine;
            string companyKey = _userLoginInfo.CompanyKey;
            long branchUserCode = _userLoginInfo.FK_BranchCodeUser;
            string entrBy = _userLoginInfo.EntrBy;
            var OtherChgDetails = Common.GetOtherCharges(data.TransMode);
            var otherChargeTax = Common.GetOtherChargeTax(data.TransMode);

            var dataresponse = objprdwise.UpdatePaperrenewal(input: new PaperModel.PaperRenewalUpdate
            {

                UserAction = 2,
                FK_Machine = FK_Machine,
                FK_BranchCodeUser = branchUserCode,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = entrBy,
                TransMode = data.TransMode,
                Debug = 0,
                EffectDate = data.EffectDate,
                ID_PaperMaintenance = data.ID_PaperMaintenance,
                FK_Master = data.FK_Master,
                TransDate = data.TransDate,
                PmTotalAmount = data.PmTotalAmount,
                FK_PaperMaintenance = data.FK_PaperMaintenance,
                PaperMaintenanceDetails = data.PaperMaintenanceDetails is null ? "" : Common.xmlTostring(data.PaperMaintenanceDetails),
                OtherChgDetails = OtherChgDetails is null ? "" : Common.xmlTostring(OtherChgDetails),
                TaxDetails = otherChargeTax is null ? "" : Common.xmlTostring(otherChargeTax),
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                TaxAmountDetails = data.TaxAmountDetails is null ? "" : Common.xmlTostring(data.TaxAmountDetails),
               LastID= (data.LastID.HasValue) ? data.LastID.Value : 0,
              




            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOtherCharges(PaperModel.BindOtherCharge Data)
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

            PurchaseModel purchaseModel = new PurchaseModel();

            var OtherCharges = purchaseModel.FillOtherCharges(input: new PurchaseModel.BindOtherCharge
            {
                TransMode = Data.TransMode,

            }, companyKey: _userLoginInfo.CompanyKey);

            var Transtypelist = Common.GetDataViaQuery<OtherChargeTypeModel.TransTypes>(parameters: new APIParameters
            {
                TableName = "TransType",
                SelectFields = "ID_TransType AS TransTypeID,TransType AS TransType",
                Criteria = "ID_TransType=1",
                SortFields = "ID_TransType",
                GroupByFileds = ""
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { OtherCharges, Transtypelist }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public ActionResult GetRenewalList(int pageSize, int pageIndex, string Name, string TransMode)
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

            PaperModel objrenew = new PaperModel();
            var data = objrenew.GetRenewalPaperSelect(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.Selectrenewalpaperdata
            {

                FK_PaperMaintenance = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                Detailed = false,
                TransMode = TransMode

            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetTaxAmount(PaperModel.BindTaxAmount Data)
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

            PaperModel SalesModel = new PaperModel();

            var datresponse = SalesModel.FillTax(input: new PaperModel.BindTaxAmount
            {
                FK_Company = _userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(datresponse, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]

        public ActionResult GetPaperRenewalInfo(PaperModel.PaperRenewalView data)
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
            PaperModel objrenew = new PaperModel();
            Common.fillOtherCharges(data.TransMode, data.ID_PaperMaintenance);
            var mptableInfo = objrenew.GetRenewalSelectData(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.GetrenewalIdDetails
            {
                FK_PaperMaintenance  = data.ID_PaperMaintenance,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = false,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode

            });

            var subtable = objrenew.GetRenewalSelectDatanew(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.GetrenewalIdDetails
            {
                FK_PaperMaintenance = data.ID_PaperMaintenance,
                Detailed = true,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode

            });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].PaperMaintenanceDetails = subtable.Data;
            }
            var OtherCharge = objrenew.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.GetSubTableSales {
                FK_Transaction = data.ID_PaperMaintenance,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });
            var paymentdetail = objrenew.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.GetPaymentin {
                FK_Master = data.ID_PaperMaintenance,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
            var Taxselect = objrenew.GetTaxDetails(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.TaxAmount {
                ID_Transaction = data.ID_PaperMaintenance,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode });

            CommonSearchPopupModel prodimg = new CommonSearchPopupModel();
            var Imageselect = prodimg.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new CommonSearchPopupModel.GetImagein
            {
                FK_Master = data.ID_PaperMaintenance,
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
            return Json(new { mptableInfo,Imageselect , OtherCharge, Taxselect, paymentdetail, subtable }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public ActionResult GetPaaperenewfill(PaperModel.PaperRenewalView data)
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
            PaperModel objrenew = new PaperModel();

            var RefillInfo = objrenew.GetRenewalfill(companyKey: _userLoginInfo.CompanyKey, input: new PaperModel.Datapaperfill
            {
                FK_Master = data.FK_Master,
                TransMode = data.TransMode,
            
            });

           

           

            return Json(RefillInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPaperRenewalDeleteReasonList()
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
        [ValidateAntiForgeryToken()]
        //delete productwise type
        public ActionResult PaperRenewalDelete(PaperModel.DeleteRenewalView data)
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



            ModelState.Remove("PrdID");
            ModelState.Remove("ServiceID");
            ModelState.Remove("ID_Mode");
            ModelState.Remove("EffectDate");
            ModelState.Remove("PeriofFrom");
            ModelState.Remove("PeriodTo");
            ModelState.Remove("ServiceCost");
            ModelState.Remove("SubProductID");
            ModelState.Remove("SubproductServiceDetails");
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

            PaperModel.DeletePaperRenewal objprddel = new PaperModel.DeletePaperRenewal
            {
                FK_PaperMaintenance = data.ID_PaperMaintenance,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = data.TransMode
            };

            Output dataresponse = Common.UpdateTableData<PaperModel.DeletePaperRenewal>(
                companyKey: _userLoginInfo.CompanyKey, procedureName: "ProPaperMaintenanceDelete", parameter: objprddel);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadRenews(string mtd,string TransMode,string vname,string vpaper,string vpano,string vpro,string vissue,string viexpir,string FK_Master)
        {
            RenewallModel vData = new RenewallModel()
            {
                NAME = vname,
                ID_Papper = vpaper,
                PapperNo = vpano,
                ID_Provider = vpro,
                IssueDate=vissue,
                ExpireDATE= viexpir,
                FK_Master= FK_Master

            };
            TempData["vData"] = vData;
            CommonMethod objCmnMethod = new CommonMethod();
            string trans = TransMode.Substring(0, 2);
            if (trans=="VL")//vehicle
            {
                TransMode ="VLPM";
            }
            else//tool
            {
                TransMode="TOPM";
            }
            string mgrp = objCmnMethod.EncryptString(TransMode);
                                 
            return RedirectToAction("Renew", "Paper", new{ mgrp}  );
        }
    }
 }
