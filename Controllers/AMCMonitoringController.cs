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
using static PerfectWebERP.Models.AMCMonitoringModel;
using static PerfectWebERP.Models.MailModel;

namespace PerfectWebERP.Controllers
{
    public class AMCMonitoringController : Controller
    {
        // GET: AMCCollection
        public ActionResult AMCMonitoring(string mtd)
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;           
            ViewBag.mtd = mtd;
            return View();
        }

        public ActionResult LoadFormAMCMonitoring(string mtd)
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
            AMCMonitoringModel.AMCMonitoringViewList AmcObj = new AMCMonitoringModel.AMCMonitoringViewList();

            AmcObj.Branch = _userLoginInfo.FK_Branch;
            AmcObj.FK_Employee = _userLoginInfo.FK_Employee;
            // if any data need to be displayed with the form, then get the data and pass it a model with the partial view and handle that data in view       

            var Category = Common.GetDataViaQuery<AMCMonitoringModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            AmcObj.CategoryList = Category.Data;

            var CompanyList = Common.GetDataViaQuery<AMCMonitoringModel.AMCMonitoringViewList>(parameters: new APIParameters
            {
                TableName = "Company",
                SelectFields = "CompName AS CompName",
                Criteria = "ID_Company=" + _userLoginInfo.FK_Company + "AND  cancelled = 0 AND Passed = 1 ",

                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            AmcObj.CompName = CompanyList.Data[0].CompName;

            var Branch = Common.GetDataViaQuery<AMCMonitoringModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch,BrName AS BranchName",
                Criteria = "Cancelled = 0 AND Passed = 1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            AmcObj.BranchList = Branch.Data;

            var amctype = Common.GetDataViaQuery<AMCTypeModel.AMCTypeView>(parameters: new APIParameters
            {
                TableName = "AMCType",
                SelectFields = "ID_AMCType as AMCTypeID,AMCName as AMCName",
                Criteria = "Cancelled=0 AND Passed =1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "",
                GroupByFileds = ""
            },
           companyKey: _userLoginInfo.CompanyKey);
           AmcObj.AMCtype = amctype.Data;

            AMCMonitoringModel objpaymode = new AMCMonitoringModel();
            var rolemodeList = objpaymode.GeLeadStatusList(input: new AMCMonitoringModel.ModeLead { Mode = 103 }, companyKey: _userLoginInfo.CompanyKey);
             AmcObj.ActionStatusList = rolemodeList.Data;
            EMIDetailsGridModel eMIDetailsGridModel = new EMIDetailsGridModel();
            CommonMethod objCmnMethod = new CommonMethod();
            ViewBag.PageTitle = objCmnMethod.DecryptString(mtd);
            return PartialView("_AMCMonitoringForm", AmcObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetAMCMonitoringListOnLoad(AMCMonitoringModel.AmcDetailsdata data)
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

            AMCMonitoringModel amc = new AMCMonitoringModel();

            var outputList = amc.GetAmcData(companyKey: _userLoginInfo.CompanyKey, input: new AMCMonitoringModel.GetAMCDetails
            {
                AMCNo = data.AMCNo,
                FK_Product = data.FK_Product,
                FK_Branch = data.FK_Branch,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_AMCType = data.FK_AMCType,
                FK_Category = data.FK_Category,
                FK_Area = data.FK_Area,
                fk_Customer = data.fk_Customer,
                Status = data.Status,
                Demand = data.Demand,
                Mode = data.Mode,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
            });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetLeadGenList(AMCMonitoringModel.AmcDetailsdata data)
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

            AMCMonitoringModel amc = new AMCMonitoringModel();


            var outputList = amc.GetAmcData(companyKey: _userLoginInfo.CompanyKey, input: new AMCMonitoringModel.GetAMCDetails
            {
                AMCNo = data.AMCNo,
                FK_Product = data.FK_Product,
                FK_Branch = data.FK_Branch,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_AMCType = data.FK_AMCType,
                FK_Category = data.FK_Category,
                FK_Area = data.FK_Area,
                fk_Customer = data.fk_Customer,
                Status = data.Status,
                Demand = data.Demand,
                Mode = data.Mode,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = data.PageIndex + 1,
                PageSize = data.PageSize,
                SMSMode=0
                
            });
            return Json(new { outputList.Process, outputList.Data, data.PageSize, data.PageIndex, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }

        #region [SendIntimation]
        [HttpPost]
        public ActionResult SendIntimation(AMCMonitoringModel.SendIntimate data)
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

            AMCMonitoringModel amc = new AMCMonitoringModel();


            var outputList = amc.SendIndimation(companyKey: _userLoginInfo.CompanyKey, input: new AMCMonitoringModel.SendIntimate
            {
                AMCNo = data.AMCNo,
                FK_Product = data.FK_Product,
                FK_Branch = data.FK_Branch,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_AMCType = data.FK_AMCType,
                FK_Category = data.FK_Category,
                FK_Area = data.FK_Area,
                fk_Customer = data.fk_Customer,
                Status = data.Status,
                Demand = data.Demand,
                Mode = data.Mode,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Company = _userLoginInfo.FK_Company,
                PageIndex = 0,
                PageSize = 0,
                SMSMode = data.SMSMode

            });

            DataTable table = outputList.Data;
            

            List<SendIntimationTable> List = new List<SendIntimationTable>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow dr in table.Rows)
                {

                    var input = new AMCMonitoringModel.SendIntimationTable
                    {
                        Content = DBNull.Value.Equals(dr["Content"]) ? default(string) : Convert.ToString(dr["Content"]),
                        Count = DBNull.Value.Equals(dr["Count"]) ? default(int) : Convert.ToInt32(dr["Count"]),
                        Email_ID = DBNull.Value.Equals(dr["Email_ID"]) ? default(string) : Convert.ToString(dr["Email_ID"]),
                        Mode = DBNull.Value.Equals(dr["Mode"]) ? default(int) : Convert.ToInt32(dr["Mode"]),
                        Subject = DBNull.Value.Equals(dr["Subject"]) ? default(string) : Convert.ToString(dr["Subject"]),
                        FK_Master = DBNull.Value.Equals(dr["FK_Master"]) ? default(Int64) : Convert.ToInt64(dr["FK_Master"]),
                    };
                    List.Add(input);
                };

            };
            Int64 ReturnValue;
            bool Istrue;

            
            returnRootmodal returnData = new returnRootmodal();
            returnData.Count = 0;
            if (data.SMSMode == 1)
                {
                    //SMS only
                    foreach (var item in List)
                    {
                        if (item.Mode == 1)
                        {
                        returnData.Count = item.Count??0;
                        }
                    }
                    returnData.Process = true;
                   
                 }
                else if (data.SMSMode == 2)
                {
                    //Emial only;
                    ReturnValue = 0;
                    foreach (var item in List)
                    {
                        if (item.Mode == 2)
                        {
                           
                            ReturnValue += 1;
                        }
                    };
                    sendMail objMail = new sendMail();
                Istrue= objMail.SendMailDatabulk(List, _userLoginInfo.FK_Company, "AMC", _userLoginInfo.CompanyKey, "");
                
                if (Istrue)
                    {
                        returnData.Process = true;
                        returnData.Count = ReturnValue;
                    }
                    else
                    {
                    returnData.Process = false;
                    
                     }

                }
                else if (data.SMSMode == 3)
                {
                    Int64? SMSCount2 = 0;
                    //SMS and Email both 
                    //Sms
                    foreach (var item in List)
                    {
                        if (item.Mode == 1)
                        {
                            SMSCount2 = item.Count;
                        }
                    }

                    //Email
                    ReturnValue = 0;
                    foreach (var item in List)
                    {
                        if (item.Mode == 2)
                        {
                           
                            ReturnValue += 1;
                        }
                    };

                    sendMail objMail = new sendMail();
                    Istrue= objMail.SendMailDatabulk(List, _userLoginInfo.FK_Company, "AMC", _userLoginInfo.CompanyKey, "");


                    if (Istrue)
                    {
                        var countobj = new ReturnCountObj
                        {
                            EmailCount = ReturnValue,
                            SMSCount = SMSCount2
                        };

                        returnData.Process = true;
                        returnData.CountObj = countobj;
                    }
                    else
                    {
                        returnData.Process = false;
                     }
                    }
                    else
                    {
                        returnData.Process = false;
                      
                    }



            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
