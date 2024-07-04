using PerfectWebERP.Filters;
using PerfectWebERP.General;
using PerfectWebERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PerfectWebERP.Models.LeadGenerateModel;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class LeadGenerationController : Controller
    {
        // GET: LeadGeneration

        public ActionResult GetBranchList(Int32 branchtypeid)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<usersModel.Branchs>(parameters: new APIParameters
            {

                TableName = "Branch",
                SelectFields = " BrName AS Branch,ID_Branch AS BranchID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND Cancelled =0 AND Passed=1 AND FK_BranchType='" + branchtypeid + "'",
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            return Json(data, JsonRequestBehavior.AllowGet);


        }

        public ActionResult Index(string mtd, string mgrp)
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 

            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.FK_Departement = _userLoginInfo.FK_Department;
           
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;

            return View();
        }
        public ActionResult LeadGen(string mtd, string mgrp)
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
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];  // session variable 


            UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.PagedAccessRights = pageAccess;

            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.FK_Departement = _userLoginInfo.FK_Department;
            ViewBag.EntrBy = _userLoginInfo.EntrBy;
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;

            ViewBag.mtd = mtd;
            
            if (Common.GetCorrectionData(mGrp) != "")
            {
                ViewBag.MasterID = Common.GetCorrectionData(mGrp);
                Common.ClearCorrectionData(mGrp);
            }

            string pageName = "";
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    pageName = "LeadGen";
                    break;
                case "2":
                    pageName = "LeadGenTravel";
                    break;
                case "7":
                    pageName = "LeadGenCooler";
                    break;
                case "8":
                    pageName = "LeadGenFurniture";
                    break;
                case "9":
                    pageName = "LeadGenGcc";
                    break;
                default:
                    pageName = "LeadGen";
                    break;
            }
            return View(pageName);
        }
        public ActionResult LoadLeadGenerateForm(string mtd)
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
            LeadGenerateModel.LeadGenerateViewList LeadFromListObj = new LeadGenerateModel.LeadGenerateViewList();
            LeadFromListObj.FK_Employee = _userLoginInfo.FK_Employee;
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
            LeadFromListObj.EmployeeInfoList = EmpName.Data;

            var empleadfrm = Common.GetDataViaQuery<LeadGenerateModel.EmployeeLeadInfo>(parameters: new APIParameters
            {
                TableName = "Employee E  join Branch B on E.FK_Branch=B.ID_Branch  join BranchType BT on B.FK_BranchType=BT.ID_BranchType  left join Department D on  E.FK_Department = D.ID_Department",
                SelectFields = "E.ID_Employee,E.EmpFName,CASE WHEN BT.FK_BranchMode IN (1,2) THEN 1 ELSE -1 END AS ID_BranchMode,B.ID_Branch,BT.ID_BranchType,E.FK_Department",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
companyKey: _userLoginInfo.CompanyKey

  );
            LeadFromListObj.EmployeeLeadInfoList = empleadfrm.Data;

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
            LeadFromListObj.LeadFromList = LeadFrmModelist.Data;


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
            LeadFromListObj.DepartementList = Deptlist.Data;

            var branchtypelist = Common.GetDataViaQuery<LeadGenerateModel.BranchTypes>(parameters: new APIParameters
            {
                TableName = "BranchType",
                SelectFields = "ID_BranchType AS BranchTypeID,BTName AS BranchType,FK_BranchMode AS BranchModeID",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
             );
            LeadFromListObj.BranchTypelists = branchtypelist.Data;

            var CatLeadgen = "";
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    CatLeadgen = "AND CatLeadGenerate = 1";
                    break;
                case "2":
                    CatLeadgen = "";
                    break;
                case "7":
                    CatLeadgen = "";
                    break;
                case "8":
                    CatLeadgen = "";
                    break;
                case "9":
                    CatLeadgen = "";
                    break;
                default:
                    CatLeadgen = "AND CatLeadGenerate = 1";
                    break;
            }
            var Category = Common.GetDataViaQuery<LeadGenerateModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                //Criteria = "Cancelled=0 AND Passed=1 " + CatLeadgen + " AND FK_Company=" + _userLoginInfo.FK_Company,
                Criteria = "Cancelled=0 AND Passed=1 " + CatLeadgen + "AND MODE='P'  AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey

               );
            LeadFromListObj.CategoryList = Category.Data;
            var NextAcList = Common.GetDataViaQuery<LeadGenerateModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction ,NxtActnName, NxtActnStage,FK_ActionStatus",
                Criteria = "FK_ActionModule=1 AND Cancelled=0 AND Passed=1 AND FK_ActionModule=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_NextAction",
                GroupByFileds = ""
            },
     companyKey: _userLoginInfo.CompanyKey

        );
            LeadFromListObj.NextActionList = NextAcList.Data;

            var actiontypelist = Common.GetDataViaQuery<LeadGenerateModel.ActionTypes>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType,ActnTypeName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 AND FK_ActionModule=1",

                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
             );


            LeadFromListObj.Actntyplists = actiontypelist.Data;

            var mediatypelist = Common.GetDataViaQuery<LeadGenerateModel.MediaTypes>(parameters: new APIParameters
            {
                TableName = "MediaMaster",
                SelectFields = "ID_MediaMaster,MdaName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1",

                SortFields = "",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey
             );
            LeadFromListObj.mediatyplists = mediatypelist.Data;

            var ProductLocationList = Common.GetDataViaQuery<LeadGenerateModel.ProductLocationList>(parameters: new APIParameters
            {
                TableName = "ProductLocation",
                SelectFields = "ID_ProductLocation as FK_ProductLocation,LocationName as ProductLocation",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company +" AND FK_Branch in("+ _userLoginInfo.FK_Branch +",0)", 
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            LeadFromListObj.ProductLocationList = ProductLocationList.Data;

            LeadGenerateModel objLead = new LeadGenerateModel();

            var AcStatusList = objLead.GeLeadStatusList(input: new LeadGenerateModel.ModeLead { Mode = 9 }, companyKey: _userLoginInfo.CompanyKey);

            LeadFromListObj.ActionStatusList = AcStatusList.Data;

            var TitleList = objLead.GetModeList(input: new LeadGenerateModel.GetModeData { Mode = 46 }, companyKey: _userLoginInfo.CompanyKey);
            LeadFromListObj.Title = TitleList.Data.AsEnumerable();

            ViewBag.ID_Country = _userLoginInfo.FK_Country;
            ViewBag.CountryName = _userLoginInfo.CntryName;
            ViewBag.ID_State = _userLoginInfo.FK_States;
            ViewBag.StateName = _userLoginInfo.StName;
            ViewBag.ID_District = _userLoginInfo.FK_District;
            ViewBag.DistrictName = _userLoginInfo.DtName;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.FK_Departement = _userLoginInfo.FK_Department;
            string pageName = "";
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    pageName = "_LeadGeneration";
                    break;
                case "2":
                    pageName = "_LeadGenerationTravel";
                    break;
                case "7":
                    pageName = "_LeadGenerationCooler";
                    break;
                case "8":
                    pageName = "_LeadGenerationFurniture";
                    break;
                case "9":
                    pageName = "_LeadGenerationGcc";
                    break;
                default:
                    pageName = "_LeadGeneration";
                    break;
            }
            ViewBag.WalkCustomer = "";
            ViewBag.Mobile = "";
            ViewBag.ID_CustomerAssignment = 0;
            ViewBag.LeadSource = 0;
            if (TempData["WalkCusData"] != null)
            {
                var data = TempData["WalkCusData"] as WalkingCustomer;
                ViewBag.WalkCustomer = data.CustomerName;
                ViewBag.Mobile = data.Mobile;
                ViewBag.ID_CustomerAssignment = data.CustomerID;
                ViewBag.Walkcategory = data.CategoryId;
                ViewBag.Walkproduct = data.ProductId;
                ViewBag.Walkprodname = data.ProdName;
                ViewBag.CusEmail = data.CusEmail;
                ViewBag.LeadSource = 11;
            }
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);

            return PartialView(pageName, LeadFromListObj);
        }



        public ActionResult GetPostList(LeadGenerateModel.Postlistrelated datas)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var data = Common.GetDataViaQuery<LeadGenerateModel.PostList>(parameters: new APIParameters
            {

                TableName = "Post",
                SelectFields = "PostName AS Post,ID_Post AS PostID,PinCode AS PinCode",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_District=" + datas.DistrictID + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


              companyKey: _userLoginInfo.CompanyKey
           );

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public ActionResult GetEmployeeLeadDefault()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<LeadGenerateModel.EmployeeLeadInfo>(parameters: new APIParameters
            {
                TableName = "Employee E  join Branch B on E.FK_Branch=B.ID_Branch  join BranchType BT on B.FK_BranchType=BT.ID_BranchType  left join Department D on  E.FK_Department = D.ID_Department",
                SelectFields = "E.ID_Employee,E.EmpFName,CASE WHEN BT.FK_BranchMode IN (1,2) THEN 1 ELSE -1 END AS ID_BranchMode,B.ID_Branch,BT.ID_BranchType,E.FK_Department, BT.BTName,B.BrName,D.DeptName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND E.Cancelled=0 AND E.Passed=1 AND E.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
companyKey: _userLoginInfo.CompanyKey

);


            //OrganizationModel Organization = new OrganizationModel();

            //var data = Organization.GetOrganizationData(companyKey: _userLoginInfo.CompanyKey, input: new OrganizationModel.OrganizationID { ID_Organization = 0 });

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        //public ActionResult GetLeadGenList()
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

        //    LeadGenerateModel leadGenerate = new LeadGenerateModel();

        //    var outputList = leadGenerate.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

        //    return Json(outputList, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetLeadGenList(int pageSize, int pageIndex, string Name, string TransModes)
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
            LeadGenerateModel leadGenerate = new LeadGenerateModel();

            var data = leadGenerate.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen
            {
                ID_LeadGenerate = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                Name = Name,
                TransMode = transMode

            });

            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);




        }
        //public ActionResult GetLeadFromList(Int32 ID_LeadFrom)
        //{
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //    var LeadbyData = Common.GetDataViaProcedure<LeadGenerateModel.LeadFromViewPop, GetLeadBy>
        //        (companyKey: _userLoginInfo.CompanyKey,
        //        procedureName: "ProLeadBySelect", 
        //        parameter: new GetLeadBy {TransMode="",
        //            ID_LeadFrom = ID_LeadFrom,
        //            EntrBy =_userLoginInfo.EntrBy,
        //            PageIndex =0,PageSize=10,
        //            FK_Company =_userLoginInfo.FK_Company,
        //            FK_Machine =_userLoginInfo.FK_Machine });

        //    return Json(LeadbyData, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult GetSubMediaList(Int32 FK_Media)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LeadGenerateModel objfld = new LeadGenerateModel();


            var data = Common.GetDataViaQuery<LeadGenerateModel.SubMedia>(parameters: new APIParameters
            {

                TableName = "MediaSubMaster",
                SelectFields = "ID_MediaSubMaster AS SubmediaID,SubMdaName AS Submedia",
                Criteria = "Cancelled =0 AND Passed=1 AND FK_Media=" + FK_Media + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


             companyKey: _userLoginInfo.CompanyKey
          );

            return Json(data, JsonRequestBehavior.AllowGet);



        }
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
        public ActionResult getReportViewList(DateTime FromDate, DateTime ToDate, Int32 Report)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ReportSettingModel objfld = new ReportSettingModel();

            var data = objfld.GetReportView(input: new ReportSettingModel.RptFld
            {
                FromDate = FromDate,
                ToDate = ToDate,
                FK_ReportSettings = Report,
                FK_Company = _userLoginInfo.FK_Company

            },

            companyKey: _userLoginInfo.CompanyKey);



            return Json(data, JsonRequestBehavior.AllowGet);


        }
        public ActionResult GetCusDtlByMob(string CusMobile)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LeadGenerateModel custinfo = new LeadGenerateModel();

            var data = Common.GetDataViaQuery<LeadGenerateModel.CustInfo>(parameters: new APIParameters
            {
                TableName = "Customer  AS C LEFT JOIN Post As Pt ON Pt.ID_Post=C.FK_Post LEFT JOIN Area A ON A.ID_Area=C.FK_Area LEFT JOIN District As D ON D.ID_District=C.FK_District   LEFT JOIN States AS ST ON ST.ID_States=D.FK_States LEFT JOIN Country AS Ct ON Ct.ID_Country=ST.FK_Country",
                SelectFields = "ID_Customer AS ID_Customer,CusName,CusAddress1,CusAddress2,CusEmail,CusPhone as CusPhnNo,CusCompany as Company,C.FK_Country as CountryID,C.FK_State as StatesID,C.FK_District as DistrictID,C.FK_Post as PostID,CntryName,StName,DtName,PostName,C.FK_Area AreaID,AreaName Area,Pt.PinCode,CusMobileAlternate",
                Criteria = "C.Cancelled=0 AND C.CusMobile<>'' AND C.Passed=1 AND C.FK_Company=" + _userLoginInfo.FK_Company + "  AND CusMobile='" + CusMobile + "'",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);




            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetCusOthrDtlByMob(string CusMobile)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LeadGenerateModel custinfo = new LeadGenerateModel();


            var cdata = Common.GetDataViaQuery<LeadGenerateModel.CustInfoOther>(parameters: new APIParameters
            {
                TableName = "CustomerOthers  AS C LEFT JOIN Post As Pt ON Pt.ID_Post=C.FK_Post LEFT JOIN Area A ON A.ID_Area=C.FK_Area LEFT JOIN District As D ON D.ID_District=C.FK_District   LEFT JOIN States AS ST ON ST.ID_States= D.FK_States LEFT JOIN Country AS Ct ON Ct.ID_Country=ST.FK_Country",
                SelectFields = "ID_CustomerOthers ,CusName,CusAddress1,CusAddress2,CusEmail,CusPhone as CusPhnNo,CusCompany as Company,C.FK_Country as CountryID,C.FK_State as StatesID,C.FK_District as DistrictID,C.FK_Post as PostID,CntryName,StName,DtName,PostName,C.FK_Area AreaID,AreaName Area,Pt.PinCode,CusMobileAlternate",
                Criteria = "C.Cancelled=0  AND  C.CusMobile<>''AND C.Passed=1 AND C.FK_Company=" + _userLoginInfo.FK_Company + "  AND CusMobile='" + CusMobile + "'",
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey);

            return Json(cdata, JsonRequestBehavior.AllowGet);





        }
        public ActionResult GetCustomerList()
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

            CustomerModel customer = new CustomerModel();

            var outputList = customer.GetCustomertData(companyKey: _userLoginInfo.CompanyKey, input: new CustomerModel.GetCustomer { ID_Customer = 0, FK_Company = _userLoginInfo.FK_Company, UserCode = _userLoginInfo.EntrBy });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmployee()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<LeadGenerateModel.EmployeeInfo>(parameters: new APIParameters
            {
                TableName = "Employee",
                SelectFields = "ID_Employee,EmpCode ,EmpFName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );

            //OrganizationModel Organization = new OrganizationModel();

            //var data = Organization.GetOrganizationData(companyKey: _userLoginInfo.CompanyKey, input: new OrganizationModel.OrganizationID { ID_Organization = 0 });

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetEmployeeDeptWise(long Dept)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<LeadGenerateModel.EmployeeInfo>(parameters: new APIParameters
            {
                TableName = "Employee",
                SelectFields = "ID_Employee ,EmpCode ,EmpFName",
                Criteria = "FK_Department=CASE WHEN " + Dept + "=0 THEN FK_Department ELSE " + Dept + " END  AND Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );



            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetProductSearch(int FK_Category, string ProdName)
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
            var ProdLeadgen = "";
            switch (_userLoginInfo.CompCategory)
            {
                case "1":
                    ProdLeadgen = "AND P.ProdLeadGenerate = 1";
                    break;
                case "2":
                    ProdLeadgen = "";
                    break;
                default:
                    ProdLeadgen = "AND P.ProdLeadGenerate = 1";
                    break;
            }
            var data = Common.GetDataViaQuery<LeadGenerateModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P LEFT JOIN OffersDetails AS OD ON OD.ID_OffersDetails = (SELECT MAX(ID_OffersDetails) FROM OffersDetails OD JOIN Offers O ON O.ID_Offers = OD.FK_Offers AND OD.Cancelled = 0 WHERE O.Cancelled = 0 AND OD.FK_Product = P.ID_Product AND O.OfrEffectDate< = CONVERT(DATE,GETDATE()) AND  O.OfrExpireDate> = CONVERT(DATE,GETDATE()) AND FK_Type = 1 )",
                SelectFields = "P.ID_Product,P.ProdName,P.ProdShortName , P.ProdHSNCode, ISNULL(OD.MRP,0) MRP, ISNULL(OD.MRP, 0) SodMRP, ISNULL(OD.SalPrice, 0) SodSalPrice, ISNULL(OD.SalPrice, 0) SalePrice ",
                Criteria = "Mode ='P' AND P.Cancelled=0 AND P.Passed=1  " + ProdLeadgen + " AND P.FK_Company=" + _userLoginInfo.FK_Company + "  AND P.FK_Category=" + FK_Category + "  AND P.ProdName LIKE +" + "'%" + ProdName + "%'",

                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProduct(Int32 FK_Category)
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

            var data = Common.GetDataViaQuery<LeadGenerateModel.GetProduct>(parameters: new APIParameters
            {
                TableName = "Product P",
                SelectFields = "ID_Product,ProdName,ProdShortName , ProdHSNCode ",
                Criteria = "Mode ='P' AND P.Cancelled=0 AND P.Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company + "  AND FK_Category=" + FK_Category,
                SortFields = "",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey
         );

            return Json(data, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddNewLeadGenerate(LeadGenerateModel.LeadGenerateView data)
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




            LeadGenerateModel LeadGenerate = new LeadGenerateModel();

            var prevId = Common.GetDataViaQuery<LeadGenerateModel.DuplicateCheck>(parameters: new APIParameters
            {
                TableName = "LeadGenerate",
                SelectFields = "TOP 1 isnull( ID_LeadGenerate,0) PreviousID",
                Criteria = "EntrBy='" + _userLoginInfo.EntrBy + "'",
                SortFields = "ID_LeadGenerate DESC",
                GroupByFileds = ""
            },
    companyKey: _userLoginInfo.CompanyKey

       );
            long pId = 0;
            if (prevId.Data != null)
            {
                pId = prevId.Data[0].PreviousID;
            }
            else
            {
                pId = 0;
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
            if (Session["UpdateQuotation"] != null)
            {
                QuotationModel.UpdateQuotation objQuo = (QuotationModel.UpdateQuotation)Session["UpdateQuotation"];
                QuotationModel objQu = new QuotationModel();

                var dataResponseQuo = objQu.UpdateQuotationData(input: new QuotationModel.UpdateQuotation
                {
                    UserAction = 1,
                    TransMode = objQuo.TransMode,
                    LastID = (objQuo.LastID.HasValue) ? objQuo.LastID.Value : 0,
                    ID_Quotation = 0,
                    QuoMode = objQuo.QuoMode,
                    QuoFrom = objQuo.QuoFrom,
                    FK_Master = objQuo.FK_Master,
                    QuoDate = objQuo.QuoDate,
                    QuoExpireDate = objQuo.QuoExpireDate,
                    QuoBillTotal = objQuo.QuoBillTotal,
                    QuoDiscount = objQuo.QuoDiscount,
                    QuoOthercharges = objQuo.QuoOthercharges,
                    QuoRoundoff = objQuo.QuoRoundoff,
                    QuoNetAmount = objQuo.QuoNetAmount,
                    QuoRemarks = objQuo.QuoRemarks,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_Branch = _userLoginInfo.FK_Branch,
                    FK_Department = _userLoginInfo.FK_Department,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    QuotationDetails = objQuo.QuotationDetails,
                    TaxDetails = objQuo.TaxDetails,
                    OtherChargeDetails = objQuo.OtherChargeDetails,
                    WarrantyDetails = objQuo.WarrantyDetails,
                    CustomerName = objQuo.CustomerName,
                    CustomerPhone = objQuo.CustomerPhone,
                    CustomerAddress = objQuo.CustomerAddress,
                    FK_QuotationGen = objQuo.FK_QuotationGen,
                    QuoReferenceNo = objQuo.QuoReferenceNo,
                    FK_Quotation = 0,
                    QuoEntrDate = objQuo.QuoEntrDate,
                }, companyKey: _userLoginInfo.CompanyKey);
                if (dataResponseQuo.IsProcess)
                {
                    data.FK_Quotation = dataResponseQuo.code;
                }
            }


            var datresponse = LeadGenerate.UpdateLeadGenerateData(input: new LeadGenerateModel.UpdateLeadGenerate
            {
                UserAction = 1,
                Debug = 0,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                TransMode = data.TransMode,
                FK_SubMedia = data.SubMedia,
                // LgLeadNo = "",
                LgLeadDate = data.LeadDate,
                FK_Customer = data.ID_Customer,
                FK_CustomerOthers = data.FK_CustomerOthers,
                LgCusNameTitle = data.CusNameTitle,
                LgCusName = data.CusName,
                LgCusAddress = data.CusAddress,
                LgCusAddress2 = data.CusAddress2,
                LgCusMobile = data.CusMobile,
                CusMobileAlternate = data.CusMobileAlternate,
                LgCusEmail = data.CusEmail,
                FK_LeadFrom = data.LeadFrom,
                FK_LeadBy = data.LeadBy,
                LeadByName = data.LeadByName,
                FK_Country = data.CountryID,
                FK_State = data.StatesID,
                FK_District = data.DistrictID,
                FK_Post = data.PostID,
                FK_Area = data.AreaID,
                FK_MediaMaster = data.ID_MediaMaster,
                CusCompany = data.Company,
                CusPhone = data.CusPhnNo,

                LgCollectedBy = data.CollectedBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
               
                FK_CustomerAssignment = data.FK_CustomerAssignment,
                SubProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                FK_Quotation = (data.FK_Quotation.HasValue) ? data.FK_Quotation.Value : 0,
                CusMobile1 = data.CusMobile1,
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
            }, companyKey: _userLoginInfo.CompanyKey);
            //if (datresponse.IsProcess)
            //{
            //    sendMail objMail = new sendMail();
            //    objMail.sendMailData(Convert.ToInt32(datresponse.code), "LEAD", _userLoginInfo.FK_Company, 1, 3, _userLoginInfo.CompanyKey,"");

            //    try
            //    {
            //        Common.SendMobileNotification(companyKey: _userLoginInfo.CompanyKey);
            //    }
            //    catch (Exception ex) {
            //    }
            //}

            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult UpdateLeadGenerate(LeadGenerateModel.LeadGenerateView data)
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




            //  ProdName = data.ProdName is null ? "" : data.ProdName,
            LeadGenerateModel LeadGenerate = new LeadGenerateModel();

            var prevId = Common.GetDataViaQuery<LeadGenerateModel.DuplicateCheck>(parameters: new APIParameters
            {
                TableName = "LeadGenerate",
                SelectFields = "TOP 1 ID_LeadGenerate PreviousID",
                Criteria = "EntrBy='" + _userLoginInfo.EntrBy + "'",
                SortFields = "ID_LeadGenerate DESC",
                GroupByFileds = ""
            },
   companyKey: _userLoginInfo.CompanyKey

      );
            long pId = 0;
            if (prevId.Data != null)
            {
                pId = prevId.Data[0].PreviousID;
            }
            else
            {
                pId = 0;
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

            if (imagenull)
            {
                data.ImageList = null;
            }
            var datresponse = LeadGenerate.UpdateLeadGenerateData(input: new LeadGenerateModel.UpdateLeadGenerate
            {

                UserAction = 2,
                TransMode = data.TransMode,
                CusPhone = data.CusPhnNo,
                LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                ID_LeadGenerate = data.LeadGenerateID,
                //LgLeadNo = data.LeadNo,
                LgLeadDate = data.LeadDate,
                LgCusName = data.CusName,
                FK_SubMedia = data.SubMedia,
                LgCusNameTitle = data.CusNameTitle,
                LgCusAddress = data.CusAddress,
                LgCusAddress2 = data.CusAddress2,
                LgCusMobile = data.CusMobile,
                CusMobileAlternate = data.CusMobileAlternate,
                LgCusEmail = data.CusEmail,
                FK_LeadFrom = data.LeadFrom,
                FK_LeadBy = data.LeadBy,
                LeadByName = data.LeadByName,
                CusCompany = data.Company,
                FK_Country = data.CountryID,
                FK_State = data.StatesID,
                FK_District = data.DistrictID,
                FK_Post = data.PostID,
                FK_Area = data.AreaID,
                FK_MediaMaster = data.ID_MediaMaster,
                FK_Customer = data.ID_Customer,
                FK_CustomerOthers = data.FK_CustomerOthers,

                LgCollectedBy = data.CollectedBy,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
              
                SubProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                FK_Quotation = (data.FK_Quotation.HasValue) ? data.FK_Quotation.Value : 0,
                ImageList = data.ImageList is null ? "" : Common.xmlTostring(data.ImageList),
                CusMobile1 = data.CusMobile1

            }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetLeadGenInfo(LeadGenerateModel.LeadGenerateView LeadGenerateInfo)
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


            LeadGenerateModel Lead = new LeadGenerateModel();
            CommonSearchPopupModel prodimg = new CommonSearchPopupModel();
            var prInfo = Lead.GeLeadGenerateData(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, EntrBy = _userLoginInfo.EntrBy });
            var subproduct = Lead.GetSubProduct(companyKey: _userLoginInfo.CompanyKey, input: new LeadGenerateModel.GetLeadGen { ID_LeadGenerate = LeadGenerateInfo.LeadGenerateID, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_Machine = _userLoginInfo.FK_Machine });

            var Imageselect = prodimg.GetImageSelect(companyKey: _userLoginInfo.CompanyKey, input: new CommonSearchPopupModel.GetImagein
            {
                FK_Master = LeadGenerateInfo.LeadGenerateID,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = LeadGenerateInfo.TransMode

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

            return Json(new { subproduct, prInfo, Imageselect }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult DeleteLeadGenerate(LeadGenerateModel.LeadGenerateView data)
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
            ModelState.Clear();
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

            LeadGenerateModel LeadGenerate = new LeadGenerateModel();

            var datresponse = LeadGenerate.DeleteLeadGenerateData(input: new LeadGenerateModel.DeleteLeadGenerate { ID_LeadGenerate = data.LeadGenerateID, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, FK_Company = _userLoginInfo.FK_Company, FK_Reason = data.ReasonID }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLeadGenerateReasonList()
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
        //public ActionResult GetMRPEdit()
        //{
        //    UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
        //    LeadGenerateModel MRPEdit = new LeadGenerateModel();

        //    var data = Common.GetDataViaQuery<LeadGenerateModel.MRPEdit>(parameters: new APIParameters
        //    {
        //        TableName = "GeneralSettings",
        //        SelectFields = "GsValue,GsField",
        //        Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "  AND GsField='MRP'",
        //        SortFields = "",
        //        GroupByFileds = ""
        //    },
        //    companyKey: _userLoginInfo.CompanyKey);

        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult GetMRPEdit()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            LeadGenerateModel MRPEdit = new LeadGenerateModel();

            var data = Common.GetDataViaQuery<LeadGenerateModel.MRPEdit>(parameters: new APIParameters
            {
                TableName = "SoftwareSecurity",
                SelectFields = "IIF(COUNT(GsValue)=0,0,MAX(GsValue)) GsValue,IIF(COUNT(GsField)=0,'',MAX(GsField)) AS GsField FROM(SELECT TOP 1 ISNULL(CONVERT(VARCHAR(20),SecValue),0)AS GsValue,ISNULL(CONVERT(VARCHAR(20),SecField),0)AS GsField",
                Criteria = "SecModule = 'LEAD' AND FK_Company ="  + _userLoginInfo.FK_Company +  "AND FK_Branch = " + _userLoginInfo.FK_Branch + " AND SecField='LEAD001'AND SecDate<=CONVERT(DATE,GETDATE())",
                SortFields = "SecDate DESC) AS T",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);

            return Json(data, JsonRequestBehavior.AllowGet);

        }


    }
}