using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.CommonIntimationSendModels;

namespace PerfectWebERP.Controllers
{
    public class CommonIntimationSendController : Controller
    {
        // GET: CommonIntimationSend
        public ActionResult Index(string mtd)
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
            CommonMethod cmnmethd = new CommonMethod();
           // string mGrp = cmnmethd.DecryptString(mgrp);
            ViewBag.mtd = mtd;
           // ViewBag.TransMode = mGrp;
            Common.ClearOtherCharges(ViewBag.TransMode);
            return View();
        }

        
        public ActionResult LoadIntimationSend(string mtd)
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


            CommonIntimationSendModels model = new CommonIntimationSendModels();
            CommonIntimationSendModels.CommonIntimationSendModelsView inc = new CommonIntimationSendModels.CommonIntimationSendModelsView();

            var Branch = Common.GetDataViaQuery<CommonIntimationSendModels.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "BrName Name ,ID_Branch ",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
           );
            inc.BranchList = Branch.Data;

            //APIParameters apiParameCate = new APIParameters
            //{
            //    TableName = "[ProjectStages]",
            //    SelectFields = "[ID_ProjectStages] AS ProjectStagesID,[PrStName] AS StageName",
            //    Criteria = "Passed=1 And Cancelled=0 AND FK_Company=" + _userLoginInfo.FK_Company,
            //    GroupByFileds = "",
            //    SortFields = ""
            //};


            //var Stages = Common.GetDataViaQuery<ProjectTransactionModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            //inc.StageList = Stages.Data;


            var Modules = model.GetModules(companyKey: _userLoginInfo.CompanyKey, input: new CommonIntimationSendModels.ModeLead
            {
                Mode = 107
            });
            inc.ModulesList = Modules.Data;


            //////
            var LeadFrmModelist = Common.GetDataViaQuery<LeadGenerateModel.LeadFrom>(parameters: new APIParameters
            {
                TableName = "LeadFrom",
                SelectFields = "ID_LeadFrom  ,LeadFromName",
                Criteria = "",
                SortFields = "ID_LeadFrom",
                GroupByFileds = ""
            },
       companyKey: _userLoginInfo.CompanyKey

          );
            inc.LeadFromList = LeadFrmModelist.Data;

            //            var NextAcStatusList = Common.GetDataViaQuery<LeadManagementModel.ActionStatus>(parameters: new APIParameters
            //            {
            //                TableName = "ActionStatus",
            //                SelectFields = "ID_ActionStatus ,ActionStatusName",
            //                Criteria = "FK_ActionModule=1",
            //                SortFields = "ID_ActionStatus",
            //                GroupByFileds = ""
            //            },
            //companyKey: _userLoginInfo.CompanyKey

            //   );
            //            LeadMgntObj.ActionStatusList = NextAcStatusList.Data;
            inc.FK_Employee = _userLoginInfo.FK_Employee;
            var EmpName = Common.GetDataViaQuery<LeadGenerateModel.EmployeeInfo>(parameters: new APIParameters
            {


                TableName = "Employee",
                SelectFields = "ID_Employee,EmpFName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""


            },
      companyKey: _userLoginInfo.CompanyKey

         );
            inc.EmployeeInfoList = EmpName.Data;

            var CatLeadgen = "";
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    CatLeadgen = "AND CatLeadGenerate = 1";
                    break;
                case "2":
                    CatLeadgen = "";
                    break;
                default:
                    CatLeadgen = "AND CatLeadGenerate = 1";
                    break;
            }

            var Category = Common.GetDataViaQuery<LeadManagementModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1 " + CatLeadgen + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey);
            inc.CategoryList = Category.Data;

            var NextAcList = Common.GetDataViaQuery<LeadManagementModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction ,NxtActnName",
                Criteria = "FK_ActionModule=1 AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_NextAction",
                GroupByFileds = ""
            },
companyKey: _userLoginInfo.CompanyKey

 );
            inc.NxtActionList = NextAcList.Data;

            var actiontypelist = Common.GetDataViaQuery<LeadManagementModel.ActionTypes>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType,ActnTypeName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 AND FK_ActionModule=1",

                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            inc.Actntyplists = actiontypelist.Data;

            var CompanyList = Common.GetDataViaQuery<LeadManagementModel.LeadManagementViewList>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName AS CompName",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",

                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            inc.CompName = CompanyList.Data[0].CompName;

            var BranchList1 = Common.GetDataViaQuery<LeadManagementModel.LeadManagementViewList>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch,BrName",
                Criteria = "ID_Branch=" + _userLoginInfo.FK_Branch + " AND  cancelled = 0 AND Passed = 1 ",

                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            inc.BrName = BranchList1.Data[0].BrName;

            LeadManagementModel objLead = new LeadManagementModel();


            //var AcStatusList = objLead.GeLeadStatusList(input: new LeadManagementModel.ModeLead { Mode = 1 }, companyKey: _userLoginInfo.CompanyKey);

            //LeadMgntObj.ActionStatusList = AcStatusList.Data;
            //string pageName = "";
            //switch (_userLoginInfo.CompCategory)
            //{
            //    case "1":
            //        pageName = "_AddLeadManagementForm";
            //        break;
            //    case "2":
            //        pageName = "_AddLeadManagementFormTravel";
            //        break;
            //    case "7":
            //        pageName = "_AddLeadManagementFormCooler";
            //        break;
            //    default:
            //        pageName = "_AddLeadManagementForm";
            //        break;
            //}
            //CommonModel objComm = new CommonModel();
            //var genSettings = objComm.GetGeneralSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.GetGeneralSettings
            //{
            //    FK_Company = _userLoginInfo.FK_Company,
            //    GsModule = "LFLR"
            //});
            //ViewBag.LeadRequest = genSettings.Data[0].GsValue;

            inc.UserCode = _userLoginInfo.EntrBy;
            ///////

            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;

            ViewBag.Manager = _userLoginInfo.UsrrlManager;
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.Employee = _userLoginInfo.UserName;
            ViewBag.UserCode = _userLoginInfo.EntrBy;
            //Common.ClearOtherCharges(TransMode);
            return PartialView("_AddCommonIntimationSend", inc);
        }

        #region GetStage
        [HttpPost]
        public ActionResult GetStages(getStagesIP input)
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


            APIParameters apiSub = new APIParameters
            {
                TableName = "ProjectWiseStages join ProjectStages on ProjectStages.ID_ProjectStages = ProjectWiseStages.FK_ProjectStages",
                SelectFields = "distinct [ID_ProjectStages] AS StageID,[PrStName] AS StageName",
                Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 And FK_Project ='" + input.FK_Project + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialRequestReportsModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);


            // var Stages = Common.GetDataViaQuery<ProjectTransactionModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);


            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region[LoadProjectTransactionList]
        public ActionResult LoadCommonIntimationList(int pageSize, int pageIndex, string Name, string TransMode)
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

            CommonIntimationSendModels modal = new CommonIntimationSendModels();
            var data = modal.LoadCommonIntimationList(companyKey: _userLoginInfo.CompanyKey, input: new CommonIntimationSendModels.LoadCommonIntimationListIp
            {

                FK_Company = _userLoginInfo.FK_Company,
                Name = Name,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = TransMode,
                FK_commonIntimation = 0
                

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            // return null;
        }
        #endregion

        #region[IntimationSearchSelect]
        [HttpPost]
        public ActionResult SelectIntimationSearchSelect(IntimationSearchSelect inputDel)
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

            CommonIntimationSendModels model = new CommonIntimationSendModels();
            inputDel.FK_Company = _userLoginInfo.FK_Company;
            inputDel.PageIndex = 0;
            inputDel.PageSize = 0;
            inputDel.EntrBy = _userLoginInfo.EntrBy;
            inputDel.FK_Machine = _userLoginInfo.FK_Machine;
            inputDel.FK_Branch = _userLoginInfo.FK_BranchCodeUser;

            inputDel.SearchBydetails = "";
       
            var data = model.LoadCommonIntimationSearchList(inputDel, companyKey: _userLoginInfo.CompanyKey);



            // return Json(data, JsonRequestBehavior.AllowGet);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region[SelectProjectTransactionbyId]
        [HttpPost]
        public ActionResult SelectCommonIntimationbyId(LoadCommonIntimationListIp inputDel)
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

            CommonIntimationSendModels model = new CommonIntimationSendModels();

            var data = model.LoadCommonIntimationList(input: new CommonIntimationSendModels.LoadCommonIntimationListIp
            {

                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_commonIntimation = inputDel.FK_commonIntimation,
                Name = "",
                TransMode = inputDel.TransMode

            }, companyKey: _userLoginInfo.CompanyKey);

           

            // return Json(data, JsonRequestBehavior.AllowGet);
            return Json( data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region[DeleteIntimation]
        [HttpPost]
        public ActionResult DeleteIntimation(DeleteIntimationIP del)
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
            CommonIntimationSendModels incentiveModel = new CommonIntimationSendModels();

            var datares = incentiveModel.DeleteIntimation(input: new CommonIntimationSendModels.DeleteIntimationIP
            {

                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Reason = del.FK_Reason,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Branch = _userLoginInfo.FK_Branch,
                TransMode = del.TransMode,
                FK_commonIntimation = del.FK_commonIntimation

            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datares, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SaveIntimation
        [HttpPost]
        public ActionResult SaveIntimation(SaveIntimationIP inputdata)
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
            CommonIntimationSendModels incentiveModel = new CommonIntimationSendModels();

            byte[] bytes; string path1="";string docPath="";
            if (inputdata.FileUrlObj != null)
            {
                string dummyData="";
                var DocumentImage = inputdata.FileUrlObj.FileUrl;
                var extension = inputdata.FileUrlObj.extension;
                string trimmedBase64="";
                if (inputdata.FileUrlObj.FileUrl != null)

                {
                  
                    dummyData = inputdata.FileUrlObj.FileUrl.Trim();
                    dummyData=dummyData.Replace(" ", "+");
                    int commaIndex = dummyData.IndexOf(',');
                    if (commaIndex >= 0)
                    {
                         trimmedBase64 = dummyData.Substring(commaIndex + 1);
                    }

               
                    var DocsavePath = "IntimationDocuments";
                    var  DocsavePath2 = Server.MapPath("IntimationDocuments");
                    bytes = Convert.FromBase64String(trimmedBase64);
                    bool exists = System.IO.Directory.Exists(Server.MapPath(DocsavePath));
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath(DocsavePath));
                    }
                    string ModuleName = "";
                    switch (inputdata.Module)
                    {
                        case 1:
                            ModuleName = "Customer";
                            break;
                        case 2:
                            ModuleName = "Lead";
                            break;
                        case 3:
                            ModuleName = "Service";
                            break;
                        case 4:
                            ModuleName = "Inventory";
                            break;
                        default:
                            break;
                    }

                    string _Q5 = ModuleName + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "__" + Guid.NewGuid().ToString();

                    String path = System.Web.HttpContext.Current.Server.MapPath("/" + DocsavePath); //Path

                    string file2 = Path.Combine(DocsavePath2, _Q5 + "." + inputdata.FileUrlObj.extension);


                    Uri fullUri = new Uri(file2);
                    Uri referenceUri = new Uri(Server.MapPath("/"));

                    Uri relativeUri = referenceUri.MakeRelativeUri(fullUri);

                    var return_path= Uri.UnescapeDataString(relativeUri.ToString());

                    var return_path_1 = ConfigurationManager.AppSettings["WebsiteURL"] + return_path;

                    if (bytes.Length > 0)
                    {
                        using (var stream = new FileStream(file2, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                            stream.Flush();
                        }
                    }
                    docPath = return_path_1;
                };
               


            }

            var datares = incentiveModel.SaveIntimation(input: new CommonIntimationSendModels.SaveIntimationIP2
            {
                UserAction=1,
                Channel =inputdata.Channel,
                Attachment = docPath,
                Date=inputdata.Date,
                DLId=inputdata.DLId,
                FK_Company=_userLoginInfo.FK_Company,
                Message=inputdata.Message,
                Module=inputdata.Module,
                SheduledType=inputdata.SheduledType,
                Subject=inputdata.Subject,
               // Unicode =inputdata.UniqueCode,
                Branch=inputdata.Branch,
                SheduledDate=inputdata.SheduledDate,
                SheduledTime= inputdata.SheduledTime,
                FK_BranchCodeUser=_userLoginInfo.FK_BranchCodeUser,
                FK_CommonIntimation=0,
                EntrBy=_userLoginInfo.EntrBy,
                Status=0,
                ID_CommonIntimation=0,
                FK_ActionType = inputdata.FK_ActionType,
                Area_ID = inputdata.Area_ID,
                FK_Category = inputdata.FK_Category,
                FK_Priority = inputdata.FK_Priority,
                FK_Employee = inputdata.FK_Employee,
                FK_LeadThrough = inputdata.FK_LeadThrough,
                FK_NetAction = inputdata.FK_NetAction,
                Collectedby_ID = inputdata.Collectedby_ID,
                FromDate = inputdata.FromDate,
                ToDate = inputdata.ToDate,
                ID_LeadFrom =  inputdata.ID_LeadFrom,
                ID_Product = inputdata.ID_Product,
                ProdType = inputdata.ProdType,
                SearchBy = inputdata.SearchBy,
                SearchBydetails = inputdata.SearchBydetails,
                LeadDetails = inputdata.LeadDetails is null ? "" : Common.xmlTostring(inputdata.LeadDetails),
                GridData = inputdata.GridData

               
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datares, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region SaveIntimationLead
        [HttpPost]
        public ActionResult SaveIntimationLaed(SaveIntimationLead inputdata)
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
            CommonIntimationSendModels incentiveModel = new CommonIntimationSendModels();

            byte[] bytes; string path1 = ""; string docPath = "";
            if (inputdata.FileUrlObj != null)
            {
                string dummyData = "";
                var DocumentImage = inputdata.FileUrlObj.FileUrl;
                var extension = inputdata.FileUrlObj.extension;
                string trimmedBase64 = "";
                if (inputdata.FileUrlObj.FileUrl != null)

                {

                    dummyData = inputdata.FileUrlObj.FileUrl.Trim();
                    dummyData = dummyData.Replace(" ", "+");
                    int commaIndex = dummyData.IndexOf(',');
                    if (commaIndex >= 0)
                    {
                        trimmedBase64 = dummyData.Substring(commaIndex + 1);
                    }

                    var DocsavePath = ConfigurationManager.AppSettings["DocSavePath"];
                    DocsavePath += "/IntimationDocuments/";
                    bytes = Convert.FromBase64String(trimmedBase64);
                    bool exists = System.IO.Directory.Exists(DocsavePath);
                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(DocsavePath);
                    }
                    string ModuleName = "";
                    switch (inputdata.Module)
                    {
                        case 1:
                            ModuleName = "Customer";
                            break;
                        case 2:
                            ModuleName = "Lead";
                            break;
                        case 3:
                            ModuleName = "Service";
                            break;
                        case 4:
                            ModuleName = "Inventory";
                            break;
                        default:
                            break;
                    }

                    string _Q5 = ModuleName + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "__" + Guid.NewGuid().ToString();


                    string file2 = Path.Combine(DocsavePath, _Q5 + "." + inputdata.FileUrlObj.extension);

                    if (bytes.Length > 0)
                    {
                        using (var stream = new FileStream(file2, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                            stream.Flush();
                        }
                    }
                    docPath = file2;
                };
               


            }

            var datares = incentiveModel.SaveIntimation_Lead(input: new CommonIntimationSendModels.SaveIntimationLead2
            {
                UserAction = 1,
                Channel = inputdata.Channel,
                Attachment = docPath,
                Date = inputdata.Date,
                DLId = inputdata.DLId,
                FK_Company = _userLoginInfo.FK_Company,
                Message = inputdata.Message,
                Module = inputdata.Module,
                SheduledType = inputdata.SheduledType,
                Subject = inputdata.Subject,
                // Unicode =inputdata.UniqueCode,
                Branch = inputdata.Branch,
                SheduledDate = inputdata.SheduledDate,
                SheduledTime = inputdata.SheduledTime,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_CommonIntimation = 0,
                EntrBy = _userLoginInfo.EntrBy,
                Status = 0,
                checklist=inputdata.Checklist,
                ID_CommonIntimation = 0
            }, companyKey: _userLoginInfo.CompanyKey);

            return Json(datares, JsonRequestBehavior.AllowGet);
        }
        #endregion


        private static string GetMimeTypeFromBase64(string base64String)
        {
            byte[] data = Convert.FromBase64String(base64String);

            if (data.Length > 4 && data[0] == 0x25 && data[1] == 0x50 && data[2] == 0x44 && data[3] == 0x46)
            {
                return "pdf";
            }

   
            if (data.Length > 2 && data[0] == 0xFF && data[1] == 0xD8)
            {
                return "jpeg";
            }
            else if (data.Length > 3 && data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
            {
                return "png";
            }
            else if (data.Length > 3 && data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x38)
            {
                return "gif";
            }

          
            return "unknown";
        }

        #region[GetRequestLeadSearch]
        public ActionResult GetRequestLeadSearch(CommonIntimationSendModels.SendLeadRequestInput inputdata)
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

            CommonIntimationSendModels common = new CommonIntimationSendModels();

            var searchInfo = common.GetSearchRequestdata(companyKey: _userLoginInfo.CompanyKey, input: new CommonIntimationSendModels.SendLeadRequestInput
            {

                FK_ActionType = inputdata.FK_ActionType,
                Area_ID = inputdata.Area_ID,
                FK_Category = inputdata.FK_Category,
                FK_Priority = inputdata.FK_Priority,
                FK_Employee = inputdata.FK_Employee,
                FK_LeadThrough = inputdata.FK_LeadThrough,
                FK_NetAction = inputdata.FK_NetAction,
                Collectedby_ID = inputdata.Collectedby_ID,
                FromDate = inputdata.FromDate,
                ToDate = inputdata.ToDate,
                ID_LeadFrom = inputdata.ID_LeadFrom,
                ID_Product = inputdata.ID_Product,
                ProdType = inputdata.ProdType,
                SearchBy = inputdata.SearchBy,
                SearchBydetails = inputdata.SearchBydetails,
                TransMode = inputdata.TransMode,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = inputdata.FK_BranchCodeUser,
                ProdName = inputdata.ProdName

            });

            var jsonResult = Json(new { searchInfo.Process, searchInfo }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = Int32.MaxValue;
            return jsonResult;
        }
        #endregion

    }
}