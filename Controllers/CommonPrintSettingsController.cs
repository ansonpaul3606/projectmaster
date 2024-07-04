using Newtonsoft.Json;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.CommonPrintSettingsModal;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class CommonPrintSettingsController : Controller
    {
        //- -// GET: CommonPrintSettings
        public ActionResult Index()
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
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.PagedAccessRights = pageAccess;
            //ViewBag.mtd = mtd;
            return View();
        }
        public ActionResult LoadCommonPrintSettings()
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

            CommonPrintSettingsModal objAl = new CommonPrintSettingsModal();
            //AuthorizationLevelModel.AuthorizationlevelInit authorizationLevelInit = new AuthorizationLevelModel.AuthorizationlevelInit();
            CommonPrintSettingsModal.CommonPrintSettingsView MenuGroup = new CommonPrintSettingsModal.CommonPrintSettingsView();
            var MenuGroupInfo = objAl.GetModules(companyKey: _userLoginInfo.CompanyKey, input: new CommonPrintSettingsModal.ModeLead
            {
               Mode=97
            });
            MenuGroup.Modules = MenuGroupInfo.Data;
            var PageOrientaion = objAl.GetPageOrientaion(companyKey: _userLoginInfo.CompanyKey, input: new CommonPrintSettingsModal.ModeLead
            {
                Mode = 117
            });
            MenuGroup.PageOrientaions = PageOrientaion.Data;

            return PartialView("_AddCommonPrintSettings", MenuGroup);
        }

        #region [UpdateCommonPrintingSettings]
        [HttpPost]
        public ActionResult UpdateCommonPrintingSettings(step1input inputdata)
        {
           

            try
            {
                //byte[] imageBytes1 = null; 
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
                CommonPrintSettingsModal modal = new CommonPrintSettingsModal();
               // var imageBytes;

                //if (inputdata.FrntImg != null)
                //{
                //    var base64arr = inputdata.FrntImg.Split(',');
                //    var imageBytes = Convert.FromBase64String(base64arr[1]);
                //    //byte[] imageBytes = Convert.FromBase64String(blIDCardPrintingSettings.FrontImgUrl);
                //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                //    //ms.Write(imageBytes, 0, imageBytes.Length);
                //    imageBytes1 = imageBytes;
                //}

                var dataresponse = modal.UpdateCommonPrintingSettings(companyKey: _userLoginInfo.CompanyKey, input: new CommonPrintSettingsModal.UpdateCommonPrintingSettingsIp
                {
                   
                    FK_Company = _userLoginInfo.FK_Company,
                    EntrBy=_userLoginInfo.EntrBy,
                    FrontSideString=inputdata.FrontSide,
                    ImagePath=inputdata.ImagePath,
                    SettingsName=inputdata.SettingsName,
                    FK_CommonPrintingSettings=inputdata.FK_CommonPrintingSettings,
                    UserAction=inputdata.UserAction,
                    CommonPrintSettingsMode = inputdata.CommonPrintSettingsMode,
                    FrntImg= inputdata.FrntImg,
                    //FrntImg2 = imageBytes1,
                    ID_CommonPrintingSettings =inputdata.ID_CommonPrintingSettings,
                    PageSize=inputdata.PageSize
                    
                });
                return Json(dataresponse, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            { return Json(ex); }
           
        }
        #endregion

        public ActionResult DownloadPdf(string InstitutionId, string DepartmentID, string ProgramID, string MajorID)
        {
            string Message = "";
            try
            {
                Message = "Success";
                List<string> ls = new List<string>();
                Session["data"] = string.Join(",", ls);

                return Redirect("~/Reports/ReportView.aspx?id=" + string.Join(",", ls));
            }
            catch (Exception ex)
            {
                Message = "Fail";
                throw;
            }



        }
        public ActionResult GetPageSize()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            CommonPrintSettingsModal objAl = new CommonPrintSettingsModal();
            var PageOrientaion = objAl.GetPageOrientaion(companyKey: _userLoginInfo.CompanyKey, input: new CommonPrintSettingsModal.ModeLead
            {
               // Mode = 98
                Mode = 117
            });
            return Json(PageOrientaion, JsonRequestBehavior.AllowGet);

        }

        #region [GetCommonPrintElements]
        [HttpPost]
        public ActionResult GetCommonPrintElements(GetCommonPrintElement data)
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

            CommonPrintSettingsModal Modal = new CommonPrintSettingsModal();

            //var BranchList = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettingElemets>(parameters: new APIParameters
            //{
            //    TableName = "CommonPrintSettingElemets",
            //    SelectFields = "ElementName,html,Id",
            //    Criteria = "FK_Company=" + _userLoginInfo.FK_Company + " AND  IsActive = 1 AND TransMode ='Sales' AND TransType=" + 1,
            //    SortFields = "",
            //    GroupByFileds = ""
            //}, companyKey: _userLoginInfo.CompanyKey);

            //var DetailsSection1 = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettingElemets>(parameters: new APIParameters
            //{
            //    TableName = "CommonPrintSettingElemets",
            //    SelectFields = "ElementName,html,Id",
            //    Criteria = "FK_Company=" + _userLoginInfo.FK_Company + " AND  IsActive = 1 AND TransMode ='Sales' AND TransType=" + 2,
            //    SortFields = "",
            //    GroupByFileds = ""
            //}, companyKey: _userLoginInfo.CompanyKey);

            //var DetailsSection2 = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettingElemets>(parameters: new APIParameters
            //{
            //    TableName = "CommonPrintSettingElemets",
            //    SelectFields = "ElementName,html,Id",
            //    Criteria = "FK_Company=" + _userLoginInfo.FK_Company + " AND  IsActive = 1 AND TransMode ='Sales' AND TransType=" + 3,
            //    SortFields = "",
            //    GroupByFileds = ""
            //}, companyKey: _userLoginInfo.CompanyKey);

            //return Json(new { BranchList, DetailsSection1, DetailsSection2 }, JsonRequestBehavior.AllowGet);



            var MakerInfo = Modal.GetCommonPrintElements(companyKey: _userLoginInfo.CompanyKey, input: new CommonPrintSettingsModal.GetCommonPrintElement
            {
                FK_Master = 0,
                TableCount = 0,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                Mode = 1

            });

            var BranchList = MakerInfo.Data.Where(process => process.TransType == 1).ToList();
            var DetailsSection1 = MakerInfo.Data.Where(process => process.TransType == 2).ToList();
            var TaxDetailColoumWise = MakerInfo.Data.Where(process => process.TransType == 3).ToList();
            var DetailsSection2 = MakerInfo.Data.Where(process => process.TransType == 4).ToList();
            var PaymentMode = MakerInfo.Data.Where(process => process.TransType == 5).ToList();
            var BankDetails = MakerInfo.Data.Where(process => process.TransType == 6).ToList();
            var DetailsSection7 = MakerInfo.Data.Where(process => process.TransType == 7).ToList();
            var DetailsSection8 = MakerInfo.Data.Where(process => process.TransType == 8).ToList();
            var DetailsSection9 = MakerInfo.Data.Where(process => process.TransType == 9).ToList();
            var DetailsSection10 = MakerInfo.Data.Where(process => process.TransType == 9).ToList();
            return Json(new { BranchList, DetailsSection1, DetailsSection2,  TaxDetailColoumWise, PaymentMode, BankDetails, DetailsSection7, DetailsSection8, DetailsSection9,DetailsSection10,  MakerInfo.Process }, JsonRequestBehavior.AllowGet);




            //return Json(BranchList, JsonRequestBehavior.AllowGet);

            //return Json(new { Process =MakerInfo.Process, MakerInfo.Data  }, JsonRequestBehavior.AllowGet);

        }

        #endregion


        [HttpPost]
        public ActionResult SavePageLayout()
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

            CommonPrintSettingsModal Modal = new CommonPrintSettingsModal();


            //var data = Modal.SavePrintLayout(input: new CommonPrintSettingsModal.GetCommonPrintElementsIP
            //{
            //    FK_Company = _userLoginInfo.FK_Company,
            //    Module = 1
            //}, companyKey: _userLoginInfo.CompanyKey);

            // return Json(data, JsonRequestBehavior.AllowGet);
            return null;

        }
        #region[SelectCommonPrintingSettings]
        public ActionResult SelectCommonPrintingSettings(int pageSize, int pageIndex, string Name, string TransMode)
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
            IncentiveModel model = new IncentiveModel();

            CommonPrintSettingsModal modal = new CommonPrintSettingsModal();
            var data = modal.SelectCommonPrintingSettings(companyKey: _userLoginInfo.CompanyKey, input: new CommonPrintSettingsModal.SelectCommonPrintingSettingsIp
            {

                FK_Company = _userLoginInfo.FK_Company,
                FK_CommonPrintingSettings=0,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Name=Name

            });
             return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
           // return null;
        }
        #endregion

        #region[SelectCommonPrintingSettingsbyId]


        [HttpPost]
        public ActionResult SelectCommonPrintingSettingsbyId(step1input inputDel)
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

            CommonPrintSettingsModal model = new CommonPrintSettingsModal();

            var data = model.SelectCommonPrintingSettingsbyId(input: new CommonPrintSettingsModal.SelectCommonPrintingSettingsIp
            {
               
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_CommonPrintingSettings = inputDel.ID_CommonPrintingSettings,
                

               
            }, companyKey: _userLoginInfo.CompanyKey);

           // data.Data[0].StrFrntImg = GetImageFromByteArray(data.Data[0].FrntImg);

            //APIGetRecordsDynamic<IncentiveModel.IncentiveViewTable> output = new APIGetRecordsDynamic<IncentiveModel.IncentiveViewTable>();
            //output.Process = data.Process;
            //output.Data = inct;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[DeleteCommonPrintingSettings]
        [HttpPost]
        public ActionResult DeleteCommonPrintingSettings(DeleteCommonPrintingSettingsIP del)
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
            CommonPrintSettingsModal incentiveModel = new CommonPrintSettingsModal();

            var datares = incentiveModel.DeleteCommonPrintingSettings(input: new CommonPrintSettingsModal.DeleteCommonPrintingSettingsIP
            {
                
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_CommonPrintingSettings = del.FK_CommonPrintingSettings,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = del.FK_Reason,
                EntrBy = _userLoginInfo.EntrBy,
                Debug = 0,
                
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datares, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CheckCommonNameExists
        public ActionResult CheckCommonNameExists(string SettingsName)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettings>(parameters: new APIParameters
            {

                TableName = "CommonPrintingSettings",
                SelectFields = "ID_CommonPrintingSettings AS ID_CommonPrintingSettings",
                Criteria = "Cancelled =0 AND Passed=1 AND SettingsName='" + SettingsName + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion



        #region GetPrintTemplate
        public ActionResult GetPrintTemplate(Int64 FK_CommonPrintSettings)
        {
            try
            {
                string Status = string.Empty;
                DataTable dt = new DataTable();
                string FImage = string.Empty;
                string BImage = string.Empty;
               
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


                    var data = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettingsTemplateData>(parameters: new APIParameters
                    {

                        TableName = "CommonPrintTemplateData",
                        SelectFields = "ID_CommonPrintTemplateData ,Logo_image,Tittle,Terms_and_conditions,Subtotal,Address,PaymentInfo,Invoice_No,Table_data,Table_column,Box_data",
                        Criteria = "Cancelled = 0 AND Passed = 1 and FK_Company= " + _userLoginInfo.FK_Company + " and ID_CommonPrintTemplateData =" + FK_CommonPrintSettings + " ORDER BY EntrOn DESC" ,
                        SortFields = "",
                        GroupByFileds = ""
                    },
                    companyKey: _userLoginInfo.CompanyKey);

                    
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public string GetImageFromByteArray(byte[] byteData)
        {
            string imgDataURL = "";
            if (byteData != null)
            {
                 
                string imreBase64Data = Convert.ToBase64String(byteData);
                imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            }
            return imgDataURL;
        }
        

        #region GetPrintData

        /// </summary>
        /// <param name="ID_CommonPrintTemplateData"></param>
        /// <returns></returns>
        public ActionResult GetPrintData(Int64 ID_CommonPrintTemplateData)
        {
            try
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
                CommonPrintSettingsModal modal = new CommonPrintSettingsModal();


                var data = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintTemplateData>(parameters: new APIParameters
                {

                    TableName = "CommonPrintTemplateData",
                    SelectFields = "ID_CommonPrintTemplateData,Logo_image,Tittle,Terms_and_conditions,Subtotal,Address,PaymentInfo,Invoice_No,Table_data",
                    Criteria = "Cancelled =0 AND Passed=1 AND ID_CommonPrintTemplateData='" + ID_CommonPrintTemplateData + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "",
                    GroupByFileds = ""
                },
                companyKey: _userLoginInfo.CompanyKey);

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region [GetTemplate]

        /// </summary>
        /// <param name="ID_CommonPrintTemplateData"></param>
        /// <returns></returns>
        public ActionResult GetTemplate(GetTemplateIp inputdata)
        {
            try
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
                CommonPrintSettingsModal modal = new CommonPrintSettingsModal();
                var tepltId = Common.GetDataViaQuery<CommonPrintSettingsModal.CommonPrintSettings>(parameters: new APIParameters
                {

                    TableName = "CommonPrintingSettings",
                    SelectFields = "TOP(1) ID_CommonPrintingSettings",
                    Criteria = "Cancelled =0 AND Passed=1 AND CommonPrintSettingsMode='" + inputdata.TransMode + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company ,
                    SortFields = "ID_CommonPrintingSettings DESC",
                    GroupByFileds = ""
                },
                companyKey: _userLoginInfo.CompanyKey);
                if (tepltId.Data != null)
                {
                    var data = Common.GetDataViaQuery<CommonPrintSettingsModal.GetTemplateCommonPrintOp>(parameters: new APIParameters
                    {

                        TableName = "CommonPrintingSettings",
                        SelectFields = "FrontSideString,FrntImg,PageSize",
                        Criteria = "Cancelled =0 AND CommonPrintSettingsMode='" + inputdata.TransMode + "'" + " AND FK_Company=" + _userLoginInfo.FK_Company + "AND ID_CommonPrintingSettings=" + tepltId.Data[0].ID_CommonPrintingSettings,
                        SortFields = "",
                        GroupByFileds = ""
                    },
                companyKey: _userLoginInfo.CompanyKey);

                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        Process = new Output
                        {
                            IsProcess = false,
                            Message = new List<string> { "" },
                            Status = "No Print Template Found.",
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
         
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public ActionResult GetCommonPrintModule()
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
            CommonPrintSettingsModal objAl = new CommonPrintSettingsModal();
            
            CommonPrintSettingsModal.CommonPrintSettingsView MenuGroup = new CommonPrintSettingsModal.CommonPrintSettingsView();
            var MenuGroupInfo = objAl.GetModules(companyKey: _userLoginInfo.CompanyKey, input: new CommonPrintSettingsModal.ModeLead
            {
                Mode = 97
            });

            return Json(MenuGroupInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetinvoiceData(GetinvoiceDataIp inputdata)
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
            CommonPrintSettingsModal objAl = new CommonPrintSettingsModal();

            CommonPrintSettingsModal.CommonPrintSettingsView MenuGroup = new CommonPrintSettingsModal.CommonPrintSettingsView();
            var MenuGroupInfo = objAl.GetinvoiceData(companyKey: _userLoginInfo.CompanyKey, input: new CommonPrintSettingsModal.GetinvoiceDataIp
            {
                FK_Master = inputdata.FK_Master,
                TransMode=inputdata.TransMode
            });

            return Json(MenuGroupInfo, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]

        public ActionResult ProjectBilling_Invoice(CommonPrintSettingsModal.ProjectBillingInvoiceIP data)
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


            CommonPrintSettingsModal modal = new CommonPrintSettingsModal();
            //var data1 = modal.Pro_ProjectBillingInvoice_table1(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            //{
            //    FK_Master = data.FK_Master,
            //    TableCount = 1,
            //    TransMode = data.TransMode,
            //    FK_Branch = _userLoginInfo.FK_BranchCodeUser,
            //    FK_Company = _userLoginInfo.FK_Company,

            //}, companyKey: _userLoginInfo.CompanyKey);
            var data1 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 1,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata1 = JsonConvert.SerializeObject(data1, Formatting.Indented);
            var data2 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 2,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata2 = JsonConvert.SerializeObject(data2, Formatting.Indented);
            var data3 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 3,
                 TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,  
                FK_Company= _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);            
            string jsondata3 = JsonConvert.SerializeObject(data3, Formatting.Indented);
            var data4 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 4,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata4 = JsonConvert.SerializeObject(data4, Formatting.Indented);
            var data5 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 5,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata5 = JsonConvert.SerializeObject(data5, Formatting.Indented);
            var data6 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 6,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata6 = JsonConvert.SerializeObject(data6, Formatting.Indented);
            var data7 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 7,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata7 = JsonConvert.SerializeObject(data7, Formatting.Indented);
            var data8 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 8,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata8 = JsonConvert.SerializeObject(data8, Formatting.Indented);
            return Json(new { jsondata1, jsondata2, jsondata3, jsondata4, jsondata5, jsondata6, jsondata7, jsondata8 }, JsonRequestBehavior.AllowGet);


        }
        //////////////////////////////hk datatab test////////////////
        //public ActionResult Sales_Invoice(CommonPrintSettingsModal.ProjectBillingInvoiceIP data)
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


        //    CommonPrintSettingsModal modal = new CommonPrintSettingsModal();
        //    var data1 = modal.Pro_ProjectBillingInvoice_table1(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
        //    {
        //        FK_Master = data.FK_Master,
        //        TableCount = 1,
        //        TransMode = data.TransMode,
        //        FK_Branch = _userLoginInfo.FK_BranchCodeUser,
        //        FK_Company = _userLoginInfo.FK_Company,

        //    }, companyKey: _userLoginInfo.CompanyKey);

        //    var data2 = modal.Pro_ProjectBillingInvoice_table2(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
        //    {
        //        FK_Master = data.FK_Master,
        //        TableCount = 2,
        //        TransMode = data.TransMode,
        //        FK_Branch = _userLoginInfo.FK_BranchCodeUser,
        //        FK_Company = _userLoginInfo.FK_Company,

        //    }, companyKey: _userLoginInfo.CompanyKey);

        //    var data3 = modal.Pro_ProjectBillingInvoice_table3(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
        //    {
        //        FK_Master = data.FK_Master,
        //        TableCount = 3,
        //        TransMode = data.TransMode,
        //        FK_Branch = _userLoginInfo.FK_BranchCodeUser,
        //        FK_Company = _userLoginInfo.FK_Company,

        //    }, companyKey: _userLoginInfo.CompanyKey);

        //    var data4 = modal.Pro_ProjectBillingInvoice_table2(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
        //    {
        //        FK_Master = data.FK_Master,
        //        TableCount = 4,
        //        TransMode = data.TransMode,
        //        FK_Branch = _userLoginInfo.FK_BranchCodeUser,
        //        FK_Company = _userLoginInfo.FK_Company,

        //    }, companyKey: _userLoginInfo.CompanyKey);


        //    return Json(new { data1, data2, data3, data4 }, JsonRequestBehavior.AllowGet);


        //}
        ///////////////////////////////////hk edit end///////////////////////////////////
        public ActionResult Sales_Invoice(CommonPrintSettingsModal.ProjectBillingInvoiceIP data)
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


            CommonPrintSettingsModal modal = new CommonPrintSettingsModal();
            //var data1 = modal.Pro_ProjectBillingInvoice_table1(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            //{
            //    FK_Master = data.FK_Master,
            //    TableCount = 1,
            //    TransMode = data.TransMode,
            //    FK_Branch = _userLoginInfo.FK_BranchCodeUser,
            //    FK_Company = _userLoginInfo.FK_Company,

            //}, companyKey: _userLoginInfo.CompanyKey);
            var table1 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 1,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata1 = JsonConvert.SerializeObject(table1, Formatting.Indented);
            var table2 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 2,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata2 = JsonConvert.SerializeObject(table2, Formatting.Indented);
            var table3 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 3,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);
            string jsondata3 = JsonConvert.SerializeObject(table3, Formatting.Indented);
            var table4 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 4,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);

            string jsondata4 = JsonConvert.SerializeObject(table4, Formatting.Indented);
            var table5 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 5,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);

            string jsondata5 = JsonConvert.SerializeObject(table5, Formatting.Indented);
            var table6 = modal.Pro_ProjectBillingInvoice_table(input: new CommonPrintSettingsModal.ProjectBillingInvoiceIP
            {
                FK_Master = data.FK_Master,
                TableCount = 6,
                TransMode = data.TransMode,
                FK_Branch = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,

            }, companyKey: _userLoginInfo.CompanyKey);

            string jsondata6 = JsonConvert.SerializeObject(table6, Formatting.Indented);
            //return Json(new { data1, data2, data3, data4 }, JsonRequestBehavior.AllowGet);
            return Json(new { jsondata1, jsondata2, jsondata3, jsondata4 , jsondata5 , jsondata6 }, JsonRequestBehavior.AllowGet);


        }

    }

}