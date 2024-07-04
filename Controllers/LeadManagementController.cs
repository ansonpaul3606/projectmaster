/*----------------------------------------------------------------------
Created By	: Haseena K
Created On	: 03/02/2022
Purpose		: LeadManagement
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
using System.Configuration;
using System.Text;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class LeadManagementController : Controller
    {
        public ActionResult Index(string mtd,string mgrp,int tid=1)
        {           
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
          
            
            ViewBag.isAdmin = _userLoginInfo.UsrrlAdmin;
            ViewBag.ID_Users = _userLoginInfo.ID_Users;
            ViewBag.EntrBy = _userLoginInfo.EntrBy;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.mtd = mtd;
            ViewBag.tid = tid;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;

            //   ViewBag.TransMode = TransModeSettings.GetTransMode(Convert.ToString(Session["MenuGroupID"]), ControllerContext.RouteData.GetRequiredString("controller"), ControllerContext.RouteData.GetRequiredString("action"), _userLoginInfo.CompanyKey, _userLoginInfo.FK_Company);
            string pageName = "";
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    pageName = "Index";
                    break;
                case "2":
                    pageName = "IndexTravel";
                    break;
                case "7":
                    pageName = "IndexCooler";
                    break;
                default:
                    pageName = "Index";
                    break;
            }
            return View(pageName);
        }

        public ActionResult LoadFormLeadManagement(string mtd,int? tid)
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

            LeadManagementModel.LeadManagementViewList LeadMgntObj = new LeadManagementModel.LeadManagementViewList();
            LeadMgntObj.UserCode = _userLoginInfo.EntrBy;
            LeadMgntObj.Branch = _userLoginInfo.FK_Branch;
            ViewBag.isAdmin = _userLoginInfo.UsrrlAdmin;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;
            // if any data need to be displayed with the form, then get the data and pass it a model with the partial view and handle that data in view

            //CustomerModel.CustomerFormViewModel formViewModel = new CustomerModel.CustomerFormViewModel();
            //return PartialView("_CustomerForm",formViewModel);

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
            LeadMgntObj.LeadFromList = LeadFrmModelist.Data;

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
            LeadMgntObj.FK_Employee = _userLoginInfo.FK_Employee;
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
            LeadMgntObj.EmployeeInfoList = EmpName.Data;

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
            LeadMgntObj.CategoryList = Category.Data;

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
            LeadMgntObj.NxtActionList = NextAcList.Data;

            var actiontypelist = Common.GetDataViaQuery<LeadManagementModel.ActionTypes>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType,ActnTypeName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company+ "AND  cancelled = 0 AND Passed = 1 AND FK_ActionModule=1",

                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            LeadMgntObj.Actntyplists = actiontypelist.Data;

            var CompanyList = Common.GetDataViaQuery<LeadManagementModel.LeadManagementViewList>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName AS CompName",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",

                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
          LeadMgntObj.CompName = CompanyList.Data[0].CompName;

            var BranchList = Common.GetDataViaQuery<LeadManagementModel.LeadManagementViewList>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch,BrName",
                Criteria = "ID_Branch=" + _userLoginInfo.FK_Branch + " AND  cancelled = 0 AND Passed = 1 ",

                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
            LeadMgntObj.BrName = BranchList.Data[0].BrName;

            LeadManagementModel objLead = new LeadManagementModel();


            //var AcStatusList = objLead.GeLeadStatusList(input: new LeadManagementModel.ModeLead { Mode = 1 }, companyKey: _userLoginInfo.CompanyKey);

            //LeadMgntObj.ActionStatusList = AcStatusList.Data;
            string pageName = "";
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    pageName = "_AddLeadManagementForm";
                    break;
                case "2":
                    pageName = "_AddLeadManagementFormTravel";
                    break;
                case "7":
                    pageName = "_AddLeadManagementFormCooler";
                    break;
                default:
                    pageName = "_AddLeadManagementForm";
                    break;
            }
            //CommonModel objComm = new CommonModel();
            //var genSettings = objComm.GetGeneralSettingsData(companyKey: _userLoginInfo.CompanyKey, input: new CommonModel.GetGeneralSettings
            //{
            //    FK_Company = _userLoginInfo.FK_Company,
            //    GsModule = "LFLR"
            //});

            //if (genSettings.Data != null)
            //{
            //    ViewBag.LeadRequest = genSettings.Data[0].GsValue;
            //}
            //else
            //{
            //    ViewBag.LeadRequest = false;
            //}

            var data = Common.GetDataViaQuery<LeadGenerateModel.MRPEdit>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'LEAD' AND FK_Company =" + _userLoginInfo.FK_Company + "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='LEAD002'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);

            if (data.Data[0].GsValue == true)
            {
                ViewBag.LeadRequest = data.Data[0].GsValue;
            }
            else
            {
                ViewBag.LeadRequest = false;
            }
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            ViewBag.tid = tid;
            ViewBag.Admin = _userLoginInfo.UsrrlAdmin;
            ViewBag.Manager = _userLoginInfo.UsrrlManager;

            return PartialView(pageName, LeadMgntObj);
        }
        #region CategorybasedonProject/Product
        public ActionResult GetCategoryList(Int32 ProdType)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

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

            var data = Common.GetDataViaQuery<LeadManagementModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1 " + CatLeadgen + " AND FK_Company=" + _userLoginInfo.FK_Company+" AND Project="+ ProdType,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
           

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public ActionResult GetLeadFromList(Int32 ID_LeadFrom)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LeadGenerateModel objfld = new LeadGenerateModel();


            var LeadbyData = objfld.GetLeadTrough(input: new LeadGenerateModel.GetLeadBy
            {
                TransMode = "",
                ID_LeadFrom = ID_LeadFrom,
                EntrBy = _userLoginInfo.EntrBy,
                PageIndex = 0,
                PageSize = 10,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine
            },
             companyKey: _userLoginInfo.CompanyKey);

            return Json(LeadbyData, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetLeadGenListOnLoad(DateTime today)
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

            LeadManagementModel leadMgt = new LeadManagementModel();
            ViewBag.isAdmin = _userLoginInfo.UsrrlAdmin;

            var outputList = leadMgt.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadManagementModel.GetLeadGen
            {
                TransMode = "",
                FK_LeadFrom = 0,// _userLoginInfo.FK_Branch,
                FK_LeadBy = 0,//_userLoginInfo.FK_BranchCodeUser,
                FromDate = null,   //today,//today 12 am 
                ToDate = null,  //today.AddDays(1),//roday night 
                FK_Product = 0,
                ProjectName = "",
                Status = 0,
                FK_Employee = _userLoginInfo.FK_Employee,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = 0,
                PageSize = 0,
                Filterid = 0
            });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
      
        public ActionResult GetLeadGenList(LeadManagementModel.GetLeadGen data)
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
            ViewBag.isAdmin = _userLoginInfo.UsrrlAdmin;
            LeadManagementModel leadMgt = new LeadManagementModel();

          
                var outputList = leadMgt.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadManagementModel.GetLeadGen
            {
                TransMode = "",
                FK_LeadFrom = data.FK_LeadFrom,
                FK_LeadBy = data.FK_LeadBy,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Product = data.FK_Product,
                ProjectName = data.ProjectName,
                ProductType=data.ProductType,
                    //  Status = data.Status
                    FK_Employee = data.FK_Employee,
                 FK_Category = data.FK_Category,
                Collectedby_ID = data.Collectedby_ID,
                Area_ID = data.Area_ID,
                 FK_NetAction = data.FK_NetAction,
                 FK_ActionType = data.FK_ActionType,
                 FK_Priority = data.FK_Priority,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                PageIndex = data.PageIndex+1,
                PageSize = data.PageSize,
                Filterid = data.Filterid,
                    SearchBy= data.SearchBy,
                    SearchBydetails = data.SearchBydetails,
                  ExpectedFrom=data.ExpectedFrom,
                  ExpectedTo=data.ExpectedTo
                });
            return Json(new { outputList.Process, outputList.Data, data.PageSize, data.PageIndex, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetLeadActionInfo(LeadGenerateActionModel.LeadFollowup LeadGenerateActionInfo)
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

            // if removing a node in model while validating do it above #region Model Validation and  not inside #region so its easly visible
            //<remove node in model validation here> 
            ModelState.Remove("ReasonID");

            #region :: Model validation  ::

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


            LeadGenerateActionModel Lead = new LeadGenerateActionModel();

            var prInfo = Lead.GetLeadGenerateActionData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateActionModel.LeadFollowup { ID_LeadGenerateAction = LeadGenerateActionInfo.ID_LeadGenerateAction, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser });

            var FoUpDetails = Lead.GetLeadGenerateActionDetails(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateActionModel.LeadGenerateActionID { ID_LeadGenerateAction = LeadGenerateActionInfo.ID_LeadGenerateAction, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, });



            return Json(new { prInfo, FoUpDetails }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetEmployee()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<LeadManagementModel.EmployeeInfo>(parameters: new APIParameters
            {
                TableName = "Employee",
                SelectFields = "ID_Employee,EmpCode EmployeeCode,EmpFName+' '+EmpLName EmployeeName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );


                

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProduct(Int32? ChannelMode)
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

            var data = Common.GetDataViaQuery<LeadManagementModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P",
                SelectFields = "ID_Product,ProdName ProductName,ProdShortName ShortName, ProdHSNCode ProductHSNCode",
                Criteria = "Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "",
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetEmployeeDeptWise(long Dept)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<LeadManagementModel.EmployeeInfo>(parameters: new APIParameters
            {
                TableName = "Employee AS E join Branch as BR ON E.FK_Branch=BR.ID_Branch",
                SelectFields = "E.ID_Employee ,E.EmpCode EmployeeCode,E.EmpFName+' '+E.EmpLName EmployeeName,E.FK_Branch,BR.FK_BranchType",
                Criteria = "E.FK_Department= "+Dept+ "AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Branch=BR.ID_Branch  AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            //OrganizationModel Organization = new OrganizationModel();

            //var data = Organization.GetOrganizationData(companyKey: _userLoginInfo.CompanyKey, input: new OrganizationModel.OrganizationID { ID_Organization = 0 });

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult LoadLeadGenerateAction()
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

            LeadGenerateActionModel.LeadGenerateActionViewList LeadMgntObj = new LeadGenerateActionModel.LeadGenerateActionViewList();
            LeadMgntObj.UserCode = _userLoginInfo.EntrBy;
            // if any data need to be displayed with the form, then get the data and pass it a model with the partial view and handle that data in view

            //CustomerModel.CustomerFormViewModel formViewModel = new CustomerModel.CustomerFormViewModel();
            //return PartialView("_CustomerForm",formViewModel);

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
            LeadMgntObj.LeadFromList = LeadFrmModelist.Data;

            var NextAcList = Common.GetDataViaQuery<LeadGenerateModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction ,NxtActnName",
                Criteria = "FK_ActionModule=1 AND NxtActnStage=1 AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_NextAction",
                GroupByFileds = ""
            },
 companyKey: _userLoginInfo.CompanyKey

    );
            LeadMgntObj.NextActionList = NextAcList.Data;
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
            LeadManagementModel objLead = new LeadManagementModel();


            var AcStatusList = objLead.GeLeadStatusList(input: new LeadManagementModel.ModeLead { Mode = 2 }, companyKey: _userLoginInfo.CompanyKey);

            LeadMgntObj.ActionStatusList = AcStatusList.Data;
            var Deptlist = Common.GetDataViaQuery<LeadGenerateModel.Departement>(parameters: new APIParameters
            {
                TableName = "Department",
                SelectFields = "ID_Department  ,DeptName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Department",
                GroupByFileds = ""
            },
     companyKey: _userLoginInfo.CompanyKey

        );
            LeadMgntObj.DepartementList = Deptlist.Data;

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

            var Category = Common.GetDataViaQuery<LeadGenerateActionModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1 "+ CatLeadgen + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey

              );
            LeadMgntObj.CategoryList = Category.Data;
            var ActModelist = Common.GetDataViaQuery<LeadGenerateActionModel.ActionType>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType  ,ActnTypeName",
                Criteria = "FK_ActionModule=1 and Cancelled=0 AND FK_Company = " + _userLoginInfo.FK_Company,
                SortFields = "ID_ActionType",
                GroupByFileds = ""
            },
    companyKey: _userLoginInfo.CompanyKey

       );
            LeadMgntObj.ActioTypeList = ActModelist.Data;

            LeadMgntObj.FK_Employee = _userLoginInfo.FK_Employee;
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
            LeadMgntObj.EmployeeInfoList = EmpName.Data;
            LeadMgntObj.MapKey= ConfigurationManager.AppSettings["googleMapKey"];
            string pageName = "";
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    pageName = "_AddLeadGenerateActionForm";
                    break;
                case "2":
                    pageName = "_AddLeadGenerateActionFormTravel";
                    break;
                case "7":
                    pageName = "_AddLeadGenerateActionFormCoolerView";
                    break;
                default:
                    pageName = "_AddLeadGenerateActionForm";
                    break;
            }
            ViewBag.isAdmin = _userLoginInfo.UsrrlAdmin;
            return PartialView(pageName, LeadMgntObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult AddNewLeadGenerateAction(LeadGenerateActionModel.LeadGenerateActionView data)
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

          
            bool IsValid = true;
            List<string> _ErrorMessage = new List<string>();

            ModelState.Clear();

            if (data.LgActStatus == 1&& data.ActDate != null)
            {
              if(data.ActDate > DateTime.Now)
                {
                    _ErrorMessage.Add("Completed Date can't be Future Date"); IsValid = false;
                }
            }

            if (data.LgActStatus == 3 && data.ActDate != null)
            {
                if (data.ActDate > DateTime.Now)
                {
                    _ErrorMessage.Add("Terminated Date can't be Future Date"); IsValid = false;
                }
            }

            if (data.LgActStatus == 4 && data.ActDate != null)
            {
                if (data.ActDate > DateTime.Now)
                {
                    _ErrorMessage.Add("Closed With Success Date can't be Future Date"); IsValid = false;
                }
            }




            if (data.LgActMode<=0)
            {
                _ErrorMessage.Add(" Follow up Details Tab : Select Follow-up Type"); IsValid = false;
            }

            if(data.LgActStatus<=0)
            {
                _ErrorMessage.Add("Follow up Details Tab : Select Status"); IsValid = false;
            }

            if(data.FK_FollowUpBy<=0)
            {
                _ErrorMessage.Add("Follow up Details Tab : Select Follow-up Employee"); IsValid = false;
            }

            if(data.ActDate==null)
            {
                if(data.LgActStatus==1)
                {
                    _ErrorMessage.Add("Follow up Details Tab  : Select Completed date"); IsValid = false;
                }
                else if(data.LgActStatus == 2)
                {
                    _ErrorMessage.Add("Follow up Details Tab: Select Postponed date"); IsValid = false;
                }
                else if(data.LgActStatus==3)
                {
                    _ErrorMessage.Add("Follow up Details Tab : Select Terminated date"); IsValid = false;
                }
                else if (data.LgActStatus == 4)
                {
                    _ErrorMessage.Add("Follow up Details Tab : Select Closed date"); IsValid = false;
                }
                else
                {
                    _ErrorMessage.Add("Follow up Details Tab : Select Status Date"); IsValid = false;
                }
            }

          

         


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

            LeadGenerateActionModel LeadGenerateAction = new LeadGenerateActionModel();

            if(IsValid==true)
            {
                var datresponse = LeadGenerateAction.UpdateLeadGenerateActionData(input: new LeadGenerateActionModel.UpdateLeadGenerateAction
                {
                    UserAction = 1,
                    Debug = 0,
                    TransMode = "ERP",
                    LgActMode = data.LgActMode,
                    LgActStatus = data.LgActStatus,
                    LgActDate = data.ActDate,
                    LgActCusComment = data.ActCusComment,
                    LgActLeadComment = data.ActLeadComment,
                    FK_Product = data.FK_Product,
                    FK_FollowUpBy = data.FK_FollowUpBy,
                    // LgActProjectName = data.LgActProjectName,
                    LgActNextAction = data.LgActNextAction,
                    LgActNextDate = data.ActNextDate,

                    FK_Departement = data.FK_Departements,
                    FK_Employee = data.FK_Employee,
                    FK_LeadGenerateProduct = data.FK_LeadGenerateProduct,
                    FK_LeadGenerate = data.LeadGenerate,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    ID_LeadGenerateAction = 0,
                    FK_LeadGenerateAction = 0,
                    SubProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                    FK_ActionType = data.FK_ActionType,
                    FK_Priority = data.FK_Priority,
                    checkval = data.checkval,
                    //LgpMRP = data.MRP,
                    LgpSalesPrice = data.OfferPrice,
                    LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                    LgGenerateQuot =data.IsQuotation,
                    LgQuotExpiryDate=data.QuotationDate,
                }, companyKey: _userLoginInfo.CompanyKey);
               
                return Json(new { Process = datresponse  }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetLeadActionHistoryList(LeadGenerateActionModel.LeadGenerateHistory data)
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

            LeadGenerateActionModel leadGenerate = new LeadGenerateActionModel();

            var outputList = leadGenerate.GetLeadGenerateActionHistory(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateActionModel.LeadGenerateHistory { LeadGenerateProduct = data.LeadGenerateProduct, PrductOnly = data.PrductOnly });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateLeadManagement(LeadManagementModel.LeadManagementView data)
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

            LeadManagementModel LeadManagement = new LeadManagementModel();
            var datresponse = LeadManagement.UpdateLeadManagementData(input: new LeadManagementModel.UpdateLeadManagement
            {
                // UserAction = 2,
                //LgLeadNo = data.LgLeadNo,
                //LgLeadDate = data.LgLeadDate,
                //LgCusName = data.LgCusName,
                //LgCusAddress = data.LgCusAddress,
                //LgCusMobile = data.LgCusMobile,
                //LgCusEmail = data.LgCusEmail,
                FK_LeadFrom = data.LeadFrom,
                FK_LeadBy = data.LeadBy,
                LeadByName = data.LeadByName,
                FK_Branch = data.Branch,
                FK_Dealer = data.Dealer,
                FK_Franchise = data.Franchise,
                FK_ExtensionCounter = data.ExtensionCounter,
                FK_Employee = data.Employee,
                FK_Customer = data.Customer,
                FK_ThirdParty = data.ThirdParty,
                FK_Freelancer = data.Freelancer,
                // LgCollectedBy = data.LgCollectedBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                //  EntrBy = _userLoginInfo.UserCode,
                FK_Machine = _userLoginInfo.FK_Machine
            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteLeadManagement(LeadManagementModel.LeadManagementView data)
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

            LeadManagementModel LeadManagement = new LeadManagementModel();

            var datresponse = LeadManagement.DeleteLeadManagementData(input: new LeadManagementModel.DeleteLeadManagement { ID_LeadManagement = data.LeadManagementID, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine, FK_Company = _userLoginInfo.FK_Company, FK_Reason = data.ReasonID, CancelOn = DateTime.UtcNow }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetLeadManagementReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy });

            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };
            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadLeadForm(string cus,string cusmob,int cusid,int cuscategoryid,int cusproductid,string cusproductname,string cusEmail)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            WalkingCustomer CusData = new WalkingCustomer()
            {               
                CustomerName = cus,
                Mobile= cusmob,
                CustomerID=cusid,
                CategoryId= cuscategoryid,
                ProductId= cusproductid,
                ProdName=cusproductname,
                CusEmail=cusEmail

            };
            TempData["WalkCusData"] = CusData;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.EncryptString("LFLG");
            //to get menu list name of lead generation
            CommonModel obj = new CommonModel();
            var Menu = obj.GetMenuFromNotification(input: new CommonModel.GetFromNotficationMenu
            {
                FK_Company = _userLoginInfo.FK_Company,
                TransMode = "LFLG",
            }, companyKey: _userLoginInfo.CompanyKey);
            string MnuLstName = "";

            if (Menu.Data != null)
            {
                MnuLstName = Menu.Data[0].MnuLstName;
            }
            //end
            return RedirectToAction("LeadGen", "LeadGeneration", new { mgrp = mGrp , mtd = objCmnMethod.EncryptString(MnuLstName) });
        }
        public ActionResult FromDashBoard(string id)
        {
            return RedirectToAction("Index", new { mtd= "sGGVU210R/5mtGNJf3Jsq6MNT5oRuM+VECkSTsGTrKc=", mgrp= "s6eKRd/Y8VtadD9Mr1mcXg==", tid = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetLeadLocation(LeadGenerateActionModel.GetLeadGenerateLocation data)
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
           
            LeadGenerateActionModel leadGenerate = new LeadGenerateActionModel();

            var outputList = leadGenerate.GetLeadGenerateLocationData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateActionModel.GetLeadGenerateLocation { FK_LeadGenerateProduct = data.FK_LeadGenerateProduct });
            List<LeadGenerateActionModel.LeadGenerateLocationList> ListData = new List<LeadGenerateActionModel.LeadGenerateLocationList>();
            if (outputList.Data != null)
            {
                foreach (var temp in outputList.Data)
                {
                    LeadGenerateActionModel.LeadGenerateLocationList obj = temp;
                    if (temp.LocLandMark1 == null || temp.LocLandMark1 == "")
                    {
                        obj.LocLandMark1 = "";
                    }
                    else
                    {
                        obj.LocLandMark1 = "data:image /; base64," + temp.LocLandMark1;
                    }
                    if (temp.LocLandMark2 == null || temp.LocLandMark2 == "")
                    {
                        obj.LocLandMark2 = "";
                    }
                    else
                    {
                        obj.LocLandMark2 = "data:image /; base64," + temp.LocLandMark2;
                    }
                    ListData.Add(obj);
                }
            }
            return Json(new { outputList.Process, ListData }, JsonRequestBehavior.AllowGet);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateLeadAssigne(LeadManagementModel.ViewLeadmanagementAssigne data)
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

            LeadManagementModel leadMgt = new LeadManagementModel();


            var datresponse = leadMgt.UpdateLeadManagementAssigne(input: new LeadManagementModel.InputLeadmanagementAssigne
            {
                ID_LeadGenerateProduct=data.ID_LeadGenerateProduct,
                NextActionDate=data.NextActionDate,
                FK_AssignedTo=data.FK_AssignedTo,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine= _userLoginInfo.FK_Machine,
                FK_BranchCodeUser= _userLoginInfo.FK_BranchCodeUser,
                FK_Company= _userLoginInfo.FK_Company,
               
                PLgpOfferPrice=data.PLgpOfferPrice

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadAudio(long id)
        {
            LeadManagementModel leadVoice = new LeadManagementModel();
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var outputList = leadVoice.GetVoiceData(companyKey: _userLoginInfo.CompanyKey, input: new LeadManagementModel.GetVoiceDataDetails
            {
                FK_Company = _userLoginInfo.FK_Company,
                FK_CustomerAssignment = id
            });
            //string Audio = outputList.Data[0].VoiceData;

            //byte[] audioBytes = Encoding.UTF8.GetBytes(outputList.Data[0].VoiceData);

            //// Convert byte array to Base64-encoded string
            //string base64Audio = Convert.ToBase64String(audioBytes);


           byte[] Audio = outputList.Data[0].VoiceData;
            
            string base64String = Convert.ToBase64String(Audio);
            

            return PartialView("_PlayAudio", base64String);
        }
            
    }
}