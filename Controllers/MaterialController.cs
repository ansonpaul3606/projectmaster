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
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult Index(string mtd,string mgrp)
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
            return View();
        }
       
        public ActionResult MaterialRequestLoad(string mtd, string TransMode)
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
            MaterialModel.MaterialRequestView Materiallist = new MaterialModel.MaterialRequestView();
          
            APIParameters apiParameCate = new APIParameters
            {
                TableName = "[ProjectStages]",
                SelectFields = "[ID_ProjectStages] AS StageID,[PrStName] AS StageName",
                Criteria = "Passed=1 And Cancelled=0 AND TransMode='PRST' AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var Stages = Common.GetDataViaQuery<MaterialModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiParameCate);
            Materiallist.StageList = Stages.Data;
            var OpSList = Common.GetDataViaQuery<MaterialModel.ModeList>(parameters: new APIParameters
            {
                TableName = "ModuleType M",
                SelectFields = "M.ID_ModuleType as ModeID,M.ModuleName as ModeName,M.Mode",
                Criteria = "Mode<>'O'",
                GroupByFileds = "",
                SortFields = ""
            },
          companyKey: _userLoginInfo.CompanyKey

          );
            Materiallist.ModeList = OpSList.Data;
            ViewBag.TransMode = TransMode;          
          

            CommonMethod objCmnMethod = new CommonMethod();

            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddMaterialRequest", Materiallist);
        }
        public ActionResult GetProductList(MaterialModel.StockTransferID datas)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


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
            MaterialModel MaterialStock = new MaterialModel();

            var MaterialtransproductInfo = MaterialStock.GetStockTransferData(companyKey: _userLoginInfo.CompanyKey, input: new MaterialModel.StockTransferID
            {
                FK_Branch = datas.FK_Branch,
                FK_Department = datas.FK_Department,
                FK_Employee = datas.FK_Employee,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = datas.TransMode
            });

            return Json(MaterialtransproductInfo, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddMaterialRequest(MaterialModel.MaterialRequestView data)
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


            MaterialModel model = new MaterialModel();
            var datresponse = model.UpdateMaterialData(input: new MaterialModel.UpdateMaterialRequest
            {
                UserAction = 1,
                TransMode = data.TransMode,
                ID_ProjectMaterialRequest = data.ID_ProjectMaterialRequest,
                ProMatRequestDate = data.ProMatRequestDate,
                FK_Project = data.FK_Project,
                FK_Stages = data.FK_Stages,
                FK_Team = data.FK_Team,
                FK_Employee = data.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_ProjectMaterialRequest = data.FK_ProjectMaterialRequest,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                MaterialRequestDetailsView = data.MaterialRequestDetailsView is null ? "" : Common.xmlTostring(data.MaterialRequestDetailsView),
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateMaterialRequest(MaterialModel.MaterialRequestView data)
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


            MaterialModel model = new MaterialModel();
            var datresponse = model.UpdateMaterialData(input: new MaterialModel.UpdateMaterialRequest
            {
                UserAction = 2,
                TransMode = data.TransMode,
                ID_ProjectMaterialRequest = data.ID_ProjectMaterialRequest,
                ProMatRequestDate = data.ProMatRequestDate,
                FK_Project = data.FK_Project,
                FK_Stages = data.FK_Stages,
                FK_Team = data.FK_Team,
                FK_Employee = data.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_ProjectMaterialRequest = data.FK_ProjectMaterialRequest,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                MaterialRequestDetailsView = data.MaterialRequestDetailsView is null ? "" : Common.xmlTostring(data.MaterialRequestDetailsView),
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetProjectTeam(ProjectwiseStagesModel.ProjectwiseStagesView cr)
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
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::
            APIParameters apiSub = new APIParameters
            {
                TableName = " [ProjectTeam] join ProjectWiseStages  on ProjectWiseStages.FK_ProjectTeam=ProjectTeam.ID_ProjectTeam and ProjectWiseStages.Cancelled=0",
                SelectFields = "DISTINCT [ID_ProjectTeam] AS ID_ProjectTeam,[ProjTeamName] AS TeamName",
                Criteria = "ProjectTeam.Passed=1 And ProjectTeam.Cancelled=0 And ProjectTeam.FK_Project ='" + cr.FK_Project + "' AND FK_ProjectStages = '" + cr.FK_Stage + "' AND ProjectTeam.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<ProjectwiseStagesModel.TeamList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetProjectStages(MaterialUsageModel.MaterialUsageView cr)
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
            //if (!ModelState.IsValid)
            //{

            //    // since no need to continue just return
            //    return Json(new
            //    {
            //        Process = new Output
            //        {
            //            IsProcess = false,
            //            Message = ModelState.Values.SelectMany(m => m.Errors)
            //                            .Select(e => e.ErrorMessage)
            //                            .ToList(),
            //            Status = "Validation failed",
            //        }
            //    }, JsonRequestBehavior.AllowGet);
            //}

            #endregion :: Model validation  ::
            APIParameters apiSub = new APIParameters
            {
                TableName = "ProjectWiseStages join ProjectStages on ProjectStages.ID_ProjectStages = ProjectWiseStages.FK_ProjectStages",
                SelectFields = "distinct [ID_ProjectStages] AS ProjectStagesID,[PrStName] AS StageName",
                Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 AND TransMode='PRST' AND FK_Project ='" + cr.FK_Project + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialUsageModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }

        //public ActionResult GetProjectTeam(MaterialModel.MaterialRequestView cr)
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
        //    #region :: Model validation  ::

        //    #endregion :: Model validation  ::
        //    APIParameters apiSub = new APIParameters
        //    //{
        //    //    TableName = "ProjectWiseStages join ProjectStages on ProjectStages.ID_ProjectStages = ProjectWiseStages.FK_ProjectStages",
        //    //    SelectFields = "distinct [ID_ProjectStages] AS ProjectStagesID,[PrStName] AS StageName",
        //    //    Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 AND TransMode='PRST' AND FK_Project ='" + cr.FK_Project + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
        //    //    GroupByFileds = "",
        //    //    SortFields = ""
        //    //};

        //    //APIParameters aPI = new APIParameters
        //    {
        //        TableName = "[ProjectTeam]",
        //        SelectFields = "[ID_ProjectTeam] AS TeamID,[ProjTeamName] AS TeamName",
        //        Criteria = "Passed=1 And Cancelled=0 And FK_Project ='" + cr.FK_Project + "'" + "AND FK_Company=" + _userLoginInfo.FK_Company,
        //        GroupByFileds = "",
        //        SortFields = ""
        //    };
        //    var SubcategoryInfo = Common.GetDataViaQuery<MaterialModel.TeamList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
        //    return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        //}

        //public ActionResult GetProjectStages(MaterialModel.MaterialRequestView cr)
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
        //    #region :: Model validation  ::


        //    #endregion :: Model validation  ::
        //    APIParameters apiSub = new APIParameters
        //    {
        //        TableName = "ProjectWiseStages join ProjectStages on ProjectStages.ID_ProjectStages = ProjectWiseStages.FK_ProjectStages",
        //        SelectFields = "distinct [ID_ProjectStages] AS StageID,[PrStName] AS StageName",
        //        Criteria = "ProjectWiseStages.Passed=1 And ProjectWiseStages.Cancelled=0 And TransMode='PRST' And FK_Project ='" + cr.FK_Project + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
        //        GroupByFileds = "",
        //        SortFields = ""
        //    };
        //    var SubcategoryInfo = Common.GetDataViaQuery<MaterialModel.StageList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
        //    return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        //}
        public ActionResult GetEmployees(MaterialModel.MaterialRequestView cr)
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
                TableName = "ProjectAttendanceDetails PAD join ProjectTeam on ProjectTeam.TeamID = PAD.FK_Master join ProjectWiseStages on ProjectWiseStages.FK_ProjectTeam = PAD.FK_Master  join Employee on PAD.FK_Employee = Employee.ID_Employee",
                SelectFields = "distinct ID_Employee as EmployeeID,EmpFName as EmployeeName",
                Criteria = "PAD.Passed=1 And PAD.Cancelled=0 And PAD.TransMode='PRTC' AND ProjectWiseStages.FK_Project ='" + cr.FK_Project + "'" + "And FK_ProjectStages = '" + cr.FK_Stages + "'" + "AND ProjectWiseStages.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            };
            var SubcategoryInfo = Common.GetDataViaQuery<MaterialModel.EmployeeList>(companyKey: _userLoginInfo.CompanyKey, parameters: apiSub);
            return Json(SubcategoryInfo, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetMaterialRequestList(int pageSize, int pageIndex, string Name, string TransModes)
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
            MaterialModel employeeStock = new MaterialModel();

            var MaterialInfo = employeeStock.GetMaterialRequestData(companyKey: _userLoginInfo.CompanyKey, input: new MaterialModel.MaterialRequestID

            {
                FK_ProjectMaterialRequest = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                //Search= Name,
                TransMode = TransModes
            });

            //  return Json(EmployeetransferInfo, JsonRequestBehavior.AllowGet);
            return Json(new { MaterialInfo.Process, MaterialInfo.Data, pageSize, pageIndex, totalrecord = (MaterialInfo.Data is null) ? 0 : MaterialInfo.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
        }
             
        [HttpPost]

        public ActionResult GetMaterfialRequestInfo(MaterialModel.MaterialRequestView data)
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


            MaterialModel objprd = new MaterialModel();

            var mptableInfo = objprd.GetMaterialData(companyKey: _userLoginInfo.CompanyKey, input: new MaterialModel.MaterialRequestID
            {
                FK_ProjectMaterialRequest = data.ID_ProjectMaterialRequest,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Detailed = 0,
                EntrBy = _userLoginInfo.EntrBy,
                TransMode = data.TransMode
            });
            var subtable = objprd.GetRenewalSelectDatanew(companyKey: _userLoginInfo.CompanyKey, input: new MaterialModel.MaterialRequestID
            {
                FK_ProjectMaterialRequest = data.ID_ProjectMaterialRequest,
                Detailed = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode

            });

            if (subtable.Process.IsProcess)
            {

                mptableInfo.Data[0].MaterialRequestDetailsView = subtable.Data;
            }

            return Json(new { mptableInfo,subtable }, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteMaterialRequest(MaterialModel.MaterialRequestView data)
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

            MaterialModel Material = new MaterialModel();


            Output datresponse = Material.DeleteMaterialRequestData(input: new MaterialModel.DeleteMaterialRequest
            {
                FK_ProjectMaterialRequest = data.ID_ProjectMaterialRequest,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Reason = data.ReasonID,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                Debug = 0,
                TransMode = data.TransMode,
            }, companyKey: _userLoginInfo.CompanyKey);


            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CheckAdditionalQuantity(MaterialModel.QuantityCheck cr)
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

            #endregion :: Model validation  ::
            var QuanityCheckResult = new MaterialModel().CheckQuantity(companyKey: _userLoginInfo.CompanyKey, input: new MaterialModel.QuantityCheck
            {
                FK_Product = cr.FK_Product,
                FK_Project = cr.FK_Project,
                FK_Stage = cr.FK_Stage,
                FK_Team = cr.FK_Team,
                Quantity = cr.Quantity,

            });
            var Warnmsg = "";
            if(QuanityCheckResult.Data!=null && QuanityCheckResult.Data.Count > 0)
            {
                Warnmsg = QuanityCheckResult.Data[0].Warnmsg;

            }
            return Json(Warnmsg , JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetMaterialReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }



    }
}