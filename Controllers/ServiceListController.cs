using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PerfectWebERP.Models;
using PerfectWebERP.Filters;
using PerfectWebERP.General;
using System.IO;
using System.Text;
using System.Data;

namespace PerfectWebERP.Controllers
{
    [CheckSessionTimeOut]
    public class ServiceListController : Controller
    {
        // GET: ServiceList
        public ActionResult Index(string mtd, string mgrp)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            //UserAcssRightInfo pageAccess = new UserAcssRightInfo();
            //pageAccess = _userLoginInfo.PageAccessRights;
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;

            //ViewBag.PagedAccessRights = pageAccess;
            CommonMethod objCmnMethod = new CommonMethod();
            if (mtd != null)
            {
                ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            }
            ViewBag.TransMode = mgrp;
            ViewBag.mtd = mtd;
            return View();

        }
        public ActionResult ServiceListCustomer1(long ID, long? ServiceID, long ID_CustomerserviceregisterProductDetails, string mgrp)
        {
            CommonMethod objCmnMethod = new CommonMethod();
            string mGrp = objCmnMethod.DecryptString(mgrp);
            ViewBag.TransMode = mGrp;
            ViewBag.TicketNo = ID;
            ViewBag.mgrp = mgrp;
            ViewBag.Service = ServiceID;



            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            APIGetRecordsDynamic<ServiceListModel.ServiceListView> obj = new APIGetRecordsDynamic<ServiceListModel.ServiceListView>();
            ServiceListModel.ServiceListView Assign = new ServiceListModel.ServiceListView();
            ViewBag.FK_Employee = _userLoginInfo.FK_Employee;
            ViewBag.BranchID = _userLoginInfo.FK_Branch;
            ViewBag.DepartmentID = _userLoginInfo.FK_Department;
            ServiceListModel data = new ServiceListModel();
            var AssignMode = Common.GetDataViaQuery<ServiceListModel.AttendedMode>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType,ActnTypeName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },


          companyKey: _userLoginInfo.CompanyKey
         );


            var NextAcListLead = Common.GetDataViaQuery<ServiceListModel.LeadNextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction ID_NextActionLead ,NxtActnName NxtActnNameLead ,NxtActnStage NxtActnStageLead",
                Criteria = "FK_ActionModule=1 AND Cancelled=0 AND Passed=1 AND FK_ActionModule=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_NextAction",
                GroupByFileds = ""
            },
  companyKey: _userLoginInfo.CompanyKey

     );


            var NetAction = Common.GetDataViaQuery<ServiceListModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction,NxtActnName,FK_ActionStatus",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_ActionStatus not in(0,1,3,7) AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },


         companyKey: _userLoginInfo.CompanyKey
        );

            var actiontypelist = Common.GetDataViaQuery<ServiceListModel.ActionTypes>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType FK_ActionType,ActnTypeName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 AND FK_ActionModule=2",

                SortFields = "",
                GroupByFileds = ""
            },
                         companyKey: _userLoginInfo.CompanyKey
              );

            var EmpName = Common.GetDataViaQuery<ServiceListModel.EmployeeInfo>(parameters: new APIParameters
            {
                TableName = "Employee",
                SelectFields = "ID_Employee,EmpFName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
     companyKey: _userLoginInfo.CompanyKey

        );
            ServiceListModel.ServiceListView AssigListData = new ServiceListModel.ServiceListView();
            //List<ServiceListModel.ServiceListView> Outputdata = new List<ServiceListModel.ServiceListView>();
            APIGetRecordsDynamic<ServiceListModel.ServiceListView> Outputdata = new APIGetRecordsDynamic<ServiceListModel.ServiceListView>();
            if (ServiceID > 0)
            {
                var serviceActionfollowupInfo = data.GetServiceMainReturnData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.InputMaindetails
                {
                    FK_ServiceFollowUp = ServiceID,
                    PageMode = 0,
                    FK_Company = _userLoginInfo.FK_Company,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine
                });

                Outputdata = serviceActionfollowupInfo;
             
            }
            else
            {
                var Lits = data.GetCustomerData(input: new ServiceListModel.InputData { ID_Customerserviceregister = ID, TicketNo = "", ID_CustomerserviceregisterProductDetails = ID_CustomerserviceregisterProductDetails, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);
                Outputdata = Lits;
            }


            var complaintproductlist = Common.GetDataViaProcedure<ServiceListModel.ComplaintproductDetails, ServiceListModel.InputData>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProEmployeeProductDetailes", parameter: new ServiceListModel.InputData { ID_Customerserviceregister = ID, TicketNo = "", FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy, ID_CustomerserviceregisterProductDetails = ID_CustomerserviceregisterProductDetails });

            //var Lits1 = data.GetProductServiceDetails(input: new ServiceListModel.GetData { FK_Customerserviceregister = ID, PageMode = 1, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);

            var PaymentViews = Common.GetDataViaQuery<PaymentMethodModel.PaymentMethodView>(parameters: new APIParameters
            {
                TableName = "PaymentMethod",
                SelectFields = "ID_PaymentMethod as PaymentmethodID,PMName as Name",
                Criteria = "cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
      companyKey: _userLoginInfo.CompanyKey
  );
            var Category = Common.GetDataViaQuery<ServiceListModel.CategoryList>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS CategoryNameID ,CatName AS CategoryName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey

              );
            var BillTypeListView = Common.GetDataViaQuery<BillTypeModel.BillTypeView>(parameters: new APIParameters
            {
                TableName = "BillType",
                SelectFields = "ID_BillType as BillTypeID,BTName as BillType",
                Criteria = "BTBillType=3 AND cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
         companyKey: _userLoginInfo.CompanyKey
        );


            Assign = Outputdata.Data[0]; /*Lits.Data[0]*/;

            Assign.ComplaintproductDetails = complaintproductlist.Data;
            Assign.AtMode = AssignMode.Data;
            Assign.NextActionMode = NetAction.Data;
            Assign.LeadNextActionlists = NextAcListLead.Data;
            Assign.Actntyplists = actiontypelist.Data;
            Assign.PaymentView = PaymentViews.Data;
            Assign.CategoryList = Category.Data;
            Assign.BillTypeListView = BillTypeListView.Data;
            Assign.EmployeeInfoList = EmpName.Data;

            ViewBag.SerialNumber = complaintproductlist.Data[0].SerielNo;
            ViewBag.ID_ComplaintList = complaintproductlist.Data[0].ID_ComplaintList;

            #region nj
             
                ViewBag.FK_Category = Assign.ComplaintproductDetails[0].FK_Category;
                ViewBag.FK_SubCategory = Assign.ComplaintproductDetails[0].FK_SubCategory;

                ViewBag.FK_Brand = Assign.ComplaintproductDetails[0].FK_Brand;
             
            var PageSettingsDetalsInfo = data.GetPageSettingsDetals(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.PageSettingsDetalsInput
            {
                PSModule = "SERVICE",
                PSPage = "CUSF",
                FK_Company = _userLoginInfo.FK_Company,
                Mode = 3
            });
            string ProdVal = "0", CategoryVal = "0", SubCategoryVal = "0", BrandVal = "0", ComplaintVal = "0";
            if (PageSettingsDetalsInfo != null)
            {

                DataTable dt = PageSettingsDetalsInfo.Data;

                foreach (DataRow Row in dt.Rows)
                {
                    switch (Convert.ToInt32(Row["PSField"]))
                    {
                        case 13:
                            ProdVal = Row["PSValue"].ToString();
                            break;
                        case 14:
                            CategoryVal = Row["PSValue"].ToString();
                            break;
                        case 15:
                            SubCategoryVal = Row["PSValue"].ToString();
                            break;
                        case 16:
                            BrandVal = Row["PSValue"].ToString();
                            break;
                        case 17:
                            ComplaintVal = Row["PSValue"].ToString();
                            break;

                    }
                }
            }
            ViewBag.ProductVal = ProdVal;

            ViewBag.CategoryVal = CategoryVal;
            if (CategoryVal == "1")
            {
                Assign.CategoryDetailsList = Common.GetDataViaQuery<ServiceListModel.Category>(parameters: new APIParameters
                {
                    TableName = "Category",
                    SelectFields = "ID_Category AS CategoryID ,CatName AS CategoryName",
                    Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "ID_Category",
                    GroupByFileds = ""
                },
           companyKey: _userLoginInfo.CompanyKey

              ).Data;
            }
            ViewBag.SubCategoryVal = SubCategoryVal;
            if (SubCategoryVal == "1")
            {
                Assign.SubCategoryList = Common.GetDataViaQuery<ServiceListModel.SubCategory>(parameters: new APIParameters
                {
                    TableName = "SubCategory",
                    SelectFields = "ID_SubCategory AS SubCategoryID ,SubCatName AS SubCategoryName",
                    Criteria = "Cancelled=0 AND Passed=1 AND FK_Category=" + Assign.ComplaintproductDetails[0].FK_Category + " AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "ID_SubCategory",
                    GroupByFileds = "ID_SubCategory,SubCatName"
                },
           companyKey: _userLoginInfo.CompanyKey

              ).Data;
            }
            ViewBag.BrandVal = BrandVal;

            if (BrandVal == "1")
            {
                Assign.BrandList = Common.GetDataViaQuery<ServiceListModel.Brand>(parameters: new APIParameters
                {
                    TableName = "Brand",
                    SelectFields = "ID_Brand AS BrandID ,BrName AS BrandName",
                    Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "ID_Brand",
                    GroupByFileds = ""
                },
           companyKey: _userLoginInfo.CompanyKey

              ).Data;
            }


            ViewBag.ComplaintVal = ComplaintVal;
            if (ComplaintVal == "1")
            {
                Assign.ComplaintsList = Common.GetDataViaQuery<ServiceListModel.Complaints>(parameters: new APIParameters
                {
                    TableName = "ComplaintCheckList CC JOIN ComplaintList  CL ON CC.FK_Complaint=CL.ID_ComplaintList",
                    SelectFields = "ID_ComplaintList   ,CompntName AS ComplaintName",
                    Criteria = "CL.Cancelled=0 AND CL.Passed=1 AND CC.Cancelled=0 AND CC.Passed=1 AND FK_Category=" + Assign.ComplaintproductDetails[0].FK_Category + "  AND CL.FK_Company=" + _userLoginInfo.FK_Company,
                    SortFields = "ID_ComplaintList",
                    GroupByFileds = " ID_ComplaintList,CompntName"
                },
           companyKey: _userLoginInfo.CompanyKey

              ).Data;
            }

            #endregion
            var NextActionPageName = Common.GetDataViaQuery<ServiceListModel.NextAction>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "MnuLstName NxtActnName",
                Criteria = "Cancelled=0  AND Passed=1 AND ControllerName='NextAction' AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },


     companyKey: _userLoginInfo.CompanyKey
    );
            ViewBag.NxtActnPageName = NextActionPageName.Data[0].NxtActnName;
            return View(Assign);
        }
        #region nj
        public ActionResult GetSubcategoryList(string FK_Category)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var SubCategoryList = Common.GetDataViaQuery<ServiceListModel.SubCategory>(parameters: new APIParameters
            {
                TableName = "SubCategory",
                SelectFields = "ID_SubCategory AS SubCategoryID ,SubCatName AS SubCategoryName",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Category=" + FK_Category + " AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_SubCategory",
                GroupByFileds = "ID_SubCategory,SubCatName"
            },
          companyKey: _userLoginInfo.CompanyKey);
            return Json(SubCategoryList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetComplaintList(string FK_Category)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var ComplaintList = Common.GetDataViaQuery<ServiceListModel.Complaint>(parameters: new APIParameters
            {
                TableName = "ComplaintCheckList CC JOIN ComplaintList  CL ON CC.FK_Complaint=CL.ID_ComplaintList",
                SelectFields = "ID_ComplaintList   ,CompntName AS ComplaintName",
                Criteria = "CL.Cancelled=0 AND CL.Passed=1 AND CC.Cancelled=0 AND CC.Passed=1 AND FK_Category=" + FK_Category + "  AND CL.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_ComplaintList",
                GroupByFileds = "ID_ComplaintList,CompntName"
            },
          companyKey: _userLoginInfo.CompanyKey);
            return Json(ComplaintList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult LoadServiceListForm(string mgrp, string mtd)
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


            ServiceListModel.ServiceListView Assign = new ServiceListModel.ServiceListView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var BranchList = Common.GetDataViaQuery<ServiceListModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = " ID_Branch,BrName BranchName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );

            var ComplaintList = Common.GetDataViaQuery<ServiceListModel.Complaint>(parameters: new APIParameters
            {
                TableName = "ComplaintList",
                SelectFields = "ID_ComplaintList,CompntName ComplaintName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey
          );
            var ChangeMod = Common.GetDataViaProcedure<ServiceListModel.ChangeMode, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 126 });
            //AssignModel. = Lits.Data;
            Assign.StatusModeList = ChangeMod.Data;

            Assign.BranchList = BranchList.Data;
            Assign.ComplaintList = ComplaintList.Data;


            ViewBag.TransMode = mgrp;
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AddServiceList", Assign);
        }

        public ActionResult GetProductHistoryDetails(ServiceListModel.ServiceListView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel data = new ServiceListModel();
            var Lits = data.GetProductPreviousData(input: new ServiceListModel.ProductInputData { FK_Product = Data.ID_Product, FK_Customer = 0, FK_CustomerOthers = 0, Mode = 99, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);




            return Json(Lits, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductHistoryDetails1(ServiceListModel.ServiceListView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel data = new ServiceListModel();
            var Lits = data.GetProductPreviousData1(input: new ServiceListModel.ProductInputData { FK_Product = Data.ID_Product, FK_Customer = Data.FK_Customer, FK_CustomerOthers = Data.FK_CustomerOthers, Mode = 99, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);


            return Json(Lits, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCustomerHistoryDetails(ServiceListModel.ServiceListView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel data = new ServiceListModel();
            var Lits = data.GetCustomerPreviousData(input: new ServiceListModel.CustomerInputData { FK_Customer = Data.FK_Customer, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);




            return Json(Lits, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public FileResult DownloadImage(int ID)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ServiceListModel data = new ServiceListModel();
            var Lits = data.GetProductPreviousData(input: new ServiceListModel.ProductInputData { FK_Product = ID, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);


            var item = Lits.Data.Find(x => x.ID_Product == ID);

            byte[] imageBytes = Convert.FromBase64String("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NTYgCjcyMiAKMjc4IAo1NTYgCjU1NiAKXSAKZW5kb2JqIAoxMiAwIG9iaiAKPDwgCi9UeXBlIC9Gb250RGVzY3JpcHRvciAKL0FzY2VudCA5MDUgCi9DYXBIZWlnaHQgNTAwIAovRGVzY2VudCAtMjEyIAovRmxhZ3MgNCAKL0ZvbnRCQm94IDEzIDAgUiAKL0ZvbnROYW1lIC9BQUFBQUIrQXJpYWwgCi9JdGFsaWNBbmdsZSAwCi9TdGVtViAwIAovU3RlbUggMCAKL0F2Z1dpZHRoIDQ0MSAKL0ZvbnRGaWxlMiAxNCAwIFIgCi9MZWFkaW5nIDAgCi9NYXhXaWR0aCAyNjY1IAovTWlzc2luZ1dpZHRoIDQ0MSAKL1hIZWlnaHQgMCAKPj4gCmVuZG9iaiAKMTMgMCBvYmogClsgLTY2NSAtMzI1IDIwMDAgMTA0MCBdIAplbmRvYmogCjE1IDAgb2JqIAooUG93ZXJlZCBCeSBDcnlzdGFsKSAKZW5kb2JqIAoxNiAwIG9iaiAKKENyeXN0YWwgUmVwb3J0cykgCmVuZG9iaiAKMTcgMCBvYmogCjw8IAovUHJvZHVjZXIgKFBvd2VyZWQgQnkgQ3J5c3RhbCkgIAovQ3JlYXRvciAoQ3J5c3RhbCBSZXBvcnRzKSAgCj4+IAplbmRvYmogCnhyZWYgCjAgMTggCjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxNyAwMDAwMCBuIAowMDAwMDIxNzU2IDAwMDAwIG4gCjAwMDAwMjE4NTUgMDAwMDAgbiAKMDAwMDAyMTg4OSAwMDAwMCBuIAowMDAwMDAwMTk0IDAwMDAwIG4gCjAwMDAwMjE5MjMgMDAwMDAgbiAKMDAwMDAyMTk4OSAwMDAwMCBuIAowMDAwMDIyMDIzIDAwMDAwIG4gCjAwMDAwMjIxMTUgMDAwMDAgbiAKMDAwMDAyMjI4NiAwMDAwMCBuIAowMDAwMDAwNzY5IDAwMDAwIG4gCjAwMDAwMjI1MzUgMDAwMDAgbiAKMDAwMDAyMjgwOSAwMDAwMCBuIAowMDAwMDAxMzU1IDAwMDAwIG4gCjAwMDAwMjI4NTIgMDAwMDAgbiAKMDAwMDAyMjg5MiAwMDAwMCBuIAowMDAwMDIyOTI5IDAwMDAwIG4gCnRyYWlsZXIgCjw8IAovU2l6ZSAxOCAKL1Jvb3QgMSAwIFIgCi9JbmZvIDE3IDAgUiAKPj4gCnN0YXJ0eHJlZiAKMjMwMTcgCiUlRU9GDQo=");

            string extn = "";



            extn = Path.GetExtension(item.ProductImageName).ToLower().Substring(1);

            return File(imageBytes, "application/" + extn, item.ProductImageName);





        }

        [HttpGet]
        public FileResult DownloadDoc(int ID)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ServiceListModel data = new ServiceListModel();
            var Lits = data.GetProductPreviousData(input: new ServiceListModel.ProductInputData { FK_Product = ID, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);

            var item = Lits.Data.Find(x => x.ID_Product == ID);

            // byte[] imageBytes = Convert.FromBase64String("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NTYgCjcyMiAKMjc4IAo1NTYgCjU1NiAKXSAKZW5kb2JqIAoxMiAwIG9iaiAKPDwgCi9UeXBlIC9Gb250RGVzY3JpcHRvciAKL0FzY2VudCA5MDUgCi9DYXBIZWlnaHQgNTAwIAovRGVzY2VudCAtMjEyIAovRmxhZ3MgNCAKL0ZvbnRCQm94IDEzIDAgUiAKL0ZvbnROYW1lIC9BQUFBQUIrQXJpYWwgCi9JdGFsaWNBbmdsZSAwCi9TdGVtViAwIAovU3RlbUggMCAKL0F2Z1dpZHRoIDQ0MSAKL0ZvbnRGaWxlMiAxNCAwIFIgCi9MZWFkaW5nIDAgCi9NYXhXaWR0aCAyNjY1IAovTWlzc2luZ1dpZHRoIDQ0MSAKL1hIZWlnaHQgMCAKPj4gCmVuZG9iaiAKMTMgMCBvYmogClsgLTY2NSAtMzI1IDIwMDAgMTA0MCBdIAplbmRvYmogCjE1IDAgb2JqIAooUG93ZXJlZCBCeSBDcnlzdGFsKSAKZW5kb2JqIAoxNiAwIG9iaiAKKENyeXN0YWwgUmVwb3J0cykgCmVuZG9iaiAKMTcgMCBvYmogCjw8IAovUHJvZHVjZXIgKFBvd2VyZWQgQnkgQ3J5c3RhbCkgIAovQ3JlYXRvciAoQ3J5c3RhbCBSZXBvcnRzKSAgCj4+IAplbmRvYmogCnhyZWYgCjAgMTggCjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDAxNyAwMDAwMCBuIAowMDAwMDIxNzU2IDAwMDAwIG4gCjAwMDAwMjE4NTUgMDAwMDAgbiAKMDAwMDAyMTg4OSAwMDAwMCBuIAowMDAwMDAwMTk0IDAwMDAwIG4gCjAwMDAwMjE5MjMgMDAwMDAgbiAKMDAwMDAyMTk4OSAwMDAwMCBuIAowMDAwMDIyMDIzIDAwMDAwIG4gCjAwMDAwMjIxMTUgMDAwMDAgbiAKMDAwMDAyMjI4NiAwMDAwMCBuIAowMDAwMDAwNzY5IDAwMDAwIG4gCjAwMDAwMjI1MzUgMDAwMDAgbiAKMDAwMDAyMjgwOSAwMDAwMCBuIAowMDAwMDAxMzU1IDAwMDAwIG4gCjAwMDAwMjI4NTIgMDAwMDAgbiAKMDAwMDAyMjg5MiAwMDAwMCBuIAowMDAwMDIyOTI5IDAwMDAwIG4gCnRyYWlsZXIgCjw8IAovU2l6ZSAxOCAKL1Jvb3QgMSAwIFIgCi9JbmZvIDE3IDAgUiAKPj4gCnN0YXJ0eHJlZiAKMjMwMTcgCiUlRU9GDQo=");

            byte[] imageBytes = Convert.FromBase64String(item.ProductImage);

            //  var testb = Convert.FromBase64String(mystr);


            string extn = "";



            extn = Path.GetExtension(item.ProductImageName).ToLower().Substring(1);

            return File(imageBytes, "application/" + extn, item.ProductImageName);





        }

        public ActionResult GetServiceAddDetails(ServiceListModel.ServiceListView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel data = new ServiceListModel();
            var Lits = data.GetProductServiceDetails1(input: new ServiceListModel.GetDataservices { FK_Services = Data.FK_Services, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);
            var ServiceMod = Common.GetDataViaProcedure<ServiceListModel.Servicetype, ServiceListModel.ServiceModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ServiceModeInput { Mode = 57 });



            return Json(new { Lits, ServiceMod }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceDetails(ServiceListModel.ServiceListView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel data = new ServiceListModel();
            var Lits = data.GetProductServiceDetails(input: new ServiceListModel.GetData { FK_Customerserviceregister = Data.FK_Customerserviceregister, PageMode = 1, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);

            var ServiceMod = Common.GetDataViaProcedure<ServiceListModel.Servicetype, ServiceListModel.ServiceModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ServiceModeInput { Mode = 57 });


            return Json(new { Lits, ServiceMod }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAttendedEmployeeDetails(ServiceListModel.ServiceListView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel data = new ServiceListModel();
            var AttendEmp = data.GetAttendedEmployeeDetails(input: new ServiceListModel.GetData { FK_Customerserviceregister = Data.FK_Customerserviceregister, PageMode = 3, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);




            return Json(AttendEmp, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductDetails(ServiceListModel.ServiceListView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel.ServiceListView AssignModel = new ServiceListModel.ServiceListView();
            ServiceListModel data = new ServiceListModel();


            var Lits = data.GetProductDetails(input: new ServiceListModel.GetData { FK_Customerserviceregister = Data.FK_Customerserviceregister, PageMode = 2, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);

            var ChangeMod = Common.GetDataViaProcedure<ServiceListModel.ChangeMode, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 8 });
            //AssignModel. = Lits.Data;
            AssignModel.ChangeMode = ChangeMod.Data;


            return Json(new { Lits, ChangeMod }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetProductMoreDetails(ServiceListModel.ServiceListView Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel.ServiceListView AssignModel = new ServiceListModel.ServiceListView();
            ServiceListModel data = new ServiceListModel();


            var Lits = data.GetMoreProductDetails(input: new ServiceListModel.GetDataProductComponent { FK_Product = Data.FK_Product, FK_Category = Data.CategoryNameID, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Company = _userLoginInfo.FK_Company, FK_Machine = _userLoginInfo.FK_Machine, EntrBy = _userLoginInfo.EntrBy }, companyKey: _userLoginInfo.CompanyKey);

            var ChangeMod = Common.GetDataViaProcedure<ServiceListModel.ChangeMode, ServiceListModel.ChangeModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ChangeModeInput { Mode = 8 });
            //AssignModel. = Lits.Data;
            AssignModel.ChangeMode = ChangeMod.Data;
            return Json(new { Lits, ChangeMod }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadCusServiceListForm()
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


            ServiceListModel.ServiceListView Assign = new ServiceListModel.ServiceListView();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];


            var BranchList = Common.GetDataViaQuery<ServiceListModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = " ID_Branch,BrName BranchName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey
           );

            var ComplaintList = Common.GetDataViaQuery<ServiceListModel.Complaint>(parameters: new APIParameters
            {
                TableName = "ComplaintList",
                SelectFields = "ID_ComplaintList,CompntName ComplaintName",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
           companyKey: _userLoginInfo.CompanyKey
          );

            Assign.BranchList = BranchList.Data;
            Assign.ComplaintList = ComplaintList.Data;

            return PartialView("_AddServiceList", Assign);
        }

        public ActionResult GetSearchResult(ServiceListModel.TicketInput Data, int pageSize, int pageIndex)
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

            ServiceListModel Model = new ServiceListModel();

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            #region :: Fill List ::

            var data = Model.GetTicketDetails(input: new ServiceListModel.TicketInput
            {
                FK_Branch = _userLoginInfo.FK_Branch,
                FK_Product = Data.FK_Product,
                EntrBy = _userLoginInfo.EntrBy,
                //  FK_ComplaintType = Data.FK_ComplaintType,
                Status = Data.Status,
                CurrentStatus = Data.CurrentStatus,
                FromDate = Data.FromDate == null ? "" : Data.FromDate,

                ToDate = Data.ToDate == null ? "" : Data.ToDate,
                TicketNumber = Data.TicketNumber,
                FK_Employee = _userLoginInfo.FK_Employee,
                Customer = Data.Customer,
                // Mobile = Data.Content,
                //  Content = Data.Content,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                FK_Company = _userLoginInfo.FK_Company
            }, companyKey: _userLoginInfo.CompanyKey);

            #endregion :: Fill List ::

            //return Json(data, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageIndex, pageSize, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult GetServiceFollowupList(int pageSize, int pageIndex, string Name)
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

            ServiceListModel objPrdServ = new ServiceListModel();
            var data = objPrdServ.GetServicefollowupData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.ServicefollowupselectID
            {

                FK_ServiceFollowUp = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_Machine = _userLoginInfo.FK_Machine,
                EntrBy = _userLoginInfo.EntrBy,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                PageIndex = pageIndex + 1,
                PageSize = pageSize,
                PageMode = 0,
                Name = Name,
                TransMode = transMode

            });

            // return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);
            return Json(new { data.Process, data.Data, pageSize, pageIndex, totalrecord = (data.Data is null) ? 0 : data.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetServicefollowupInfoByID(ServiceListModel.ServiceListView data)
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


            ModelState.Remove("VisitedDate");
            ModelState.Remove("NextAction");
            ModelState.Remove("ReasonID");
            // ModelState.Remove("TotalCount");



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

            ServiceListModel servicefollowup = new ServiceListModel();

            var servicefollowupInfo = servicefollowup.GetServicefollowupData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.ServicefollowupselectID
            {
                FK_ServiceFollowUp = data.ID_ServiceFollowUp,
                PageMode = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var servicefollowupInfoServicecost = servicefollowup.GetServicecostData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.SubtabledataSelect
            {
                FK_ServiceFollowUp = data.ID_ServiceFollowUp,
                PageMode = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            if (servicefollowupInfoServicecost.Process.IsProcess)
            {

                servicefollowupInfo.Data[0].ServiceDetails = servicefollowupInfoServicecost.Data;
            }
            var servicefollowupInfoReproduct = servicefollowup.GetReplacedProductData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.SubtabledataSelect
            {
                FK_ServiceFollowUp = data.ID_ServiceFollowUp,
                PageMode = 2,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            if (servicefollowupInfoReproduct.Process.IsProcess)
            {

                servicefollowupInfo.Data[0].ProductDetails = servicefollowupInfoReproduct.Data;
            }
            var servicefollowupInfoAssignee = servicefollowup.GetAttendedEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.SubtabledataSelect
            {
                FK_ServiceFollowUp = data.ID_ServiceFollowUp,
                PageMode = 3,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            if (servicefollowupInfoAssignee.Process.IsProcess)
            {

                servicefollowupInfo.Data[0].AttendedEmployeeDetails = servicefollowupInfoAssignee.Data;
            }

            var paymentdetail = servicefollowup.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.GetPaymentin
            {
                FK_Master = data.ID_ServiceFollowUp,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });
            if (paymentdetail.Process.IsProcess)
            {
                servicefollowupInfo.Data[0].PaymentDetail = paymentdetail.Data;

            }
            return Json(servicefollowupInfo, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult UpdateServiceFollowUpDetails(ServiceListModel.ServiceListView data)
        //  {
        //      if (Session["UserLoginInfo"] is null)
        //      {
        //          return Json(new
        //          {
        //              Process = new Output
        //              {
        //                  IsProcess = false,
        //                  Message = new List<string> { "Please login to continue" },
        //                  Status = "Session Timeout",
        //              }
        //          }, JsonRequestBehavior.AllowGet);
        //      }

        //      UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

        //      bool IsValid = true;
        //      List<string> _ErrorMessage = new List<string>();

        //      ModelState.Clear();

        //      if (data.AttendedEmployeeDetails == null)
        //      {
        //          _ErrorMessage.Add("Please Mark Attended Person"); IsValid = false;
        //      }
        //      if (!ModelState.IsValid)
        //      {
        //          List<string> errorList = new List<string>();

        //          return Json(new
        //          {
        //              Process = new Output
        //              {
        //                  IsProcess = false,
        //                  Message = ModelState.Values.SelectMany(m => m.Errors)
        //                  .Select(e => e.ErrorMessage)
        //                  .ToList(),
        //                  Status = "Validation failed",
        //              }
        //          }, JsonRequestBehavior.AllowGet);
        //      }

        //      ServiceListModel serviceList = new ServiceListModel();

        //      if (IsValid == true)
        //      {
        //          var datresponse = serviceList.UpdateServiceListData(input: new ServiceListModel.UpdateServiceList
        //          {
        //              UserAction = 2,
        //              ID_ServiceFollowUp = data.ID_ServiceFollowUp,
        //              FK_Customerserviceregister = data.FK_Customerserviceregister,

        //              FK_Customer = data.FK_Customer,
        //              CustomerNotes = data.CustomerNote,
        //              EmployeeNote = data.EmployeeNote,
        //              StartingDate = data.StartingDate,

        //              ServiceAmount = data.ServiceAmount,

        //              ProductAmount = data.ProductAmount,
        //              NetAmount = data.NetAmount,
        //              DiscountAmount = data.DiscountAmount,
        //              TotalAmount = data.TotalAmount,
        //              ReplaceAmount = data.ReplaceAmount,
        //              FK_Company = _userLoginInfo.FK_Company,
        //              FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
        //              EntrBy = _userLoginInfo.EntrBy,
        //              FK_Machine = _userLoginInfo.FK_Machine,

        //              TransMode = data.TransMode,
        //              FK_ActionType = data.FK_ActionType,

        //              FK_NextAction = data.ID_NextAction,

        //              FK_NextActionLead = data.ID_NextActionLead,
        //              FK_ActionTypeLead = data.FK_ActionType,

        //              NextActionDateLead = data.NextActionDateLead,
        //              FK_EmployeeLead = data.FK_EmployeeAssign,



        //              Debug = false,

        //              FK_BillType = data.BillTypeID,

        //              ProductDetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
        //              ServiceDetails = data.ServiceDetails is null ? "" : Common.xmlTostring(data.ServiceDetails),
        //              AttendedEmployeeDetails = data.AttendedEmployeeDetails is null ? "" : Common.xmlTostring(data.AttendedEmployeeDetails),
        //              PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),

        //          }, companyKey: _userLoginInfo.CompanyKey);

        //          return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        //      }

        //      else
        //      {
        //          return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);
        //      }
        //  }


        [HttpPost]
        [ValidateAntiForgeryToken()]

        public ActionResult DeleteServiceFollowUpInfo(ServiceListModel.ServiceListView data)
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

            ModelState.Remove("VisitedDate");
            ModelState.Remove("NextAction");
            // ModelState.Remove("ReasonID");


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

            // DistrictModel.DeleteDistrict district = new DistrictModel.DeleteDistrict
            ServiceListModel.DeleteServiceFollowUp serviceFollowUp = new ServiceListModel.DeleteServiceFollowUp
            {
                ID_ServiceFollowUp = data.ID_ServiceFollowUp,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Debug = 0,
                FK_Company = _userLoginInfo.FK_Company,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                FK_Reason = data.ReasonID,
                TransMode = ""
            };

            Output dataresponse = Common.UpdateTableData<ServiceListModel.DeleteServiceFollowUp>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProServiceFollowUpDelete", parameter: serviceFollowUp);

            return Json(new { Process = dataresponse }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetServiceFollowUpReasonList()
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

            var outputList = reason.GetReasonData(companyKey: _userLoginInfo.CompanyKey, input: new ReasonModel.InputReasonID { FK_Reason = 0, FK_Company = _userLoginInfo.FK_Company, EntrBy = _userLoginInfo.EntrBy, FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser, FK_Machine = _userLoginInfo.FK_Machine, PageIndex = 0, PageSize = 0, TransMode = "" });


            APIGetRecordsDynamic<ReasonModel.ReasonsView> deleteReason = new APIGetRecordsDynamic<ReasonModel.ReasonsView>
            {
                Process = outputList.Process,
                Data = outputList.Data.Where(a => a.ModeID == 1).OrderBy(a => a.SortOrder).ToList()
            };


            return Json(deleteReason, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetServiceTaxAndNet(ServiceListModel.ServiceNetAndTax data)
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
            ServiceListModel servicefollowup = new ServiceListModel();

            var TaxNet = servicefollowup.GetServicetaxAndNetData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.ServiceNetAndTaxInput
            {
                FK_ServiceType = data.FK_ServiceType,
                TaxableAmount = data.TaxableAmount
            });

            return Json(new { TaxNet }, JsonRequestBehavior.AllowGet);
        }

        /************new code****************/
        //GetProductDetailsusingReferenceNo
        public ActionResult GetProductSubDetailsusingReferenceNos(ServiceListModel.GetDataRefernceNo Data)
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

            #region :: Fill List ::


            #endregion :: Fill List ::
            ServiceListModel data = new ServiceListModel();
            var Lits = data.GetCustomerProductDetails(input: new ServiceListModel.GetDataRefernceNo { ReferenceNo = Data.ReferenceNo, Mode = Data.Mode, FK_Company = _userLoginInfo.FK_Company }, companyKey: _userLoginInfo.CompanyKey);
            return Json(new { Lits }, JsonRequestBehavior.AllowGet);


        }

        // [HttpPost]
        public ActionResult GetProductSubDetailsusingReferenceNo(ServiceListModel.GetDataRefernceNo Data)
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



            ServiceListModel data = new ServiceListModel();

            var Lits = data.GetCustomerProductDetailss(input: new ServiceListModel.GetDataRefernceNo
            {
                ReferenceNo = Data.ReferenceNo,
                Mode = Data.Mode,
                FK_Company = _userLoginInfo.FK_Company

            }, companyKey: _userLoginInfo.CompanyKey);



            //get id

            List<ServiceListModel.CustomerSubproductView> inct = new List<ServiceListModel.CustomerSubproductView>();


            if (Lits.Data != null)
            {
                var moduleList = Lits.Data.Select(a => a.MasterProduct).Distinct().ToList();
                foreach (var i in moduleList)
                {

                    var module = Lits.Data.Where(a => a.MasterProduct == i).FirstOrDefault();

                    inct.Add(new ServiceListModel.CustomerSubproductView
                    {

                        MasterProduct = module.MasterProduct,

                        CustomerProductdetails = Lits.Data
                                        .Where(a => a.MasterProduct == i)
                                        .Select(b => new ServiceListModel.CustomerProductdetails
                                        {
                                            FK_Product = b.FK_Product,
                                            FK_Category = b.FK_Category,
                                            Product = b.Product,
                                            SLNo = b.SLNo,
                                            SNo = b.SNo,
                                            ServiceWarrantyExpireDate = b.ServiceWarrantyExpireDate,
                                            ReplacementWarrantyExpireDate = b.ReplacementWarrantyExpireDate,
                                            Warranty = b.Warranty,
                                            ID_CustomerWiseProductDetails = b.ID_CustomerWiseProductDetails,
                                            ReplacementWarrantyExpired = b.ReplacementWarrantyExpired,
                                            ServiceWarrantyExpired = b.ServiceWarrantyExpired,
                                            FK_ProductNumberingDetails = b.FK_ProductNumberingDetails,
                                            //Check = b.Check
                                        }).ToList()

                    });

                }
            }


            //create variable

            APIGetRecordsDynamic<ServiceListModel.CustomerSubproductView> output = new APIGetRecordsDynamic<ServiceListModel.CustomerSubproductView>();
            output.Process = Lits.Process;
            output.Data = inct;
            return Json(output, JsonRequestBehavior.AllowGet);


        }



        public ActionResult GetBindcomplaintlist(int categoryid, int productid)
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var data = Common.GetDataViaQuery<ServiceListModel.Complaints>(parameters: new APIParameters
            {
                TableName = "ComplaintCheckList CCL join ComplaintList CL on CCL.FK_Complaint= CL.ID_ComplaintList",
                SelectFields = "DISTINCT CL.CompntName AS ComplaintName,CL.ID_ComplaintList AS ID_ComplaintList",
                Criteria = "CCL.Cancelled =0 AND CCL.Passed=1 AND CCL.FK_Category='" + categoryid + "'" + " OR CCL.FK_Product=" + productid + " AND CCL.FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },


          companyKey: _userLoginInfo.CompanyKey
       );



            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductSubDetails(ServiceListModel.ProductSubDetailstobind data)
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
            ServiceListModel serviceList = new ServiceListModel();


            var datresponse = serviceList.ProductSubdetailsData(input: new ServiceListModel.ProductSubDetailstobindinput
            {

                ProductSubDetails = data.ProductSubDetails is null ? "" : Common.xmlTostring(data.ProductSubDetails),
                FK_Employee = _userLoginInfo.FK_Employee

            }, companyKey: _userLoginInfo.CompanyKey);


            List<ServiceListModel.Subproductreplacedetails> inct = new List<ServiceListModel.Subproductreplacedetails>();

            if (datresponse.Data != null)
            {
                var moduleList = datresponse.Data.Select(a => a.ID_MasterProduct).Distinct().ToList();
                foreach (var i in moduleList)
                {

                    var module = datresponse.Data.Where(a => a.ID_MasterProduct == i).FirstOrDefault();

                    inct.Add(new ServiceListModel.Subproductreplacedetails
                    {

                        MainProduct = module.MainProduct,
                        ID_MasterProduct = module.ID_MasterProduct,

                        Subproductreplacedetailss = datresponse.Data
                                        .Where(a => a.ID_MasterProduct == i)
                                        .Select(b => new ServiceListModel.Subproductreplacedetailss
                                        {
                                            ID_MasterProduct = i,
                                            ID_Product = b.ID_Product,
                                            Componant = b.Componant,
                                            WarrantyMode = b.WarrantyMode,
                                            ProductAmount = b.ProductAmount,
                                            ReplceMode = b.ReplceMode,

                                            FK_Stock = b.FK_Stock,

                                        }).ToList()

                    });
                }
            }

            //create variable

            APIGetRecordsDynamic<ServiceListModel.Subproductreplacedetails> output = new APIGetRecordsDynamic<ServiceListModel.Subproductreplacedetails>();
            output.Process = datresponse.Process;
            output.Data = inct;
            return Json(output, JsonRequestBehavior.AllowGet);
            //  return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindNextActionList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            //   ServiceListModel.ServiceListView Assign = new ServiceListModel.ServiceListView();
            var NextAction = Common.GetDataViaQuery<ServiceListModel.NextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction,NxtActnName,FK_ActionStatus",
                Criteria = "Cancelled=0  AND Passed=1 AND FK_ActionModule=2 AND FK_ActionStatus not in(0,1,3) AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },


     companyKey: _userLoginInfo.CompanyKey
    );
            var NextActionPageName = Common.GetDataViaQuery<ServiceListModel.NextAction>(parameters: new APIParameters
            {
                TableName = "MenuList",
                SelectFields = "MnuLstName NxtActnName",
                Criteria = "Cancelled=0  AND Passed=1 AND ControllerName='NextAction' AND FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },


     companyKey: _userLoginInfo.CompanyKey
    );                    
            ViewBag.NxtActnPageName = NextActionPageName.Data[0].NxtActnName;
            return Json(NextAction, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BindNextLeadActionList()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];

            var NextAcListLead = Common.GetDataViaQuery<ServiceListModel.LeadNextAction>(parameters: new APIParameters
            {
                TableName = "NextAction",
                SelectFields = "ID_NextAction ID_NextActionLead ,NxtActnName NxtActnNameLead ,NxtActnStage NxtActnStageLead",
                Criteria = "FK_ActionModule=1 AND Cancelled=0 AND Passed=1 AND FK_ActionModule=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_NextAction",
                GroupByFileds = ""
            },
          companyKey: _userLoginInfo.CompanyKey

    );

            return Json(NextAcListLead, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BindActiontypelist()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var actiontypelist = Common.GetDataViaQuery<ServiceListModel.ActionTypes>(parameters: new APIParameters
            {
                TableName = "ActionType",
                SelectFields = "ID_ActionType FK_ActionType,ActnTypeName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 AND FK_ActionModule=1",

                SortFields = "",
                GroupByFileds = ""
            },
                       companyKey: _userLoginInfo.CompanyKey
            );
            return Json(actiontypelist, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LoginEmployeename()
        {

            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            var EmpName = Common.GetDataViaQuery<ServiceListModel.EmployeeInfo>(parameters: new APIParameters
            {
                TableName = "Employee",
                SelectFields = "ID_Employee,EmpFName",
                Criteria = "ID_Employee=" + _userLoginInfo.FK_Employee + "  AND Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Employee",
                GroupByFileds = ""
            },
              companyKey: _userLoginInfo.CompanyKey

                 );
            return Json(EmpName, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ServiceFollowUpSave(ServiceListModel.ServiceListView data)
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
            if (data.AttendedEmployeeDetails == null)
            {
                _ErrorMessage.Add("Please Mark Attended Person"); IsValid = false;
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

            ServiceListModel serviceList = new ServiceListModel();

            if (IsValid == true)
            {
                var datresponse = serviceList.UpdateServiceListData(input: new ServiceListModel.UpdateServiceList
                {
                    UserAction = 1,
                    ID_ServiceFollowUp = 0,
                    FK_Customerserviceregister = data.FK_Customerserviceregister,
                    ID_CustomerServiceRegisterProductDetails = data.ID_CustomerServiceRegisterProductDetails,
                    StartingDate = data.StartingDate,
                    TotalSecurityAmount = data.TotalSecurityAmount,
                    ComponentCharge = data.ComponentCharge,
                    ServiceCharge = data.ServiceCharge,
                    OtherCharge = data.OtherCharge,
                    TotalAmount = data.NetAmount,
                    DiscountAmount = data.DiscountAmount,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = data.TransMode,
                    Debug = false,
                    OtherChgDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                    CustomerProductdetails = data.ServiceDetails is null ? "" : Common.xmlTostring(data.ServiceDetails),
                    Subproductreplacedetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                    AttendedEmployeeDetails = data.AttendedEmployeeDetails is null ? "" : Common.xmlTostring(data.AttendedEmployeeDetails),
                    Actionproductdetails = data.Actionproductdetails is null ? "" : Common.xmlTostring(data.Actionproductdetails),
                    FK_BillType = data.BillTypeID,
                    PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                    ServiceIncentiveDetails = data.ServiceIncentiveDetails is null ? "" : Common.xmlTostring(data.ServiceIncentiveDetails),
                    LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,

                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult ServiceFollowUpUpdate(ServiceListModel.ServiceListView data)
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
            if (data.AttendedEmployeeDetails == null)
            {
                _ErrorMessage.Add("Please Mark Attended Person"); IsValid = false;
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

            ServiceListModel serviceList = new ServiceListModel();

            if (IsValid == true)
            {
                var datresponse = serviceList.UpdateServiceListData(input: new ServiceListModel.UpdateServiceList
                {
                    UserAction = 2,
                    ID_ServiceFollowUp = data.ID_ServiceFollowUp,
                    FK_Customerserviceregister = data.FK_Customerserviceregister,
                    ID_CustomerServiceRegisterProductDetails = data.ID_CustomerServiceRegisterProductDetails,
                    StartingDate = data.StartingDate,
                    TotalSecurityAmount = data.TotalSecurityAmount,
                    ComponentCharge = data.ComponentCharge,
                    ServiceCharge = data.ServiceCharge,
                    OtherCharge = data.OtherCharge,
                    TotalAmount = data.NetAmount,
                    DiscountAmount = data.DiscountAmount,
                    FK_Company = _userLoginInfo.FK_Company,
                    FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser,
                    EntrBy = _userLoginInfo.EntrBy,
                    FK_Machine = _userLoginInfo.FK_Machine,
                    TransMode = data.TransMode,
                    Debug = false,
                    OtherChgDetails = data.OtherChgDetails is null ? "" : Common.xmlTostring(data.OtherChgDetails),
                    CustomerProductdetails = data.ServiceDetails is null ? "" : Common.xmlTostring(data.ServiceDetails),
                    Subproductreplacedetails = data.ProductDetails is null ? "" : Common.xmlTostring(data.ProductDetails),
                    AttendedEmployeeDetails = data.AttendedEmployeeDetails is null ? "" : Common.xmlTostring(data.AttendedEmployeeDetails),
                    Actionproductdetails = data.Actionproductdetails is null ? "" : Common.xmlTostring(data.Actionproductdetails),
                    FK_BillType = data.BillTypeID,
                    LastID = (data.LastID.HasValue) ? data.LastID.Value : 0,
                    PaymentDetail = data.PaymentDetail is null ? "" : Common.xmlTostring(data.PaymentDetail),
                    ServiceIncentiveDetails = data.ServiceIncentiveDetails is null ? "" : Common.xmlTostring(data.ServiceIncentiveDetails),


                }, companyKey: _userLoginInfo.CompanyKey);

                return Json(new { Process = datresponse }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { Process = new Output { IsProcess = false, Message = _ErrorMessage, Status = "Error" } }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetServicefollowupInfoByIDReturn(ServiceListModel.ServiceListView data)
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

            ServiceListModel servicefollowup = new ServiceListModel();
            List<ServiceListModel.CustomerSubproductView> inct = new List<ServiceListModel.CustomerSubproductView>();
            var servicefollowupInfo = servicefollowup.GetServiceProductReturnData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.SubtabledataSelect
            {
                FK_ServiceFollowUp = data.FK_ServiceFollowUp,
                PageMode = 1,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            if (servicefollowupInfo.Data != null)
            {
                var moduleList = servicefollowupInfo.Data.Select(a => a.MasterProduct).Distinct().ToList();

            
                foreach (var i in moduleList)
                {

                    var module = servicefollowupInfo.Data.Where(a => a.MasterProduct == i).FirstOrDefault();

                    inct.Add(new ServiceListModel.CustomerSubproductView
                    {

                        MasterProduct = module.MasterProduct,

                        CustomerProductdetails = servicefollowupInfo.Data
                                        .Where(a => a.MasterProduct == i)
                                        .Select(b => new ServiceListModel.CustomerProductdetails
                                        {
                                            FK_Product = b.FK_Product,
                                            FK_Category = b.FK_Category,
                                            Product = b.Product,
                                            SLNo = b.SLNo,
                                            SNo = b.SNo,
                                            ServiceWarrantyExpireDate = b.ServiceWarrantyExpireDate,
                                            ReplacementWarrantyExpireDate = b.ReplacementWarrantyExpireDate,
                                            Warranty = b.Warranty,
                                            ID_CustomerWiseProductDetails = b.ID_CustomerWiseProductDetails,
                                            FK_ProductNumberingDetails = b.FK_ProductNumberingDetails,
                                            Check = 1,
                                            ReplacementWarrantyExpired = b.ReplacementWarrantyExpired,
                                            ServiceWarrantyExpired = b.ServiceWarrantyExpired,
                                            Description = b.Description,
                                            FK_ComplaintList = b.FK_ComplaintList,
                                             FK_Brand=b.FK_Brand,
                                             FK_Subcategory=b.FK_SubCategory
                                        }).ToList()

                    });

                }
                ViewBag.FK_Category = inct[0].CustomerProductdetails[0].FK_Category;
                ViewBag.FK_SubCategory = inct[0].CustomerProductdetails[0].FK_Subcategory;
                ViewBag.ID_ComplaintList = inct[0].CustomerProductdetails[0].FK_ComplaintList;
                ViewBag.FK_Brand = inct[0].CustomerProductdetails[0].FK_Brand;
            }


            APIGetRecordsDynamic<ServiceListModel.CustomerSubproductView> output = new APIGetRecordsDynamic<ServiceListModel.CustomerSubproductView>();
            output.Process = servicefollowupInfo.Process;
            output.Data = inct;
            
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetServicefollowupInfoByIDReturnSubProduct(ServiceListModel.ServiceListView data)
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
            ServiceListModel serviceList = new ServiceListModel();


            var datresponse = serviceList.GetServiceProductReturnTab2Data(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.SubtabledataSelect
            {
                FK_ServiceFollowUp = data.FK_ServiceFollowUp,
                PageMode = 2,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });


            List<ServiceListModel.Subproductreplacedetails> inct = new List<ServiceListModel.Subproductreplacedetails>();

            if (datresponse.Data != null)
            {
                var moduleList = datresponse.Data.Select(a => a.ID_MasterProduct).Distinct().ToList();
                foreach (var i in moduleList)
                {

                    var module = datresponse.Data.Where(a => a.ID_MasterProduct == i).FirstOrDefault();

                    inct.Add(new ServiceListModel.Subproductreplacedetails
                    {

                        MainProduct = module.MainProduct,
                        ID_MasterProduct = module.ID_MasterProduct,

                        Subproductreplacedetailss = datresponse.Data
                                        .Where(a => a.ID_MasterProduct == i)
                                        .Select(b => new ServiceListModel.Subproductreplacedetailss
                                        {
                                            ID_MasterProduct = i,
                                            ID_Product = b.ID_Product,
                                            Componant = b.Componant,
                                            WarrantyMode = b.WarrantyMode,
                                            ProductAmount = b.ProductAmount,
                                            ReplceMode = b.ReplceMode,
                                            Quantity = b.Quantity,
                                            FK_Stock = b.FK_Stock,
                                            WarrantyType = b.WarrantyType,
                                            ReplceType = b.ReplceType,

                                        }).ToList()

                    });
                }
            }

            //create variable

            APIGetRecordsDynamic<ServiceListModel.Subproductreplacedetails> output = new APIGetRecordsDynamic<ServiceListModel.Subproductreplacedetails>();
            output.Process = datresponse.Process;
            output.Data = inct;

            var servicefollowupInfoAssignee = serviceList.GetAttendedEmployeeData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.SubtabledataSelect
            {
                FK_ServiceFollowUp = data.FK_ServiceFollowUp,
                PageMode = 3,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });
            if (servicefollowupInfoAssignee.Process.IsProcess)
            {

                servicefollowupInfoAssignee.Data = servicefollowupInfoAssignee.Data;
            }

            return Json(new { output, servicefollowupInfoAssignee }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetServiceActionTakenReturn(ServiceListModel.ServiceListView data)
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
            ServiceListModel serviceList = new ServiceListModel();

            var serviceActionfollowupInfo = serviceList.GetServiceActionTakenResultData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.SubtabledataSelect
            {
                FK_ServiceFollowUp = data.FK_ServiceFollowUp,
                PageMode = 4,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });


            return Json(new { serviceActionfollowupInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetServiceAllAmounts(ServiceListModel.ServiceListView data)
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
            ServiceListModel serviceList = new ServiceListModel();
            var servicefollowupInfoDetails = serviceList.GetServicefollowupData(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.ServicefollowupselectID
            {
                FK_ServiceFollowUp = data.FK_ServiceFollowUp,
                PageMode = 0,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });

            var OtherChargeData = serviceList.GetOthrChargeDetails(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.GetSubTableSales
            {
                FK_Transaction = data.FK_ServiceFollowUp,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode
            });

            var paymentdetailData = serviceList.GetPaymentselect(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.GetPaymentin
            {
                FK_Master = data.FK_ServiceFollowUp,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                TransMode = data.TransMode,
                FK_BranchCodeUser = _userLoginInfo.FK_BranchCodeUser
            });

            return Json(new { servicefollowupInfoDetails, OtherChargeData, paymentdetailData }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetServicefollowupInfoByIDReturnService(ServiceListModel.ServiceListView data)
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
            ServiceListModel serviceList = new ServiceListModel();


            var datresponse = serviceList.GetServiceProductReturnService(companyKey: _userLoginInfo.CompanyKey, input: new ServiceListModel.SubtabledataSelect
            {
                FK_ServiceFollowUp = data.FK_ServiceFollowUp,
                PageMode = 5,
                FK_Company = _userLoginInfo.FK_Company,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine
            });


            List<ServiceListModel.ProductServiceCostdetails> inct = new List<ServiceListModel.ProductServiceCostdetails>();

            if (datresponse.Data != null)
            {
                var moduleList = datresponse.Data.Select(a => a.ID_MasterProduct).Distinct().ToList();
                foreach (var i in moduleList)
                {

                    var module = datresponse.Data.Where(a => a.ID_MasterProduct == i).FirstOrDefault();

                    inct.Add(new ServiceListModel.ProductServiceCostdetails
                    {

                        MainProduct = module.MainProduct,
                        ID_MasterProduct = module.ID_MasterProduct,

                        ProductServiceCostdetailss = datresponse.Data
                                        .Where(a => a.ID_MasterProduct == i)
                                        .Select(b => new ServiceListModel.ProductServiceCostdetailss
                                        {
                                            ID_MasterProduct = i,
                                            MainProduct = module.MainProduct,
                                            FK_Service = b.FK_Service,
                                            Service = b.Service,
                                            ServiceType = b.ServiceType,
                                            ServiceCost = b.ServiceCost,
                                            ServiceTax = b.ServiceTax,
                                            ServiceNetAmount = b.ServiceNetAmount,
                                            ServiceRemarks = b.ServiceRemarks,
                                        }).ToList()

                    });
                }
            }

            APIGetRecordsDynamic<ServiceListModel.ProductServiceCostdetails> output = new APIGetRecordsDynamic<ServiceListModel.ProductServiceCostdetails>();
            output.Process = datresponse.Process;
            output.Data = inct;
            var ServiceMod = Common.GetDataViaProcedure<ServiceListModel.Servicetype, ServiceListModel.ServiceModeInput>(companyKey: _userLoginInfo.CompanyKey, procedureName: "ProCommonPopupValues", parameter: new ServiceListModel.ServiceModeInput { Mode = 57 });
            return Json(new { output, ServiceMod }, JsonRequestBehavior.AllowGet);
        }
    }



}


