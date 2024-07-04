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
    public class EMIDetailsGridRptController : Controller
    {
        // GET: EMIDetailsGridRpt
        public ActionResult EMIDetailsGridRpt()
        {
            UserLoginInfo _userLoginInfo = (UserLoginInfo)Session["UserLoginInfo"];
            ViewBag.Username = _userLoginInfo.UserName;
            ViewBag.UserRole = _userLoginInfo.UserRole;
            ViewBag.UserAvatar = _userLoginInfo.UserAvatar;
            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;                
            return View();
        }
        public ActionResult LoadFormEMIMonitoring()
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

            EMIDetailsGridModel.EmiViewList EmiObj = new EMIDetailsGridModel.EmiViewList();
            EMIDetailsGridModel Modal = new EMIDetailsGridModel();

            EmiObj.UserCode = _userLoginInfo.EntrBy;
            EmiObj.Branch = _userLoginInfo.FK_Branch;

            var Category = Common.GetDataViaQuery<EMIDetailsGridModel.Category>(parameters: new APIParameters
            {
                TableName = "Category",
                SelectFields = "ID_Category AS ID_Catg ,CatName AS CatgName, Project",
                Criteria = "Cancelled=0 AND Passed=1 AND FK_Company=" + _userLoginInfo.FK_Company,
                SortFields = "ID_Category",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            EmiObj.CategoryList = Category.Data;

            var BranchList = Common.GetDataViaQuery<EMIDetailsGridModel.Branch>(parameters: new APIParameters
            {
                TableName = "Branch",
                SelectFields = "ID_Branch,BrName BranchName",
                Criteria = "FK_Company=" + _userLoginInfo.FK_Company + " AND  cancelled = 0 AND Passed = 1 ",
                SortFields = "",
                GroupByFileds = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            EmiObj.BranchList = BranchList.Data;

            var FinancePlan = Common.GetDataViaQuery<EMIDetailsGridModel.FinancePlanType>(parameters: new APIParameters
            {
                TableName = "FinancePlanType FPT",
                SelectFields = "FPT.ID_FinancePlanType AS FinancePlanTypeID,FPT.FpName AS FinanceName",
                Criteria = "FPT.Cancelled=0 AND FPT.Passed=1 AND FPT.FK_Company=" + _userLoginInfo.FK_Company,
                GroupByFileds = "",
                SortFields = ""
            },
            companyKey: _userLoginInfo.CompanyKey);
            EmiObj.FinancePlanlists = FinancePlan.Data;
 
            var rolemodeList = Modal.GeLeadStatusList(input: new EMIDetailsGridModel.ModeLead { Mode = 103 }, companyKey: _userLoginInfo.CompanyKey);
            

            EmiObj.ActionStatusList = rolemodeList.Data;

            ViewBag.FK_Branch = _userLoginInfo.FK_Branch;

            return PartialView("_EMIDetailsGridRptForm", EmiObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult GetEMICollectionListOnLoad(EMIDetailsGridModel.EmiDetails data)
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

            EMIDetailsGridModel EmiGrid = new EMIDetailsGridModel();


            var outputList = EmiGrid.GeEMIGridTodaysData(companyKey: _userLoginInfo.CompanyKey, input: new EMIDetailsGridModel.GetEmiDetails
            {
                Mode = 0,
                FK_FinancePlanType=data.FK_FinancePlanType,
                FK_Product=data.FK_Product, 
                FK_Branch=data.FK_Branch,
                FK_Company =_userLoginInfo.FK_Company,
                fk_Customer =data.FK_Customer,
                CedEMINo =data.EMINo,
                Status =0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                Pageindex = 0,
                PageSize = 0,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Area = data.FK_Area,
                FK_District = data.FK_District,
                FK_Category = data.FK_Category, 
                Demand =data.Demand,
            });

            return Json(outputList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetEmiList(EMIDetailsGridModel.EmiDetails data)
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


            EMIDetailsGridModel EmiGrid = new EMIDetailsGridModel();


            var outputList = EmiGrid.GeEMIGridTodaysData(companyKey: _userLoginInfo.CompanyKey, input: new EMIDetailsGridModel.GetEmiDetails
            {
                Mode = data.Mode,
                FK_FinancePlanType = data.FK_FinancePlanType,
                FK_Product = data.FK_Product,
                FK_Branch = data.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                fk_Customer = data.FK_Customer,
                CedEMINo = data.EMINo,
                Status = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Area = data.FK_Area,
                FK_District = data.FK_District,
                FK_Category = data.FK_Category,
                Demand = data.Demand,
                Pageindex = data.PageIndex + 1,
                PageSize = data.PageSize,
            });
            return Json(new { outputList.Process, outputList.Data, data.PageSize, data.PageIndex, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

        }


        #region [SendIntimation]
        [HttpPost]
        public ActionResult SendIntimation(EMIDetailsGridModel.EmiDetails data)
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


            EMIDetailsGridModel EmiGrid = new EMIDetailsGridModel();


            var outputList = EmiGrid.SendIndimation(companyKey: _userLoginInfo.CompanyKey, input: new EMIDetailsGridModel.GetEmiDetails
            {
                Mode = data.Mode,
                FK_FinancePlanType = data.FK_FinancePlanType,
                FK_Product = data.FK_Product,
                FK_Branch = data.FK_Branch,
                FK_Company = _userLoginInfo.FK_Company,
                fk_Customer = data.FK_Customer,
                CedEMINo = data.EMINo,

                Status = 0,
                EntrBy = _userLoginInfo.EntrBy,
                FK_Machine = _userLoginInfo.FK_Machine,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                FK_Area = data.FK_Area,
                FK_District = data.FK_District,
                FK_Category = data.FK_Category,
                Demand = data.Demand,
                Pageindex = data.PageIndex + 1,
                PageSize = data.PageSize,
                SMSMode= data.SMSMode
            });
            //return Json(new { outputList.Process, outputList.Data, data.PageSize, data.PageIndex, totalrecord = (outputList.Data is null) ? 0 : outputList.Data[0].TotalCount }, JsonRequestBehavior.AllowGet);

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
                        returnData.Count = item.Count ?? 0;
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
                Istrue = objMail.SendMailDatabulk(List, _userLoginInfo.FK_Company, "AMC", _userLoginInfo.CompanyKey, "");
                
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
                Istrue = objMail.SendMailDatabulk(List, _userLoginInfo.FK_Company, "AMC", _userLoginInfo.CompanyKey, "");

                if (Istrue)
                {
                    var countobj1 = new ReturnCountObj
                    {
                        EmailCount = ReturnValue,
                        SMSCount = SMSCount2
                    };
                    returnData.Process = true;
                    returnData.CountObj = countobj1;
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